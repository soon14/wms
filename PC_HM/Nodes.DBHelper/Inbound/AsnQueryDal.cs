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
    public class AsnQueryDal
    {
        public const string ASN_HEADER_FIELD = "SELECT H.BILL_ID, H.BILL_NO, H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, " +
            "H.S_CODE, C.C_NAME, C.NAME_S, H.BILL_TYPE, TP.ITEM_DESC BILL_TYPE_DESC,H.CLOSE_DATE, " +
            "H.CREATE_DATE, H.CONTRACT_NO, H.REMARK, H.ROW_COLOR, H.WMS_REMARK, H.CREATOR, H.ORIGINAL_BILL_NO, H.SALES_MAN, " +
            "H.PRINTED,H.PRINTED_TIME, H.INSTORE_TYPE, IP.ITEM_DESC INSTORE_TYPE_DESC " +
            "FROM WM_ASN_HEADER H " +
            "INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '104' AND H.BILL_STATE = ST.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE TP ON TP.GROUP_CODE = '103' AND H.BILL_TYPE = TP.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE IP ON IP.GROUP_CODE = '105' AND H.INSTORE_TYPE = IP.ITEM_VALUE " +
            "LEFT JOIN CUSTOMERS C ON H.S_CODE = C.C_CODE ";

        /// <summary>
        /// 获取单据状态，只返回状态字段BILL_STATE、BILL_STATE_DESC、备注、颜色
        /// </summary>
        /// <param name="billID"></param>
        /// <returns>-1：不存在</returns>
        public AsnBodyEntity GetBillState(int billID)
        {
            string sql = "SELECT H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, H.REMARK, H.ROW_COLOR FROM WM_ASN_HEADER H " +
                "INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '104' AND H.BILL_STATE = ST.ITEM_VALUE WHERE H.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<AsnBodyEntity>(sql, new { BillID = billID });
        }


        /// <summary>
        /// 获取单据头信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public AsnHeaderEntity GetBillHeader(int billID)
        {
            string sql = "SELECT H.BILL_ID, H.BILL_NO, H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, " +
            "H.S_CODE, C.C_NAME, C.NAME_S, H.BILL_TYPE, TP.ITEM_DESC BILL_TYPE_DESC,H.CLOSE_DATE, " +
            "H.CREATE_DATE, H.CONTRACT_NO, H.REMARK, H.ROW_COLOR, H.WMS_REMARK, H.CREATOR, H.ORIGINAL_BILL_NO, H.SALES_MAN, " +
            "H.PRINTED, IFNULL(H.PRINTED_TIME,NOW() )  PRINTED_TIME, H.INSTORE_TYPE, IP.ITEM_DESC INSTORE_TYPE_DESC " +
            "FROM WM_ASN_HEADER H " +
            "INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '104' AND H.BILL_STATE = ST.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE TP ON TP.GROUP_CODE = '103' AND H.BILL_TYPE = TP.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE IP ON IP.GROUP_CODE = '105' AND H.INSTORE_TYPE = IP.ITEM_VALUE " +
            "LEFT JOIN CUSTOMERS C ON H.S_CODE = C.C_CODE " + 
            "WHERE H.BILL_ID = @BillID";

            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<AsnHeaderEntity>(sql, new { BillID = billID });
        }

        public List<AsnBodyEntity> QueryBills(string warehouseCode, string billID, string poNO, string billState, string supplier,
            string billType, string material, string sales, DateTime? dateFrom, DateTime? dateTo, DateTime? dateComFrom, DateTime? dateComTo)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();

            string strWhereCondition = "WHERE H.WH_CODE = @WH_CODE";

            //先把仓库参数添加到集合
            parms.Add("WH_CODE", warehouseCode);

            //建单日期
            if (dateFrom.HasValue)
            {
                parms.Add("P_CREATE_DATE_FROM", dateFrom.Value);
                strWhereCondition += " AND H.CREATE_DATE >= @P_CREATE_DATE_FROM";
            }

            if (dateTo.HasValue)
            {
                parms.Add("P_CREATE_DATE_TO", dateTo.Value);
                strWhereCondition += " AND H.CREATE_DATE <= @P_CREATE_DATE_TO";
            }
            //最后更新日期
            if (dateComFrom.HasValue)
            {
                parms.Add("P_CLODE_DATE_FROM", dateComFrom.Value);
                strWhereCondition += " AND H.LAST_UPDATETIME >= @P_CLODE_DATE_FROM";
            }

            if (dateComTo.HasValue)
            {
                parms.Add("P_CLODE_DATE_TO", dateComTo.Value);
                strWhereCondition += " AND H.LAST_UPDATETIME <= @P_CLODE_DATE_TO";
            }

            //单据编号
            if (!string.IsNullOrEmpty(billID))
            {
                parms.Add("P_BILL_NO", billID);
                strWhereCondition += " AND H.BILL_NO = @P_BILL_NO";
            }

            //原采购单编号
            if (!string.IsNullOrEmpty(poNO))
            {
                parms.Add("P_PO_NO", poNO);
                strWhereCondition += " AND H.PO_NO = @P_PO_NO";
            }

            //供应商
            if (!string.IsNullOrEmpty(supplier))
            {
                parms.Add("P_SUPPLIER", supplier);
                strWhereCondition += " AND H.SUPPLIER = @P_SUPPLIER";
            }

            //业务类型
            if (!string.IsNullOrEmpty(billType))
            {
                parms.Add("P_BILL_TYPE", billType);
                strWhereCondition += " AND H.BILL_TYPE = @P_BILL_TYPE";
            }

            //业务员
            if (!string.IsNullOrEmpty(sales))
            {
                parms.Add("P_SALES", sales);
                strWhereCondition += " AND H.SALES = @P_SALES";
            }

            //状态有可能是多个，这个需要转换为OR，直接拼接成字符串，不用参数了
            if (!string.IsNullOrEmpty(billState))
            {
                //假设billState=12,13,15，函数FormatParameter转换为BILL_STATE = '12' OR BILL_STATE = '13' OR BILL_STATE = '15'
                strWhereCondition += string.Concat(" AND (", DBUtil.FormatParameter("H.BILL_STATE", billState), ")");
            }

            //物料编码或名称，支持模糊查询，因为物料在明细表中，反查出的主表数据会重复，所以要用DISTINCT
            //另外不要使用字段拼接，oracle和sql的语法不一样
            if (!string.IsNullOrEmpty(material))
            {
                parms.Add("P_MTL_CODE", material);
                strWhereCondition += " AND EXISTS(SELECT 1 FROM WM_ASN_DETAIL D INNER JOIN WM_MATERIALS M ON D.MTL_CODE = M.MTL_CODE WHERE H.BILL_ID = D.BILL_ID AND (D.MTL_CODE like @P_MTL_CODE OR M.MTL_NAME LIKE @P_MTL_CODE OR M.MTL_NAME_S LIKE @P_MTL_CODE OR M.NAME_PY LIKE @P_MTL_CODE))";
            }

            string sql = string.Concat(ASN_HEADER_FIELD, strWhereCondition);
            return map.Query<AsnBodyEntity>(sql, parms);
        }

        /// <summary>
        /// 查询未关闭的采购单
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public List<AsnBodyEntity> QueryNotClosedBills(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnBodyEntity>(string.Format("{0} WHERE H.WH_CODE = @WarehouseCode AND H.BILL_STATE < '{1}'", ASN_HEADER_FIELD, BillStateConst.ASN_STATE_CODE_COMPLETE),
                new
                {
                    WarehouseCode = warehouseCode
                });
        }
        public List<AsnBodyEntity> QueryNotRelatedBills(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnBodyEntity>(string.Format("{0} WHERE H.WH_CODE = @WarehouseCode AND H.BILL_STATE < '{1}'", ASN_HEADER_FIELD, BillStateConst.ASN_STATE_CODE_ARRIVED),
                new
                {
                    WarehouseCode = warehouseCode
                });
        }

        /// <summary>
        /// 获取单据明细列表
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<PODetailEntity> GetDetailByBillID(int billID)
        {
            string sql = "SELECT D.ID, D.BILL_ID, D.SKU_CODE, UM.UM_NAME, WUS.SKU_BARCODE BARCODE1, D.QTY, D.PUT_QTY, D.REMARK, ROUND(D.PRICE,4) PRICE, D.BATCH_NO, D.EXP_DATE, " +
                "D.SPEC, M.SKU_NAME, M.SKU_NAME_S, M.NAME_PY  " +
                "FROM WM_ASN_DETAIL D " +
                "LEFT JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "LEFT JOIN WM_UM UM ON D.UM_CODE = UM.UM_CODE " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<PODetailEntity>(sql, new { BillID = billID });
        }

        public List<AsnBodyEntity> QueryBills(string state)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnBodyEntity>(string.Format("{0} WHERE H.BILL_STATE = '{1}'", ASN_HEADER_FIELD, state));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="cardNO"></param>
        /// <param name="driver"></param>
        /// <param name="contact"></param>
        /// <param name="vehicleNO"></param>
        /// <param name="creator"></param>
        /// <returns>-1: 送货牌不存在；-2：送货牌在使用中；-3：单据状态不是等待到货</returns>
        public int CreateVechile(int billNO, string cardNO, string driver, string contact, string vehicleNO, string creator)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billNO);
            parms.Add("V_CARD_NO", cardNO);
            parms.Add("V_DRIVER", driver);
            parms.Add("V_CONTACT", contact);
            parms.Add("V_VEHICLE_NO", vehicleNO);
            parms.Add("V_CREATOR", creator);
            parms.AddOut("V_RESULT", DbType.Int32);

            map.Execute("P_ASN_SAVE_VEHICLE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="creator"></param>
        /// <returns>1:取消成功；-1：未关联任何单据，已将送货牌还原为初始状态；-2：已开始收货，无法取消</returns>
        public int CancelVechile(string cardNO, string creator)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_CARD_NO", cardNO);
            parms.Add("V_CREATOR", creator);
            parms.AddOut("V_RESULT", DbType.Int32);

            map.Execute("P_ASN_CANCEL_VEHICLE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 生成清点任务，复核任务，上架任务
        /// </summary>
        /// <param name="billlNO"></param>
        /// <param name="userQD"></param>
        /// <param name="userCheck"></param>
        /// <param name="userPutaway"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public int CreateAsnPlan(int billlNO, string userQD, string userCheck, string userPutaway,string cardNO, string creator)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("V_BILL_NO", billlNO);
            p.Add("V_USERQD", userQD);
            p.Add("V_USERCHECK", userCheck);
            p.Add("V_USERPUTAWAY", userPutaway);
            p.Add("V_CREATOR", creator);
            p.Add("V_CARD_NO", cardNO);
            p.AddOut("V_RESULT", DbType.Int32);
            map.Execute("P_ASN_CREATE_PLAN", p, CommandType.StoredProcedure);
            return p.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 已经登记，但是收货未完成的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetVehicles(int? billID, string billNO, string cardNO, string cardState)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT S.CARD_NO, S.CARD_STATE, CS.ITEM_DESC CARD_STATE_DESC, H.BILL_NO, C.C_NAME, BS.ITEM_DESC BILL_STATE_DESC, " +
                "V.CONTACT, V.DRIVER, V.VEHICLE_NO, V.CREATOR, V.CREATE_DATE " +
                "FROM WM_CARD_STATE S " +
                "LEFT JOIN WM_ASN_VEHICLE V ON S.HEADER_ID = V.BILL_ID  AND S.CARD_NO=V.CARD_NO " +
                "LEFT JOIN WM_ASN_HEADER H ON V.BILL_ID = H.BILL_ID " +
                "INNER JOIN WM_BASE_CODE CS ON S.CARD_STATE = CS.ITEM_VALUE " +
                "LEFT JOIN WM_BASE_CODE BS ON H.BILL_STATE = BS.ITEM_VALUE " +
                "LEFT JOIN CUSTOMERS C ON H.S_CODE = C.C_CODE " +
                "WHERE (@BillID IS NULL OR V.BILL_ID = @BillID) " +
                "AND (@BillNO IS NULL OR H.BILL_NO = @BillNO) " +
                "AND (@CardNO IS NULL OR V.CARD_NO = @CardNO)";

            if (!string.IsNullOrEmpty(cardState))
                sql = string.Format(sql + " AND ({0})", DBUtil.FormatParameter("S.CARD_STATE", cardState));
            return map.LoadTable(sql, new { BillID = billID, BillNO = billNO, CardNO = cardNO });
        }

        public DataTable ListCardHistory(string cardNO)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT V.CARD_NO, H.BILL_NO, C.C_NAME, BS.ITEM_DESC BILL_STATE_DESC, " +
                "V.CONTACT, V.DRIVER, V.VEHICLE_NO, V.CREATOR, V.CREATE_DATE " +
                "FROM WM_ASN_VEHICLE V " +
                "LEFT JOIN WM_ASN_HEADER H ON V.BILL_ID = H.BILL_ID " +
                "LEFT JOIN WM_BASE_CODE BS ON H.BILL_STATE = BS.ITEM_VALUE " +
                "LEFT JOIN CUSTOMERS C ON H.S_CODE = C.C_CODE " +
                "WHERE V.CARD_NO = @CardNO";

            return map.LoadTable(sql, new { CardNO = cardNO });
        }

        /// <summary>
        /// 查询托盘状态
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="containerCode"></param>
        /// <param name="containerState"></param>
        /// <returns></returns>
        public List<TrayStatusTableEntity> ListContainerState(string billNO, string containerCode, string containerState, string warehouse)
        {
            string sql = String.Format("SELECT C.CT_CODE, S.CT_STATE, ST.ITEM_DESC STATE_DESC, S.UNIQUE_CODE,  S.BILL_HEAD_ID, "
                         + " wah.BILL_NO AS 'IN_BILL', wsh.BILL_NO AS 'OUT_BILL', "
                         + " CIN.C_NAME AS 'IN_CNAME', COUT.C_NAME AS 'OUT_CNAME' "
                         + " FROM WM_CONTAINER C "
                         + " LEFT JOIN WM_CONTAINER_STATE S ON S.CT_CODE = C.CT_CODE "
                         + " LEFT JOIN WM_BASE_CODE ST ON ST.ITEM_VALUE = S.CT_STATE "
                         + " LEFT JOIN wm_asn_header wah ON S.BILL_HEAD_ID = wah.BILL_ID   "
                         + " LEFT JOIN wm_so_header wsh ON wsh.BILL_ID = S.BILL_HEAD_ID "
                         + " LEFT JOIN CUSTOMERS CIN ON CIN.C_CODE = wah.S_CODE "
                         + " LEFT JOIN CUSTOMERS COUT ON COUT.C_CODE = wsh.C_CODE "
                         + " WHERE C.CT_TYPE = '50' AND C.WH_CODE='{0}' ORDER BY IN_BILL DESC, OUT_BILL DESC", warehouse);

            TrayStatusTableEntity tste = new TrayStatusTableEntity();
            IMapper map = DatabaseInstance.Instance();
            List<TrayStatusTableEntity> list = map.Query<TrayStatusTableEntity>(sql);
            return list;
            //return map.LoadTable(sql);



            //if (!string.IsNullOrEmpty(containerState))
            //    sql = string.Format(sql + " AND ({0})", DBUtil.FormatParameter("S.CARD_STATE", containerState));
            //return map.LoadTable(sql, new { BillID = containerCode, BillNO = billNO, CardNO = containerState });
        }

        /// <summary>
        /// 查询托盘当前记录
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public DataTable GetContainerRecords(string containerCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT US.SKU_BARCODE, R.SKU_CODE, SKU.SKU_NAME, UM.UM_NAME, R.QTY, R.CREATE_DATE, R.CREATOR " +
                "FROM WM_ASN_CONTAINER R " +
                "INNER JOIN WM_UM_SKU US ON R.UM_SKU_ID = US.ID " +
                "INNER JOIN WM_SKU SKU ON US.SKU_CODE = SKU.SKU_CODE " +
                "INNER JOIN WM_UM UM ON US.UM_CODE = UM.UM_CODE " +
                "WHERE R.UNIQUE_CODE = '{0}'", containerCode);

            return map.LoadTable(sql);
        }

        /// <summary>
        /// 查询托盘当前记录 2015-6-12 17:35:01 by wangjw
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public DataTable GetContainerRecords(string ctCode, string billNo, string billType)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Empty;
            if (string.Equals(billType, "入库单据"))
            {
                sql = string.Format(@"SELECT wus.SKU_BARCODE, ws.SKU_NAME, wac.QTY, wu.UM_NAME, wac.CREATE_DATE, wac.CREATOR
  FROM wm_asn_container wac
  INNER JOIN wm_um_sku wus ON wus.ID = wac.UM_SKU_ID
  INNER JOIN wm_sku ws ON ws.SKU_CODE = wus.SKU_CODE
  INNER JOIN wm_um wu ON wu.UM_CODE = wus.UM_CODE
  INNER JOIN wm_asn_header wah ON wah.BILL_ID = wac.BILL_ID
  WHERE wac.CT_CODE = '{0}' AND wah.BILL_NO = '{1}'", ctCode, billNo);
            }
            else if (string.Equals(billType, "出库单据"))
            {
                sql = string.Format(@"SELECT wus.SKU_BARCODE, ws.SKU_NAME, wspr.PICK_QTY AS QTY, wu.UM_NAME, wspr.PICK_DATE AS CREATE_DATE, u.USER_NAME AS CREATOR
  FROM wm_so_pick_record wspr
  INNER JOIN wm_um_sku wus ON wus.ID = wspr.UM_SKU_ID
  INNER JOIN wm_sku ws ON ws.SKU_CODE = wus.SKU_CODE
  INNER JOIN wm_um wu ON wu.UM_CODE = wus.UM_CODE
  INNER JOIN users u ON u.USER_CODE = wspr.USER_CODE
  INNER JOIN wm_so_header wsh ON wsh.BILL_ID = wspr.BILL_ID
  WHERE wspr.CT_CODE = '{0}' AND wsh.BILL_NO = '{1}'", ctCode, billNo);
            }
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 查询单据日志
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetBillLog(int billID)
        {
            string sql = string.Format(@"SELECT L.EVT, L.CREATE_DATE, 
  (CASE WHEN U.USER_NAME IS NULL THEN L.CREATOR ELSE U.USER_NAME END) CREATOR
  FROM WM_ASN_LOG L
  LEFT JOIN USERS U ON U.USER_CODE = L.CREATOR
  WHERE L.BILL_ID = {0}", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        #region 2015-06-03
        public int CleanLPN(string ctCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "UPDATE wm_container_state wcs SET wcs.BILL_HEAD_ID=NULL ,wcs.CT_STATE='80',wcs.UNIQUE_CODE=NULL , "
                      + "wcs.GROSS_WEIGHT=NULL,wcs.NET_WEIGHT=NULL,wcs.LC_CODE=NULL,wcs.LAST_UPDATETIME=NOW() "
                      + "WHERE wcs.CT_CODE=@CtCode";
            return map.Execute(sql, new { CtCode = ctCode });
        }
        #endregion

        /// <summary>
        /// 获取上架记录
        /// </summary>
        /// <param name="dateBegin">起始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public static DataTable GetPutawayRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            //string sql = "SELECT R.CT_CODE, R.SKU_CODE, S.SKU_NAME, R.LC_CODE, " +
            //    "ROUND(R.QTY / US.QTY, 0) QTY, U.UM_NAME, R.PUT_TIME " +
            //    "FROM WM_ASN_PUTAWAY_RECORDS R " +
            //    "LEFT JOIN WM_ASN_DETAIL D ON D.ID = R.BILL_DETAIL_ID " +
            //    "LEFT JOIN WM_UM_SKU US ON D.UM_CODE = US.UM_CODE AND R.SKU_CODE = US.SKU_CODE " +
            //    "LEFT JOIN WM_UM U ON U.UM_CODE = D.UM_CODE " +
            //    "LEFT JOIN WM_SKU S ON S.SKU_CODE = R.SKU_CODE " +
            //    "WHERE R.PUT_BY = @UserCode AND R.PUT_TIME >= @DateBegin AND R.PUT_TIME <= @DateEnd";
            string sql = @"SELECT R.BILL_DETAIL_ID, R.CT_CODE, R.SKU_CODE, S.SKU_NAME, R.LC_CODE, ROUND(R.QTY / US.QTY, 0) QTY,
  U.UM_NAME, R.PUT_TIME 
  FROM WM_ASN_PUTAWAY_RECORDS R 
  LEFT JOIN (SELECT * FROM (SELECT A.ID, A.BILL_ID, A.SKU_CODE, A.UM_CODE, A.QTY FROM WM_ASN_DETAIL A
              UNION
              SELECT C.DETAIL_ID ID, C.BILL_ID, C.SKU_CODE, S.UM_CODE, C.QTY FROM WM_ASN_CONTAINER C
                LEFT JOIN WM_UM_SKU S ON S.ID = C.UM_SKU_ID) A LIMIT 1) D ON D.BILL_ID = R.BILL_ID 
    AND D.SKU_CODE = R.SKU_CODE AND D.ID = R.BILL_DETAIL_ID
  LEFT JOIN WM_UM_SKU US ON D.UM_CODE = US.UM_CODE AND R.SKU_CODE = US.SKU_CODE 
  LEFT JOIN WM_UM U ON U.UM_CODE = D.UM_CODE 
  LEFT JOIN WM_SKU S ON S.SKU_CODE = R.SKU_CODE  
  WHERE R.PUT_BY = @UserCode AND (R.PUT_TIME >= @DateBegin AND R.PUT_TIME <= @DateEnd)";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { @DateBegin = dateBegin, @DateEnd = dateEnd, @UserCode = userCode });
        }

        public static int GetPutawayRecordsCount(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            string sql = @"SELECT COUNT(R.CNT) QTY FROM (SELECT COUNT(1) CNT
  FROM WM_ASN_PUTAWAY_RECORDS R 
  WHERE R.PUT_BY = @UserCode AND (R.PUT_TIME >= @DateBegin AND R.PUT_TIME <= @DateEnd)
  GROUP BY R.CT_CODE, R.PUT_TIME) R";
            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sql, new { @DateBegin = dateBegin, @DateEnd = dateEnd, @UserCode = userCode });
            int result = 0;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// 获取人员入库清点记录
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static DataTable GetAsnRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            //string sql = "SELECT WAC.DETAIL_ID, WAC.CT_CODE, WAC.SKU_CODE, WAC.UM_SKU_ID, " +
            //    "ROUND(WAC.QTY, 0) CHECK_QTY, WU.UM_NAME, WAC.CREATE_DATE, WAC.CHECK_STATE, H.BILL_NO " +
            //    "FROM WM_ASN_CONTAINER WAC " +
            //    "LEFT JOIN WM_ASN_HEADER H ON H.BILL_ID = WAC.BILL_ID " +
            //    "LEFT JOIN WM_UM_SKU WUS ON WUS.ID = WAC.UM_SKU_ID " +
            //    "LEFT JOIN WM_UM WU ON WU.UM_CODE = WUS.UM_CODE " +
            //    "WHERE WAC.CREATOR = @UserCode AND (WAC.CREATE_DATE BETWEEN @DateBegin AND @DateEnd)";
            string sql = "SELECT H.BILL_NO, WAC.SKU_CODE, WS.SKU_NAME, WAC.QTY CHECK_QTY, " +
                "WU.UM_NAME, WAC.CREATE_DATE, WAC.CHECK_STATE " +
                "FROM WM_ASN_HEADER H " +
                "LEFT JOIN WM_ASN_CONTAINER WAC ON WAC.BILL_ID = H.BILL_ID " +
                "LEFT JOIN WM_UM_SKU WUS ON WUS.ID = WAC.UM_SKU_ID " +
                "LEFT JOIN WM_UM WU ON WU.UM_CODE = WUS.UM_CODE " +
                "LEFT JOIN WM_SKU WS ON WS.SKU_CODE = WAC.SKU_CODE " +
                "WHERE H.BILL_TYPE IN (1, 3) AND WAC.CREATOR = @UserCode AND " +
                "WAC.CREATE_DATE >= @DateBegin AND WAC.CREATE_DATE <= @DateEnd";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { UserCode = userCode, DateBegin = dateBegin, DateEnd = dateEnd });
        }
        /// <summary>
        /// 根据角色找人员
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<UserEntity> GetUserByRole(string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format("SELECT C.USER_CODE,C.USER_NAME FROM roles A "
                        + "JOIN user_role B ON A.ROLE_ID=B.ROLE_ID "
                        + "JOIN users C ON B.USER_CODE=C.USER_CODE "
                        + "JOIN user_online D ON C.USER_CODE=D.USER_CODE "
                        + "WHERE D.IS_ONLINE='Y' AND C.IS_DELETED IS  NULL  AND A.ROLE_NAME='{0}';", roleName);
            return map.Query<UserEntity>(sql);
        }
    }
}
