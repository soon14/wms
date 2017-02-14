using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class CustomerDal
    {

        /// <summary>
        /// 添加或编辑客户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int CustomerAddAndUpdate(CustomerEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (!isNew)
            {
                //更新
                ret = map.Execute("UPDATE CUSTOMERS SET RT_CODE = @RouteCode,DISTANCE=@Distance WHERE C_CODE = @CustomerCode",
                new
                {
                    //NAME = entity.CustomerName,
                    //NAME_S = entity.CustomerNameS,
                    //NAMPY = entity.CustomerNamePY,
                    //SORT_ORDER = entity.SortOrder,
                    //IS_ACTIVE = entity.IsActive,
                    //UPDATE_BY = entity.LastUpdateBy,
                    //UPDATE_DATE = entity.LastUpdateDate,
                    RouteCode = entity.RouteCode,
                    Distance=entity.Distance,
                    CustomerCode = entity.CustomerCode,

                });
            }

            return ret;
        }

        ///<summary>
        ///查询所有客户及默认地址
        ///</summary>
        ///<returns></returns>
        public List<CustomerEntity> GetAllCustomer(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT C.C_CODE, C.C_NAME, C.NAME_S, C.AREA_ID, A.AR_NAME, C.IS_ACTIVE, " +
                "C.SORT_ORDER, C.REMARK, C.UPDATE_BY, C.UPDATE_DATE, C.IS_OWN, C.WH_CODE, C.RT_CODE, R.RT_NAME, " +
                "C.ADDRESS, C.CONTACT, C.PHONE, C.POSTCODE,C.DISTANCE FROM CUSTOMERS C " +
                "LEFT JOIN WM_ROUTE R ON R.RT_CODE = C.RT_CODE " +
                "LEFT JOIN AREA A ON C.AREA_ID = A.ID ");
            return map.Query<CustomerEntity>(sql);
        }

        public List<CustomerEntity> GetActiveCustomer(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT C.C_CODE, C.C_NAME, C.NAME_S, C.AREA_ID, A.AR_NAME, C.IS_ACTIVE, " +
                "C.SORT_ORDER, C.REMARK, C.UPDATE_BY, C.UPDATE_DATE, C.IS_OWN, C.WH_CODE, C.RT_CODE, R.RT_NAME, " +
                "C.ADDRESS, C.CONTACT, C.PHONE, C.POSTCODE FROM CUSTOMERS C " +
                "LEFT JOIN WM_ROUTE R ON R.RT_CODE = C.RT_CODE " +
                "LEFT JOIN AREA A ON C.AREA_ID = A.AR_CODE " +
                "WHERE C.IS_ACTIVE = 'Y'");
            return map.Query<CustomerEntity>(sql);
        }

        //public CustomerEntity GetByCode(string code)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    string sql = "";
        //    return map.QuerySingle<CustomerEntity>(sql, new { CODE = code });
        //}

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="StockCustomerCode"></param>
        /// <returns></returns>
        public bool DeleteCustomer(string CustomerCode)
        {
            //IMapper map = DatabaseInstance.Instance();
            //map.Execute("", new { CODE = CustomerCode });
            //map.Execute("", new { CODE = CustomerCode });
            return true;
        }
    }
}