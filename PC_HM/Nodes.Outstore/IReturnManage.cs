using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public interface IReturnManage
    {
        ReturnHeaderEntity GetFocusedBill();
        List<ReturnHeaderEntity> GetFocusedBills();
        void BindingGrid(List<ReturnHeaderEntity> headers);
        void ShowFocusDetail();
        void ShowQueryCondition(int queryType, string billNO, string customer, string salesMan, string itemDesc, string billStatus,
            string returnDriver, string shipNO, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// 刷新单据行表格数据
        /// </summary>
        void RefreshHeaderGrid();
    }
}
