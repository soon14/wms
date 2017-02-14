using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
   public  class OccupyRecordDal
    {
        /// <summary>
        /// 添加占用记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
       public int OccupyRecordAdd(OccupyRecordEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -1;
                ret = map.Execute("INSERT INTO OCCUPY_RECORD ( STOCK_ID ,CREATOR ,CREATEDATE ,OCCUPY_QTY ,STATUS ,REMARK ) " +
                    "VALUES (@STOCK_ID,@CREATOR,@CREATEDATE,@OCCUPY_QTY,@STATUS,@REMARK)",
                new
                {
                    STOCK_ID = entity.StockID,
                    CREATOR = entity.Creator,
                    CREATEDATE = entity.CreateDate,
                    OCCUPY_QTY = entity.OccupyQty,
                    STATUS = entity.Status,
                    REMARK = entity.Remark
                });
            return ret;
        }

       public int OccupyRecordStatusEdit(int OccupyID,string OccupyStatus,int StockID,int OccupyQty)
       {
           IMapper map = DatabaseInstance.Instance();
           int ret = -1;
           ret = map.Execute(@"update OCCUPY_RECORD set  STATUS=@STATUS where OCCUPY_ID=@OCCUPY_ID;
                            update STOCK_RECORD set OCCUPY_QTY=(select OCCUPY_QTY from STOCK_RECORD where STOCK_ID=@STOCK_ID)-@OccupyQty where STOCK_ID=@STOCK_ID;",
           new
           {
               STATUS = OccupyStatus,
               OCCUPY_ID = OccupyID,
               OccupyQty = OccupyQty,
               STOCK_ID = StockID
           });
           return ret;
       }

       /// <summary>
       /// 查询库存占用数据
       /// </summary>
       /// <param name="Creator"></param>
       /// <param name="CreateDateFrom"></param>
       /// <param name="CreateDateTo"></param>
       /// <param name="Status"></param>
       /// <returns></returns>
       public List<OccupyRecordEntity> QueryOccupyRecod(string Creator, DateTime? CreateDateFrom, DateTime? CreateDateTo, String Status)
       {
           string sql = @"select o.OCCUPY_ID,o.STOCK_ID,o.CREATOR,o.CREATEDATE,o.OCCUPY_QTY,o.STATUS,o.REMARK,s.BATCH_NO,s.LOCATION, 
                            s.LOCATION,s.MATERIAL,s.BATCH_NO,s.DUE_DATE,s.COM_MATERIAL,c.NAM as STATUS_NAME,m.NAM AS MATERIAL_NAME
                            from OCCUPY_RECORD o
                            inner join STOCK_RECORD s on s.STOCK_ID=o.STOCK_ID
                            inner join CODEITEM c on c.COD=o.STATUS
                            inner join MATERIAL m on m.COD=s.MATERIAL where 1=1 ";
           if (Creator != null && Creator != "")
               sql += string.Format(" and o.CREATOR='{0}'", Creator);
           if (CreateDateFrom != null)
               sql += string.Format(" and o.CREATEDATE>'{0}'", CreateDateFrom);
           if (CreateDateTo != null)
               sql += string.Format(" and o.CREATEDATE<'{0}'", CreateDateTo);
           if (Status != null && Status != "")
               sql += string.Format(" and o.STATUS='{0}'", Status);
           IMapper map = DatabaseInstance.Instance();

           return map.Query<OccupyRecordEntity>(sql);

       }

    }
}
