using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.C02.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmChannelEdit : DevExpress.XtraEditors.XtraForm
    {
        private ChannelEntity channelEntity = null;
        public event EventHandler DataSourceChanged = null;
        //private ChannelDal channelDal = null;
        private bool isNew = true;
        public FrmChannelEdit()
        {
            InitializeComponent();
        }
        public FrmChannelEdit(ChannelEntity channelEntity):this()
        {
            this.channelEntity = channelEntity;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
       /// 保存或更新通道表
       /// </summary>
       /// <param name="channel"></param>
       /// <param name="isNew"></param>
       /// <returns></returns>
        public bool SaveAddChannel(ChannelEntity channel, bool isNew, int isEdit, int bak_Code)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 
                loStr.Append("chCode=").Append(channel.Ch_Code).Append("&");
                loStr.Append("bakChCode=").Append(channel.Bak_Ch_Code).Append("&");
                loStr.Append("chName=").Append(channel.Ch_Name).Append("&");
                loStr.Append("isActive=").Append(channel.Is_Active).Append("&");
                loStr.Append("remark=").Append(channel.Remark).Append("&");
                loStr.Append("creator=").Append(channel.Creator);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddChannel);
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
        /// C02通道管理（更新通道信息）
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="isNew"></param>
        /// <param name="isEdit"></param>
        /// <param name="bak_Code"></param>
        /// <returns></returns>
        public bool SaveUpdateChannelInfo(ChannelEntity channel, bool isNew, int isEdit, int bak_Code)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("chCode=").Append(channel.Ch_Code).Append("&");
                loStr.Append("bakChCode=").Append(channel.Bak_Ch_Code).Append("&");
                loStr.Append("chName=").Append(channel.Ch_Name).Append("&");
                loStr.Append("isActive=").Append(channel.Is_Active).Append("&");
                loStr.Append("remark=").Append(channel.Remark).Append("&");
                loStr.Append("isEdit=").Append(isEdit);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateChannelInfo);
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
            if (ConvertUtil.ToInt(txtCode.Text)<=0)
            {
                MsgBox.Warn("通道编码应为大于0的整数。");
            }
            try
            {
                //是否要修改货位标记
                int isEdit = 0;
                int bak_Code = 0;
                if (isNew == false)
                {
                    //更改过启用状态
                    if (channelEntity.Is_Active != comboIsActive.Text)
                    {
                        isEdit = 1;
                    }
                    else if (comboIsActive.Text == "N")
                    {
                        isEdit = 2;
                        bak_Code = channelEntity.Bak_Ch_Code;
                    }
                }
                ChannelEntity editEntity = PrepareSave();
                bool ret;
                if (isNew)
                    ret = SaveAddChannel(editEntity, isNew, isEdit, bak_Code);
                else
                    ret = SaveUpdateChannelInfo(editEntity, isNew, isEdit, bak_Code);
                //string ret = channelDal.Save(editEntity, isNew, isEdit,bak_Code);
                //if (ret == "-1")
                //    MsgBox.Warn("通道编号或名称已存在，请改为其他的通道编号或名称。");
                //else if (ret == "-2")
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else if(ret=="-3")
                //    MsgBox.Warn("更新失败，通道名称已存在，请改为其他通道名称。");
                //else if (ret.Length > 5)
                //{
                //    MsgBox.Warn(ret);
                //}
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
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }
        public ChannelEntity PrepareSave()
        {
            ChannelEntity editEntity = channelEntity;
            if (editEntity == null) editEntity = new ChannelEntity();
            editEntity.Ch_Code = ConvertUtil.ToInt(txtCode.Text.Trim());
            editEntity.Ch_Name = PassagewayName.Text.Trim();
            editEntity.Is_Active = comboIsActive.Text;
            if (comboIsActive.Text=="Y")
            {
                editEntity.Bak_Ch_Code = 0;
                editEntity.CanName = PassagewayName.Text.Trim();
            }
            else
            {
                editEntity.CanName = comboIsSpare.Text;
                editEntity.Bak_Ch_Code = Convert.ToInt32(ConvertUtil.ToString(comboIsSpare.EditValue));
            }
            editEntity.Remark = txtRemark.Text.Trim();
            editEntity.Creator = GlobeSettings.LoginedUser.ToString();
            return editEntity;
        }
        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MsgBox.Warn("通道编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(PassagewayName.Text))
            {
                MsgBox.Warn("通道名称不能为空。");
                return false;
            }
            if (comboIsActive.Text == "N")
            {
                if (string.IsNullOrEmpty(ConvertUtil.ToString(comboIsSpare.EditValue)) || ConvertUtil.ToInt(comboIsSpare.EditValue) < 0)
                {
                    MsgBox.Warn("主通道为未启用状态，请选择备用通道。");
                    return false;
                }
            }
            return true;
        }
        private void FrmChannelEdit_Load(object sender, EventArgs e)
        {
            //channelDal = new ChannelDal();
            BindAllChannelName(channelEntity);
            if (channelEntity != null)
            {
                this.Text = "通道-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(channelEntity);
                isNew = false;
               
            }
            comboxNOrY();
        }
        private void ShowEditInfo(ChannelEntity channelEntity)
        {
            txtCode.Text = channelEntity.Ch_Code.ToString();
            PassagewayName.Text = channelEntity.Ch_Name;
            comboIsActive.EditValue = channelEntity.Is_Active;
            comboIsSpare.Text = channelEntity.Bak_Ch_Name;
            txtRemark.Text = channelEntity.Remark;
        }

        private void comboIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboxNOrY();
            //comboIsSpare.Text = comboIsSpare.Properties.ValueMember[0].ToString();
        }

         /// <summary>
       /// 获取通道编号和通道名称
       /// </summary>
       /// <returns></returns>
        public List<ChannelEntity> GetAllChannelName()
        {
             List<ChannelEntity> list = new List<ChannelEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllChannelName);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllChannelName bill = JsonConvert.DeserializeObject<JsonGetAllChannelName>(jsonQuery);
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
                foreach (JsonGetAllChannelNameResult jbr in bill.result)
                {
                    ChannelEntity asnEntity = new ChannelEntity();
                    asnEntity.Ch_Code = Convert.ToInt32(jbr.chCode);
                    asnEntity.Ch_Name = jbr.chName;
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

        private void BindAllChannelName(ChannelEntity channelEntity)
        {
            List<ChannelEntity> AllChannel = new List<ChannelEntity>();
            AllChannel = GetAllChannelName();
            if (channelEntity != null)
            {
                for (int i = 0; i < AllChannel.Count; i++)
                {
                    int cod = AllChannel[i].Ch_Code;
                    if (cod == channelEntity.Ch_Code)
                    {
                        AllChannel.RemoveAt(i);
                    }
                }
            }
            comboIsSpare.Properties.ValueMember = "Ch_Code";
            comboIsSpare.Properties.DisplayMember = "Ch_Name";
            comboIsSpare.Properties.DataSource = AllChannel;
        }
        private void comboxNOrY()
        {
            if (comboIsActive.Text == "N")
            {
                comboIsSpare.Enabled = true;
               
            }
            else
            {
                comboIsSpare.EditValue = -1;
                comboIsSpare.Enabled = false;
            }
        }
    }
}