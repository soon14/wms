using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class LoginLogEntiy : UserEntity
    {
       [ColumnName("IP")]
       public string IP
       {
           get;
           set;
       }

       [ColumnName("LOGINDATE")]
       public DateTime LoginDate
       {
           get;
           set;
       }

       [ColumnName("LOGINTYPE")]
       public string LoginType
       {
           get;
           set;
       }
    }
}
