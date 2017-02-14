using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.Outstore
{
    public partial class FrmSODetails : DevExpress.XtraEditors.XtraForm
    {
        private int BillID;
        private SODal soDal = null;

        public FrmSODetails(int billID)
        {
            InitializeComponent();
            BillID = billID;
            soDal = new SODal();
        }        

        private void FrmSODetails_Load(object sender, EventArgs e)
        {
            try
            {
                List<SoGroupEntity> lstDetail = soDal.QueryBillDetailByID(BillID);
                gridDetails.DataSource = lstDetail;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }
    }
}