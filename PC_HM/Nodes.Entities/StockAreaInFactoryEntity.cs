using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class StockAreaInFactoryEntity
    {
        #region Model
        private int _lineid;
        private string _factorycode;
        private string _stockareacode;
        /// <summary>
        /// 行号
        /// </summary>
        public int LineID
        {
            set { _lineid = value; }
            get { return _lineid; }
        }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode
        {
            set { _factorycode = value; }
            get { return _factorycode; }
        }
        /// <summary>
        /// 库存地点编码
        /// </summary>
        public string StockAreaCode
        {
            set { _stockareacode = value; }
            get { return _stockareacode; }
        }
        #endregion Model
    }
}
