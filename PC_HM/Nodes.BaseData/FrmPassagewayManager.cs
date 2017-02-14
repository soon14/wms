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
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.C02.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmChannelManager : DevExpress.XtraEditors.XtraForm
    {
        //private ChannelDal channelDal = null;
        public FrmChannelManager()
        {
            InitializeComponent();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreatePassageway();
                    break;
                case "修改":
                    ShowEditPassageway();
                    break;
                case "删除":
                    DeleteSelectedPassageway();
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }

        /// <summary>
       /// 根据通道编号删除通道
       /// </summary>
       /// <param name="code"></param>
       /// <returns></returns>
        public bool DeleteChannel(int cod)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("chCode=").Append(cod);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteChannel);
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
        /// 删除
        /// </summary>
        private void DeleteSelectedPassageway()
        {
            ChannelEntity removeEntity = SelectedChannelRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除通道{0}吗？", removeEntity.Ch_Name)) == DialogResult.OK)
            {
                bool ret = DeleteChannel(removeEntity.Ch_Code);
                //if (ret==-1)
                //{
                //     MsgBox.Warn("不能删除，该通道有相关联的货位。");
                //}
                //else
                if(ret)
                    bindingSource1.Remove(removeEntity);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        private void ShowEditPassageway()
        {
            ChannelEntity editEntity = SelectedChannelRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmChannelEdit frmChannelEdit = new FrmChannelEdit(editEntity);
            frmChannelEdit.DataSourceChanged += OnEditChanage;
            frmChannelEdit.ShowDialog();
        }

        /// <summary>
        /// 获取选中数据
        /// </summary>
        ChannelEntity SelectedChannelRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as ChannelEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

         /// <summary>
       /// 获取所有通道数据
       /// </summary>
       /// <returns></returns>
        public List<ChannelEntity> GetAllChannel()
        {
            List<ChannelEntity> list = new List<ChannelEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllChannel);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllChannel bill = JsonConvert.DeserializeObject<JsonGetAllChannel>(jsonQuery);
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
                foreach (JsonGetAllChannelResult jbr in bill.result)
                {
                    ChannelEntity asnEntity = new ChannelEntity();
                    asnEntity.Bak_Ch_Code = Convert.ToInt32(jbr.bakChCode);
                    asnEntity.Bak_Ch_Name = jbr.bakChName;
                    asnEntity.Ch_Code = Convert.ToInt32(jbr.chCode);
                    asnEntity.Ch_Name = jbr.chName;
                    asnEntity.Is_Active = jbr.isActive;
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

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = GetAllChannel();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void DoCreatePassageway()
        {
            FrmChannelEdit frmEdit = new FrmChannelEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }
        private void OnCreateChanage(object sender, EventArgs e)
        {
            ReLoad();
            //FrmChannelEdit newEntity = (FrmChannelEdit)sender;
            //bindingSource1.Add(newEntity);
            //bindingSource1.ResetBindings(false);
        }
        private void OnEditChanage(object sender, EventArgs e)
        {
            //bindingSource1.ResetBindings(true);
            ReLoad();
        }

        private void FrmChannelManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            //channelDal = new ChannelDal();
            LoadDataAndBindGrid();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowEditPassageway();
        }
    }
}