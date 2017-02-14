using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.Resources;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.UI;
using Nodes.Common;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOManage : DevExpress.XtraEditors.XtraForm, ISoManage
    {
        private PSoManage myPre;
        //private SODal soDal;
        private string warehouseCode = string.Empty;

        public FrmSOManage()
        {
            InitializeComponent();

            myPre = new PSoManage(this);
            //soDal = new SODal();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.download;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolToday.ImageIndex = (int)AppResource.EIcons.today;
            toolWeek.ImageIndex = (int)AppResource.EIcons.week;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            barSubItem1.ImageIndex = (int)AppResource.EIcons.log;
            barButtonItem8.ImageIndex = (int)AppResource.EIcons.edit;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.ok;
            toolCreateCRNBill.ImageIndex = (int)AppResource.EIcons.design;
            this.toolJoinVehicle.ImageIndex = (int)AppResource.EIcons.tree;
            toolCancelOrder.ImageIndex = (int)AppResource.EIcons.remove;
            InitDate();
            BindLookUpControl();

            //默认显示所有未完成单据
            myPre.Query(1, DateTime.Now, DateTime.Now);
        }

        /// <summary>
        /// 默认系统加载一月内的单据
        /// </summary>
        private void InitDate()
        {
            dateEditFrom.DateTime = System.DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = System.DateTime.Now;
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

        /// <summary>
        /// 绑定页面下拉列表
        /// </summary>
        private void BindLookUpControl()
        {
            //单据状态
            listBillState.Properties.DataSource = GetItemList(BaseCodeConstant.SO_STATE);

            //单据类型
            lookUpEditBillType.Properties.DataSource = GetItemList(BaseCodeConstant.SO_TYPE);

            //拣货方式
            lookUpEditStrategeType.Properties.DataSource = GetItemList(BaseCodeConstant.OUTSTORE_TYPE);
        }

        #region 接口
        public void BindingGrid(List<SOHeaderEntity> headers)
        {
            bindingSource1.DataSource = headers;
        }

        public SOHeaderEntity GetFocusedBill()
        {
            return SelectedHeader;
        }

        public void ShowQueryCondition(int queryType, string billNO, string customer, string salesMan, string billType, string billStatus,
            string outboundType, string material, DateTime dateFrom, DateTime dateTo)
        {
            string displayContent = string.Empty;
            string separator = " && ";
            if (queryType == 1)
                displayContent = "查询条件：“所有发货尚未完成”的出库单。";
            else if (queryType == 2)
                displayContent = "查询条件：“最近 7 天发货未完成”的出库单。";
            else
            {
                displayContent = string.Format(@"查询条件: {0}{1}{2}{3}{4}{5}{6}{7}",
                     string.IsNullOrEmpty(billNO) ? "" : "单据号=" + billNO + separator,
                    string.IsNullOrEmpty(customer) ? "" : "供应商包含'" + txtCustomer.Text.Trim() + "'" + separator,
                    string.IsNullOrEmpty(salesMan) ? "" : "业务员=" + salesMan + separator,
                    string.IsNullOrEmpty(material) ? "" : "物料包含'" + material + "'" + separator,
                    string.IsNullOrEmpty(billType) ? "" : "单据类型=" + lookUpEditBillType.Text + separator,
                    string.IsNullOrEmpty(billStatus) ? "" : "单据状态=" + listBillState.Text + separator,
                    string.IsNullOrEmpty(outboundType) ? "" : "拣货方式=" + lookUpEditStrategeType.Text + separator,
                  "时间范围 从 " + dateFrom.ToShortDateString() + " 至 " + dateTo.AddDays(-1).ToShortDateString());
            }

            lblQueryContent.Text = displayContent.TrimEnd('&');
            ClosePopup();
        }

        private void QueryCustom()
        {
            string billNO = txtBillNO.Text.Trim();
            string customer = ConvertUtil.StringToNull(txtCustomer.Text.Trim());
            string salesMan = txtSalesMan.Text.Trim();
            string billType = ConvertUtil.ToString(lookUpEditBillType.EditValue);
            string billState = ConvertUtil.ToString(listBillState.EditValue);
            string outboundType = ConvertUtil.ToString(lookUpEditStrategeType.EditValue);
            string material = ConvertUtil.StringToNull(txtMaterial.Text.Trim());

            myPre.BindQueryResult(3, billNO, customer, salesMan, billType, StringTrim.DeleteTrim(billState), outboundType, material,
                        dateEditFrom.DateTime.Date, dateEditTo.DateTime.AddDays(1).Date);
        }

        private void ClosePopup()
        {
            popupControlContainer1.HidePopup();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusDetail();
        }

        /// <summary>
        /// 出库单管理，查询出库单明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<SODetailEntity> GetDetails(int billID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetails);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetails bill = JsonConvert.DeserializeObject<JsonGetDetails>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonGetDetailsResult jbr in bill.result)
                {
                    SODetailEntity asnEntity = new SODetailEntity();
                    #region 0-10
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CombMaterial = jbr.comMaterial;
                    asnEntity.DetailID = Convert.ToInt32(jbr.detailId);
                    asnEntity.DueDate = jbr.dueDate;
                    asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.PickQty = Convert.ToDecimal(jbr.pickQty);
                    asnEntity.Price1 = Convert.ToDecimal(jbr.price);
                    #endregion
                    #region 11-20
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowNO = Convert.ToInt32(jbr.rowNo);
                    ///jbr.rowNo1;
                    asnEntity.SkuBarcode = jbr.skuBarCode;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.SuitNum = Convert.ToDecimal(jbr.suitNum);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    #endregion
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
                    }
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

        public void ShowFocusDetail()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = GetDetails(selectedHeader.BillID);
                gvDetails.ViewCaption = string.Format("单据号: {0};  超市名称：{1}; 地址：{2}", selectedHeader.BillNO, selectedHeader.CustomerName, selectedHeader.Address);
            }
            this.toolJoinVehicle.Enabled = (selectedHeader != null && selectedHeader.DelayMark == 1);
        }
        #endregion

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    myPre.Requery();
                    break;
                case "所有未完成":
                    myPre.Query(1, DateTime.Now, DateTime.Now);
                    break;
                case "近一周单据":
                    myPre.Query(2, DateTime.Now.AddDays(-7).Date, DateTime.Now.AddDays(1).Date);
                    break;
                case "修改备注":
                    myPre.WriteWMSRemark();
                    break;
                case "拣货记录":
                    myPre.ShowPickRecords();
                    break;
                case "拣货计划":
                    myPre.ShowPickPlan();
                    break;
                case "操作日志":
                    myPre.ShowBillLog();
                    break;
                case "打印出库单":
                    myPre.PrintSO();
                    break;
                case "发货完成":
                    myPre.CloseBill();
                    break;
                case "等待装车":
                    myPre.SetBillState();
                    break;
                case "修改出库方式":
                    myPre.UpdateOutstoreStype();
                    break;
                case "称重记录":
                    myPre.ShowWeighRecords();
                    break;
                case "取消订单":
                    myPre.CancelOrder();
                    break;
                case "订单详情":
                    myPre.ContainerDescribe();
                    break;
                case "订单落放明细":
                    using (FrmBillContainerInfo frmBillContainerInfo = new FrmBillContainerInfo())
                    {
                        frmBillContainerInfo.ShowDialog();
                    }
                    break;
            }
        }

        public List<SOHeaderEntity> GetFocusedBills()
        {
            List<SOHeaderEntity> checkedBills = new List<SOHeaderEntity>();
            int[] focusedHandles = gvHeader.GetSelectedRows();
            foreach (int handle in focusedHandles)
            {
                if (handle >= 0)
                    checkedBills.Add(gvHeader.GetRow(handle) as SOHeaderEntity);
            }

            return checkedBills;
        }

        public void RefreshHeaderGrid()
        {
            bindingSource1.ResetBindings(false);
        }

        SOHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as SOHeaderEntity;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryCustom();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void OnQueryPanelKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                QueryCustom();
        }

        /// <summary>
        /// 利用RowCellStyle给特殊的行标记字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHeaderRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView vw = (sender as GridView);
            try
            {
                SOHeaderEntity header = vw.GetRow(e.RowHandle) as SOHeaderEntity;
                if (header != null)
                {
                    if (header.RowForeColor != null)
                    {
                        e.Appearance.ForeColor = Color.FromArgb(header.RowForeColor.Value);
                    }
                }
            }
            catch (Exception ex) { }
        }

        //Add by ZXQ 20150525
        private void toolCreateCRNBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SOHeaderEntity selectedHeader = SelectedHeader;
                if (selectedHeader == null)
                {
                    MsgBox.Warn("未选中单据！");
                    return;
                }
                if (selectedHeader.Status == "68")
                {
                    FrmCreateCRNBill frm = new FrmCreateCRNBill(selectedHeader);
                    frm.ShowDialog();
                }
                else
                {
                    MsgBox.Warn("只有<发货完成>的出库单才能创建退货单！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 关联车辆 彭伟 2015-07-14
        /// </summary>
        private void toolJoinVehicle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null || selectedHeader.DelayMark != 1)
            {
                MsgBox.Warn("请选择延时发货的订单!");
                return;
            }
            using (FrmChooseVehicle frmChooseVehicle = new FrmChooseVehicle(ConvertUtil.ToInt(selectedHeader.ShipNO)))
            {
                if (frmChooseVehicle.ShowDialog() != DialogResult.OK || frmChooseVehicle.SelectedVehicle == null)
                    return;
                //int result = this.soDal.JoinBillNOAndVehicle(
                //    ConvertUtil.ToString(frmChooseVehicle.SelectedVehicle.ID),
                //    selectedHeader.BillID);
                //if (result < 1)
                //{
                //    MsgBox.Warn("关联失败，请重试！");
                //    return;
                //}
                //myPre.Query(1, DateTime.Now, DateTime.Now);
                //MsgBox.OK("关联成功。");
            }
        }

        private void gvDetails_RowDoubleClick(object sender, EventArgs e)
        {
            //SOHeaderEntity header = this.gvHeader.GetFocusedRow() as SOHeaderEntity;
            //SODetailEntity detail = this.gvDetails.GetFocusedRow() as SODetailEntity;
            //if (header == null || detail == null)
            //    return;
            //if ((header.Status != "60" && header.Status != "61") || detail.Qty != 0)
            //{
            //    MsgBox.Err("目前只允许修改还未生成拣货计划的订单并且订购量为0的商品！");
            //    return;
            //}
            //using (FrmInputNumeral frmInput = new FrmInputNumeral(detail.Qty))
            //{
            //    if (frmInput.ShowDialog() != DialogResult.OK)
            //        return;
            //    using (FrmTempAuthorize frmTempAuthorize = new FrmTempAuthorize("管理员"))
            //    {
            //        if (frmTempAuthorize.ShowDialog() != DialogResult.OK)
            //            return;
            //        if (SODal.UpdateDetailQtyByID(detail.DetailID, frmInput.Qty) < 1)
            //            return;
            //        this.ShowFocusDetail();
            //        // 记录日志
            //        LogDal.Insert(ELogType.修改数量, frmTempAuthorize.AuthUserCode, header.BillNO, "修改数量", "出库单管理", JsonConvert.SerializeObject(detail));
            //    }
            //}
        }
        
    }
}