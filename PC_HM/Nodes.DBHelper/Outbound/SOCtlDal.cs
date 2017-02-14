using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities.OutBound;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper.Outbound
{
    public class SOCtlDal
    {

        private bool IsCodeExists(SoContainerLocation sclEntity)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT wcl.CTL_NAME FROM wm_container_location wcl" +
                                                " INNER JOIN wm_base_code wbc ON wcl.CTL_TYPE =wbc.ITEM_VALUE " +
                                                " WHERE wcl.CTL_NAME= @CTLNAME AND wbc.ITEM_DESC=@CTL_TYPE", new { CTLNAME = sclEntity.CTLName, CTL_TYPE = sclEntity.CTlType });
            return !string.IsNullOrEmpty(id);
        }

        public int Save(SoContainerLocation sclEntity, bool isNew, string whCode, string ctlTye)
        {
            IMapper map = DatabaseInstance.Instance();
            if (IsCodeExists(sclEntity))
                return -1;
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在,不存在添加

                ret = map.Execute("INSERT INTO wm_container_location(CTL_NAME,CTL_STATE,LAST_UPDATETIME,WH_CODE,CTL_TYPE)" +

                                    "VALUES(@CTL_NAME, @CTL_STATE,NOW(),@WH_CODE,CASE @CTL_TYPE WHEN '托盘位'THEN 'L95'WHEN '笼车位'THEN 'L96' END)",
                new
                {
                    CTL_NAME = sclEntity.CTLName,
                    CTL_STATE = sclEntity.CTLState,
                    WH_CODE = whCode,
                    CTL_TYPE = ctlTye
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE wm_container_location wcl " +
                                  " INNER JOIN wm_base_code wbc ON wcl.CTL_TYPE=wbc.ITEM_VALUE " +
                                  "SET CTL_NAME= @CTL_NAME,LAST_UPDATETIME = NOW() " +
                                  "WHERE CTL_NAME = @CTL_NAME_OLD AND wbc.ITEM_DESC=@CTL_TYPE",
                new
                {
                    CTL_NAME = sclEntity.CTLName,
                    CTL_NAME_OLD = sclEntity.CTLName_Old,
                    CTL_TYPE = ctlTye
                });
            }
            return ret;
        }

        public int DeleteCTL(string ctlName, string ctlType)
        {
            IMapper map = DatabaseInstance.Instance();
            //return map.Execute("DELETE FROM wm_container_location WHERE CTL_NAME =@CTL_NAME", new { CTL_NAME = ctlName });
            return map.Execute(" UPDATE wm_container_location wcl" +
                               " INNER JOIN wm_base_code wbc ON wcl.CTL_TYPE=wbc.ITEM_VALUE" +
                               " SET wcl.IS_DELETE =2 WHERE wcl.CTL_NAME =@CTL_NAME AND wbc.ITEM_DESC=@CTL_TYPE AND wcl.CTL_STATE=90", new { CTL_NAME = ctlName, CTL_TYPE = ctlType });
        }

        public List<SoContainerLocation> QeryCTL()
        {
            string sql = string.Format("SELECT wcl.CTL_NAME,wbc.ITEM_DESC ITEM_DESC1,wbc1.ITEM_DESC ITEM_DESC2 FROM wm_container_location wcl " +
                                        " INNER JOIN wm_base_code wbc ON wcl.CTL_STATE=wbc.ITEM_VALUE" +
                                        " INNER JOIN wm_base_code wbc1 ON wcl.CTL_TYPE=wbc1.ITEM_VALUE" +
                                        " WHERE wcl.IS_DELETE = 1" +
                                        " ORDER BY wcl.CTL_TYPE DESC");
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SoContainerLocation>(sql);
        }

        public long GetMaxName(string ctlType)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<long>("SELECT IFNULL( MAX(CAST(A.CTL_NAME AS SIGNED)),0)+1 FROM wm_container_location A WHERE A.CTL_TYPE=@CTL_TYPE", new { CTL_TYPE = ctlType });
        }

    }
}
