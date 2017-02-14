using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonJsonGetTaskDetailResult
    {
        #region 146
        [ColumnName("cardState")]
        public string cardState
        {
            get;
            set;
        }
        [ColumnName("itemDesc")]
        public string itemDesc
        {
            get;
            set;
        }
        [ColumnName("cardNo")]
        public string cardNo
        {
            get;
            set;
        }
        #endregion

        #region 140
        [ColumnName("isCode")]
        public string isCode
        {
            get;
            set;
        }
        [ColumnName("lcState")]
        public string lcState
        {
            get;
            set;
        }
        [ColumnName("creator")]
        public string creator
        {
            get;
            set;
        }
        [ColumnName("createDate")]
        public string createDate
        {
            get;
            set;
        }
        [ColumnName("whCode")]
        public string whCode
        {
            get;
            set;
        }
        [ColumnName("remark")]
        public string remark
        {
            get;
            set;
        }
        #endregion

        #region 142
        [ColumnName("supplier")]
        public string supplier
        {
            get;
            set;
        }
        #endregion

        #region 143
        [ColumnName("lcCode")]
        public string lcCode
        {
            get;
            set;
        }
        [ColumnName("pickQty")]
        public string pickQty
        {
            get;
            set;
        }
        #endregion

        #region 144
        [ColumnName("skuName")]
        public string skuName
        {
            get;
            set;
        }
        [ColumnName("sourceLcCode")]
        public string sourceLcCode
        {
            get;
            set;
        }
        [ColumnName("targetLcCode")]
        public string targetLcCode
        {
            get;
            set;
        }
        #endregion

        #region 147
        [ColumnName("skuCode")]
        public string skuCode
        {
            get;
            set;
        }
        [ColumnName("qty")]
        public string qty
        {
            get;
            set;
        }
        [ColumnName("umName")]
        public string umName
        {
            get;
            set;
        }
        #endregion

        #region 148-145
        [ColumnName("ctCode")]
        public string ctCode
        {
            get;
            set;
        }
        [ColumnName("vhTrainNo")]
        public string vhTrainNo
        {
            get;
            set;
        }
        [ColumnName("ctlName")]
        public string ctlName
        {
            get;
            set;
        }
        [ColumnName("billNo")]
        public string billNo
        {
            get;
            set;
        }
        [ColumnName("inVhSort")]
        public string inVhSort
        {
            get;
            set;
        }
        #endregion
    }
}
