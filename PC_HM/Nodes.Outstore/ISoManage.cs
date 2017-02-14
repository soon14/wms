using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public interface ISoManage
    {
        SOHeaderEntity GetFocusedBill();
        List<SOHeaderEntity> GetFocusedBills();
        void BindingGrid(List<SOHeaderEntity> headers);
        void ShowFocusDetail();
        void ShowQueryCondition(int queryType, string billNO, string customer, string salesMan, string billType, string billStatus,
            string outboundType, string shipNO, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// 刷新单据行表格数据
        /// </summary>
        void RefreshHeaderGrid();
    }
}
