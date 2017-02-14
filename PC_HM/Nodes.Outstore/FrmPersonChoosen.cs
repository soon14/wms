using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.Shares;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmPersonChoosen : DevExpress.XtraEditors.XtraForm
    {
        private List<UserEntity> List = null;
        private List<UserEntity> List2 = null;
        //private UserDal userDal = new UserDal();
        public FrmPersonChoosen()
        {
            InitializeComponent();
        }

        public UserEntity SelectedPersonnel
        {
            get
            {
                return this.searchLookUpEdit1.EditValue as UserEntity;
            }
        }
        public List<UserEntity> _selectedPersonnelList = null;
        public List<UserEntity> SelectedPersonnelList
        {
            get
            {
                if (this._selectedPersonnelList == null)
                {
                    this._selectedPersonnelList = new List<UserEntity>();
                    //list.Add(SelectedPersonnel);

                    gridView1.PostEditor();
                    //添加助理
                    for (int i = 0; i < this.gridView1.DataRowCount; i++)
                    {
                        UserEntity user = gridView1.GetRow(i) as UserEntity;
                        if (user != null && user.HasChecked)
                        {
                            this._selectedPersonnelList.Add(user);
                        }
                    }
                }
                return this._selectedPersonnelList;
            }
        }

        /// <summary>
        /// 列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleAndWarehouseCode(string warehouseCode, string roleName)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByRoleAndWarehouseCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByRoleAndWarehouseCode bill = JsonConvert.DeserializeObject<JsonListUsersByRoleAndWarehouseCode>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListUsersByRoleAndWarehouseCodeResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.ROLE_ID = Convert.ToInt32(jbr.roleId);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.UserPwd = jbr.pwd;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.updateDate))
                        //    asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
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

        private void FrmPersonChoosen_Load(object sender, EventArgs e)
        {
            List = ListUsersByRoleAndWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode, "司机助理");

            bindingSource1.DataSource = List;
            List2 = ListUsersByRoleAndWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode, "司机");
            searchLookUpEdit1.Properties.DataSource = List2;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.searchLookUpEdit1.Text))
            {
                MsgBox.Warn("请选择司机！");
                return;
            }
            if (this.SelectedPersonnelList.Contains(this.SelectedPersonnel))
            {
                MsgBox.Warn("同一个人不能即是司机又是助理！");
                return;
            }
            this.SelectedPersonnelList.Add(SelectedPersonnel);
            this.DialogResult = DialogResult.OK;
        }
    }
}