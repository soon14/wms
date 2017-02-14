using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using Nodes.Sinolube.TaobaoWMS.Entity;
using DevExpress.XtraReports.UI;

namespace Nodes.Sinolube.TaobaoWMS.Client.Rpt
{
    public partial class RptExpress : DevExpress.XtraReports.UI.XtraReport
    {
        private string Child_OrderID ;
        public RptExpress(string _orderID)
        {
            
            InitializeComponent();
            Child_OrderID = _orderID;
        }

        private void RptExpress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //lblCity1.Text = "北京";
            //lblName1.Text = "长城润滑油旗舰店";
            //lblAddress1.Text = "【重要提示】：收货时务必先开箱验货，无误后再签收。";
            //lblTel1.Text = "400-810-9886";
            //lblZip1.Text = "100085";

            if (Child_OrderID.Equals("-1"))
            {
                lblSmallID.Text = "8";
                lblPici.Text = "第328批";
                lblCity2.Text = "模板省份";
                lblName2.Text = "收货人名";
                lblAddress2.Text = "北京市中关村南大街31号神舟大厦705室705室705室705室705室705室705室(100081)";
                lblTel2.Text = "010-51652232  13888888888";
                return;
            }
            string orderID = Common.Order.GetOrderID(Child_OrderID);
            OrderInfo oi = new Dal.OrderInfoDal().GetOrderInfoByID(orderID);
            OrderInPick oip = new Dal.OrderInPickDal().GetOrderInPickByOrderID(Child_OrderID);
            if (oip != null)
            {
                lblSmallID.Text = oip.SmallID.ToString();
                lblPici.Text = "第" + oip.PickNO.Split('-')[1].TrimStart('0') + "批";
            }
            else
            {
                lblSmallID.Visible = false;
                lblPici.Visible = false;
            }
            lblCity2.Text = oi.ConsigneeProvinces;
            lblName2.Text = oi.Consignee;
            lblAddress2.Text = oi.BuyerAddress;
            lblTel2.Text = oi.Telephone + "  " + oi.Mobile;
        }

    }
}
