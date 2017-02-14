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
    public partial class FrmPoFirstApprove : DevExpress.XtraEditors.XtraForm, IPOManager
    {
        PODal poDal = null;
        POQueryDal poQueryDal = null;
        PrePOManager prePOManager = null;

        public FrmPoFirstApprove()
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

            //初始化显示的物料明细字段
            ucPoBody1.RemoveColumn("RealQty");
            ucPoBody1.CustomGridCaption();

            //默认列出等待一审的单据
            ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_COMMITED, "单据状态=等待审批（一审）的采购单");
        }

        #region 处理点击菜单事件
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (ConvertUtil.ToString(e.Item.Tag))
            {
                case "刷新":
                    ReloadPO();
                    break;
                case "审批":
                    prePOManager.FirstApproveBills(ucPoBody1.FocusedHeaders);
                    break;
                case "反审":
                    prePOManager.CancelFirstApproveBills(ucPoBody1.FocusedHeaders);
                    break;
                case "所有待审批":
                    ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_COMMITED, "单据状态=等待审批（一审）的采购单");
                    break;
                case "所有已审批":
                    ucQueryCondition.DoQuery(BillStateConst.PO_STATE_CODE_FIRST_APPROVED, "单据状态=已经审批（一审），但是尚未二审或开始收货的采购单");
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

        //public POHeaderEntity GetFocusedHeader()
        //{
        //    return ucPoBody1.FocusedHeader;
        //}

        /// <summary>
        /// 对于删除单据的动作，需要重新按查询条件加载
        /// </summary>
        public void ReloadPO()
        {
            ucQueryCondition.Reload();
        }

        /// <summary>
        /// 对于提交、审批等状态更新操作，只需要刷新界面显示
        /// </summary>
        public void RefreshState()
        {
            ucPoBody1.RefreshMeMemory();
        }
        #endregion

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