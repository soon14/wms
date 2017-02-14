using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.Common
{
    public partial class FrmInputNumeral : XtraForm
    {
        #region 变量
        private decimal _decimalQty = 0.00m;
        private int _intQty = 0;
        private ENumeralType _type = ENumeralType.Decimal;
        #endregion

        #region 构造函数

        public FrmInputNumeral(ENumeralType type)
        {
            InitializeComponent();
            this._type = type;
        }

        public FrmInputNumeral(ENumeralType type, decimal qty)
            : this(type)
        {
            this._decimalQty = qty;
        }
        public FrmInputNumeral(ENumeralType type, int qty)
            : this(type)
        {
            this._intQty = qty;
        }

        #endregion

        #region 属性
        public decimal DecimalQty
        {
            get
            {
                return this._decimalQty;
            }
        }
        public int IntQty
        {
            get { return this._intQty; }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txtValue.Focus();
            if (this._type == ENumeralType.Decimal)
                this.txtValue.Text = this._decimalQty.ToString();
            else if (this._type == ENumeralType.Int32)
                this.txtValue.Text = this._intQty.ToString();
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string value = this.txtValue.Text.Trim();
            if (this._type == ENumeralType.Decimal && !ConvertUtil.IsDecimal(value))
            {
                MsgBox.Warn("请输入数字！");
                return;
            }
            else if (this._type == ENumeralType.Int32 && !ConvertUtil.IsInt(value))
            {
                MsgBox.Warn("请输入整数！");
                return;
            }
            if (MsgBox.AskOK("修改后不可更改，是否确认修改？") == DialogResult.OK)
            {
                if (this._type == ENumeralType.Decimal)
                    this._decimalQty = ConvertUtil.ToDecimal(value);
                else if (this._type == ENumeralType.Int32)
                    this._intQty = ConvertUtil.ToInt(value);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
    public enum ENumeralType : uint
    {
        Decimal = 0,
        Int32 = 10
    }
}
