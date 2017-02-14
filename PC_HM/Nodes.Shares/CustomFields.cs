using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.DBHelper;
using Nodes.Entities;

namespace Nodes.Shares
{
    public class CustomFields
    {
        /// <summary>
        /// 物料备用字段的定制信息
        /// </summary>
        private static List<CustomFieldEntity> materialCustomFields = null;
        public static List<CustomFieldEntity> MaterialCustomFields
        {
            get
            {
                if (materialCustomFields == null)
                    materialCustomFields = BaseCodeDal.GetCustomFields(BaseCodeConstant.MATERIAL_CUSTOM_FIELD);

                return materialCustomFields;
            }
            set
            {
                materialCustomFields = value;
            }
        }

        public static void AppendMaterialFields(GridView gridView)
        {
            List<CustomFieldEntity> customFields = MaterialCustomFields;
            foreach (CustomFieldEntity field in customFields)
            {
                if (field.IsActive == "Y")
                {
                    gridView.Columns.Add(
                        new GridColumn()
                        {
                            Caption = field.FieldDesc,
                            FieldName = field.FieldName,
                            VisibleIndex = gridView.Columns.Count
                        });
                }
            }
        }

        /// <summary>
        /// 批号、效期字段启用
        /// </summary>
        private static List<CustomFieldEntity> lotExpCustomFields = null;
        public static List<CustomFieldEntity> LotExpCustomFields
        {
            get
            {
                if (lotExpCustomFields == null)
                    lotExpCustomFields = BaseCodeDal.GetCustomFields(BaseCodeConstant.LOT_EXP_FIELD);

                return lotExpCustomFields;
            }
            set
            {
                lotExpCustomFields = value;
            }
        }

        public static void AppendLotExpFields(GridView gridView, bool allowEdit)
        {
            List<CustomFieldEntity> customFields = LotExpCustomFields;
            foreach (CustomFieldEntity field in customFields)
            {
                if (field.IsActive == "Y")
                {
                    GridColumn col = new GridColumn();
                    col.Caption = field.FieldDesc;
                    col.FieldName = field.FieldName;
                    col.VisibleIndex = gridView.Columns.Count;

                    if (allowEdit)
                    {
                        col.OptionsColumn.AllowEdit = allowEdit;
                        col.AppearanceCell.ForeColor = System.Drawing.Color.Green;
                        col.AppearanceCell.Options.UseForeColor = true;
                        col.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
                        col.AppearanceHeader.Options.UseForeColor = true;
                    }

                    gridView.Columns.Add(col);
                }
            }
        }

        public static BaseCodeEntity approveType = null;
        public static BaseCodeEntity ApproveType
        {
            get
            {
                if (approveType == null)
                    approveType = BaseCodeDal.GetItemList(BaseCodeConstant.APPROVE_TYPE)[0];

                return approveType;
            }
        }
    }
}
