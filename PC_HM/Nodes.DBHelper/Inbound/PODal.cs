using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 入库单据处理
    /// </summary>
    public class PODal
    {
        /// <summary>
        /// 保存编辑的采购单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="commitNow">false：存为草稿；true：直接提交</param>
        /// <param name="creator">创建者姓名</param>
        /// <returns>-1：单据状态不允许编辑；0：更新表头时失败；1：成功</returns>
        public string SaveBill(POBodyEntity bill, bool commitNow, string creator, out string errMsg)
        {
            //记录详细的错误，例如具体是哪个物料不存在了等等
            errMsg = string.Empty;

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("P_BILL_ID", bill.BillID);
                parms.Add("P_ORG_CODE", bill.OrgCode);

                //直接提交还是另存为草稿
                if (commitNow)
                    parms.Add("P_BILL_STATE", BillStateConst.PO_STATE_CODE_COMMITED);
                else
                    parms.Add("P_BILL_STATE", BillStateConst.PO_STATE_CODE_DRAFT);

                parms.Add("P_SOURCE_BILL_ID", bill.SourceBillID);
                parms.Add("P_SALES", bill.Sales);
                parms.Add("P_SUPPLIER", bill.Supplier);
                parms.Add("P_BILL_TYPE", bill.BillType);
                parms.Add("P_CONTRACT_NO", bill.ContractNO);
                parms.Add("P_REMARK", bill.Remark);
                parms.Add("P_PO_STR1", bill.PO_STR1);
                parms.Add("P_PO_STR2", bill.PO_STR2);
                parms.Add("P_PO_STR3", bill.PO_STR3);
                parms.Add("P_PO_STR4", bill.PO_STR4);
                parms.Add("P_PO_NUM1", bill.PO_NUM1);
                parms.Add("P_PO_NUM2", bill.PO_NUM2);
                parms.Add("P_PO_DATE1", bill.PO_DATE1);
                parms.Add("P_PO_DATE2", bill.PO_DATE2);
                parms.Add("P_CREATOR", creator);
                parms.AddOut("P_NEW_BILL_ID", DbType.String, 50);
                parms.AddOut("P_RET_VAL", DbType.String, 2);

                //先写入主表
                map.Execute("P_PO_SAVE_HEADER", parms, trans, CommandType.StoredProcedure);

                //获取返回值，只有1表示成功
                string retVal = parms.Get<string>("P_RET_VAL");
                if (retVal != "1")
                {
                    trans.Rollback();
                    return retVal;
                }

                //保存明细
                string newBillID = parms.Get<string>("P_NEW_BILL_ID");

                parms = new DynamicParameters();
                parms.Add("P_BILL_ID", newBillID);
                parms.Add("P_MTL_CODE");
                parms.Add("P_QTY");
                parms.Add("P_PRICE");
                parms.Add("P_REMARK");
                parms.AddOut("P_RET_VAL", DbType.String, 2);

                //再写明细
                foreach (PODetailEntity line in bill.Details)
                {
                    parms.Set("P_MTL_CODE", line.MaterialCode);
                    parms.Set("P_QTY", line.PlanQty);
                    parms.Set("P_PRICE", line.Price);
                    parms.Set("P_REMARK", line.Remark);

                    map.Execute("P_PO_SAVE_DETAIL", parms, trans, CommandType.StoredProcedure);
                    retVal = parms.Get<string>("P_RET_VAL");
                    if (retVal != "1")
                    {
                        if (retVal == "11")
                            errMsg = line.MaterialCode;

                        trans.Rollback();
                        break;
                    }
                }

                if (retVal == "1")
                {
                    trans.Commit();

                    if (commitNow)
                    {
                        bill.BillState = BillStateConst.PO_STATE_CODE_COMMITED;
                        bill.BillStateDesc = BillStateConst.PO_STATE_DESC_COMMITED;
                    }
                    else
                    {
                        bill.BillState = BillStateConst.PO_STATE_CODE_DRAFT;
                        bill.BillStateDesc = BillStateConst.PO_STATE_DESC_DRAFT;
                    }

                    bill.BillID = newBillID;
                }

                return retVal;
            }
            catch (Exception ex) //只能是insert语句产生了异常，先回滚再抛出异常信息
            {
                trans.Rollback();
                throw ex;
            }
        }

        #region 删除、提交、取消提交、审批、反审
        /// <summary>
        /// 取消提交，将单据变回草稿状态
        /// </summary>
        /// <param name="billID">单据ID</param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是提交；-3：更新失败，可能网络故障、锁表或其他未知原因；</returns>
        public string CancelCommit(List<POBodyEntity> focusedHeaders, string userName, out POBodyEntity errHeader)
        {
            errHeader = null;
            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_CANCEL_COMMIT", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 提交待审
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <param name="errHeader"></param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是提交；-3：更新失败，可能网络故障、锁表或其他未知原因；</returns>
        public string Commit(List<POBodyEntity> focusedHeaders, string userName, out POBodyEntity errHeader)
        {
            errHeader = null;
            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_COMMIT_BILL", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 删除选中的单据，状态必须为草稿
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName"></param>
        /// <param name="errHeader"></param>
        /// <returns>1：成功；0：未知；-1：单据未找到；-2：不是草稿；-3：删除明细时，一行都没受影响；-4：删除主表时，一行都没受影响</returns>
        public string Delete(List<POBodyEntity> focusedHeaders, string userName, out POBodyEntity errHeader)
        {
            errHeader = null;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_DEL_BILL", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="billID">单据ID</param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <param name="userCode">操作者编号，会存放到表体中，记录审批人</param>
        /// <param name="errHeader">若返回值不是1，记录错误的单据；成功则返回null</param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是提交；-3：更新失败，可能网络故障或其他未知原因；</returns>
        public string FirstApprove(List<POBodyEntity> focusedHeaders, string userName, string userCode, out POBodyEntity errHeader)
        {
            errHeader = null;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.Add("P_USER_CODE", userCode);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_FIRST_APPROVE", parms, tran, CommandType.StoredProcedure);
                    
                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 反审：不能是已经开始收货的，必须是自己审核的，不能反审别人审核通过的
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CancelFirstApprove(List<POBodyEntity> focusedHeaders, string userName, string userCode, out POBodyEntity errHeader)
        {
            errHeader = null;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.Add("P_USER_CODE", userCode);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_CANCEL_FIRST_APPROVE", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 二审
        /// </summary>
        /// <param name="focusedHeaders">单据集合</param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <param name="userCode">操作者编号，记录审核人，取消审核的时候会用到</param>
        /// <param name="errHeader">若返回值不是1，记录错误的单据；成功则返回null</param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是提交；-3：更新失败，可能网络故障或其他未知原因；</returns>
        public string SecondApprove(List<POBodyEntity> focusedHeaders, string userName, string userCode, out POBodyEntity errHeader)
        {
            errHeader = null;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.Add("P_USER_CODE", userCode);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_SECOND_APPROVE", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 反审：不能是已经开始收货的，必须是自己审核的，不能反审别人审核通过的
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CancelSecondApprove(List<POBodyEntity> focusedHeaders, string userName, string userCode, out POBodyEntity errHeader)
        {
            errHeader = null;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("P_BILL_ID");
            parms.Add("P_USER_NAME", userName);
            parms.Add("P_USER_CODE", userCode);
            parms.AddOut("P_RET_VAL", DbType.String, 2);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                string result = string.Empty;
                foreach (POBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_PO_CANCEL_SECOND_APPROVE", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("P_RET_VAL");
                    if (result != "1")
                    {
                        errHeader = header;
                        break;
                    }
                }

                if (result == "1")
                    tran.Commit();
                else
                    tran.Rollback();

                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 修改单据的备注（含备注和字体颜色）
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="colorArgb"></param>
        /// <returns>1：成功</returns>
        public int UpdateRemark(string billID, string remark, int? colorArgb, string userName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("UPDATE PO_HEADER SET REMARK = @Remark, ROW_COLOR = @Color, UPDATE_DATE = {0}, " +
                "UPDATE_BY = @UserName WHERE BILL_ID = @BillID", map.GetSysDateString());
            return map.Execute(sql, new { Remark = remark, Color = colorArgb, UserName = userName, BillID = billID });
        }
    }
}
