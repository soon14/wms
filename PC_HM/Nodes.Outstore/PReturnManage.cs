using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Shares;
using System.Windows.Forms;
using Nodes.UI;
using System.Threading;
using Nodes.Entities.HttpEntity;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public class PReturnManage
    {
        private ReturnManageDal soDal;
        private IReturnManage IParent;
        
        public PReturnManage(IReturnManage iso)
        {
            IParent = iso;
            soDal = new ReturnManageDal();
        }
        /// <summary>
        /// 返回单据的编号字符串，以逗号隔开
        /// </summary>
        /// <param name="bills"></param>
        /// <returns></returns>
        string GetBillNOs(List<ReturnHeaderEntity> bills)
        {
            string billNOs = string.Empty;
            foreach (ReturnHeaderEntity header in bills)
                billNOs += header.BillNo + ",";

            return billNOs.TrimEnd(',');
        }

        #region 处理查询
        private int QueryType = 1; //1：所有未完成；2：未完成（仅限7日内的单据）；3：自定义
        private string BillNO, BillState, ItemDesc, ReturnDriver, Customer, SalesMan, ShipNO;
        private DateTime DateFrom, DateTo;

        public void Requery()
        {
            BindQueryResult(QueryType, BillNO, Customer, SalesMan, ItemDesc, BillState,
                ReturnDriver, ShipNO, DateFrom, DateTo);
        }

        public void Query(int queryType, DateTime dateFrom, DateTime dateTo)
        {
            BindQueryResult(queryType, null, null, null, null, null, null, null, dateFrom, dateTo);
        }

        public void BindQueryResult(int queryType, string billNO, string customer, string salesMan, string itemDesc,
            string billStatus, string returnDriver, string shipNO, DateTime dateFrom, DateTime dateTo)
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
                List<ReturnHeaderEntity> soHeaderEntitys = null;

                if (this.QueryType == 3)
                {
                    this.BillNO = string.IsNullOrEmpty(billNO) ? null : billNO;
                    this.Customer = string.IsNullOrEmpty(customer) ? null : customer;
                    this.ItemDesc = string.IsNullOrEmpty(itemDesc) ? null : itemDesc;
                    this.SalesMan = string.IsNullOrEmpty(salesMan) ? null : salesMan;
                    this.BillState = string.IsNullOrEmpty(billStatus) ? null : billStatus;
                    this.ReturnDriver = string.IsNullOrEmpty(returnDriver) ? null : returnDriver;
                    this.ShipNO = string.IsNullOrEmpty(shipNO) ? null : shipNO;

                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    soHeaderEntitys = soDal.QueryBills(BillNO, Customer, SalesMan, ItemDesc, BillState,
                        ReturnDriver, ShipNO, DateFrom, DateTo);
                }
                else if (this.QueryType == 1)
                {
                    soHeaderEntitys = soDal.QueryBillsQuickly(null, null, null);
                }
                else
                {
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    soHeaderEntitys = soDal.QueryBillsQuickly(null, dateFrom, dateTo);
                }

                IParent.BindingGrid(soHeaderEntitys);
                IParent.ShowFocusDetail();

                IParent.ShowQueryCondition(QueryType, BillNO, Customer, SalesMan, ItemDesc, BillState,
                ReturnDriver, shipNO, DateFrom, DateTo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 删除单据
        public void DeleteReturnBill()
        {
            try
            {
                ReturnHeaderEntity header = IParent.GetFocusedBill();
                if (header == null)
                {
                    MsgBox.Warn("请选中要删除的单据。");
                    return;
                }
                if (header.StatusName != "等待清点")
                {
                    MsgBox.Warn("只有<等待清点>状态的单据才能删除。");
                    return;
                }
                if (header.BillTypeName == "系统退货单")
                {
                    MsgBox.Warn("<系统退货单>不能删除。");
                    return;
                }
                if (MsgBox.AskOK("确定要删除单据号为<" + header.BillNo + ">的退货单吗？") != DialogResult.OK) return;

                int rtn = soDal.DeleteReturnBill(header.BillID);
                if (rtn > 0)
                {
                    MsgBox.OK("删除成功！");
                    Requery();
                }
                else
                {
                    MsgBox.OK("删除失败！");
                }
                LogDal.Insert(ELogType.退货单, GlobeSettings.LoginedUser.UserName, header.BillNo, "删除退货单", "退货单管理");
            }
            catch (Exception ex)
            {
                MsgBox.OK("删除失败，原因是:" + ex.Message);
                return;
            }
        }

        #endregion
        public void PrintReturnBill()
        {
            List<ReturnHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要打印的单据。");
                return;
            }
            //foreach (ReturnHeaderEntity entity in focusedBills)
            //{
            //    if (entity.BillState != "20" && entity.BillState != "21" && entity.BillState != "22")
            //    {
            //        MsgBox.Warn(String.Format("只有<等待复核>状态之前才能打印退货单。"));
            //        return;
            //    }
            //}
            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要开始打印吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;
            string module = "退货单管理";
            foreach (ReturnHeaderEntity header in focusedBills)
            {
                RepReturn repSO = new RepReturn(header.BillID, 1, module);
                //repSO.ShowPreviewDialog();
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(50);
                    repSO.Print();//.ShowPreviewDialog();//
                }
                //更新打印标记为已打印
                this.soDal.UpdatePrintedFlag(header.BillID);
                header.Printed = 1;
            }
            IParent.RefreshHeaderGrid();
        }

        public void ModifyReturnAmount()
        {
            ReturnHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要修改的单据。");
                return;
            }
            if (header.StatusName != "等待清点")
            {
                MsgBox.Warn("只有<等待清点>状态的单据才能修改。");
                return;
            }
            FrmModifyAmount frm = new FrmModifyAmount(header);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LogDal.Insert(ELogType.退货单, GlobeSettings.LoginedUser.UserName, header.BillNo,
                    string.Format("修改退货金额；修改前：{0}  修改后：{1}", header.CrnAmount, frm.CrnAmount),
                    "退货单管理");
                Requery();
            }
        }

        public void RelatingStackInfo()
        {
            ReturnHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的单据。");
                return;
            }
            FrmRelatingStack frm = new FrmRelatingStack(header);
            frm.ShowDialog();
        }

        /// <summary>
        /// 退货单管理,关闭退货订单
        /// </summary>
        public bool CloseReturn(int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CloseReturn);
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

        public void ReturnComplete()
        {
            ReturnHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要完成的单据。");
                return;
            }
            if (header.BillState == "27")
            {
                MsgBox.Warn("该订单已经完成，请选择其他订单！");
                return;
            }
            if (MsgBox.AskOK("确定要完成该订单吗？") == DialogResult.OK)
            {
                bool result = CloseReturn(header.BillID);
                if (result)
                {
                    MsgBox.Warn("订单关闭完成！");
                    LogDal.Insert(ELogType.退货单, GlobeSettings.LoginedUser.UserName, header.BillNo, "手动完成退货单", "退货单管理");
                }
                //else
                //{
                //    MsgBox.Warn(result);
                //}
            }
        }
    }
}
