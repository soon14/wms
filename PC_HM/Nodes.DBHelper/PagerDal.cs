using System;
using System.Collections.Generic;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 用来处理分页
    /// </summary>
    public class PagerDal
    {
        private string sqlTemplate = string.Empty;
        private string SqlTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(sqlTemplate))
                {
                    IMapper map = DatabaseInstance.Instance();
                    sqlTemplate = map.GetPagingString();
                }

                return sqlTemplate;
            }
        }

        public PagerDal() {}

        public bool EnablePaging
        {
            get;
            set;
        }

        /// <summary>
        /// 显示页数
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 表名，包括视图
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 表字段FieldStr
        /// </summary>
        public string QueryFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy
        {
            get;
            set;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string WhereCondition
        {
            get;
            set;
        }

        public List<T> QueryNextPage<T>()
        {
            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("必须填写要查询的表名。");

            string sql = string.Empty;
            if (EnablePaging)
            {
                sql = SqlTemplate;
                sql = sql.Replace("{QueryField}", string.IsNullOrEmpty(QueryFieldName) ? "*" : QueryFieldName);
                sql = sql.Replace("{TableName}", TableName);
                sql = sql.Replace("{WhereCondition}", string.IsNullOrEmpty(WhereCondition) ? "" : " WHERE " + WhereCondition);
                sql = sql.Replace("{OrderByField}", string.IsNullOrEmpty(OrderBy) ? "" : " ORDER BY " + OrderBy);
                sql = sql.Replace("{PageSize}", PageSize.ToString());
                sql = sql.Replace("{RowIndexFrom}", (PageSize * (PageIndex - 1)).ToString());
                sql = sql.Replace("{RowIndexTo}", (PageSize * PageIndex).ToString());
            }
            else
            {
                sql = string.Format("SELECT {0} FROM {1}",
                    string.IsNullOrEmpty(QueryFieldName) ? "*" : QueryFieldName,
                    TableName);

                if (!string.IsNullOrEmpty(WhereCondition))
                    sql = string.Format("{0} WHERE {1}", sql, WhereCondition);

                if (!string.IsNullOrEmpty(OrderBy))
                    sql = string.Format("{0} ORDER BY {1}", sql, OrderBy);
            }

            IMapper map = DatabaseInstance.Instance();
            return map.Query<T>(sql);
        }

        public int GetTotalCount()
        {
            IMapper map = DatabaseInstance.Instance();
            string strSql = " SELECT COUNT(1) FROM " + TableName;
            if (!string.IsNullOrEmpty(WhereCondition))
                strSql += " WHERE " + WhereCondition;

            object obj = map.ExecuteScalar<object>(strSql);
            return Convert.ToInt32(obj);
        }
    }
}
