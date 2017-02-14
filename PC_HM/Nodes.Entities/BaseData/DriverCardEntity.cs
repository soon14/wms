using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 司机到货后领的送货牌
    /// </summary>
    public class DriverCardEntity
    {
        /// <summary>
        /// 送货牌编码
        /// </summary>
        [ColumnName("CARD_NO")]
        public string CardNO
        {
            set;
            get;
        }

        /// <summary>
        /// 送货牌状态
        /// </summary>
        [ColumnName("CARD_STATE")]
        public string CardState
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("HEADER_ID")]
        public int HeaderID
        {
            set;
            get;
        }
    }
}
