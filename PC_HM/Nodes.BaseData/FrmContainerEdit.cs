using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;


namespace Nodes.BaseData
{
    public partial class FrmContainerEdit : DevExpress.XtraEditors.XtraForm
    {
        private ContainerEntity containerEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private ContainerDal containerDal = null;

        public FrmContainerEdit()
        {
            InitializeComponent();
        }

        public FrmContainerEdit(ContainerEntity containerEntity)
            : this()
        {
            this.containerEntity = containerEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //containerDal = new ContainerDal();
            BindingCombox();

            if (containerEntity != null)
            {
                this.Text = "容器-修改";
                this.listContainerType.Enabled = false;
                this.checkMultIncrement.Enabled = false;
                this.checkSetBeginNum.Enabled = false;
                txtCode.Enabled = false;
                txtName.Enabled = false;
                layoutControlItemCtCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItemCtName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ShowEditInfo(containerEntity);
                isNew = false;
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

        #region 自定义方法
        private void BindingCombox()
        {
            listContainerType.Properties.DataSource = GetItemList(BaseCodeConstant.CONTAINER_TYPE);
        }

        private void ShowEditInfo(ContainerEntity containerEntity)
        {
            txtCode.Text = containerEntity.ContainerCode;
            txtName.Text = containerEntity.ContainerName;
            listContainerType.EditValue = containerEntity.ContainerType;
            txtWeight.Text = ConvertUtil.ToString(containerEntity.ContainerWeight);
        }

        private bool IsFieldValueValid()
        {
            if (listContainerType.EditValue == null)
            {
                MsgBox.Warn("容器类别不能为空。");
                return false;
            }
            if (this.checkSetBeginNum.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    MsgBox.Warn("未设置正确的起始值");
                    return false;
                }
            }
            int ctNum = Nodes.Utils.ConvertUtil.ToInt(txtNum.Text);
            decimal ctWeight = ConvertUtil.ToDecimal(txtWeight.Text);
            if (ctNum <= 0 || ctWeight <= 0)
            {
                MsgBox.Warn("请输入正确的数值。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 批量新增容器/修改容器信息
        /// </summary>
        public bool SaveAddContainerInfo(ContainerEntity Container, bool isNew, int ctNum)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(Container.ContainerCode).Append("&");
                loStr.Append("ctName=").Append(Container.ContainerName).Append("&");
                loStr.Append("ctType=").Append(Container.ContainerType).Append("&");
                loStr.Append("ctWeight=").Append(Container.ContainerWeight).Append("&");
                loStr.Append("ctNum=").Append(ctNum).Append("&");
                loStr.Append("whCode=").Append(Container.WarehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddContainerInfo);
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

        private int AddInfo(ContainerEntity Container, bool isNew, int ctNum)
        {
            int sucNum = 0;
            long ctCode = long.Parse(Container.ContainerCode);
            long ctName = long.Parse(Container.ContainerName);
            //批量增加容器
            //for (int i = 0; i < ctNum; i++)
            //{
            //    if (SaveAddContainerInfo(Container, isNew, ctNum))
            //        sucNum++;
            //}

            if (SaveAddContainerInfo(Container, isNew, ctNum))
                return ctNum;

            return sucNum;
        }

        /// <summary>
        /// 基础管理（容器信息-更改容器删除状态）
        /// </summary>
        /// <param name="Container"></param>
        /// <param name="isNew"></param>
        /// <param name="ctNum"></param>
        /// <returns></returns>
        public bool SaveUpdateContainerInfo(ContainerEntity Container, bool isNew, int ctNum)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(Container.ContainerCode).Append("&");
                loStr.Append("ctWeight=").Append(Container.ContainerWeight);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateContainerInfo);
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
            DevExpress.Utils.WaitDialogForm waitForm = new DevExpress.Utils.WaitDialogForm("正在更新容器数据...", "请稍候...");
            try
            {
                ContainerEntity editEntity = PrepareSave();
                //int ret = containerDal.SaveContainerCode(editEntity, isNew, Utils.ConvertUtil.ToInt(txtNum.Text));
                //if (ret == -1)
                //    MsgBox.Warn("容器编号已存在，请改为其他的容器编号。");
                //else if (ret == 0)
                //    MsgBox.Warn("未更新任何容器。");
                //else
                if (isNew)
                {
                    int ret = AddInfo(editEntity, isNew, Utils.ConvertUtil.ToInt(txtNum.Text));
                    if (ret == 0)
                    {
                        MsgBox.Warn("未更新任何容器。");
                    }
                    else
                    {
                        success = true;
                        if (DataSourceChanged != null)
                        {
                            DataSourceChanged(editEntity, null);
                        }
                        MsgBox.OK(string.Format("已更新{0}个容器。", ret));
                    }
                }
                else
                {
                    bool ret = SaveUpdateContainerInfo(editEntity, isNew, Utils.ConvertUtil.ToInt(txtNum.Text));
                    success = true;
                    if (DataSourceChanged != null)
                    {
                        DataSourceChanged(editEntity, null);
                    }
                    MsgBox.OK(string.Format("已更新{0}个容器。", 1));
                }
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                MsgBox.Err(ex.Message);
            }
            return success;
        }
        #endregion

        /// <summary>
        /// 获取容器最大值（批量新增）
        /// </summary>
        /// <param name="ctType"></param>
        /// <returns></returns>
        public string GetMaxContainerCode(string ctType)
        {
            string ret = string.Empty;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctType=").Append(ctType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetMaxContainerCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return ret;
                }
                #endregion

                #region 正常错误处理

                JsonGetMaxContainerCode bill = JsonConvert.DeserializeObject<JsonGetMaxContainerCode>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return ret;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return ret;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].maxCtCode;

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return ret;
        }

        public ContainerEntity PrepareSave()
        {
            ContainerEntity editEntity = containerEntity;
            if (editEntity == null)
            {
                editEntity = new ContainerEntity();
                editEntity.WarehouseCode = GlobeSettings.LoginedUser.WarehouseCode;
            }

            editEntity.ContainerType = ConvertUtil.ToString(listContainerType.EditValue);
            editEntity.ContainerTypeDesc = listContainerType.Text;

            if (isNew)
            {
                string newCode = string.Empty;
                string wareHouseCode = string.Format("{0:D4}", Utils.ConvertUtil.ToInt(GlobeSettings.LoginedUser.WarehouseCode));
                if (checkSetBeginNum.Checked == false)
                {
                    string maxCtCode = GetMaxContainerCode(editEntity.ContainerType);
                    if (string.IsNullOrEmpty(maxCtCode))
                    { //库房中还没有该类型的容器编号
                        newCode = GenerateContainerCode(editEntity.ContainerType, wareHouseCode, this.txtName.Text.Trim());
                    }
                    else
                    {
                        newCode = Utils.ConvertUtil.ToString((long.Parse(maxCtCode) + 1));
                    }
                }
                else
                {
                    newCode = GenerateContainerCode(editEntity.ContainerType, wareHouseCode, this.txtName.Text.Trim());
                }
                editEntity.ContainerCode = newCode;
                editEntity.ContainerName = editEntity.ContainerCode.Substring(editEntity.ContainerCode.Length - 5, 5);
            }
            else
            {
                editEntity.ContainerCode = txtCode.Text.Trim();
                editEntity.ContainerName = txtName.Text.Trim();
            }

            editEntity.ContainerWeight = ConvertUtil.ToDecimal(txtWeight.Text.Trim());

            return editEntity;
        }


        private string GenerateContainerCode(string ctType, string whCode, string ctName)
        {
            string ctCode = string.Empty;
            switch (ctType)
            {
                case "51":    //物流箱
                    ctCode = string.Concat("2", whCode, ctName);
                    break;
                case "52":    //笼车
                    ctCode = string.Concat("4", whCode, ctName);
                    break;
                case "53":    //拣货车
                    ctCode = string.Concat("8", whCode, ctName);
                    break;
                case "54":    //地牛
                    ctCode = string.Concat("5", whCode, ctName);
                    break;
                default:      //托盘
                    ctCode = string.Concat("3", whCode, ctName);
                    break;
            }
            return ctCode;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            this.DialogResult = DialogResult.OK;
        }

        private void checkMultIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (layoutControlItemAutoNum.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                layoutControlItemAutoNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemAutoNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void checkSetBeginNum_CheckedChanged(object sender, EventArgs e)
        {
            if (layoutControlItemCtName.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                layoutControlItemCtName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                checkSetBeginNum.Enabled = true;
            }
            else
            {
                layoutControlItemCtName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
    }
}
