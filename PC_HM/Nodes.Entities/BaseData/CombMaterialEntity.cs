using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    public class CombMaterialEntity : MaterialEntity, ICloneable
    {
        #region Model

        [ColumnName("ID")]
        public string ID
        {
            set;
            get;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [ColumnName("MATERIAL_COD")]
        public new string MaterialCode
        {
            set;
            get;
        }

        /// <summary>
        ///组分料编号
        /// </summary>
        [ColumnName("COMB_COD")]
        public string CombMaterialCode
        {
            set;
            get;
        }
        #endregion Model
    }
}

