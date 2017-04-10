using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Panda_Explorer.Core;
using Panda_Explorer.Properties;
using Settings = Panda_Explorer.PandaSettings.Settings;

namespace Panda_Explorer.Controls {
    public partial class TitleBar : UserControl {
        public TitleBar(Control parent) {
            Parent = parent;
            InitializeComponent();
            LayoutTable.Parent = this;
            LayoutTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            LayoutTable.AutoSize = true;
            LayoutTable.AutoScroll = false;
            Icon.Parent = LayoutTable;
            ControlButtons.Parent = LayoutTable;
            TitleLbl.Parent = LayoutTable;
            TitleLbl.Width = 100;
            TitleLbl.Font = new Font("Microsoft Sans Serif", 20);
            Icon.Size = new Size(30, 30);
            Icon.Image = new Bitmap(Resources.PandaExplorer, 30, 30);
            Icon.SizeMode = PictureBoxSizeMode.CenterImage;
            ParentChanged += (o, e) => {
                if (ParentForm != null)
                    TitleLbl.Text = ParentForm.Name;
            };

            new MovementManager(Parent, LayoutTable);
        }

        protected override void OnPaint(PaintEventArgs e) {
            TitleLbl.Font = new Font("Microsoft Sans Serif", 8 * Settings.DpiScaling.Width);
            Debugger.Report("Font", TitleLbl.Font.Size.ToString(CultureInfo.InvariantCulture));
            base.OnPaint(e);
        }
    }
}