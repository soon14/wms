using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;
using System.Data;
using System.Text.RegularExpressions;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmUserEdit : DevExpress.XtraEditors.XtraForm, IvUserEdit
    {
        #region 变量

       // private UserDal userDal = null;
        private UserEntity editUser = null;
        private PreUserEdit pUserEdit;
        public event EventHandler DataSourceChanged;
        List<WarehouseEntity> lstOrgs;
        private bool isCreateNew = true;

        #endregion

        #region 构造函数

        public FrmUserEdit()
        {
            InitializeComponent();

            pUserEdit = new PreUserEdit(this);
            BindOrganization();
        }
        public FrmUserEdit(UserEntity editUser)
            : this()
        {
            this.editUser = editUser;
        }

        #endregion

        ///<summary>
        ///查询所有仓库
        ///</summary>
        ///<returns></returns>
        public List<WarehouseEntity> GetAllWarehouse()
        {
            List<WarehouseEntity> list = new List<WarehouseEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllWarehouse);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllWarehouse bill = JsonConvert.DeserializeObject<JsonGetAllWarehouse>(jsonQuery);
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
                foreach (JsonGetAllWarehouseResult jbr in bill.result)
                {
                    WarehouseEntity asnEntity = new WarehouseEntity();
                    asnEntity.OrgCode = jbr.orgCode;
                    asnEntity.OrgName = jbr.orgName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
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

        private void BindOrganization()
        {
            //WarehouseDal warehouseDal = new WarehouseDal();
            lstOrgs = GetAllWarehouse();
            cbOrganization.Properties.DataSource = lstOrgs;

            //默认选中第一个
            if (lstOrgs.Count > 0)
                cbOrganization.EditValue = lstOrgs[0].OrgCode;
        }

        public void ShowDetail(UserEntity user)
        {
            txtCode.Text = user.UserCode;
            txtName.Text = user.UserName;

            comboIsActive.Text = user.IsActive;
            cbOrganization.EditValue = user.WarehouseCode;
            this.cboUserType.EditValue = user.UserType;
            this.cboUserAttri.EditValue = user.UserAttri;
            txtRemark.Text = user.Remark;
            this.txtMobilePhone.Text = user.MobilePhone;

            CheckMyRoles(user.UserCode);

            if (user.AllowEdit == "N")
            {
                this.lblMsgInfo.Text = "系统预定义用户，不允许删除与修改。";
                this.btnSave.Enabled = this.btnSaveClose.Enabled = false;
            }
        }

        /// <summary>
        /// 收货单据管理， baseCode信息查询(用于业务类型和单据状态筛选条件)
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetItemList(string groupCode)
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetItemList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBaseCodeInfo bill = JsonConvert.DeserializeObject<JsonBaseCodeInfo>(jsonQuery);
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
                foreach (JsonBaseCodeInfoResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
                    asnEntity.Remark = jbr.remark;
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
            LoadCheckBoxImage();

            //userDal = new UserDal();
            pUserEdit.GetRolesAndBindToGrid();
            // 资源类型
            List<BaseCodeEntity> list = GetItemList("119");
            this.cboUserType.Properties.DataSource = list;
            if (list != null && list.Count > 0)
                this.cboUserType.EditValue = list[0].ItemValue;
            // 人员属性
            List<BaseCodeEntity> attrList = GetItemList("121");
            this.cboUserAttri.Properties.DataSource = attrList;
            if (attrList != null && attrList.Count > 0)
                this.cboUserAttri.EditValue = attrList[0].ItemValue;
            if (editUser != null)
            {
                isCreateNew = false;
                ShowDetail(editUser);

                //修改时，隐藏保存并关闭按钮
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtCode.Enabled = false;
                this.Text = "修改用户信息";
            }
            else
            {
                //新增时 用户编号是自动生成，不需输入
                checkAutoIncrement.Checked = false;
                cbOrganization.EditValue = GlobeSettings.LoginedUser.WarehouseCode;
                cbOrganization.Enabled = false;
                layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void LoadCheckBoxImage()
        {
            gridView1.Images = GridUtil.GetCheckBoxImages();
            gridColumn1.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gridView1, "Checked", MousePosition);
            }
        }

        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<RoleEntity> _data = bindingSource1.DataSource as List<RoleEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                _data.ForEach(d => d.Checked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        public List<RoleEntity> myRolesList = null;
        private void CheckMyRoles(string userCode)
        {
            //RoleDal roleDal = new RoleDal();
            myRolesList = ListMyRoles(userCode);
            List<RoleEntity> allRoles = bindingSource1.DataSource as List<RoleEntity>;
            foreach (RoleEntity role in myRolesList)
            {
                var _m = allRoles.Find(m => m.RoleId == role.RoleId);
                if (_m != null)
                {
                    _m.Checked = true;
                    role.Deleted = 1;
                }
            }
        }

        public void BindRoleToGrid(List<RoleEntity> roles)
        {
            bindingSource1.DataSource = roles;
        }

        /// <summary>
        /// 根据库房编号获取用户编号最大值（自动生成用户编号）
        /// </summary>
        /// <param name="ctType"></param>
        /// <returns></returns>
        public string GetMaxUserCode(string wareHouseCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wareHouseCode=").Append(wareHouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetMaxUserCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetMaxUserCode bill = JsonConvert.DeserializeObject<JsonGetMaxUserCode>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].maxValues;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        private string GenerateUserCode()
        {
            string newUserCode = string.Empty;

            //string oldMaxUserCode = userDal.GetMaxUserCode();
            int wareHouseCode = Utils.ConvertUtil.ToInt(GlobeSettings.LoginedUser.WarehouseCode);
            string oldMaxUserCode = GetMaxUserCode(wareHouseCode.ToString());
            //if (oldMaxUserCode.Trim().Length != 8)    //兼容老员工号
            //{
            //    newUserCode = string.Format("{0:D4}0001", wareHouseCode);
            //}
            //else
            //{
            //int newCode = Utils.ConvertUtil.ToInt(oldMaxUserCode.Substring(4)) + 1;
            int newCode = Utils.ConvertUtil.ToInt(oldMaxUserCode) + 1;
            if (newCode <= 9999)
            {
                newUserCode = string.Format("{0:D4}{1:D4}", wareHouseCode, newCode);
            }
            else
            {
                newUserCode = string.Format("{0:D4}{1}", wareHouseCode, newCode);
            }
            //}
            return newUserCode;
        }

        bool IsFieldValueValid()
        {
            //if (txtCode.Text.Trim().Length == 0)
            //{
            //    txtCode.Focus();
            //    MsgBox.Warn("请填写用户帐号。");
            //    return false;
            //}

            if (txtName.Text.Trim().Length == 0)
            {
                txtName.Focus();
                MsgBox.Warn("请填写姓名。");
                return false;
            }

            if (cbOrganization.EditValue == null)
            {
                cbOrganization.Focus();
                MsgBox.Warn("请选择所属仓库。");
                return false;
            }

            return true;
        }

        public UserEntity PrepareSave()
        {
            UserEntity user = editUser;
            if (user == null)
            {
                user = new UserEntity();
                user.AllowEdit = "Y";
            }

            if (isCreateNew)
            {
                user.UserCode = GenerateUserCode();
            }
            else
            {
                user.UserCode = txtCode.Text.Trim();
            }

            user.UserName = txtName.Text.Trim();
            user.IsActive = comboIsActive.Text;
            user.UserType = this.cboUserType.EditValue == null ? "190" : this.cboUserType.EditValue.ToString();
            user.UserTypeStr = this.cboUserType.Text;
            user.UserAttri = this.cboUserAttri.EditValue == null ? "210" : this.cboUserAttri.EditValue.ToString();
            user.UserAttriStr = this.cboUserAttri.Text;
            user.UserPwd = SecurityUtil.MD5Encrypt(GlobeSettings.InitialPassword);

            user.WarehouseName = cbOrganization.Text;
            user.WarehouseCode = ConvertUtil.ToString(cbOrganization.EditValue);
            user.Remark = txtRemark.Text.Trim();
            user.LastUpdateDate = DateTime.Now;
            user.MobilePhone = this.txtMobilePhone.Text.Trim();
            return user;
        }

        public bool IsCreateNew()
        {
            return isCreateNew;
        }

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
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
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        /// <summary>
        /// 获取某个用户的角色信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<RoleEntity> ListMyRoles(string userCode)
        {
            List<RoleEntity> list = new List<RoleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListMyRoles);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListMyRoles bill = JsonConvert.DeserializeObject<JsonListMyRoles>(jsonQuery);
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
                foreach (JsonListMyRolesResult jbr in bill.result)
                {
                    RoleEntity asnEntity = new RoleEntity();
                    asnEntity.RoleId = Convert.ToInt32(jbr.roleId);
                    asnEntity.RoleName = jbr.roleName;   
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

        bool Save()
        {
            if (!IsFieldValueValid()) return false;

            //先保存到数据库
            bool success = false;
            try
            {
                List<RoleEntity> allRoles = bindingSource1.DataSource as List<RoleEntity>;
                List<RoleEntity> myRoles = allRoles.Where(m => m.Checked == true).ToList();
                List<RoleEntity> oldRoles = editUser == null ? new List<RoleEntity>() : ListMyRoles(editUser.UserCode);

                CheckMyRoles(txtCode.Text);
                List<RoleEntity> myRoleDisplay =myRolesList.Where(m => m.Deleted == 0).ToList();
                myRoles.AddRange(myRoleDisplay);
                string newRolesStr = StringUtil.JoinBySign<RoleEntity>(myRoles, "RoleName");
                string oldRolesStr = StringUtil.JoinBySign<RoleEntity>(oldRoles, "RoleName");
                bool ret = pUserEdit.SaveUser(myRoles);
                Insert(ELogType.用户, GlobeSettings.LoginedUser.UserName, pUserEdit.User.UserCode, newRolesStr, isCreateNew ? "新增用户" : "修改用户信息", oldRolesStr);
                //if (ret == -1)
                //{
                //    txtCode.SelectAll();
                //    txtCode.Focus();
                //    MsgBox.Warn(string.Format("用户帐号[{0}]已存在，新增失败。", pUserEdit.User.UserCode));
                //}
                //else if (ret == -2)
                //{
                //    MsgBox.Warn("该用户可能已经被其他人删除，保存失败。");
                //}
                if (!ret)
                {
                    txtCode.SelectAll();
                    txtCode.Focus();
                }
                else
                {
                    success = true;
                    MsgBox.OK(string.Format("用户[{0}]编号为[{1}]新增或修改成功。", pUserEdit.User.UserName, pUserEdit.User.UserCode));
                    if (DataSourceChanged != null)
                        DataSourceChanged(pUserEdit.User, null);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return success;
        }

        void Continue()
        {
            txtName.Text = "";

            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtName.Focus();
            }
            else
            {
                txtRemark.Text = txtCode.Text = "";
                txtCode.Focus();
            }

            comboIsActive.SelectedIndex = 0;
            List<RoleEntity> roles = bindingSource1.DataSource as List<RoleEntity>;
            roles.ForEach(r => r.Checked = false);
            gridControl1.RefreshDataSource();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (this.editUser == null)
                {
                    Continue();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save()) this.DialogResult = DialogResult.OK;
        }
    }
}