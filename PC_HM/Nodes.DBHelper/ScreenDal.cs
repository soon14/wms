using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using System.Data;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ScreenDal
    {

        //public DataTable LoadingQuery()
        //{
        //    //装车任务列表
        //    string sql = "SELECT E.VH_NO, COUNT(DISTINCT B.BILL_NO) BILL_COUNT, (SUM(D.PICK_QTY)+ F_GET_BULK_PIECES(C.BILL_ID)) A_COUNT, " +
        //                " SUM(D.PICK_QTY) Z_COUNT,F_GET_BULK_PIECES(C.BILL_ID) S_COUNT,GROUP_CONCAT(DISTINCT G.USER_NAME) LOAD_NAME " +
        //                " FROM WM_LOADING_HEADER A    " +
        //                " LEFT JOIN WM_LOADING_DETAIL B ON A.VH_TRAIN_NO = B.VH_TRAIN_NO   " +
        //                " LEFT JOIN WM_LOADING_USERS G ON A.VH_TRAIN_NO = G.VH_TRAIN_NO AND G.USER_CODE IS NOT NULL" +
        //                " LEFT JOIN WM_SO_HEADER C ON B.BILL_NO = C.BILL_NO   " +
        //                " LEFT JOIN WM_SO_DETAIL D ON C.BILL_ID = D.BILL_ID AND D.IS_CASE = 1  " +
        //                " LEFT JOIN WM_VEHICLE E ON A.VH_ID = E.ID  " +
        //                " WHERE  C.BILL_STATE = 66   GROUP BY A.VH_ID limit 1; ";

        //    IMapper map = DatabaseInstance.Instance();
        //    return map.LoadTable(sql);
        //}

        //public DataTable PickingQuery()
        //{
        //    string sql = " SELECT C.USER_NAME , D.ITEM_DESC, COUNT(A.BILL_ID) billID, IFNULL(B.QTY/F.QTY,0) PickQty  " +
        //                " FROM tasks A INNER JOIN wm_so_pick B ON B.BILL_ID = A.BILL_ID  " +
        //                " INNER JOIN wm_so_detail E ON B.DETAIL_ID = E.ID  " +
        //                " INNER JOIN wm_um_sku F ON E.SKU_CODE = F.SKU_CODE AND E.UM_CODE = F.UM_CODE  " +
        //                " INNER JOIN users C ON A.USER_CODE = C.USER_CODE INNER JOIN wm_base_code D ON  " +
        //                " A.TASK_TYPE = D.ITEM_VALUE  " +
        //                " WHERE A.TASK_TYPE = '143' GROUP BY A.USER_CODE  ";
        //    IMapper map = DatabaseInstance.Instance();
        //    return map.LoadDataSet(sql);
        //}
        /// <summary>
        /// 获取绩效
        /// </summary>
        /// <param name="flag">1-当天；2-当月</param>
        /// <returns></returns>
        //public List<ScreenAchievementEntity> SummaryByPersonnel(int flag)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    DynamicParameters parms = new DynamicParameters();
        //    parms.Add("V_FLAG", flag);
        //    return map.Query<ScreenAchievementEntity>("P_SCREEN_ACHIEVEMENT_DISPLAY", parms, false, CommandType.StoredProcedure);
        //}
        public List<ScreenAchievementEntity> SummaryByPersonnel(int flag)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_FLAG", flag);
            DataTable dt = map.LoadTable("P_SCREEN_ACHIEVEMENT_DISPLAY", parms, CommandType.StoredProcedure);
            List<ScreenAchievementEntity> list = new List<ScreenAchievementEntity>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ScreenAchievementEntity()
                    {
                        userName = row["人员姓名"].ToString(),
                        userAttribute = row["所属"].ToString(),
                        normalPick = decimal.Parse(row["拣货量"].ToString()),
                        mostPick = decimal.Parse(row["批市拣货量"].ToString()),
                        dispatching220 = decimal.Parse(row["220_配送量"].ToString()),
                        dispatching221 = decimal.Parse(row["221_配送量"].ToString()),
                        //UserRole = row["配送角色"].ToString(),
                        loading = decimal.Parse(row["装车量"].ToString()),
                        forklift = decimal.Parse(row["叉车量"].ToString()),
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 获取执行效率
        /// </summary>
        /// <param name="flag">1-当天；2-当月</param>
        /// <returns></returns>
        public List<EfficiencyEntity> SummaryByPersonnelEfficiency(int flag)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_FLAG", flag);
            return map.Query<EfficiencyEntity>("P_SCREEN_EFFICIENCY_DISPLAY", parms, false, CommandType.StoredProcedure);
        }

        public static void execute()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"DELIMITER $$
DROP PROCEDURE IF EXISTS P_SCREEN_ACHIEVEMENT_DISPLAY$$
CREATE DEFINER = 'nodes'@'%'
PROCEDURE P_SCREEN_ACHIEVEMENT_DISPLAY(
  IN V_FLAG INT)
BEGIN
  IF(V_FLAG = 1)THEN
  SELECT U.USER_NAME 人员姓名, WBC.ITEM_DESC 所属,  ROUND(IFNULL(A.QTY, 0), 0) 拣货量, ROUND(IFNULL(Z.QTY, 0), 0) 批市拣货量
  , ROUND(F.`220_配送量`, 2) '220_配送量', ROUND(F.`221_配送量`, 2) '221_配送量', ROUND((IFNULL(G.整货件数, 0)+IFNULL(G.散货件数, 0)), 2) 装车量
  , ROUND((IFNULL(B.QTY, 0)+ IFNULL(C.QTY, 0)), 0) 叉车量
   FROM USERS U
  LEFT JOIN (SELECT A.USER_CODE, COUNT(A.BILL_NO) B_COUNT, IFNULL(SUM(A.QTY), 0) QTY FROM (
      SELECT H.BILL_NO, R.USER_CODE, SUM(IFNULL(R.PICK_QTY / S.QTY, 0)) QTY FROM WM_SO_HEADER H 
      LEFT JOIN WM_SO_DETAIL D ON H.BILL_ID = D.BILL_ID
      LEFT JOIN WM_SO_PICK P ON P.BILL_ID = H.BILL_ID AND P.DETAIL_ID = D.ID 
      LEFT JOIN WM_SO_PICK_RECORD R ON R.BILL_ID = H.BILL_ID AND R.PICK_ID = P.ID 
      INNER JOIN WM_UM_SKU S ON S.SKU_CODE = D.SKU_CODE AND S.UM_CODE = D.UM_CODE
      WHERE R.USER_CODE IS NOT NULL AND DATE(R.PICK_DATE) = DATE(NOW())
      GROUP BY H.BILL_ID) A GROUP BY A.USER_CODE) A ON U.USER_CODE = A.USER_CODE
  LEFT JOIN (SELECT IFNULL(SUM(R.PICK_QTY / S.QTY), 0) QTY, R.USER_CODE FROM WM_SO_HEADER H 
      LEFT JOIN WM_SO_DETAIL D ON H.BILL_ID = D.BILL_ID
      LEFT JOIN WM_SO_PICK P ON P.BILL_ID = H.BILL_ID AND P.DETAIL_ID = D.ID 
      LEFT JOIN WM_SO_PICK_RECORD R ON R.BILL_ID = H.BILL_ID AND R.PICK_ID = P.ID 
      LEFT JOIN WM_UM_SKU S ON S.SKU_CODE = D.SKU_CODE AND S.UM_CODE = D.UM_CODE
      WHERE LENGTH(H.BILL_NO) = 21 AND (SUBSTRING(H.BILL_NO, 3, 1) = 3 OR SUBSTRING(H.BILL_NO, 3, 1) = 8) AND R.USER_CODE IS NOT NULL AND date(R.PICK_DATE) = date(NOW())
      GROUP BY H.BILL_ID) Z ON U.USER_CODE = Z.USER_CODE
  LEFT JOIN (SELECT B.USER_CODE, SUM(IFNULL(B.QTY, 0)) QTY FROM (
      SELECT COUNT(R.TO_LC_CODE) QTY, R.USER_CODE 
      FROM WM_TRANS_RECORD R 
      LEFT JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID 
      LEFT JOIN WM_UM U ON S.UM_CODE = U.UM_CODE 
      WHERE R.USER_CODE IS NOT NULL AND date(R.CREATE_DATE)= date(NOW()) 
      GROUP BY R.BILL_ID) B GROUP BY B.USER_CODE) B ON U.USER_CODE = B.USER_CODE
--   LEFT JOIN (SELECT COUNT(1) CNT, R.USER_CODE
--       FROM WM_TRANS_RECORD R 
--       LEFT JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID 
--       LEFT JOIN WM_UM U ON S.UM_CODE = U.UM_CODE 
--       WHERE R.USER_CODE IS NOT NULL AND (R.CREATE_DATE >= V_DATE_BEGIN AND R.CREATE_DATE <= V_DATE_END)
--       GROUP BY R.USER_CODE) Y ON Y.USER_CODE = U.USER_CODE
  LEFT JOIN (SELECT SUM(A.QTY) QTY,A.PUT_BY FROM ( SELECT COUNT(R.LC_CODE) QTY, R.PUT_BY
    FROM WM_ASN_PUTAWAY_RECORDS R 
    WHERE R.PUT_BY IS NOT NULL AND date(R.PUT_TIME) = DATE(NOW())
    GROUP BY R.PUT_BY,R.BILL_ID)A GROUP BY A.PUT_BY) C ON U.USER_CODE = C.PUT_BY
--   LEFT JOIN (SELECT COUNT(R.CNT) QTY, R.PUT_BY FROM (SELECT COUNT(1) CNT, R.PUT_BY
--     FROM WM_ASN_PUTAWAY_RECORDS R 
--     WHERE R.PUT_BY IS NOT NULL AND (R.PUT_TIME >= V_DATE_BEGIN AND R.PUT_TIME <= V_DATE_END)
--     GROUP BY R.CT_CODE, R.PUT_TIME) R GROUP BY R.PUT_BY) X ON X.PUT_BY = U.USER_CODE
--   LEFT JOIN (SELECT D.CREATOR, COUNT(D.BILL_NO) B_COUNT, SUM(IFNULL(D.CHECK_QTY, 0)) QTY FROM (
--     SELECT H.BILL_NO, SUM(IFNULL(WAC.QTY, 0)) CHECK_QTY, WAC.CREATOR 
--     FROM WM_ASN_HEADER H 
--     LEFT JOIN WM_ASN_CONTAINER WAC ON WAC.BILL_ID = H.BILL_ID 
--     LEFT JOIN WM_UM_SKU WUS ON WUS.ID = WAC.UM_SKU_ID 
--     LEFT JOIN WM_UM WU ON WU.UM_CODE = WUS.UM_CODE 
--     WHERE H.BILL_TYPE IN (1, 3) AND WAC.CREATOR IS NOT NULL AND WAC.CREATE_DATE >= V_DATE_BEGIN AND WAC.CREATE_DATE <= V_DATE_END 
--     GROUP BY H.BILL_NO) D GROUP BY D.CREATOR) D ON U.USER_CODE = D.CREATOR
--   LEFT JOIN (SELECT E.CREATOR, COUNT(E.BILL_NO) B_COUNT, SUM(IFNULL(E.QTY, 0)) QTY FROM (
--     SELECT H.BILL_NO, SUM(C.QTY) QTY, C.CREATOR FROM WM_CRN_HEADER H 
--     LEFT JOIN WM_ASN_CONTAINER C ON C.BILL_ID = H.BILL_ID 
--     WHERE C.CREATOR IS NOT NULL AND C.CREATE_DATE > @DateBegin AND C.CREATE_DATE <= V_DATE_END
--     GROUP BY H.BILL_ID) E GROUP BY E.CREATOR) E ON U.USER_CODE = E.CREATOR
  LEFT JOIN (
    
    SELECT A.USER_CODE,A.配送单量,A.配送整货,A.配送散货,
  case A.VH_ATTRI WHEN '220' THEN SUM(A.配送整货+A.配送散货) END '220_配送量',
  case A.VH_ATTRI WHEN '221' THEN SUM(A.配送整货+A.配送散货) END '221_配送量'
  FROM (
SELECT A.USER_CODE, SUM(IFNULL(A.BILL_COUNT, 0)) / B.U_COUNT 配送单量,SUM(A.CASE_QTY) / B.U_COUNT 配送整货, SUM(A.QTY) / B.U_COUNT 配送散货,
 A.VH_ATTRI
    FROM (
  
  SELECT U.USER_CODE, U.USER_NAME, A.VH_TRAIN_NO, A.VH_NO,COUNT(A.BILL_NO) BILL_COUNT, SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY,
    V.VH_ATTRI
  FROM USERS U 
  LEFT JOIN (
    SELECT H.VH_TRAIN_NO, H.VH_NO, D.BILL_NO, U.USER_CODE, U.USER_NAME, 
                        H.WHOLE_GOODS QTY1, H.BULK_CARGO_QTY QTY2
                        FROM WM_VEHICLE_TRAIN_HEADER H 
                        LEFT JOIN WM_VEHICLE_TRAIN_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        WHERE 
                        DATE(D.UPDATE_DATE) = DATE(NOW())
                        -- MONTH(D.UPDATE_DATE) = MONTH(NOW()) 
                        GROUP BY D.VH_TRAIN_NO, U.USER_CODE
      )  A ON A.USER_CODE = U.USER_CODE 
    LEFT JOIN wm_vehicle V ON A.VH_NO=V.VH_NO
            GROUP BY U.USER_CODE, A.VH_TRAIN_NO,V.VH_ATTRI) A 
  LEFT JOIN (

    SELECT H.VH_TRAIN_NO, COUNT(U.USER_CODE) U_COUNT FROM WM_VEHICLE_TRAIN_HEADER H 
                LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
                GROUP BY H.VH_TRAIN_NO
      
      ) B ON B.VH_TRAIN_NO = A.VH_TRAIN_NO 
 
  GROUP BY A.USER_CODE,A.VH_ATTRI) A
  GROUP BY A.USER_CODE
      
      ) F ON U.USER_CODE = F.USER_CODE
  LEFT JOIN (SELECT A.USER_CODE, SUM(A.CASE_QTY) 整货件数, SUM(A.QTY) 散货件数
    FROM (SELECT U.USER_CODE, U.USER_NAME, COUNT(A.BILL_NO) BILL_COUNT, SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY
            FROM USERS U 
            LEFT JOIN (SELECT H.VH_TRAIN_NO, D.BILL_NO, V.VH_NO, U.USER_CODE, U.USER_NAME, A.QTY1 / B.U_COUNT QTY1, A.QTY2 / B.U_COUNT QTY2
                        FROM WM_LOADING_HEADER H 
                        LEFT JOIN WM_LOADING_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN WM_LOADING_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN (SELECT H.BILL_NO, SUM(D.PICK_QTY) QTY1, F_GET_BULK_PIECES(H.BILL_ID) QTY2 FROM WM_SO_HEADER H
                                    LEFT JOIN WM_SO_DETAIL D ON D.BILL_ID = H.BILL_ID AND D.IS_CASE = 1
                                    WHERE H.BILL_NO IN (SELECT D.BILL_NO FROM WM_LOADING_DETAIL D
                                                          WHERE date(D.UPDATE_DATE) = DATE(NOW()))
                                    GROUP BY H.BILL_NO) A ON D.BILL_NO = A.BILL_NO
                        LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID 
                        LEFT JOIN (SELECT H.VH_TRAIN_NO, COUNT(1) U_COUNT FROM WM_LOADING_HEADER H 
                                    LEFT JOIN WM_LOADING_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
                                    GROUP BY H.VH_TRAIN_NO) B ON B.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        WHERE date(D.UPDATE_DATE) = DATE(NOW())
                        ) A ON A.USER_CODE = U.USER_CODE 
            GROUP BY U.USER_CODE) A 
    GROUP BY A.USER_CODE) G ON U.USER_CODE = G.USER_CODE
  LEFT JOIN WM_BASE_CODE WBC ON U.USER_TYPE = WBC.ITEM_VALUE
  GROUP BY U.USER_CODE;
  END IF;
  IF(V_FLAG = 2) THEN
     SELECT U.USER_NAME 人员姓名, WBC.ITEM_DESC 所属,  ROUND(IFNULL(A.QTY, 0), 0) 拣货量, ROUND(IFNULL(Z.QTY, 0), 0) 批市拣货量
  , ROUND(F.`220_配送量`, 2) '220_配送量', ROUND(F.`221_配送量`, 2) '221_配送量', ROUND((IFNULL(G.整货件数, 0)+IFNULL(G.散货件数, 0)), 2) 装车量
  , ROUND((IFNULL(B.QTY, 0)+ IFNULL(C.QTY, 0)), 0) 叉车量
   FROM USERS U
  LEFT JOIN (SELECT A.USER_CODE, COUNT(A.BILL_NO) B_COUNT, IFNULL(SUM(A.QTY), 0) QTY FROM (
      SELECT H.BILL_NO, R.USER_CODE, SUM(IFNULL(R.PICK_QTY / S.QTY, 0)) QTY FROM WM_SO_HEADER H 
      LEFT JOIN WM_SO_DETAIL D ON H.BILL_ID = D.BILL_ID
      LEFT JOIN WM_SO_PICK P ON P.BILL_ID = H.BILL_ID AND P.DETAIL_ID = D.ID 
      LEFT JOIN WM_SO_PICK_RECORD R ON R.BILL_ID = H.BILL_ID AND R.PICK_ID = P.ID 
      INNER JOIN WM_UM_SKU S ON S.SKU_CODE = D.SKU_CODE AND S.UM_CODE = D.UM_CODE
      WHERE R.USER_CODE IS NOT NULL 
      AND R.PICK_DATE>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
      AND R.PICK_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
      -- AND MONTH(R.PICK_DATE) = MONTH(NOW())
      GROUP BY H.BILL_ID) A GROUP BY A.USER_CODE) A ON U.USER_CODE = A.USER_CODE
  LEFT JOIN (SELECT IFNULL(SUM(R.PICK_QTY / S.QTY), 0) QTY, R.USER_CODE FROM WM_SO_HEADER H 
      LEFT JOIN WM_SO_DETAIL D ON H.BILL_ID = D.BILL_ID
      LEFT JOIN WM_SO_PICK P ON P.BILL_ID = H.BILL_ID AND P.DETAIL_ID = D.ID 
      LEFT JOIN WM_SO_PICK_RECORD R ON R.BILL_ID = H.BILL_ID AND R.PICK_ID = P.ID 
      LEFT JOIN WM_UM_SKU S ON S.SKU_CODE = D.SKU_CODE AND S.UM_CODE = D.UM_CODE
      WHERE LENGTH(H.BILL_NO) = 21 AND (SUBSTRING(H.BILL_NO, 3, 1) = 3 OR SUBSTRING(H.BILL_NO, 3, 1) = 8) 
      AND R.USER_CODE IS NOT NULL 
      AND R.PICK_DATE>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
      AND R.PICK_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
      -- AND MONTH(R.PICK_DATE) = MONTH(NOW())
      GROUP BY H.BILL_ID) Z ON U.USER_CODE = Z.USER_CODE
  LEFT JOIN (SELECT B.USER_CODE, SUM(IFNULL(B.QTY, 0)) QTY FROM (
      SELECT  COUNT(R.TO_LC_CODE) QTY, R.USER_CODE 
      FROM WM_TRANS_RECORD R 
      LEFT JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID 
      LEFT JOIN WM_UM U ON S.UM_CODE = U.UM_CODE 
      WHERE R.USER_CODE IS NOT NULL 
      AND R.CREATE_DATE>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
      AND R.CREATE_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
      -- AND MONTH(R.CREATE_DATE)= MONTH(NOW()) 
      GROUP BY R.BILL_ID) B GROUP BY B.USER_CODE) B ON U.USER_CODE = B.USER_CODE
--   LEFT JOIN (SELECT COUNT(1) CNT, R.USER_CODE
--       FROM WM_TRANS_RECORD R 
--       LEFT JOIN WM_UM_SKU S ON S.ID = R.UM_SKU_ID 
--       LEFT JOIN WM_UM U ON S.UM_CODE = U.UM_CODE 
--       WHERE R.USER_CODE IS NOT NULL AND (R.CREATE_DATE >= V_DATE_BEGIN AND R.CREATE_DATE <= V_DATE_END)
--       GROUP BY R.USER_CODE) Y ON Y.USER_CODE = U.USER_CODE
  LEFT JOIN (SELECT SUM(A.QTY) QTY,A.PUT_BY FROM ( SELECT COUNT(R.LC_CODE) QTY, R.PUT_BY
    FROM WM_ASN_PUTAWAY_RECORDS R 
    WHERE R.PUT_BY IS NOT NULL 
    AND R.PUT_TIME>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
    AND R.PUT_TIME<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
    -- AND MONTH(R.PUT_TIME) = MONTH(NOW())
    GROUP BY R.PUT_BY,R.BILL_ID)A GROUP BY A.PUT_BY) C ON U.USER_CODE = C.PUT_BY
--   LEFT JOIN (SELECT COUNT(R.CNT) QTY, R.PUT_BY FROM (SELECT COUNT(1) CNT, R.PUT_BY
--     FROM WM_ASN_PUTAWAY_RECORDS R 
--     WHERE R.PUT_BY IS NOT NULL AND (R.PUT_TIME >= V_DATE_BEGIN AND R.PUT_TIME <= V_DATE_END)
--     GROUP BY R.CT_CODE, R.PUT_TIME) R GROUP BY R.PUT_BY) X ON X.PUT_BY = U.USER_CODE
--   LEFT JOIN (SELECT D.CREATOR, COUNT(D.BILL_NO) B_COUNT, SUM(IFNULL(D.CHECK_QTY, 0)) QTY FROM (
--     SELECT H.BILL_NO, SUM(IFNULL(WAC.QTY, 0)) CHECK_QTY, WAC.CREATOR 
--     FROM WM_ASN_HEADER H 
--     LEFT JOIN WM_ASN_CONTAINER WAC ON WAC.BILL_ID = H.BILL_ID 
--     LEFT JOIN WM_UM_SKU WUS ON WUS.ID = WAC.UM_SKU_ID 
--     LEFT JOIN WM_UM WU ON WU.UM_CODE = WUS.UM_CODE 
--     WHERE H.BILL_TYPE IN (1, 3) AND WAC.CREATOR IS NOT NULL AND WAC.CREATE_DATE >= V_DATE_BEGIN AND WAC.CREATE_DATE <= V_DATE_END 
--     GROUP BY H.BILL_NO) D GROUP BY D.CREATOR) D ON U.USER_CODE = D.CREATOR
--   LEFT JOIN (SELECT E.CREATOR, COUNT(E.BILL_NO) B_COUNT, SUM(IFNULL(E.QTY, 0)) QTY FROM (
--     SELECT H.BILL_NO, SUM(C.QTY) QTY, C.CREATOR FROM WM_CRN_HEADER H 
--     LEFT JOIN WM_ASN_CONTAINER C ON C.BILL_ID = H.BILL_ID 
--     WHERE C.CREATOR IS NOT NULL AND C.CREATE_DATE > @DateBegin AND C.CREATE_DATE <= V_DATE_END
--     GROUP BY H.BILL_ID) E GROUP BY E.CREATOR) E ON U.USER_CODE = E.CREATOR
  LEFT JOIN (
      
      SELECT A.USER_CODE,A.配送单量,A.配送整货,A.配送散货,
  case A.VH_ATTRI WHEN '220' THEN SUM(A.配送整货+A.配送散货) END '220_配送量',
  case A.VH_ATTRI WHEN '221' THEN SUM(A.配送整货+A.配送散货) END '221_配送量'
  FROM (
SELECT A.USER_CODE, SUM(IFNULL(A.BILL_COUNT, 0)) / B.U_COUNT 配送单量,SUM(A.CASE_QTY) / B.U_COUNT 配送整货, SUM(A.QTY) / B.U_COUNT 配送散货,
 A.VH_ATTRI
    FROM (
  
  SELECT U.USER_CODE, U.USER_NAME, A.VH_TRAIN_NO, A.VH_NO,COUNT(A.BILL_NO) BILL_COUNT, SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY,
    V.VH_ATTRI
  FROM USERS U 
  LEFT JOIN (
    SELECT H.VH_TRAIN_NO, H.VH_NO, D.BILL_NO, U.USER_CODE, U.USER_NAME, 
                        H.WHOLE_GOODS QTY1, H.BULK_CARGO_QTY QTY2
                        FROM WM_VEHICLE_TRAIN_HEADER H 
                        LEFT JOIN WM_VEHICLE_TRAIN_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        WHERE 
                        D.UPDATE_DATE>=DATE_FORMAT(date_add(NOW(),interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
                        AND D.UPDATE_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
                        -- MONTH(D.UPDATE_DATE) = MONTH(NOW()) 
                        GROUP BY D.VH_TRAIN_NO, U.USER_CODE
      )  A ON A.USER_CODE = U.USER_CODE 
    LEFT JOIN wm_vehicle V ON A.VH_NO=V.VH_NO
            GROUP BY U.USER_CODE, A.VH_TRAIN_NO,V.VH_ATTRI) A 
  LEFT JOIN (

    SELECT H.VH_TRAIN_NO, COUNT(U.USER_CODE) U_COUNT FROM WM_VEHICLE_TRAIN_HEADER H 
                LEFT JOIN WM_VEHICLE_TRAIN_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
                GROUP BY H.VH_TRAIN_NO
      
      ) B ON B.VH_TRAIN_NO = A.VH_TRAIN_NO 
 
  GROUP BY A.USER_CODE,A.VH_ATTRI) A
  GROUP BY A.USER_CODE
        
        ) F ON U.USER_CODE = F.USER_CODE
  LEFT JOIN (SELECT A.USER_CODE, SUM(A.CASE_QTY) 整货件数, SUM(A.QTY) 散货件数
    FROM (SELECT U.USER_CODE, U.USER_NAME, COUNT(A.BILL_NO) BILL_COUNT, SUM(A.QTY1) CASE_QTY, SUM(A.QTY2) QTY
            FROM USERS U 
            LEFT JOIN (SELECT H.VH_TRAIN_NO, D.BILL_NO, V.VH_NO, U.USER_CODE, U.USER_NAME, A.QTY1 / B.U_COUNT QTY1, A.QTY2 / B.U_COUNT QTY2
                        FROM WM_LOADING_HEADER H 
                        LEFT JOIN WM_LOADING_DETAIL D ON D.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN WM_LOADING_USERS U ON D.VH_TRAIN_NO = U.VH_TRAIN_NO AND U.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        LEFT JOIN (SELECT H.BILL_NO, SUM(D.PICK_QTY) QTY1, F_GET_BULK_PIECES(H.BILL_ID) QTY2 FROM WM_SO_HEADER H
                                    LEFT JOIN WM_SO_DETAIL D ON D.BILL_ID = H.BILL_ID AND D.IS_CASE = 1
                                    WHERE NOT exists (SELECT D.BILL_NO FROM WM_LOADING_DETAIL D
                                                          WHERE 
                                                        D.UPDATE_DATE>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
                        AND D.UPDATE_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
                                                       -- MONTH(D.UPDATE_DATE) = MONTH(NOW()) 
                                                        AND H.BILL_NO=D.BILL_NO)
                                    GROUP BY H.BILL_NO) A ON D.BILL_NO = A.BILL_NO
                        LEFT JOIN WM_VEHICLE V ON V.ID = H.VH_ID 
                        LEFT JOIN (SELECT H.VH_TRAIN_NO, COUNT(1) U_COUNT FROM WM_LOADING_HEADER H 
                                    LEFT JOIN WM_LOADING_USERS U ON H.VH_TRAIN_NO = U.VH_TRAIN_NO 
                                    GROUP BY H.VH_TRAIN_NO) B ON B.VH_TRAIN_NO = H.VH_TRAIN_NO 
                        WHERE
                        D.UPDATE_DATE>=DATE_FORMAT(date_add(NOW(), interval - day(curdate()) + 1 day),'%Y-%m-%d 00:00:00') 
                        AND D.UPDATE_DATE<=DATE_FORMAT(DATE_ADD(NOW(),INTERVAL -1 DAY),'%Y-%m-%d 23:59:59')
                        -- MONTH(D.UPDATE_DATE) = MONTH(NOW())
                        ) A ON A.USER_CODE = U.USER_CODE 
            GROUP BY U.USER_CODE) A 
    GROUP BY A.USER_CODE) G ON U.USER_CODE = G.USER_CODE
  LEFT JOIN WM_BASE_CODE WBC ON U.USER_TYPE = WBC.ITEM_VALUE
  GROUP BY U.USER_CODE;
  END IF;
END
$$
DELIMITER;
INSERT INTO wm_base_code(GROUP_CODE, ITEM_VALUE, ITEM_DESC, IS_ACTIVE, REMARK, Attri1) VALUES
('120', 'v1', '司机配送系数（金杯）', 'Y', '权重系数', NULL);";

            map.ExecuteMySqlScript(sql);
        }
    }
}
