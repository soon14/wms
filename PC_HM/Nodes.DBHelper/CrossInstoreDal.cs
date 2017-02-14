using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;

namespace Nodes.DBHelper
{
    public class CrossInstoreDal
    {
        /// <summary>
        /// 查询越库单--越库入库和无单据收货
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="billState">20-等待到货;23-等待复核;27-收货完成</param>
        /// <returns></returns>
        public List<AsnBodyEntity> QueryOverStockBills(string warehouseCode, string billState)
        {
            string sql = "SELECT H.BILL_ID, H.BILL_NO, H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, " +
            "H.S_CODE, C.C_NAME, C.NAME_S, H.BILL_TYPE, TP.ITEM_DESC BILL_TYPE_DESC, " +
            "H.CREATE_DATE, H.CONTRACT_NO, H.REMARK, H.ROW_COLOR, H.WMS_REMARK, H.CREATOR, H.ORIGINAL_BILL_NO, H.SALES_MAN, " +
            "H.PRINTED, H.PRINTED_TIME, H.INSTORE_TYPE, IP.ITEM_DESC INSTORE_TYPE_DESC " +
            "FROM WM_ASN_HEADER H " +
            "LEFT JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '104' AND H.BILL_STATE = ST.ITEM_VALUE " +
            "LEFT JOIN WM_BASE_CODE TP ON TP.GROUP_CODE = '103' AND H.BILL_TYPE = TP.ITEM_VALUE " +
            "LEFT JOIN WM_BASE_CODE IP ON IP.GROUP_CODE = '105' AND H.INSTORE_TYPE = IP.ITEM_VALUE " +
            "LEFT JOIN CUSTOMERS C ON H.S_CODE = C.C_CODE ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AsnBodyEntity>(string.Format("{0} WHERE H.WH_CODE = @WarehouseCode AND (H.INSTORE_TYPE = '31' or H.INSTORE_TYPE = '33') AND H.BILL_STATE = @BillState", sql, BillStateConst.ASN_STATE_CODE_COMPLETE),
                new
                {
                    WarehouseCode = warehouseCode,
                    BillState = billState
                });
        }

        /// <summary>
        /// 获取退库临时收货区
        /// </summary>
        /// <param name="whCode"></param>
        /// <returns></returns>
        public string GetTempZone(string whCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT IFNULL( wl.LC_CODE,'') FROM wm_location wl  JOIN wm_zone wz ON wl.ZN_CODE=wz.ZN_CODE WHERE wz.ZT_CODE='79' AND wz.WH_CODE='{0}' LIMIT 1";
            return map.ExecuteScalar<string>(String.Format(sql, whCode));
        }
        /// <summary>
        /// 越库收货确认
        /// </summary>
        public string SaveOverStock(PODetailEntity entity, string targetLocCode, string whCode)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters param = new DynamicParameters();
            param.Add("V_DETAIL_ID", entity.DetailID);
            param.Add("V_PUT_QTY", entity.PutQty);
            param.Add("V_TARGET_LC_CODE", targetLocCode);
            param.Add("V_WH_CODE", whCode);
            param.AddOut("V_RESULT_MSG", DbType.String);
            map.Execute("P_ASN_OVERSTOCK_SAVE", param, CommandType.StoredProcedure);
            return param.Get<string>("V_RESULT_MSG");
        }
        /// <summary>
        /// 更新订单明细的收货量
        /// </summary>
        public int SaveOverStock(PODetailEntity entity)
        {
            string sql = "UPDATE wm_asn_detail wad SET wad.PUT_QTY = {0},wad.LAST_UPDATETIME=NOW()  WHERE wad.ID = {1}";

            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, entity.PutQty, entity.DetailID));
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int BillState_Change(string state, AsnBodyEntity entity)
        {
            string sql = "UPDATE wm_asn_header wah SET wah.BILL_STATE = '{0}',wah.LAST_UPDATETIME=NOW(),wah.CLOSE_DATE =NOW() WHERE wah.BILL_ID = {1}";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, state, entity.BillID));
        }

        //public string GetCrossInLocation(string whCode)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    string sql = "SELECT IFNULL( wl.LC_CODE,'') FROM wm_location wl  JOIN wm_zone wz ON wl.ZN_CODE=wz.ZN_CODE WHERE wz.ZT_CODE='78' AND wz.WH_CODE='{0}' LIMIT 1";
        //    return map.ExecuteScalar<string>(String.Format(sql, whCode));
        //}
    }
}
