using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Shares;
using DevExpress.Utils;
using Nodes.Icons;
using Nodes.Utils;
using Nodes.Controls;

namespace Nodes.Instore
{
    public partial class FrmPoSecondApprove : DevExpress.XtraEditors.XtraForm, IPOManager
    {
        PODal poDal = null;
        POQueryDal poQueryDal = null;
        PrePOManager prePOManager = null;

        public FrmPoSecondApprove()
        {
            InitializeComponent();
            prePOManager = new PrePOManager(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)IconHelper.Images.ok;
            barButtonItem2.ImageIndex = (int)IconHelper.Images.back;
            barButtonItem3.ImageIndex = (int)IconHelper.Images.information;
            barButtonItem4.ImageIndex = (int)IconHelper.Images.approved;
            barButtonItem5.ImageIndex = (int)IconHelper.Images.search;
            barButtonItem6.ImageIndex = (int)IconHelper.Images.refresh;
            barButtonItem7.ImageIndex = (int)IconHelper.Images.message;
            barButtonItem8.ImageIndex = (int)IconHelper.Images.log;
            
            poDal = new PODal();
            poQueryDal = new POQueryDal();

            //如果没有二审权限，把一些按钮的功能禁掉
            EnableToolbarByApproveType();

            //初始化显示的物料明细字段
            ucPoBody1.RemoveColumn("RealQty");
            ucPoBody1.CustomGridCaption();

            //默认列出等待二审的单据
            ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_FIRST_APPROVED, "单据状态=等待审批（二审）的采购单");
        }

        //查看是否支持二级审批
        private void EnableToolbarByApproveType()
        {
            //try
            //{
            //    //读取审批方式，默认为仅一审，只能是一条数据
            //    List<BaseCodeEntity> approveTypes = BaseCodeDal.GetItemList(BaseCodeConstant.APPROVE_TYPE);
            //    string approveType = approveTypes[0].ItemValue;
            //    if (approveType == "1")
            //    {
            //        barButtonItem1.Enabled = barButtonItem2.Enabled = barButtonItem3.Enabled =
            //            barButtonItem4.Enabled = barButtonItem7.Enabled = false;

            //        MsgBox.Warn("系统当前设置不支持二级审批，只具有查看功能。");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err(ex.Message);
            //}
        }

        //处理点击菜单事件
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (ConvertUtil.ToString(e.Item.Tag))
            {
                case "刷新":
                    ReloadPO();
                    break;
                case "审批":
                    prePOManager.SecondApproveBills(ucPoBody1.FocusedHeaders);
                    break;
                case "反审":
                    prePOManager.CancelSecondApproveBills(ucPoBody1.FocusedHeaders);
                    break;
                case "所有待审批":
                    ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_FIRST_APPROVED, "单据状态=等待审批（二审）的采购单");
                    break;
                case "所有已审批":
                    ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_SECOND_APPROVED, "单据状态=已经审批（二审），但是尚未开始收货的采购单");
                    break;
                case "编写备注":
                    prePOManager.EditRemark(ucPoBody1.FocusedHeader);
                    break;
                case "单据日志":
                    prePOManager.ViewLog(ucPoBody1.FocusedHeader);
                    break;
            }
        }

        public List<POBodyEntity> GetFocusedHeaders()
        {
            return ucPoBody1.FocusedHeaders;
        }

        public void ReloadPO()
        {
            ucQueryCondition.Reload();
        }

        /// <summary>
        /// 更新选中单据的状态、备注、颜色标记等
        /// </summary>
        public void RefreshState()
        {
            ucPoBody1.RefreshMeMemory();
        }

        private void OnQueryCompleted(List<POBodyEntity> dataSource)
        {
            ucPoBody1.DataSource = dataSource;
            ucPoBody1.ShowCondition(ucQueryCondition.QueryCondition);
            ucPoBody1.ShowTimeMsg(ucQueryCondition.ElapsedTime);

            popupControlContainer1.HidePopup();
        }

        private void popupControlContainer1_Popup(object sender, EventArgs e)
        {
            ucQueryCondition.LoadDataSource();
        }
    }
}