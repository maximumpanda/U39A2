using System.Drawing;
using System.Windows.Forms;

namespace Panda_Explorer.Core {
    internal class MovementManager {
        private bool _isDragging;
        private readonly Control _parent;
        private Point _startingLoc;

        public MovementManager(Control parent, Control client) {
            _parent = parent;
            client.MouseDown += MouseDown;
            client.MouseUp += MouseUp;
            client.MouseMove += MouseMove;
        }
        private Point CalculateOffset(Point newLoc) {
            return new Point(newLoc.X - _startingLoc.X, newLoc.Y - _startingLoc.Y);
        }
        internal void MouseDown(object sender, MouseEventArgs args) {
            if (args.Button == MouseButtons.Left) {
                _isDragging = true;
                _startingLoc = args.Location;
            }
        }
        internal void MouseMove(object sender, MouseEventArgs args) {
            if (_isDragging) {
                Point origin = _parent.Location;
                Point offset = CalculateOffset(args.Location);
                Point newLoc = new Point(origin.X + offset.X, origin.Y + offset.Y);
                foreach (Screen s in Screen.AllScreens)
                    if (s.Bounds.Contains(newLoc)) _parent.Location = newLoc;
            }
        }
        internal void MouseUp(object sender, MouseEventArgs args) {
            if (args.Button == MouseButtons.Left) {
                _isDragging = false;
                _startingLoc = new Point();
            }
        }
    }
}