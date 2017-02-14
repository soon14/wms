using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;


namespace Nodes.Common
{
    /// <summary>
    /// 选择任务
    /// </summary>
    public partial class FrmChooseTask : XtraForm
    {
        #region 变量
        private ETaskType _taskType = ETaskType.无;
        private string _warnMsg = null;
        #endregion

        #region 构造函数

        public FrmChooseTask()
        {
            InitializeComponent();
        }
        public FrmChooseTask(ETaskType taskType)
            : this()
        {
            this._taskType = taskType;
        }
        public FrmChooseTask(ETaskType taskType, string warnMsg)
            : this(taskType)
        {
            this._warnMsg = warnMsg;
        }

        #endregion

        #region 属性
        public List<TaskEntity> SelectedTasks
        {
            get
            {
                gvHeader.PostEditor();
                List<TaskEntity> list = new List<TaskEntity>();
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    TaskEntity header = gvHeader.GetRow(i) as TaskEntity;
                    if (header.HasChecked)
                    {
                        list.Add(header);
                    }
                }
                return list;
            }
        }
        #endregion

        /// <summary>
        /// 根据任务类型，获取指定任务列表
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public List<TaskEntity> GetTasksByType(ETaskType taskType)
        {
            List<TaskEntity> list = new List<TaskEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTasksByType);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetTasksByType bill = JsonConvert.DeserializeObject<JsonGetTasksByType>(jsonQuery);
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
                foreach (JsonGetTasksByTypeResult jbr in bill.result)
                {
                    TaskEntity asnEntity = new TaskEntity();
                    #region 0-10
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.TaskID = Convert.ToInt32(jbr.id);
                    asnEntity.TaskName = jbr.itemDesc;
                    asnEntity.Qty = Convert.ToInt32(jbr.qty);
                    asnEntity.TaskDesc = jbr.taskDesc;
                    asnEntity.TaskType = jbr.taskType;
                    asnEntity.UserCode = jbr.userCode;
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.confirmDate))
                            asnEntity.ConfirmDate = Convert.ToDateTime(jbr.confirmDate);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
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

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadCheckBoxImage();
            this.gridControl1.DataSource = GetTasksByType(this._taskType);
        }
        #endregion

        #region 事件

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this._warnMsg) && MsgBox.Warn(this._warnMsg) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region "选中与复选框"
        private void LoadCheckBoxImage()
        {
            gvHeader.Images = GridUtil.GetCheckBoxImages();
            colCheck.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(this.gvHeader, "HasChecked", MousePosition);
            }
        }

        private void OnViewCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "HasChecked") return;
            SOHeaderEntity selectedHeader = gvHeader.GetFocusedRow() as SOHeaderEntity;
            if (selectedHeader == null) return;

            selectedHeader.HasChecked = ConvertUtil.ToBool(e.Value);
            gvHeader.CloseEditor();
        }

        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<TaskEntity> _data = this.gridControl1.DataSource as List<TaskEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    gvHeader.SetRowCellValue(i, "HasChecked", flag);
                }
                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        #endregion
    }
}
