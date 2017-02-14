using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class CompanyDal
    {
        /// <summary>
        /// 公司编号是否存在
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        private bool IsCompanyCodeExists(CompanyEntity company)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select COMPANY_CODE from COMPANY where COMPANY_CODE = @ID",
            new { ID = company.CompanyCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 新建或者修改公司信息
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public int CreateOrUpdateCompany(CompanyEntity Company, bool isCreateNew)
        {
            int ret = 0;

            //检查编号是否已经存在
            bool exists = IsCompanyCodeExists(Company);
            if (isCreateNew && exists)
                return -1;

            //更新
            if (!isCreateNew && !exists)
                return -2;

            IMapper map = DatabaseInstance.Instance();
            if (isCreateNew)
            {
                ret = map.Execute("insert into COMPANY(COMPANY_CODE, COMPANY_NAME, ADDR, PHONE, FAX, EMAIL, POSTCODE, REMARK) " +
                    "values(@ID, @Name, @Address, @Phone, @Fax, @Email, @Postcode, @Remark)",
                new
                {
                    ID = Company.CompanyCode,
                    Name = Company.CompanyName,
                    Address = Company.Address,
                    Phone = Company.Phone,
                    Fax = Company.Fax,
                    Email = Company.Email,
                    Postcode = Company.Postcode,
                    Remark = Company.Remark
                });
            }
            else
            {
                //更新
                ret = map.Execute("update COMPANY set COMPANY_NAME = @Name, ADDR = @Address, PHONE = @Phone, " +
                    "FAX = @Fax, EMAIL = @Email, POSTCODE = @Postcode, REMARK = @Remark where COMPANY_CODE = @ID",
                new
                {
                    Name = Company.CompanyName,
                    Address = Company.Address,
                    Phone = Company.Phone,
                    Fax = Company.Fax,
                    Email = Company.Email,
                    Postcode = Company.Postcode,
                    Remark = Company.Remark,
                    ID = Company.CompanyCode
                });
            }

            return ret;
        }

        public List<CompanyEntity> GetCompanys()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT COMPANY_CODE, COMPANY_NAME, ADDR, PHONE, FAX, EMAIL, POSTCODE, REMARK FROM COMPANY";
            return map.Query<CompanyEntity>(sql);
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>0：成功；1：被用户引用，无法删除</returns>
        public int DeleteCompany(string CompanyCode)
        {
            IMapper map = DatabaseInstance.Instance();

            //查看是否有部门在引用
            //string sql = "select COMPANYCODE from DEPARTMENT where COMPANYCODE = @companyCode";
            //string temp = map.ExecuteScalar<string>(sql, new { companyCode = CompanyCode });
            //if (string.IsNullOrEmpty(temp))
            //{
            map.Execute("delete from Company where COMPANY_CODE = @companyCode", new { companyCode = CompanyCode });
                return 0;
            //}
            //else
            //{
            //    return -1;
            //}
        }
    }
}