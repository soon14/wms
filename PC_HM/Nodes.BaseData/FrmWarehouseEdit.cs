using System;
using System.Collections.Generic;
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
    public partial class FrmWarehouseEdit : DevExpress.XtraEditors.XtraForm
    {
        private WarehouseEntity wareHouseEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
       // private WarehouseDal warehouseDal = null;
        //private OrganizationDal orgDal = null;

        public FrmWarehouseEdit()
        {
            InitializeComponent();
        }

        public FrmWarehouseEdit(WarehouseEntity warehouseEntity)
            : this()
        {
            this.wareHouseEntity = warehouseEntity;
        }

        ///<summary>
        ///枚举所有
        ///</summary>
        ///<returns></returns>
        public List<OrgEntity> GetAllOrganization()
        {
            List<OrgEntity> list = new List<OrgEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllOrganization);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllOrganization bill = JsonConvert.DeserializeObject<JsonGetAllOrganization>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonGetAllOrganizationResult jbr in bill.result)
                {
                    OrgEntity asnEntity = new OrgEntity();
                    asnEntity.OrgCode = jbr.orgCode;
                    asnEntity.OrgName = jbr.orgName;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //warehouseDal = new WarehouseDal();
            //orgDal = new OrganizationDal();
            List<OrgEntity> listArea = GetAllOrganization();
            listOrgs.Properties.DataSource = listArea;

            if (wareHouseEntity != null)
            {
                this.Text = "仓库-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(wareHouseEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(WarehouseEntity WarehouseEntity)
        {
            txtCode.Text = WarehouseEntity.WarehouseCode;
            txtName.Text = WarehouseEntity.WarehouseName;
            listOrgs.EditValue = WarehouseEntity.OrgCode;
        }

        private void Continue()
        {
            txtName.Text = "";

            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtName.Focus();
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
                MsgBox.Warn("仓库编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("仓库名称不能为空。");
                return false;
            }
            if (listOrgs.EditValue == null)
            {
                MsgBox.Warn("请选择所属区域。");
                return false;
            }
            return true;
        }

        public bool WarehouseAddAndUpdate(WarehouseEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whName=").Append(entity.WarehouseName).Append("&");
                loStr.Append("orgCode=").Append(entity.OrgCode).Append("&");
                loStr.Append("whCode=").Append(entity.WarehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_WarehouseAddAndUpdate);
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
                WarehouseEntity editEntity = PrepareSave();
                bool ret = WarehouseAddAndUpdate(editEntity, isNew); ;
                //int ret = warehouseDal.WarehouseAddAndUpdate(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("仓库编号或名称已存在，请改为其他的仓库编号或名称。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                if(ret)
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

        #region IvCommonEdit 成员

        public WarehouseEntity PrepareSave()
        {
            WarehouseEntity editEntity = wareHouseEntity;
            if (editEntity == null) editEntity = new WarehouseEntity();
            editEntity.WarehouseCode = txtCode.Text.Trim();
            editEntity.WarehouseName = txtName.Text.Trim();
            editEntity.OrgCode = ConvertUtil.ToString(listOrgs.EditValue);
            editEntity.OrgName = ConvertUtil.ToString(listOrgs.Text);
            return editEntity;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (wareHouseEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                listOrgs.EditValue = null;
                listOrgs.ClosePopup();
            }
        }
    }
}