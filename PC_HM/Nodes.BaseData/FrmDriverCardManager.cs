using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using System.Collections.Generic;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmDriverCardManager : DevExpress.XtraEditors.XtraForm
    {
        //private DriverCardDal cardStateDal = null;

        public FrmDriverCardManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            toolDesign.ImageIndex = (int)AppResource.EIcons.tree;
            //cardStateDal = new DriverCardDal();
            LoadDataAndBindGrid();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateCardState();
                    break;
                case "修改":
                    ShowEditCardState();
                    break;
                case "删除":
                    DoDeleteSelectedCardState();
                    break;
                case "打印":
                    DoPrint();
                    break;
                case "设计":
                    RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
                    RepDriverCard rep = new RepDriverCard();
                    try
                    {
                        designForm.OpenReport(rep, rep.RepFileName);
                        designForm.ShowDialog();
                        designForm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Err(ex.Message);
                    }
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }

        private void DoPrint()
        {
            int[] selectedIndex = gridView1.GetSelectedRows();
            if (selectedIndex.Length == 0)
            {
                MsgBox.Warn("请选中要打印的行。");
                return;
            }

            if (MsgBox.AskOK("确定开始打印吗？") == DialogResult.OK)
            {
                List<DriverCardEntity> driverCard = new List<DriverCardEntity>();
                foreach (int i in selectedIndex)
                {
                    if (i >= 0)
                        driverCard.Add(gridView1.GetRow(i) as DriverCardEntity);
                }
                RepDriverCard repContianer = new RepDriverCard(driverCard, 1);
                repContianer.Print();
            }
        }

        ///<summary>
        ///查询所有送货牌
        ///</summary>
        ///<returns></returns>
        public List<DriverCardEntity> GetAllCardState()
        {
            List<DriverCardEntity> list = new List<DriverCardEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("whCode=").Append(warehouseCode).Append("&");
                //loStr.Append("state=").Append(state);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllCardState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllCardState bill = JsonConvert.DeserializeObject<JsonGetAllCardState>(jsonQuery);
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
                foreach (JsonGetAllCardStateResult jbr in bill.result)
                {
                    DriverCardEntity asnEntity = new DriverCardEntity();
                    #region
                    asnEntity.CardNO = jbr.cardNo;
                    asnEntity.CardState = jbr.cardState;
                    asnEntity.HeaderID = Convert.ToInt32(jbr.headerId);
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

        private void LoadDataAndBindGrid() 
        {
            try
            {
                bindingSource1.DataSource = GetAllCardState();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        DriverCardEntity SelectedCardStateRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as DriverCardEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad() 
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 增加
        /// </summary>
        private void DoCreateCardState() 
        {
            FrmDriverCardEdit frmCardStateEdit = new FrmDriverCardEdit();
            frmCardStateEdit.DataSourceChanged += OnCreateChanage;
            frmCardStateEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditCardState()
        {
            //DriverCardEntity CardStateEntity = SelectedCardStateRow;
            //if (CardStateEntity == null)
            //{
            //    MsgBox.Warn("没有要修改的数据。");
            //    return;
            //}

            //FrmDriverCardEdit frmCardStateEdit = new FrmDriverCardEdit(CardStateEntity);
            //frmCardStateEdit.DataSourceChanged += OnEditChanage;
            //frmCardStateEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            DriverCardEntity newEntity = (DriverCardEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelectedCardState()
        {
            // DriverCardEntity removeEntity = SelectedCardStateRow;
            //if (removeEntity == null)
            //{
            //    MsgBox.Warn("没有要删除的数据。");
            //    return;
            //}

            //if (MsgBox.AskOK(string.Format("确定要删除送货牌编号“({0}){1}”吗？", removeEntity.CardNO, removeEntity.CardState)) == DialogResult.OK)
            //{
            //    int ret = cardStateDal.Delete(removeEntity.CardNO);
            //    if (ret == 1)
            //    {
            //        bindingSource1.Remove(removeEntity);
            //    }
            //    else
            //        MsgBox.Warn("删除失败。");
            //}
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditCardState();
        }
    }
}
