using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Controls;
using Nodes.Shares;
using Nodes.DBHelper;
using System.Windows.Forms;

namespace Nodes.Instore
{
    public class PrePOManager
    {
        PODal poDal = null;
        POQueryDal poQueryDal = null;
        IPOManager ipo;

        public PrePOManager(IPOManager ipo)
        {
            poDal = new PODal();
            poQueryDal = new POQueryDal();
            this.ipo = ipo;
        }

        public void ViewLog(POBodyEntity focusedHeader)
        {
            if (focusedHeader == null)
            {
                MsgBox.Warn("请选中要查看的行。");
                return;
            }

            if (GetFocusedRowCount(ipo.GetFocusedHeaders()) > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            using (FrmViewBillLog frmLog = new FrmViewBillLog(focusedHeader.BillID, BaseCodeConstant.PO_STATE))
            {
                frmLog.ShowDialog();
            }
        }

        public void EditRemark(POBodyEntity focusedHeader)
        {
            if (focusedHeader == null)
            {
                MsgBox.Warn("请选中要编写备注的行。");
                return;
            }

            if (GetFocusedRowCount(ipo.GetFocusedHeaders()) > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            using (FrmPoEditRemark frmEditRemark = new FrmPoEditRemark(focusedHeader))
            {
                if (frmEditRemark.ShowDialog() == DialogResult.OK)
                    ipo.RefreshState();
            }
        }

        private int GetFocusedRowCount(List<POBodyEntity> headers)
        {
            if (headers == null)
                return 0;
            else
                return headers.Count;
        }

        private string GetFocusedBillIDs(List<POBodyEntity> headers)
        {
            if (headers == null)
                return string.Empty;

            string focusedBillIDs = string.Empty;
            foreach (POBodyEntity header in headers)
                focusedBillIDs = string.Concat(focusedBillIDs, ",", header.BillID);

            //去除第一个逗号
            return focusedBillIDs.TrimStart(',');
        }

        /// <summary>
        /// 删除选中的单据
        /// </summary>
        /// <param name="selectedHeader"></param>
        public void DeleteSelectedBill(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要删除的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_DRAFT)
                {
                    MsgBox.Warn(string.Format("单据“{0}”状态不是草稿，无法删除。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要删除吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.Delete(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                switch (result)
                {
                    case "1":
                        //成功，刷新界面即可，不再提示
                        ipo.ReloadPO();
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("删除失败，单据“{0}”不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”的状态不是草稿，无法删除。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("删除单据“{0}”的明细失败，请稍后重试。", errHeader.BillID));
                        break;
                    case "-4":
                        MsgBox.Warn(string.Format("删除单据“{0}”的表头失败，请稍后重试。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消提交选中的单据，变回草稿状态
        /// </summary>
        /// <param name="selectedHeader"></param>
        public void CancelCommitBill(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要取消提交的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_COMMITED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许取消，必须是已提交但尚未审批的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要取消提交吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.CancelCommit(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                switch (result)
                {
                    case "1":
                        //成功，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("取消失败，单据“{0}”不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”取消失败，状态必须是已提交但尚未审批的单据，请刷新列表进行确认。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("更新单据“{0}”的状态时失败，请稍后重试或联系管理员解决此问题。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 提交选中单据
        /// </summary>
        /// <param name="selectedHeader"></param>
        public void CommitBill(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要提交的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_DRAFT)
                {
                    MsgBox.Warn(string.Format("单据“{0}”已经提交，不允许多次提交。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，提交后不再允许编辑，确认要提交吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.Commit(focusedHeaders, GlobeSettings.LoginedUser.UserName, out errHeader);
                switch (result)
                {
                    case "1":
                        //成功，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("提交失败，单据“{0}”不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”已经提交，不允许多次提交。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("更新单据“{0}”的状态时失败，请稍后重试或联系管理员解决此问题。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 一审
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void FirstApproveBills(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要审批的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_COMMITED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许审批，必须是已提交并且没有一审的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要审核通过吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.FirstApprove(focusedHeaders, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode, out errHeader);
                switch (result)
                {
                    case "1":
                        //状态更新成功后，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据状态必须是仅提交，请刷新后重试。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("更新单据“{0}”状态时失败，失败原因未知，请稍后重试。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消一审
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void CancelFirstApproveBills(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要反审批的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_FIRST_APPROVED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许反审，必须是已经完成一审并且没有进行二审或开始收货的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要做反审操作吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.CancelFirstApprove(focusedHeaders, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode, out errHeader);
                switch (result)
                {
                    case "1":
                        //成功，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”反审失败，必须是“已审批(一审)通过”的单据才允许反审。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("单据“{0}”反审失败，审批人不是“您本人”，无法反审。", errHeader.BillID));
                        break;
                    case "-4":
                        MsgBox.Warn(string.Format("更新单据“{0}”状态时失败，请稍后重试。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 二审
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void SecondApproveBills(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要审批的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_FIRST_APPROVED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许审批，必须是已通过一审并且没有二审的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要审核通过吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.SecondApprove(focusedHeaders, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode, out errHeader);
                switch (result)
                {
                    case "1":
                        //状态更新成功后，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据状态必须是一审通过，请刷新后重试。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("更新单据“{0}”状态时失败，失败原因未知，请稍后重试。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消二审
        /// </summary>
        /// <param name="focusedHeaders"></param>
        public void CancelSecondApproveBills(List<POBodyEntity> focusedHeaders)
        {
            if (GetFocusedRowCount(focusedHeaders) == 0)
            {
                MsgBox.Warn("请选中要反审批的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            foreach (POBodyEntity header in focusedHeaders)
            {
                if (header.BillState != BillStateConst.PO_STATE_CODE_SECOND_APPROVED)
                {
                    MsgBox.Warn(string.Format("单据“{0}”的状态不允许反审，必须是已经完成二审并且没有开始收货的单据。", header.BillID));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("您选择了“{0}”张单据“{1}”，确认要做反审操作吗？",
                GetFocusedRowCount(focusedHeaders), GetFocusedBillIDs(focusedHeaders))) != DialogResult.OK)
                return;

            try
            {
                POBodyEntity errHeader = null;
                string result = poDal.CancelSecondApprove(focusedHeaders, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode, out errHeader);
                switch (result)
                {
                    case "1":
                        //成功，刷新界面即可，不再提示
                        ReloadStateDesc(focusedHeaders);
                        break;
                    case "-1":
                        MsgBox.Warn(string.Format("单据“{0}”操作失败，单据不存在。", errHeader.BillID));
                        break;
                    case "-2":
                        MsgBox.Warn(string.Format("单据“{0}”反审失败，必须是已通过二审的单据才允许反审。", errHeader.BillID));
                        break;
                    case "-3":
                        MsgBox.Warn(string.Format("单据“{0}”反审失败，审批人不是您本人，无法反审。", errHeader.BillID));
                        break;
                    case "-4":
                        MsgBox.Warn(string.Format("更新单据“{0}”状态时失败，请稍后重试。", errHeader.BillID));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void ReloadStateDesc(List<POBodyEntity> headers)
        {
            //从集合中取出一个单据的状态，更新集合中的其他单据的状态
            if (headers != null && headers.Count > 0)
            {
                POBodyEntity poState = poQueryDal.GetBillState(headers[0].BillID);
                headers.ForEach(h =>
                {
                    h.BillState = poState.BillState;
                    h.BillStateDesc = poState.BillStateDesc;
                });

                //刷新界面
                ipo.RefreshState();
            }
        }
    }
}
