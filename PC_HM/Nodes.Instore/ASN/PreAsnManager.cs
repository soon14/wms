using System;
using System.Collections.Generic;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Shares;
//using Nodes.DBHelper;
using System.Windows.Forms;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public class PreAsnManager
    {
        //AsnDal asnDal = null;
        //AsnQueryDal asnQueryDal = null;
        IAsnManager iasn;

        public PreAsnManager(IAsnManager iasn)
        {
            //asnDal = new AsnDal();
            //asnQueryDal = new AsnQueryDal();
            this.iasn = iasn;
        }

        public void ViewLog(AsnBodyEntity focusedHeader)
        {
            if (focusedHeader == null)
            {
                MsgBox.Warn("请选中要查看的行。");
                return;
            }

            if (GetFocusedRowCount(iasn.GetFocusedHeaders()) > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            using (FrmViewBillLog frmLog = new FrmViewBillLog(focusedHeader.BillID,focusedHeader.BillNO, "入库单据"))
            {
                frmLog.ShowDialog();
            }
        }

        public void EditRemark(AsnBodyEntity focusedHeader)
        {
            if (focusedHeader == null)
            {
                MsgBox.Warn("请选中要编写备注的行。");
                return;
            }

            if (GetFocusedRowCount(iasn.GetFocusedHeaders()) > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            using (FrmAsnEditRemark frmEditRemark = new FrmAsnEditRemark(focusedHeader))
            {
                if (frmEditRemark.ShowDialog() == DialogResult.OK)
                    iasn.RefreshState();
            }
        }

        private int GetFocusedRowCount(List<AsnBodyEntity> headers)
        {
            if (headers == null)
                return 0;
            else
                return headers.Count;
        }

        private string GetFocusedBillIDs(List<AsnBodyEntity> headers)
        {
            if (headers == null)
                return string.Empty;

            string focusedBillIDs = string.Empty;
            foreach (AsnBodyEntity header in headers)
                focusedBillIDs = string.Concat(focusedBillIDs, ",", header.BillNO);

            //去除第一个逗号
            return focusedBillIDs.TrimStart(',');
        }

        /// <summary>
        /// 删除选中的单据
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void DeleteSelectedBill(List<AsnBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要删除的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (AsnBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.ASN_STATE_CODE_NOT_ARRIVE)
                {
                    MsgBox.Warn(string.Format("单据“{0}”无法删除，只有状态为“{1}”的单据才能删除。", header.BillID, BillStateConst.ASN_STATE_DESC_NOT_ARRIVE));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要删除吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                AsnBodyEntity errHeader = null;
                //string result = asnDal.Delete(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                //switch (result)
                //{
                //    case "1":
                //        //成功，刷新界面即可，不再提示
                //        iasn.ReloadAsn();
                //        break;
                //    case "-1":
                //        MsgBox.Warn(string.Format("删除失败，单据“{0}”不存在。", errHeader.BillID));
                //        break;
                //    case "-2":
                //        MsgBox.Warn(string.Format("单据“{0}”的状态不是“{1}”，无法删除。", errHeader.BillID, BillStateConst.ASN_STATE_DESC_NOT_ARRIVE));
                //        break;
                //    case "-3":
                //        MsgBox.Warn(string.Format("删除单据“{0}”的明细失败，请稍后重试。", errHeader.BillID));
                //        break;
                //    case "-4":
                //        MsgBox.Warn(string.Format("删除单据“{0}”的表头失败，请稍后重试。", errHeader.BillID));
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 收货单据管理，收货完成
        /// </summary>
        /// <param name="focusedHeaders"></param>
        /// <param name="userName"></param>
        /// <param name="authUserCode"></param>
        /// <returns></returns>
        public bool ReceivedComplete(List<AsnBodyEntity> focusedHeaders, string userName, string authUserCode)
        {
            try
            {
                #region 组装发送数据
                string jsons = string.Empty;
                jsons += "userName=";
                jsons += userName;
                jsons += "&billIds=";
                foreach (AsnBodyEntity header in focusedHeaders)
                {
                    jsons += header.BillID;
                    jsons += ",";
                }
                jsons = jsons.Substring(0, jsons.Length - 1);
                #endregion

                #region 请求数据
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_ReceivedComplete);
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
                MsgBox.Warn(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 收货完成
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void ReceivedComplete(List<AsnBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要设置为“收货完成”的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (AsnBodyEntity header in focusedHeaders)
            {
                if (header.Printed <=0)
                {
                    MsgBox.Warn("单据" + header.BillNO + "还未打印！");
                    return;
                }
                if (header.BillState == BaseCodeConstant.ASN_STATE_CODE_COMPLETE)
                {
                    MsgBox.Warn(string.Format("单据“{0}”已经设置为“收货完成”，不允许多次执行。", header.BillNO));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要设置为“收货完成”吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                bool result = ReceivedComplete(focusedHeaders, GlobeSettings.LoginedUser.UserName, null);
                string bills = string.Empty;
                foreach (AsnBodyEntity item in focusedHeaders)
                {
                    bills += (item.BillID + ",");
                }
                if (result)
                {
                    //成功，刷新界面即可，不再提示
                    ReloadStateDesc(focusedHeaders);
                    //LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, null, bills, "收货单管理");
                }

                #region 不要这个功能了
                /*string result = asnDal.ReceivedComplete(focusedHeaders, GlobeSettings.LoginedUser.UserName, null);
                string bills = string.Empty;
                foreach (AsnBodyEntity item in focusedHeaders)
                {
                    bills += (item.BillID + ",");
                }
                if (result == "Y")
                {
                    //成功，刷新界面即可，不再提示
                    ReloadStateDesc(focusedHeaders);
                    LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, null, bills, "收货单管理");
                }
                else if (result == "-1")
                {
                    FrmTempAuthorize frm = new FrmTempAuthorize("单据审核员");
                    frm.Text += "  订单数量与实收数量不一致，请确认。";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        result = asnDal.ReceivedComplete(focusedHeaders, GlobeSettings.LoginedUser.UserName, frm.AuthUserCode);
                        if (result == "Y")
                        {
                            //成功，刷新界面即可，不再提示
                            ReloadStateDesc(focusedHeaders);
                        }
                        else
                        {
                            MsgBox.Warn(result);
                        }
                        LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, null, bills, "收货单管理", frm.AuthUserCode);
                    }
                }
                else
                {
                    MsgBox.Warn(result);
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 到货确认
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void ReceivedConfirm(List<AsnBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要执行到货确认的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (AsnBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.ASN_STATE_CODE_NOT_ARRIVE)
                {
                    MsgBox.Warn(string.Format("单据“{0}”已经执行了到货确认，不允许多次执行。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要执行到货确认吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                AsnBodyEntity errHeader = null;
                //string result = asnDal.ReceivedConfirm(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                //switch (result)
                //{
                //    case "1":
                //        //成功，刷新界面即可，不再提示
                //        ReloadStateDesc(focusedHeaders);
                //        break;
                //    case "-1":
                //        MsgBox.Warn(string.Format("确认失败，单据“{0}”不存在。", errHeader.BillID));
                //        break;
                //    case "-2":
                //        MsgBox.Warn(string.Format("单据“{0}”已经执行了到货确认，不允许多次执行。", errHeader.BillID));
                //        break;
                //    case "-3":
                //        MsgBox.Warn(string.Format("更新单据“{0}”的状态时失败，请稍后重试或联系管理员解决此问题。", errHeader.BillID));
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消到货确认
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void CancelReceivedConfirm(List<AsnBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要取消确认的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (AsnBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.ASN_STATE_CODE_ARRIVED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”不允许取消，必须是已执行到货确认但尚未开始验收的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要取消确认吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                AsnBodyEntity errHeader = null;
                //string result = asnDal.CancelReceivedConfirm(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                //switch (result)
                //{
                //    case "1":
                //        //成功，刷新界面即可，不再提示
                //        ReloadStateDesc(focusedHeaders);
                //        break;
                //    case "-1":
                //        MsgBox.Warn(string.Format("取消失败，单据“{0}”不存在。", errHeader.BillID));
                //        break;
                //    case "-2":
                //        MsgBox.Warn(string.Format("单据“{0}”不允许取消，必须是已执行到货确认但尚未开始验收的单据。", errHeader.BillID));
                //        break;
                //    case "-3":
                //        MsgBox.Warn(string.Format("更新单据“{0}”的状态时失败，请稍后重试或联系管理员解决此问题。", errHeader.BillID));
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消验收
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void CancelCheck(List<AsnBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要取消验收的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (AsnBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_FIRST_APPROVED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许取消验收，必须是正在验收但是实际验收数量为0的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要取消验收吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                AsnBodyEntity errHeader = null;
                //string result = asnDal.CancelCheck(focusedHeaders, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode, out errHeader);
                //switch (result)
                //{
                //    case "1":
                //        //成功，刷新界面即可，不再提示
                //        ReloadStateDesc(focusedHeaders);
                //        break;
                //    case "-1":
                //        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据不存在。", errHeader.BillID));
                //        break;
                //    case "-2":
                //        MsgBox.Warn(string.Format("单据“{0}”反审失败，必须是“已审批(一审)通过”的单据才允许反审。", errHeader.BillID));
                //        break;
                //    case "-3":
                //        MsgBox.Warn(string.Format("单据“{0}”反审失败，审批人不是“您本人”，无法反审。", errHeader.BillID));
                //        break;
                //    case "-4":
                //        MsgBox.Warn(string.Format("更新单据“{0}”状态时失败，请稍后重试。", errHeader.BillID));
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public AsnBodyEntity GetBillState(int billID)
        {
            AsnBodyEntity tm = new AsnBodyEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillState bill = JsonConvert.DeserializeObject<JsonGetBillState>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tm;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tm;
                }
                #endregion
                List<AsnBodyEntity> list = new List<AsnBodyEntity>();
                #region 赋值数据
                foreach (JsonGetBillStateResult jbr in bill.result)
                {
                    AsnBodyEntity asnEntity = new AsnBodyEntity();
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.RowColor);
                    asnEntity.BillState = jbr.BillState;
                    asnEntity.BillStateDesc = jbr.BillStateDesc;
                    asnEntity.Remark = jbr.Remark;
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tm;
        }

        private void ReloadStateDesc(List<AsnBodyEntity> headers)
        {
            //从集合中取出一个单据的状态，更新集合中的其他单据的状态
            if (headers != null && headers.Count > 0)
            {
                AsnBodyEntity poState = GetBillState(headers[0].BillID);
                headers.ForEach(h =>
                {
                    h.BillState = poState.BillState;
                    h.BillStateDesc = poState.BillStateDesc;
                    h.LastUpdateBy = poState.LastUpdateBy;
                    h.LastUpdateDate = poState.LastUpdateDate;
                    h.RowForeColor = poState.RowForeColor;
                    h.Remark = poState.Remark;
                });

                //刷新界面
                iasn.RefreshState();
            }
        }
    }
}
