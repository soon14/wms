using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;
using Nodes.Entities.Inventory;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    public class StockDal
    {
        /// <summary>
        /// 返回货位是否在使用中，以防止将货位删除
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsLocationUsing(string location)
        {
            string sql = "SELECT LC_CODE FROM WM_STOCK WHERE LC_CODE = @Location";
            IMapper map = DatabaseInstance.Instance();
            string _location = map.ExecuteScalar<string>(sql, new { Location = location });
            return !string.IsNullOrEmpty(_location);
        }

        public List<StockTransEntity> QueryStock(string location, string materialName)
        {
            string sql = string.Format("SELECT STK.ID, STK.LC_CODE, Z.ZN_NAME, STK.SKU_CODE, M.SKU_NAME, " +
                "UM.UM_NAME, STK.QTY, STK.PICKING_QTY, STK.OCCUPY_QTY, STK.EXP_DATE " +
                "FROM WM_STOCK STK " +
                "INNER JOIN WM_LOCATION L ON L.LC_CODE = STK.LC_CODE AND L.IS_ACTIVE = 'Y' " +
                "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
                "INNER JOIN WM_SKU M ON STK.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_UM UM ON UM.UM_CODE = STK.UM_CODE " +
                " WHERE (@LOCATION is null or STK.LC_CODE = @LOCATION) " +
                "AND STK.QTY > 0 " +
                "{0} " +
                "ORDER BY STK.LC_CODE ASC",
                string.IsNullOrEmpty(materialName) ? "" : "AND M.SKU_NAME LIKE '%" + materialName + "%'");
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockTransEntity>(sql,
                new
                {
                    LOCATION = location
                });
        }

        /// <summary>
        /// 实时库存查询
        /// </summary>
        /// <param name="location"></param>
        /// <param name="materialCode"></param>
        /// <param name="batchNO"></param>
        /// <param name="dueDate"></param>
        /// <param name="status"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<StockRecordEntity> QueryStock(string location, string materialName, bool withZeroQty)
        {
            //string sql = string.Format("SELECT STK.ID, STK.LC_CODE, Z.ZN_NAME, STK.SKU_CODE, M.SKU_NAME,M.SPEC, " +
            //    "UM.UM_NAME, STK.QTY, STK.PICKING_QTY, STK.OCCUPY_QTY, STK.EXP_DATE, STK.LATEST_IN, STK.LATEST_OUT " +
            //    "FROM WM_STOCK STK " +
            //    "INNER JOIN WM_LOCATION L ON L.LC_CODE = STK.LC_CODE " +
            //    "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
            //    "INNER JOIN WM_SKU M ON STK.SKU_CODE = M.SKU_CODE " +
            //    "INNER JOIN WM_UM UM ON UM.UM_CODE = STK.UM_CODE " +
            //    " WHERE (@LOCATION is null or STK.LC_CODE = @LOCATION) " +
            //    "{0} " +
            //    "{1} " +
            //    "ORDER BY STK.LC_CODE ASC",
            //    string.IsNullOrEmpty(materialName) ? "" : "AND M.SKU_NAME LIKE '%" + materialName + "%'",
            //    withZeroQty ? "" : "AND STK.QTY > 0");
            string sql = string.Format(@"SELECT A.ID,A.LC_CODE,A.ZN_NAME,A.SKU_CODE,A.SKU_NAME,A.SPEC,A.UM_NAME, A.QTY,A.PICKING_QTY,
  A.OCCUPY_QTY,A.EXP_DATE,A.LATEST_IN,A.LATEST_OUT,A.EXP_DAYS,A.IS_ACTIVE, A.CREATE_DATE ,A.SKU_QUALITY
  FROM (SELECT STK.ID, STK.LC_CODE, Z.ZN_NAME, STK.SKU_CODE, M.SKU_NAME,M.SPEC, L.IS_ACTIVE,
          UM.UM_NAME, STK.QTY, STK.PICKING_QTY, STK.OCCUPY_QTY, STK.EXP_DATE, STK.LATEST_IN, 
          STK.LATEST_OUT,M.EXP_DAYS, DATE_SUB(STK.EXP_DATE,INTERVAL M.EXP_DAYS DAY) CREATE_DATE, WBC.ITEM_DESC SKU_QUALITY
          FROM WM_STOCK STK 
          INNER JOIN WM_LOCATION L ON L.LC_CODE = STK.LC_CODE
          INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
          INNER JOIN WM_SKU M ON STK.SKU_CODE = M.SKU_CODE 
          INNER JOIN WM_UM UM ON UM.UM_CODE = STK.UM_CODE 
          LEFT JOIN WM_BASE_CODE WBC ON STK.SKU_QUALITY = WBC.ITEM_VALUE " +
            " WHERE (@LOCATION is null or STK.LC_CODE = @LOCATION) " +
            "{0} " +
            "{1} " +
            "ORDER BY STK.LC_CODE ASC) A",
               string.IsNullOrEmpty(materialName) ? "" : "AND M.SKU_NAME LIKE '%" + materialName + "%'",
               withZeroQty ? "" : "AND STK.QTY > 0");
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockRecordEntity>(sql,
                new
            {
                LOCATION = location
            });
        }

        public List<StockRecordEntity> QueryStock()
        {
            string sql = "SELECT A.ID,A.LC_CODE,A.ZN_NAME,A.SKU_CODE,A.SKU_NAME,A.SPEC,A.UM_NAME, " +
                "A.QTY,A.PICKING_QTY,A.OCCUPY_QTY,A.EXP_DATE,A.LATEST_IN,A.LATEST_OUT,A.EXP_DAYS FROM (" +
                "SELECT STK.ID, STK.LC_CODE, Z.ZN_NAME, STK.SKU_CODE, M.SKU_NAME,M.SPEC, " +
               "UM.UM_NAME, STK.QTY, STK.PICKING_QTY, STK.OCCUPY_QTY, STK.EXP_DATE, STK.LATEST_IN, STK.LATEST_OUT,M.EXP_DAYS " +
               "FROM WM_STOCK STK " +
               "INNER JOIN WM_LOCATION L ON L.LC_CODE = STK.LC_CODE " +
               "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
               "INNER JOIN WM_SKU M ON STK.SKU_CODE = M.SKU_CODE " +
               "INNER JOIN WM_UM UM ON UM.UM_CODE = STK.UM_CODE " +
               "WHERE Z.ZT_CODE in ('70','71') AND STK.QTY > 0  " +
               "ORDER BY STK.LC_CODE ASC) A";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<StockRecordEntity>(sql);
        }

        /// <summary>
        /// 列出所有未上架的物料信息
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public DataTable ListNotPutawayItems(string warehouse)
        {
            string sql = "SELECT S.ID, H.BILL_NO, S.DETAIL_ID, D.ROW_NO, D.MATERIAL, M.NAM MATERIAL_NAME, S.BATCH_NO, S.DUE_DATE, S.STAT, C.NAM STATE_NAME, " +
                "S.CHECK_QTY-S.PUT_QTY QTY " +
                "FROM ASN_CHECK_SMRY S " +
                "INNER JOIN ASN_DETAIL D ON S.DETAIL_ID = D.DETAIL_ID " +
                "INNER JOIN MATERIAL M ON D.MATERIAL = M.COD " +
                "INNER JOIN ASN_HEADER H ON D.BILL_ID = H.Bill_ID " +
                "INNER JOIN CODEITEM C ON S.STAT = C.COD " +
                "where H.WAREHOUSE = @Warehouse AND S.CHECK_QTY > S.PUT_QTY";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { Warehouse = warehouse });
        }

        public void SplitComMaterial(int stockID, int qty, List<MaterialEntity> materials)
        {
            IMapper map = DatabaseInstance.Instance();

            DynamicParameters parms = new DynamicParameters();
            parms.Add("STOCK_ID", stockID);
            parms.Add("MATERIAL", null, DbType.String, ParameterDirection.Input, 30);
            parms.Add("QTY", qty);
            parms.AddOut("RET_VAL", DbType.Int32);

            foreach (MaterialEntity material in materials)
            {
                parms.Set("MATERIAL", material.MaterialCode);
                map.Execute("P_TOOL_SPLIT_MATERIAL_TEMP", parms, CommandType.StoredProcedure);
            }

            //将原库存清除
            parms = new DynamicParameters();
            parms.Add("STOCK_ID", stockID);
            parms.Add("QTY", qty);
            parms.AddOut("RET_VAL", DbType.Int32);

            map.Execute("P_TOOL_DELETE_STOCK", parms, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据库存序号修改占用数量
        /// </summary>
        /// <param name="OccupyQty"></param>
        /// <param name="StockID"></param>
        /// <returns></returns>
        public int UpdateOccupyQty(int OccupyQty, int StockID)
        {
            string sql = "update STOCK_RECORD set OCCUPY_QTY=@OCCUPY_QTY where STOCK_ID=@STOCK_ID";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { OCCUPY_QTY = OccupyQty, STOCK_ID = StockID });
        }

        public DataTable QuerySkuLog(int stockID, string skuCode, DateTime beginDate, DateTime endDate)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            string name = "";
            if (skuCode != "")
            {
                parms.Add("V_SKU_CODE", skuCode);
                parms.Add("V_BEGIN_DATE", beginDate);
                parms.Add("V_END_DATE", endDate);
                name = "P_STK_LOG_BY_SKU";
            }
            else if (stockID != 0)
            {
                parms.Add("V_STOCK_ID", stockID);
                parms.Add("V_BEGIN_DATE", beginDate);
                parms.Add("V_END_DATE", endDate);
                name = "P_STK_LOG";
            }
            else
            {
                return null;
            }
            return map.LoadTable(name, parms, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查看库存SKU情况
        /// </summary>
        /// <param name="type">0-全部；1-查看拣货区；</param>
        /// <returns></returns>
        public List<StockSKUEntity> GetStockSKU(int type)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT ws.SKU_CODE,ws1.SKU_NAME,ROUND( SUM(ws.QTY/wus.QTY),0) TotalQty, "
  + "wu.UM_NAME,ROUND(ws1.MIN_STOCK_QTY/wus.QTY,0) MIN_STOCK_QTY,ROUND(ws1.MAX_STOCK_QTY/wus.QTY,0) MAX_STOCK_QTY "
  + "FROM wm_stock ws "
  + "JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE "
  + "JOIN wm_zone wz ON wl.ZN_CODE=wz.ZN_CODE "
  + "LEFT JOIN wm_um_sku wus ON ws.SKU_CODE=wus.SKU_CODE AND wus.SKU_LEVEL=3 "
  + "JOIN wm_um wu ON wus.UM_CODE=wu.UM_CODE "
  + "JOIN wm_sku ws1 ON wus.SKU_CODE=ws1.SKU_CODE ";
            if (type == 1)
            {
                sql += "WHERE wz.ZT_CODE='70' ";
            }
            sql += "GROUP BY ws.SKU_CODE ORDER BY TotalQty DESC ";

            return map.Query<StockSKUEntity>(sql);
        }
        #region zhangyj 查看库存占用货主
        public DataTable GetPickingScan(int stockID)
        {
            string sql = "SELECT  wsp.BILL_ID,wsh.BILL_NO,wbc.ITEM_DESC,wsd.SKU_CODE,ws.SKU_NAME,ws.SPEC,wsp.DETAIL_ID,wsp.QTY,wu.UM_NAME " +
                        "FROM wm_so_pick wsp " +
                        "JOIN wm_so_header wsh ON wsp.BILL_ID=wsh.BILL_ID " +
                        "JOIN wm_so_detail wsd ON wsp.DETAIL_ID=wsd.ID " +
                        "JOIN wm_sku ws ON wsd.SKU_CODE=ws.SKU_CODE " +
                        "JOIN wm_um_sku wus ON wsd.SKU_CODE=wus.SKU_CODE AND wus.SKU_LEVEL=1 " +
                        "JOIN wm_um wu ON wus.UM_CODE=wu.UM_CODE " +
                        "JOIN wm_base_code wbc ON wsh.BILL_STATE=wbc.ITEM_VALUE " +
                        "WHERE wsp.PICK_QTY<wsp.QTY AND wsp.STOCK_ID={0}";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(String.Format(sql, stockID));
        }
        #endregion
        public StockRecordEntity FindStock(int stockID)
        {
            string sql = "SELECT ID, LC_CODE, SKU_CODE, QTY, PICKING_QTY, OCCUPY_QTY, EXP_DATE, LATEST_IN, LATEST_OUT " +
                "FROM WM_STOCK WHERE ID = @StockID";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<StockRecordEntity>(sql, new { StockID = stockID });
        }
        public int DeleteStock(int stockID)
        {
            //先查看是否数量全部为0
            StockRecordEntity s = FindStock(stockID);
            if (s == null)
                return -1;
            else if (s.Qty != 0 || s.PickingQty != 0 || s.OccupyQty != 0)
                return -2;
            else
            {
                string sql = "DELETE FROM WM_STOCK WHERE ID = @StockID";
                IMapper map = DatabaseInstance.Instance();
                return map.Execute(sql, new { StockID = stockID });
            }
        }

        #region zhangyj 集货区查询
        /// <summary>
        /// 按照SKU统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetTempStockBySKU()
        {
            string sql1 = "SELECT D.SKU_NAME '商品名称',A.SKU_CODE '商品编码',D.SPEC '商品规格',ROUND( A.QTY,0) '数量', "
            + "E.UM_NAME '单位名称',A.UM_CODE '单位编码' FROM wm_stock A "
            + "INNER JOIN wm_location B ON A.LC_CODE=B.LC_CODE "
            + "INNER JOIN wm_zone C ON B.ZN_CODE=C.ZN_CODE "
            + "INNER JOIN wm_sku D ON A.SKU_CODE=D.SKU_CODE "
            + "INNER JOIN wm_um E ON A.UM_CODE=E.UM_CODE "
            + "WHERE C.ZT_CODE='77' AND A.QTY>0 "
            + "GROUP BY A.SKU_CODE "
            + "ORDER BY A.QTY DESC ;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadDataSet(sql1);
        }
        /// <summary>
        /// 按照托盘统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetTempStockByCTCode()
        {
            string sql1 = "SELECT  A.CT_CODE '托盘编号',D.ITEM_DESC '托盘状态',C.BILL_NO '订单编号',E.ITEM_DESC '订单状态',A.LC_CODE '托盘位' "
                        + "FROM wm_container_state A "
                        + "INNER JOIN wm_so_pick_record B ON A.CT_CODE=B.CT_CODE "
                        + "INNER JOIN wm_so_header C ON A.BILL_HEAD_ID=C.BILL_ID "
                        + "INNER JOIN wm_base_code D ON A.CT_STATE=D.ITEM_VALUE "
                        + "INNER JOIN wm_base_code E ON C.BILL_STATE=E.ITEM_VALUE "
                        + "WHERE A.CT_STATE='84' "
                        + "GROUP BY A.CT_CODE;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadDataSet(sql1);
        }
        /// <summary>
        /// 按照订单统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetTempStockByBill()
        {
            string sql1 = "SELECT  A.BILL_NO '订单编号',E.ITEM_DESC '订单状态',GROUP_CONCAT(B.CT_CODE) '托盘列表' FROM wm_so_header A "
                        + "INNER JOIN wm_base_code E ON A.BILL_STATE=E.ITEM_VALUE "
                        + "INNER JOIN wm_container_state B ON A.BILL_ID=B.BILL_HEAD_ID "
                        + "WHERE A.BILL_STATE>63 AND A.BILL_STATE < 68 "
                        + "GROUP BY A.BILL_NO "
                        + "ORDER BY A.CONFIRM_DATE;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadDataSet(sql1);
        }
        #endregion

        public static DataTable QueryReplenishStock()
        {
            string sql = @"SELECT S.SKU_CODE '物料编码', WS.SKU_NAME '商品名称', 
  (CASE WHEN SUM(IFNULL(S.QTY, 0)) - IFNULL(T.QTY, 0) < 0 THEN 0 ELSE SUM(IFNULL(S.QTY, 0)) - IFNULL(T.QTY, 0) END) '备货区库存', 
  U.UM_NAME '单位' 
  FROM WM_STOCK S
  LEFT JOIN WM_LOCATION L ON L.LC_CODE = S.LC_CODE
  LEFT JOIN WM_ZONE Z ON Z.ZN_CODE = L.ZN_CODE
  LEFT JOIN (SELECT D.SKU_CODE, SUM(IFNULL(D.QTY, 0)) QTY FROM TASKS T
              LEFT JOIN WM_TRANS_DETAIL D ON D.BILL_ID = T.BILL_ID
              WHERE T.TASK_TYPE = '144'
              GROUP BY D.SKU_CODE) T ON T.SKU_CODE = S.SKU_CODE
  LEFT JOIN WM_SKU WS ON WS.SKU_CODE = S.SKU_CODE
  LEFT JOIN WM_UM U ON U.UM_CODE = S.UM_CODE
  WHERE L.IS_ACTIVE = 'Y' AND Z.ZT_CODE = '71' AND S.QTY > 0
  GROUP BY S.SKU_CODE";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        public static int QueryNoActiveLocBySku(string skuStr)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM WM_STOCK S
  LEFT JOIN WM_LOCATION L ON L.LC_CODE = S.LC_CODE
  LEFT JOIN WM_ZONE Z ON Z.ZN_CODE = L.ZN_CODE
  WHERE Z.ZT_CODE = '70' AND S.SKU_CODE IN ({0}) AND S.QTY > 0 ", skuStr);
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sql, new { SkuStr = skuStr });
            return obj == null || ConvertUtil.ToInt(obj) == 0 ? 0 : ConvertUtil.ToInt(obj);
        }

        /// <summary>
        ///  添加商品质量
        /// </summary>
        /// <param name="stockId">库存Id</param>
        /// <returns></returns>
        public int UpdateSkuQuality(int skuQuatity, int stockId) 
        {
            string sql = string.Format("UPDATE wm_stock set sku_quality = {0} WHERE ID = {1}", skuQuatity, stockId);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }
    }
}
