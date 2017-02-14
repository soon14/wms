using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DotNetSpeech;
using System.Threading;
using Nodes.UI;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmCallingScreen : DevExpress.XtraEditors.XtraForm
    {
        private string DefaultCallNum = "3";
        private string LabelText = "";
        Thread th = null;
        //SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFDefault;
        SpVoice Voice = new SpVoice();
        private delegate void RefreshGrid();
        private delegate void RefreshLabel();
        private RefreshGrid rg = null;
        private RefreshLabel rl = null;
        private bool IsRun = true;
        public FrmCallingScreen()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 更新叫号状态
        /// </summary>
        /// <param name="callID"></param>
        /// <returns></returns>
        public bool UpdateCallState(int callID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("callId=").Append(callID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateCallState);
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

        private void CallVoice()
        {
            try
            {
                Thread.Sleep(2000);
                string callType = "";
                string callState = "";
                string taskDesc = "";
                string userName = "";
                while (IsRun)
                {
                    this.Invoke(rg);

                    if (gridView1.RowCount == 0)
                    {
                        Thread.Sleep(10000);
                        continue;
                    }
                    Thread.Sleep(1000);
                    callState = ConvertUtil.ToString(gridView1.GetRowCellValue(0, "CALL_STATE"));
                    if (callState == "未叫号")
                    {
                        userName = ConvertUtil.ToString(gridView1.GetRowCellValue(0, "DESCRIPTION"));
                        taskDesc = ConvertUtil.ToString(gridView1.GetRowCellValue(0, "TASK_DESC"));
                        LabelText = String.Format("当前呼叫人员：{0}", userName);
                        this.Invoke(rl);
                        for (int j = 0; j < ConvertUtil.ToInt(textEdit1.Text.Trim()); j++)
                        {
                            Voice.Speak("请 " + userName + "  执行   " + taskDesc, SpFlags);
                            Thread.Sleep(50);
                        }
                        //更新叫号状态
                        UpdateCallState(ConvertUtil.ToInt(gridView1.GetRowCellValue(0, "ID")));
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                textEdit1.Text = DefaultCallNum;
                BindData();
                gridControl1.Focus();
                rg = new RefreshGrid(BindData);
                rl = new RefreshLabel(RefreshLabelText);
                th = new Thread(new ThreadStart(CallVoice));
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获取叫号内同
        /// </summary>
        /// <returns></returns>
        public  DataTable GetCallingData()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("TASK_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("CALL_TYPE", Type.GetType("System.String"));
            tblDatas.Columns.Add("DESCRIPTION", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CALL_NUM", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("CALL_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("LAST_UPDATE_TIME", Type.GetType("System.String"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetCallingData);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetCallingData bill = JsonConvert.DeserializeObject<JsonGetCallingData>(jsonQuery);
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
                foreach (JsonGetCallingDataResult tm in bill.result)
                {
                    DataRow newRow;
                    #region 0-10
                    newRow = tblDatas.NewRow();
                    newRow["TASK_DESC"] = tm.taskDesc;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["BILL_STATE"] = tm.billState;
                    newRow["BILL_DESC"] = tm.billDesc;
                    newRow["ID"] = Convert.ToInt32(tm.id);
                    newRow["CALL_TYPE"] = tm.callType;
                    newRow["DESCRIPTION"] = tm.description;
                    newRow["USER_CODE"] = tm.userCode;
                    newRow["CALL_NUM"] = Convert.ToInt32(tm.callNum);
                    newRow["CALL_STATE"] = tm.callState;
                    newRow["LAST_UPDATE_TIME"] = tm.lastUpdateTime;
                    #endregion
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

        private void BindData()
        {
            try
            {
                gridControl1.DataSource = GetCallingData();
                simpleLabelItem1.Text = " ";
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

        }

        private void RefreshLabelText()
        {
            simpleLabelItem1.Text = LabelText;
        }

        private void FrmCallingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsRun = false;
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textEdit1.Text))
            {
                textEdit1.Text = DefaultCallNum;
            }
        }

        /// <summary>
        /// 重复叫号
        /// </summary>
        /// <param name="callID"></param>
        /// <returns></returns>
        public bool ReCall(int callID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("callId=").Append(callID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ReCall);
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

        private void btnReCallClick(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.RowCount == 0)
                    return;
                int id = ConvertUtil.ToInt(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID"));
                if(ReCall(id))
                    BindData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}