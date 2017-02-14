using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 库存转移
    /// </summary>
    public class StockTransferDal
    {
        /// <summary>
        /// 插入移库单头表
        /// </summary>
        /// <param name="billType">单据类型：160-移库单；161-补货单</param>
        /// <returns></returns>
        public int SaveStockTransferHeader(string remark, string userName, string whCode, string uniqueCode, string billType)
        {
            string sql = "INSERT INTO wm_stock_transfer_header(BILL_STATE,REMARK,CREATE_DATE,CREATOR,WH_CODE,UNIQUE_CODE,BILL_TYPE) "
                        + "VALUES('150','{0}',now(),'{1}','{2}','{3}','{4}');";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, remark, userName, whCode, uniqueCode, billType));
        }

        /// <summary>
        /// 插入移库单明细表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int SaveStockTransferDetail(StockTransEntity entity, string uniqueCode)
        {
            string sql = "INSERT INTO wm_stock_transfer_detail(SKU_CODE,SOURCE_LC_CODE,TARGET_LC_CODE,QTY,SKU_NAME,UNIQUE_CODE,CREATE_TIME) "
                        + "VALUES('{0}','{1}','{2}',{3},'{4}','{5}',now());";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, entity.Material, entity.Location, entity.TargetLocation, entity.TransferQty, entity.MaterialName, uniqueCode));
        }

        /// <summary>
        /// 获取新增移库单的ID值
        /// </summary>
        /// <returns></returns>
        public int GetMAXTransferHeadID()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT MAX(ID) FROM wm_stock_transfer_header";
            return map.ExecuteScalar<int>(sql) + 1;

        }

        #region 转移记录查询
        /// <summary>
        /// 获取移库单头表数据
        /// </summary>
        public DataSet GetTransferBillHead(string sqlWhere)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT  wsth.BILL_STATE,wbc.ITEM_DESC as BILLSTATE,wbc1.ITEM_DESC as BILLTYPE,wsth.BILL_TYPE,wsth.REMARK, wsth.CREATE_DATE, wsth.CREATOR, wsth.COMPLETE_DATE, wsth.WH_CODE, wsth.UNIQUE_CODE "
                      + "FROM wm_stock_transfer_header wsth "
                      + "JOIN wm_base_code wbc ON wsth.BILL_STATE=wbc.ITEM_VALUE "
                      + "JOIN wm_base_code wbc1 ON wsth.BILL_TYPE=wbc1.ITEM_VALUE {0}";
            DataSet ds = map.LoadDataSet(String.Format(sql, sqlWhere));
            ds.Tables[0].TableName = "ZHU";
            return ds;
        }

        /// <summary>
        /// 查询移库记录
        /// </summary>
        public DataTable QueryTransRecords(DateTime dateFrom, DateTime dateTo, string skuName, string location)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT WTR.FROM_LC_CODE, WTD.SKU_CODE, WS.SKU_NAME, WTR.TO_LC_CODE, WTD.QTY PLAN_QTY, " +
                "WTR.QTY QTY_TRANS, WTR.CREATE_DATE, WTR.USER_CODE, U.USER_NAME, US.USER_NAME AUTH_USER_NAME, B.ITEM_DESC " +
                "FROM WM_TRANS_RECORD WTR " +
                "INNER JOIN WM_TRANS_HEADER H ON WTR.BILL_ID = H.ID " +
                "INNER JOIN WM_BASE_CODE B ON H.BILL_TYPE = B.ITEM_VALUE " +
                "INNER JOIN USERS U ON WTR.USER_CODE = U.USER_CODE " +
                "LEFT JOIN USERS US ON US.USER_CODE = WTR.AUTH_USER_CODE " +
                "INNER JOIN WM_TRANS_DETAIL WTD ON WTD.ID = WTR.DETAIL_ID " +
                "INNER JOIN WM_SKU WS ON WTD.SKU_CODE = WS.SKU_CODE " +
                "WHERE WTR.CREATE_DATE >= @DateFrom AND WTR.CREATE_DATE <= @DateTo " +
                "{0} {1}",
                string.IsNullOrEmpty(skuName) ? "" : string.Format("AND WS.SKU_NAME LIKE '%{0}%' ", skuName),
                string.IsNullOrEmpty(location) ? "" : string.Format("AND (WTR.FROM_LC_CODE = '{0}' OR WTR.TO_LC_CODE = '{0}')", location));
            return map.LoadTable(sql, new { DateFrom = dateFrom, DateTo = dateTo });
        }

        public static DataTable QueryTransRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            string sql = @"SELECT R.FROM_LC_CODE, R.TO_LC_CODE, R.UM_QTY, U.UM_NAME, R.CREATE_DATE 
                FROM WM_TRANS_RECORD R 
                LEFT JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID 
                LEFT JOIN WM_UM U ON S.UM_CODE = U.UM_CODE 
                WHERE R.USER_CODE = @UserCode AND R.CREATE_DATE >= @DateBegin AND R.CREATE_DATE <= @DateEnd";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { DateBegin = dateBegin, DateEnd = dateEnd, UserCode = userCode });
        }
        #endregion

        #region 彭伟 2015-08-20
        public static DateTime? GetBeginDate(int taskID)
        {
            string sql = "SELECT MIN(R.CREATE_DATE) FROM TASK_DETAIL D " +
                "LEFT JOIN wm_trans_record R ON R.BILL_ID = D.ACTION_ID " +
                "WHERE D.TASK_ID = " + taskID;
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<DateTime>(sql);
        }
        #endregion

        public static int CreateSingleTransfer(string creator, string warehouseCode, string skuCode, string fromLocation, decimal qty)
        {
            string sql = @"INSERT INTO WM_TRANS_HEADER(BILL_STATE, CREATE_DATE, CREATOR, WH_CODE, BILL_TYPE) VALUES('150', NOW(), @Creator, @WarehouseCode, '161');
  SELECT LAST_INSERT_ID();";
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            int result = -1;
            try
            {
                object objHeader = map.ExecuteScalar<object>(sql, new { Creator = creator, WarehouseCode = warehouseCode });
                sql = @"INSERT INTO WM_TRANS_DETAIL(BILL_ID, SKU_CODE, SOURCE_LC_CODE, TARGET_LC_CODE, QTY, TRANS_QTY, CREATE_DATE) 
            VALUES(@HeaderID, @SkuCode, @FromLocation, NULL, @Qty, 0, NOW()) ";
                result = map.Execute(sql, new 
                { 
                    HeaderID = objHeader, 
                    SkuCode = skuCode, 
                    FromLocation = fromLocation,
                    Qty = qty
                });
                result = ConvertUtil.ToInt(objHeader);
            }
            catch
            {
                result = -1;
                trans.Rollback();
            }
            return result;
        }
    }
}
