using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 整货仓
    /// </summary>
    public class FullWarehouse : WarehouseBase
    {
        public override void GetIsCaseQty(StringBuilder strBuilder, out int? isCase1, out int? isCase2)
        {
            isCase1 = isCase2 = 0;
            string str = strBuilder.ToString();
            if (str.Length == 0)
            {
                isCase1 = isCase2 = 0;
            }
            str = str.Remove(str.Length - 1);
            IMapper mapper = DatabaseInstance.Instance();
            string sqlIsCase1 = String.Format("SELECT ROUND(SUM(A.PICK_QTY),0) FROM wm_so_detail A  WHERE A.IS_CASE = 1 AND  A.BILL_ID IN ({0}) ;", str);
            string sqlIsCase2 = String.Format("SELECT COUNT(1) FROM WM_CONTAINER_RECORD A  WHERE A.BILL_HEAD_ID IN ({0});", str);
            object objIsCase1 = mapper.ExecuteScalar<object>(sqlIsCase1);
            object objIsCase2 = mapper.ExecuteScalar<object>(sqlIsCase2);
            isCase1 = ConvertUtil.ToInt(objIsCase1);
            isCase2 = ConvertUtil.ToInt(objIsCase2);
        }
    }
}
