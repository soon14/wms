using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 资产分类实体类
    /// </summary>
    public class AreaEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ColumnName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("PARENT_ID")]
        public int ParentID { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        [ColumnName("AR_CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("AR_NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 显示到界面上的文本，同时也作为模糊搜索的字段
        /// </summary>
        public string DisplayName
        {
            get
            {
                return string.Format("[{0}] {1}", Code, Name);
            }
        }
    }
}
