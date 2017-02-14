using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.XtraEditors.Repository;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 任务优先级
    /// </summary>
    public partial class FrmTaskLevel : DevExpress.XtraEditors.XtraForm
    {
        #region 常量
        private const string TASK_GROUP_ID = "114";
        #endregion

        #region 构造函数

        public FrmTaskLevel()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 任务优先级-
        /// </summary>
        /// <returns></returns>
        public  List<TaskLevelEntity> Select()
        {
            List<TaskLevelEntity> list = new List<TaskLevelEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("beginTime=").Append(beginTime).Append("&");
                //loStr.Append("endTime=").Append(endTime);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_SelectZLM);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSelectZLM bill = JsonConvert.DeserializeObject<JsonSelectZLM>(jsonQuery);
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
                foreach (JsonSelectZLMResult jbr in bill.result)
                {
                    TaskLevelEntity asnEntity = new TaskLevelEntity();
                    #region 0-10
                    asnEntity.DiffValue = Convert.ToInt32(jbr.diffValue);
                    asnEntity.TaskLevel = Convert.ToInt32(jbr.taskLevel);
                    asnEntity.TaskType = Convert.ToInt32(jbr.taskType);
                    asnEntity.TaskTypeDesc = jbr.taskTypeDesc;
                    asnEntity.ID = Convert.ToInt32(jbr.tId);
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.beginTime))
                            asnEntity.BeginTime = Convert.ToDateTime(jbr.beginTime);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.endTime))
                            asnEntity.EndTime = Convert.ToDateTime(jbr.endTime);
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


        #region 方法
        private void LoadData()
        {
            this.bindingSource1.DataSource = Select();
            List<BaseCodeEntity> list = GetItemList(TASK_GROUP_ID);
            this.bindingSource2.DataSource = list;
            RepositoryItemComboBox combo = this.gridColumn7.ColumnEdit as RepositoryItemComboBox;
            if (combo == null)
                return;
            combo.Items.Clear();
            for (int i = 1; i <= list.Count; i++)
            {
                combo.Items.Add(i * 1000);
            }
        }
        private void CreateTaskLevel()
        {
            List<BaseCodeEntity> codeList = this.bindingSource2.DataSource as List<BaseCodeEntity>;
            try
            {
                DateTime beginTime = ConvertUtil.ToDatetime(this.editBeginTime.EditValue);
                DateTime endTime = ConvertUtil.ToDatetime(this.editEndTime.EditValue);
                int diffValue = 0;
                if (beginTime > endTime)
                {
                    diffValue = ConvertUtil.ToInt(Math.Abs((endTime.AddDays(1) - beginTime).TotalMinutes));
                    endTime = endTime.AddDays(1);
                }
                else
                {
                    diffValue = ConvertUtil.ToInt(Math.Abs((beginTime - endTime).TotalMinutes));
                }
                if (beginTime == endTime)
                {
                    MsgBox.Warn("<开始时段>不能等于<结束时段>！");
                    return;
                }
                if (codeList == null || codeList.Count == 0)
                {
                    MsgBox.Warn("未找到可保存的数据！");
                    return;
                }
                if (codeList.Exists(u => string.IsNullOrEmpty(u.Level)))
                {
                    MsgBox.Warn("还有未选择优先级的任务，请全部选择优先级！");
                    return;
                }
                // 验证在同一时段内，不允许出现优先级相等的情况
                bool state = false;
                for (int i = 0; i < codeList.Count; i++)
                {
                    BaseCodeEntity entityI = codeList[i];
                    for (int j = 1; j < codeList.Count; j++)
                    {
                        BaseCodeEntity entityJ = codeList[j];
                        if (entityI.ItemValue != entityJ.ItemValue && entityI.Level == entityJ.Level)
                        {
                            state = true;
                            break;
                        }
                    }
                }
                if (state)
                {
                    MsgBox.Warn("在同一时段内，不允许出现优先级相等的情况!");
                    return;
                }
                // 在同一时段内，不允许出现两个相同的任务类型
                List<TaskLevelEntity> taskLevel = this.bindingSource1.DataSource as List<TaskLevelEntity>;
                state = taskLevel.Find(u => (beginTime.TimeOfDay < u.EndTime.TimeOfDay)) != null;
                if (state)
                {
                    MsgBox.Warn("该时段内已有相同的任务类型!");
                    return;
                }
                if (MsgBox.AskOK("是否确认保存?") != DialogResult.OK)
                    return;
                List<TaskLevelEntity> taskLevelList = new List<TaskLevelEntity>();
                foreach (BaseCodeEntity baseCode in codeList)
                {
                    TaskLevelEntity entity = new TaskLevelEntity()
                    {
                        TaskType = ConvertUtil.ToInt(baseCode.ItemValue),
                        TaskLevel = ConvertUtil.ToInt(baseCode.Level),
                        BeginTime = beginTime,
                        EndTime = endTime,
                        DiffValue = ConvertUtil.ToInt(Math.Abs((beginTime - endTime).TotalMinutes))
                    };
                    taskLevelList.Add(entity);
                }
                if (InsertFor(taskLevelList) > 0)
                {
                    this.LoadData();
                }
                else
                {
                    MsgBox.Warn("创建任务优先级失败!");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        private void UpdateTaskLevel()
        {
            //try
            //{
            //    TaskLevelEntity entity = this.gridView1.GetFocusedRow() as TaskLevelEntity;
            //    if (entity == null)
            //    {
            //        MsgBox.Warn("请选择一条记录!");
            //        return;
            //    }
            //    if (MsgBox.AskOK("是否确认修改当前选择的任务优先级?") != DialogResult.OK)
            //        return;
            //    object taskTypeObj = this.cboTaskType.EditValue;
            //    int taskLevel = ConvertUtil.ToInt(this.editTaskLevel.EditValue);
            //    DateTime beginTime = ConvertUtil.ToDatetime(this.editBeginTime.EditValue);
            //    DateTime endTime = ConvertUtil.ToDatetime(this.editEndTime.EditValue);
            //    if (taskTypeObj == null)
            //    {
            //        MsgBox.Warn("请选择任务类型!");
            //        return;
            //    }
            //    if (beginTime >= endTime)
            //    {
            //        MsgBox.Warn("<开始时段>不能大于或等于<结束时段>!");
            //        return;
            //    }
            //    entity.TaskType = ConvertUtil.ToInt(taskTypeObj);
            //    entity.TaskLevel = taskLevel;
            //    entity.BeginTime = beginTime;
            //    entity.EndTime = endTime;
            //    if (TaskLevelDal.Update(entity) > 0)
            //    {
            //        this.LoadData();
            //    }
            //    else
            //    {
            //        MsgBox.Warn("更新任务优先级失败!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err("错误:" + ex.Message);
            //}
        }
        private void DeleteTaskLevel()
        {
            //TaskLevelEntity entity = this.gridView1.GetFocusedRow() as TaskLevelEntity;
            //if (entity == null)
            //{
            //    MsgBox.Warn("请选择一条记录!");
            //    return;
            //}
            //if (MsgBox.AskOK("是否确认删除当前选择的任务优先级?") != DialogResult.OK)
            //    return;
            //if (TaskLevelDal.Delete(entity) > 0)
            //{
            //    this.LoadData();
            //}
            //else
            //{
            //    MsgBox.Warn("更新任务优先级失败!");
            //}
        }

        /// <summary>
        /// 任务池管理(新)--任务优先级管理--保存insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertZLM(TaskLevelEntity entity)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskType=").Append(entity.TaskType).Append("&");
                loStr.Append("taskLevel=").Append(entity.TaskLevel).Append("&");
                loStr.Append("beginTime=").Append(entity.BeginTime).Append("&");
                loStr.Append("endTime=").Append(entity.EndTime).Append("&");
                loStr.Append("diffValue=").Append(entity.DiffValue);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertZLM);
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

        public int InsertFor(List<TaskLevelEntity> list)
        {
            int result = 0;
            foreach (TaskLevelEntity entity in list)
            {
                if (InsertZLM(entity))
                    result++;
            }
            return result;
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

        public  bool DeleteZLM()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_DeleteZLM);
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

        private void ClearTaskLevel()
        {
            try
            {
                if (MsgBox.AskOK("清空数据后不可恢复，是否确认删除？") != DialogResult.OK)
                    return;
                DeleteZLM();
                Insert(ELogType.操作任务, GlobeSettings.LoginedUser.UserName, null, "清空任务优先级", "任务优先级管理");
                this.LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 按钮事件
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button == null || button.Tag == null)
                return;
            switch (button.Tag.ToString())
            {
                case "保存":
                    this.CreateTaskLevel();
                    break;
                case "新增":
                    //this.CreateTaskLevel();
                    break;
                case "修改":
                    //this.UpdateTaskLevel();
                    break;
                case "删除":
                    //this.DeleteTaskLevel();
                    break;
                case "清空":
                    this.ClearTaskLevel();
                    break;
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TaskLevelEntity entity = this.gridView1.GetFocusedRow() as TaskLevelEntity;
            if (entity == null)
                return;
            //this.cboTaskType.EditValue = entity.TaskType;
            //this.editTaskLevel.EditValue = entity.TaskLevel;
            this.editBeginTime.EditValue = entity.BeginTime;
            this.editEndTime.EditValue = entity.EndTime;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadData();
        }
        #endregion
    }
}
