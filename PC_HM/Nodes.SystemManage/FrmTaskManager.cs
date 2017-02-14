using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using System.Data;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 任务池管理（旧版）
    /// </summary>
    public partial class FrmTaskManager : DevExpress.XtraEditors.XtraForm
    {
        #region 变量

        private TaskDal taskDal = new TaskDal();

        #endregion

        #region 构造函数

        public FrmTaskManager()
        {
            InitializeComponent();
        }

        #endregion

        #region 属性
        /// <summary>
        /// 获得选中数据
        /// </summary>
        TaskEntity SelectedTaskRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as TaskEntity;
            }
        }
        #endregion

        #region 方法
        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = TaskDal.GetCurrentTask();
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }
        private void CloseTask()
        {
            try
            {
                TaskEntity entity = SelectedTaskRow;
                if (entity == null)
                {
                    MsgBox.Warn("请选择一条任务。");
                    return;
                }
                if (entity.TaskType != "143")
                {
                    MsgBox.Warn("现阶段只支持关闭下架任务。后续功能开发中。。。");
                    return;
                }
                //List<SODetailEntity> list = this.CalcNonFullDetails(entity.TaskID);
                //DialogResult result = DialogResult.OK;
                //if (list.Count > 0)
                //{
                //    using (FrmNonFullDetails frmNonFullDetails = new FrmNonFullDetails(list))
                //    {
                //        result = frmNonFullDetails.ShowDialog();
                //    }
                //}
                //if (result != DialogResult.OK)
                //    return;
                FrmTempAuthorize frmAuthorize = new FrmTempAuthorize("管理员");
                if (frmAuthorize.ShowDialog() == DialogResult.OK)
                {
                    if (this.taskDal.CloseTask(entity) > 0)
                        ReLoad();
                    LogDal.Insert(ELogType.操作任务, frmAuthorize.AuthUserCode, ConvertUtil.ToString(entity.TaskID), "关闭任务", "任务池管理");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
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
        /// 编辑
        /// </summary>
        private void ShowEditZone()
        {
            TaskEntity editEntity = SelectedTaskRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            if (editEntity.TaskType != "143")
            {
                MsgBox.Warn("先阶段只允许更改拣货任务，其他功能后续更新。");
                return;
            }
            #region
            //List<TaskEntity> list = new List<TaskEntity>();
            //foreach(TaskEntity entity in bindingSource1.DataSource as List<TaskEntity>)
            //{
            //    if(editEntity.UserCode==entity.UserCode)
            //    {
            //        list.Add(entity);
            //    }
            //}
            #endregion
            string resultMsg = this.taskDal.TaskState(editEntity.TaskType, editEntity.TaskID);
            if (resultMsg != "Y")
            {
                MsgBox.Warn(resultMsg);
                return;
            }
            FrmTaskChange frm = new FrmTaskChange(editEntity.TaskType, editEntity);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataAndBindGrid();
            }
        }
        /// <summary>
        /// 计算指定任务中是否存在不完整的套餐
        /// </summary>
        /// <param name="taskID">任务 ID</param>
        /// <returns></returns>
        private List<SODetailEntity> CalcNonFullDetails(int taskID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();

            List<SODetailEntity> details = this.taskDal.GetDetailsByTaskID(taskID);
            List<SODetailEntity> findList = details.FindAll(u => !string.IsNullOrEmpty(u.CombMaterial));
            if (findList.Count > 0) // 验证套餐完整性
            {
                // 根据套餐分组 key=CombMaterial_RowNo
                Dictionary<string, List<SODetailEntity>> dicPackage = new Dictionary<string, List<SODetailEntity>>();
                foreach (SODetailEntity detail in findList)
                {
                    string key = string.Format("{0}_{1}", detail.CombMaterial, detail.RowNO);
                    if (dicPackage.ContainsKey(key))
                    {
                        dicPackage[key].Add(detail);
                    }
                    else
                    {
                        dicPackage.Add(key,
                            new List<SODetailEntity>(new SODetailEntity[] { detail }));
                    }
                }
                // 计算每个套餐中最小的成套量与应返回数,
                foreach (string key in dicPackage.Keys)
                {
                    List<SODetailEntity> array = dicPackage[key];
                    SODetailEntity detail = null;
                    // 从该套餐中找中成套数最小的
                    foreach (SODetailEntity item in array)
                    {
                        if (detail == null)
                        {
                            detail = item;
                            continue;
                        }
                        SODetailEntity minPick = array.Find(
                            u => u.PickQty == 0 ||
                               u.PickQty / u.SuitNum < detail.PickQty / detail.SuitNum);
                        if (minPick == null)
                            break;
                        detail = minPick;
                    }
                    if (detail == null)
                        continue;
                    decimal packageCount = detail.PickQty < detail.SuitNum ?
                        0 : detail.PickQty / detail.SuitNum;// 最小成套数
                    array.ForEach(u =>
                    {
                        u.ReturnQty = u.PickQty - packageCount * u.SuitNum;
                    });
                    list.AddRange(array.FindAll(u => u.ReturnQty > 0));
                }
            }
            return list;
        }
        private void ListLocation()
        {
            if (SelectedTaskRow != null)
            {
                //gridControl2.DataSource = null;
                DataTable dt = this.taskDal.GetTaskDetail(SelectedTaskRow.TaskID);
                gridView2.Columns.Clear();
                gridControl2.DataSource = dt;
                if (gridView2.RowCount > 0)
                {
                    gridView2.Columns[0].GroupIndex = 0;
                }
            }
            else
                gridControl2.DataSource = null;
        }
        #endregion

        #region 事件
        private void FrmTaskManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            LoadDataAndBindGrid();
        }
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag == null)
                return;
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "修改":
                    ShowEditZone();
                    break;
                case "关闭任务":
                    CloseTask();
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }
        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditZone();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ListLocation();
        }
        #endregion
    }
}