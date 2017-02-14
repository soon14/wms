using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class VehicleDal
    {
        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(VehicleEntity unit)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select VH_CODE from WM_VEHICLE where VH_CODE = @COD OR VH_NO = @VehicleNo", new
            {
                COD = unit.VehicleCode,
                VehicleNo = unit.VehicleNO
            });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(VehicleEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute("INSERT INTO WM_VEHICLE(VH_CODE, VH_NO, VH_VOLUME, USER_CODE, RT_CODE, IS_ACTIVE, VH_TYPE, VH_ATTRI) " +
                    "VALUES(@VH_CODE, @VH_NO, @VH_VOLUME, @USER_CODE, @RT_CODE, @IS_ACTIVE, @VH_TYPE, @VH_ATTRI)",
                new
                {
                    VH_CODE = entity.VehicleCode,
                    VH_NO = entity.VehicleNO,
                    VH_VOLUME = entity.VehicleVolume,
                    USER_CODE = entity.UserCode,
                    RT_CODE = entity.RouteCode,
                    IS_ACTIVE = 'Y',
                    VH_TYPE = entity.VhType,
                    VH_ATTRI = entity.VhAttri
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE WM_VEHICLE SET VH_NO = @VH_NO, VH_VOLUME = @VH_VOLUME, " +
                    "USER_CODE = @USER_CODE, RT_CODE = @RT_CODE, IS_ACTIVE = @IS_ACTIVE, VH_TYPE = @VH_TYPE, " +
                    "VH_ATTRI = @VH_ATTRI " +
                    "WHERE VH_CODE = @VH_CODE",
                new
                {
                    VH_NO = entity.VehicleNO,
                    VH_VOLUME = entity.VehicleVolume,
                    USER_CODE = entity.UserCode,
                    RT_CODE = entity.RouteCode,
                    IS_ACTIVE = entity.IsActive,
                    VH_CODE = entity.VehicleCode,
                    VH_TYPE = entity.VhType,
                    VH_ATTRI = entity.VhAttri
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<VehicleEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT V.ID, V.VH_CODE, V.VH_NO, V.VH_VOLUME, V.USER_CODE, U.USER_NAME, V.RT_CODE, R.RT_NAME, " +
                "V.IS_ACTIVE,U.MOBILE_PHONE, V.VH_TYPE, WBC.ITEM_DESC VH_TYPE_DESC, V.VH_ATTRI, BC.ITEM_DESC VH_ATTRI_DESC " +
                "FROM WM_VEHICLE V " +
                "LEFT JOIN USERS U ON V.USER_CODE = U.USER_CODE " +
                "LEFT JOIN WM_ROUTE R ON R.RT_CODE = V.RT_CODE " +
                "LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = V.VH_TYPE " +
                "LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = V.VH_ATTRI " +
                "WHERE IFNULL(V.IS_DELETED, 0) <> 1";
            return map.Query<VehicleEntity>(sql);
        }

        public DataTable GetVehicleInfo()
        {
            string sql = "SELECT V.ID, V.VH_CODE, V.VH_NO, V.VH_VOLUME, V.USER_CODE, U.USER_NAME, V.RT_CODE, R.RT_NAME,CONCAT (V.VH_NO ,'——',IFNULL(R.RT_NAME, '')) JIANCH,  " +
                "V.IS_ACTIVE,U.MOBILE_PHONE, V.VH_TYPE, WBC.ITEM_DESC VH_TYPE_DESC, V.VH_ATTRI, BC.ITEM_DESC VH_ATTRI_DESC " +
                "FROM WM_VEHICLE V " +
                "LEFT JOIN USERS U ON V.USER_CODE = U.USER_CODE " +
                "LEFT JOIN WM_ROUTE R ON R.RT_CODE = V.RT_CODE " +
                "LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = V.VH_TYPE " +
                "LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = V.VH_ATTRI " +
                "WHERE IFNULL(V.IS_DELETED, 0) <> 1";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        ///  查询车辆信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVehicleAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT V.VH_NO '车牌号',  R.RT_NAME '路线名称',U.USER_NAME '司机姓名',"+
               " WBC.ITEM_DESC '所属方' "+
               " FROM WM_VEHICLE V  "+
               " LEFT JOIN USERS U ON V.USER_CODE = U.USER_CODE  "+
               " LEFT JOIN WM_ROUTE R ON R.RT_CODE = V.RT_CODE  "+
               " LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = V.VH_TYPE  "+
               " LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = V.VH_ATTRI  "+
               " WHERE IFNULL(V.IS_DELETED, 0) <> 1";
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 根据 Vehicle ID 获取实体
        /// </summary>
        /// <param name="vehicleID">Vehicle 主键ID</param>
        /// <returns></returns>
        //public VehicleEntity GetEntity(int vehicleID)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    string sql = "SELECT V.ID, V.VH_CODE, ";
        //    return map.Query<VehicleEntity>(sql);
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="VehicleCode"></param>
        /// <returns></returns>
        public int Delete(string VehicleCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("UPDATE WM_VEHICLE SET IS_DELETED = 1 WHERE VH_CODE = @VH_CODE", new { VH_CODE = VehicleCode });
        }

    }
}
