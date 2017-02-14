using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;
using Nodes.Entities.OutBound;

namespace Nodes.DBHelper
{
    public class SOQueryDal
    {
        /// <summary>
        /// 查询未分组的订单
        /// </summary>
        /// <returns></returns>
        public List<SOSummaryEntity> QueryBills(string billStates)
        {
            string sql = string.Format("SELECT A.FROM_WH_CODE, A.BILL_ID, A.BILL_NO,A.BILL_STATE, W.WH_NAME FROM_WH_NAME, " +
            "S.C_NAME, S.SORT_ORDER, S.RT_CODE, R.RT_NAME, S.ADDRESS, C.ITEM_DESC, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR, S.DISTANCE, SUM(D.QTY * D.PRICE) AMOUNT, SUM(US.LENGTH * US.WIDTH * US.HEIGHT) VOLUME, " +
            "F_CALC_PIECES(A.BILL_ID) TOTAL_COUNT " +
            "FROM WM_SO_HEADER A " +
            "LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE " +
            "LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE  " +
            "LEFT JOIN WM_ROUTE R ON R.RT_CODE = S.RT_CODE " +
            "INNER JOIN wm_base_code C on A.BILL_STATE = C.ITEM_VALUE " +
            "INNER JOIN WM_SO_DETAIL D ON A.BILL_ID = D.BILL_ID " +
            "INNER JOIN WM_UM_SKU US ON D.SKU_CODE = US.SKU_CODE AND D.UM_CODE = US.UM_CODE " +
            "WHERE {0} " +
            "GROUP BY A.BILL_ID, A.BILL_NO, W.X_COOR, W.Y_COOR, S.C_NAME, S.ADDRESS, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR", DBUtil.FormatParameter("A.BILL_STATE", billStates));

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOSummaryEntity>(sql);
        }
        public List<SOSummaryEntity> QueryBills(string billStates, int syncState)
        {
            string sql = string.Format("SELECT A.FROM_WH_CODE, A.BILL_ID, A.BILL_NO,A.BILL_STATE, W.WH_NAME FROM_WH_NAME, " +
            "S.C_NAME, S.SORT_ORDER, S.RT_CODE, R.RT_NAME, S.ADDRESS, C.ITEM_DESC, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR, S.DISTANCE, SUM(D.QTY * D.PRICE) AMOUNT, SUM(US.LENGTH * US.WIDTH * US.HEIGHT) VOLUME, " +
            "F_CALC_PIECES(A.BILL_ID) TOTAL_COUNT " +
            "FROM WM_SO_HEADER A " +
            "LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE " +
            "LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE  " +
            "LEFT JOIN WM_ROUTE R ON R.RT_CODE = S.RT_CODE " +
            "INNER JOIN wm_base_code C on A.BILL_STATE = C.ITEM_VALUE " +
            "INNER JOIN WM_SO_DETAIL D ON A.BILL_ID = D.BILL_ID " +
            "INNER JOIN WM_UM_SKU US ON D.SKU_CODE = US.SKU_CODE AND D.UM_CODE = US.UM_CODE " +
            "WHERE {0} AND SYNC_STATE = {1} " +
            "GROUP BY A.BILL_ID, A.BILL_NO, W.X_COOR, W.Y_COOR, S.C_NAME, S.ADDRESS, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR", DBUtil.FormatParameter("A.BILL_STATE", billStates), syncState);

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOSummaryEntity>(sql);
        }

        public List<SOSummaryEntity> QueryBillsQuery(string billStates)
        {
            string sql = string.Format("SELECT A.FROM_WH_CODE,wspr.CT_CODE, A.BILL_ID, A.BILL_NO,A.BILL_STATE, W.WH_NAME FROM_WH_NAME, " +
            "S.C_NAME, S.SORT_ORDER, S.RT_CODE, R.RT_NAME, S.ADDRESS, C.ITEM_DESC, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR, S.DISTANCE, SUM(D.QTY * D.PRICE) AMOUNT, SUM(US.LENGTH * US.WIDTH * US.HEIGHT) VOLUME " +
            "FROM WM_SO_HEADER A " +
            "LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE " +
            "LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE  " +
            "LEFT JOIN WM_ROUTE R ON R.RT_CODE = S.RT_CODE " +
            "LEFT JOIN wm_so_pick_record wspr ON A.BILL_ID=wspr.BILL_ID " +
            "INNER JOIN wm_base_code C on A.BILL_STATE = C.ITEM_VALUE " +
            "INNER JOIN WM_SO_DETAIL D ON A.BILL_ID = D.BILL_ID " +
            "INNER JOIN WM_UM_SKU US ON D.SKU_CODE = US.SKU_CODE AND D.UM_CODE = US.UM_CODE " +
            "WHERE {0} " +
            "GROUP BY A.BILL_ID, A.BILL_NO, W.X_COOR, W.Y_COOR, S.C_NAME, S.ADDRESS, " +
            "A.CREATE_DATE, S.X_COOR, S.Y_COOR,wspr.CT_CODE", DBUtil.FormatParameter("A.BILL_STATE", billStates));

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOSummaryEntity>(sql);
        }
        //private bool IsCodeExists(SoContainerLocation sclEntity)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    string id = map.ExecuteScalar<string>("SELECT wcl.CTL_NAME FROM wm_container_location wcl" +
        //                                            " WHERE wcl.CTL_NAME= @CTLNAME", new { CTLNAME = sclEntity.CTLName });
        //    return !string.IsNullOrEmpty(id);
        //}

        //public int Save(SoContainerLocation sclEntity, bool isNew)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    int ret = -2;
        //    if (isNew)
        //    {
        //        //检查编号是否已经存在,不存在添加
        //        if (IsCodeExists(sclEntity))
        //            return -1;
        //        ret = map.Execute("INSERT INTO wm_container_location(CTL_NAME,CTL_STATE,LAST_UPDATETIME)" +
        //                            " VALUES(@CTL_NAME, @CTL_STATE,NOW())",
        //        new
        //        {
        //            CTL_NAME = sclEntity.CTLName,
        //            CTL_STATE = sclEntity.CTLState
        //        });
        //    }
        //    else
        //    {
        //        //修改
        //        ret = map.Execute("UPDATE wm_container_location wcl SET wcl.CTL_NAME= @CTL_NAME WHERE CTL_NAME = @CTL_NAME",
        //        new
        //        {
                    
        //            CTL_NAME = sclEntity.CTLName
        //        });
        //    }
        //    return ret;
        //}

        //public void DeleteCTL()
        //{
        //    string sql = string.Format("");
        //    IMapper map = DatabaseInstance.Instance();
        //    map.LoadTable(sql);
        //}

        //public List<SoContainerLocation> QeryCTL()
        //{
        //    string sql = string.Format("SELECT wcl.CTL_NAME,wbc.ITEM_DESC FROM wm_container_location wcl "+
        //                          " INNER JOIN wm_base_code wbc ON wcl.CTL_STATE=wbc.ITEM_VALUE "+
        //                          " WHERE wcl.IS_DELETE = 1 "+
        //                          " GROUP BY CAST(wcl.CTL_NAME AS SIGNED INTEGER)");
        //    IMapper map = DatabaseInstance.Instance();
        //    return map.Query<SoContainerLocation>(sql);
        //}

        public DataTable QuerySoDetails(string billNO, string materialCode, string materialName,
            string customerName, DateTime dateFrom, DateTime dateTo)
        {
            string sql = string.Format("SELECT H.BILL_NO, C.C_NAME, H.CREATE_DATE, H.CONFIRM_DATE, D.SKU_CODE, M.SKU_NAME, D.COM_MATERIAL, M.SPEC, " +
                "UM.UM_NAME, D.PRICE, D.QTY, D.PICK_QTY, D.PICK_QTY * D.PRICE AMOUNT " +
                "FROM WM_SO_DETAIL D " +
                "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_UM UM ON D.UM_CODE = UM.UM_CODE " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE " +
                "WHERE H.CONFIRM_DATE >= @DateFrom AND H.CONFIRM_DATE < @DateTo " +
                "{0} {1} {2} {3}",
                string.IsNullOrEmpty(billNO) ? "" : "AND H.BILL_NO LIKE '%" + billNO + "%'",
                string.IsNullOrEmpty(materialCode) ? "" : "AND D.SKU_CODE LIKE '%" + materialCode + "%'",
                string.IsNullOrEmpty(materialName) ? "" : "AND M.SKU_NAME LIKE '%" + materialName + "%'",
                string.IsNullOrEmpty(customerName) ? "" : "AND C.C_NAME LIKE '%" + customerName + "%'");
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }

        public DataTable GetSKUSaleSort(string dateStart, string dateEnd)
        {
            string sql = string.Format("SELECT D.SKU_CODE, WS.SKU_NAME, SUM(D.PICK_QTY) QTY, D.SPEC, WU.UM_NAME " +
                "FROM WM_SO_DETAIL D " +
                "JOIN WM_SKU WS ON D.SKU_CODE = WS.SKU_CODE " +
                "JOIN WM_UM WU ON D.UM_CODE = WU.UM_CODE " +
                "JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.CONFIRM_DATE >= '{0}' AND H.CONFIRM_DATE <= '{1}' " +
                "GROUP BY D.SKU_CODE, WS.SKU_NAME, D.SPEC, WU.UM_NAME ", dateStart, dateEnd);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 查询拣货记录
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="materialCode"></param>
        /// <param name="materialName"></param>
        /// <param name="skuBarcode"></param>
        /// <param name="ctCode"></param>
        /// <param name="customerName"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable QueryPickRecords(string billNO, string materialCode, string materialName, string skuBarcode, string ctCode,
            string customerName, DateTime dateFrom, DateTime dateTo)
        {
            string sql = string.Format(
                "SELECT H.BILL_ID, H.BILL_NO, C.C_NAME, H.CREATE_DATE, H.CONFIRM_DATE, D.SKU_CODE, M.SKU_NAME, D.COM_MATERIAL, " +
                "M.SPEC, UM.UM_NAME, WUS.SKU_BARCODE, R.CT_CODE, R.LC_CODE, R.PICK_DATE, R.PICK_QTY / WUS.QTY QTY, U.USER_NAME " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN USERS U ON R.USER_CODE = U.USER_CODE " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_UM UM ON D.UM_CODE = UM.UM_CODE " +
                "INNER JOIN WM_UM_SKU WUS ON D.UM_CODE = WUS.UM_CODE AND D.SKU_CODE = WUS.SKU_CODE " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE " +
                "WHERE R.PICK_DATE >= @DateFrom AND R.PICK_DATE < @DateTo " +
                "{0} {1} {2} {3} {4} {5} " +
                "GROUP BY H.BILL_ID",
                string.IsNullOrEmpty(billNO) ? "" : "AND H.BILL_NO LIKE '%" + billNO + "%'",
                string.IsNullOrEmpty(materialCode) ? "" : "AND D.SKU_CODE LIKE '%" + materialCode + "%'",
                string.IsNullOrEmpty(materialName) ? "" : "AND M.SKU_NAME LIKE '%" + materialName + "%'",
                string.IsNullOrEmpty(skuBarcode) ? "" : "AND W.SKU_BARCODE LIKE '%" + skuBarcode + "%'",
                string.IsNullOrEmpty(customerName) ? "" : "AND C.C_NAME LIKE '%" + customerName + "%'",
                string.IsNullOrEmpty(ctCode) ? "" : "AND R.CT_CODE LIKE '%" + ctCode + "%'");
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }
        #region 拣货人人途统计
        public DataTable QueryBills(string userCode, DateTime dateFrom, DateTime dateTo)
        {
            //string sql = string.Format(
            //    "SELECT H.BILL_ID, H.BILL_NO, U.USER_NAME, R.PICK_DATE, " +
            //    "C.C_NAME, c.ADDRESS, c.CONTACT, MIN(R.PICK_DATE) BEGIN_DATE, MAX(R.PICK_DATE) END_DATE,SUM(wsd.PICK_QTY) QTY " +
            //    "FROM (SELECT wspr.BILL_ID,wspr.USER_CODE,wspr.PICK_DATE FROM wm_so_pick_record wspr GROUP BY wspr.BILL_ID,wspr.USER_CODE) R " +
            //    "INNER JOIN wm_so_header H ON R.BILL_ID = H.BILL_ID " +
            //    "INNER JOIN wm_so_detail wsd ON H.BILL_ID=wsd.BILL_ID " +
            //    "INNER JOIN users U ON U.USER_CODE = R.USER_CODE " +
            //    "INNER JOIN customers c ON c.C_CODE = H.C_CODE " +
            //    "INNER JOIN wm_warehouse WH ON H.WH_CODE = WH.WH_CODE " +
            //    "WHERE R.PICK_DATE >= @DateFrom AND R.PICK_DATE < @DateTo " +
            //    "{0} " +
            //    "GROUP BY H.BILL_ID ",
            //    string.IsNullOrEmpty(userCode) ? string.Empty : "AND R.USER_CODE LIKE '%" + userCode + "%'");
            string sql = string.Format(@"SELECT H.BILL_ID, H.BILL_NO, U.USER_NAME, C.C_NAME, C.ADDRESS, C.CONTACT, 
                                  MIN(R.PICK_DATE) BEGIN_DATE, MAX(R.PICK_DATE) END_DATE, 
                                  IFNULL(B.QTY, 0) W_QTY, IFNULL(A.BOX_COUNT, 0) B_QTY, P.IS_CASE
                                  FROM WM_SO_DETAIL D 
                                  LEFT JOIN WM_SO_PICK P ON P.DETAIL_ID = D.ID
                                  LEFT JOIN WM_SO_PICK_RECORD R ON R.PICK_ID = P.ID
                                  INNER JOIN wm_so_header H ON H.BILL_ID = D.BILL_ID 
                                  INNER JOIN users U ON U.USER_CODE = R.USER_CODE 
                                  LEFT JOIN customers c ON c.C_CODE = H.C_CODE 
                                  LEFT JOIN wm_warehouse WH ON H.WH_CODE = WH.WH_CODE 
                                  LEFT JOIN (SELECT P.BILL_ID, SUM(R.PICK_QTY / A.QTY) QTY 
                                              FROM WM_SO_PICK_RECORD R
                                              INNER JOIN wm_so_pick P ON P.ID = R.PICK_ID AND P.IS_CASE = 1
                                              INNER JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID
                                              LEFT JOIN (SELECT SKU_CODE, MAX(QTY) QTY
                                                          FROM WM_UM_SKU GROUP BY SKU_CODE) A ON A.SKU_CODE = S.SKU_CODE
                                              WHERE R.PICK_DATE > @DateFrom AND R.PICK_DATE <= @DateTo {0}
                                              GROUP BY R.BILL_ID) B ON B.BILL_ID = D.BILL_ID AND P.IS_CASE = 1
                                  LEFT JOIN (SELECT R.BILL_ID, COUNT(R.CT_CODE) BOX_COUNT FROM WM_SO_PICK_RECORD R 
                                              WHERE R.PICK_DATE > @DateFrom AND R.PICK_DATE <= @DateTo AND R.CT_CODE LIKE '2%' {0}
                                              GROUP BY R.BILL_ID) A ON A.BILL_ID = D.BILL_ID AND P.IS_CASE = 2
                                  WHERE R.PICK_DATE > @DateFrom AND R.PICK_DATE <= @DateTo {0}
                                  GROUP BY H.BILL_ID, P.IS_CASE",
                string.IsNullOrEmpty(userCode) ? string.Empty : "AND R.USER_CODE LIKE '%" + userCode + "%'");
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }
        #endregion

        #region 彭伟 2015-08-07
        public static List<SODetailEntity> GetDetailsByBills(string bills, int isCase)
        {
            string sql = string.Format("SELECT D.ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, D.SPEC, M.SKU_NAME, " +
                "WUS.SKU_BARCODE,ifnull(D.SUIT_NUM,1) SUIT_NUM, D.COM_MATERIAL, D.QTY, D.UM_CODE, " +
                "UM.UM_NAME, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK, D.PICK_QTY, IS_CASE,D.ROW_NO " +
                "FROM WM_SO_DETAIL D " +
                "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE D.BILL_ID IN ({0}) AND D.IS_CASE = {1} ", bills, isCase);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SODetailEntity>(sql);
        }
        #endregion

        #region 彭伟 2015-08-20
        /// <summary>
        /// 获取拣货任务的拣货开始时间
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static DateTime? GetBeginDate(int taskID)
        {
            string sql = "SELECT MIN(R.PICK_DATE) FROM WM_SO_PICK_RECORD R WHERE R.TASK_ID = " + taskID;
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<DateTime>(sql);
        }
        #endregion

        public static DataTable QueryDriverRecords(DateTime beginDate, DateTime endDate, string userCode)
        {
            //string sql = "SELECT DISTINCT SH.BILL_NO 订单编号, ROUND(SUM(SD.PICK_QTY), 0) 整货件数, " +
            //    "F_GET_BULK_PIECES(SH.BILL_ID) 散货件数 " +
            //    "FROM WM_VEHICLE_TRAIN_USERS U " +
            //    "LEFT JOIN WM_VEHICLE_TRAIN_DETAIL D ON U.VH_TRAIN_NO = D.VH_TRAIN_NO " +
            //    "LEFT JOIN WM_VEHICLE_TRAIN_HEADER H ON H.VH_TRAIN_NO = D.VH_TRAIN_NO " +
            //    "LEFT JOIN WM_SO_HEADER SH ON SH.BILL_NO = D.BILL_NO " +
            //    "LEFT JOIN wm_so_detail SD ON SD.BILL_ID = SH.BILL_ID AND SD.IS_CASE = 1 " +
            //    "WHERE U.USER_CODE = @UserCode AND H.UPDATE_DATE >= @BeginDate AND H.UPDATE_DATE <= @EndDate " +
            //    "GROUP BY SH.BILL_ID ";
            string sql = @"SELECT A.USER_CODE, SUM(IFNULL(A.BILL_COUNT, 0)) / B.U_COUNT 配送单量,SUM(A.CASE_QTY) / B.U_COUNT 配送整货, SUM(A.QTY) / B.U_COUNT 配送散货
  FROM (SELECT U.USER_CODE, U.USER_NAME, A.VH_TRAIN_NO, A.VH_NO,COUNT(A.BILL_NO) BILL_COUNT, SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY
          FROM USERS U 
          LEFT JOIN (SELECT H.VH_TRAIN_NO, H.VH_NO, D.BILL_NO, U.USER_CODE, U.USER_NAME, 
                      H.WHOLE_GOODS QTY1, H.BULK_CARGO_QTY QTY2 
                      FROM WM_VEHICLE_TRAIN_HEADER H 
                      LEFT JOIN WM_VEHICLE_TRAIN_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                      LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                      WHERE D.UPDATE_DATE BETWEEN @BeginDate AND @EndDate 
                      GROUP BY D.VH_TRAIN_NO, U.USER_CODE) A ON A.USER_CODE = U.USER_CODE 
          GROUP BY U.USER_CODE, A.VH_TRAIN_NO) A 
  LEFT JOIN (SELECT H.VH_TRAIN_NO, COUNT(U.USER_CODE) U_COUNT FROM WM_VEHICLE_TRAIN_HEADER H 
              LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
              GROUP BY H.VH_TRAIN_NO) B ON B.VH_TRAIN_NO = A.VH_TRAIN_NO 
  WHERE A.USER_CODE = @UserCode
  GROUP BY A.USER_CODE";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BeginDate = beginDate, EndDate = endDate, UserCode = userCode });
        }
    }
}
