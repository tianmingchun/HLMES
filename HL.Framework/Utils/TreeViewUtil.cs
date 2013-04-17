using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HL.Framework.Utils
{
    public static class TreeViewUtil
    {
        #region TreeView_AfterCheck
        /// <summary>
        /// 响应父子节点的选中变化
        /// </summary>
        /// <param name="treeNode"></param>
        public static void TreeView_AfterCheck(TreeNode treeNode)
        {
            try
            {
                //是父节点，则影响所有子节点
                CheckedParentNodes(treeNode);
                //如果是子节点，则反向设置父节点状态
                CheckedChildNodes(treeNode);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private static void CheckedParentNodes(TreeNode treeNode)
        {
            if (treeNode.GetNodeCount(false) > 0)
            {
                for (int i = 0; i < treeNode.Nodes.Count; i++)
                {
                    treeNode.Nodes[i].Checked = treeNode.Checked;
                    CheckedParentNodes(treeNode.Nodes[i]);
                }
            }
        }

        private static void CheckedChildNodes(TreeNode treeNode)
        {
            if (treeNode.Parent != null)
            {
                TreeNode parentTreeNode = treeNode.Parent;
                int j = 0;
                for (int i = 0; i < parentTreeNode.Nodes.Count; i++)
                {
                    if (parentTreeNode.Nodes[i].Checked)
                    {
                        parentTreeNode.Checked = true;
                        break;
                    }
                    j++;
                }
                if (j == parentTreeNode.Nodes.Count)
                {
                    parentTreeNode.Checked = false;
                }
                CheckedChildNodes(parentTreeNode);
            }

        }
        #endregion
    }
}
