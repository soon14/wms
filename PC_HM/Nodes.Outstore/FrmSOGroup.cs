using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.Outstore
{
    public partial class FrmSOGroup : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = null;
        private WarehouseDal whDal = null;
        private List<SoGroupEntity> lstUnGroup;
        private List<SoGroupEntity> lstGroup;
        private List<SoGroupEntity> lstMarker;

        public FrmSOGroup()
        {
            InitializeComponent();

            soDal = new SODal();
            whDal = new WarehouseDal();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //lstUnGroup = soDal.QueryBillsForUnGroup();
                lstGroup = soDal.QueryBillsForGroup();
                gridUnGroup.DataSource = lstUnGroup;
                gridGroup.DataSource = lstGroup;

                btnLookALL_Click(sender, e);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnLookALL_Click(object sender, EventArgs e)
        {
            try
            {
                this.wbMap.Url = new Uri(Path.Combine(Application.StartupPath, "BaiduMap.htm?"));
                lstMarker = new List<SoGroupEntity>();
                foreach (SoGroupEntity itm in lstUnGroup)
                {
                    lstMarker.Add(itm);
                }
                foreach (SoGroupEntity itm in lstGroup)
                {
                    lstMarker.Add(itm);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void btnLookSelected_Click(object sender, EventArgs e)
        {
            try
            {
                this.wbMap.Url = new Uri(Path.Combine(Application.StartupPath, "BaiduMap.htm?"));
                lstMarker = new List<SoGroupEntity>();

                GetSelectedMarker(gridView2);
                GetSelectedMarker(gridView1);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        //获取选中数据的地图位置，便于在地图上做标记
        private void GetSelectedMarker(DevExpress.XtraGrid.Views.Grid.GridView gView)
        {
            if (gView.SelectedRowsCount > 0)
            {
                SoGroupEntity itm = new SoGroupEntity();
                int[] iHandle = gView.GetSelectedRows();
                for (int i = 0; i < iHandle.Length; i++)
                {
                    if (iHandle[i] < 0) continue;
                    itm = gView.GetRow(iHandle[i]) as SoGroupEntity;
                    lstMarker.Add(itm);
                }
            }
        }

        private void btnUpdateGroup_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0 || gridView2.SelectedRowsCount > 0)
            {
                FrmSelectGroup frm = new FrmSelectGroup();
                frm.UpdateGroup = UpdateGroup;
                frm.ShowDialog();
            }
            else
            {
                MsgBox.Warn("没有选中行，请选中数据行再进行此操作！");
            }
        }

        //给选中的数据更改分组号
        private void UpdateGroup(string GroupNo)
        {
            try
            {
                SetGroupNo(gridView2, GroupNo);
                SetGroupNo(gridView1, GroupNo);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void SetGroupNo(DevExpress.XtraGrid.Views.Grid.GridView gView, string GroupNo)
        {
            if (gView.SelectedRowsCount > 0)
            {
                int[] iHandle = gView.GetSelectedRows();
                for (int j = 0; j < iHandle.Length; j++)
                {
                    if (iHandle[j] < 0) continue;
                    gView.SetRowCellValue(iHandle[j], "PositionType", GroupNo);
                }
                gView.RefreshData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MsgBox.AskOK("确定保存当前单据分组结果吗？") != DialogResult.OK) return;
                Cursor.Current = Cursors.WaitCursor;
                string curGroupNo = "";
                string prvGroupNo = "";
                int maxGroupID = -1;
                int billID = -1;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    curGroupNo = gridView1.GetRowCellValue(i, "PositionType").ToString();
                    if (string.IsNullOrEmpty(prvGroupNo) || prvGroupNo != curGroupNo)
                    {
                        maxGroupID = soDal.GetMaxGroupID();
                    }
                    billID = Convert.ToInt32(gridView1.GetRowCellValue(i, "BillID"));
                    soDal.ManualUpdateGroupNo(billID, curGroupNo, maxGroupID);
                    prvGroupNo = curGroupNo;
                }
                for (int i = 0; i < gridView2.DataRowCount; i++)
                {
                    curGroupNo = gridView2.GetRowCellValue(i, "PositionType").ToString();
                    if (string.IsNullOrEmpty(prvGroupNo) || prvGroupNo != curGroupNo)
                    {
                        maxGroupID = soDal.GetMaxGroupID();
                    }
                    billID = Convert.ToInt32(gridView2.GetRowCellValue(i, "BillID"));
                    soDal.ManualUpdateGroupNo(billID, curGroupNo, maxGroupID);
                    prvGroupNo = curGroupNo;
                }
                MsgBox.OK("保存完成！");
                OnFormLoad(null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void wbMap_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            decimal centerX = (decimal)116.402832;
            decimal centerY = (decimal)39.915600;

            WarehouseEntity warehouse = whDal.GetWarehouseByCode(GlobeSettings.LoginedUser.WarehouseCode);
            centerX = warehouse.XCoor;
            centerY = warehouse.YCoor;
            this.wbMap.Document.InvokeScript("getCenterPostion", new object[] { centerX, centerY, "北京市" });

            foreach (SoGroupEntity itm in lstMarker)
            {
                this.wbMap.Document.InvokeScript("bdGEO", new object[] { itm.CustomerAddress });
            }
        }

        private void wbMap_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData == Keys.F5)
            //{
            //    OnFormLoad(sender, e);
            //}
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "BillNO")
                {
                    object objBillID = gridView2.GetRowCellValue(e.RowHandle, "BillID");
                    if (objBillID == null) return;
                    FrmSODetails frm = new FrmSODetails(Convert.ToInt32(objBillID.ToString()));
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "BillNO")
                {
                    object objBillID = gridView1.GetRowCellValue(e.RowHandle, "BillID");
                    if (objBillID == null) return;
                    FrmSODetails frm = new FrmSODetails(Convert.ToInt32(objBillID.ToString()));
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void gridView2_GotFocus(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
        }

        private void gridView1_GotFocus(object sender, EventArgs e)
        {
            gridView2.ClearSelection();
        }

        
    }
}