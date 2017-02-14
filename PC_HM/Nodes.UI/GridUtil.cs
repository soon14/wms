using System.Data;
using System.Drawing;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Utils;

namespace Nodes.UI
{
    public class GridUtil
    {
        public static ImageCollection GetCheckBoxImages()
        {
            Skin currentSkin = EditorsSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel);
            SkinElement element = currentSkin[EditorsSkins.SkinCheckBox];

            return element.Image.GetImages();
        }

        public static ImageCollection GetComboImages()
        {
            Skin currentSkin = EditorsSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel);
            SkinElement element = currentSkin[EditorsSkins.SkinSpinDown];

            return element.Image.GetImages();
        }

        public static void CheckOneGridColumn(GridView view, DataTable data, string checkedField, string filterField, Point mousePosition)
        {
            if (data == null || data.Rows.Count == 0) return;

            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                foreach (DataRow row in data.Rows)
                {
                    if (!ConvertUtil.ToBool(row[filterField])) row[checkedField] = flag;
                }
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }

        public static void CheckOneGridColumn(GridView view, DataTable data, string checkedField, Point mousePosition)
        {
            if (data == null || data.Rows.Count == 0) return;

            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                foreach (DataRow row in data.Rows) row[checkedField] = flag;
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }

        static DataView GetFilteredData(GridView view)
        {
            if (view == null) return null;
            if (view.ActiveFilter == null || !view.ActiveFilterEnabled
                || view.ActiveFilter.Expression == "")
                return view.DataSource as DataView;

            DataTable table = ((DataView)view.DataSource).Table;
            DataView filteredDataView = new DataView(table);
            filteredDataView.RowFilter = view.ActiveFilter.Expression;
            return filteredDataView;
        }

        public static void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            DataView _dataView = GetFilteredData(view);

            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                foreach (DataRowView drv in _dataView) drv[checkedField] = flag;
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
    }
}
