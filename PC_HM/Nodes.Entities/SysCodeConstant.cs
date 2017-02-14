using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
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

        #region 到货通知单状态
        /// <summary>
        /// 入库模块-到货通知单状态代码集编码
        /// </summary>
        public const string ASN_STATUS = "INBOUND_ASN_STATUS";

        /// <summary>
        /// 等待到货确认
        /// </summary>
        public const string ASN_STATUS_ARRIVE_CONFIRM = "20";
        public const string ASN_STATUS_ARRIVE_CONFIRM_DESC = "等待到货确认";

        /// <summary>
        /// 入库模块-收货通知单-状态-待验收
        /// </summary>
        public const string ASN_STATUS_AWAIT_CHECK = "21";

        /// <summary>
        /// 正在验收
        /// </summary>
        public const string ASN_STATUS_CHECKING = "22";

        /// <summary>
        /// 验收完成，这是个中间状态
        /// </summary>
        public const string ASN_STATUS_CHECK_COMPLETE = "23";

        /// <summary>
        /// 正在上架
        /// </summary>
        public const string ASN_STATUS_PUTAWAY = "24";

        /// <summary>
        /// 入库模块-收货通知单-状态-收货完成
        /// </summary>
        public const string ASN_STATUS_CLOSE = "25";
        #endregion

        /// <summary>
        /// 入库模块-到货通知单类型代码集编码
        /// </summary>
        public const string INBOUND_ASN_TYPE = "INBOUND_ASN_TYPE";

        #region 收货方式：正常、越库
        public const string INBOUND_TYPE = "INBOUND_ASN_TYPE";
        public const string INBOUND_TYPE_REGULAR = "101";

        /// <summary>
        /// 越库
        /// </summary>
        public const string INBOUND_TYPE_ACROSS = "102";

        /// <summary>
        /// 虚拟入库
        /// </summary>
        public const string INBOUND_TYPE_VIRTUAL = "103";

        #endregion

        /// <summary>
        /// 物料分类：物料、包材、组分料
        /// </summary>
        public const string MATERIAL_TYPE = "MATERIAL_TYPE";

        #region 物料管理类型：序列号管理/批管理
        /// <summary>
        /// 物料管理类型：序列号管理/批管理
        /// </summary>
        public const string MATERIAL_ADMIN_TYPE = "MATERIAL_ADMIN_TYPE";
        public const string MATERIAL_ADMIN_TYPE_SN = "11";
        public const string MATERIAL_ADMIN_TYPE_SN_DESC = "1号1物";
        public const string MATERIAL_ADMIN_TYPE_BATCH = "12";
        public const string MATERIAL_ADMIN_TYPE_BATCH_DESC = "1号多物";
        #endregion


        /// <summary>
        /// 存储类型-入库
        /// </summary>
        public const string INSTRORE_TYPE_INBOUND = "601";

        /// <summary>
        /// 存储类型-出库
        /// </summary>
        public const string INSTRORE_TYPE_OUTBOUND = "602";

        /// <summary>
        /// 基础物料
        /// </summary>
        public const string MATERIAL_TYPE_MATERIAL = "701";

        /// <summary>
        /// 包材
        /// </summary>
        public const string MATERIAL_TYPE_PACK = "702";

        /// <summary>
        /// 组分料
        /// </summary>
        public const string MATERIAL_TYPE_COMB = "703";

        #region "出库相关"
        public const string SO_STATUS = "SO_STATUS";

        /// <summary>
        /// 单据类型：销售订单、采购退货单
        /// </summary>
        public const string SO_TYPE = "SO_TYPE";

        /// <summary>
        /// 拣货方式：正常出库、越库发货
        /// </summary>
        public const string PICK_TYPE = "PICK_TYPE";
        public const string PICK_TYPE_REGULAR = "1";
        public const string PICK_TYPE_ACROSS = "2";

        /// <summary>
        /// 等待拣配计算
        /// </summary>
        public const string SO_STATUS_WAITING_CREATE_PICK_PLAN = "901";

        /// <summary>
        /// 等待分派任务
        /// </summary>
        public const string SO_STATUS_WAITING_ASSIGN_TASK = "902";

        /// <summary>
        /// 等待拣货
        /// </summary>
        public const string SO_STATUS_WAITING_PICK = "903";

        /// <summary>
        /// 正在拣货
        /// </summary>
        public const string SO_STATUS_PICKING = "904";

        /// <summary>
        /// 拣货完成
        /// </summary>
        public const string SO_STATUS_PICK_COMPLETE = "905";

        /// <summary>
        /// 等待打包复核
        /// </summary>
        public const string SO_STATUS_NOT_PACK = "906";

        /// <summary>
        /// 等待打包
        /// </summary>
        public const string SO_STATUS_PACKING = "907";

        /// <summary>
        /// 发货完成
        /// </summary>
        public const string SO_STATUS_CLOSE = "908";

        #endregion

        #region "流水号的质量"
        public const string SEQ_QUALIFIED = "SEQ_STATUS";
        public const string SEQ_STATUS_QUALIFIED = "401";
        public const string SEQ_STATUS_QUALIFIED_DESC = "合格";
        public const string SEQ_STATUS_UNQUALIFIED = "402";
        public const string SEQ_STATUS_UNQUALIFIED_DESC = "不合格";

        #endregion

        #region "货区功能划分"
        /// <summary>
        /// 存储区编码
        /// </summary>
        public const string ZONE_TYPE_STOCK = "00";

        /// <summary>
        /// 暂存区代码
        /// </summary>
        public const string ZONE_TYPE_TEMP = "03";

        #endregion

        #region "存储状态"

        /// <summary>
        /// 刚刚验收，在验收区
        /// </summary>
        public const string STORAGE_STATUS_CHECK = "501";

        /// <summary>
        /// 在库，已上架
        /// </summary>
        public const string STORAGE_STATUS_PUTAWAY = "502";

        #endregion

        #region 盘点单状态
        public const string COUNT_STATE = "COUNT_STATUS";
        public const string COUNT_STATE_NOT_START = "31";
        public const string COUNT_STATE_DOING = "32";
        public const string COUNT_STATE_END = "33";
        public const string COUNT_STATE_CLOSE = "34";
        #endregion


        #region 库存占用状态
        public const string OCCUPY_STATUS = "OCCUPY_STATUS";
        public const string OCCUPY_STATUS_OK = "41";
        public const string OCCUPY_STATUS_CANCLE = "42";
        #endregion
    }
}
