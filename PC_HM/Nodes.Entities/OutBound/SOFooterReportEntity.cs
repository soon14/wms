using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    /// <summary>
    /// 报表尾部
    /// </summary>
    [Serializable]
    public class SOFooterReportEntity
    {
        #region 构造函数
        public SOFooterReportEntity() { }
        public SOFooterReportEntity(string value3, string value4)
            : this()
        {
            this.Value3 = value3;
            this.Value4 = value4;
        }
        public SOFooterReportEntity(string value1, string value2, string value3, string value4)
            : this(value3, value4)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }
        #endregion

        #region 属性
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        #endregion
    }
}
