using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;

namespace Nodes.Instore
{
    public partial class FrmShowAsnInboundDetail : DevExpress.XtraEditors.XtraForm
    {
        private ASNDetailEntity asnDetailEntity = null;

        private AsnDal asnDal = null;

        public FrmShowAsnInboundDetail()
        {
            InitializeComponent();
        }

        public FrmShowAsnInboundDetail(ASNDetailEntity asnDetailEntity)
            : this()
        {
            asnDal = new AsnDal();

            this.asnDetailEntity = asnDetailEntity;
        }

        private void FrmShowAsnInboundDetail_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            //List<SequenceBill> sequenceBills = asnDal.GetAsnLineInboundInfo(asnDetailEntity.DetailId);
            //gridControl1.DataSource = sequenceBills;
        }
    }
}