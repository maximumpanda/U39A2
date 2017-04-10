using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Panda_Explorer.Controls;
using Panda_Explorer.Core;
using Panda_Explorer.Properties;
using Settings = Panda_Explorer.PandaSettings.Settings;

namespace Panda_Explorer {
    public partial class Window : Form {
        internal TableLayoutPanel ContentsLayout;
        internal DataManager DataManager;
        internal ListView ListView;
        internal TableLayoutPanel MasterLayout;
        internal TitleBar TitleBar;
        internal TreeView TreeView;
        private Panel _treeViewPanel;

        public Window(bool debugMode = false) {
            Settings.DpiScaling = GetScalingFactor();
            Settings.EnableDebugger = debugMode;
            InitializeComponent();

            InitControls();
            InitDataManager();
            AssignControlsToLayouts();
            InitFrames();
            AssignEvents();

            InitDebugWindow();
        }

        private void AssignControlsToLayouts() {
            MasterLayout.RowStyles.Add(new RowStyle());
            MasterLayout.Controls.Add(TitleBar, 0, 0);
            _treeViewPanel.Controls.Add(TreeView);
            ContentsLayout.Controls.Add(_treeViewPanel, 0, 0);
            ContentsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, TreeView.Width));
            ContentsLayout.Controls.Add(ListView);
            ContentsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            MasterLayout.Controls.Add(ContentsLayout, 1, 2);
            Controls.Add(MasterLayout);
        }
        private void AssignEvents() {
            TreeView.NodeMouseClick += TreeViewOnNodeMouseClick;
            TreeView.SizeChanged += TreeViewOnSizeChanged;
            MouseEnter += (sender, args) => { Debugger.Report("window", Bounds.ToString()); };
            SizeChanged += (s, e) => {
                MasterLayout.MaximumSize = new Size(ClientSize.Width - Padding.Horizontal,
                    ClientSize.Height - Padding.Vertical);
            };
            SizeChanged += (s, e) => {
                Size newSize = new Size(ClientSize.Width - Padding.Horizontal,
                    ClientSize.Height - (Padding.Vertical + TitleBar.Height));
                TreeView.MaximumSize = newSize;
                ListView.MaximumSize = newSize;
                Debugger.Report("treeview", TreeView.Size.ToString());
                Debugger.Report("listview", ListView.Size.ToString());
            };
            TreeView.NodeMouseClick += UpdateListViewOnNodeClick;
            DataManager.ListViewItemsChanged += DataManagerOnListViewItemsChanged;
        }
        private void DataManagerOnListViewItemsChanged(object sender, EventArgs eventArgs) {
            ListView.Items.Clear();
            foreach (ListViewItem item in DataManager.ListViewItems)
                ListView.Items.Add(item);
            ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private void DataManagerOnNodesChanged(object sender, EventArgs eventArgs) {
            if (TreeView.Nodes.Count == 0)
                foreach (TreeNode dataManagerNode in DataManager.Nodes)
                    TreeView.Nodes.Add(dataManagerNode);
            TreeView.Update();
        }

        // GetDeviceCaps & GetScaleFactor code taken from Farshid T on stack overflow
        // http://stackoverflow.com/a/21450169
        // Needed to find proper DPI Scaling as windows doesn't report this properly.
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        private SizeF GetScalingFactor() {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int logicalScreenHeight = GetDeviceCaps(desktop, (int) DeviceCap.Vertres);
            int physicalScreenHeight = GetDeviceCaps(desktop, (int) DeviceCap.Desktopvertres);

            float screenScalingFactor = physicalScreenHeight / (float) logicalScreenHeight;

            return new SizeF(screenScalingFactor, screenScalingFactor); // 1.25 = 125%
        }
        private void InitControls() {
            Padding = new Padding(Settings.BorderSize);
            MinimumSize = new Size(600, 400);
            MasterLayout = new TableLayoutPanel {
                Name = "MasterLayout",
                Parent = this,
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoScroll = true,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            TitleBar = new TitleBar(this) {
                Dock = DockStyle.Top,
                MaximumSize = new Size(0, 30),
                MinimumSize = new Size(400, 30),
                Name = "TitleBar"
            };

            ContentsLayout = new TableLayoutPanel {
                Name = "ContentsLayout",
                Parent = MasterLayout,
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Settings.Colors.Background,
                Margin = new Padding(0),
                GrowStyle = TableLayoutPanelGrowStyle.AddColumns
            };
            _treeViewPanel = new Panel {
                Name = "TreeViewPanel",
                Margin = new Padding(0),
                Padding = new Padding(0, 0, Settings.BorderSize, 0),
                Parent = ContentsLayout,
                Dock = DockStyle.Left
            };
            TreeView = new TreeView {
                Parent = ContentsLayout,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Font = new Font("Microsoft Sans Serif", 10 * Settings.DpiScaling.Width),
                ForeColor = Settings.Colors.Highlight,
                BackColor = Settings.Colors.Menu,
                MinimumSize = new Size(150, 0),
                SelectedImageKey = Resources.DefaultNodeImageKey
            };
            ListView = new ListView {
                Name = "Contents",
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Font = new Font("Microsoft Sans Serif", 10 * Settings.DpiScaling.Width),
                ForeColor = Settings.Colors.Text,
                BackColor = Settings.Colors.Background
            };
            _treeViewPanel.MinimumSize = new Size(TreeView.MinimumSize.Width + _treeViewPanel.Padding.Right, 0);
            TreeView.ImageList = IconManager.IconList;
            TreeView.ShowLines = false;
            ListView.SmallImageList = IconManager.IconList;
            ListView.LargeImageList = IconManager.IconList;
            ListView.View = View.Details;
            ListView.Columns.Add("Name");
            ListView.Columns.Add("DateModified");
            ListView.Columns.Add("Type");
            ListView.Columns.Add("Size");
            SetWindowTheme(TreeView.Handle, "explorer", null);
        }
        private void InitDataManager() {
            DataManager = new DataManager();
            DataManager.NodesChanged += DataManagerOnNodesChanged;
            DataManager.Refresh();
        }
        private void InitDebugWindow() {
            Debugger.CreateDebugWindow(ContentsLayout);
            //ContentsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }
        private void InitFrames() {
            new FrameManager(this);
            new FrameManager(_treeViewPanel, FrameFlags.Right);
        }
        protected override void OnLoad(EventArgs e) {
            Scale(Settings.DpiScaling);
            TitleBar.Icon.Image = new Bitmap(Resources.PandaExplorer, TitleBar.Icon.Height, TitleBar.Icon.Height);
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        private void TreeViewOnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            e.Node.SelectedImageKey = Resources.DefaultNodeImageKey;
            DataManager.UpdateNode(e.Node);
        }
        private void TreeViewOnSizeChanged(object sender, EventArgs eventArgs) {
            ContentsLayout.Refresh();
        }
        private void UpdateListViewOnNodeClick(object sender, TreeNodeMouseClickEventArgs e) {
            DataManager.GenerateListView(e.Node);
        }

        // code taken from Farshid T on stack overflow
        // http://stackoverflow.com/a/21450169
        // Needed to find proper DPI Scaling as windows doesn't report this properly.
        public enum DeviceCap {
            Vertres = 10,
            Desktopvertres = 117

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }
    }
}