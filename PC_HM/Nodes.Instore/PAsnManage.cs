using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Shares;
using Nodes.Entities;
using Nodes.Utils;
using System.Windows.Forms;
using Nodes.DBHelper;
using System.Linq;
using Nodes.Controls;

namespace Nodes.Instore
{
    public class PAsnManage
    {
        private ASNDal asnDal;
        private IAsnManage IParent;

        public PAsnManage(IAsnManage iAsn)
        {
            IParent = iAsn;
            asnDal = new ASNDal();
        }

        string IsSelectedBill()
        {
            ASNHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
                return null;
            }
            else
            {
                return header.BillID;
            }
        }

        /// <summary>
        /// 显示单据状态更新日志
        /// </summary>
        /// <param name="billID"></param>
        public void ShowBillLog()
        {
            ASNHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                //FrmViewBillLog frmLog = new FrmViewBillLog(header.BillID, header.BillID);
                //frmLog.ShowDialog();
            }
        }

        /// <summary>
        /// 显示入库明细
        /// </summary>
        /// <param name="billID"></param>
        //public void ShowInboundDetails()
        //{
        //    ASNHeaderEntity header = IParent.GetFocusedBill();
        //    if (header == null)
        //    {
        //        MsgBox.Warn("请选中要查看的行。");
        //    }
        //    else
        //    {
        //        //FrmListInboundDetails frmDetails = new FrmListInboundDetails(header.BillID, header.BillNO);
        //        //frmDetails.ShowDialog();
        //    }
        //}

        ///// <summary>
        ///// 显示入库汇总
        ///// </summary>
        ///// <param name="billID"></param>
        //public void ShowInboundSummary()
        //{
        //    ASNHeaderEntity header = IParent.GetFocusedBill();
        //    if (header == null)
        //    {
        //        MsgBox.Warn("请选中要查看的行。");
        //    }
        //    else
        //    {
        //        FrmListInboundSummary frmDetails = new FrmListInboundSummary(header.BillID, header.BillNO);
        //        frmDetails.ShowDialog();
        //    }
        //}

        /// <summary>
        /// 返回单据的编号字符串，以逗号隔开
        /// </summary>
        /// <param name="bills"></param>
        /// <returns></returns>
        string GetBillNOs(List<ASNHeaderEntity> bills)
        {
            string billNOs = string.Empty;
            //foreach (ASNHeaderEntity header in bills)
            //    billNOs += header.BillNO + ",";

            return billNOs.TrimEnd(',');
        }

        #region 删除选中的单据（支持多选）
        public void DeleteSelectBills()
        {
            List<ASNHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要删除的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除选中的“{0}”个单据“{1}”吗？", 
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;

            try
            {
                foreach (ASNHeaderEntity header in focusedBills)
                {
                    //ASNHeaderEntity _header = asnDal.GetLastestHeaderStatus(header.BillID);
                    //if (_header.Status.CompareTo(SysCodeConstant.ASN_STATUS_CHECKING) < 0)
                    //{
                    //    int result = asnDal.DeleteASN(header.BillID, header.BillNO, GlobeSettings.LoginedUser.UserName);
                    //    if (result == -2)
                    //    {
                    //        MsgBox.Warn(string.Format("单据“{0}”状态为{1}，无法删除，只能删除尚未开始验收的单据。", header.BillNO, _header.StatusName));
                    //        return;
                    //    }

                    //    IParent.RemoveBill(header);
                    //}
                    //else
                    //{
                    //    MsgBox.Warn(string.Format("单据“{0}”状态为{1}，无法删除，只能删除尚未开始验收的单据。", header.BillNO, _header.StatusName));
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 执行货到确认操作
        public void DoASNArrived()
        {
            List<ASNHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要确认的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要执行到货确认吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;

            try
            {
                foreach (ASNHeaderEntity header in focusedBills)
                {
                    //string currStatus = asnDal.GetStatus(header.BillID);
                    //if (currStatus != SysCodeConstant.ASN_STATUS_ARRIVE_CONFIRM)
                    //{
                    //    MsgBox.Warn(string.Format("单据“{0}”当前状态为“{1}”，必须为尚未确认的状态才可以执行到货确认操作。", header.BillNO, header.StatusName));
                    //    return;
                    //}

                    ////更新状态
                    //asnDal.SetStatusToArriveConfirm(header.BillID);

                    ////填写日志
                    //BillLogDal.WriteStatusUpdate(header.BillID, "99", SysCodeConstant.ASN_STATUS_AWAIT_CHECK, GlobeSettings.LoginedUser.UserName);

                    ////更新界面
                    //header.UpdateState(asnDal.GetLastestHeaderStatus(header.BillID));
                }

                IParent.RefreshHeaderGrid();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 关闭单据
        public void DoCloseBill()
        {
            List<ASNHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills == null || focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要收货完成的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要将单据设置收货完成（关闭单据）吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;

            try
            {
                foreach (ASNHeaderEntity header in focusedBills)
                {
                    int result = asnDal.SetStatusToPutawayComplete(header.BillID, GlobeSettings.LoginedUser.UserName);
                    ASNHeaderEntity _header = asnDal.GetLastestHeaderStatus(header.BillID);
                    if (result == 0)
                    {
                        //从服务器重新获取单据信息，并更新界面
                        header.UpdateState(_header);
                    }
                    else if (result == -1)
                    {
                        MsgBox.Warn(string.Format("单据“{0}”的当前状态为{1}，无法设置为完成，请刷新后重试。", header.BillNO, _header.StatusName));
                        break;
                    }
                    else if (result == -2)
                    {
                        MsgBox.Warn(string.Format("单据“{0}”仍有已验收的物料没有上架，无法设置为收获完成状态。", header.BillNO));
                        break;
                    }
                    else
                    {
                        MsgBox.Warn(string.Format("单据“{0}”无法设置为完成，请检查组分料的每一项上架数量是否完全一样。", header.BillNO));
                        break;
                    }
                }

                IParent.RefreshHeaderGrid();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 打印通知单
        public void PrintAsn()
        {
            List<ASNHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要打印的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要开始打印吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;

            foreach (ASNHeaderEntity header in focusedBills)
            {
                //RepAsn repAsn = new RepAsn(header.BillID, 1);
                //repAsn.Print();

                ////更新打印标记为已打印
                //asnDal.UpdatePrintedFlag(header.BillID);
                //header.Printed = 1;
            }

            IParent.RefreshHeaderGrid();
        }
        #endregion

        #region 打印流水号标签
        /// <summary>
        /// 打印流水号标签
        /// </summary>
        public void PrintLabel()
        {
            List<ASNHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要打印的单据。");
                return;
            }
            else
            {
                int total = 0;
                foreach (ASNHeaderEntity header in focusedBills)
                    total += asnDal.GetOrderQtyByBillID(header.BillID);

                //FrmPrintLabel frmPrintSeq = new FrmPrintLabel(total);
                //    frmPrintSeq.ShowDialog();
            }
        }
        #endregion

        #region 编辑备注
        public void WriteWMSRemark()
        {
            ASNHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要修改的单据。");
            }
            else
            {
                //FrmEditAsn frmEdit = new FrmEditAsn(header);
                //if (frmEdit.ShowDialog() == DialogResult.OK)
                //{
                //    //刷新界面显示
                //    header.UpdateRemark(asnDal.GetHeaderInfoByBillID(header.BillID));
                //}
            }
        }
        #endregion

        #region 打开报表设计器
        public void OpenAsnReport()
        {
            //RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
            //RepAsn rep = new RepAsn();
            //try
            //{
            //    designForm.OpenReport(rep, rep.RepFileName);
            //    designForm.ShowDialog();
            //    designForm.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err(ex.Message);
            //}
        }
        #endregion

        #region 修改入库方式
        /// <summary>
        /// 修改入库方式
        /// </summary>
        public void ChangeInboundStyle()
        {
            ASNHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要修改的单据行！");
                return;
            }

            if (SysCodeConstant.ASN_STATUS_AWAIT_CHECK != header.Status)
            {
                MsgBox.Warn("只有等待验收状态的单据才能制定入库策略！");
                return;
            }

            //FrmStrategyDialog frmStrategyDialog = new FrmStrategyDialog(header);
            //frmStrategyDialog.dataSourceChanged += OnEditChanage;
            //frmStrategyDialog.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            IParent.RefreshHeaderGrid();
        }
        #endregion

        #region 处理查询
        private int QueryType = 1; //1：所有未完成；2：未完成（仅限7日内的单据）；3：自定义
        private string BillNO, BillType, BillState, InboundType, Supplier, SalesMan;
        private DateTime DateFrom, DateTo;

        public void Requery()
        {
            BindQueryResult(QueryType, BillNO, Supplier, SalesMan, BillType, BillState,
                InboundType, DateFrom, DateTo);
        }

        public void Query(int queryType, DateTime dateFrom, DateTime dateTo)
        {
            BindQueryResult(queryType, null, null, null, null, null, null, dateFrom, dateTo);
        }

        public void BindQueryResult(int queryType, string billNO, string supplier, string salesMan, string billType, string billStatus,
            string inboundType, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                if (queryType == 3)
                {
                    if (dateFrom > dateTo)
                    {
                        MsgBox.Warn("开始时间不能大于结束时间。");
                        return;
                    }

                    if (dateFrom.Subtract(dateTo).Days > 180)
                    {
                        MsgBox.Warn("时间区间不能超过180天。");
                        return;
                    }
                }

                this.QueryType = queryType;
                List<ASNHeaderEntity> asnHeaderEntitys = null;

                if (this.QueryType == 3)
                {
                    this.BillNO = string.IsNullOrEmpty(billNO) ? null : billNO;
                    this.BillType = string.IsNullOrEmpty(billType) ? null : billType;
                    this.BillState = string.IsNullOrEmpty(billStatus) ? null : billStatus;
                    this.InboundType = string.IsNullOrEmpty(inboundType) ? null : inboundType;
                    this.Supplier = string.IsNullOrEmpty(supplier) ? null : supplier;
                    this.SalesMan = string.IsNullOrEmpty(salesMan) ? null : salesMan;
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    //asnHeaderEntitys = asnDal.QueryAsnBills(BillNO, Supplier, SalesMan, BillType, BillState,
                    //    InboundType, DateFrom, DateTo, warehouse);
                }
                else if (this.QueryType == 1)
                {
                    //asnHeaderEntitys = asnDal.QueryBillsQuickly(warehouse, null, null, null);
                }
                else
                {
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    //asnHeaderEntitys = asnDal.QueryBillsQuickly(warehouse, null, dateFrom, dateTo);
                }

                IParent.BindingGrid(asnHeaderEntitys);
                IParent.ShowFocusDetail();

                IParent.ShowQueryCondition(QueryType, BillNO, Supplier, SalesMan, BillType, BillState,
                InboundType, DateFrom, DateTo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 显示原始单据信息
        /// <summary>
        /// 修改入库方式
        /// </summary>
        public void ShowSapBill()
        {
            ASNHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的单据行！");
                return;
            }

            //FrmShowSapAsn frmSapBill = new FrmShowSapAsn(header.BillNO);
            //frmSapBill.ShowDialog();
        }
        #endregion
    }
}
