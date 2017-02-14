using System;
using System.Collections.Generic;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 到货通知单数据访问类
    /// </summary>
    public class AsnDal
    {
        private const string SELECT_ASN_BODY = "SELECT BILL_ID, BILL_NO, ASN_TYPE, " +
                "ASN_TYPE_NAME, BILL_STATE, STATUS_NAME, INBOUND_TYPE, INBOUND_TYPE_NAME, " +
                "SALES_MAN, CONTRACT_NO, ARRIVE_DATE, SUPPLIER, SUPPLIER_NAME, " +
                "CLOSE_DATE, REMARK, CREATE_DATE, PRINTED, WMS_REMARK, ROW_COLOR " +
                "FROM V_ASN_HDR ";

        /// <summary>
        /// 保存编辑的采购单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="creator">创建者姓名</param>
        /// <returns>-1：单据状态不允许编辑；0：更新表头时失败；1：成功</returns>
        public string SaveBill(AsnBodyEntity bill, string creator, out string errMsg)
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
                parms.Add("P_BILL_TYPE", bill.BillType);
                parms.Add("P_INSTORE_TYPE", bill.InstoreType);
                parms.Add("P_PO_NO", bill.OriginalBillNO);
                parms.Add("P_CONTRACT_NO", bill.ContractNO);
                parms.Add("P_SALES", bill.Sales);
                parms.Add("P_SUPPLIER", bill.SupplierCode);
                parms.Add("P_REMARK", bill.Remark);
                parms.Add("P_CREATOR", creator);
                parms.AddOut("P_NEW_BILL_ID", DbType.String, 50);
                parms.AddOut("P_RET_VAL", DbType.String, 2);

                //先写入主表
                map.Execute("P_ASN_SAVE_HEADER", parms, trans, CommandType.StoredProcedure);

                //获取返回值，只有1表示成功
                string retVal = parms.Get<string>("P_RET_VAL");
                if (retVal != "1")
                {
                    trans.Rollback();
                    return retVal;
                }

                //保存明细
                int newBillID = parms.Get<int>("P_NEW_BILL_ID");

                parms = new DynamicParameters();
                parms.Add("P_BILL_ID", newBillID);
                parms.Add("P_MTL_CODE");
                parms.Add("P_QTY");
                parms.Add("P_PRICE");
                parms.Add("P_LOT_NO");
                parms.Add("P_EXP_DATE");
                parms.Add("P_REMARK");
                parms.AddOut("P_RET_VAL", DbType.String, 2);

                //再写明细
                foreach (PODetailEntity line in bill.Details)
                {
                    parms.Set("P_MTL_CODE", line.MaterialCode);
                    parms.Set("P_QTY", line.PlanQty);
                    //parms.Set("P_PRICE", line.Price);
                    parms.Set("P_LOT_NO", line.BatchNO);
                    parms.Set("P_EXP_DATE", line.ExpDate);
                    parms.Set("P_REMARK", line.Remark);

                    map.Execute("P_ASN_SAVE_DETAIL", parms, trans, CommandType.StoredProcedure);
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

                    bill.BillState = BillStateConst.ASN_STATE_CODE_NOT_ARRIVE;
                    bill.BillStateDesc = BillStateConst.ASN_STATE_DESC_NOT_ARRIVE;

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

        /// <summary>
        /// 删除选中的单据，状态必须为草稿
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName"></param>
        /// <param name="errHeader"></param>
        /// <returns>1：成功；0：未知；-1：单据未找到；-2：不是草稿；-3：删除明细时，一行都没受影响；-4：删除主表时，一行都没受影响</returns>
        public string Delete(List<AsnBodyEntity> focusedHeaders, string userName, out AsnBodyEntity errHeader)
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
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_ASN_DEL_BILL", parms, tran, CommandType.StoredProcedure);

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
        /// 收货完成
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <param name="errHeader"></param>
        /// <returns>Y:成功；其他：失败</returns>
        public string ReceivedComplete(List<AsnBodyEntity> focusedHeaders, string userName, string authUserCode)
        {
            string result = "";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID");
            parms.Add("V_USER_NAME", userName);
            parms.Add("V_AUTH_USER_CODE", authUserCode);
            parms.AddOut("V_RESULT", DbType.String, 3000);

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction tran = map.BeginTransaction();

            try
            {
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    parms.Set("V_BILL_ID", header.BillID);
                    map.Execute("P_ASN_CLOSE", parms, tran, CommandType.StoredProcedure);

                    result = parms.Get<string>("V_RESULT");
                    if (result != "Y")
                    {
                        break;
                    }
                }

                if (result == "Y")
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
        /// 到货确认
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <param name="errHeader"></param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是等待到货确认；-3：更新失败，可能网络故障、锁表或其他未知原因；</returns>
        public string ReceivedConfirm(List<AsnBodyEntity> focusedHeaders, string userName, out AsnBodyEntity errHeader)
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
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_ASN_RECEIVED_CONFIRM", parms, tran, CommandType.StoredProcedure);

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
        /// 取消到货确认，将单据变回等到到货确认状态
        /// </summary>
        /// <param name="billID">单据ID</param>
        /// <param name="userName">操作者姓名，用于记录日志</param>
        /// <returns>1：成功；0：未知；-1：单据不存在；-2：单据状态不是提交；-3：更新失败，可能网络故障、锁表或其他未知原因；</returns>
        public string CancelReceivedConfirm(List<AsnBodyEntity> focusedHeaders, string userName, out AsnBodyEntity errHeader)
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
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    map.Execute("P_ASN_CANCEL_CONFIRM", parms, tran, CommandType.StoredProcedure);

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
        public string CancelCheck(List<AsnBodyEntity> focusedHeaders, string userName, string userCode, out AsnBodyEntity errHeader)
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
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    parms.Set("P_BILL_ID", header.BillID);
                    //map.Execute("P_PO_CANCEL_FIRST_APPROVE", parms, tran, CommandType.StoredProcedure);

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
        /// 修改单据的备注（含备注和字体颜色）
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="colorArgb"></param>
        /// <returns>1：成功</returns>
        public int UpdateRemark(int billID, string remark, int? colorArgb, string userName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "UPDATE WM_ASN_HEADER SET WMS_REMARK = @Remark, ROW_COLOR = @Color WHERE BILL_ID = @BillID";
            return map.Execute(sql, new { Remark = remark, Color = colorArgb, BillID = billID });
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// 获取单据的最新信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public AsnHeaderEntity GetHeaderInfoByBillID(int billID)
        {
            string sql = SELECT_ASN_BODY + "WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<AsnHeaderEntity>(sql, new { BillID = billID });
        }

        public AsnHeaderEntity GetHeaderInfoByBillNO(string warehouse, string billNO)
        {
            string sql = SELECT_ASN_BODY + "WHERE WAREHOUSE = @Warehouse and BILL_NO = @BillNO";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<AsnHeaderEntity>(sql, new { Warehouse = warehouse, BillNO = billNO });
        }

        /// <summary>
        /// 按照库房、收货方式、状态（是小于某个状态）的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<AsnHeaderEntity> QueryBillsQuickly(string warehouse, string inboundType, DateTime? dateFrom, DateTime? dateTo)
        {
            string whereCondition = string.Format("WHERE WAREHOUSE = @Warehouse AND BILL_STATE < '{0}'", SysCodeConstant.ASN_STATUS_CLOSE);
            if (!string.IsNullOrEmpty(inboundType))
                whereCondition += string.Format(" AND INBOUND_TYPE = '{0}'", inboundType);

            if (dateFrom != null)
                whereCondition += string.Format(" AND CREATE_DATE >= '{0}'", dateFrom.Value);

            if (dateTo != null)
                whereCondition += string.Format(" AND CREATE_DATE <= '{0}'", dateTo.Value);

            string sql = SELECT_ASN_BODY + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnHeaderEntity>(sql,
                new
                {
                    Warehouse = warehouse
                });
        }

        public List<AsnHeaderEntity> QueryAsnBills(string billNO, string supplier, string saleMan, string billType,
            string billStatus, string inboundType, DateTime dateFrom, DateTime dateTo, string warehouse)
        {
            string sql = SELECT_ASN_BODY +
                "WHERE (@BillNO IS NULL OR BILL_NO = @BillNO) AND  " +
                "(@Warehouse IS NULL OR WAREHOUSE = @Warehouse) AND " +
                "(@AsnType IS NULL OR ASN_TYPE = @AsnType) AND " +
                "(@InboundType IS NULL OR INBOUND_TYPE = @InboundType) AND " +
                "(@SalesMan IS NULL OR SALES_MAN = @SalesMan) AND " +
                "(@Supplier IS NULL OR SUPPLIER = @Supplier) AND " +
                "(@StartTime IS NULL OR CREATE_DATE >= @StartTime) AND " +
                "(@EndTime IS NULL OR CREATE_DATE <= @EndTime)";
            if (!string.IsNullOrEmpty(billStatus))
                sql = string.Format(SELECT_ASN_BODY +
                "WHERE (@BillNO IS NULL OR BILL_NO = @BillNO) AND  " +
                "(BILL_STATE in ({0})) AND " +
                "(@Warehouse IS NULL OR WAREHOUSE = @Warehouse) AND " +
                "(@AsnType IS NULL OR ASN_TYPE = @AsnType) AND " +
                "(@InboundType IS NULL OR INBOUND_TYPE = @InboundType) AND " +
                "(@SalesMan IS NULL OR SALES_MAN = @SalesMan) AND " +
                "(@Supplier IS NULL OR SUPPLIER = @Supplier) AND " +
                "(@StartTime IS NULL OR CREATE_DATE >= @StartTime) AND " +
                "(@EndTime IS NULL OR CREATE_DATE <= @EndTime)", DBUtil.FormatParameter(billStatus));

            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnHeaderEntity>(sql,
                new
                {
                    BillNO = billNO,
                    Warehouse = warehouse,
                    AsnType = billType,
                    InboundType = inboundType,
                    SalesMan = saleMan,
                    Supplier = supplier,
                    StartTime = dateFrom,
                    EndTime = dateTo
                });
        }

        /// <summary>
        /// 获取一个单据的订购量
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int GetOrderQtyByBillID(int billID)
        {
            string sql = @"SELECT SUM(QTY) FROM ASN_DETAIL WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<int>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 持久化入库策略
        /// </summary>
        /// <param name="entity">到货通知到实体</param>
        /// <returns></returns>
        public int AsnUpdate(AsnHeaderEntity entity, string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            ret = map.Execute("UPDATE ASN_HEADER SET INBOUND_TYPE = @StrategyType WHERE BILL_ID = @BillId",
            new
            {
                StrategyType = entity.InstoreType,
                BillId = entity.BillID
            });

            return ret;
        }

        /// <summary>
        /// 越库入库确认操作
        /// </summary>
        /// <param name="entity">越库入库实体</param>
        /// <returns></returns>
        public string ExecuteCrossInstore(int billID, string location, List<ASNDetailEntity> details, string userName)
        {
            string errMsg = "";

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("USER_NAME", userName);
            parms.AddOut("RET_VAL", DbType.Int32);

            map.Execute("P_ASN_CROSS_INSTRORE", parms, trans, CommandType.StoredProcedure);
            int retVal = parms.Get<int>("RET_VAL");
            if (retVal == -1)
            {
                trans.Rollback();
                errMsg = "未查到该单据，该单据可能已经被其他人删除。";
            }
            else if (retVal == -2)
            {
                trans.Rollback();
                errMsg = "单据的状态必须为等待验收才能做越库收货操作。";
            }
            else if (retVal == -3)
            {
                trans.Rollback();
                errMsg = "单据的收货方式必须为越库，请重新确认单据信息无误后重试。";
            }
            else
            {
                parms = new DynamicParameters();
                parms.Add("DETAIL_ID");
                parms.Add("PUT_QTY");
                parms.Add("DUE_DATE");
                parms.Add("LOT_NO");
                parms.Add("STATUS");
                parms.Add("LOCATION", location);
                parms.Add("USER_NAME", userName);
                parms.AddOut("RET_VAL", DbType.Int32);

                foreach (ASNDetailEntity detail in details)
                {
                    parms.Set("DETAIL_ID", detail.DetailID);
                    parms.Set("PUT_QTY", detail.PutawayQty);
                    parms.Set("DUE_DATE", detail.ExpDate);
                    parms.Set("LOT_NO", detail.BatchNO);
                    parms.Set("STATUS", SysCodeConstant.SEQ_STATUS_QUALIFIED);
                    map.Execute("P_ASN_CROSS_INSTRORE_DETAIL", parms, trans, CommandType.StoredProcedure);
                    retVal = parms.Get<int>("RET_VAL");
                    if (retVal == -1)
                    {
                        errMsg = "未查到该单据，该单据可能已经被其他人删除。";
                        trans.Rollback();
                    }
                }

                trans.Commit();
            }

            return errMsg;
        }

        /// <summary>
        /// 读取单据最新状态
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public string GetStatus(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>("SELECT BILL_STATE FROM ASN_HEADER WHERE BILL_ID = @BillID", new { BillID = billID });
        }

        /// <summary>
        /// 读取单据最新的状态
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public AsnHeaderEntity GetLastestHeaderStatus(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<AsnHeaderEntity>("SELECT H.BILL_STATE, C.NAM STATUS_NAME FROM ASN_HEADER H INNER JOIN CODEITEM C ON H.BILL_STATE = C.COD WHERE H.BILL_ID = @BillID", new { BillID = billID });
        }

        /// <summary>
        /// 做货到确认
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int SetStatusToArriveConfirm(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("UPDATE ASN_HEADER SET BILL_STATE = @Status, ARRIVE_DATE = GETDATE() WHERE BILL_ID = @BillID",
                new { Status = SysCodeConstant.ASN_STATUS_AWAIT_CHECK, BillID = billID });
        }

        /// <summary>
        /// 更新单据状态为收货完成
        /// 检查：1、单据状态必须为正在验收
        /// 2、组分料拆分后的每一行验收数量必须相等
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userCode"></param>
        /// <returns>0：成功；-1：状态不是验收完成或正在上架 -2：验收数量必须等于上架数量 -3：组分料每一行验收数量不相等</returns>
        public int SetStatusToPutawayComplete(int billID, string userCode)
        {
            IMapper map = DatabaseInstance.Instance();

            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("USER_NAME", userCode);
            parms.AddOut("RET_VAL", DbType.Int32);

            map.Execute("P_ASN_PUTAWAY_COMPLETE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("RET_VAL");
        }

        /// <summary>
        /// 删除一个ASN单据，会自动将记录转移到历史表中
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="billNO"></param>
        /// <param name="userName"></param>
        public int DeleteASN(int billID, string billNO, string userName)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("BILL_NO", billNO);
            parms.Add("USER_NAME", userName);
            parms.AddOut("RET_VAL", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_ASN_DELETE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("RET_VAL");
        }


        public void UpdatePrintedFlag(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("UPDATE WM_ASN_HEADER SET PRINTED = 1,PRINTED_TIME=NOW() WHERE BILL_ID = @BillID", new { BillID = billID });
        }

        /// <summary>
        /// 列出验收与入库操作明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable ListInboundDetails(int billID)
        {
            string sql = "SELECT r.[SEQ], r.[LOCATION], d.QTY, r.[CHECK_QTY], " +
                "r.[PUT_QTY], r.[CHECK_DATE], r.[CHECK_MAN], r.[PUTAWAY_DATE], r.[PUTAWAY_MAN], " +
                "d.MATERIAL, m.NAM FROM ASN_CHECK_SEQ r " +
                "inner join ASN_DETAIL d on r.DETAIL_ID = d.DETAIL_ID " +
                "inner join MATERIAL m on d.MATERIAL = m.COD " +
                "where r.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        /// <summary>
        /// 列出没有上架的流水号
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public DataTable ListNotPutawaySeqs(int summaryID)
        {
            string sql = "SELECT R.[SEQ], R.[CHECK_QTY] - R.[PUT_QTY] QTY, R.[CHECK_DATE], R.[CHECK_MAN] " +
                "FROM ASN_CHECK_SEQ R INNER JOIN ASN_CHECK_SMRY S ON R.DETAIL_ID = S.DETAIL_ID " +
                "INNER JOIN SEQUENCES SQ ON SQ.SEQ = R.SEQ AND S.BATCH_NO = SQ.BATCH_NO AND S.DUE_DATE = SQ.DUE_DATE AND S.STAT = SQ.STAT " +
                "where S.ID = @ID and R.[CHECK_QTY] > R.[PUT_QTY]";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { ID = summaryID });
        }

        /// <summary>
        /// 列出验收与入库汇总
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable ListInboundSummary(int billID)
        {
            string sql = "SELECT d.ROW_NO, r.DUE_DATE, r.BATCH_NO, r.[CHECK_QTY], " +
                "r.[PUT_QTY], r.STAT, c.NAM STATUS_NAME, d.MATERIAL, m.NAM FROM ASN_CHECK_SMRY r " +
                "inner join ASN_DETAIL d on r.DETAIL_ID = d.DETAIL_ID " +
                "inner join CODEITEM c on r.STAT = c.COD " +
                "inner join MATERIAL m on d.MATERIAL = m.COD " +
                "where d.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        /// <summary>
        /// 获取SAP原始订单信息，只有一行数据
        /// </summary>
        /// <param name="billNO"></param>
        /// <returns></returns>
        public DataTable GetSapAsnBill(string billNO)
        {
            string sql = "SELECT BILL_NO, WAREHOUSE, DOC_TYPE, BASE_LINE, SALES_MAN, SUPPLIER, SUPPLIER_NAME, CREATE_DATE, ORIGINAL_BILL_NO, REMARK, CONTRACT_NO " +
                "FROM SAP_ASN_HEADER where BILL_NO = @BillNO";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillNO = billNO });
        }

        public DataTable GetSapAsnDetail(string billNO)
        {
            string sql = "SELECT BILL_NO, ROW_NO, MATERIAL, MATERIAL_NAME, UNIT, QTY, QUAL_QTY, UNQUAL_QTY, DUE_DATE, BATCH_NO, PRICE, FO_NUM, CONTRACT_NO " +
                "FROM SAP_ASN_DETAIL where BILL_NO = @BillNO";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillNO = billNO });
        }
        /// <summary>
        /// 获取订单对应的托盘使用记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetContainerStateByBillID(int billID)
        {
            string sql = string.Format(@"SELECT WAC.CT_CODE, WAC.SKU_CODE, ws.SKU_NAME, WAC.CREATE_DATE, WAC.CREATOR, U.USER_NAME, 
  WAC.CHECK_STATE, WAC.BUG_CODE, WAC.PRODUCT_DATE, WAC.QTY, WU.UM_NAME, WAC.QTY * WUS.QTY UM_QTY, 
  WCS.CT_STATE, CASE WHEN wah.BILL_STATE <>27 AND WCS.CT_STATE <>80 THEN WBC.ITEM_DESC 
  ELSE '已上架' END AS CT_STATE_NAME, WAC.CHECK_NAME  
  FROM WM_ASN_CONTAINER WAC 
  INNER JOIN wm_asn_header wah ON WAC.BILL_ID =wah.BILL_ID  
  INNER JOIN WM_UM_SKU WUS ON WAC.UM_SKU_ID = WUS.ID 
  INNER JOIN WM_SKU WS ON WAC.SKU_CODE = WS.SKU_CODE 
  INNER JOIN WM_UM WU ON WUS.UM_CODE = WU.UM_CODE 
  INNER JOIN WM_CONTAINER_STATE WCS ON WCS.CT_CODE = WAC.CT_CODE 
  INNER JOIN WM_BASE_CODE WBC ON WCS.CT_STATE = WBC.ITEM_VALUE 
  LEFT JOIN USERS U ON U.USER_CODE = WAC.CREATOR 
  WHERE WAC.BILL_ID = {0}", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 更新入库方式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int UpdateInstoreType(string type, int billID)
        {
            string sql = "UPDATE wm_asn_header wah SET wah.INSTORE_TYPE='{0}' WHERE wah.BILL_ID={1}";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, type, billID));
        }
        /// <summary>
        ///获取没有复核的托盘
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string GetContainerNochek(AsnBodyEntity header)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>("SELECT GROUP_CONCAT(wcs.CT_CODE) FROM wm_container_state wcs WHERE wcs.BILL_HEAD_ID =@BillID AND wcs.CT_STATE <>83", new { BillID = header.BillID });
        }

        public static int UpdatePrinted(int billID, int printed)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = null;
            if (printed > 1)
            {
                sql = "UPDATE WM_ASN_HEADER H SET H.PRINTED = @Printed WHERE H.BILL_ID = @BillID";
            }
            else
            {
                sql = "UPDATE WM_ASN_HEADER H SET H.PRINTED = @Printed,H.PRINTED_TIME =NOW() WHERE H.BILL_ID = @BillID";
            }
            return map.Execute(sql, new { Printed = printed, BillID = billID });
        }

    }
}
