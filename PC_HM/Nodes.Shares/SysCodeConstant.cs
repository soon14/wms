using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.WMS.Shares
{
    /// <summary>
    /// 系统代码集、代码项常量
    /// </summary>
    public class SysCodeConstant
    {
        /// <summary>
        /// 入库模块-入库策略代码集编码
        /// </summary>
        public const string INBOUND_ASN_STRATEGY = "INBOUND_STRATEGY";

        /// <summary>
        /// 入库模块-到货通知单状态代码集编码
        /// </summary>
        public const string INBOUND_ASN_STATUS = "INBOUND_ASN_STATUS";

        /// <summary>
        /// 入库模块-到货通知单类型代码集编码
        /// </summary>
        public const string INBOUND_ASN_TYPE = "INBOUND_ASN_TYPE";

        /// <summary>
        /// 物料分类：物料、包材、组合料
        /// </summary>
        public const string MATERIAL_TYPE = "MATERIAL_TYPE";

        /// <summary>
        /// 入库模块-收货通知单-状态-待验收
        /// </summary>
        public const string INBOUND_ASN_STATUS_AWAIT_CHECK = "100201";

        /// <summary>
        /// 入库模块-收货通知单-状态-上架完成
        /// </summary>
        public const string INBOUND_ASN_STATUS_PUTAWAY_COMPLETE = "100205";

        /// <summary>
        /// 入库模块-入库策略类型-越库入库
        /// </summary>
        public const string INBOUND_STRATEGY_CROSS_INBOUND = "100102";

        /// <summary>
        /// 存储类型-入库
        /// </summary>
        public const string INSTRORE_TYPE_INBOUND = "100601";

        /// <summary>
        /// 存储类型-出库
        /// </summary>
        public const string INSTRORE_TYPE_OUTBOUND = "100602";

        /// <summary>
        /// 基础物料
        /// </summary>
        public const string MATERIAL_TYPE_MATERIAL = "100701";

        /// <summary>
        /// 包材
        /// </summary>
        public const string MATERIAL_TYPE_PACK = "100702";

        /// <summary>
        /// 组合料
        /// </summary>
        public const string MATERIAL_TYPE_COMB = "100703";

    }
}
