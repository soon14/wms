using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.DBHelper;

namespace Nodes.WMS.Inbound
{
    public partial class FrmShowSapAsn : DevExpress.XtraEditors.XtraForm
    {
        private AsnDal asnDal = new AsnDal();
        private string billNO;

        public FrmShowSapAsn(string billNO)
        {
            InitializeComponent();
            this.billNO = billNO;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                //显示表头信息
                DataTable dataBill = asnDal.GetSapAsnBill(this.billNO);
                gridControl1.DataSource = dataBill;

                //显示明细信息
                gridControl2.DataSource = asnDal.GetSapAsnDetail(this.billNO);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}