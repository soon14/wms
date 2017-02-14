using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    /// <summary>
    /// 系统代码集、代码项常量
    /// </summary>
    public class BaseCodeConstant
    {
        /// <summary>
        /// 货区类型编码
        /// </summary>
        public const string ZONE_TYPE = "101";

        /// <summary>
        /// 采购单状态
        /// </summary>
        public const string PO_STATE = "102";

        /// <summary>
        /// 采购单类型/收货单类型
        /// </summary>
        public const string PO_TYPE = "103";

        #region 收货单状态
        /// <summary>
        /// 收货单状态
        /// </summary>
        public const string ASN_STATE = "104";
        public const string ASN_STATE_DENG_DAI_DAO_HUO = "20";

        public const string ASN_STATE_CODE_COMPLETE = "27";
        public const string ASN_STATE_DESC_COMPLETE = "收货完成";
        #endregion

        /// <summary>
        /// 收货方式
        /// </summary>
        public const string INSTORE_TYPE = "105";

        /// <summary>
        /// 送货牌状态
        /// </summary>
        public const string CARD_STATE = "106";
        public const string CARD_STATE_KONG_XIAN = "40";//空闲
        public const string CARD_STATE_ZAI_YONG = "41"; //在用

        public const string CTL_STATE = "123";
        public const string CTL_STATE_KONG_XIAN = "90";//空闲
        public const string CTL_STATE_ZAI_YONG = "91";//占用


        /// <summary>
        /// 容器类型
        /// </summary>
        public const string CONTAINER_TYPE = "107";
        
        /// <summary>
        /// 物料字段定制
        /// </summary>
        public const string MATERIAL_CUSTOM_FIELD = "MATERIAL";

        /// <summary>
        /// 料号和效期定制
        /// </summary>
        public const string LOT_EXP_FIELD = "USE_LOT_EXP";

        #region "发货单状态"
        /// <summary>
        /// 销货单状态
        /// </summary>
        public const string SO_STATE = "108";

        /// <summary>
        /// 等待排序
        /// </summary>
        public const string SO_WAIT_GROUP = "60";

        /// <summary>
        /// 等待拣配
        /// </summary>
        public const string SO_WAIT_PICK = "61";

        /// <summary>
        /// 等待分派任务
        /// </summary>
        public const string SO_WAIT_TASK = "62";

        /// <summary>
        /// 等待拣货
        /// </summary>
        public const string SO_WAIT_PICKING = "63";

        /// <summary>
        /// 正在拣货
        /// </summary>
        public const string SO_DO_PICKING = "64";

        /// <summary>
        /// 等待称重
        /// </summary>
        public const string SO_WAIT_WEIGHT = "65";
        /// <summary>
        /// 等待装车
        /// </summary>
        public const string SO_WAIT_LOADING = "66";


        public const string SO_STATUS_CLOSE = "68";

        /// <summary>
        /// 出库方式
        /// </summary>
        public const string OUTSTORE_TYPE = "111";

        /// <summary>
        /// 越库发货
        /// </summary>
        public const string OUT_TYPE_CROSS = "110";

        /// <summary>
        /// 称重发货
        /// </summary>
        public const string OUT_TYPE_SCAN = "111";

        /// <summary>
        /// 杂项出库，不称重，但是需要扫描下架
        /// </summary>
        public const string OUT_TYPE_OTHER = "112";

        /// <summary>
        /// 销货单类型
        /// </summary>
        public const string SO_TYPE = "112";

        /// <summary>
        /// 销售订单
        /// </summary>
        public const string SO_TYPE_SALES = "120";
        #endregion

        /// <summary>
        /// 盘点单状态-未开始
        /// </summary>
        public const string COUNT_STATE = "113";
        public const string COUNT_STATE_NOT_START = "130";
        public const string COUNT_STATE_DOING = "131";
        public const string COUNT_STATE_CLOSE = "132";

        /// <summary>
        /// 移库单类型
        /// </summary>
        public const string BILL_TYPE_TRANS = "160";

        /// <summary>
        /// 补货单类型
        /// </summary>
        public const string BILL_TYPE_REPLENISH = "161";

        /// <summary>
        /// 退货原因，Add by ZXQ 20150602
        /// </summary>
        public const string RETURN_REASON = "117";

        public const string SKU_TYPE = "118";
        /// <summary>
        /// 权重系数
        /// </summary>
        public const string WEIGHT_RAIDO = "120";

        public const string ROLE_RECEIVE = "收货清点员";
        public const string ROLE_CHECK = "收货复核员";
        public const string ROLE_PUT = "上架员";
        /// <summary>
        /// 盘点任务
        /// </summary>
        public const string TASK_COUNT = "140";
        /// <summary>
        /// 移库任务
        /// </summary>
        public const string TASK_TRANS = "141";
        /// <summary>
        /// 上架任务
        /// </summary>
        public const string TASK_PUT = "142";
        /// <summary>
        /// 拣货任务
        /// </summary>
        public const string TASK_PICK = "143";
        /// <summary>
        /// 补货/移货任务
        /// </summary>
        public const string TASK_TRANS_BUHUO = "144";
        /// <summary>
        /// 装车任务
        /// </summary>
        public const string TASK_LOADING = "145";
        /// <summary>
        /// 清点任务
        /// </summary>
        public const string TASK_RECEIVE = "146";
        /// <summary>
        /// 复核任务
        /// </summary>
        public const string TASK_CHECK = "147";
        /// <summary>
        /// 称重任务
        /// </summary>
        public const string TASK_WEIGHT = "148";
    }
}
