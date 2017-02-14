using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ContainerDal
    {
        /// <summary>
        /// 检查托盘编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(ContainerEntity Container)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT CT_CODE FROM WM_CONTAINER WHERE CT_CODE = @COD", new { COD = Container.ContainerCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 根据托盘编号找到对应的托盘
        /// </summary>
        /// <param name="code">托盘编号</param>
        /// <returns></returns>
        public T GetContainerByCode<T>(string code, string warehouseCode)
        {
            string sql = string.Format(
                "SELECT C.CT_CODE, C.CT_NAME, C.CT_TYPE, T.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, C.IS_DELETED " +
                "FROM WM_CONTAINER C INNER JOIN WM_BASE_CODE T ON C.CT_TYPE = T.ITEM_VALUE WHERE " +
                "C.WH_CODE = '{0}' AND C.CT_CODE = '{1}' AND IFNULL(C.IS_DELETED, 0) <> 1 ", warehouseCode, code);
            return DatabaseInstance.Instance().QuerySingle<T>(sql);
        }

        /// <summary>
        /// 获取容器最大值（批量新增）
        /// </summary>
        /// <param name="ctType"></param>
        /// <returns></returns>
        public string GetMaxContainerCode(string ctType)
        {
            string sql = string.Format("SELECT MAX(wc.CT_CODE) FROM wm_container wc WHERE wc.CT_TYPE = '{0}';", ctType);
            return DatabaseInstance.Instance().ExecuteScalar<string>(sql);
        }

        /// <summary>
        /// 批量新增容器/修改容器信息
        /// </summary>
        public int SaveContainerCode(ContainerEntity Container, bool isNew, int ctNum)
        {
            IMapper map = DatabaseInstance.Instance();
            int result = 0;

            if (!isNew)
            {
                //更新
                result = map.Execute("UPDATE WM_CONTAINER SET CT_WEIGHT = @WEIGHT, LAST_UPDATETIME = NOW() WHERE CT_CODE = @CODE;",
                new
                {
                    CODE = Container.ContainerCode,
                    WEIGHT = Container.ContainerWeight
                });
            }
            else
            {
                //检查编号是否已经存在
                if (IsCodeExists(Container))
                {
                    return -1;    //容器编号已存在
                }

                long ctCode = long.Parse(Container.ContainerCode);
                long ctName = long.Parse(Container.ContainerName);
                //批量增加容器
                for (int i = 0; i < ctNum; i++)
                {
                    try
                    {
                        map.Execute("INSERT INTO WM_CONTAINER(CT_CODE, CT_NAME, CT_TYPE, CT_WEIGHT, LAST_UPDATETIME, WH_CODE) VALUES(@CODE, @NAME, @TYPE, @WEIGHT, NOW(), @WH_CODE)",
                        new
                        {
                            CODE = Utils.ConvertUtil.ToString(ctCode + i),
                            NAME = string.Format("{0:d5}", (ctName + i)),
                            TYPE = Container.ContainerType,
                            WEIGHT = Container.ContainerWeight,
                            WH_CODE = Container.WarehouseCode
                        });

                        map.Execute("INSERT INTO WM_CONTAINER_STATE(CT_CODE, CT_STATE, LAST_UPDATETIME) VALUES(@CODE, '80', NOW())",
                        new
                        {
                            CODE = Utils.ConvertUtil.ToString(ctCode + i)
                        });

                        result++;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return result; //更新成功的次数
        }

        ///<summary>
        ///查询所有托盘
        ///</summary>
        ///<returns></returns>
        public List<ContainerEntity> GetAllContainer(string warehouseCode, string state)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = null;
            if (state == "Y")
            {
                sql = string.Format(
                    "SELECT C.CT_CODE, C.CT_NAME, C.CT_TYPE, T.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, C.IS_DELETED " +
                    "FROM WM_CONTAINER C INNER JOIN WM_BASE_CODE T ON C.CT_TYPE = T.ITEM_VALUE " +
                    "WHERE C.WH_CODE = '{0}' AND IFNULL(C.IS_DELETED, 0) <> 1 ", warehouseCode);
            }
            else
            {
                sql = string.Format(
                    "SELECT C.CT_CODE, C.CT_NAME, C.CT_TYPE, T.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, C.IS_DELETED " +
                    "FROM WM_CONTAINER C INNER JOIN WM_BASE_CODE T ON C.CT_TYPE = T.ITEM_VALUE " +
                    "WHERE C.WH_CODE = '{0}' AND IFNULL(C.IS_DELETED, 0) = 1 ", warehouseCode);
            }
            return map.Query<ContainerEntity>(sql);
        }
        /// <summary>
        /// 获取所有有关联的托盘
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        public static List<ContainerEntity> GetContainerListByBillID()
        {
            string sql = string.Format(
                "SELECT C.CT_CODE, C.CT_NAME, C.CT_TYPE, T.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, CS.BILL_HEAD_ID, C.IS_DELETED " +
                "FROM WM_CONTAINER C INNER JOIN WM_BASE_CODE T ON C.CT_TYPE = T.ITEM_VALUE " +
                "LEFT JOIN WM_CONTAINER_STATE CS ON CS.CT_CODE = C.CT_CODE " +
                "WHERE IFNULL(CS.BILL_HEAD_ID, 0) <> 0 AND C.CT_TYPE = '51' AND IFNULL(C.IS_DELETED, 0) <> 1 ");
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ContainerEntity>(sql);
        }
        public static List<ContainerEntity> GetContainerListByBillID(int billID, EWarehouseType wType)
        {
            string sql = string.Empty;
            if (wType == EWarehouseType.整货仓)
            {
                sql = string.Format(
                     "SELECT DISTINCT R.CT_CODE, C.CT_NAME, C.CT_TYPE, BC.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, " +
                     "R.BILL_HEAD_ID, C.IS_DELETED " +
                     "FROM WM_CONTAINER_RECORD R " +
                     "LEFT JOIN WM_CONTAINER C ON C.CT_CODE = R.CT_CODE " +
                     "LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = C.CT_TYPE " +
                     "WHERE R.BILL_HEAD_ID = {0} AND IFNULL(C.IS_DELETED, 0) <> 1", billID);
            }
            else
            {
                sql = string.Format(
                    "SELECT DISTINCT R.CT_CODE, C.CT_NAME, C.CT_TYPE, BC.ITEM_DESC CT_TYPE_DESC, C.CT_WEIGHT, " +
                    "R.BILL_ID, C.IS_DELETED " +
                    "FROM WM_SO_PICK_RECORD R " +
                    "LEFT JOIN WM_SO_PICK P ON P.ID = R.PICK_ID " +
                    "LEFT JOIN WM_SO_DETAIL D ON D.ID = P.DETAIL_ID AND D.IS_CASE = 2 " +
                    "INNER JOIN WM_CONTAINER C ON C.CT_CODE = R.CT_CODE AND C.CT_TYPE = '51' " +
                    "LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = C.CT_TYPE " +
                    "WHERE R.BILL_ID = {0} AND IFNULL(C.IS_DELETED, 0) <> 1", billID);
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ContainerEntity>(sql);
        }

        /// <summary>
        /// 删除托盘
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public int Delete(string ContainerCode, int deleteFlag)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("UPDATE WM_CONTAINER SET IS_DELETED = @DeleteFlag WHERE CT_CODE = @COD", new 
            { 
                DeleteFlag = deleteFlag,
                COD = ContainerCode 
            });
        }
        /// <summary>
        /// 更新容器重量
        /// </summary>
        /// <param name="code">容器编号</param>
        /// <param name="warehouseCode">库房编号</param>
        /// <returns></returns>
        public int UpdateWeight(string code, string warehouseCode, decimal weight)
        {
            string sql = "UPDATE WM_CONTAINER SET CT_WEIGHT = @Weight " +
                "WHERE CT_CODE = @CTCode AND WH_CODE= @WHCode";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CTCode = code, WHCode = warehouseCode, Weight = weight });
        }
        public int UpdateWeight(ContainerEntity entity, decimal weight)
        {
            return UpdateWeight(entity.ContainerCode, entity.WarehouseCode, weight);
        }
    }
        
}
