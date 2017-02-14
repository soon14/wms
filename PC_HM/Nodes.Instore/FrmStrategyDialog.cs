using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.WMS.Inbound
{
    public partial class FrmStrategyDialog : DevExpress.XtraEditors.XtraForm
    {
        private AsnHeaderEntity asnHeaderEntity = null;
        public event EventHandler dataSourceChanged = null;
        private AsnDal asnDal = null;
        private CodeItemDal codeItemDal;
        private IBindDataSouce bindDataSouce;

        public FrmStrategyDialog()
        {
            InitializeComponent();
        }

        public FrmStrategyDialog(AsnHeaderEntity asnHeaderEntity)
            : this()
        {
            asnDal = new AsnDal();
            codeItemDal = new CodeItemDal();
            bindDataSouce = new BindLookUpEditDataSouce();
            this.asnHeaderEntity = asnHeaderEntity;
        }

        private void FrmStrategySelect_Load(object sender, EventArgs e)
        {
            bindDataSouce.BindCommonLookUpEdit(lookUpEditStrategy, SysCodeConstant.INBOUND_ASN_STRATEGY);
            lookUpEditStrategy.EditValue = asnHeaderEntity.InboundType;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        private bool Save()
        {
            if (!IsFieldValueValid()) return false;
            bool success = false;
            try
            {
                string status = asnDal.GetStatus(asnHeaderEntity.BillID);
                if (status != SysCodeConstant.ASN_STATUS_AWAIT_CHECK)
                {
                    MsgBox.Warn("该单据的状态已发生变化，只有等待验收状态的单据才能制定入库策略，请稍后重试。");
                    return false;
                }

                AsnHeaderEntity editEntity = prepareSave();
                int ret = asnDal.AsnUpdate(editEntity, GlobeSettings.LoginedUser.UserCode);
                if (ret == -2)
                    MsgBox.Warn("更新失败。");
                else
                {
                    success = true;
                    if (dataSourceChanged != null)
                    {
                        dataSourceChanged(editEntity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            return success;
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(lookUpEditStrategy.Text))
            {
                MsgBox.Warn("请选择入库策略类型。");
                return false;
            }

            return true;
        }

        public AsnHeaderEntity prepareSave()
        {
            AsnHeaderEntity editEntity = asnHeaderEntity;
            editEntity.InboundType = ConvertUtil.ToString(lookUpEditStrategy.EditValue);
            editEntity.InboundTypeName = lookUpEditStrategy.Text;
            return editEntity;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}