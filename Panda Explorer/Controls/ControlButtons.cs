using System;
using System.Drawing;
using System.Windows.Forms;
using Panda_Explorer.Properties;
using Settings = Panda_Explorer.PandaSettings.Settings;

namespace Panda_Explorer.Controls {
    public partial class ControlButtons : UserControl {
        internal bool IsMaximized;
        public ControlButtons() {
            InitializeComponent();

            foreach (Control control in Layout1.Controls) {
                control.MouseEnter += MouseEnterControlButton;
                control.MouseLeave += MouseLeaveControlButton;
            }

            ExitBtn.BackColor = Settings.Colors.TitleBackground;
            MinMaxButn.BackColor = Settings.Colors.TitleBackground;
            MinimizeBtn.BackColor = Settings.Colors.TitleBackground;

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void MouseClickExitBtn(object sender, MouseEventArgs e) {
            ParentForm?.Close();
        }
        private void MouseClickMinimizeBtn(object sender, MouseEventArgs e) {
            if (ParentForm != null) ParentForm.WindowState = FormWindowState.Minimized;
        }
        private void MouseClickMinMaxBtn(object sender, MouseEventArgs e) {
            if (ParentForm == null) return;
            ParentForm.WindowState = IsMaximized ? FormWindowState.Normal : FormWindowState.Maximized;
            IsMaximized = !IsMaximized;
        }
        private void MouseEnterControlButton(object sender, EventArgs e) {
            ((PictureBox) sender).BackColor = Settings.Colors.Highlight;
        }
        private void MouseLeaveControlButton(object sender, EventArgs e) {
            ((PictureBox) sender).BackColor = Settings.Colors.TitleBackground;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            ExitBtn.Image = ResizeImage(Resources.Exit);
            MinMaxButn.Image = ResizeImage(Resources.MaxMin);
            MinimizeBtn.Image = ResizeImage(Resources.Minimize);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Layout1.Size = new Size(Parent.Height * 3, Parent.Height);
            ExitBtn.Size = new Size(Height, Height);
            MinMaxButn.Size = new Size(Height, Height);
            MinimizeBtn.Size = new Size(Height, Height);
        }
        private Bitmap ResizeImage(Bitmap image) {
            Bitmap bitmap = new Bitmap(image,
                new Size((int) (image.Width + image.Width * Settings.DpiScaling.Width) / 2,
                    (int) (image.Height + image.Height * Settings.DpiScaling.Height) / 2));
            return bitmap;
        }
    }
}