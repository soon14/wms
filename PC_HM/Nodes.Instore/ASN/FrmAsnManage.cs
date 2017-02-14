using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Instore.ASN;
using System.Windows.Forms;
using Nodes.WMS.Inbound;
using System.Threading;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;

namespace Nodes.Instore
{
    public partial class FrmAsnManage : DevExpress.XtraEditors.XtraForm, IAsnManager
    {
        AsnDal asnDal = new AsnDal();
        PreAsnManager preAsnManager = null;

        public FrmAsnManage()
        {
            InitializeComponent();
            preAsnManager = new PreAsnManager(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolToday.ImageIndex = (int)AppResource.EIcons.today;
            toolWeek.ImageIndex = (int)AppResource.EIcons.week;
            toolView.ImageIndex = (int)AppResource.EIcons.eye;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            barSubItem1.ImageIndex = (int)AppResource.EIcons.print;
            toolCopy.ImageIndex = (int)AppResource.EIcons.copy;
            toolCancelCommit.ImageIndex = (int)AppResource.EIcons.remove;
            toolCommit.ImageIndex = (int)AppResource.EIcons.ok;
            barButtonItem10.ImageIndex = (int)AppResource.EIcons.message;
            toolModifyInstoreType.ImageIndex = (int)AppResource.EIcons.edit;

            //ucGridPanel.CustomGridCaption();
            ucQueryPanel.DoQueryNotCompleteBill("所有进行中(未完成收货)的单据");
        }

        public void ReloadAsn()
        {
            ucQueryPanel.Reload();
        }

        public void RefreshState()
        {
            ucGridPanel.RefreshMeMemory();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        /// <summary>
        /// 收货单管理，修改入库方式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool UpdateInstoreType(string type, int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateInstoreType);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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

        void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    ReloadAsn();
                    break;
                case "进行中单据":
                    ucQueryPanel.DoQueryNotCompleteBill("所有进行中(未完成收货)的单据");
                    break;
                case "近一周单据":
                    ucQueryPanel.DoQuery(DateTime.Now.AddDays(-6).Date, DateTime.Now.AddDays(1).Date, 
                        string.Format("最近一周(【{0}】-【{1}】)创建的单据", 
                        DateTime.Now.AddDays(-6).Date.ToShortDateString(), 
                        DateTime.Now.Date.ToShortDateString()));
                    break;
                case "新建":
                    //using (FrmPoEdit frmNewBill = new FrmPoEdit())
                    //{
                    //    frmNewBill.MdiParent = this.MdiParent;
                    //    frmNewBill.Show();
                    //}
                    break;
                case "编辑":
                    //DoEditOne();            whc        
                    break;
                case "删除":
                    //preAsnManager.DeleteSelectedBill(ucGridPanel.FocusedHeaders);         whc
                    break;
                case "取消确认":
                    //preAsnManager.CancelReceivedConfirm(ucGridPanel.FocusedHeaders);      whc
                    break;
                case "到货确认":
                    //preAsnManager.ReceivedConfirm(ucGridPanel.FocusedHeaders);            whc
                    break;
                case "收货完成":
                    preAsnManager.ReceivedComplete(ucGridPanel.FocusedHeaders);
                    ReloadAsn();
                    break;
                case "新收货单":
                    //DoCopyOne();
                    break;
                case "编辑备注":
                    //preAsnManager.EditRemark(ucGridPanel.FocusedHeader);                  whc
                    break;
                case "单据日志":
                    preAsnManager.ViewLog(ucGridPanel.FocusedHeader);
                    break;
                case "托盘记录":
                    if(ucGridPanel.FocusedHeader==null)
                    {
                        MsgBox.Warn("请先选择一个收货单！");
                        return;
                    }
                    FrmASNShowContainer frm = new FrmASNShowContainer(ucGridPanel.FocusedHeader);
                    frm.ShowDialog();
                    break;
                case "修改入库方式":
                    if (ucGridPanel.FocusedHeader == null)
                    {
                        MsgBox.Warn("请先选择一个收货单！");
                        return;
                    }
                    if (ucGridPanel.FocusedHeader.BillState != "20")
                    {
                        MsgBox.Warn("只有等待入库的订单才能修改入库方式！");
                        return;
                    }
                    FrmInstoreTypeModify frmInstoreTypeModify = new FrmInstoreTypeModify(ucGridPanel.FocusedHeader);
                    if (frmInstoreTypeModify.ShowDialog() == DialogResult.OK)
                    {
                        //保存到数据库
                        UpdateInstoreType(frmInstoreTypeModify.ItemValue, ucGridPanel.FocusedHeader.BillID);
                        ReloadAsn();
                    }
                    break;
                case "打印收货单":
                    PrintASN();
                    //ReloadAsn();
                    break;
                //case "d":
                //    test t = new test();
                //    t.Print();
                //    break;
                default:
                    MsgBox.OK("正在实现");
                    break;
            }
        }

        #region 查看日志、删除、提交等操作
        private void DoEditOne()
        {
            if (ucGridPanel.FocusedRowCount == 0)
            {
                MsgBox.Warn("请选中要编辑的行。");
                return;
            }

            if (ucGridPanel.FocusedRowCount > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            AsnBodyEntity focusedHeader = ucGridPanel.FocusedHeader;
            if (focusedHeader.BillState != BillStateConst.ASN_STATE_CODE_NOT_ARRIVE)
            {
                MsgBox.Warn(string.Format("单据“{0}”不允许编辑，只有状态为“{1}”的单据才能编辑。", 
                    focusedHeader.BillID, BillStateConst.ASN_STATE_DESC_NOT_ARRIVE));
                return;
            }

            //FrmAsnEdit frmEditBill = new FrmAsnEdit(focusedHeader, false);
            //frmEditBill.MdiParent = this.MdiParent;
            //frmEditBill.Show();
        }

        public List<AsnBodyEntity> GetFocusedHeaders()
        {
            return ucGridPanel.FocusedHeaders;
        }

        private void DoCopyOne()
        {
            if (ucGridPanel.FocusedRowCount == 0)
            {
                MsgBox.Warn("请选中要复制的行。");
                return;
            }

            if (ucGridPanel.FocusedRowCount > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            //FrmAsnEdit frmEditBill = new FrmAsnEdit(ucGridPanel.FocusedHeader, true);
            //frmEditBill.MdiParent = this.MdiParent;
            //frmEditBill.Show();
        }

        #endregion

        private void OnQueryCompleted(List<AsnBodyEntity> dataSource)
        {
            ucGridPanel.DataSource = dataSource;
            ucGridPanel.ShowCondition(ucQueryPanel.QueryCondition);
            ucGridPanel.ShowTimeMsg(ucQueryPanel.ElapsedTime);

            popupControlContainer1.HidePopup();
        }

        private void popupControlContainer1_Popup(object sender, EventArgs e)
        {
            ucQueryPanel.LoadDataSource();
        }

        /// <summary>
        /// 收货单据管理，查询订单中未复核的托盘
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool GetContainerNochek(AsnBodyEntity header)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(header.BillID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerNochek);
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

        #region List转换成Json
        /// <summary>
        /// List转换成Json
        /// </summary>
        public  string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        /// <summary>
        /// List转换成Json 
        /// </summary>
        public  string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = list[0].GetType().Name;
            Json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + string.Format(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        public bool CheckQtyPutQty(int billId)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CheckQtyPutQty);
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
                    if (MsgBox.AskOK(bill.error) != DialogResult.OK)
                        return false;
                    else
                        return true;
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

        public void PrintASN()
        {
            List<AsnBodyEntity> focusedBills = ucGridPanel.FocusedHeaders;
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要打印的单据。");
                return;
            }
            
            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要开始打印吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;
            try
            {
                #region
                foreach (AsnBodyEntity header in focusedBills)
                {
                    XtraReport repAsn = null;
                    bool containerNoCheck = GetContainerNochek(header);
                    if (!containerNoCheck)
                    {
                        //MsgBox.Warn("该订单关联的托盘 " + containerNoCheck + " 还未复核完毕！");
                        return;
                    }

                    if (!CheckQtyPutQty(header.BillID))
                        return;

                    repAsn = GetRepDetail(header, repAsn, 1);
                    //repAsn.ShowPreviewDialog();
                    for (int i = 0; i < 2; i++)
                    {
                        Thread.Sleep(50);
                        //repAsn.ShowPreviewDialog();
                        repAsn.Print();
                        repAsn = GetRepDetail(header, repAsn, 2); //whc
                    }
                    //更新打印标记为已打印
                    //this.asnDal.UpdatePrintedFlag(header.BillID);
                    //header.Printed = 1;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message+"=="+ex.StackTrace);
            }
            
        }

        private  XtraReport GetRepDetail(AsnBodyEntity header, XtraReport repAsn,int tag)
        {
            if (header.BillType == "3")
            {
                repAsn = new RepAsnTransfer(header.BillID, 1);
            }
            else
            {
                repAsn = new RepAsn(header.BillID, 1, tag);
            }
            return repAsn;
        }

        string GetBillNOs(List<AsnBodyEntity> bills)
        {
            string billNOs = string.Empty;
            foreach (AsnBodyEntity header in bills)
                billNOs += header.BillNO + ",";

            return billNOs.TrimEnd(',');
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        
    }
}