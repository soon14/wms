using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
   public class ChannelEntity
    {
       /// <summary>
       /// 库房编码
       /// </summary>
       [ColumnName("WH_CODE")]
        public string Wh_Code { get; set; }

       /// <summary>
       /// 通道编码
       /// </summary>
       [ColumnName("CH_CODE")]
       public int Ch_Code { get; set; }

       /// <summary>
       /// 备用通道编码
       /// </summary>
       [ColumnName("BAK_CH_CODE")]
       public int Bak_Ch_Code { get; set; }

       /// <summary>
       /// 备用通道名称
       /// </summary>
       [ColumnName("BAK_CH_NAME")]
       public string Bak_Ch_Name { get; set; }

       /// <summary>
       /// 通道名称
       /// </summary>
       [ColumnName("CH_NAME")]
       public string Ch_Name { get; set; }

       /// <summary>
       /// 是否删除，0未删除，非0删除
       /// </summary>
       [ColumnName("IS_DELETE")]
       public bool Is_Delete { get; set; }

       /// <summary>
       /// 是否启用，Y-启用；N-不启用；默认未启用
       /// </summary>
       [ColumnName("IS_ACTIVE")]
       public string Is_Active { get; set; }

       /// <summary>
       /// 通道备注
       /// </summary>
       [ColumnName("REMARK")]
       public string Remark { get; set; }

       /// <summary>
       /// 有效通道名称
       /// </summary>
       [ColumnName("CANNAME")]
       public string CanName { get; set; }

      /// <summary>
       /// 创建人
      /// </summary>
       [ColumnName("CREATOR")]
       public string Creator { get; set; }

       [ColumnName("BAK1")]
       public string Bak1 { get; set; }

       [ColumnName("BAK2")]
       public string Bak2 { get; set; }

       [ColumnName("BAK3")]
       public string Bak3 { get; set; }

       [ColumnName("BAK4")]
       public string Bak4 { get; set; }

       [ColumnName("BAK5")]
       public string Bak5 { get; set; }
    }
}
