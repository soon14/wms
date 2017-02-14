using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.UI;
using Nodes.Entities;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.Common;
using Nodes.Shares;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 任务池管理（新版）
    /// </summary>
    public partial class FrmTaskManagerNew : Form
    {
        #region 变量
        //private TaskDal taskDal = new TaskDal();
        #endregion

        #region 构造函数

        public FrmTaskManagerNew()
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
        /// 任务池管理--查询1
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public  List<TaskEntity> GetUserByTasks(DateTime beginTime, DateTime endTime)
        {
            List<TaskEntity> list = new List<TaskEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginTime=").Append(beginTime).Append("&");
                loStr.Append("endTime=").Append(endTime);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetUserByTasks);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetUserByTasks bill = JsonConvert.DeserializeObject<JsonGetUserByTasks>(jsonQuery);
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
                foreach (JsonGetUserByTasksResult jbr in bill.result)
                {
                    TaskEntity asnEntity = new TaskEntity();
                    asnEntity.DiffTimeValue = Convert.ToInt64(jbr.diffTime);
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.TaskName = jbr.itemDesc;
                    asnEntity.CompletedTaskCount = Convert.ToInt64(jbr.taskCount);
                    asnEntity.UserName = jbr.userName;
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
        /// 获取任务池当前状态信息
        /// </summary>
        /// <returns></returns>
        public  List<TaskEntity> GetCurrentTaskNew()
        {
            List<TaskEntity> list = new List<TaskEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("beginTime=").Append(beginTime).Append("&");
                //loStr.Append("endTime=").Append(endTime);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetCurrentTaskNew);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCurrentTaskNew bill = JsonConvert.DeserializeObject<JsonGetCurrentTaskNew>(jsonQuery);
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
                foreach (JsonGetCurrentTaskNewResult jbr in bill.result)
                {
                    TaskEntity asnEntity = new TaskEntity();
                    #region 0-10
                    asnEntity.BillDesc = jbr.billDesc;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillTypeDess = jbr.billTypeDEsc;
                    asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                    asnEntity.TaskName = jbr.itemDesc;
                    asnEntity.Qty = Convert.ToInt32(jbr.qty);
                    asnEntity.TaskDesc = jbr.taskDesc;
                    asnEntity.TaskType = jbr.taskType;
                    asnEntity.TaskID = Convert.ToInt32(jbr.tskId);
                    #endregion

                    #region 11-14
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.beginDate))
                            asnEntity.BeginDate = Convert.ToDateTime(jbr.beginDate);
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
                        if (!string.IsNullOrEmpty(jbr.confirmDate))
                            asnEntity.ConfirmDate = Convert.ToDateTime(jbr.confirmDate);
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

        /// <summary>
        /// 查询等待分配任务的单据
        /// </summary>
        /// <returns></returns>
        public  DataTable GetTask62()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("TASK_TYPE", Type.GetType("System.String"));
            tblDatas.Columns.Add("TASK_LEVEL", Type.GetType("System.String"));
            tblDatas.Columns.Add("begin_time", Type.GetType("System.String"));
            tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("ATTRI", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetTask62);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTask62 bill = JsonConvert.DeserializeObject<JsonGetTask62>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                foreach (JsonGetTask62Result tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ID"] = tm.id;
                    newRow["TASK_TYPE"] = tm.taskType;
                    newRow["TASK_LEVEL"] = tm.taskLevel;
                    newRow["begin_time"] = tm.beginTime;
                    newRow["ITEM_DESC"] = tm.itemDesc;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["CREATE_DATE"] = tm.createDate;
                    newRow["ATTRI"] = tm.attri;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }


        #region 方法
        private void LoadData()
        {
            try
            {
                List<TaskEntity> taskList = GetUserByTasks(
                    DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
                this.gridControl3.DataSource = taskList;
                this.gridColumn11.SummaryItem.SetSummary(
                    DevExpress.Data.SummaryItemType.Custom,
                    ConvertUtil.ToString(taskList.FindAll(u => !string.IsNullOrEmpty(u.TaskName)).Count));
                this.gridControl3.RefreshDataSource();
                this.gridControl1.DataSource = GetCurrentTaskNew();//GetCurrentTaskDetailInfo();
                this.gridControl1.RefreshDataSource();
                this.gridControl2.DataSource = GetTask62();
                this.gridControl2.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }



        /// <summary>
        /// 根据任务ID 获取订单明细
        /// </summary>
        /// <param name="taskID">任务 ID</param>
        /// <returns></returns>
        public List<SODetailEntity> GetDetailsByTaskID(int taskID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetailsByTaskID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetailsByTaskID bill = JsonConvert.DeserializeObject<JsonGetDetailsByTaskID>(jsonQuery);
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
                foreach (JsonGetDetailsByTaskIDResult jbr in bill.result)
                {
                    SODetailEntity asnEntity = new SODetailEntity();
                    #region 0-10
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CombMaterial = jbr.comMaterial;
                    asnEntity.DueDate = jbr.dueDate;
                    asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                    //jbr.id;
                    asnEntity.PickQty = Convert.ToDecimal(jbr.pickQty);
                    asnEntity.Price1 = Convert.ToDecimal(jbr.price);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowNO = Convert.ToInt32(jbr.roeNo);
                    #endregion

                    #region 11-18
                    asnEntity.SkuBarcode = jbr.skuBarCode;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.SuitNum = Convert.ToDecimal(jbr.suitNum);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
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
        /// 计算指定任务中是否存在不完整的套餐
        /// </summary>
        /// <param name="taskID">任务 ID</param>
        /// <returns></returns>
        private List<SODetailEntity> CalcNonFullDetails(int taskID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();

            List<SODetailEntity> details = GetDetailsByTaskID(taskID);
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

        /// <summary>
        /// 关闭任务---
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CloseTask(TaskEntity entity)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskId=").Append(entity.TaskID).Append("&");
                loStr.Append("type=").Append(entity.TaskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CloseTask);
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

        private void CloseTask()
        {
            try
            {
                TaskEntity entity = this.SelectedTaskRow;
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
                    if (CloseTask(entity))
                        this.LoadData();
                    Insert(ELogType.操作任务, String.Format("操作人:{0};授权人:{1}", GlobeSettings.LoginedUser.UserName, frmAuthorize.AuthUserCode), String.Format("任务ID:{0}；订单ID:{1}", entity.TaskID, entity.BillID), "手动关闭任务", "任务池管理");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获取任务对应的订单状态
        /// </summary>
        /// <param name="taskType"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool TaskState(string taskType, int taskID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskType=").Append(taskType).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_TaskState);
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
            //string resultMsg = this.taskDal.TaskState(editEntity.TaskType, editEntity.TaskID);
            //if (resultMsg != "Y")
            //{
            //    MsgBox.Warn(resultMsg);
            //    return;
            //}

            if (TaskState(editEntity.TaskType, editEntity.TaskID))
                return;

            FrmTaskChange frm = new FrmTaskChange(editEntity.TaskType, editEntity);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 删除装车任务
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userCode">操作人员</param>
        public bool DeleteLoadingTask(List<TaskEntity> list, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                List<string> prop = new List<string>() { "TaskID", "TaskName", "UserCode" };
                string jsonStr = GetResList<TaskEntity>("jsonStr", list, prop);
                jsonStr = "{" + jsonStr + "}";
                loStr.Append("jsonStr=").Append(jsonStr);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteLoadingTask);
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
        /// 删除装车任务
        /// </summary>
        private void DeleteLoadingTask()
        {
            using (FrmChooseTask frmChooseTask = new FrmChooseTask(ETaskType.装车任务, "是否确认删除选择的装车任务？"))
            {
                if (frmChooseTask.ShowDialog() == DialogResult.OK)
                {
                    FrmTempAuthorize frmTempAuth = new FrmTempAuthorize("管理员");
                    if (frmTempAuth.ShowDialog() == DialogResult.OK)
                        DeleteLoadingTask(frmChooseTask.SelectedTasks, GlobeSettings.LoginedUser.UserCode);
                    this.LoadData();
                }
            }
        }
        #endregion

        #region 事件
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    this.LoadData();
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
                case "任务优先级管理":
                    using (FrmTaskLevel frmTaskLevel = new FrmTaskLevel())
                    {
                        frmTaskLevel.ShowDialog();
                    }
                    break;
                case "装车任务优先":
                    #region old code
                    //if (e.Item.Caption == "装车任务优先")
                    //{
                    //    if (BaseCodeDal.UpdateFieldByAttri(145, 1) > 0)
                    //    {
                    //        e.Item.Caption = "取消装车任务优先";
                    //        this.LoadData();
                    //    }
                    //}
                    //else
                    //{
                    //    if (BaseCodeDal.UpdateFieldByAttri(145, 0) > 0)
                    //    {
                    //        e.Item.Caption = "装车任务优先";
                    //        this.LoadData();
                    //    }
                    //}
                    #endregion
                    //using (FrmNonLoadingBills frmNonLoadingBills = new FrmNonLoadingBills())
                    //{
                    //    frmNonLoadingBills.ShowDialog();
                    //    this.LoadData();
                    //}
                    break;
                case "取消装车优先":
                    //if (BaseCodeDal.UpdateFieldByAttri(145, 0) > 0)
                    //{
                    //    this.LoadData();
                    //}
                    break;
                case "删除装车任务":
                    this.DeleteLoadingTask();
                    break;
                case "人员调度":
                    using (FrmPersonalScheduling frmPersonal = new FrmPersonalScheduling())
                    {
                        frmPersonal.ShowDialog();
                    }
                    break;
                case "人员变更":
                    this.ChangePersonnel();
                    break;
                case "人员添加":
                    this.AddPerson();
                    break;
            }
        }

        /// <summary>
        /// 查询任务名称
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public string GetRoleNameByTaskType(string taskType)
        {
            string jsons = string.Empty;

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetRoleNameByTaskType);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return jsons;
                }
                #endregion

                #region 正常错误处理

                JsonGetRoleNameByTaskType bill = JsonConvert.DeserializeObject<JsonGetRoleNameByTaskType>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return jsons;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return jsons;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].roleName;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return jsons;
        }

        /// <summary>
        /// 判断是否存在已分配某个人
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="billID"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public bool CanAdd(string userCode, int billID, string taskType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CanAdd);
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
        /// 添加人员
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userCode"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public bool AddInstoreTaskPerson(int taskID, string userCode, string taskType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("taskID=").Append(taskID).Append("&");
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_AddInstoreTaskPerson);
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

        private void AddPerson()
        {
            try
            {
                TaskEntity editEntity = SelectedTaskRow;
                if (editEntity == null)
                {
                    MsgBox.Warn("没有要修改的数据。");
                    return;
                }
                if (editEntity.TaskType != "142" && editEntity.TaskType != "146" && editEntity.TaskType != "147")
                {
                    MsgBox.Warn("当前只能变更 清点/复核/上架 任务！");
                    return;
                }
                string roleName = GetRoleNameByTaskType(editEntity.TaskType);
                using (FrmChoosePersonnel frmChoose = new FrmChoosePersonnel(false, roleName, true))
                {
                    if (frmChoose.ShowDialog() != DialogResult.OK)
                        return;

                    if (!CanAdd(frmChoose.SelectedPersonnel.UserCode, editEntity.BillID, editEntity.TaskType))
                    {
                        MsgBox.Warn("无法将任务重复分给同一个人！");
                        return;
                    }
                    using (FrmTempAuthorize frmAuth = new FrmTempAuthorize("管理员"))
                    {
                        if (frmAuth.ShowDialog() == DialogResult.OK)
                        {
                            AddInstoreTaskPerson(
                                editEntity.TaskID,
                                frmChoose.SelectedPersonnel.UserCode,editEntity.TaskType);
                            Insert(ELogType.任务, GlobeSettings.LoginedUser.UserCode,
                                editEntity.TaskID.ToString(), "添加入库任务操作人员", this.Text, frmChoose.SelectedPersonnel.UserCode);
                            this.LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 变更人员
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool ChangeInstoreTask(int taskID, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskID=").Append(taskID).Append("&");
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ChangeInstoreTask);
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


        private void ChangePersonnel()
        {
            try
            {
                TaskEntity editEntity = SelectedTaskRow;
                if (editEntity == null)
                {
                    MsgBox.Warn("没有要修改的数据。");
                    return;
                }
                if (editEntity.TaskType != "142" && editEntity.TaskType != "146" && editEntity.TaskType != "147")
                {
                    MsgBox.Warn("当前只能变更 清点/复核/上架 任务！");
                    return;
                }
                string roleName = GetRoleNameByTaskType(editEntity.TaskType);
                using (FrmChoosePersonnel frmChoose = new FrmChoosePersonnel(false, roleName, true))
                {
                    if (frmChoose.ShowDialog() != DialogResult.OK)
                        return;
                    if (!CanAdd(frmChoose.SelectedPersonnel.UserCode, editEntity.BillID, editEntity.TaskType))
                    {
                        MsgBox.Warn("无法将任务重复分给同一个人！");
                        return;
                    }
                    using (FrmTempAuthorize frmAuth = new FrmTempAuthorize("管理员"))
                    {
                        if (frmAuth.ShowDialog() == DialogResult.OK)
                        {
                            ChangeInstoreTask(
                                editEntity.TaskID,
                                frmChoose.SelectedPersonnel.UserCode);
                            Insert(ELogType.任务, GlobeSettings.LoginedUser.UserCode,
                                editEntity.TaskID.ToString(), "变更入库任务", this.Text);
                            this.LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditZone();
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            this.LoadData();
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            this.LoadData();
        }
        #endregion


        /// <summary>
        /// 任务自动刷新
        /// </summary>
        /// <returns></returns>
        public bool AutoAssignTask()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_AutoAssignTask);
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
        /// 任务优先--stickTask
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool StickTask(int taskID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_StickTask);
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

        private void simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SimpleButton button = sender as SimpleButton;
                if (button == null || button.Tag == null)
                    return;
                switch (button.Tag.ToString())
                {
                    case "分派任务":
                        //string result = new SODal().AutoAssignTask();
                        //if (result == "Y")
                        //{
                        //    this.LoadData();
                        //}
                        if (AutoAssignTask())
                        {
                            this.LoadData();
                        }
                        break;
                    case "任务优先":
                        //using (FrmTempAuthorize frmAuth = new FrmTempAuthorize("管理员"))
                        //{
                        //    if (frmAuth.ShowDialog() == DialogResult.OK)
                        //    {
                                DataRow row = this.gridView2.GetDataRow(this.gridView2.FocusedRowHandle);
                                int taskID = ConvertUtil.ToInt(row.ItemArray[0]);
                                if (!StickTask(taskID))
                                    return;
                                this.LoadData();
                        //    }
                        //}
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 得到任务类型
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int GetTaskType(int taskID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTaskType);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return 0;
                }
                #endregion

                #region 正常错误处理

                JsonGetTaskType bill = JsonConvert.DeserializeObject<JsonGetTaskType>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return 0;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return 0;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].taskType;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return 0;
        }


        /// <summary>
        /// 获取任务详情
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public DataTable GetTaskDetail(int taskID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_IGetTaskDetail);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTaskDetail bill = JsonConvert.DeserializeObject<JsonGetTaskDetail>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region
                switch (GetTaskType(taskID))
                {
                    case 140:
                        {
                            #region 140
                            tblDatas.Columns.Add("盘点状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("货位", Type.GetType("System.String"));
                            tblDatas.Columns.Add("货位状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("创建人", Type.GetType("System.String"));
                            tblDatas.Columns.Add("创建时间", Type.GetType("System.String"));
                            tblDatas.Columns.Add("所属库房", Type.GetType("System.String"));
                            tblDatas.Columns.Add("备注", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["盘点状态"] = tm.itemDesc;
                                newRow["货位"] = tm.isCode;
                                newRow["货位状态"] = tm.lcState;
                                newRow["创建人"] = tm.creator;
                                newRow["创建时间"] = tm.createDate;
                                newRow["所属库房"] = tm.whCode;
                                newRow["备注"] = tm.remark;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 142:
                        {
                            #region 142
                            tblDatas.Columns.Add("托盘号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("托盘状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("订单号", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["托盘号"] = tm.ctCode;
                                newRow["托盘状态"] = tm.itemDesc;
                                newRow["订单号"] = tm.billNo;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 143:
                        {
                            #region 143
                            tblDatas.Columns.Add("订单编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("商品名称", Type.GetType("System.String"));
                            tblDatas.Columns.Add("拣货货位", Type.GetType("System.String"));
                            tblDatas.Columns.Add("计划拣货量", Type.GetType("System.Decimal"));
                            tblDatas.Columns.Add("实际拣货量", Type.GetType("System.Decimal"));
                            tblDatas.Columns.Add("单位", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["订单编号"] = tm.billNo;
                                newRow["商品名称"] = tm.skuName;
                                newRow["拣货货位"] = tm.lcCode;
                                newRow["计划拣货量"] = StringToDecimal.GetTwoDecimal(tm.qty);
                                newRow["实际拣货量"] = StringToDecimal.GetTwoDecimal(tm.pickQty);
                                newRow["单位"] = tm.umName;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 144:
                        {
                            #region 144
                            tblDatas.Columns.Add("任务状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("商品名称", Type.GetType("System.String"));
                            tblDatas.Columns.Add("来源货位", Type.GetType("System.String"));
                            tblDatas.Columns.Add("数量", Type.GetType("System.Decimal"));
                            tblDatas.Columns.Add("目标货位", Type.GetType("System.String"));
                            tblDatas.Columns.Add("单位", Type.GetType("System.String"));
                            tblDatas.Columns.Add("创建日期", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["任务状态"] = tm.itemDesc;
                                newRow["商品名称"] = tm.skuName;
                                newRow["来源货位"] = tm.sourceLcCode;
                                newRow["数量"] = StringToDecimal.GetTwoDecimal(tm.qty);
                                newRow["目标货位"] = tm.targetLcCode;
                                newRow["单位"] = tm.umName;
                                newRow["创建日期"] = tm.createDate;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 145:
                    case 148:
                        {
                            #region 145,148
                            tblDatas.Columns.Add("托盘号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("装车编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("托盘位编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("单据编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("仓库编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("车内顺序", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["托盘号"] = tm.ctCode;
                                newRow["装车编号"] = tm.vhTrainNo;
                                newRow["托盘位编号"] = tm.ctlName;
                                newRow["单据编号"] = tm.billNo;
                                newRow["仓库编号"] = tm.whCode;
                                newRow["车内顺序"] = tm.inVhSort;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 146:
                        {
                            #region 146
                            tblDatas.Columns.Add("送货牌号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("送货牌状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("订单编码", Type.GetType("System.String"));
                            tblDatas.Columns.Add("订单状态", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["送货牌号"] = tm.cardNo;
                                newRow["送货牌状态"] = tm.cardState;
                                newRow["订单编码"] = tm.billNo;
                                newRow["订单状态"] = tm.itemDesc;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    case 147:
                        {
                            #region 147
                            tblDatas.Columns.Add("托盘号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("托盘状态", Type.GetType("System.String"));
                            tblDatas.Columns.Add("订单号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("商品编号", Type.GetType("System.String"));
                            tblDatas.Columns.Add("商品数量", Type.GetType("System.Decimal"));
                            tblDatas.Columns.Add("商品单位", Type.GetType("System.String"));
                            #endregion

                            #region 赋值
                            foreach (JsonJsonGetTaskDetailResult tm in bill.result)
                            {
                                DataRow newRow;
                                newRow = tblDatas.NewRow();
                                newRow["托盘号"] = tm.ctCode;
                                newRow["托盘状态"] = tm.itemDesc;
                                newRow["订单号"] = tm.billNo;
                                newRow["商品编号"] = tm.skuCode;
                                newRow["商品数量"] = StringToDecimal.GetTwoDecimal(tm.qty);
                                newRow["商品单位"] = tm.umName;
                                tblDatas.Rows.Add(newRow);
                            }
                            #endregion
                        }
                        break;
                    default:
                        break;
                }
                #endregion

                return tblDatas;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
                return;
            try
            {
                object row = gridView.GetFocusedRow();
                int taskID = -1;
                string billNo = string.Empty;
                if (row is DataRowView)
                {
                    object[] array = (row as DataRowView).Row.ItemArray;
                    taskID = ConvertUtil.ToInt(array[0]);
                    billNo = ConvertUtil.ToString(array[5]);
                }
                else if (row is TaskEntity)
                {
                    TaskEntity entity = row as TaskEntity;
                    taskID = entity.TaskID;
                    billNo = entity.BillNO;
                }
                this.gridView4.Columns.Clear();
                this.gridControl4.DataSource = GetTaskDetail(taskID);
                this.gridView4.ViewCaption = string.Format("订单编号：{0}", billNo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.timerRefresh.Enabled = ConvertUtil.ToBool(this.barEditItem1.EditValue);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}
