using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace Nodes.UI
{
    public class TreeListUtil
    {
        /// <summary>
        /// 递归查找树（模糊查找）
        /// </summary>
        /// <param name="nodeRoot">根节点</param>
        /// <param name="filterText">要查找的文本字符串</param>
        /// <param name="fieldName">要查找的字段</param>
        /// <returns></returns>
        public static TreeListNode SearchNode(TreeListNode nodeRoot, string filterText, string fieldName)
        {
            if (nodeRoot == null
                || string.IsNullOrEmpty(filterText)
                || string.IsNullOrEmpty(fieldName))
                return null;

            if (nodeRoot[fieldName].ToString().Contains(filterText))
                return nodeRoot;

            if (nodeRoot.Nodes.Count == 0)
                return null;

            TreeListNode node = null;
            foreach (TreeListNode child in nodeRoot.Nodes)
            {
                node = SearchNode(child, filterText, fieldName);
                if (node != null)
                    break;
            }

            return node;
        }
    }
}
