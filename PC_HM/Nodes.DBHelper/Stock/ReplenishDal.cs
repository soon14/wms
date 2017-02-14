using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Entities;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 补货
    /// </summary>
    public class ReplenishDal
    {

        public List<StockSafeSUPEntity> GetNotSafeStock()
        {
            string sql = string.Format(@"SELECT ws.LC_CODE,ws.QTY ,ws.LATEST_OUT,ws.SKU_CODE,wu.UM_NAME,ws1.SPEC,ws.PICKING_QTY,ws1.SKU_NAME,ws1.SECURITY_QTY *20/100 -ws.QTY DIFF_QTY
                                          FROM wm_stock ws
                                          INNER JOIN wm_location wl ON ws.LC_CODE =wl.LC_CODE
                                          INNER JOIN wm_zone wz ON wl.ZN_CODE =wz.ZN_CODE AND wz.ZT_CODE='70'
                                          INNER JOIN wm_sku ws1 ON ws.SKU_CODE =ws1.SKU_CODE
                                          INNER JOIN wm_um wu ON ws.UM_CODE =wu.UM_CODE
                                          WHERE ws.QTY BETWEEN 1 AND ws1.SECURITY_QTY *20/100 AND wl.IS_ACTIVE ='Y'
                                          UNION ALL

                                        SELECT LC_CODE,qty,latest_out,SKU_CODE,UM_NAME,SPEC,PICKING_QTY,SKU_NAME,DIFF_QTY
                                          FROM(
                                          SELECT  ws.LC_CODE ,SUM(ws.QTY) AS qty,MAX(ws.LATEST_OUT) AS latest_out,ws.SKU_CODE,wu.UM_NAME,ws1.SPEC,ws.PICKING_QTY,ws1.SKU_NAME,ws1.SECURITY_QTY *20/100 -ws.QTY DIFF_QTY
                                          FROM wm_stock ws
                                          INNER JOIN wm_location wl ON ws.LC_CODE =wl.LC_CODE
                                          INNER JOIN wm_zone wz ON wl.ZN_CODE =wz.ZN_CODE AND wz.ZT_CODE='70'
                                          INNER JOIN wm_sku ws1 ON ws.SKU_CODE =ws1.SKU_CODE
                                          INNER JOIN wm_um wu ON ws.UM_CODE =wu.UM_CODE
                                          WHERE wl.IS_ACTIVE ='Y'
                                          GROUP BY ws.LC_CODE) AS aaa WHERE qty=0");
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockSafeSUPEntity>(sql);

        }

        public List<StockTransEntity> InquiryStock()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockTransEntity>("P_REPLENISH_INQUIRY", null, false, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据单号获取状态
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public string GetBillState(int billID)
        {
            string sql = string.Format("SELECT BILL_STATE FROM WM_TRANS_HEADER WHERE ID = {0}", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql);
        }
        /// <summary>
        /// 获取没有分派的补货任务
        /// </summary>
        /// <returns></returns>
        public int GetSupQty()
        {
            string sql = string.Format("SELECT COUNT(1)  FROM tasks t WHERE t.USER_CODE IS NULL AND t.TASK_TYPE='144';");
            IMapper map = DatabaseInstance.Instance();
            return Convert.ToInt32(map.ExecuteScalar<object>(sql));
        }
        /// <summary>
        /// 保存编辑的采购单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="creator">创建者姓名</param>
        /// <returns>-1：单据状态不允许编辑；0：更新表头时失败；1：成功</returns>
        public List<int> SaveBill(string billType, string remark, string whCode,
            string creator, List<StockTransEntity> details)
        {
            List<int> billIdList = new List<int>();
            string billState = string.Empty;
            //if (billID > 0)
            //    billState = GetBillState(billID);

            ////单据已保存
            //if (!string.IsNullOrEmpty(billState))
            //    throw new Exception("单据已保存。");

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            try
            {
                List<StockTransEntity> listCase1 = details.FindAll((item) => { return item.IsCase == 1; });
                List<StockTransEntity> listCase2 = details.FindAll((item) => { return item.IsCase != 1; });
                if (listCase1.Count > 0) // 创建整货补货任务
                {
                    string sql = string.Format("INSERT INTO WM_TRANS_HEADER(BILL_STATE, REMARK, CREATE_DATE, CREATOR, WH_CODE, BILL_TYPE) " +
                    "VALUES('150', '{0}', NOW(), '{1}', '{2}', '{3}')", remark, creator, whCode, billType);
                    //先写入主表
                    map.Execute(sql, null, trans);

                    //没有异常肯定是成功了
                    //先获取新生成的单号，然后保存明细
                    int billId = map.GetAutoIncreasementID("WM_TRANS_HEADER", "ID");

                    sql = "INSERT INTO WM_TRANS_DETAIL(BILL_ID, SKU_CODE, SOURCE_LC_CODE, TARGET_LC_CODE, QTY, TRANS_QTY, CREATE_DATE) "
                            + "VALUES({0}, '{1}', '{2}', '{3}', {4}, 0, NOW())";
                    //再写明细
                    foreach (StockTransEntity line in listCase1)
                    {
                        string sqlDetail = string.Format(sql, billId, line.Material, line.Location, line.TargetLocation, line.TransferQty);
                        map.Execute(sqlDetail, null, trans);
                    }
                    billIdList.Add(billId);
                }
                if (listCase2.Count > 0)// 创建非整货（散货）补货任务
                {
                    string sql = string.Format("INSERT INTO WM_TRANS_HEADER(BILL_STATE, REMARK, CREATE_DATE, CREATOR, WH_CODE, BILL_TYPE) " +
                    "VALUES('150', '{0}', NOW(), '{1}', '{2}', '{3}')", remark, creator, whCode, billType);
                    //先写入主表
                    map.Execute(sql, null, trans);

                    //没有异常肯定是成功了
                    //先获取新生成的单号，然后保存明细
                    int billId = map.GetAutoIncreasementID("WM_TRANS_HEADER", "ID");

                    sql = "INSERT INTO WM_TRANS_DETAIL(BILL_ID, SKU_CODE, SOURCE_LC_CODE, TARGET_LC_CODE, QTY, TRANS_QTY, CREATE_DATE) "
                            + "VALUES({0}, '{1}', '{2}', '{3}', {4}, 0, NOW())";
                    //再写明细
                    foreach (StockTransEntity line in listCase2)
                    {
                        string sqlDetail = string.Format(sql, billId, line.Material, line.Location, line.TargetLocation, line.TransferQty);
                        map.Execute(sqlDetail, null, trans);
                    }
                    billIdList.Add(billId);
                }
                trans.Commit();
            }
            catch (Exception ex) //只能是insert语句产生了异常，先回滚再抛出异常信息
            {
                trans.Rollback();
                throw ex;
            }
            return billIdList;
        }

        public void InquiryBySku(string skuCode, decimal shortQty, string gID, int isCase)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_REPLENISH_BY_SKU", new { V_SKU_CODE = skuCode, V_SHORT_QTY = shortQty, V_G_ID = gID, V_IS_CASE = isCase }, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 安全货位补货计划
        /// </summary>
        /// <param name="v_lccode"></param>
        /// <param name="sku_code"></param>
        /// <param name="user_code"></param>
        public void CreateReplenishPlan(string v_lccode, string sku_code, string user_code)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_STOCKUNDERSAFEQTR_REPLENISH", new { V_LC_CODE = v_lccode, V_SKU_CODE = sku_code, V_USER_CODE = user_code, }, CommandType.StoredProcedure);
        }

        public List<StockTransEntity> GetResultByGID(string gID)
        {
            string sql = string.Format("SELECT TMP.FROM_STOCK_ID, F.LC_CODE, F.SKU_CODE, SKU.SKU_NAME, WU.UM_NAME,SKU.SPEC, " +
                "TMP.TRANS_QTY, TMP.TRANS_QTY SALE_QTY, TMP.LC_CODE TO_LC_CODE, TMP.IS_CASE " +
                "FROM WM_REPLENISH_TEMP TMP " +
                "INNER JOIN WM_STOCK F ON TMP.FROM_STOCK_ID = F.ID " +
                "INNER JOIN WM_LOCATION L ON F.LC_CODE = L.LC_CODE  AND L.IS_ACTIVE ='Y' " +
                "INNER JOIN WM_SKU SKU ON F.SKU_CODE = SKU.SKU_CODE " +
                "INNER JOIN WM_UM WU ON F.UM_CODE = WU.UM_CODE " +
                "WHERE G_ID = '{0}' " +
                "ORDER BY L.SORT_ORDER ASC", gID);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockTransEntity>(sql);
        }

        public int DeleteTempReplenish()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("TRUNCATE WM_REPLENISH_TEMP");
        }
    }
}
