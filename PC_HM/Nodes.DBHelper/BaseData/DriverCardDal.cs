using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class DriverCardDal
    {
        /// <summary>
        /// 检查送货牌编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(DriverCardEntity cardState)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT CARD_NO FROM WM_CARD_STATE WHERE CARD_NO = @COD", new { COD = cardState.CardNO });
            return !string.IsNullOrEmpty(id);
        }

        ///<summary>
        ///查询所有送货牌
        ///</summary>
        ///<returns></returns>
        public List<DriverCardEntity> GetAllCardState()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT CARD_NO, CARD_STATE, HEADER_ID FROM WM_CARD_STATE";
            return map.Query<DriverCardEntity>(sql);
        }

        /// <summary>
        /// 添加或编辑送货牌
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(DriverCardEntity cardState, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(cardState))
                    return -1;
                ret = map.Execute("INSERT INTO WM_CARD_STATE(CARD_NO, CARD_STATE) VALUES(@CARD_NO, @CARD_STATE)",
                new
                {
                    CARD_NO = cardState.CardNO,
                    CARD_STATE = cardState.CardState
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE WM_CARD_STATE SET CARD_NO = @CARD_NO WHERE CARD_NO = @CARD_NO",
                new
                {
                    CARD_NO = cardState.CardNO
                });
            }
            return ret;
        }

        /// <summary>
        /// 删除送货牌
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public int Delete(string cardNO)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM WM_CARD_STATE WHERE CARD_NO = @CARD_NO", new { CARD_NO = cardNO });
        }
    }
}
