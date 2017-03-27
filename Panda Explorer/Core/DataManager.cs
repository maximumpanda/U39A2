using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Panda_Explorer.Properties;

namespace Panda_Explorer.Core {
    public class DataManager {
        public List<string> Groups;
        public List<ListViewItem> ListViewItems;
        public List<TreeNode> Nodes;
        private readonly DirectoryCrawler _crawler;
        public event EventHandler ListViewItemsChanged;

        public event EventHandler NodesChanged;

        public DataManager() {
            Groups = new List<string>();
            Nodes = new List<TreeNode>();
            _crawler = new DirectoryCrawler(this);
            ListViewItems = new List<ListViewItem>();
            SeedNodes();
        }

        private void GenerateDrives() {
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                DirectoryInfo info = drive.RootDirectory;
                if (!info.Exists) return;
                TreeNode node = new TreeNode(info.Name) {Tag = info, ImageKey = Resources.DefaultNodeImageKey};
                _crawler.GetDirectories(info.GetDirectories(), node);
                Nodes.First(x => x.Name == Resources.Drives).Nodes.Add(node);
            }
        }

        public void GenerateListView(TreeNode e) {
            ListViewItems.Clear();
            ListViewItems = _crawler.GetListViewItemsThumbnail(e);
            ListViewItemsChanged?.Invoke(ListViewItems, EventArgs.Empty);
        }

        public void Refresh() {
            NodesChanged?.Invoke(Nodes, EventArgs.Empty);
        }

        public void SeedNodes() {
            Groups.Add(Resources.Drives);
            foreach (string group in Groups) {
                TreeNode newNode = new TreeNode(group) {
                    Name = group,
                    ImageKey = Resources.NodeEmptyImageKey
                };
                Nodes.Add(newNode);
                NodesChanged?.Invoke(newNode, EventArgs.Empty);
            }
            GenerateDrives();
        }

        public void UpdateNode(TreeNode node) {
            _crawler.Nodes.Enqueue(node);
        }
    }
}