using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Nodes.Shares;
using Nodes.DBHelper;
using System.Collections.Generic;
using System.IO;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public partial class RptXT : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepXT.repx";
        private SODal spDal = new SODal();
        public RptXT()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 单据对应LPN总数
        /// </summary>
        public int LPNCount { get; set; }
        /// <summary>
        /// 当前打印第几箱
        /// </summary>
        public int LPNNum { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int BillID { get; set; }
        /// <summary>
        /// 物流箱号
        /// </summary>
        public string LPN { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string BillNO { get; set; }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTitle.Text = BillNO;
            //lblBuyerId.Text = this.Customer;
            lblConsignee.Text = this.CustomerName;
            lblWeight.Text = this.Weight + "KG";
            lblSmallID.Text = String.Format("{0}/{1}箱", this.LPNNum, this.LPNCount);
        }

        private void RepLocation_AfterPrint(object sender, System.EventArgs e)
        {
            //记录打印张数和人
            //int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(this.PrintedData[0].Header.BILL_ID.ToString(), pageCount, "打印箱贴标签", GlobeSettings.LoginedUser.UserName); 
        }
    }
}
