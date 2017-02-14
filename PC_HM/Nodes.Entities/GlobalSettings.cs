using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    public class GlobalSettings
    {
        /// <summary>
        /// 正常拣货系数
        /// </summary>
        public static decimal NormalPickRatio = 0.12M;
        /// <summary>
        /// 批示拣货系数
        /// </summary>
        public static decimal MostPickRatio = 0.04M;
        /// <summary>
        /// 司机配送系数（金杯）
        /// </summary>
        public static decimal DispatchingRatio1_220 = 0.5M;
        /// <summary>
        /// 司机配送系数（箱货）
        /// </summary>
        public static decimal DispatchingRatio1_221 = 0.5M;
        /// <summary>
        /// 司机助理配送系数
        /// </summary>
        public static decimal DispatchingRatio2 = 0.3M;
        /// <summary>
        /// 装车系数
        /// </summary>
        public static decimal LoadingRatio = 0.1M;
        /// <summary>
        /// 叉车系数
        /// </summary>
        public static decimal ForkliftRatio = 1;
        /// <summary>
        /// 当日排名 显示行数
        /// </summary>
        public static int DefaultShowRowsDay = 15;
        /// <summary>
        /// 当月排名 显示行数
        /// </summary>
        public static int DefaultSHowRowsMonth = 20;

    }
}
