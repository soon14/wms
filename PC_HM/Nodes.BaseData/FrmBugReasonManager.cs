using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmBugReasonManager : DevExpress.XtraEditors.XtraForm
    {
        //private BugReasonDal bugDal = null;
        public FrmBugReasonManager()
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

            LoadDataAndBindGrid();
        }

        ///<summary>
        ///基础管理（不合格原因-查询所有不合格原因）
        ///</summary>
        ///<returns></returns>
        public List<BusReasonEntity> GetAllNotHeGe()
        {
            List<BusReasonEntity> list = new List<BusReasonEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllNotHeGe);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllNotHeGe bill = JsonConvert.DeserializeObject<JsonGetAllNotHeGe>(jsonQuery);
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
                foreach (JsonGetAllNotHeGeResult jbr in bill.result)
                {
                    BusReasonEntity asnEntity = new BusReasonEntity();
                    #region 
                    asnEntity.BugCode = jbr.bugCode;
                    asnEntity.BugName = jbr.bugName;
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
                //bugDal = new BugReasonDal();
                List<BusReasonEntity> bugEntities = GetAllNotHeGe();
                BindGrid(bugEntities);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
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
                    DoCreate();
                    break;
                case "修改":
                    ShowEditForm();
                    break;
                case "删除":
                    DoDeleteSelected();
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
        /// 获得选中数据
        /// </summary>
        BusReasonEntity SelectedRow
        {
            get
            {
                return gridView1.GetFocusedRow() as BusReasonEntity;
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
        /// 新增
        /// </summary>
        private void DoCreate()
        {
            FrmBugReasonEdit frmEdit = new FrmBugReasonEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditForm()
        {
            BusReasonEntity editEntity = SelectedRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmBugReasonEdit frmEdit = new FrmBugReasonEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            BusReasonEntity newEntity = (BusReasonEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelected()
        {
            BusReasonEntity removeEntity = SelectedRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除“({0}){1}”吗？", removeEntity.BugCode, removeEntity.BugName)) == DialogResult.OK)
            {
                bool ret = DoDelete(removeEntity);
                if (ret)
                {
                    ReLoad();
                }
                //else 
                //    MsgBox.Warn("删除失败。");
            }
        }

        /// <summary>
        /// 基础管理（不合格原因-删除不合格原因）
        /// </summary>
        /// <param name="StockAreaCode"></param>
        /// <returns></returns>
        public bool DeleteUnit(string Code)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("bugCode=").Append(Code);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteUnitZJQ);
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

        public bool DoDelete(BusReasonEntity removeEntity)
        {
            bool result = false;
            try
            {
                result = DeleteUnit(removeEntity.BugCode);
                if (result)
                    RemoveRowFromGrid(removeEntity);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }

            return result;
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditForm();
        }

        public void BindGrid(List<BusReasonEntity> objs)
        {
            bindingSource1.DataSource = objs;
        }

        public void RemoveRowFromGrid(BusReasonEntity obj)
        {
            bindingSource1.Remove(obj);
        }
    }
}