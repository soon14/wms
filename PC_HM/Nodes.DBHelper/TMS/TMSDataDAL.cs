using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;

namespace Nodes.DBHelper
{
    public class TMSDataDAL
    {

        public static string GetVHtype(TMSDataHeader header)
        {
            string sql = string.Format("SELECT wbc.ITEM_DESC FROM tms_data_header tdh "+
                              "JOIN wm_base_code wbc ON tdh.VH_TYPE =wbc.ITEM_VALUE AND wbc.GROUP_CODE =131 " +
                              "WHERE tdh.GROUP_NO ='{0}'", header.id);
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql);
        }

        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="header">分组对象</param>
        /// <returns>新增结果</returns>
        public static int Insert(TMSDataHeader header)
        {
            string sql = @"INSERT INTO TMS_DATA_HEADER(GROUP_NO, VH_TYPE, WH_CODE, CREATE_DATE, START_TIME, 
            ATTRI_1, ATTRI_2, ATTRI_3, ATTRI_4, ATTRI_5) VALUES(@GroupNo, @VhType, @WhCode, NOW(),@StartTime, @Attri1, 
            @Attri2, @Attri3, @Attri4, @Attri5)";
            IMapper map = DatabaseInstance.Instance();
            //IDbTransaction trans = map.BeginTransaction();
            int result = -1;
            try
            {
                result = map.Execute(sql, new
                {
                    GroupNo = header.id,
                    VhType = header.car_type,
                    WhCode = header.storehouse,
                    StartTime = header.start_time,
                    Attri1 = header.Attri1,
                    Attri2 = header.Attri2,
                    Attri3 = header.Attri3,
                    Attri4 = header.Attri4,
                    Attri5 = header.Attri5
                });

                if (result > 0)
                {
                    foreach (string marketKey in header.order_list.Keys)
                    {
                        TMSDataMarket market = header.order_list[marketKey];

                        market.marketid = marketKey;
                        market.GroupNo = header.id;

                        result += InsertMarket(market);
                    }
                }

                //trans.Commit();
            }
            catch
            {
                result = -1;
                //trans.Rollback();
            }
            return result;
        }

        /// <summary>
        /// 新增分组
        /// </summary>
        /// <remarks>Add By 万伟超</remarks>
        /// <param name="header">市场对象</param>
        /// <returns>新增结果</returns>
        public static int InsertMarket(TMSDataMarket market)
        {
            int result = -1;

            string sql = @"INSERT INTO TMS_DATA_MARKET(MARKET_ID,GROUP_NO, X, Y,
ATTRI_1, ATTRI_2, ATTRI_3, ATTRI_4, ATTRI_5) VALUES(@MarketId, @GroupNo, @X, @Y, @Attri1, 
@Attri2, @Attri3, @Attri4, @Attri5)";

            IMapper map = DatabaseInstance.Instance();

            result = map.Execute(sql, new
            {
                MarketId = market.marketid,
                GroupNo = market.GroupNo,
                X = market.x,
                Y = market.y,
                Attri1 = market.Attri1,
                Attri2 = market.Attri2,
                Attri3 = market.Attri3,
                Attri4 = market.Attri4,
                Attri5 = market.Attri5
            });

            if (result > 0)
            {
                foreach (string detailKey in market.order_info.Keys)
                {
                    TMSDataDetail detail = market.order_info[detailKey];

                    detail.orderid = detailKey;
                    detail.MarketID = market.marketid;
                    detail.GroupNo = market.GroupNo;

                    result += InsertDetail(detail);
                }
            }

            return result;
        }

        /// <summary>
        /// 新增明细
        /// </summary>
        /// <remarks>Update By 万伟超</remarks>
        /// <param name="header">明细对象</param>
        /// <returns>新增结果</returns>
        public static int InsertDetail(TMSDataDetail detail)
        {
            string sql = @"INSERT INTO TMS_DATA_DETAIL(MARKET_ID, BILL_NO, IN_SORT,
WHLOE_QTY, BULK_QTY, ATTRI_1, ATTRI_2, ATTRI_3, ATTRI_4, ATTRI_5, GROUP_NO) VALUES(@MarketId,
@BillNo, @InSort, @WhloeQty, @BulkQty, @Attri1, @Attri2, @Attri3, @Attri4, @Attri5, @GroupNo)";

            IMapper map = DatabaseInstance.Instance();

            return map.Execute(sql, new
            {
                MarketId = detail.MarketID,
                BillNo = detail.orderid,
                InSort = detail.sort,
                WhloeQty = detail.zhengnum,
                BulkQty = detail.sannum,
                Attri1 = detail.Attri1,
                Attri2 = detail.Attri2,
                Attri3 = detail.Attri3,
                Attri4 = detail.Attri4,
                Attri5 = detail.Attri5,
                GroupNo = detail.GroupNo
            });
        }

        /// <summary>
        /// 获取表头
        /// </summary>
        /// <param name="locState">本地状态：-1：所有；0：未装车；1：已装车</param>
        /// <returns></returns>
        public static List<TMSDataHeader> Select(int locState)
        {
            string sql = @"SELECT H.HEADER_ID, H.GROUP_NO, H.VH_TYPE, H.WH_CODE, H.LOC_STATE,
                           H.CREATE_DATE, H.ATTRI_1, H.ATTRI_2, H.ATTRI_3, H.ATTRI_4, H.ATTRI_5
                            FROM TMS_DATA_HEADER H 
                           LEFT JOIN wm_loading_header wlh ON H.GROUP_NO =wlh.VH_TRAIN_NO ";
            if (locState != -1)
            {
                sql += "WHERE H.LOC_STATE = " + locState;
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TMSDataHeader>(sql);
        }

        /// <summary>
        /// 根据分组，查询待装车信息
        /// </summary>
        /// <remarks>Update By 万伟超</remarks>
        /// <param name="groupNo">分组编号</param>
        /// <returns></returns>
        public static List<SOHeaderEntity> Details(string groupNo)
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO,(CASE WHEN TDM.GROUP_NO IS NULL THEN OS.VEHICLE_NO ELSE TDM.GROUP_NO END) VEHICLE_NO, 
  A.FROM_WH_CODE, A.BILL_TYPE, C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
  C3.ITEM_DESC OUTSTORE_TYPE_NAME, A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, 
  S.CONTACT, S.PHONE,A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, 
  A.CLOSE_DATE, W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, 
  A.DELAYMARK, F_CALC_PIECES_BY_PICK(A.BILL_ID, 1) BOX_NUM,  TDM.X X_COOR, TDM.Y Y_COOR,  
  F_CALC_BULK_PIECES(A.BILL_ID) CASE_BOX_NUM, TDD.IN_SORT ORDER_SORT 
  FROM TMS_DATA_DETAIL TDD
  LEFT JOIN WM_SO_HEADER A ON A.BILL_NO = TDD.BILL_NO
  LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = A.BILL_NO 
  LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
  LEFT JOIN TMS_DATA_MARKET TDM ON TDM.MARKET_ID = TDD.MARKET_ID AND TDM.GROUP_NO = TDD.GROUP_NO
  INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
  LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
  WHERE TDD.GROUP_NO = @GroupNo AND TDD.BILL_NO NOT IN (SELECT BILL_NO FROM wm_loading_detail )
  GROUP BY A.BILL_ID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { GroupNo = groupNo });
        }



        public static int UpdateLocState(int headerID, int state)
        {
            string sql = @"UPDATE TMS_DATA_HEADER SET LOC_STATE = @State WHERE HEADER_ID = @HeaderID";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { State = state, HeaderID = headerID });
        }
    }
}
