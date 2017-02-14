using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.Utils;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Common
{
    /// <summary>
    /// 选择人员
    /// </summary>
    public partial class FrmChoosePersonnel : XtraForm
    {
        #region 变量
        private UserDal _userDal = new UserDal();
        private string _roleName = null;    // 角色名称
        private string _warnMsg = null;     // 警告提示消息
        private bool _multiRow = false;     // 是否是多选
        private bool? _isOnLine = null;     // 是否在线
        private List<UserEntity> List = null;
        #endregion

        #region 构造函数

        public FrmChoosePersonnel()
        {
            InitializeComponent();
        }
        public FrmChoosePersonnel(bool multiRow)
            : this()
        {
            this._multiRow = multiRow;
        }
        /// <summary>
        /// 只显示有指定角色的人员
        /// </summary>
        /// <param name="roleID"></param>
        public FrmChoosePersonnel(string roleName)
            : this()
        {
            this._roleName = roleName;
        }
        /// <summary>
        /// 只显示有指定角色的人员（多选）
        /// </summary>
        /// <param name="roleID"></param>
        public FrmChoosePersonnel(bool multiRow, string roleName)
            : this(multiRow)
        {
            this._roleName = roleName;
        }
        public FrmChoosePersonnel(bool multiRow, string roleName, bool isOnLine)
            : this(multiRow, roleName)
        {
            this._isOnLine = isOnLine;
        }
        /// <summary>
        /// 确认选择时弹出的警示提示消息
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="warnMsg"></param>
        public FrmChoosePersonnel(string roleID, string warnMsg)
            : this(roleID)
        {
            this._warnMsg = warnMsg;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 选择的人员
        /// </summary>
        public UserEntity SelectedPersonnel
        {
            get
            {
                return this.cboPersonnel.EditValue as UserEntity;
            }
        }
        public List<UserEntity> SelectedPersonnelList
        {
            get
            {
                List<UserEntity> list = new List<UserEntity>();
                if (this._multiRow)
                {
                    gridView1.PostEditor();
                    //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                    for (int i = 0; i < this.gridView1.DataRowCount; i++)
                    {
                        UserEntity user = gridView1.GetRow(i) as UserEntity;
                        if (user != null && user.HasChecked)
                        {
                            list.Add(user);
                        }
                    }
                }
                else
                {
                    if (this.cboPersonnel.EditValue is UserEntity)
                    {
                        list.Add(this.cboPersonnel.EditValue as UserEntity);
                    }
                }
                return list;
            }
        }
        #endregion

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

        /// <summary>
        /// 车次信息---查询所有人员
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> ListUsers()
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_ListUsers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsers bill = JsonConvert.DeserializeObject<JsonListUsers>(jsonQuery);
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
                foreach (JsonListUsersResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.UserPwd = jbr.pwd;
                    asnEntity.UserTypeStr = jbr.itemDesc;
                    asnEntity.UserAttri = jbr.userAttri;
                    asnEntity.UserAttriStr = jbr.bcItemDesc;
                    asnEntity.UserType = jbr.userType;
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

        #region 方法
        private void LoadData()
        {
            if (!string.IsNullOrEmpty(this._roleName))
            {
                List = ListUsersByRoleAndWarehouseCode(
                    GlobeSettings.LoginedUser.WarehouseCode, this._roleName);
            }
            else
            {
                List = ListUsers();
            }
            if (this._isOnLine != null)
            {
                string onLine = "Y";
                if (!(bool)this._isOnLine)
                    onLine = "N";
                List = List.FindAll(u => { return u.IsOnlineDesc == onLine; });
            }
            this.bindingSource1.DataSource = List;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.cboPersonnel.Visible = !(this.gridControl1.Visible = this._multiRow);
                if (this.cboPersonnel.Visible)
                {

                    this.cboPersonnel.Location = new Point(12, 74);
                    this.btnEnter.Location = new Point(142, 143);
                    this.btnCancel.Location = new Point(240, 143);
                    this.Size = new Size(360, 230);
                }
                else
                {
                    if (!String.IsNullOrEmpty(this._roleName))
                    {
                        labelControl1.Text = "请选择配送人员";
                    }
                    this.gridControl1.Location = new Point(12, 74);
                    this.gridControl1.Size = new Size(321, 201);
                    this.btnEnter.Location = new Point(142, 281);
                    this.btnCancel.Location = new Point(240, 281);
                    this.Size = new Size(360, 360);
                }
                this.LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 事件

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (this.SelectedPersonnelList.Count == 0)
            {
                MsgBox.Warn("请选择人员!");
                return;
            }
            DialogResult result = DialogResult.Cancel;
            if (string.IsNullOrEmpty(this._warnMsg))
            {
                result = DialogResult.OK;
            }
            else
            {
                result = MsgBox.AskOK(this._warnMsg);
            }
            if (result == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
