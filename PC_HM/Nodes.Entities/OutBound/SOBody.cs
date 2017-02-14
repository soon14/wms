using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// 发货单，用于报表打印
    /// </summary>
    public class SOBody
    {
        #region 变量
        private List<SODetailReportEntity> _reportDetails = null;

        private List<SODetailAttributeEntity> _reportDetailAttri = null;   // 订单优惠券
        private List<SODetailAttributeEntity> _physicalBondList = null;    // 所有实物券
        private List<SODetailAttributeEntity> _phyUsed = null;             // 使用的实物券
        private List<SODetailAttributeEntity> _phyReturn = null;           // 返回的实物券
        private List<SODetailAttributeEntity> _paymentList = null;         // 所有预付
        private List<SODetailAttributeEntity> _cashList = null;            // 现金券
        private List<SODetailAttributeEntity> _suitList = null;            // 套餐优惠
        private List<SODetailAttributeEntity> _huiFuList = null;           // 惠付通支付
        private List<SODetailAttributeEntity> _cardList = null;            // 刷卡支付
        private List<SODetailAttributeEntity> _alipayList = null;          // 支付宝支付
        private List<SODetailAttributeEntity> _bianMinList = null;         // 便民账户
        private Dictionary<string, decimal> _paymentUsedGroup = null;      // 分组后已用的预付

        private List<SOFooterReportEntity> _reportFooter1 = null;          // 报表尾部1
        private List<SOFooterReportEntity> _reportFooter2 = null;          // 报表尾部2

        private decimal _billAmount = 0.00m;                               // 下单金额
        private decimal _totalAmount = 0.00m;                              // 商品合计
        private decimal _paymentTotalAmount = 0.00m;                       // 预付总金额
        private decimal _paymentAmount = 0.00m;                            // 预付可用金额
        private decimal _phyUsedAmount = 0.00m;
        #endregion

        #region 属性
        /// <summary>
        /// 公司信息
        /// </summary>
        public CompanyEntity CompanyInfo { get; set; }

        /// <summary>
        /// 发货单的客户信息
        /// </summary>
        public CustomerEntity Customer { get; set; }

        /// <summary>
        /// 单据头
        /// </summary>
        public SOHeaderEntity Header { get; set; }

        /// <summary>
        /// 单据明细
        /// </summary>
        public List<SODetailEntity> Details { get; set; }

        public List<SODetailReportEntity> ReportDetails
        {
            get { return this._reportDetails; }
            set
            {
                this._reportDetails = value;
                if (this.ReportDetails != null && this.ReportDetails.Count > 0)
                {
                    this._reportDetails = value.FindAll(u => u.Qty != 0 && u.SuitNum != 0);
                    this._billAmount = this.ReportDetails.Sum(u =>u.Qty / (u.Qty / u.SuitNum) * u.Price);
                    this._totalAmount = this.ReportDetails.Sum(u => u.PickQtyReport);
                }
            }
        }

        public List<SODetailReportEntity> ReportDetailCopy { get; set; }
        /// <summary>
        /// 订单所有优惠券
        /// </summary>
        public List<SODetailAttributeEntity> ReportDetailAttri
        {
            get { return this._reportDetailAttri; }
            set
            {
                if (value == null)
                    return;
                this._reportDetailAttri = value;

                #region 获取实物券总金额和可用金额
                if (this.PhysicalBondList.Count > 0)
                {
                    foreach (SODetailAttributeEntity item in this.PhysicalBondList)
                    {
                        // 找出有优惠券的商品
                        SODetailReportEntity detail = this.ReportDetails.Find(
                            new Predicate<SODetailReportEntity>((d) =>
                            {
                                return d.MaterialCode == ConvertUtil.ToString(item.SkuCode);
                            }));
                        if (detail != null && detail.PickQty > 0)
                        {
                            // 计算使用的优惠券是否已经达到拣货量
                            decimal count = this.PhyUsedList.Count(new Func<SODetailAttributeEntity, bool>((d) =>
                            {
                                return ConvertUtil.ToString(d.SkuCode) == detail.MaterialCode;
                            }));
                            if (count < detail.PickQty)
                                this.PhyUsedList.Add(item);
                            else
                                this.PhyReturnList.Add(item);
                        }
                        else
                        {
                            this.PhyReturnList.Add(item);
                        }
                    }
                }
                #endregion

                #region 获取预付
                this._paymentUsedGroup = new Dictionary<string, decimal>();
                if (this.PaymentList.Count > 0 || this.ReportDetails.Count > 0)
                {
                    this.ReportDetails.ForEach(new Action<SODetailReportEntity>((item) =>
                    {
                        // 查找当前商品是否有预付(考虑到惠民给咱们的数据可能会有同样的预付给多条记录)
                        List<SODetailAttributeEntity> list = this.PaymentList.FindAll(
                            new Predicate<SODetailAttributeEntity>((yufu) =>
                            {
                                return ConvertUtil.ToString(yufu.SkuCode) == item.MaterialCode;
                            }));
                        if (list == null || list.Count == 0) return;
                        // 求预付总数量
                        SODetailAttributeEntity attr = list[0];
                        string key = string.Format("{0}_{1}", attr.BillID, attr.SkuCode);
                        decimal sum = item.PickQty - list.Sum(u => ConvertUtil.ToInt(u.Num));
                        this._phyUsedAmount += Math.Abs(sum * attr.SellPrice);
                        this._paymentUsedGroup[key] = sum;
                    }));
                }
                #endregion
            }
        }
        /// <summary>
        /// 获取实物券列表
        /// </summary>
        public List<SODetailAttributeEntity> PhysicalBondList
        {
            get
            {
                if (this._physicalBondList == null)
                    this._physicalBondList = this.GetAttrCount(2);
                return this._physicalBondList;
            }
        }
        /// <summary>
        /// 套餐优惠信息
        /// </summary>
        public List<SODetailAttributeEntity> SuitList
        {
            get
            {
                if (this._suitList == null)
                    this._suitList = this.GetAttrCount(1);
                return this._suitList;
            }
        }
        /// <summary>
        /// 获取现金券列表
        /// </summary>
        public List<SODetailAttributeEntity> CashList
        {
            get
            {
                if (this._cashList == null)
                    this._cashList = this.GetAttrCount(5);
                return this._cashList;
            }
        }
        /// <summary>
        /// 获取未使用的实物券列表
        /// </summary>
        public List<SODetailAttributeEntity> PhyReturnList
        {
            get
            {
                if (this._phyReturn == null)
                    this._phyReturn = new List<SODetailAttributeEntity>();
                return this._phyReturn;
            }
        }
        /// <summary>
        /// 获取已使用的实物券列表
        /// </summary>
        public List<SODetailAttributeEntity> PhyUsedList
        {
            get
            {
                if (this._phyUsed == null)
                    this._phyUsed = new List<SODetailAttributeEntity>();
                return this._phyUsed;
            }
        }
        /// <summary>
        /// 获取预付列表
        /// </summary>
        public List<SODetailAttributeEntity> PaymentList
        {
            get
            {
                if (this._paymentList == null)
                    this._paymentList = this.GetAttrCount(4);
                return this._paymentList;
            }
        }
        /// <summary>
        /// 获取分组后已使用的预付 key:billID_SkuCode value:PickQty - 预付数
        /// </summary>
        public Dictionary<string, decimal> PaymentUsedGroup
        {
            get
            {
                if (this._paymentUsedGroup == null)
                    this._paymentUsedGroup = new Dictionary<string, decimal>();
                return this._paymentUsedGroup;
            }
        }
        /// <summary>
        /// 获取惠付通支付列表
        /// </summary>
        public List<SODetailAttributeEntity> HuiFuList
        {
            get
            {
                if (this._huiFuList == null)
                    this._huiFuList = this.GetAttrCount(3);
                return this._huiFuList;
            }
        }
        /// <summary>
        /// 获取刷新支付列表
        /// </summary>
        public List<SODetailAttributeEntity> CardList
        {
            get
            {
                if (this._cardList == null)
                    this._cardList = this.GetAttrCount(6);
                return this._cardList;
            }
        }
        /// <summary>
        /// 获取支付宝支付列表
        /// </summary>
        public List<SODetailAttributeEntity> AlipayList
        {
            get
            {
                if (this._alipayList == null)
                    this._alipayList = this.GetAttrCount(7);
                return this._alipayList;
            }
        }
        /// <summary>
        /// 便民账户
        /// </summary>
        public List<SODetailAttributeEntity> BianMinList
        {
            get
            {
                if (this._bianMinList == null)
                    this._bianMinList = this.GetAttrCount(8);
                return this._bianMinList;
            }
        }
        /// <summary>
        /// 报表尾部信息1
        /// </summary>
        public List<SOFooterReportEntity> ReportFooter1
        {
            get
            {
                if (this._reportFooter1 == null)
                    this._reportFooter1 = new List<SOFooterReportEntity>();
                return this._reportFooter1;
            }
        }
        /// <summary>
        /// 报表尾部信息2
        /// </summary>
        public List<SOFooterReportEntity> ReportFooter2
        {
            get
            {
                if (this._reportFooter2 == null)
                    this._reportFooter2 = new List<SOFooterReportEntity>();
                return this._reportFooter2;
            }
        }
        /// <summary>
        /// 获取下单金额
        /// </summary>
        public decimal BillAmount
        {
            get { return this._billAmount; }
        }
        /// <summary>
        /// 获取商品合计金额
        /// </summary>
        public decimal TotalAmount
        {
            get { return this._totalAmount; }
        }
        /// <summary>
        /// 获取实物券总金额
        /// </summary>
        public decimal PhyTotalAmount
        {
            get { return Math.Abs(this.PhysicalBondList.Sum(item => item.SellPrice)); }
        }
        /// <summary>
        /// 获取实物券可使用金额
        /// </summary>
        public decimal PhyAmount
        {
            get { return Math.Abs(this.PhyUsedList.Sum(item => item.SellPrice)); }
        }
        /// <summary>
        /// 获取预付总金额
        /// </summary>
        public decimal PaymentTotalAmount
        {
            get { return this._paymentTotalAmount; }
        }
        /// <summary>
        /// 获取预付可用金额
        /// </summary>
        public decimal PaymentAmount
        {
            get { return this._paymentAmount; }
        }
        /// <summary>
        /// 获取现金券总金额
        /// </summary>
        public decimal CashAmount
        {
            get
            {
                return Math.Abs(this.CashList.Sum(u => u.SellPrice));
            }
        }
        /// <summary>
        /// 获取现金券总金额 字符串表示形式
        /// </summary>
        public string CashAmountStr
        {
            get
            {
                return this.CashAmount.ToString("f2");
            }
        }
        /// <summary>
        /// 应收合计，未算 现金券/预付/已支付(惠付通/支付宝/...)
        /// </summary>
        public decimal AmountReceivable
        {
            get
            {
                // 应收合计 = 商品合计 - 实物券可用金额
                return this.TotalAmount - Math.Abs(this.PhyAmount);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取指定商品的优惠券
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SODetailAttributeEntity> GetAttrCountBySku(int skuCode, int type)
        {
            List<SODetailAttributeEntity> list = new List<SODetailAttributeEntity>();
            if (this.ReportDetailAttri != null && this.ReportDetailAttri.Count != 0)
            {
                list = this.ReportDetailAttri.FindAll(
                    new Predicate<SODetailAttributeEntity>((item) =>
                    {
                        return item.SkuCode == skuCode && item.Type == type;
                    }));
            }
            return list;
        }
        /// <summary>
        /// 获取指定类型的优惠券
        /// </summary>
        /// <param name="type">0:商品 1套餐 2:实物券 3:惠付通 4:预付 5:现金券 6:刷卡 5:支付宝 5:便民账户</param>
        /// <returns></returns>
        public List<SODetailAttributeEntity> GetAttrCount(int type)
        {
            List<SODetailAttributeEntity> list = new List<SODetailAttributeEntity>();
            if (this.ReportDetailAttri != null && this.ReportDetailAttri.Count != 0)
            {
                list = this.ReportDetailAttri.FindAll(new Predicate<SODetailAttributeEntity>((item) =>
                {
                    return item.Type == type;
                }));
            }
            return list;
        }
        #endregion
    }
}
