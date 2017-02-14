using System;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmDriverCardEdit : DevExpress.XtraEditors.XtraForm
    {
        private DriverCardEntity cardStateEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private DriverCardDal cardStateDal = null;

        public FrmDriverCardEdit()
        {
            InitializeComponent();
        }

        public FrmDriverCardEdit(DriverCardEntity cardStateEntity)
            : this()
        {
            this.cardStateEntity = cardStateEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //cardStateDal = new DriverCardDal();

            if (cardStateEntity != null)
            {
                this.Text = "送货牌-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(cardStateEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(DriverCardEntity cardStateEntity)
        {
            txtCode.Text = cardStateEntity.CardNO;
        }

        private void Continue()
        {
            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtCode.Focus();
            }
            else
            {
                txtCode.Text = "";
                txtCode.Focus();
            }
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MsgBox.Warn("送货牌编号不能为空。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑送货牌
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveAddCardState(DriverCardEntity cardState, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("cardNo=").Append(cardState.CardNO).Append("&");
                loStr.Append("cardState=").Append(cardState.CardState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddCardState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        private bool Save()
        {
            if (!IsFieldValueValid()) return false;
            bool success = false;
            try
            {
                DriverCardEntity editEntity = PrepareSave();
                //int ret = cardStateDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("送货牌编号已存在，请改为其他的送货牌编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                if (SaveAddCardState(editEntity,isNew))
                {
                    success = true;
                    if (DataSourceChanged != null)
                    {
                        DataSourceChanged(editEntity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            return success;
        }
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public DriverCardEntity PrepareSave()
        {
            DriverCardEntity editEntity = cardStateEntity;
            if (editEntity == null)
            {
                editEntity = new DriverCardEntity();
                editEntity.CardState = BaseCodeConstant.CARD_STATE_KONG_XIAN;
            }

            editEntity.CardNO = txtCode.Text.Trim();

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (cardStateEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}
