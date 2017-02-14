using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities.Report;

namespace Nodes.DBHelper
{
    public class ReportDal
    {
        /// <summary>
        /// 查询库存修正记录
        /// </summary>
        /// <param name="lc_code"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetStockReviseRecords(DateTime? dateBegin, DateTime? dateEnd)
        {
            string sql = string.Format(@"SELECT wsul.lcCode ,wsul.skuCode ,wsul.oldStockQty ,wsul.newStockQty ,
                  u.USER_NAME CZ_USER,u1.USER_NAME FH_USER,wsul.createtime,wu.UM_NAME,ws.SKU_NAME,wsul.newStockQty-wsul.oldStockQty DIFFQTY
                  FROM wm_stock_update_log wsul
                  INNER JOIN users u ON wsul.czUserCode =u.USER_CODE
                  LEFT JOIN users u1 ON wsul.fhUserCode =u1.USER_CODE
                  LEFT JOIN wm_um_sku wus ON wsul.skuCode =wus.SKU_CODE AND wus.SKU_LEVEL =1
                  LEFT JOIN wm_um wu ON wus.UM_CODE =wu.UM_CODE
                  LEFT JOIN wm_sku ws ON wsul.skuCode =ws.SKU_CODE
                  WHERE wsul.createtime >'{0}'AND wsul.createtime <='{1}'
                  GROUP BY wsul.createtime
            ", dateBegin == null ? DateTime.MinValue : dateBegin, dateEnd == null ? DateTime.MaxValue : dateEnd);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 查询上架记录
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="driver"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetPutAwayRecords(int skuCode, string driver, DateTime? dateStart, DateTime? dateEnd)
        {
            string sql = String.Format("SELECT wapr.CT_CODE,wapr.SKU_CODE,ws.SKU_NAME,ws.SPEC,wapr.LC_CODE,ROUND(wapr.QTY/wus.QTY,0) QTY,wu.UM_NAME,wapr.PUT_BY,wapr.PUT_TIME,wapr.BILL_ID, " +
                          "wah.BILL_NO,u.USER_NAME,wbc.ITEM_DESC BILL_STATE,wbc1.ITEM_DESC BILL_TYPE " +
                          "FROM wm_asn_putaway_records wapr " +
                          "INNER JOIN wm_sku ws ON wapr.SKU_CODE=ws.SKU_CODE " +
                          "INNER JOIN wm_asn_header wah ON wapr.BILL_ID=wah.BILL_ID " +
                          "INNER JOIN wm_asn_detail wad ON wapr.BILL_DETAIL_ID=wad.ID " +
                          "INNER JOIN users u ON wapr.PUT_BY=u.USER_CODE " +
                          "INNER JOIN wm_um_sku wus ON wapr.SKU_CODE=wus.SKU_CODE AND wad.UM_CODE=wus.UM_CODE " +
                          "INNER JOIN wm_base_code wbc ON wah.BILL_STATE=wbc.ITEM_VALUE " +
                          "INNER JOIN wm_base_code wbc1 ON wah.BILL_TYPE=wbc1.ITEM_VALUE " +
                          "INNER JOIN wm_um wu ON wad.UM_CODE=wu.UM_CODE " +
                          "WHERE wapr.PUT_TIME >='{0}' AND wapr.PUT_TIME<='{1}' ", dateStart == null ? DateTime.MinValue : dateStart, dateEnd == null ? DateTime.MaxValue : dateEnd);
            if (skuCode != 0)
            {
                sql += String.Format("and wapr.SKU_CODE={0} ", skuCode);
            }
            if (!String.IsNullOrEmpty(driver))
            {
                sql += String.Format("and wapr.PUT_BY='{0}' ", driver);
            }
            sql += "ORDER BY wapr.PUT_TIME DESC ;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);

        }

        /// <summary>
        /// 查询本月配货率
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDistribution()
        {
            string sql = @"SELECT wsh.BILL_NO AS '出库单编号', 
  ROUND(SUM(IFNULL(WSD.QTY, 0) / IFNULL(WSD.QTY, 0) / IFNULL(WSD.SUIT_NUM, 0) * wsd.PRICE), 2) AS '订购金额',
  ROUND(SUM(IFNULL(WSD.PICK_QTY, 0) / IFNULL(WSD.QTY, 0) / IFNULL(WSD.SUIT_NUM, 0) * wsd.PRICE), 2) AS '拣出金额',
    (SELECT ROUND(AVG(e),2) FROM 
      (SELECT SUM(
        IFNULL(wsd.PICK_QTY, 0) / IFNULL(wsd.QTY, 0) / IFNULL(wsd.SUIT_NUM, 0) * wsd.PRICE) / 
        SUM(IFNULL(wsd.QTY, 0) / IFNULL(wsd.QTY, 0) / IFNULL(wsd.SUIT_NUM, 0) * wsd.PRICE) * 100 AS e FROM wm_so_detail wsd
    LEFT JOIN wm_so_header wsh ON wsd.BILL_ID = wsh.BILL_ID
    WHERE IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= '" + DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString() + @"' 
      AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= '" + DateTime.Now.AddDays(1).ToString() + @"'
    GROUP BY wsd.BILL_ID) e) AS '平均拣货率'
  FROM wm_so_header wsh
  INNER JOIN wm_so_detail wsd ON wsh.BILL_ID = wsd.BILL_ID
  WHERE IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= '" + DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString() + @"' 
    AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= '" + DateTime.Now.AddDays(1).ToString() + @"'
  GROUP BY wsd.BILL_ID;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 根据时间查询配货率
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetDistribution(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT wsh.BILL_NO AS '出库单编号',  ROUND(SUM(IFNULL(WSD.QTY, 0) / " +
                "IFNULL(WSD.QTY, 0) / IFNULL(WSD.SUIT_NUM, 0) * wsd.PRICE), 2) AS '订购金额', " +
                "ROUND(SUM(IFNULL(WSD.PICK_QTY, 0) / IFNULL(WSD.QTY, 0) / IFNULL(WSD.SUIT_NUM, 0) " +
                "* wsd.PRICE), 2) AS '拣出金额', (SELECT ROUND(AVG(e),2) FROM (SELECT SUM( " +
                "IFNULL(wsd.PICK_QTY, 0) / IFNULL(wsd.QTY, 0) / IFNULL(wsd.SUIT_NUM, 0) * wsd.PRICE) / " +
                "SUM(IFNULL(wsd.QTY, 0) / IFNULL(wsd.QTY, 0) / IFNULL(wsd.SUIT_NUM, 0) * wsd.PRICE) * 100 " +
                "AS e FROM wm_so_detail wsd " +
                "LEFT JOIN wm_so_header wsh ON wsd.BILL_ID = wsh.BILL_ID " +
                "WHERE IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime " +
                "GROUP BY wsd.BILL_ID) e) AS '平均拣货率' " +
                "FROM wm_so_header wsh " +
                "INNER JOIN wm_so_detail wsd ON wsh.BILL_ID = wsd.BILL_ID " +
                "WHERE IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime " +
                "GROUP BY wsd.BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }
        /// <summary>
        /// 查询本月发货率
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDelivery()
        {
            string sql = @"SELECT wsh.BILL_NO AS '出库单号', CASE when (UNIX_TIMESTAMP(IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME)) - UNIX_TIMESTAMP(wsh.CONFIRM_DATE) < 86400) THEN '是' ELSE '否' END AS '发货是否及时'
                  FROM wm_so_header wsh
                WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE = '120'  AND DATE_FORMAT(IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME),'%Y%m')=DATE_FORMAT(CURDATE( ),'%Y%m')";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 根据时间查询发货率
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetDelivery(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT wsh.BILL_NO AS '出库单号', CASE when (UNIX_TIMESTAMP(IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME)) " +
                "- UNIX_TIMESTAMP(wsh.CONFIRM_DATE) < 86400) THEN '是' ELSE '否' END AS '发货是否及时' " +
                "FROM wm_so_header wsh " +
                "WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE = '120'  AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 查询本月回款
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllReturnedMoney()
        {
            string sql = @"SELECT (a.ConfirmCount + COUNT(sh.BILL_ID)) AS '总单数', a.ConfirmCount AS '回款单数', a.RealAmount AS '回款金额', 
              COUNT(sh.BILL_ID) AS '未回款单数', ROUND(100 * a.ConfirmCount / (a.ConfirmCount + COUNT(sh.BILL_ID)), 2) AS '回款率'
              FROM wm_so_header sh,
            (SELECT COUNT(1) AS ConfirmCount, SUM(wsh.REAL_AMOUNT) AS RealAmount FROM wm_so_header wsh 
            WHERE (wsh.CONFIRM_FLAG = 1 OR wsh.CONFIRM_FLAG IS NULL) AND wsh.BILL_TYPE = '120' AND wsh.BILL_STATE = '68'
             AND DATE_FORMAT(IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME),'%Y%m')=DATE_FORMAT(CURDATE( ),'%Y%m')) a
             WHERE sh.CONFIRM_FLAG = 0 AND sh.BILL_TYPE = '120' AND sh.BILL_STATE = '68'
            AND DATE_FORMAT(IFNULL(sh.CLOSE_DATE, sh.LAST_UPDATETIME),'%Y%m')=DATE_FORMAT(CURDATE( ),'%Y%m')";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 根据时间查询回款
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReturnedMoney(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT (a.ConfirmCount + COUNT(sh.BILL_ID)) AS '总单数', a.ConfirmCount AS " +
                "'回款单数', a.RealAmount AS '回款金额', COUNT(sh.BILL_ID) AS '未回款单数', " +
                "ROUND(100 * a.ConfirmCount / (a.ConfirmCount + COUNT(sh.BILL_ID)), 2) AS '回款率' " +
                "FROM wm_so_header sh, (SELECT COUNT(1) AS ConfirmCount, SUM(wsh.REAL_AMOUNT) AS " +
                "RealAmount FROM wm_so_header wsh WHERE (wsh.CONFIRM_FLAG = 1 OR wsh.CONFIRM_FLAG IS " +
                "NULL) AND wsh.BILL_TYPE = '120' AND wsh.BILL_STATE = '68' AND IFNULL(wsh.CLOSE_DATE, " +
                "wsh.LAST_UPDATETIME) >= @StartTime AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) " +
                "<= @EndTime) a WHERE sh.CONFIRM_FLAG = 0 AND sh.BILL_TYPE = '120' AND " +
                "sh.BILL_STATE = '68' AND IFNULL(sh.CLOSE_DATE, sh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(sh.CLOSE_DATE, sh.LAST_UPDATETIME) <= @EndTime";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询配货单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="type">类型ID，1.销售单、2.调拨单</param>
        /// <returns></returns>
        public static DataTable GetAllocationCargo(DateTime startTime, DateTime endTime, int type)
        {
            if (type != 1 && type != 2)
                return null;
            string extSql = string.Empty;
            if (type == 1)
                extSql = " AND H.BILL_TYPE = 120 ";
            else if (type == 2)
                extSql = " AND H.BILL_TYPE IN (121, 122, 123) ";
            string sql = "SELECT s.单号, s.件数, s.金额, t.平均件数 FROM " +
                "(SELECT ROUND(SUM(D.PICK_QTY) / COUNT(DISTINCT H.BILL_NO), 2) '平均件数' " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 " + extSql +
                "AND R.PICK_DATE BETWEEN @StartTime AND @EndTime) t, " +
                "(SELECT H.BILL_NO '单号', SUM(D.PICK_QTY) '件数', " +
                "ROUND(SUM(IFNULL(D.PICK_QTY, 0) / IFNULL(D.QTY, 0) / IFNULL(D.SUIT_NUM, 0) * D.PRICE), 2) '金额' " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 " + extSql +
                "AND R.PICK_DATE BETWEEN @StartTime AND @EndTime " +
                "GROUP BY H.BILL_NO) s";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 配货件数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetPickedCount(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.销售配货件数, B.调拨配货件数 FROM ( " +
                "SELECT SUM(IFNULL(D.PICK_QTY, 0)) AS '销售配货件数' " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 AND H.BILL_TYPE = 120 " +
                "AND R.PICK_DATE BETWEEN @StartTime AND @EndTime) A,( " +
                "SELECT SUM(IFNULL(D.PICK_QTY, 0)) AS '调拨配货件数' " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 AND H.BILL_TYPE IN (121, 122, 123) " +
                "AND R.PICK_DATE BETWEEN '2015-7-01 18:00:00' AND '2015-7-31 8:00:00') B";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 配货金额
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetPickedAmount(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.销售单配货金额, B.调拨单配货金额 FROM ( " +
                "SELECT ROUND(SUM(IFNULL(D.PICK_QTY, 0) / IFNULL(D.QTY, 0) / IFNULL(D.SUIT_NUM, 0) " +
                "* D.PRICE), 2) AS '销售单配货金额' FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 AND H.BILL_TYPE = 120 " +
                "AND R.PICK_DATE BETWEEN @StartTime AND @EndTime) A, " +
                "(SELECT ROUND(SUM(IFNULL(D.PICK_QTY, 0) / IFNULL(D.QTY, 0) / IFNULL(D.SUIT_NUM, 0) " +
                "* D.PRICE), 2) '调拨单配货金额' FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE > 64 AND D.IS_CASE = 1 AND H.BILL_TYPE IN (121, 122, 123) " +
                "AND R.PICK_DATE BETWEEN @StartTime AND @EndTime) B";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间获取配货件数平均值(D类库)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetCountAvg(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT SUM(R.PICK_QTY)/ COUNT(DISTINCT R.USER_CODE) AS '配货平均件数'" +
                "FROM WM_SO_PICK_RECORD R INNER JOIN WM_SO_PICK P ON R.PICK_ID = P.ID " +
                "INNER JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE = '68' AND R.PICK_DATE BETWEEN @StartTime AND @EndTime ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询验货单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="type">订单类型：1.销售单，2.调拨单</param>
        /// <returns></returns>
        public static DataTable GetExamineCargoCrop(DateTime startTime, DateTime endTime, int type)
        {
            if (type != 1 && type != 2)
                return null;
            string extSql = string.Empty;
            if (type == 1)
                extSql = " AND H.BILL_TYPE = 120 ";
            else if (type == 2)
                extSql = " AND H.BILL_TYPE IN (121, 122, 123) ";
            string sql = "SELECT COUNT(DISTINCT W.BILL_ID) '复核单量', DATE(W.CREATE_DATE) AS 日期 " +
                "FROM WM_SO_WEIGHT W " +
                "LEFT JOIN WM_SO_HEADER H ON H.BILL_ID = W.BILL_ID " +
                "WHERE W.CREATE_DATE BETWEEN @StartTime AND @EndTime " +
                extSql +
                "GROUP BY 日期 ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询装车单量（D类库）
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetTruckCropD(DateTime startTime, DateTime endTime, int type)
        {
            if (type != 1 && type != 2)
                return null;
            string extSql = string.Empty;
            if (type == 1)
                extSql = " AND H.BILL_TYPE = 120 ";
            else
                extSql = " AND H.BILL_TYPE IN (121, 122, 123) ";
            string sql = "SELECT COUNT(DISTINCT H.BILL_ID) AS '打印订单数量', " +
                "ROUND(SUM(IFNULL(D.PICK_QTY, 0) / IFNULL(D.QTY, 0) / IFNULL(D.SUIT_NUM, 0) * D.PRICE), 2) " +
                "AS '实际发货金额' FROM WM_SO_HEADER H " +
                "INNER JOIN wm_so_detail D ON H.BILL_ID = D.BILL_ID " +
                "WHERE H.BILL_STATE = '68' AND H.PRINTED > 0 " + extSql +
                "AND H.CLOSE_DATE BETWEEN @StartTime AND @EndTime";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询装车金额（D类库）
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetTruckPriceD(DateTime startTime, DateTime endTime, int type)
        {
            if (type != 1 && type != 2)
                return null;
            string extSql = string.Empty;
            if (type == 1)
                extSql = " AND H.BILL_TYPE = 120 ";
            else
                extSql = " AND H.BILL_TYPE IN (121, 122, 123) ";
            string sql = "SELECT SUM(D.PICK_QTY) AS '实际出库数量', " +
                "ROUND(SUM(IFNULL(D.PICK_QTY, 0) / IFNULL(D.QTY, 2) / IFNULL(D.SUIT_NUM, 0)*D.PRICE), 2) " +
                "AS '实际出库金额' FROM WM_SO_DETAIL D INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE = '68' AND D.IS_CASE = 1 " + extSql +
                "AND H.CLOSE_DATE BETWEEN @StartTime AND @EndTime";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询配送完成件数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetTruckCount(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.销售单配送完成件数, B.调拨单配送完成件数 FROM ( " +
                "SELECT ROUND(IFNULL(SUM(IFNULL(wsd.PICK_QTY, 0)), 0), 0) AS '销售单配送完成件数' " +
                "FROM wm_so_detail wsd WHERE wsd.BILL_ID IN ( " +
                "SELECT wsh.BILL_ID FROM wm_so_header wsh WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE = 120 " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) " +
                "AND wsd.BILL_TYPE = 120) A, ( " +
                "SELECT ROUND(IFNULL(SUM(IFNULL(wsd.PICK_QTY, 0)), 0), 0) AS '调拨单配送完成件数' " +
                "FROM wm_so_detail wsd WHERE wsd.BILL_ID IN ( " +
                "SELECT wsh.BILL_ID FROM wm_so_header wsh WHERE wsh.BILL_STATE = '68' " +
                "AND wsh.BILL_TYPE IN (121, 122, 123) " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) " +
                "AND wsd.BILL_TYPE IN (121, 122, 123)) B";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询配送完成单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetTruckCrop(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.销售单配送完成单数, B.调拨单配送完成单数 FROM ( " +
                "SELECT COUNT(1) AS '销售单配送完成单数' FROM wm_so_header wsh " +
                "WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE = 120 " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) A, " +
                "(SELECT COUNT(1) AS '调拨单配送完成单数' FROM wm_so_header wsh " +
                "WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE IN (121, 122, 123)" +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) B";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询验收单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetCheckCrop(DateTime startTime, DateTime endTime)
        {
            string sql = @"SELECT A.入库单完成单数, B.退货单完成单数, C.销售单完成单数, D.调拨单完成单数 FROM " +
                "(SELECT COUNT(1) AS '入库单完成单数' FROM WM_ASN_HEADER WAH WHERE WAH.BILL_STATE = 27 " +
                "AND WAH.BILL_TYPE IN (1, 2, 3) AND WAH.CLOSE_DATE >= @StartTime AND WAH.CLOSE_DATE <= @EndTime) A, " +
                "(SELECT COUNT(1) AS '退货单完成单数' FROM WM_ASN_HEADER WAH WHERE WAH.BILL_STATE = 27 " +
                "AND WAH.BILL_TYPE IN (4, 5) AND WAH.CLOSE_DATE >= @StartTime AND WAH.CLOSE_DATE <= @EndTime) B, " +
                "(SELECT COUNT(1) AS '销售单完成单数' FROM WM_SO_HEADER WSH WHERE WSH.BILL_STATE = 68 " +
                "AND wsh.BILL_TYPE = 120 AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) C, " +
                "(SELECT COUNT(1) AS '调拨单完成单数' FROM WM_SO_HEADER WSH WHERE WSH.BILL_STATE = 68 " +
                "AND wsh.BILL_TYPE IN (121, 122, 123) AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) D ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询验收金额
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetCheckPrice(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.入库单验收金额, B.退货单验收金额, C.销售单验收金额, D.调拨单验收金额 FROM " +
                "(SELECT ROUND(IFNULL(SUM(IFNULL(D.PUT_QTY, 0) * IFNULL(D.PRICE, 0)), 0), 2) AS '入库单验收金额' " +
                "FROM WM_ASN_DETAIL D WHERE D.BILL_ID IN " +
                "(SELECT H.BILL_ID FROM WM_ASN_HEADER H WHERE H.BILL_STATE = 27 AND H.BILL_TYPE IN (1, 2, 3) " +
                "AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE <= @EndTime)) A," +
                "(SELECT ROUND(IFNULL(SUM(IFNULL(D.PUT_QTY, 0) * IFNULL(D.PRICE, 0)), 0), 2) AS '退货单验收金额' " +
                "FROM WM_ASN_DETAIL D WHERE D.BILL_ID IN " +
                "(SELECT H.BILL_ID FROM WM_ASN_HEADER H WHERE H.BILL_STATE = 27 AND H.BILL_TYPE IN (4, 5) " +
                "AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE <= @EndTime)) B, " +
                "(SELECT ROUND(IFNULL(SUM(IFNULL(D.PRICE, 0) * IFNULL(D.PICK_QTY, 0)), 0), 2) AS '销售单验收金额' " +
                "FROM WM_SO_HEADER H LEFT JOIN WM_SO_DETAIL D ON D.BILL_ID = H.BILL_ID WHERE H.BILL_STATE = 68 " +
                "AND H.BILL_TYPE = 120 AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE <= @EndTime) C, " +
                "(SELECT ROUND(IFNULL(SUM(IFNULL(D.PRICE, 0) * IFNULL(D.PICK_QTY, 0)), 0), 2) AS '调拨单验收金额' " +
                "FROM WM_SO_HEADER H LEFT JOIN WM_SO_DETAIL D ON D.BILL_ID = H.BILL_ID WHERE H.BILL_STATE = 68 " +
                "AND H.BILL_TYPE IN (121, 122, 123) AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE <= @EndTime) D ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });

        }

        /// <summary>
        /// 送货总金额
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetDeliverGoodsPrice(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.销售单应收总金额, B.调拨单应收总金额 FROM " +
                "(SELECT IFNULL(SUM(IFNULL(wsh.RECEIVE_AMOUNT, 0)), 0) AS '销售单应收总金额' " +
                "FROM wm_so_header wsh " +
                "WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE = 120 " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) A, " +
                "(SELECT IFNULL(SUM(IFNULL(wsh.RECEIVE_AMOUNT, 0)), 0) AS '调拨单应收总金额' " +
                "FROM wm_so_header wsh " +
                "WHERE wsh.BILL_STATE = '68' AND wsh.BILL_TYPE IN (121, 122, 123) " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) B";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 打印发货单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetPrintDeliverGoodsCrop(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT A.打印销售单数, B.打印调拨单数 FROM ( " +
                "SELECT COUNT(1) AS '打印销售单数' FROM wm_so_header wsh " +
                "WHERE wsh.PRINTED >= 1 AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND wsh.BILL_TYPE = '120'  AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) A, ( " +
                "SELECT COUNT(1) AS '打印调拨单数' FROM wm_so_header wsh " +
                "WHERE wsh.PRINTED >= 1 AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) >= @StartTime " +
                "AND wsh.BILL_TYPE IN (121, 122, 123) " +
                "AND IFNULL(wsh.CLOSE_DATE, wsh.LAST_UPDATETIME) <= @EndTime) B ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 根据时间查询装车金额(C类库)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static decimal GetTruckPriceC(DateTime startTime, DateTime endTime)
        {
            string sql = @"SELECT ROUND(SUM(IFNULL(d.PICK_QTY, 0) / IFNULL(d.QTY, 0) / IFNULL(d.SUIT_NUM, 0) * d.PRICE), 2) as '夜班装车金额'
              FROM WM_SO_CONTAINER_MOVE w 
              INNER JOIN wm_so_detail d ON w.BILL_ID = d.BILL_ID 
              WHERE w.CREATE_DATE BETWEEN @starttime AND @endtime";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<decimal>(sql, new { starttime = startTime, endtime = endTime });
        }

        /// <summary>
        /// 根据时间查询装车单量(C类库)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string GetTruckCropC(DateTime startTime, DateTime endTime)
        {
            string sql = @"SELECT COUNT(DISTINCT LC_CODE) as '夜班装车单量' FROM wm_so_container_move WHERE CREATE_DATE BETWEEN @starttime AND @endtime";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<object>(sql, new { starttime = startTime, endtime = endTime }).ToString();
        }
        /// <summary>
        /// 根据时间查询入库单量和总件数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetAsnBillCountAndTotalPiece(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT H.BILL_NO 订单编号, IFNULL(SUM(D.PUT_QTY), 0) 总件数 " +
                "FROM WM_ASN_HEADER H " +
                "LEFT JOIN WM_ASN_DETAIL D ON D.BILL_ID = H.BILL_ID " +
                "WHERE H.BILL_STATE = 27 AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE < @EndTime " +
                "GROUP BY D.BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        public static DataTable GetCrnReport(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT H.BILL_NO 订单编号, C.ITEM_DESC 单据类型, IFNULL(SUM(D.CHECK_QTY), 0) 件数, " +
                "IFNULL(SUM(D.PUT_QTY), 0) 上架数 FROM WM_CRN_HEADER H " +
                "LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = H.BILL_TYPE " +
                "LEFT JOIN WM_CRN_DETAIL D ON D.BILL_ID = H.BILL_ID " +
                "WHERE (H.BILL_TYPE = 4 OR H.BILL_TYPE = 5) AND H.BILL_STATE = 27 " +
                "AND H.CLOSE_DATE >= @StartTime AND H.CLOSE_DATE <= @EndTime " +
                "GROUP BY D.BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { StartTime = startTime, EndTime = endTime });
        }

        /// <summary>
        /// 统计某个人在指定时间内产生的绩效（）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static DataTable SummaryByPersonnel(DateTime dateBegin, DateTime dateEnd)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_DATE_BEGIN", dateBegin);
            parms.Add("V_DATE_END", dateEnd);

            return map.LoadTable("P_REPORT_SUMMARY", parms, CommandType.StoredProcedure);
        }
        //获取容器位信息
        public static DataTable GetContainerInfo()
        {
            string sql = @"SELECT A.CTL_NAME,A.CTL_STATE,A.CTL_TYPE,C.ITEM_DESC,B.CT_CODE,B.BILL_HEAD_ID,D.BILL_NO,E.ITEM_DESC BILL_STATE FROM wm_container_location A
                          LEFT JOIN wm_base_code C ON A.CTL_TYPE=C.ITEM_VALUE
                          LEFT JOIN wm_container_state B ON A.CTL_NAME=B.LC_CODE
                          LEFT JOIN wm_so_header D ON B.BILL_HEAD_ID=D.BILL_ID
                          LEFT JOIN wm_base_code E ON D.BILL_STATE=E.ITEM_VALUE
                          WHERE A.IS_DELETE=1; ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
    }
}
