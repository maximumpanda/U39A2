using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Panda_Explorer.Properties;
using Panda_Explorer.Types;

namespace Panda_Explorer.Core {
    internal class DirectoryCrawler {
        public readonly PandaQueue<TreeNode> Nodes = new PandaQueue<TreeNode>();
        private readonly DataManager _dataManager;

        public DirectoryCrawler(DataManager dataManager) {
            _dataManager = dataManager;
            Nodes.ItemAdded += NodesOnItemAdded;
        }

        private ListViewItem GenerateListViewItem(DirectoryInfo dir) {
            try {
                ListViewItem item = new ListViewItem(dir.Name, 0);
                item.ImageKey = Resources.DefaultNodeImageKey;
                item.ImageKey = IconManager.DetermineIconKey(dir.FullName);
                ListViewItem.ListViewSubItem[] subItems = {
                    new ListViewItem.ListViewSubItem(item,
                        dir.LastAccessTime.ToShortDateString() + " " + dir.LastAccessTime.ToShortTimeString()),
                    new ListViewItem.ListViewSubItem(item, ""),
                    new ListViewItem.ListViewSubItem(item, "")
                };
                subItems[0].ForeColor = Color.Gray;
                item.SubItems.AddRange(subItems);
                return item;
            }
            catch {
            }
            return null;
        }

        public ListViewItem GenerateListViewItem(FileInfo info) {
            try {
                ListViewItem item = new ListViewItem(info.Name, 1);
                item.ImageKey = Path.GetExtension(info.Name);
                item.ImageKey = IconManager.DetermineIconKey(info.FullName);
                ListViewItem.ListViewSubItem[] subItems = {
                    new ListViewItem.ListViewSubItem(item, info.LastAccessTime.ToLongTimeString()),
                    new ListViewItem.ListViewSubItem(item, info.Extension),
                    new ListViewItem.ListViewSubItem(item, info.Length / 1000000 + " MB")
                };
                item.SubItems.AddRange(subItems);
                return item;
            }
            catch (Exception) {
            }
            return null;
        }

        internal void GetDirectories(DirectoryInfo[] subDirs, TreeNode parent) {
            parent.Nodes.RemoveByKey(Resources.DummyNode);
            foreach (DirectoryInfo subDir in subDirs)
                if (!NodeExists(parent, subDir))
                    if (!subDir.Attributes.HasFlag(FileAttributes.Hidden)) {
                        TreeNode node = new TreeNode(subDir.Name, 0, 0) {
                            Tag = subDir,
                            ImageKey = IconManager.DetermineIconKey(subDir.FullName)
                        };
                        DirectoryInfo[] subSubDirs = GetSubDirectoriesSafe(subDir);
                        if (subSubDirs.Length != 0)
                            node.Nodes.Add(new TreeNode {Name = Resources.DummyNode});
                        parent.Nodes.Add(node);
                    }
        }

        internal void GetDirectoriesSafe(DirectoryInfo dir, TreeNode parent) {
            try {
                GetDirectories(dir.GetDirectories(), parent);
            }
            catch (UnauthorizedAccessException) {
                MessageBox.Show(Resources.AccessDeniedRelaunchAdmin);
            }
        }
        public DirectoryInfo GetDirectoryInfo(TreeNode e) {
            return (DirectoryInfo) e.Tag;
        }

        internal List<ListViewItem> GetListViewItemsThumbnail(TreeNode node) {
            List<ListViewItem> items = new List<ListViewItem>();
            if (node.Tag == null) return items;
            try {
                DirectoryInfo info = (DirectoryInfo) node.Tag;
                foreach (DirectoryInfo dirInfo in info.GetDirectories())
                    items.Add(GenerateListViewItem(dirInfo));
                foreach (FileInfo fileInfo in info.GetFiles())
                    items.Add(GenerateListViewItem(fileInfo));
            }
            catch (UnauthorizedAccessException e) {
            }
            return items;
        }

        internal string GetPath(TreeNode node) {
            return ((DirectoryInfo) node.Tag).FullName;
        }

        internal DirectoryInfo[] GetSubDirectoriesSafe(DirectoryInfo dir) {
            DirectoryInfo[] info = new DirectoryInfo[0];
            try {
                info = dir.GetDirectories();
            }
            catch (UnauthorizedAccessException) {
                Debugger.Report(dir.FullName);
            }
            return info;
        }

        private bool NodeExists(TreeNode node, DirectoryInfo dir) {
            foreach (TreeNode subNode in node.Nodes) {
                DirectoryInfo subNodeDir = (DirectoryInfo) subNode.Tag;
                if (dir.FullName == subNodeDir.FullName) return true;
            }
            return false;
        }
        private void NodesOnItemAdded(object sender, EventArgs eventArgs) {
            TreeNode node = Nodes.Dequeue();
            DirectoryInfo info = (DirectoryInfo) node.Tag;
            if (info != null) {
                GetDirectoriesSafe(info, node);
                _dataManager.Refresh();
            }
        }
    }
}