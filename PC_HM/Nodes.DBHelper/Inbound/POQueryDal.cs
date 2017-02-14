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
    public class POQueryDal
    {
        public const string PO_HEADER_FIELD = "SELECT H.BILL_ID, H.ORG_CODE, H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, " +
            "H.SUPPLIER, S.SUP_NAME, S.NAME_S, H.BILL_TYPE, TP.ITEM_DESC BILL_TYPE_DESC, " +
			"H.CREATE_DATE, H.CONTRACT_NO, H.REMARK, H.ROW_COLOR, H.CREATOR, H.SOURCE_BILL_ID, H.SALES, " +
            "H.UPDATE_DATE, H.UPDATE_BY, H.PO_STR1, H.PO_STR2, H.PO_STR3, H.PO_STR4, H.PO_NUM1, H.PO_NUM2, H.PO_DATE1, H.PO_DATE2 " +
            "FROM PO_HEADER H " +
			"INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '102' AND H.BILL_STATE = ST.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE TP ON TP.GROUP_CODE = '103' AND H.BILL_TYPE = TP.ITEM_VALUE " +
			"INNER JOIN SUPPLIERS S ON H.SUPPLIER = S.SUP_CODE ";

        /// <summary>
        /// 获取单据状态，只返回状态字段BILL_STATE、BILL_STATE_DESC、备注、颜色
        /// </summary>
        /// <param name="billID"></param>
        /// <returns>-1：不存在</returns>
        public POBodyEntity GetBillState(string billID)
        {
            if (string.IsNullOrEmpty(billID))
                return null;

            string sql = "SELECT H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, H.REMARK, H.ROW_COLOR, H.UPDATE_DATE, H.UPDATE_BY FROM PO_HEADER H " +
                "INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '102' AND H.BILL_STATE = ST.ITEM_VALUE WHERE H.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<POBodyEntity>(sql, new { BillID = billID });
        }

        public List<POBodyEntity> QueryBills(string orgCode, string billID, string billState, string supplier,
            string billType, string material, string sales, DateTime? dateFrom, DateTime? dateTo)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();

            string strWhereCondition = "WHERE H.ORG_CODE = @ORG_CODE";

            //先把组织参数添加到集合
            parms.Add("ORG_CODE", orgCode);

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

            //单据编号
            if (!string.IsNullOrEmpty(billID))
            {
                parms.Add("P_BILL_ID", billID);
                strWhereCondition += " AND H.BILL_ID = @P_BILL_ID";
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
                strWhereCondition += " AND EXISTS(SELECT 1 FROM PO_DETAIL D INNER JOIN WM_MATERIALS M ON D.MTL_CODE = M.MTL_CODE WHERE H.BILL_ID = D.BILL_ID AND (D.MTL_CODE like @P_MTL_CODE OR M.MTL_NAME LIKE @P_MTL_CODE OR M.MTL_NAME_S LIKE @P_MTL_CODE OR M.NAME_PY LIKE @P_MTL_CODE))";
            }

            string sql = string.Concat(PO_HEADER_FIELD, strWhereCondition);
            return map.Query<POBodyEntity>(sql, parms);
        }

        /// <summary>
        /// 查询未关闭的采购单
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public List<POBodyEntity> QueryNotClosedBills(string orgCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<POBodyEntity>(string.Format("{0} WHERE H.ORG_CODE = @OrgCode AND H.BILL_STATE < '{1}'", PO_HEADER_FIELD, BillStateConst.PO_STATE_CODE_COMPLETE),
                new
                {
                    OrgCode = orgCode
                });
        }

        /// <summary>
        /// 获取单据明细列表
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<PODetailEntity> GetDetailByBillID(string billID)
        {
            string sql = "SELECT D.DETAIL_ID, D.BILL_ID, D.MTL_CODE, D.PLAN_QTY, D.REAL_QTY, D.REMARK, D.PRICE, " +
                "M.MTL_NAME, M.MTL_NAME_S, M.NAME_PY, M.SPEC, D.UG_CODE, D.UM_CODE, UG.UG_NAME, UM.UM_NAME, " +
                "M.MTL_STR1, M.MTL_STR2, M.MTL_STR3, M.MTL_STR4, M.MTL_NUM1, M.MTL_NUM2, M.MTL_DATE1, M.MTL_DATE2 " +
                "FROM PO_DETAIL D " +
                "INNER JOIN WM_MATERIALS M ON D.MTL_CODE = M.MTL_CODE " +
                "LEFT JOIN UNIT_GROUP UG ON UG.UG_CODE = D.UG_CODE " +
                "LEFT JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<PODetailEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 获取单据头信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public POBodyEntity GetBillHeader(string billID)
        {
            string sql = PO_HEADER_FIELD + "WHERE H.BILL_ID = @BillID";

            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<POBodyEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 采购明细表：表头与明细在一块
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="billID"></param>
        /// <param name="contractNO"></param>
        /// <param name="supplier"></param>
        /// <param name="billType"></param>
        /// <param name="material"></param>
        /// <param name="sales"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable PoList(string orgCode, string billID, string contractNO, string supplier,
            string billType, string material, string sales, DateTime dateFrom, DateTime dateTo)
        {
            string sql = "SELECT H.BILL_ID, H.ORG_CODE, H.BILL_STATE, ST.ITEM_DESC BILL_STATE_DESC, " +
            "H.SUPPLIER, S.SUP_NAME, S.NAME_S, H.BILL_TYPE, TP.ITEM_DESC BILL_TYPE_DESC, " +
            "H.CREATE_DATE, H.CONTRACT_NO, H.REMARK, H.ROW_COLOR, H.CREATOR, H.SOURCE_BILL_ID, H.SALES, " +
            "H.UPDATE_DATE, H.UPDATE_BY, " +
            "H.UPDATE_DATE, H.UPDATE_BY, H.PO_STR1, H.PO_STR2, H.PO_STR3, H.PO_STR4, H.PO_NUM1, H.PO_NUM2, H.PO_DATE1, H.PO_DATE2, " +
            "D.DETAIL_ID, D.MTL_CODE, D.PLAN_QTY, D.REAL_QTY, D.REMARK, D.PRICE, " +
            "M.MTL_NAME, M.MTL_NAME_S, M.NAME_PY, M.SPEC, UM.UM_NAME, M.MTL_STR1, " +
            "M.MTL_STR2, M.MTL_STR3, M.MTL_STR4, M.MTL_NUM1, M.MTL_NUM2, M.MTL_DATE1, M.MTL_DATE2 " +
            "FROM PO_HEADER H " +
            "INNER JOIN PO_DETAIL D ON H.BILL_ID = D.BILL_ID " +
            "INNER JOIN WM_MATERIALS M ON D.MTL_CODE = M.MTL_CODE " +
            "LEFT JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
            "INNER JOIN WM_BASE_CODE ST ON ST.GROUP_CODE = '102' AND H.BILL_STATE = ST.ITEM_VALUE " +
            "INNER JOIN WM_BASE_CODE TP ON TP.GROUP_CODE = '103' AND H.BILL_TYPE = TP.ITEM_VALUE " +
            "INNER JOIN SUPPLIERS S ON H.SUPPLIER = S.SUP_CODE " +
            "WHERE H.ORG_CODE = @OrgCode " +
            "AND (H.CREATE_DATE BETWEEN @DateFrom AND @DateTo) " +
            "AND (@BillID is null or H.BILL_ID = @BillID) " +
            "AND (@ContractNO is null or H.CONTRACT_NO = @ContractNO) " +
            "AND (@Supplier is null or H.SUPPLIER = @Supplier) " +
            "AND (@BillType is null or H.BILL_TYPE = @BillType) " +
            "AND (@Material is null or D.MTL_CODE = @Material) " +
            "AND (@Sales is null or H.SALES = @Sales)";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new
            {
                OrgCode = orgCode,
                DateFrom = dateFrom,
                DateTo = dateTo,
                BillID = billID,
                ContractNO = contractNO,
                Supplier = supplier,
                BillType = billType,
                Material = material,
                Sales = sales

            });
        }

        /// <summary>
        /// 取建单-一审-二审-...及对应的时间
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable PoDateTimes(string billID)
        {
            string sql = "SELECT '创建' EV_DESC, CREATE_DATE EV_DATE FROM PO_HEADER WHERE BILL_ID = @BillID " +
                "UNION ALL " +
                "SELECT '审批（一审）' EV_DESC, FIRST_APPROVE_DATE EV_DATE FROM PO_HEADER WHERE BILL_ID = @BillID " +
                "UNION ALL " +
                "SELECT '审批（二审）' EV_DESC, SECOND_APPROVE_DATE EV_DATE FROM PO_HEADER WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }
    }
}
