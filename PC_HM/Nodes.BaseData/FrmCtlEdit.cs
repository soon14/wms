using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.Entities.OutBound;
//using Nodes.DBHelper;
using Nodes.UI;
//using Nodes.DBHelper.Outbound;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmCtlEdit : DevExpress.XtraEditors.XtraForm
    {
        private SoContainerLocation ctlEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //SOCtlDal soQuery = new SOCtlDal();
        public FrmCtlEdit()
        {
            InitializeComponent();
        }
        public FrmCtlEdit(SoContainerLocation ctlEntity)
            : this()
        {
            this.ctlEntity = ctlEntity;
        }
        /// <summary>
        /// 判断容器位是否为空
        /// </summary>
        /// <returns></returns>
        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(textEdit1.Text))
            {
                MsgBox.Warn("容器位编号不能为空。");
                return false;
            }
            if (lookUpEdit1.EditValue.Equals(""))
            {
                MsgBox.Warn("容器位类型不能为空。");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Continue()
        {
            if (checkBox1.Checked)
            {
                textEdit1.Text = AutoIncrement.NextCode(textEdit1.Text.Trim());
                if (textEdit1.Text=="A")
                {
                    textEdit1.Text = Convert.ToString(10);
                }
                textEdit1.Focus();
            }
            else
            {
                textEdit1.Text = "";
                textEdit1.Focus();
            }
        }
        /// <summary>
        /// 获取准备操作的实体
        /// </summary>
        /// <returns></returns>
        public SoContainerLocation PrepareSave()
        {
            SoContainerLocation editEntity = new SoContainerLocation();
            editEntity.CTLState = BaseCodeConstant.CTL_STATE_KONG_XIAN;
            editEntity.CTlType = lookUpEdit1.Text;
            editEntity.CTLName_Old = ctlEntity == null ? "" : ctlEntity.CTLName;
            editEntity.CTLName = textEdit1.Text.Trim();
            return editEntity;
        }

        /// <summary>
        /// 基础管理（容器位维护-添加容器位）
        /// </summary>
        /// <param name="sclEntity"></param>
        /// <param name="isNew"></param>
        /// <param name="whCode"></param>
        /// <param name="ctlTye"></param>
        /// <returns></returns>
        public bool SaveAddCTLInfo(SoContainerLocation sclEntity, bool isNew, string whCode, string ctlTye)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctlName=").Append(sclEntity.CTLName).Append("&");
                loStr.Append("ctlState=").Append(sclEntity.CTLState).Append("&");
                loStr.Append("whCode=").Append(whCode).Append("&");
                loStr.Append("ctlType=").Append(ctlTye);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddCTLInfo);
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

        /// <summary>
        /// 基础管理（容器位维护-编辑容器位）
        /// </summary>
        /// <param name="sclEntity"></param>
        /// <param name="isNew"></param>
        /// <param name="whCode"></param>
        /// <param name="ctlTye"></param>
        /// <returns></returns>
        public bool SaveUpdateCTLInfo(SoContainerLocation sclEntity, bool isNew, string whCode, string ctlTye)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctlName=").Append(sclEntity.CTLName).Append("&");
                loStr.Append("ctlNameOld=").Append(sclEntity.CTLName_Old).Append("&");
                loStr.Append("ctlType=").Append(ctlTye);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateCTLInfo);
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
            if (!IsFieldValueValid())
                return false;
            bool success = false;
            try
            {
                //BaseCodeEntity ctlType = this.lookUpEdit1.SelectedText as BaseCodeEntity;
                SoContainerLocation ctLEntity = PrepareSave();

                //int ret = soQuery.Save(ctLEntity, isNew, GlobeSettings.LoginedUser.WarehouseCode, this.lookUpEdit1.Text);

                //if (ret == -1)
                //{
                //    MsgBox.Warn("容器位编号已存在，请改为其他的容器位编号。");
                //    return false;
                //}
                //else if (ret == -2)
                //{
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //    return false;
                //}
                //else
                bool ret;
                if (isNew)
                    ret = SaveAddCTLInfo(ctLEntity, isNew, GlobeSettings.LoginedUser.WarehouseCode, this.lookUpEdit1.Text);
                else
                    ret = SaveUpdateCTLInfo(ctLEntity, isNew, GlobeSettings.LoginedUser.WarehouseCode, this.lookUpEdit1.Text);
                if(ret)
                {
                    success = true;
                    if (DataSourceChanged != null)
                    {
                        DataSourceChanged(ctLEntity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            return success;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (ctlEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void FrmCtlEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.lookUpEdit1.Properties.DataSource = GetItemList("124");
                BaseCodeEntity ctlType = this.lookUpEdit1.EditValue as BaseCodeEntity;
                if (ctlEntity != null)
                {
                    isNew = false;
                    //simpleButton2.Visible = false;
                    textEdit1.Text = ctlEntity.CTLName;
                    lookUpEdit1.Text = ctlEntity.CTlType;
                    //lookUpEdit1.Visible = false;
                }
                //else
                //{
                //    //  获取当前的最大值
                //    textEdit1.Text = soQuery.GetMaxName(ctlType.ItemValue).ToString();
                //}
            }
            catch (Exception ex)
            {

                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 基础管理（容器位维护-getMaxName）
        /// </summary>
        /// <param name="ctlType"></param>
        /// <returns></returns>
        public long GetMaxName(string ctlType)
        {
            long result = -1;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctlType=").Append(ctlType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetMaxName);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return result;
                }
                #endregion

                #region 正常错误处理

                JsonGetMaxName bill = JsonConvert.DeserializeObject<JsonGetMaxName>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return result;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return result;
                }
                #endregion

                #region 赋值数据
                foreach (JsonGetMaxNameResult jbr in bill.result)
                {
                    result = Convert.ToInt64(jbr.maxName);
                }
                return result;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return result;
        }

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            if (this.isNew == true)
            {
                BaseCodeEntity ctlType = this.lookUpEdit1.EditValue as BaseCodeEntity;
                textEdit1.Text = GetMaxName(ctlType.ItemValue).ToString();
            }

        }

    }
}