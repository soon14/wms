using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class CodeItemDal
    {
       ///<summary>
       ///根据代码集编号获取代码项信息
       ///</summary>
       ///<returns></returns>
        public List<CodeItemEntity> GetCodeItemByCodeSetCode(string codeSetCode)
       {
           IMapper map = DatabaseInstance.Instance();
           string sql = "SELECT COD, NAM FROM CODEITEM WHERE CODESET_CODE = @CODESET_CODE AND IS_ACTIVE = 1 ORDER BY COD ASC";
           List<CodeItemEntity> ZoneEntities = map.Query<CodeItemEntity>(sql, new { CODESET_CODE = codeSetCode });
           return ZoneEntities;
       }
    }
}
