using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
   public class PrintLogDal
    {
       public List<PrintLogEntity> ListPrintLogs(PrintLogEntity PrintLog, DateTime DateFrom, DateTime DateTo)
       {
           IMapper map = DatabaseInstance.Instance();
           string sql = @"SELECT START_SEQ,QTY,PRINT_USER,PRINT_DATE,TYP FROM PRINT_LOGS 
                        where (@Name is null or PRINT_USER = @Name) 
                        and (@DateFrom is null or PRINT_DATE >= @DateFrom)
                        and (@DateTo is null or PRINT_DATE <= @DateTo)";
           return map.Query<PrintLogEntity>(sql,
               new
               {
                   Name = PrintLog.PRINT_USER,
                   DateFrom = DateFrom,
                   DateTo = DateTo
               });
       }
    }
}
