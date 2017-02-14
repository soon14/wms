using System.Collections.Generic;
using Nodes.Entities;

namespace Nodes.Instore
{
    public interface IAsnManager
    {
        /// <summary>
        /// 重新加载数据
        /// </summary>
        void ReloadAsn();

        /// <summary>
        /// 更新选中单据的状态信息以及备注、颜色标记等
        /// </summary>
        void RefreshState();

        /// <summary>
        /// 获取选中的表头
        /// </summary>
        /// <returns></returns>
        List<AsnBodyEntity> GetFocusedHeaders();
    }
}
