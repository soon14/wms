using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmForkManager : DevExpress.XtraEditors.XtraForm
    {
       // private ForkDal forkDal = null;
        public FrmForkManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;

            //forkDal = new ForkDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有叉车
        ///</summary>
        ///<returns></returns>
        public List<ForkEntity> GetAllFork()
        {
            List<ForkEntity> list = new List<ForkEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("whCode=").Append(warehouseCode).Append("&");
                //loStr.Append("state=").Append(state);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllFork);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllFork bill = JsonConvert.DeserializeObject<JsonGetAllFork>(jsonQuery);
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
                foreach (JsonGetAllForkResult jbr in bill.result)
                {
                    ForkEntity asnEntity = new ForkEntity();
                    #region
                    asnEntity.ForkliftCode = jbr.forkCode;
                    asnEntity.ForkliftName = jbr.forkName;
                    #endregion
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
                bindingSource1.DataSource = GetAllFork();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateFork();
                    break;
                case "修改":
                    ShowEditFork();
                    break;
                case "删除":
                    DoDeleteSelectedUnit();
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
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        ForkEntity SelectedForkRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as ForkEntity;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreateFork()
        {
            FrmForkEdit frmEdit = new FrmForkEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditFork()
        {
            ForkEntity editEntity = SelectedForkRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmForkEdit frmEdit = new FrmForkEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            ForkEntity newEntity = (ForkEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 添加删除标记
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public bool DeleteForkInfo(string forkCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("forkCode=").Append(forkCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteForkInfo);
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
        private void DoDeleteSelectedUnit()
        {
            ForkEntity removeEntity = SelectedForkRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除叉车“({0}){1}”吗？", removeEntity.ForkliftCode, removeEntity.ForkliftName)) == DialogResult.OK)
            {
                bool ret = DeleteForkInfo(removeEntity.ForkliftCode);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                //else
                //    MsgBox.Warn("删除失败。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditFork();
        }
    }
}
