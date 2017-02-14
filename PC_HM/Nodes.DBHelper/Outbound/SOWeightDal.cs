using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nodes.Dapper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Entities.SO;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 称重模块相关数据访问类
    /// </summary>
    public class SOWeightDal
    {
        /// <summary>
        /// 根据当前扫描的容器(物流箱或托盘)查出与该容器关联的订单信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public SOHeaderEntity GetBillInfoByContainer(string containerCode)
        {
            string sql = "SELECT WSH.BILL_ID, WSH.BILL_NO, C.C_NAME, C.ADDRESS, C.CONTACT, " +
                "WSH.BILL_STATE, WSH.SYNC_STATE, WSH.CANCEL_FLAG " +
                "FROM WM_CONTAINER_STATE WCS " +
                "INNER JOIN WM_SO_HEADER WSH ON WCS.BILL_HEAD_ID = WSH.BILL_ID " +
                "LEFT JOIN CUSTOMERS C ON WSH.C_CODE = C.C_CODE " +
                "WHERE WCS.CT_CODE = @CT_CODE ";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<SOHeaderEntity>(sql, new { CT_CODE = containerCode });
        }

        /// <summary>
        /// 查询托盘订单的车辆及装车编号
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public DataTable GetBillVhNoAndVhTrainNo(string ctCode)
        {
            string sql = "SELECT wld.VH_TRAIN_NO, wlh.VH_ID, wv.VH_NO " +
              "FROM wm_container_state wcs " +
              "INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID " +
              "INNER JOIN wm_loading_detail wld ON wld.BILL_NO = wsh.BILL_NO " +
              "INNER JOIN wm_loading_header wlh ON wlh.VH_TRAIN_NO = wld.VH_TRAIN_NO " +
              "INNER JOIN wm_vehicle wv ON wv.ID = wlh.VH_ID " +
              "WHERE wcs.CT_CODE = @CT_CODE LIMIT 1;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { CT_CODE = ctCode });
        }

        public string GetBillNOS(int vhID)
        {
            string sql = @"SELECT GROUP_CONCAT(wsh.BILL_NO) BILLNOS FROM wm_so_header wsh 
                          JOIN tasks t ON wsh.BILL_ID =t.BILL_ID AND t.TASK_TYPE=143
                          JOIN wm_loading_detail wld ON wsh.BILL_NO =wld.BILL_NO
                          JOIN wm_loading_header wlh ON wld.VH_TRAIN_NO=wlh.VH_TRAIN_NO
                          WHERE wlh.VH_ID=@VH_ID";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { VH_ID = vhID });
        }

        /// <summary>
        /// 获取未称重或未验证的容器信息
        /// </summary>
        /// <param name="vhID"></param>
        /// <returns></returns>
        public DataTable GetCtCodeCanT(int vhID)
        {
            string sql = "SELECT wsh.BILL_NO ,wcs.CT_CODE,CASE WHEN wc.CT_TYPE ='50'THEN '未称重' ELSE '未验证' END AS STATE " + 
                      " FROM wm_loading_detail wld  " +
                      " JOIN wm_loading_header wlh ON wld.VH_TRAIN_NO =wlh.VH_TRAIN_NO  " +
                      " JOIN wm_so_header wsh ON wld.BILL_NO =wsh.BILL_NO AND wsh.OUTSTORE_TYPE <>'110' " +
                      " JOIN wm_container_state wcs ON wsh.BILL_ID =wcs.BILL_HEAD_ID  " +
                      " JOIN wm_container wc ON wcs.CT_CODE =wc.CT_CODE  " +
                      " WHERE wsh.BILL_STATE <68 AND wlh.VH_ID=@VH_ID AND wcs.CT_STATE NOT IN(87,893) AND wsh.BILL_STATE NOT IN ('68', '693', '692')  " +
                      " GROUP BY wsh.BILL_NO,wcs.CT_CODE;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { VH_ID = vhID });
        }
        /// <summary>
        /// 查询当前装车编号所有的订单的托盘
        /// </summary>
        /// <param name="ctCode">装车编号</param>
        /// <returns>DataTable类型的数据</returns>
        public DataTable GetCurrentVhNoAllTuoPans(string vhTrainNo)
        {
            string sql = "SELECT wld.VH_TRAIN_NO, wld.IN_VH_SORT, wcs.CT_CODE, wsh.BILL_ID, wsh.BILL_NO, wcs.CT_STATE,wcs.LC_CODE, " +
              "((IFNULL(SUM(wspr.PICK_QTY * wus.WEIGHT/wus.QTY), 0) + wc.CT_WEIGHT)/1000) CALC_WEIGHT, " +
              "(wcs.GROSS_WEIGHT/1000) GROSS_WEIGHT, SUM(ROUND(wspr.PICK_QTY/wus.QTY,0)) SAILQTY " +
              "FROM wm_container_state wcs " +
              "INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE AND wc.CT_TYPE = '50' " +
              "INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID " +
              "INNER JOIN wm_loading_detail wld ON wld.BILL_NO = wsh.BILL_NO " +
            "LEFT JOIN wm_so_pick_record wspr ON wspr.BILL_ID = wsh.BILL_ID AND wspr.CT_CODE = wcs.CT_CODE " +
            "LEFT JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID " +
            "LEFT JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID " +
            "LEFT JOIN wm_um_sku wus ON wus.UM_CODE = wsd.UM_CODE AND wus.SKU_CODE = wsd.SKU_CODE " +
              "WHERE wld.VH_TRAIN_NO = @VH_TRAIN_NO " +
              "GROUP BY wspr.BILL_ID, wspr.CT_CODE " +
              "ORDER BY wld.IN_VH_SORT ASC, wld.BILL_NO ASC;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { VH_TRAIN_NO = vhTrainNo });
        }

        /// <summary>
        /// 查询当前装车编号所有的订单的托盘（不含托盘自重）
        /// </summary>
        /// <param name="vhTrainNo">装车编号</param>
        /// <param name="loadingOrder">称重装车顺序</param>
        /// <param name="isIncludeWLX">是否显示订单的物流箱</param>
        /// <returns>DataTable类型的数据</returns>
        public DataTable GetCurrentVhNoAllContainers(string vhTrainNo, string loadingOrder, bool isIncludeWLX)
        {
            string sql = @"SELECT wld.VH_TRAIN_NO, wld.IN_VH_SORT, wcs.CT_CODE, wsh.BILL_ID, wsh.BILL_NO, wc.CT_TYPE, wcs.CT_STATE, 
  ((IFNULL(SUM(wspr.PICK_QTY * wus.WEIGHT/wus.QTY), 0))/1000) CALC_WEIGHT, 
  (wcs.GROSS_WEIGHT/1000) GROSS_WEIGHT,  
  CASE wc.CT_TYPE WHEN '51' THEN 1 WHEN '50' THEN SUM(ROUND(wspr.PICK_QTY/wus.QTY,0)) END SAILQTY 
  FROM wm_container_state wcs 
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE AND wc.CT_TYPE = '50' 
  INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID 
  INNER JOIN wm_loading_detail wld ON wld.BILL_NO = wsh.BILL_NO 
  LEFT JOIN wm_so_pick_record wspr ON wspr.BILL_ID = wsh.BILL_ID AND wspr.CT_CODE = wcs.CT_CODE 
  LEFT JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID 
  LEFT JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID 
  LEFT JOIN wm_um_sku wus ON wus.UM_CODE = wsd.UM_CODE AND wus.SKU_CODE = wsd.SKU_CODE 
  WHERE wld.VH_TRAIN_NO = @VH_TRAIN_NO  
  GROUP BY wspr.BILL_ID, wspr.CT_CODE 
  ORDER BY wld.IN_VH_SORT ASC, wld.BILL_NO ASC, wcs.CT_CODE ASC;";
            if (loadingOrder == "1")
            {
                sql = sql.Replace("wld.IN_VH_SORT ASC", "wld.IN_VH_SORT DESC");
            }
            if (isIncludeWLX)
            {
                sql = sql.Replace("AND wc.CT_TYPE = '50'", "");
            }
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { VH_TRAIN_NO = vhTrainNo });
        }
        /// <summary>
        /// 查询当前装车编号所有的订单的托盘（不含托盘自重）[重载]
        /// </summary>
        /// <param name="vhTrainNo"></param>
        /// <param name="loadingOrder"></param>
        /// <param name="isIncludeWLX"></param>
        /// <param name="isBulkToCase">只为了函数重载</param>
        /// <returns></returns>
        public DataTable GetCurrentVhNoAllContainers(string vhTrainNo, string loadingOrder, bool isIncludeWLX, bool isBulkToCase)
        {
            string sql = @"SELECT wld.VH_TRAIN_NO, wld.IN_VH_SORT, wcs.CT_CODE, wsh.BILL_ID, wsh.BILL_NO, wc.CT_TYPE, wcs.CT_STATE,
  ((IFNULL(SUM(
    (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
      THEN (wspr.PICK_QTY/(SELECT t.QTY FROM wm_um_sku t 
                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                  ORDER BY t.SKU_LEVEL DESC LIMIT 1))
      ELSE wspr.PICK_QTY
      END)
    * 
    (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
      THEN (SELECT t.WEIGHT FROM wm_um_sku t 
                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                  ORDER BY t.SKU_LEVEL DESC LIMIT 1)
      ELSE wus.WEIGHT/wus.QTY
      END)
  ), 0))/1000) CALC_WEIGHT, 
  (wcs.GROSS_WEIGHT/1000) GROSS_WEIGHT,
    
  (CASE wc.CT_TYPE WHEN '51' THEN 1 
  WHEN '50' THEN SUM(

  CASE wsd.IS_CASE WHEN 2 THEN 
    ROUND((wspr.PICK_QTY/(SELECT t.QTY FROM wm_um_sku t 
                                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                                  ORDER BY t.SKU_LEVEL DESC LIMIT 1)),0)
  ELSE ROUND(wspr.PICK_QTY/wus.QTY,0) END 

  ) END ) SAILQTY 

  FROM wm_container_state wcs 
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE AND wc.CT_TYPE = '50' 
  INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID 
  INNER JOIN wm_loading_detail wld ON wld.BILL_NO = wsh.BILL_NO 
  LEFT JOIN wm_so_pick_record wspr ON wspr.BILL_ID = wsh.BILL_ID AND wspr.CT_CODE = wcs.CT_CODE 
  LEFT JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID 
  LEFT JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID 
  LEFT JOIN wm_um_sku wus ON wus.UM_CODE = wsd.UM_CODE AND wus.SKU_CODE = wsd.SKU_CODE
  WHERE wld.VH_TRAIN_NO = @VH_TRAIN_NO 
  GROUP BY wspr.BILL_ID, wspr.CT_CODE 
  ORDER BY wld.IN_VH_SORT ASC, wld.BILL_NO ASC, wcs.CT_CODE ASC;";
            if (loadingOrder == "1")
            {
                sql = sql.Replace("wld.IN_VH_SORT ASC", "wld.IN_VH_SORT DESC");
            }
            if (isIncludeWLX)
            {
                sql = sql.Replace("AND wc.CT_TYPE = '50'", "");
            }
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { VH_TRAIN_NO = vhTrainNo });
        }
        /// <summary>
        /// 获取订单指定托盘的记录，如果是散归整，计算整箱的重量
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public DataTable GetPickRecordsByCtCode(int billID, string ctCode)
        {
            string sql = @"SELECT wsd.SKU_CODE, ws.SKU_NAME, ws.SPEC, u.USER_NAME, wspr.PICK_DATE,
  (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
  THEN ROUND((SUM(wspr.PICK_QTY)/(SELECT t.QTY FROM wm_um_sku t 
                                    WHERE t.SKU_CODE = wsd.SKU_CODE 
                                    ORDER BY t.SKU_LEVEL DESC LIMIT 1)),0)
  ELSE ROUND((SUM(wspr.PICK_QTY)/(wus1.QTY/wus.QTY)),0) 
  END)  SAILQTY,

  (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
  THEN (SELECT t1.UM_NAME FROM wm_um_sku t 
          INNER JOIN wm_um t1 ON t1.UM_CODE = t.UM_CODE
          WHERE t.SKU_CODE = wsd.SKU_CODE               
          ORDER BY t.SKU_LEVEL DESC LIMIT 1)
  ELSE wu1.UM_NAME 
  END)  SAILUMNAME, 

  (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
  THEN ROUND((SELECT t.WEIGHT FROM wm_um_sku t 
                INNER JOIN wm_um t1 ON t1.UM_CODE = t.UM_CODE
                WHERE t.SKU_CODE = wsd.SKU_CODE               
                ORDER BY t.SKU_LEVEL DESC LIMIT 1),0)
  ELSE wus1.WEIGHT 
  END)  WEIGHT,

  (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
  THEN ROUND((SUM(wspr.PICK_QTY)/(SELECT t.QTY FROM wm_um_sku t 
                                    WHERE t.SKU_CODE = wsd.SKU_CODE 
                                    ORDER BY t.SKU_LEVEL DESC LIMIT 1)) * 
            (SELECT t.WEIGHT FROM wm_um_sku t 
              INNER JOIN wm_um t1 ON t1.UM_CODE = t.UM_CODE
              WHERE t.SKU_CODE = wsd.SKU_CODE               
              ORDER BY t.SKU_LEVEL DESC LIMIT 1),0)
  ELSE ROUND((SUM(wspr.PICK_QTY)/(wus1.QTY/wus.QTY)),0) * wus1.WEIGHT 
  END)  TotalWeight
   FROM wm_so_pick_record wspr
   INNER JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID AND wsp.LC_CODE = wspr.LC_CODE
   INNER JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID
   INNER JOIN wm_container wc ON wc.CT_CODE = wspr.CT_CODE
   LEFT JOIN wm_sku ws ON ws.SKU_CODE = wsd.SKU_CODE
   LEFT JOIN wm_um_sku wus ON wus.SKU_CODE = ws.SKU_CODE AND wus.ID = wspr.UM_SKU_ID
   LEFT JOIN wm_um_sku wus1 ON wus1.SKU_CODE = wsd.SKU_CODE AND wus1.UM_CODE = wsd.UM_CODE
   LEFT JOIN wm_um wu ON wu.UM_CODE =wus.UM_CODE
   LEFT JOIN wm_um wu1 ON wu1.UM_CODE =wus1.UM_CODE
   LEFT JOIN users u ON u.USER_CODE = wspr.USER_CODE
   WHERE wspr.BILL_ID = @BILL_ID and wspr.CT_CODE = @CT_CODE
   GROUP BY ws.SKU_CODE, ws.SPEC;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BILL_ID = billID, CT_CODE = ctCode });
        }
        /// <summary>
        /// 根据单据查询所有关联的容器以及重量
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetContainerWeightByBillID(int billID)
        {
            string sql = "SELECT S.BILL_HEAD_ID, S.CT_CODE, S.CT_STATE, C.CT_TYPE, S.LAST_UPDATETIME, (S.GROSS_WEIGHT/1000) GROSS_WEIGHT, " +
                "CASE C.CT_TYPE WHEN '50' THEN (IFNULL(SUM(R.PICK_QTY * WUS.WEIGHT/WUS.QTY), 0) + C.CT_WEIGHT)/1000 ELSE (S.GROSS_WEIGHT/1000) END AS CALC_WEIGHT " +
                "FROM WM_CONTAINER_STATE S " +
                "INNER JOIN WM_CONTAINER C ON S.CT_CODE = C.CT_CODE " +
                "LEFT JOIN WM_SO_PICK_RECORD R ON S.BILL_HEAD_ID = R.BILL_ID AND S.CT_CODE = R.CT_CODE " +
                "LEFT JOIN WM_SO_PICK P ON P.ID = R.PICK_ID " +
                "LEFT JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE S.BILL_HEAD_ID = @BillID " +
                "GROUP BY S.BILL_HEAD_ID, S.CT_CODE, S.LAST_UPDATETIME " +
                "ORDER BY S.LAST_UPDATETIME ASC ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        /// <summary>
        /// 根据单据查询所有关联的 托盘 以及重量
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetTuoPanWeightByBillID(int billID)
        {
            string sql = "SELECT S.BILL_HEAD_ID, S.CT_CODE, S.CT_STATE, C.CT_TYPE, S.LAST_UPDATETIME, (S.GROSS_WEIGHT/1000) GROSS_WEIGHT, " +
                "(IFNULL(SUM(R.PICK_QTY * WUS.WEIGHT/WUS.QTY), 0) + C.CT_WEIGHT)/1000 AS CALC_WEIGHT " +
                "FROM WM_CONTAINER_STATE S " +
                "INNER JOIN WM_CONTAINER C ON S.CT_CODE = C.CT_CODE " +
                "LEFT JOIN WM_SO_PICK_RECORD R ON S.BILL_HEAD_ID = R.BILL_ID AND S.CT_CODE = R.CT_CODE " +
                "LEFT JOIN WM_SO_PICK P ON P.ID = R.PICK_ID " +
                "LEFT JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE S.BILL_HEAD_ID = @BillID AND C.CT_TYPE = '50' " +
                "GROUP BY S.BILL_HEAD_ID, S.CT_CODE, S.LAST_UPDATETIME " +
                "ORDER BY S.LAST_UPDATETIME ASC ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        /// <summary>
        /// 获取当前容器的信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public ContainerEntity GetCurrentContainerInfo(string containerCode)
        {
            string sql = "SELECT wcs.CT_CODE, wcs.BILL_HEAD_ID, wcs.CT_STATE, wc.CT_TYPE, wc.CT_WEIGHT, wcs.GROSS_WEIGHT,wcs.NET_WEIGHT " +
  "FROM wm_container_state wcs " +
  "LEFT JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE " +
  "WHERE wcs.CT_CODE = @CT_CODE ";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<ContainerEntity>(sql, new { CT_CODE = containerCode });
        }

        /// <summary>
        /// 更新目标物流箱的状态
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public int UpdateWuliuxiangState(string wuliuxiangCode, string stateValue)
        {
            string sql = "UPDATE wm_container_state wcs SET wcs.CT_STATE = @CT_STATE WHERE wcs.CT_CODE = @CT_CODE ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CT_CODE = wuliuxiangCode, CT_STATE = stateValue });
        }

        /// <summary>
        /// 更新目标订单信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public int UpdateCurrentBillState(int billID, int shipNO, string stateValue)
        {
            string sql = "UPDATE wm_so_header wsh SET wsh.SHIP_NO=@SHIP_NO, wsh.BILL_STATE=@BILL_STATE, " +
"wsh.LAST_UPDATETIME=NOW(), wsh.CLOSE_DATE = NOW() WHERE wsh.BILL_ID = @BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILL_ID = billID, SHIP_NO = shipNO, BILL_STATE = stateValue });
        }

        /// <summary>
        /// 更新目标订单信息(重载)
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public int UpdateCurrentBillState(int billID, int shipNO)
        {
            string sql = "UPDATE wm_so_header wsh SET wsh.SHIP_NO=@SHIP_NO,  " +
"wsh.LAST_UPDATETIME=NOW() WHERE wsh.BILL_ID = @BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILL_ID = billID, SHIP_NO = shipNO });
        }

        /// <summary>
        /// 更新目标订单信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public int UpdateCurrentBillState(int billID, string stateValue)
        {
            string sql = "UPDATE wm_so_header wsh SET wsh.BILL_STATE=@BILL_STATE, " +
                         "wsh.LAST_UPDATETIME=NOW() WHERE wsh.BILL_ID = @BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILL_ID = billID, BILL_STATE = stateValue });
        }

        /// <summary>
        /// 判断当前订单总货品与散货品的差值（返回0全散单、整散单、numBulk=0全整单）
        /// </summary>
        /// <param name="billID">订单ID</param>
        /// <param name="numBulk">散货品为0表示全整</param>
        /// <returns>该单所有货品与该单散货品的差值，0表示全散,</returns>
        public int GetBulkDiffByBillID(int billID, out int numBulk)
        {
            //查询该订单共有几种 货品
            string sqlAll = string.Format("SELECT COUNT(1) FROM wm_so_detail wsd WHERE wsd.BILL_ID = '{0}';", billID);

            //查询该订单共有几种 散货
            string sqlBulk = string.Format("SELECT COUNT(1) FROM wm_so_detail wsd WHERE wsd.BILL_ID = '{0}' AND wsd.IS_CASE = 2;", billID);
            IMapper map = DatabaseInstance.Instance();
            int numAll = map.ExecuteScalar<int>(sqlAll);
            numBulk = map.ExecuteScalar<int>(sqlBulk);
            return numAll - numBulk;  //0表示全散,
        }

        /// <summary>
        /// 判断单据是否有散货
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool IsHasCase2(int billID)
        {
            string sqlCase2 = "SELECT wsd.ID FROM wm_so_detail wsd " +
                 "WHERE wsd.BILL_ID = @BillID AND wsd.IS_CASE = 2 ";
            IMapper map = DatabaseInstance.Instance();
            DataTable dt = map.LoadTable(sqlCase2, new { BillID = billID });
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 判断单据是否有指定的货品
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool IsHasCase(int billID, int isCase)
        {
            string sqlCase = "SELECT wsd.ID FROM wm_so_detail wsd " +
                 "WHERE wsd.BILL_ID = @BillID AND wsd.IS_CASE = @IsCase ";
            IMapper map = DatabaseInstance.Instance();
            DataTable dt = map.LoadTable(sqlCase, new { BillID = billID, IsCase = isCase });
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 获得系统设置某项的值 (如 物流箱标准偏差)
        /// </summary>
        /// <returns></returns>
        public object GetSystemDiffSet(string itemName)
        {
            string sql = "SELECT SET_VALUE FROM WM_SETTING WHERE SET_ITEM = '{0}'";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<object>(string.Format(sql, itemName));
        }
        /// <summary>
        /// 获取当前托盘内最轻商品的重量（销售单位）
        /// </summary>
        /// <returns></returns>
        public object GetCTCodeMinWeight(string ctCode, int billID)
        {
            string sql = "SELECT  ROUND(MIN(IFNULL(D.WEIGHT,0)),0) " +
                        "FROM wm_so_pick_record A " +
                        "JOIN wm_so_pick B ON A.PICK_ID=B.ID " +
                        "JOIN wm_so_detail C ON B.DETAIL_ID=C.ID " +
                        "JOIN wm_um_sku D ON C.SKU_CODE=D.SKU_CODE AND C.UM_CODE=D.UM_CODE " +
                        "WHERE A.CT_CODE='{0}' AND A.BILL_ID={1};";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<object>(string.Format(sql, ctCode, billID));
        }

        /// <summary>
        /// 根据车辆编号获得车辆信息
        /// </summary>
        /// <param name="vehicleNO"></param>
        /// <returns></returns>
        public VehicleEntity GetVehicleIDByNO(string vehicleNO)
        {
            string sql = "SELECT wv.ID, wv.VH_CODE, wv.VH_NO, wv.VH_VOLUME, wv.USER_CODE, wv.RT_CODE, wv.IS_ACTIVE FROM wm_vehicle wv WHERE wv.VH_NO = '{0}' ";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<VehicleEntity>(string.Format(sql, vehicleNO));
        }

        /// <summary>
        /// 更新容器状态表信息
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public int UpdateContainerStateInfo(string ctCode, string ctState, string vhCode, decimal grossWeight, decimal netWeight)
        {
            string sql = "UPDATE WM_CONTAINER_STATE " +
                "SET CT_STATE = @CTState, LC_CODE = @VHCode, GROSS_WEIGHT = @GrossWeight, NET_WEIGHT = @NetWeight, LAST_UPDATETIME=NOW() " +
                "WHERE CT_CODE = @CTCode ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CTCode = ctCode, CTState = ctState, VHCode = vhCode, GrossWeight = grossWeight, NetWeight = netWeight });
        }

        /// <summary>
        /// 写入称重记录
        /// </summary>
        /// <returns></returns>
        public int InsertWeightRecord(int billID, string ctCode, decimal grossWeight, decimal netWeight, string userCode, string authUserCode, string vhCode)
        {
            IMapper map = DatabaseInstance.Instance();
            int result = -1;

            string sql = @"SELECT COUNT(1) FROM WM_SO_WEIGHT WHERE BILL_ID = @BillID AND CT_CODE = @CtCode";
            result = ConvertUtil.ToInt(map.ExecuteScalar<object>(sql));
            if (result <= 0)
            {
                sql = "INSERT INTO WM_SO_WEIGHT(BILL_ID, CT_CODE, GROSS_WEIGHT, NET_WEIGHT, USER_CODE, AUTH_USER_CODE, CREATE_DATE, VH_CODE) " +
                "VALUES(@BILL_ID, @CT_CODE, @GROSS_WEIGHT, @NET_WEIGHT, @USER_CODE, @AUTH_USER_CODE, NOW(), @VH_CODE );";

                result = map.Execute(sql, new
                {
                    BILL_ID = billID,
                    CT_CODE = ctCode,
                    GROSS_WEIGHT = grossWeight,
                    NET_WEIGHT = netWeight,
                    USER_CODE = userCode,
                    AUTH_USER_CODE = authUserCode,
                    VH_CODE = vhCode
                });
            }
            return result;
        }

        /// <summary>
        /// 写入出库单日志
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="evt"></param>
        /// <param name="dateTime"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int InsertSoLog(int billID, string evt, DateTime dateTime, string userName)
        {
            string sql = "INSERT INTO wm_so_log(BILL_ID, EVT, CREATE_DATE, CREATOR)"
+ "VALUES(@BILL_ID, @EVT,@CREATE_DATE, @CREATOR);";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILL_ID = billID, EVT = evt, CREATE_DATE = dateTime, CREATOR = userName });
        }

        /// <summary>
        /// 查询当前扫描容器所在的车次
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public string GetVehicleTripByCurrentContainer(string containerCode)
        {
            string sql = "SELECT wos.VEHICLE_NO, wos.BILL_NO, wcs.CT_CODE " +
    "FROM wm_container_state wcs " +
    "INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID " +
    "INNER JOIN wm_order_sort wos ON wos.BILL_NO = wsh.BILL_NO " +
    "WHERE wcs.CT_CODE = @CT_CODE;";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { CT_CODE = containerCode });
        }

        /// <summary>
        /// 查询当前车次所有单据的托盘列表P_SO_WEIGHT_PALLETS
        /// </summary>
        /// <param name="s">vehicleNo</param>
        /// <returns></returns>
        public DataTable GetCurrentVehicleTripAllPallets(string vehicleNo)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("V_VEHICLE_NO", vehicleNo);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable("P_SO_WEIGHT_PALLETS", paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询当前车次的所有订单的的预估散货件数
        /// </summary>
        /// <param name="vehicleNo"></param>
        /// <returns>车次的散货总件数</returns>
        public object GetSoBillIdsByVehicleNo(string vehicleNo)
        {
            string sql = "SELECT SUM(F_CALC_BULK_PIECES(wsh.BILL_ID)) TOTAL_COUNT " +
   "FROM wm_order_sort os " +
   "LEFT JOIN wm_so_header wsh ON os.BILL_NO = wsh.BILL_NO " +
   "WHERE os.VEHICLE_NO = @VEHICLE_NO;";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<object>(sql, new { VEHICLE_NO = vehicleNo });
        }

        /// <summary>
        /// 获得当前托盘相关的订单所属的 车辆
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public string GetVTruckNameByTuoPan(string ctCode)
        {
            string sql = "SELECT wv.VH_NO, wsh.BILL_NO, wcs.CT_CODE FROM wm_so_header wsh " +
  "INNER JOIN wm_vehicle wv ON wv.ID = wsh.SHIP_NO " +
  "INNER JOIN wm_container_state wcs ON wcs.BILL_HEAD_ID = wsh.BILL_ID " +
  "WHERE wcs.CT_CODE = @CT_CODE;";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { CT_CODE = ctCode });
        }

        /// <summary>
        /// 获取指定订单的称重记录数量，不包含传入的容器编号
        /// </summary>
        /// <param name="billID">订单ID</param>
        /// <param name="ctCode">容器编号</param>
        /// <returns></returns>
        public static int GetWeightRecordsCountByBillID(int billID, string ctCode)
        {
            string sql = @"SELECT COUNT(1) FROM WM_SO_WEIGHT W 
                            WHERE W.BILL_ID = @BillID AND W.CT_CODE <> @CTCode";
            IMapper map = DatabaseInstance.Instance();
            return ConvertUtil.ToInt(map.ExecuteScalar<object>(sql, new { BillID = billID, CTCode = ctCode }));
        }

        /// <summary>
        /// 判断单据 有 指定类型的任务 的个数
        /// </summary>
        public int GetCountOfTaskByCase(int billID, int isCase)
        {
            string sqlCase2 = @"SELECT COUNT(1) FROM (
                                SELECT t.ID, t.TASK_TYPE, t.BILL_ID, t.IS_CASE FROM tasks t WHERE t.BILL_ID = @BillID
                                  UNION ALL 
                                SELECT th.ID, th.TASK_TYPE, th.BILL_ID, th.IS_CASE FROM task_history th WHERE th.BILL_ID = @BillID
                                ) A WHERE A.BILL_ID = @BillID AND A.IS_CASE = @IsCase;";
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sqlCase2, new { BillID = billID, IsCase = isCase });
            return Utils.ConvertUtil.ToInt(obj);
        }
        /// <summary>
        /// sql:查询散货任务关闭，并且物流箱都验证了
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="isCase"></param>
        /// <returns></returns>
        public bool JudgetContainerReversed(int billID, int isCase)
        {
            string sqlCase2 = @"SELECT COUNT(1)FROM wm_container_state wcs 
                              JOIN wm_container wc ON wcs.CT_CODE =wc.CT_CODE AND wc.CT_TYPE ='51'
                              WHERE wcs.BILL_HEAD_ID =@BillID AND wcs.CT_STATE <>893 
                                ";
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sqlCase2, new { BillID = billID });
            int i = Utils.ConvertUtil.ToInt(obj);
            if (i > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查当前托盘是否符合预期的 称重装车顺序
        /// </summary>
        public int CheckTuopanIsExpect(string ctCode)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters paras = new DynamicParameters();
            paras.Add("V_CT_CODE", ctCode);
            paras.AddOut("V_RESULT", DbType.Int32);
            map.Execute("P_SO_CONTAINER_WEIGHT_EXPECT", paras, CommandType.StoredProcedure);
            return paras.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 获取指定类型、非指定状态的容器个数
        /// </summary>
        public int GetNumOfContainer(int billID, string ctType, string ctState)
        {
            string sql = @"SELECT COUNT(1) 
  FROM wm_container_state wcs
  INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE AND wc.CT_TYPE = @CtType AND wcs.CT_STATE <> @CtState
  WHERE wcs.BILL_HEAD_ID = @BillID;";
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sql, new { BillID = billID, CtType = ctType, CtState = ctState });
            return Utils.ConvertUtil.ToInt(obj);
        }

        /// <summary>
        /// 检测订单物流箱是否全部在电子称上称重
        /// </summary>
        public bool IsWeightedAllWLXByBillID(int billID)
        {
            string sql1 = @"SELECT COUNT(1) FROM wm_container_state wcs
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE 
  AND wc.CT_TYPE = '51' AND (wcs.CT_STATE = '87' OR wcs.CT_STATE = '88' OR wcs.CT_STATE = '892')
  WHERE wcs.BILL_HEAD_ID = @BillID;";
            string sql2 = @"SELECT COUNT(1) FROM wm_container_state wcs
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE 
  AND wc.CT_TYPE = '51' 
  WHERE wcs.BILL_HEAD_ID = @BillID;";
            IMapper map = DatabaseInstance.Instance();
            object obj1 = map.ExecuteScalar<object>(sql1, new { BillID = billID });
            object obj2 = map.ExecuteScalar<object>(sql2, new { BillID = billID });
            int num1 = Utils.ConvertUtil.ToInt(obj1);
            int num2 = Utils.ConvertUtil.ToInt(obj2);
            return num1 == num2;
        }

        public decimal GetWLXsGrossWeight(List<string> wlxList)
        {
            string test = string.Join(",", wlxList.ToArray());
            string sql = @"SELECT SUM(wcs.GROSS_WEIGHT) 
  FROM wm_container_state wcs 
  WHERE wcs.CT_CODE IN ({0});";
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(string.Format(sql, test));
            return Convert.ToDecimal(obj);
        }
        /// <summary>
        /// 常识清空托盘位
        /// </summary>
        /// <param name="ct_code"></param>
        /// <param name="billID"></param>
        public void ClearCtl(string ct_code, int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("@V_CT_CODE", ct_code);
            p.Add("@V_BILLID", billID);
            map.Execute("P_SO_CLEAR_CONTAIER_LOCATION", p, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新容器状态表信息,写入称重是所用的地牛
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public int UpdateContainerStateSetDiNiu(string ctCode, string dnCode)
        {
            string sql = "UPDATE WM_CONTAINER_STATE " +
                "SET LC_CODE = @DNCode " +
                "WHERE CT_CODE = @CTCode ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CTCode = ctCode, DNCode = dnCode });
        }
        /// <summary>
        /// 查询当前装车编号所有的订单的托盘（理论重量含托盘和地牛自重）
        /// </summary>
        public DataTable GetCurrentVhNoAllContainers2(string vhTrainNo, string loadingOrder, bool isIncludeWLX, bool isBulkToCase)
        {
            string sql = @"SELECT wld.VH_TRAIN_NO, wld.IN_VH_SORT, wcs.CT_CODE, wsh.BILL_ID, wsh.BILL_NO, wc.CT_TYPE, wcs.CT_STATE,
  (((IFNULL(SUM(
    (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
      THEN (wspr.PICK_QTY/(SELECT t.QTY FROM wm_um_sku t 
                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                  ORDER BY t.SKU_LEVEL DESC LIMIT 1))
      ELSE wspr.PICK_QTY
      END)
    * 
    (CASE WHEN wc.CT_TYPE='50' AND wsd.IS_CASE=2 
      THEN (SELECT t.WEIGHT FROM wm_um_sku t 
                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                  ORDER BY t.SKU_LEVEL DESC LIMIT 1)
      ELSE wus.WEIGHT/wus.QTY
      END)
  ), 0))+wc.CT_WEIGHT+
  (CASE wc.CT_TYPE WHEN '50' THEN IFNULL((SELECT wc1.CT_WEIGHT FROM wm_container wc1 WHERE wc1.CT_CODE=wcs.LC_CODE),0)
  ELSE 0 END))/1000)
  CALC_WEIGHT,
  (wcs.GROSS_WEIGHT/1000) GROSS_WEIGHT,
    
  (CASE wc.CT_TYPE WHEN '51' THEN 1 
  WHEN '50' THEN SUM(

  CASE wsd.IS_CASE WHEN 2 THEN 
    ROUND((wspr.PICK_QTY/(SELECT t.QTY FROM wm_um_sku t 
                                  WHERE t.SKU_CODE = wsd.SKU_CODE 
                                  ORDER BY t.SKU_LEVEL DESC LIMIT 1)),0)
  ELSE ROUND(wspr.PICK_QTY/wus.QTY,0) END 

  ) END ) SAILQTY 

  FROM wm_container_state wcs 
  INNER JOIN wm_container wc ON wc.CT_CODE = wcs.CT_CODE AND wc.CT_TYPE = '50' 
  INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wcs.BILL_HEAD_ID 
  INNER JOIN wm_loading_detail wld ON wld.BILL_NO = wsh.BILL_NO 
  LEFT JOIN wm_so_pick_record wspr ON wspr.BILL_ID = wsh.BILL_ID AND wspr.CT_CODE = wcs.CT_CODE 
  LEFT JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID 
  LEFT JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID 
  LEFT JOIN wm_um_sku wus ON wus.UM_CODE = wsd.UM_CODE AND wus.SKU_CODE = wsd.SKU_CODE
  WHERE wld.VH_TRAIN_NO = @VH_TRAIN_NO 
  GROUP BY wspr.BILL_ID, wspr.CT_CODE 
  ORDER BY wld.IN_VH_SORT ASC, wld.BILL_NO ASC, wcs.CT_CODE ASC;";
            if (loadingOrder == "1")
            {
                sql = sql.Replace("wld.IN_VH_SORT ASC", "wld.IN_VH_SORT DESC");
            }
            if (isIncludeWLX)
            {
                sql = sql.Replace("AND wc.CT_TYPE = '50'", "");
            }
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { VH_TRAIN_NO = vhTrainNo });
        }
        /// <summary>
        /// 更新容器状态表信息
        /// </summary>
        /// <returns></returns>
        public int UpdateContainerStateInfo(string ctCode, string ctState, decimal grossWeight, decimal netWeight)
        {
            string sql = "UPDATE WM_CONTAINER_STATE " +
                "SET CT_STATE = @CTState, GROSS_WEIGHT = @GrossWeight, NET_WEIGHT = @NetWeight, LAST_UPDATETIME=NOW()  " +
                "WHERE CT_CODE = @CTCode ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CTCode = ctCode, CTState = ctState, GrossWeight = grossWeight, NetWeight = netWeight });
        }
    }
}
