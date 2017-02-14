using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;

namespace Nodes.Outstore
{
    public partial class FrmSoWeightCanT : Form
    {
        DataTable ctCodeCanTDT;
        public FrmSoWeightCanT(DataTable dt)
        {
            InitializeComponent();
            this.ctCodeCanTDT = dt;
        }

        private void FrmSoWeightCanT_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ctCodeCanTDT;
        }
    }
}
