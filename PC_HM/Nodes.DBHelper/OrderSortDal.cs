using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Entities;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    public class OrderSortDal
    {
        public static void Insert(List<OrderSortEntity> list)
        {
            if (list == null || list.Count == 0)
                return;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                string sql = "INSERT INTO wm_order_sort(VEHICLE_NO, BILL_NO, IN_VEHICLE_SORT, PIECES_QTY, Attri1) " +
                    "VALUES('{0}', '{1}', {2}, {3}, {4});";
                foreach (OrderSortEntity item in list)
                {
                    map.Execute(string.Format("DELETE FROM WM_ORDER_SORT WHERE BILL_NO = '{0}' AND Attri1 = {1}", item.BillNo, item.Attri1), null, trans);
                    map.Execute(string.Format(sql, item.VehicleNo, item.BillNo, item.InVehicleSort, item.PiecesQty, item.Attri1), null, trans);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public static void Insert(OrderSortEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                string insertSql = "INSERT INTO wm_order_sort(VEHICLE_NO, BILL_NO, IN_VEHICLE_SORT, PIECES_QTY, Attri1) " +
                    "VALUES('{0}', '{1}', {2}, {3}, {4});";
                string deleteSql = "DELETE FROM WM_ORDER_SORT WHERE BILL_NO = '{0}' AND Attri1 = {1}";
                map.Execute(string.Format(deleteSql, entity.BillNo, entity.Attri1), null, trans);
                map.Execute(string.Format(insertSql, entity.VehicleNo, entity.BillNo, entity.InVehicleSort, entity.PiecesQty, entity.Attri1), null, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public static DataTable Query(DateTime beginDate, DateTime endDate)
        {
            string sql = "SELECT os.VEHICLE_NO, os.BILL_NO, os.IN_VEHICLE_SORT, " +
                "os.PIECES_QTY, OS.CREATE_DATE SORT_DATE, wsh.CREATE_DATE, C.C_NAME, c.ADDRESS " +
                "FROM wm_order_sort os " +
                "LEFT JOIN wm_so_header wsh ON os.BILL_NO = wsh.BILL_NO " +
                "LEFT JOIN customers c ON c.C_CODE = wsh.C_CODE " +
                "WHERE OS.CREATE_DATE >= @BeginDate AND OS.CREATE_DATE <= @EndDate " +
                "ORDER BY os.IN_VEHICLE_SORT DESC";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new
            {
                BeginDate = beginDate,
                EndDate = endDate
            });
        }

        public static int Delete(string billNO)
        {
            string sql = string.Format("DELETE FROM WM_ORDER_SORT WHERE BILL_NO = '{0}'", billNO);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }

        public static int Delete(string billNO, int attri)
        {
            string sql = string.Format(
                "DELETE FROM WM_ORDER_SORT WHERE BILL_NO = '{0}' AND Attri1 = {1}",
                billNO, attri);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }

        public static List<OrderSortDetailPrintEntity> Query(string vhTrainNo)
        {
            string sql = @"SELECT W.WH_NAME, SH.BILL_ID, D.BILL_NO, C.ADDRESS, C.C_NAME,
  F_CALC_PIECES_BY_PICK(SH.BILL_ID, 1) FULL_COUNT, LH.VH_ID, LD.IN_VH_SORT, SH.WMS_REMARK 
  FROM wm_vehicle_train_detail D 
  LEFT JOIN WM_SO_HEADER SH ON SH.BILL_NO = D.BILL_NO 
  LEFT JOIN CUSTOMERS C ON C.C_CODE = SH.C_CODE 
  LEFT JOIN WM_SO_DETAIL SD ON SD.BILL_ID = SH.BILL_ID AND SD.IS_CASE = 1 
  LEFT JOIN WM_WAREHOUSE W ON W.WH_CODE = SH.WH_CODE 
  LEFT JOIN WM_LOADING_DETAIL LD ON LD.BILL_NO = D.BILL_NO 
  LEFT JOIN WM_LOADING_HEADER LH ON LH.VH_TRAIN_NO = LD.VH_TRAIN_NO
  WHERE D.VH_TRAIN_NO = @VhTrainNo 
 -- GROUP BY D.BILL_NO 
  ORDER BY LD.IN_VH_SORT";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<OrderSortDetailPrintEntity>(sql, new { VhTrainNo = vhTrainNo });
        }
    }
}
