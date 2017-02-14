using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;
using Nodes.Utils;
using Nodes.DBHelper.Print;
using Nodes.Net;
using Nodes.UI;

namespace Nodes.DBHelper
{
    public class LoadingDal
    {
        #region 装车信息

        public static List<SortMapReceiveDataEntity> GetLoadingNOUnSelected(string LoadingNO)
        {
            string sql = string.Format(@"SELECT  tdd.BILL_NO orderId FROM tms_data_detail tdd
                  WHERE tdd.BILL_NO NOT IN(SELECT wld.BILL_NO FROM wm_loading_detail wld 
                  WHERE wld.VH_TRAIN_NO='{0}') AND tdd.GROUP_NO='{1}';", LoadingNO, LoadingNO);
            IMapper map=DatabaseInstance.Instance();
            return map.Query<SortMapReceiveDataEntity>(sql);

        }

        /// <summary>
        /// 生成装车信息
        /// </summary>
        /// <returns></returns>
        public static int CreateLoadingInfo(LoadingHeaderEntity header)
        {
            int result = 0;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                string sql = "INSERT INTO WM_LOADING_HEADER(WH_CODE, VH_TRAIN_NO, VH_ID, USER_NAME, UPDATE_DATE) " +
                    "VALUES(@WarehouseCode, @LoadingNO, @VehicleID, @UserName, @UpdateDate);";
                result += map.Execute(sql, new
                {
                    WarehouseCode = header.WarehouseCode,
                    LoadingNO = header.LoadingNO,
                    VehicleID = header.VehicleID,
                    UserName = header.UserName,
                    UpdateDate = header.UpdateDate
                }, trans);
                if (header.Details != null && header.Details.Count > 0)
                {
                    sql = "INSERT INTO WM_LOADING_DETAIL(VH_TRAIN_NO, BILL_NO, IN_VH_SORT, UPDATE_DATE) " +
                        "VALUES(@LoadingNO, @BillNo, @InVehicleSort, @UpdateDate);" +
                        "INSERT INTO WM_SO_LOG(BILL_ID, EVT, CREATE_DATE, CREATOR)" +
                        "VALUES(@billID,@content, NOW(), '" + header.UserName + "');";
                    foreach (LoadingDetailEntity detail in header.Details)
                    {
                        result += map.Execute(sql, new
                        {
                            LoadingNO = detail.LoadingNO,
                            BillNo = detail.BillNO,
                            InVehicleSort = detail.InVehicleSort,
                            UpdateDate = detail.UpdateDate,
                            billID = detail.BillID,
                            content = ESOOperationType.已分派装车.ToString()
                        }, trans);
                        // 更新WM_SO_HEADER表中的SHIP_NO字段
                        map.Execute(
                            "UPDATE WM_SO_HEADER H SET H.SHIP_NO = @VehicleID WHERE H.BILL_NO = @BillNO",
                            new { VehicleID = header.VehicleID, BillNO = detail.BillNO });
                    }
                }
                if (header.Users != null && header.Users.Count > 0)
                {
                    sql = "INSERT INTO WM_LOADING_USERS(VH_TRAIN_NO, USER_NAME, USER_CODE, UPDATE_DATE, ATTRI1) " +
                        "VALUES(@LoadingNO, @UserName, @UserCode, @UpdateDate, @TaskType)";
                    foreach (LoadingUserEntity user in header.Users)
                    {
                        result += map.Execute(sql, new
                        {
                            LoadingNO = user.LoadingNO,
                            UserName = user.UserName,
                            UserCode = user.UserCode,
                            UpdateDate = user.UpdateDate,
                            TaskType = user.TaskType
                        }, trans);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 获取所有装车信息表头
        /// </summary>
        /// <param name="vehicleID">车辆主键ID</param>
        /// <returns></returns>
        public static List<LoadingHeaderEntity> GetLoadingHeaders(int vehicleID, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Format(@"SELECT W.WH_NAME, H.VH_TRAIN_NO, TH.VH_TRAIN_NO TRAIN_NO, H.VH_ID, 
  V.VH_NO, H.USER_NAME, H.UPDATE_DATE, H.FINISH_DATE, TH.UPDATE_DATE TRAIN_DATE
  FROM WM_LOADING_HEADER H 
  LEFT JOIN WM_WAREHOUSE W ON W.WH_CODE = H.WH_CODE 
  LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID 
  LEFT JOIN WM_VEHICLE_TRAIN_HEADER TH ON TH.LOADING_NO = H.VH_TRAIN_NO
  WHERE H.UPDATE_DATE BETWEEN @BeginDate AND @EndDate {0} " +
                "ORDER BY H.UPDATE_DATE DESC ", vehicleID != -1 ? "AND H.VH_ID = " + vehicleID : string.Empty);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<LoadingHeaderEntity>(sql, new { BeginDate = beginDate, EndDate = endDate });
        }
        public static List<LoadingDetailEntity> GetLoadingDetails(string loadingNo)
        {
            //StringBuilder loSql = new StringBuilder();
            //loSql.AppendLine("SELECT LOADINGDETAIL.*");
            //loSql.AppendLine(" ,group_concat(distinct  WM_SO_PICK_RECORD.CT_CODE  separator '、') AS CT_CODE");
            //loSql.AppendLine(" ,group_concat(distinct  WM_CONTAINER_STATE.CT_STATE) AS CT_STATE");
            //loSql.AppendLine(" FROM (");
            //loSql.AppendLine("	SELECT D.ID");
            //loSql.AppendLine("		,D.BILL_NO");
            //loSql.AppendLine("		,H.BILL_STATE");
            //loSql.AppendLine("		,D.IN_VH_SORT");
            //loSql.AppendLine("		,F_CALC_PIECES_BY_PICK(H.BILL_ID, 1) WHOLE_COUNT");
            //loSql.AppendLine("		,F_GET_BULK_PIECES(H.BILL_ID) BULK_COUNT");
            //loSql.AppendLine("		,F_CALC_BULK_PIECES(H.BILL_iD) BULK_COUNT2");
            //loSql.AppendLine("		,D.UPDATE_DATE");
            //loSql.AppendLine("		,H.SALES_MAN");
            //loSql.AppendLine("		,H.CONTRACT_NO");
            //loSql.AppendLine("		,C.C_NAME");
            //loSql.AppendLine("		,C.CONTACT");
            //loSql.AppendLine("		,C.PHONE");
            //loSql.AppendLine("		,C.ADDRESS");
            //loSql.AppendLine("		,OS.VEHICLE_NO");
            //loSql.AppendLine("		,H.SYNC_STATE");
            //loSql.AppendLine("		,H.DELAYMARK");
            //loSql.AppendLine("		,OS.IN_VEHICLE_SORT");
            //loSql.AppendLine("		,H.BILL_iD");
            //loSql.AppendLine("		,H.BILL_TYPE");
            //loSql.AppendLine("		,H.OUTSTORE_TYPE");
            //loSql.AppendLine("		,H.PICK_ZN_TYPE");
            //loSql.AppendLine("		,H.FROM_WH_CODE");
            //loSql.AppendLine("		,H.CREATE_DATE");
            //loSql.AppendLine("		,W.WH_NAME");
            //loSql.AppendLine("	FROM WM_LOADING_DETAIL D");
            //loSql.AppendLine("	LEFT JOIN WM_SO_HEADER H ON D.BILL_NO = H.BILL_NO");
            //loSql.AppendLine("	LEFT JOIN WM_SO_DETAIL SD ON SD.BILL_ID = H.BILL_ID");
            //loSql.AppendLine("		AND SD.IS_CASE = 1");
            //loSql.AppendLine("	LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE");
            //loSql.AppendLine("	LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = H.BILL_NO");
            //loSql.AppendLine("		AND OS.Attri1 = 1");
            //loSql.AppendLine("	LEFT JOIN WM_WAREHOUSE W ON H.FROM_WH_CODE = W.WH_CODE");
            //loSql.AppendLine("	WHERE D.VH_TRAIN_NO = @LoadingNo");
            //loSql.AppendLine("		AND FLAG = 0");
            //loSql.AppendLine("	GROUP BY H.BILL_ID");
            //loSql.AppendLine("		,D.VH_TRAIN_NO");
            //loSql.AppendLine("	) AS LOADINGDETAIL");
            //loSql.AppendLine("LEFT JOIN WM_SO_PICK_RECORD ON WM_SO_PICK_RECORD.BILL_ID = LOADINGDETAIL.BILL_ID");
            //loSql.AppendLine("LEFT JOIN WM_CONTAINER_STATE ON WM_SO_PICK_RECORD.CT_CODE = WM_CONTAINER_STATE.CT_CODE");
            //loSql.AppendLine("	GROUP BY LOADINGDETAIL.ID");
            //loSql.AppendLine("    ,LOADINGDETAIL.BILL_NO");
            //loSql.AppendLine("		,LOADINGDETAIL.BILL_TYPE");
            //loSql.AppendLine("		,LOADINGDETAIL.BILL_STATE");
            //loSql.AppendLine("		,LOADINGDETAIL.OUTSTORE_TYPE");
            //loSql.AppendLine("		,LOADINGDETAIL.PICK_ZN_TYPE");
            //loSql.AppendLine("		,LOADINGDETAIL.IN_VH_SORT");
            //loSql.AppendLine("		,LOADINGDETAIL.WHOLE_COUNT");
            //loSql.AppendLine("		,LOADINGDETAIL.BULK_COUNT");
            //loSql.AppendLine("		,LOADINGDETAIL.BULK_COUNT2");
            //loSql.AppendLine("		,LOADINGDETAIL.UPDATE_DATE");
            //loSql.AppendLine("		,LOADINGDETAIL.SALES_MAN");
            //loSql.AppendLine("		,LOADINGDETAIL.CONTRACT_NO");
            //loSql.AppendLine("		,LOADINGDETAIL.C_NAME");
            //loSql.AppendLine("		,LOADINGDETAIL.CONTACT");
            //loSql.AppendLine("		,LOADINGDETAIL.PHONE");
            //loSql.AppendLine("		,LOADINGDETAIL.ADDRESS");
            //loSql.AppendLine("		,LOADINGDETAIL.VEHICLE_NO");
            //loSql.AppendLine("		,LOADINGDETAIL.SYNC_STATE");
            //loSql.AppendLine("		,LOADINGDETAIL.DELAYMARK");
            //loSql.AppendLine("		,LOADINGDETAIL.IN_VEHICLE_SORT");
            //loSql.AppendLine("		,LOADINGDETAIL.BILL_iD");
            //loSql.AppendLine("		,LOADINGDETAIL.FROM_WH_CODE");
            //loSql.AppendLine("		,LOADINGDETAIL.WH_NAME");
            //loSql.AppendLine("		,LOADINGDETAIL.CREATE_DATE");
            //loSql.AppendLine("ORDER BY LOADINGDETAIL.IN_VH_SORT");
            //IMapper map = DatabaseInstance.Instance();
            //List<LoadingDetailEntity> details = map.Query<LoadingDetailEntity>(loSql.ToString(), new { LoadingNo = loadingNo });
            //return details;
            string sql = @"SELECT D.ID, D.VH_TRAIN_NO, D.BILL_NO, H.BILL_STATE, D.IN_VH_SORT, F_CALC_PIECES_BY_PICK(H.BILL_ID, 1) WHOLE_COUNT, 
  F_GET_BULK_PIECES(H.BILL_ID) BULK_COUNT, F_CALC_BULK_PIECES(H.BILL_iD) BULK_COUNT2, 
  D.UPDATE_DATE, H.SALES_MAN, H.CONTRACT_NO, C.C_NAME, C.CONTACT, C.PHONE, C.ADDRESS, 
  OS.VEHICLE_NO, H.SYNC_STATE, H.DELAYMARK, OS.IN_VEHICLE_SORT ,H.BILL_iD, H.WMS_REMARK 
  FROM WM_LOADING_DETAIL D 
  LEFT JOIN WM_SO_HEADER H ON D.BILL_NO = H.BILL_NO 
  LEFT JOIN WM_SO_DETAIL SD ON SD.BILL_ID = H.BILL_ID AND SD.IS_CASE = 1 
  LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE 
  LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = H.BILL_NO AND OS.Attri1 = 1 
  WHERE D.VH_TRAIN_NO = @LoadingNo 
  GROUP BY H.BILL_ID, D.VH_TRAIN_NO
  ORDER BY D.IN_VH_SORT ";
            IMapper map = DatabaseInstance.Instance();
            List<LoadingDetailEntity> details = map.Query<LoadingDetailEntity>(sql, new { LoadingNo = loadingNo });
            if (details != null && details.Count > 0)
            {
                sql = "SELECT R.CT_CODE FROM WM_SO_PICK_RECORD R LEFT JOIN WM_SO_HEADER H " +
                    "ON H.BILL_ID = R.BILL_ID WHERE H.BILL_NO = @BillNo GROUP BY R.CT_CODE ";
                foreach (LoadingDetailEntity detail in details)
                {
                    detail.TrayList = map.Query<ContainerEntity>(sql, new { BillNo = detail.BillNO });
                }
            }
            return details;
        }
        public static List<LoadingUserEntity> GetLoadingUsers(string loadingNo)
        {
            string sql = "SELECT U.USER_CODE, U.USER_NAME,A.ITEM_DESC FROM WM_LOADING_USERS U "
                + " JOIN wm_base_code A ON U.ATTRI1=A.ITEM_VALUE"
                + " WHERE U.VH_TRAIN_NO = @LoadingNo ";//AND ATTRI1 = '145'
            IMapper map = DatabaseInstance.Instance();
            return map.Query<LoadingUserEntity>(sql, new { LoadingNo = loadingNo });
        }

        //public static int DeleteDetails(List<LoadingDetailEntity> list)
        //{
        //    int result = 0;
        //    string sql = "DELETE FROM WM_LOADING_DETAIL WHERE ID = @ID";
        //    IMapper map = DatabaseInstance.Instance();
        //    IDbTransaction trans = map.BeginTransaction();
        //    try
        //    {
        //        foreach (LoadingDetailEntity entity in list)
        //        {
        //            result += map.Execute("UPDATE WM_SO_HEADER H SET H.SHIP_NO=NULL WHERE H.BILL_NO=@BillNo",
        //                new { BillNo = entity.BillNO }, trans);
        //            result += map.Execute(sql, new { ID = entity.ID }, trans);
        //        }
        //        trans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    return result;
        //}
        public static bool DelleteGp(TMSDataHeader tmsDataHeader)
        {
            int result = 0;
            string sql = @"UPDATE tms_data_header tdh SET tdh.LOC_STATE =2 WHERE tdh.GROUP_NO =@GROUP_NO;
                    DELETE FROM tms_data_detail WHERE MARKET_ID IN(SELECT tdm.MARKET_ID FROM tms_data_market tdm WHERE tdm.GROUP_NO =@GROUP_NO);";
            IMapper map = DatabaseInstance.Instance();
            result = map.Execute(sql, new { GROUP_NO = tmsDataHeader.id });
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int DeleteVehicleInfo(string voLoadingNo, string voBillID, string voUserStr, string voLoadingDetail, string voUpdateHeader)
        {
            int result = 0;
            StringBuilder loSql = new StringBuilder();
            loSql.AppendLine("DELETE FROM WM_LOADING_USERS WHERE VH_TRAIN_NO = @LoadingNo;");//删除该车号下的所有装车员
            loSql.AppendLine("UPDATE WM_SO_HEADER H INNER JOIN WM_LOADING_DETAIL D ON D.BILL_NO = H.BILL_NO AND D.VH_TRAIN_NO =@LoadingNo SET H.SHIP_NO = NULL;");//更新该车号下的所有运单编号
            loSql.AppendLine("DELETE FROM WM_LOADING_DETAIL WHERE VH_TRAIN_NO = @LoadingNo;");//删除该车号下的所有装车明细

            if (!string.IsNullOrEmpty(voBillID))
                loSql.AppendLine("DELETE FROM TASKS WHERE BILL_ID IN (" + voBillID + ");");//根据订单ID删除该ID下的所有任务
            //批量增加装车员
            loSql.AppendLine("INSERT INTO WM_LOADING_USERS(VH_TRAIN_NO, USER_NAME, USER_CODE, UPDATE_DATE, ATTRI1) ");
            loSql.AppendLine(" VALUES ");
            loSql.AppendLine(voUserStr);
            if (voLoadingDetail != null && voLoadingDetail.Length > 0)
            {
                //批量增加装车明细
                loSql.AppendLine("INSERT INTO WM_LOADING_DETAIL(VH_TRAIN_NO, BILL_NO, IN_VH_SORT, UPDATE_DATE) ");
                loSql.AppendLine(" VALUES ");
                loSql.AppendLine(voLoadingDetail);
            }
            loSql.AppendLine(voUpdateHeader);

            //int result = 0;
            //string sql = "DELETE FROM WM_LOADING_DETAIL WHERE ID = @ID";
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                result = map.Execute(loSql.ToString(), new { LoadingNo = voLoadingNo }, trans);
                //foreach (LoadingDetailEntity entity in list)
                //{
                //    result += map.Execute("UPDATE WM_SO_HEADER H SET H.SHIP_NO=NULL WHERE H.BILL_NO=@BillNo",
                //        new { BillNo = entity.BillNO }, trans);
                //    result += map.Execute(sql, new { ID = entity.ID }, trans);
                //}
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;



            //IMapper map = DatabaseInstance.Instance();
            //IDbTransaction trans = map.BeginTransaction();
            //try
            //{
            //    result += map.Execute(loSql.ToString(), new { LoadingNo = loadingNo }, trans);
            //    trans.Commit();
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //    throw ex;
            //}
            //return result;
        }
        public static int InsertUser(LoadingUserEntity user)
        {
            int result = 0;
            string sql = "INSERT INTO WM_LOADING_USERS(VH_TRAIN_NO, USER_NAME, USER_CODE, UPDATE_DATE, ATTRI1) " +
                "VALUES(@LoadingNO, @UserName, @UserCode, @UpdateDate, '145')";
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                result += map.Execute(sql, new
                {
                    LoadingNO = user.LoadingNO,
                    UserName = user.UserName,
                    UserCode = user.UserCode,
                    UpdateDate = user.UpdateDate
                }, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;
        }
        public static int DeleteDetails(List<LoadingDetailEntity> list, HttpContext _httpContext, int flag)
        {

            int result = 0;
            int result1 = 0;
            string matketID = null;
            if (list != null && list.Count > 0)
            {
                string sql = @"DELETE FROM WM_LOADING_DETAIL WHERE VH_TRAIN_NO = @LoadingNo AND BILL_NO = @BillNo;
                                UPDATE WM_SO_HEADER SET SHIP_NO = NULL WHERE BILL_NO = @BillNo;";
                //                string sql1 = @"SELECT tdm.MARKET_ID FROM tms_data_market tdm
                //                                        WHERE tdm.GROUP_NO =@GROUP_NO";
                string sql2 = @"DELETE FROM tms_data_detail WHERE GROUP_NO =@GROUP_NO AND BILL_NO =@BILL_NO;";
                IMapper map = DatabaseInstance.Instance();
                foreach (LoadingDetailEntity item in list)
                {
                    result += map.Execute(sql, new { LoadingNo = item.LoadingNO, BillNo = item.BillNO });
                    if (flag == 1)
                    {
                        //matketID = map.ExecuteScalar<string>(sql1, new{ GROUP_NO =item.LoadingNO});
                        result1 = map.Execute(sql2, new { GROUP_NO = item.LoadingNO, BillNo = item.BillNO });
                        RequestPackage request = new RequestPackage("removeOrderid.php");
                        request.Method = EHttpMethod.Get.ToString();
                        request.Params.Add("orderid", item.BillNO);

                        ResponsePackage response = _httpContext.Request(request);

                        if (response.Result == EResponseResult.成功)
                        {
                            string jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                            if (jsonData == "NULL")
                            {
                                //MsgBox.Warn("网络环境异常，请检查网络！");
                            }
                        }
                    }


                }
            }
            return result;
        }
        //public static int InsertDetails(List<LoadingDetailEntity> list, HttpContext _httpContext, int flag)
        //{
        //    int result = 0;
        //    string sql = "INSERT INTO WM_LOADING_DETAIL(VH_TRAIN_NO, BILL_NO, IN_VH_SORT, UPDATE_DATE) " +
        //        "VALUES(@LoadingNO, @BillNo, @InVehicleSort, @UpdateDate)";
        //    IMapper map = DatabaseInstance.Instance();
        //    IDbTransaction trans = map.BeginTransaction();
        //    try
        //    {
        //        foreach (LoadingDetailEntity detail in list)
        //        {
        //            if (ExistDetail(detail.BillNO))
        //                continue;
        //            result += map.Execute(sql, new
        //            {
        //                LoadingNO = detail.LoadingNO,
        //                BillNo = detail.BillNO,
        //                InVehicleSort = detail.InVehicleSort,
        //                UpdateDate = detail.UpdateDate
        //            }, trans);
        //            // 更新WM_SO_HEADER表中的SHIP_NO字段
        //            map.Execute(
        //                "UPDATE WM_SO_HEADER H SET H.SHIP_NO = @VehicleID WHERE H.BILL_NO = @BillNO",
        //                new { VehicleID = detail.VehicleID, BillNO = detail.BillNO });
        //            if (flag == 1)
        //            {
        //                RequestPackage request = new RequestPackage("AddOrders.php");
        //                request.Method = EHttpMethod.Get.ToString();
        //                request.Params.Add("carid", detail.LoadingNO);
        //                request.Params.Add("orderno", detail.BillNO);

        //                ResponsePackage response = _httpContext.Request(request);

        //                if (response.Result == EResponseResult.成功)
        //                {
        //                    string jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
        //                    if (jsonData == "NULL")
        //                    {

        //                    }
        //                }
        //                else
        //                {
        //                    MsgBox.Warn("回传添加的订单数据失败！");
        //                }
        //            }
        //        }
        //        trans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    return result;
        //}

        public static int InsertDetails(List<LoadingDetailEntity> list, HttpContext _httpContext, int flag)
        {
            int result = 0;
            string sql = "INSERT INTO WM_LOADING_DETAIL(VH_TRAIN_NO, BILL_NO, IN_VH_SORT, UPDATE_DATE) " +
                "VALUES(@LoadingNO, @BillNo, @InVehicleSort, @UpdateDate)";
            IMapper map = DatabaseInstance.Instance();

            IDbTransaction trans = map.BeginTransaction();
            try
            {
                foreach (LoadingDetailEntity detail in list)
                {
                    if (ExistDetail(detail.BillNO))
                        continue;
                    result += map.Execute(sql, new
                    {
                        LoadingNO = detail.LoadingNO,
                        BillNo = detail.BillNO,
                        InVehicleSort = detail.InVehicleSort,
                        UpdateDate = detail.UpdateDate
                    }, trans);
                    // 更新WM_SO_HEADER表中的SHIP_NO字段
                    map.Execute(
                        "UPDATE WM_SO_HEADER H SET H.SHIP_NO = @VehicleID WHERE H.BILL_NO = @BillNO",
                        new { VehicleID = detail.VehicleID, BillNO = detail.BillNO });
                    if (flag == 1)
                    {
                        RequestPackage request = new RequestPackage("AddOrders.php");
                        request.Method = EHttpMethod.Get.ToString();
                        request.Params.Add("carid", detail.LoadingNO);
                        request.Params.Add("orderno", detail.BillNO);

                        ResponsePackage response = _httpContext.Request(request);

                        if (response.Result == EResponseResult.成功)
                        {
                            string jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                            if (jsonData == "NULL")
                            {

                            }
                        }
                        else
                        {
                            MsgBox.Warn("回传添加的订单数据失败！");
                        }
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;
        }
        public static bool ExistDetail(string billNo)
        {
            string sql = @"SELECT COUNT(1) FROM WM_LOADING_DETAIL WHERE BILL_NO = @BillNO AND FLAG = 0 ";

            IMapper map = DatabaseInstance.Instance();

            object obj = map.ExecuteScalar<object>(sql);
            return obj != null && ConvertUtil.ToInt(obj) > 0;
        }
        /// <summary>
        /// 获取指定装车编号中装车顺序最大的值
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public static int GetMaxInVehicleSort(string loadingNo)
        {
            string sql = "SELECT MAX(IN_VH_SORT) FROM WM_LOADING_DETAIL WHERE VH_TRAIN_NO = @LoadingNo";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<int>(sql, new { LoadingNo = loadingNo });
        }

        public static DataTable GetLoadingReport(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            string sql = @"SELECT H.VH_TRAIN_NO, D.BILL_NO, V.VH_NO, D.IN_VH_SORT, 
                ROUND(SUM(SD.PICK_QTY), 0) WHOLE_COUNT, F_GET_BULK_PIECES(SH.BILL_ID) BULK_COUNT, 
                D.UPDATE_DATE 
                FROM WM_LOADING_HEADER H 
                LEFT JOIN WM_LOADING_DETAIL D ON H.VH_TRAIN_NO = D.VH_TRAIN_NO 
                LEFT JOIN WM_LOADING_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO AND D.VH_TRAIN_NO = U.VH_TRAIN_NO 
                LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID 
                LEFT JOIN WM_SO_HEADER SH ON SH.BILL_NO = D.BILL_NO 
                LEFT JOIN WM_SO_DETAIL SD ON SD.BILL_ID = SH.BILL_ID 
                WHERE U.USER_CODE = @UserCode AND (D.UPDATE_DATE BETWEEN @DateBegin AND @DateEnd) AND U.ATTRI1 = '145'
                GROUP BY D.BILL_NO 
                ORDER BY H.VH_TRAIN_NO DESC, D.IN_VH_SORT";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { UserCode = userCode, DateBegin = dateBegin, DateEnd = dateEnd });
        }

        public static DataTable GetLoadingReport2(DateTime dateBegin, DateTime dateEnd)
        {
            string sql = @"SELECT A.USER_CODE 人员编号, A.USER_NAME 人员姓名, WBC.ITEM_DESC 所属,
  ROUND(SUM(A.CASE_QTY) / B.U_COUNT, 2) 整货件数, ROUND(SUM(A.QTY) / B.U_COUNT, 2) 散货件数 
  FROM (SELECT U.USER_CODE, U.USER_NAME, U.USER_TYPE, A.VH_TRAIN_NO, A.VH_NO,COUNT(A.BILL_NO) BILL_COUNT, 
            SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY 
          FROM USERS U 
          INNER JOIN USER_ROLE UR ON UR.USER_CODE = U.USER_CODE 
          LEFT JOIN ROLES R ON R.ROLE_ID = UR.ROLE_ID 
          LEFT JOIN (SELECT H.VH_TRAIN_NO, V.VH_NO, D.BILL_NO, U.USER_CODE, U.USER_NAME, 
                        ROUND(SUM(SD.PICK_QTY), 0) QTY1, F_GET_BULK_PIECES(SD.BILL_ID) QTY2 
                      FROM WM_LOADING_HEADER H 
                      LEFT JOIN WM_LOADING_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                      LEFT JOIN WM_LOADING_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                      LEFT JOIN WM_SO_HEADER SH ON SH.BILL_NO = D.BILL_NO 
                      LEFT JOIN WM_SO_DETAIL SD ON SD.BILL_ID = SH.BILL_ID 
                      LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID 
                      WHERE D.UPDATE_DATE BETWEEN @DateBegin AND @DateEnd AND U.ATTRI1 = '145'
                      GROUP BY SD.BILL_ID, U.USER_CODE) A ON A.USER_CODE = U.USER_CODE 
          WHERE R.ROLE_NAME = '装车员' 
          GROUP BY U.USER_CODE, A.VH_TRAIN_NO) A 
  LEFT JOIN (SELECT H.VH_TRAIN_NO, COUNT(1) U_COUNT FROM WM_LOADING_HEADER H 
              LEFT JOIN WM_LOADING_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
              WHERE ATTRI1 = '145'
              GROUP BY H.VH_TRAIN_NO) B ON B.VH_TRAIN_NO = A.VH_TRAIN_NO 
  LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = A.USER_TYPE
  GROUP BY A.USER_CODE ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { DateBegin = dateBegin, DateEnd = dateEnd });
        }

        public static DataTable GetLoadingRecords(string billNo)
        {
            string sql = string.Format(
                @"SELECT D.VH_TRAIN_NO 装车编号, V.VH_NO 车牌号, D.BILL_NO 订单编号, D.IN_VH_SORT 装车顺序, 
  H.USER_NAME 创建人员, GROUP_CONCAT(U.USER_NAME) 装车人员, D.UPDATE_DATE 装车时间
  FROM wm_loading_detail D
  LEFT JOIN WM_LOADING_HEADER H ON H.VH_TRAIN_NO = D.VH_TRAIN_NO
  LEFT JOIN WM_LOADING_USERS U ON U.VH_TRAIN_NO = D.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO
  LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID
  WHERE D.BILL_NO LIKE '%{0}%' AND U.ATTRI1 = '145'
  GROUP BY D.VH_TRAIN_NO, D.BILL_NO
  ORDER BY D.UPDATE_DATE DESC;", billNo);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 获取一个未选择人员的装车信息
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public static LoadingHeaderEntity GetLoadingInfoByNonChooseUser(int vehicleID)
        {
            string sql = @"SELECT H.VH_TRAIN_NO FROM WM_LOADING_HEADER H
  LEFT JOIN WM_LOADING_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO
  INNER JOIN WM_SO_HEADER SH ON SH.BILL_NO = D.BILL_NO AND (SH.BILL_STATE < 68 OR SH.BILL_STATE = 691)
  WHERE H.VH_TRAIN_NO NOT in (SELECT U.VH_TRAIN_NO FROM WM_LOADING_USERS U) AND H.VH_ID = @VehicleID
  GROUP BY H.VH_TRAIN_NO LIMIT 1";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<LoadingHeaderEntity>(sql, new { VehicleID = vehicleID });
        }
        /// <summary>
        /// 获取一个未选择人员的装车信息
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public static List<LoadingUserEntity> GetLoadingInfoByNonChooseUser(string loadingNo)
        {
            string sql = @"SELECT * FROM WM_LOADING_USERS WHERE VH_TRAIN_NO = @LoadingNo";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<LoadingUserEntity>(sql, new { LoadingNo = loadingNo });
        }
        /// <summary>
        /// 完成装车
        /// </summary>
        /// <param name="loadingNo">装车编号</param>
        /// <returns></returns>
        public static int FinishLoadingInfo(string loadingNo)
        {
            string sql = @"UPDATE WM_LOADING_HEADER SET FINISH_DATE = NOW() WHERE VH_TRAIN_NO = @VehicleNo ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { VehicleNo = loadingNo });
        }

        public static int ChangeVehicle(LoadingHeaderEntity header)
        {
            string sql = @"UPDATE WM_LOADING_HEADER SET VH_ID = @VehicleID WHERE VH_TRAIN_NO = @LoadingNo; 
UPDATE WM_SO_HEADER H
  INNER JOIN WM_LOADING_DETAIL D ON D.BILL_NO = H.BILL_NO AND D.VH_TRAIN_NO = @LoadingNo 
  SET H.SHIP_NO = @VehicleID;";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { VehicleID = header.VehicleID, LoadingNo = header.LoadingNO });
        }

        public static int DeleteUser(string loadingNo, string userCode)
        {
            string sql = @"DELETE FROM WM_LOADING_USERS WHERE VH_TRAIN_NO = @LoadingNo AND USER_CODE = @UserCode AND ATTRI1 = 145 ";

            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { LoadingNo = loadingNo, UserCode = userCode });
        }

        //        public static int DeleteDetails(List<LoadingDetailEntity> list)
        //        {
        //            int result = 0;
        //            if (list != null && list.Count > 0)
        //            {
        //                string sql = @"DELETE FROM WM_LOADING_DETAIL WHERE VH_TRAIN_NO = @LoadingNo AND BILL_NO = @BillNo;
        //UPDATE WM_SO_HEADER SET SHIP_NO = NULL WHERE BILL_NO = @BillNo;";
        //                IMapper map = DatabaseInstance.Instance();
        //                foreach (LoadingDetailEntity item in list)
        //                {
        //                    result += map.Execute(sql, new { LoadingNo = item.LoadingNO, BillNo = item.BillNO });
        //                }
        //            }
        //            return result;
        //        }

        public static List<LoadingHeaderEntity> GetLoadingHeaderByVehicleID(int vehicleID)
        {
            string sql = string.Format(
@"SELECT * FROM WM_LOADING_HEADER H 
  INNER JOIN WM_LOADING_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO AND D.FLAG = 0
  INNER JOIN WM_SO_HEADER WSH ON WSH.BILL_NO = D.BILL_NO AND WSH.BILL_STATE NOT IN ('68', '692', '693')
  WHERE H.VH_ID = {0}
  GROUP BY H.VH_TRAIN_NO ", vehicleID);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<LoadingHeaderEntity>(sql);
        }
        /// <summary>
        /// 根据订单ID查看关联笼车是否都已接收
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public static bool IsReceiveContainer(int billID)
        {
            string sql = @"SELECT COUNT(1) FROM WM_CONTAINER_RECORD R
  INNER JOIN WM_CONTAINER_STATE S ON R.LC_CODE = S.CT_CODE AND S.CT_STATE < '891'
  WHERE R.BILL_HEAD_ID = @BillID ";

            IMapper map = DatabaseInstance.Instance();
            object obj = map.ExecuteScalar<object>(sql, new { BillID = billID });
            return obj == null ? true : ConvertUtil.ToInt(obj) == 0;
        }

        public static List<SOFindGoodsDetail> GetFindGoodsDetail(string vhCode)
        {
            string sql = @"SELECT WSH.BILL_ID, D.BILL_NO, D.IN_VH_SORT 'LOADING_SORT', 
  F_CALC_PIECES_BY_PICK(WSH.BILL_ID, 1) 'ZHENG_NUM', WSH.DELAYMARK,  
  F_CALC_PIECES_BY_PICK(WSH.BILL_ID, 2) 'SAN_NUM', C.C_NAME, C.ADDRESS
  FROM WM_LOADING_DETAIL D
  LEFT JOIN WM_SO_HEADER WSH ON WSH.BILL_NO = D.BILL_NO
  LEFT JOIN CUSTOMERS C ON C.C_CODE = WSH.C_CODE
  WHERE D.VH_TRAIN_NO = @LoadingNo 
  ORDER BY D.IN_VH_SORT asc";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOFindGoodsDetail>(sql, new { LoadingNo = vhCode });
        }

        public static List<SOFindGoodsDetail> GetFindGoodsDetail()
        {
            string sql = @"SELECT WSH.BILL_ID, WSH.BILL_NO,  
          F_CALC_PIECES_BY_PICK(WSH.BILL_ID, 1) 'ZHENG_NUM', WSH.DELAYMARK,  
          F_CALC_PIECES_BY_PICK(WSH.BILL_ID, 2) 'SAN_NUM', C.C_NAME, C.ADDRESS
          FROM WM_SO_HEADER WSH 
          LEFT JOIN CUSTOMERS C ON C.C_CODE = WSH.C_CODE
          WHERE WSH.BILL_STATE >= '65' AND WSH.BILL_STATE < '68' 
      ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOFindGoodsDetail>(sql);
        }

        public static DataTable GetTuoPanInfo(int billID)
        {
            string sql = @"  SELECT A.CTL_ID, A.CT_CODE 
                      FROM WM_SO_PICK_RECORD R
                      LEFT JOIN (SELECT WCLR.BILL_ID, WCLR.CTL_ID, WCLR.CT_CODE, WCLR.CTL_TYPE
                                  FROM WM_CONTAINER_LOCATION_RECORDS WCLR
                                  WHERE WCLR.BILL_ID = @BillID
                                  ORDER BY WCLR.ASSOCIATED_TIME DESC) A ON A.CT_CODE = R.CT_CODE
                        AND A.BILL_ID = R.BILL_ID AND A.CTL_TYPE = 'L95'
                      JOIN wm_container wc ON R.CT_CODE =wc.CT_CODE AND wc.CT_TYPE ='50'
                      WHERE R.BILL_ID = @BillID
                      GROUP BY R.BILL_ID, R.CT_CODE";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        public static DataTable GetWuLiuXiangInfo(int billID, string wareHouseType)
        {
            string sql = "";
            if (wareHouseType == "混合仓")
            {
                sql = @"SELECT  ws.CT_CODE ,ws.LC_CODE,wscm.LC_CODE CTL_ID FROM wm_container_state ws
                          INNER JOIN wm_container wc ON ws.CT_CODE =wc.CT_CODE AND wc.CT_TYPE ='51'
                          LEFT JOIN wm_so_container_move wscm ON ws.CT_CODE =wscm.CT_CODE
                          WHERE ws.BILL_HEAD_ID=@BillID
                          GROUP BY wc.CT_CODE";
            }
            else
            {
                sql = @"SELECT A.CTL_ID, R.LC_CODE, R.CT_CODE 
                      FROM WM_CONTAINER_RECORD R 
                      LEFT JOIN (SELECT R.CTL_ID, R.CT_CODE, R.CTL_TYPE
                                  FROM WM_CONTAINER_LOCATION_RECORDS R
                                  INNER JOIN WM_CONTAINER C ON C.CT_CODE = R.CT_CODE AND C.CT_TYPE = '52'
                                  WHERE R.BILL_ID = @BillID 
                                  ORDER BY R.ASSOCIATED_TIME DESC) A ON A.CTL_TYPE = 'L96' AND A.CT_CODE = R.LC_CODE
                      WHERE R.BILL_HEAD_ID = @BillID 
                      GROUP BY R.CT_CODE;";
            }

            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        #endregion

        #region 车次信息
        public static DataTable GetLoadingTrainRecords(string billNo)
        {
            string sql = string.Format(
                @"SELECT D.VH_TRAIN_NO 车次编号, H.VH_NO 车牌号, D.BILL_NO 订单编号,
  H.USER_NAME 创建人员, GROUP_CONCAT(U.USER_NAME) 配送人员, D.UPDATE_DATE 配送时间
  FROM WM_VEHICLE_TRAIN_DETAIL D
  LEFT JOIN WM_VEHICLE_TRAIN_HEADER H ON H.VH_TRAIN_NO = D.VH_TRAIN_NO
  LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON U.VH_TRAIN_NO = D.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO
  WHERE D.BILL_NO LIKE '%{0}%'
  GROUP BY D.VH_TRAIN_NO, D.BILL_NO
  ORDER BY D.UPDATE_DATE DESC;", billNo);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 创建装车信息表头
        /// </summary>
        /// <returns>车次信息编号</returns>
        public static string CreateTrainHeader(string whCode, string vhNo, string vehicleName,
            string userName, string userPhone, string billsStr, EWarehouseType warehouseType)
        {
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            string trainNo = null;
            try
            {
                trainNo = String.Format("C{0}{1}{2}", whCode, DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(1000, 10000));
                //int? isCase2 = 0;//散货件数
                //int? isCase1 = 0;//整货件数
                //GetIscaseQty(billsStr, warehouseType, out isCase1, out isCase2);
                string sql = @"INSERT INTO wm_vehicle_train_header(WH_CODE, VH_TRAIN_NO, VH_NO, RANDOM_CODE, 
VEHICLE_NAME, USER_PHONE, BULK_CARGO_QTY, WHOLE_GOODS, STATE, SYNC_STATE, USER_NAME, UPDATE_DATE) VALUES(
@WhCode, @TrainNo, @VhNo, @RandomCode, @VehicleName, @UserPhone, @BulkQty, @WholeGoods, 1, NULL, @UserName, NOW());SELECT @@identity;";
                object result = map.ExecuteScalar<object>(sql, new
                {
                    WhCode = whCode,
                    TrainNo = trainNo,
                    VhNo = vhNo,
                    RandomCode = new Random().Next(100000, 1000000),
                    VehicleName = vehicleName,
                    UserPhone = userPhone,
                    BulkQty = 0,//isCase2,
                    WholeGoods = 0,//isCase1,
                    UserName = userName
                });
                if (result == null)
                    throw new Exception();
                string randomCode = string.Format("{0}{1}{2}", whCode, ConvertUtil.ToInt(result).ToString("0000000"), new Random().Next(1000, 10000));
                if (UpdateTrainRandomCode(trainNo, randomCode) > 0)
                    trans.Commit();
                else
                    throw new Exception();
            }
            catch
            {
                trainNo = null;
                trans.Rollback();
            }
            return trainNo;
        }
        public static string CreateTrain(string whCode, string creator, string vhNo, string vehicleName,
            string userPhone, List<SOHeaderEntity> list, List<UserEntity> listUsers,
            EWarehouseType warehouseType)
        {
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            string trainNo = string.Empty;
            try
            {
                trainNo = String.Format(
                    "C{0}{1}{2}",
                    whCode,
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    new Random().Next(1000, 10000));            // 车次编号
                int ret = 0;
                //插入订单信息
                foreach (SOHeaderEntity entity in list)
                {
                    //插入明细表
                    ret = InsertTrainDetail(trainNo, entity);
                    if (ret <= 0)
                        throw new Exception("创建车次明细时失败！");
                }
                //关联配送司机和助理
                ret = InsertTrainUsers(trainNo, listUsers);
                if (ret <= 0)
                {
                    throw new Exception("关联配送司机与助理时失败！");
                }
                // 获取订单整散数量
                int? isCase1, isCase2;
                GetIscaseQty(StringUtil.JoinBySign<SOHeaderEntity>(list, "BillID"), warehouseType, out isCase1, out isCase2);
                isCase1 = isCase1 == null || isCase1 == int.MinValue ? 0 : isCase1;
                isCase2 = isCase2 == null || isCase2 == int.MinValue ? 0 : isCase2;
                // 创建车次表头
                string sql = @"INSERT INTO wm_vehicle_train_header(WH_CODE, VH_TRAIN_NO, VH_NO, RANDOM_CODE, 
VEHICLE_NAME, USER_PHONE, BULK_CARGO_QTY, WHOLE_GOODS, STATE, SYNC_STATE, USER_NAME, UPDATE_DATE) VALUES(
@WhCode, @TrainNo, @VhNo, @RandomCode, @VehicleName, @UserPhone, @BulkQty, @WholeGoods, 1, 1, @UserName, NOW());SELECT @@identity;";
                object result = map.ExecuteScalar<object>(sql, new
                {
                    WhCode = whCode,
                    TrainNo = trainNo,
                    VhNo = vhNo,
                    RandomCode = new Random().Next(100000, 1000000),
                    VehicleName = vehicleName,
                    UserPhone = userPhone,
                    BulkQty = isCase2,
                    WholeGoods = isCase1,
                    UserName = creator
                });
                if (result == null)
                    throw new Exception("创建车次表头时失败！");
                string randomCode = string.Format("{0}{1}{2}",
                    whCode,
                    ConvertUtil.ToInt(result).ToString("0000000"),
                    new Random().Next(1000, 10000));
                if (UpdateTrainRandomCode(trainNo, randomCode) > 0)
                    trans.Commit();
                else
                    throw new Exception("更新车次表头随机码时失败！");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return trainNo;
        }
        public static string CreateTrain(string whCode, string creator, string vhNo, string vehicleName,
            string userPhone, List<SOHeaderEntity> list, List<UserEntity> listUsers,
            EWarehouseType warehouseType, string loadingNo)
        {
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            string trainNo = string.Empty;
            try
            {
                trainNo = String.Format(
                    "C{0}{1}{2}",
                    whCode,
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    new Random().Next(1000, 10000));            // 车次编号
                int ret = 0;
                //插入订单信息
                foreach (SOHeaderEntity entity in list)
                {
                    if (entity.BillType == "120")
                    {
                        //插入明细表
                        ret = InsertTrainDetail(trainNo, entity);
                        if (ret <= 0)
                            throw new Exception("创建车次明细时失败！");
                    }
                }
                //关联配送司机和助理
                ret = InsertTrainUsers(trainNo, listUsers);
                if (ret <= 0)
                {
                    throw new Exception("关联配送司机与助理时失败！");
                }
                // 获取订单整散数量
                int? isCase1, isCase2;
                GetIscaseQty(StringUtil.JoinBySign<SOHeaderEntity>(list, "BillID"), warehouseType, out isCase1, out isCase2);
                isCase1 = isCase1 == null || isCase1 == int.MinValue ? 0 : isCase1;
                isCase2 = isCase2 == null || isCase2 == int.MinValue ? 0 : isCase2;
                // 创建车次表头
                string sql = @"INSERT INTO wm_vehicle_train_header(WH_CODE, VH_TRAIN_NO, VH_NO, RANDOM_CODE, 
VEHICLE_NAME, USER_PHONE, BULK_CARGO_QTY, WHOLE_GOODS, STATE, SYNC_STATE, USER_NAME, UPDATE_DATE, LOADING_NO) VALUES(
@WhCode, @TrainNo, @VhNo, @RandomCode, @VehicleName, @UserPhone, @BulkQty, @WholeGoods, 1, 1, @UserName, NOW(), @LoadingNo);SELECT @@identity;";
                object result = map.ExecuteScalar<object>(sql, new
                {
                    WhCode = whCode,
                    TrainNo = trainNo,
                    VhNo = vhNo,
                    RandomCode = new Random().Next(100000, 1000000),
                    VehicleName = vehicleName,
                    UserPhone = userPhone,
                    BulkQty = isCase2,
                    WholeGoods = isCase1,
                    UserName = creator,
                    LoadingNo = loadingNo
                });
                if (result == null)
                    throw new Exception("创建车次表头时失败！");
                string randomCode = string.Format("{0}{1}{2}",
                    whCode,
                    ConvertUtil.ToInt(result).ToString("0000000"),
                    new Random().Next(1000, 10000));
                if (UpdateTrainRandomCode(trainNo, randomCode) > 0)
                    trans.Commit();
                else
                    throw new Exception("更新车次表头随机码时失败！");
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return trainNo;
        }
        public static int UpdateTrainRandomCode(string trainNo, string randomCode)
        {
            string sql = @"UPDATE WM_VEHICLE_TRAIN_HEADER SET RANDOM_CODE = @RandomCode WHERE VH_TRAIN_NO = @TrainNo";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { RandomCode = randomCode, TrainNo = trainNo });
        }
        public static int UpdateTrainInfo(string trainNo, string billIds, EWarehouseType warehouseType)
        {
            int? isCase1, isCase2;
            GetIscaseQty(billIds, warehouseType, out isCase1, out isCase2);
            isCase1 = isCase1 == null || isCase1 == int.MinValue ? 0 : isCase1;
            isCase2 = isCase2 == null || isCase2 == int.MinValue ? 0 : isCase2;
            string sql = "UPDATE WM_VEHICLE_TRAIN_HEADER SET BULK_CARGO_QTY = @BulkQty, WHOLE_GOODS = @WholeGoods, SYNC_STATE=1 WHERE VH_TRAIN_NO = @TrainNo";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new
            {
                BulkQty = isCase2,
                WholeGoods = isCase1,
                TrainNo = trainNo
            });
        }
        public static int InsertTrainUsers(string trainNo, List<UserEntity> userList, IDbTransaction trans = null)
        {
            IMapper map = DatabaseInstance.Instance();
            int result = 0;
            try
            {
                string sql = @"INSERT INTO wm_vehicle_train_users(VH_TRAIN_NO ,USER_NAME ,USER_CODE ,ROLE_ID,UPDATE_DATE) 
VALUES(@TrainNo, @UserName, @UserCode,@RoleId, NOW())";
                foreach (UserEntity user in userList)
                {
                    result += map.Execute(sql, new
                    {
                        TrainNo = trainNo,
                        UserName = user.UserName,
                        UserCode = user.UserCode,
                        RoleId = user.ROLE_ID
                    });
                }
                if (result != userList.Count)
                    result = -1;
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        public static int InsertTrainDetail(string trainNo, SOHeaderEntity soHeader)
        {
            string sql = @"INSERT INTO wm_vehicle_train_detail(VH_TRAIN_NO ,BILL_NO ,ORIGINAL_BILL_NO ,UPDATE_DATE)
VALUES (@TrainNO, @BillNo, @OriginalBillNo, NOW());UPDATE WM_SO_HEADER SET BILL_STATE = '68' WHERE BILL_NO = @BillNo";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new
            {
                TrainNo = trainNo,
                BillNo = soHeader.BillNO,
                OriginalBillNo = soHeader.OriginalBillNo
            });
        }
        /// <summary>
        /// 根据车次编号删除表头
        /// </summary>
        /// <param name="trainSO"></param>
        /// <returns></returns>
        public static int DeleteTrainHeader(string trainNo)
        {
            string sql = "DELETE FROM WM_VEHICLE_TRAIN_HEADER WHERE VH_TRAIN_NO = @TrainNo ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { TrainNo = trainNo });
        }
        public static int DeleteTrainAll(string trainNo)
        {
            string sql = @"DELETE FROM WM_VEHICLE_TRAIN_HEADER WHERE VH_TRAIN_NO = @TrainNo;
DELETE FROM WM_VEHICLE_TRAIN_DETAIL WHERE VH_TRAIN_NO = @TrainNo;
DELETE FROM WM_VEHICLE_TRAIN_USERS WHERE VH_TRAIN_NO = @TrainNo;";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { TrainNo = trainNo });
        }
        public static void GetIscaseQty(string strBuilder, EWarehouseType warehouseType, out int? isCase1, out int? isCase2)
        {
            isCase1 = isCase2 = 0;
            string str = strBuilder.ToString();
            if (str.Length == 0)
            {
                isCase1 = isCase2 = 0;
            }
            IMapper mapper = DatabaseInstance.Instance();
            //string sqlIsCase1 = String.Format("SELECT ROUND(SUM(IFNULL(A.PICK_QTY, 0)),0) FROM wm_so_detail A  WHERE A.IS_CASE = 1 AND  A.BILL_ID IN ({0}) ;", str);
            //Update By 万伟超
            string sqlIsCase1 = string.Format(@"
                SELECT SUM(IFNULL(A.QTY, 0)) QTY 
  FROM (SELECT D.BILL_ID, IFNULL(ROUND(SUM(p.QTY / WUS.QTY),0), 0) QTY FROM WM_SO_PICK P
          INNER JOIN WM_SO_DETAIL D ON D.ID = P.DETAIL_ID AND D.IS_CASE = 2
          INNER JOIN WM_UM_SKU WUS ON WUS.SKU_CODE = D.SKU_CODE AND WUS.SKU_LEVEL = 3
          WHERE P.IS_CASE = 1 AND P.BILL_ID IN ({0})
        UNION ALL
        SELECT D.BILL_ID, IFNULL(ROUND(SUM(p.QTY / S.QTY),0), 0) QTY FROM wm_so_pick p
          INNER JOIN WM_SO_DETAIL D ON D.ID = P.DETAIL_ID AND D.IS_CASE = 1
          INNER JOIN WM_UM_SKU S ON S.SKU_CODE = D.SKU_CODE AND S.UM_CODE = D.UM_CODE
          WHERE P.BILL_ID IN ({0})) A 
                 ", str);
            string sqlIsCase2 = string.Empty;
            if (warehouseType == EWarehouseType.混合仓)
                sqlIsCase2 = String.Format("SELECT COUNT(DISTINCT wc.CT_CODE) FROM wm_so_pick_record wspr INNER JOIN wm_container wc ON wc.CT_CODE = wspr.CT_CODE AND wc.CT_TYPE = '51' WHERE wspr.BILL_ID IN ({0});", str);
            else
                sqlIsCase2 = String.Format("SELECT COUNT(1) FROM WM_CONTAINER_RECORD A  WHERE A.BILL_HEAD_ID IN ({0});", str);
            object objIsCase1 = mapper.ExecuteScalar<object>(sqlIsCase1);
            object objIsCase2 = mapper.ExecuteScalar<object>(sqlIsCase2);
            isCase1 = ConvertUtil.ToInt(objIsCase1);
            isCase2 = ConvertUtil.ToInt(objIsCase2);
        }
        public static int ConfirmTrain(string trainNo, int ctQty)
        {
            string sql = @"UPDATE WM_VEHICLE_TRAIN_HEADER SET CONFIRM_DATE = NOW(), CT_QTY = @CtQty WHERE VH_TRAIN_NO = @TrainNo ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { TrainNo = trainNo, CtQty = ctQty });
        }

        public static int GetBulkCargoQty(string trainNo)
        {
            string sql = string.Format("SELECT WVTH.BULK_CARGO_QTY FROM WM_VEHICLE_TRAIN_HEADER WVTH WHERE WVTH.VH_TRAIN_NO = '{0}' ", trainNo);
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<int>(sql);
        }
        #endregion

        #region 删除没有明显的装车表头
        public static int DeleteLoading(string loadingNo)
        {

            IMapper map = DatabaseInstance.Instance();

            string sqlSel = string.Format("SELECT COUNT(1) FROM wm_loading_detail  WHERE VH_TRAIN_NO = {0}", loadingNo);
            string sqlDel = string.Format("DELETE FROM WM_LOADING_HEADER WHERE VH_TRAIN_NO = {0}", loadingNo);

            int isDetail = ConvertUtil.ToInt(map.ExecuteScalar<long>(sqlSel));

            if (isDetail > 0)
            {
                return 1;
            }
            else
            {
                int result = map.Execute(sqlDel);
                if (result > 0)
                {
                    return 2;
                }
            }

            return 0;
        }
        #endregion
    }
}
