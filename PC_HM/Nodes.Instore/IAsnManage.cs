using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.Instore
{
    public interface IAsnManage
    {
        /// <summary>
        /// 获取选中的单据号，支持多选
        /// </summary>
        /// <returns></returns>
        List<AsnHeaderEntity> GetFocusedBills();

        /// <summary>
        /// 只返回第一个选中的单据
        /// </summary>
        /// <returns></returns>
        AsnHeaderEntity GetFocusedBill();

        /// <summary>
        /// 从界面上移出单据行
        /// </summary>
        /// <param name="header"></param>
        void RemoveBill(AsnHeaderEntity header);

        /// <summary>
        /// 刷新单据行表格数据
        /// </summary>
        void RefreshHeaderGrid();

        void BindingGrid(List<AsnHeaderEntity> headers);
        void ShowFocusDetail();
        void ShowQueryCondition(int queryType, string billNO, string supplier, string salesMan, string billType, string billStatus,
            string inboundType, DateTime dateFrom, DateTime dateTo);
    }
}
