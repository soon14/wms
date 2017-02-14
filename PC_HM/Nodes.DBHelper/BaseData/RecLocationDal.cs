using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities.BaseData;
using Nodes.Dapper;

namespace Nodes.DBHelper.BaseData
{
    public class RecLocationDal
    {
        public List<RecLocationEntity> GetAllRecLocation()
        {
            string sql = "SELECT wz.ZN_NAME,wl.LC_CODE,wsl.SKU_CODE,ws.SKU_NAME,ws.SPEC,ifnull(wsl.LC_CODE,'') RECLOC FROM wm_location wl "
                      + "JOIN wm_zone wz ON wl.ZN_CODE=wz.ZN_CODE "
                      + "LEFT JOIN wm_sku_location wsl ON wl.LC_CODE=wsl.LC_CODE "
                      + "LEFT JOIN wm_sku ws ON wsl.SKU_CODE=ws.SKU_CODE ";
             
            IMapper map = DatabaseInstance.Instance();
            return map.Query<RecLocationEntity>(sql);
        }

        public int Save(RecLocationEntity entity, bool IsCreateNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret=-1;
            if (entity.RecLoc=="")
            {
                ret = map.Execute("Insert into wm_sku_location (LC_CODE,SKU_CODE) values(@lcCode,@skuCode)",
                    new
                    {
                        lcCode = entity.Location,
                        skuCode = entity.SkuCode
                    });
            }
            else
            {
                ret = map.Execute("Update wm_sku_location set  SKU_CODE=@skuCode where LC_CODE=@lcCode",
                    new
                    {
                        skuCode = entity.SkuCode,
                        lcCode = entity.Location
                    });
            }
            return ret;
        }

        public int DeleteRecLoc(string recLoc)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("delete from wm_sku_location where LC_CODE=@lcCode;", new { lcCode = recLoc });
        }
    }
}
