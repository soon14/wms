using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class SupplierDal
    {
        /// <summary>
        /// 检查供应商编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsSupplierCodeExists(SupplierEntity Supplier)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT S_CODE FROM SUPPLIERS WHERE S_CODE = @CODE",
            new { CODE = Supplier.SupplierCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑供应商
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int Save(SupplierEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsSupplierCodeExists(entity))
                    return -1;
                ret = map.Execute(
                    string.Format("insert into SUPPLIERS(SUP_CODE, SUP_NAME, NAME_S, NAME_PY, PROVINCE, CONTACT, PHONE, ADDRESS, POSTCODE, SORT_ORDER, IS_ACTIVE, UPDATE_BY, UPDATE_DATE, IS_OWN) " +
                    "values(@CODE, @NAME, @NAME_S, @NAME_PY, @PROVINCE, @CONTACT, @PHONE, @ADDRESS, @POSTCODE, @SORT_ORDER, @IS_ACTIVE, @UPDATE_BY, {0}, @IS_OWN)", map.GetSysDateString()),
                new
                {
                    //CODE = entity.SupplierCode,
                    //NAME = entity.SupplierName,
                    //NAME_S = entity.SupplierNameS,
                    //NAME_PY = entity.SupplierNamePY,
                    //PROVINCE = entity.Province,
                    //CONTACT = entity.ContactName,
                    //PHONE = entity.Phone,
                    //ADDRESS = entity.Address,
                    //POSTCODE = entity.Postcode,
                    //SORT_ORDER = entity.SortOrder,
                    //IS_ACTIVE = entity.IsActive,
                    //UPDATE_BY = entity.LastUpdateBy,
                    //IS_OWN = "Y"
                });
            }
            else
            {
                //更新
                ret = map.Execute(
                    string.Format("update SUPPLIERS set SUP_NAME = @NAME, NAME_S = @NAME_S, NAME_PY = @NAMPY, " +
                    "PROVINCE = @PROVINCE, CONTACT = @CONTACT, PHONE = @PHONE, ADDRESS = @ADDRESS, POSTCODE = @POSTCODE, " +
                    "SORT_ORDER = @SORT_ORDER, IS_ACTIVE = @IS_ACTIVE, UPDATE_BY = @UPDATE_BY, UPDATE_DATE = {0} " +
                    "WHERE SUP_CODE = @CODE", map.GetSysDateString()),
                new
                {
                    //NAME = entity.SupplierName,
                    //NAME_S = entity.SupplierNameS,
                    //NAMPY = entity.SupplierNamePY,
                    //PROVINCE = entity.Province,
                    //CONTACT = entity.ContactName,
                    //PHONE = entity.Phone,
                    //ADDRESS = entity.Address,
                    //POSTCODE = entity.Postcode,
                    //SORT_ORDER = entity.SortOrder,
                    //IS_ACTIVE = entity.IsActive,
                    //UPDATE_BY = entity.LastUpdateBy,
                    //CODE = entity.SupplierCode
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有供应商
        ///</summary>
        ///<returns></returns>
        public List<SupplierEntity> GetAllSupplier()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT S.S_CODE, S.NAME_S, S.S_NAME, S.AREA_ID, A.AR_NAME, S.CONTACT, S.PHONE, S.ADDRESS, " +
                "S.POSTCODE, S.SORT_ORDER, S.IS_ACTIVE, S.REMARK, S.UPDATE_BY, S.UPDATE_DATE, S.IS_OWN FROM SUPPLIERS S " +
                "JOIN AREA A ON A.AR_CODE = S.AREA_ID";
            return map.Query<SupplierEntity>(sql);
        }

        /// <summary>
        /// 按照次序排序的供应商列表
        /// </summary>
        /// <returns></returns>
        public List<SupplierEntity> ListActiveSupplierByPriority()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT S.S_CODE, S.NAME_S, S.S_NAME, S.AREA_ID, A.AR_NAME, S.CONTACT, S.PHONE, S.ADDRESS, " +
                "S.POSTCODE, S.SORT_ORDER, S.IS_ACTIVE, S.REMARK, S.UPDATE_BY, S.UPDATE_DATE, S.IS_OWN FROM SUPPLIERS S " +
                "JOIN AREA A ON A.AR_CODE = S.AREA_ID WHERE S.IS_ACTIVE = 'Y' ORDER BY S.SORT_ORDER DESC";
            return map.Query<SupplierEntity>(sql);
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="StockSupplierCode"></param>
        /// <returns></returns>
        public bool DeleteSupplier(string SupplierCode)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("delete from SUPPLIERS where S_CODE = @CODE", new { CODE = SupplierCode });
            return true;
        }
    }
}