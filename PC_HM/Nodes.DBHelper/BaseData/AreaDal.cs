using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 区域数据访问类
    /// </summary>
    public class AreaDal
    {
        private const string SELECT_BODY = "SELECT ID, PARENT_ID, AR_CODE, AR_NAME FROM AREA ";
        
        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private bool IsCodeExists(AreaEntity area)
        {
            IMapper map = DatabaseInstance.Instance();
            string code = map.ExecuteScalar<string>("select AR_CODE from AREA where AR_CODE = @AR_CODE",
            new { AR_CODE = area.Code });

            return !string.IsNullOrEmpty(code);
        }

        /// <summary>
        /// 添加或编辑区域
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(AreaEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;

                ret = map.Execute("INSERT INTO AREA(PARENT_ID, AR_CODE, AR_NAME)" +
                    "VALUES(@PARENT_ID, @AR_CODE, @AR_NAME)",
                new
                {
                    PARENT_ID = entity.ParentID,
                    AR_CODE = entity.Code,
                    AR_NAME = entity.Name
                });
            }
            else
            {
                //更新，不要更新ParentID
                ret = map.Execute("UPDATE AREA SET AR_CODE = @AR_CODE, AR_NAME = @AR_NAME WHERE ID = @ID",
                new
                {
                    Code = entity.Code,
                    AR_NAME = entity.Name,
                    ID = entity.ID
                });
            }

            return ret;
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<AreaEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<AreaEntity>(SELECT_BODY);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>0：成功；-1：有资产引用，无法删除</returns>
        public int Delete(int id)
        {
            return -1;
            //IMapper map = DatabaseInstance.Instance();

            //DynamicParameters parms = new DynamicParameters();
            //parms.Add("ID", id);
            //parms.AddOut("RET_VAL", System.Data.DbType.Int32, 4);

            //map.Execute("P_AREA_DELETE",  parms, System.Data.CommandType.StoredProcedure);
            //return parms.Get<int>("RET_VAL");
        }
    }
}
