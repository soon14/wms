using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    /// <summary>
    /// 订单的物料明细行
    /// 包含入库、出库、退库
    /// </summary>
    public class PODetailEntity : MaterialEntity
    {
        [ColumnName("ID")]
        public int DetailID { get; set; }

        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        [ColumnName("QTY")]
        public int PlanQty
        {
            set;
            get;
        }

        /// <summary>
        /// 实收数量
        /// </summary>
        [ColumnName("PUT_QTY")]
        public int PutQty
        {
            set;
            get;
        }

        /// <summary>
        /// 单价
        /// </summary>
        [ColumnName("PRICE")]
        public decimal Price
        {
            set;
            get;
        }

        public decimal PutAmount { get { return Math.Round( Price * PutQty,4); } }

        /// <summary>
        /// 按计划数量得到的金额合计 = 计划数量 * 单价
        /// </summary>
        public decimal? PlanAmount { get { return Math.Round(PlanQty * Price, 4); } }

        /// <summary>
        /// 批号
        /// </summary>
        [ColumnName("BATCH_NO")]
        public string BatchNO
        {
            set;
            get;
        }

        /// <summary>
        /// 效期
        /// </summary>
        [ColumnName("EXP_DATE")]
        public string ExpDate
        {
            set;
            get;
        }

        /// <summary>
        /// 规格
        /// </summary>
        [ColumnName("SPEC")]
        public string Spec { get; set; }

        /// <summary>
        /// 单位及规格
        /// </summary>
        public string SpecAndUnit
        {
            get
            {
                return Spec + "*" + base.UnitName;
            }
        }

        /// <summary>
        /// 不要复制物料的备注信息、计划数量信息，也就是Line的自有信息不要复制
        /// </summary>
        /// <param name="material"></param>
        public virtual void Copy(MaterialEntity material)
        {
            this.MaterialCode = material.MaterialCode;
            this.MaterialName = material.MaterialName;
            this.MaxStockQty = material.MaxStockQty;
            this.MinStockQty = material.MinStockQty;
            this.BrandCode = material.BrandCode;
            this.UnitGrpCode = material.UnitGrpCode;
            this.UnitGrpName = material.UnitGrpName;
            this.UnitCode = material.UnitCode;
            this.UnitName = material.UnitName;
            this.Remark = material.Remark;
            this.IsActive = material.IsActive;
        }

        /// <summary>
        /// 不要初始化备注信息、计划数量信息
        /// </summary>
        public virtual void Init()
        {
            this.MaterialCode = null;
            this.MaterialName = null;
            this.MaxStockQty = 0;
            this.MinStockQty = 0;
            this.BrandCode = null;
            this.UnitGrpCode = null;
            this.UnitGrpName = null;
            this.UnitCode = null;
            this.UnitName = null;
            this.Remark = null;
            this.IsActive = "Y";
        }
    }
}
