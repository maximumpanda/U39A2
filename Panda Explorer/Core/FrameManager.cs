using System;
using System.Drawing;
using System.Windows.Forms;
using Panda_Explorer.PandaSettings;

namespace Panda_Explorer.Core {
    internal class FrameManager {
        private readonly Rectangle[] _border = new Rectangle[8];
        private int _borderSize;
        private bool _bottom;
        private bool _bottomLeft;
        private bool _bottomRight;
        private bool _canRefresh = true;
        private int _clickedFrame = -1;
        private Flags _flags;
        private bool _isDragging;
        private bool _left;
        private Control _parent;
        private Timer _refreshTimer;
        private Point _resizeStart;
        private bool _right;
        private bool _top;
        private bool _topLeft;
        private bool _topRight;

        public FrameManager(Control parent) {
            SetParentEvents(parent);
            SetMouseEvents();
        }

        public FrameManager(Control parent, Flags flags) {
            SetParentEvents(parent, flags);
            SetMouseEvents();
        }

        private void BuildBorder(object sender, PaintEventArgs args) {
            //
            // top left corner
            //
            if (_topLeft)
                _border[Indexes.TopLeft] = new Rectangle {
                    Location = new Point(0, 0),
                    Width = _borderSize,
                    Height = _borderSize
                };
            //
            // top right corner
            //
            if (_topRight)
                _border[Indexes.TopRight] = new Rectangle {
                    Location = new Point(_parent.Bounds.Width - _borderSize, 0),
                    Width = _borderSize,
                    Height = _borderSize
                };
            //
            // bottom left corner
            //
            if (_bottomLeft)
                _border[Indexes.BottomLeft] = new Rectangle {
                    Location = new Point(0, _parent.Height - _borderSize),
                    Height = _borderSize,
                    Width = _borderSize
                };
            //
            // bottom right
            //
            if (_bottomRight)
                _border[Indexes.BottomRight] = new Rectangle {
                    Location = new Point(_parent.Width - _borderSize, _parent.Height - _borderSize),
                    Height = _borderSize,
                    Width = _borderSize
                };
            //
            // top
            //
            if (_top)
                _border[Indexes.Top] = new Rectangle {
                    Location = new Point(_border[Indexes.TopLeft].Right, 0),
                    Width = _parent.Width - (_border[Indexes.TopLeft].Width + _border[Indexes.TopRight].Width),
                    Height = _borderSize
                };

            //
            // left
            //
            if (_left)
                _border[Indexes.Left] = new Rectangle {
                    Location = new Point(0, _border[Indexes.TopLeft].Height),
                    Width = _borderSize,
                    Height =
                        _parent.Height - (_border[Indexes.TopLeft].Height + _border[Indexes.BottomLeft].Height)
                };
            //
            // right
            //
            if (_right) {
                _border[Indexes.Right] = new Rectangle {
                    Location = new Point(_parent.Width - _borderSize, 0 + _border[Indexes.TopRight].Height),
                    Width = _borderSize,
                    Height =
                        _parent.Height - (_border[Indexes.TopRight].Height + _border[Indexes.BottomRight].Height)
                };
                Debugger.Report(_parent.Name, _border[Indexes.Right].ToString());
            }

            //
            // bottom
            //
            if (_bottom)
                _border[Indexes.Bottom] = new Rectangle {
                    Location =
                        new Point(0 + _border[Indexes.BottomLeft].Width, _parent.Height - _borderSize),
                    Width =
                        _parent.Width - (_border[Indexes.BottomLeft].Width + _border[Indexes.BottomRight].Width),
                    Height = _borderSize
                };
            //
            // DEBUG PAINT
            //
            /*
            foreach (Rectangle rectangle in _border)
            {
                using (Graphics g = _parent.CreateGraphics())
                {
                    g.FillRectangle(Pens.Brown.Brush, rectangle);
                }
            }*/
        }
        private Point CalculateOffset(Point current) {
            return new Point(current.X - _resizeStart.X, current.Y - _resizeStart.Y);
        }
        private int CheckFrameMouseCollision(Point position) {
            for (int i = 0; i < _border.Length; i++)
                if (_border[i].Contains(position)) return i;
            return -1;
        }
        private bool CheckResize(Rectangle newBounds) {
            return newBounds.X >= 0 && newBounds.Y >= 0 && newBounds.Width >= _parent.MinimumSize.Width &&
                   newBounds.Height >= _parent.MinimumSize.Height;
        }
        private bool FlagIsSet(Flags f) {
            return (_flags & f) == f;
        }
        private void ModifyCursor(int index) {
            switch (index) {
                case Indexes.TopLeft:
                case Indexes.BottomRight:
                    _parent.Cursor = Cursors.SizeNWSE;
                    break;
                case Indexes.Top:
                case Indexes.Bottom:
                    _parent.Cursor = Cursors.SizeNS;
                    break;
                case Indexes.TopRight:
                case Indexes.BottomLeft:
                    _parent.Cursor = Cursors.SizeNESW;
                    break;
                case Indexes.Left:
                case Indexes.Right:
                    _parent.Cursor = Cursors.SizeWE;
                    break;
                default:
                    _parent.Cursor = Cursors.Default;
                    break;
            }
            Application.DoEvents();
        }
        private void MouseDownFrame(object sender, MouseEventArgs args) {
            int frameCheck = CheckFrameMouseCollision(args.Location);
            if (args.Button == MouseButtons.Left && frameCheck != -1) {
                _isDragging = true;
                _resizeStart = _parent.PointToScreen(args.Location);
                _clickedFrame = frameCheck;
                _refreshTimer = new Timer();
                _refreshTimer.Tick += (o, e) => { _canRefresh = true; };
                _refreshTimer.Interval = 40;
                _refreshTimer.Start();
            }
        }
        private void MouseLeaveFrame(object sender, EventArgs args) {
            if (!_isDragging)
                _parent.Cursor = Cursors.Default;
        }
        private void MouseMoveFrame(object sender, MouseEventArgs args) {
            if (_canRefresh)
                if (_isDragging) {
                    ResizeFrame(_clickedFrame, _parent.PointToScreen(new Point(args.X, args.Y)));
                }
                else {
                    int collisionIndex = CheckFrameMouseCollision(_parent.PointToClient(Cursor.Position));
                    ModifyCursor(collisionIndex);
                }
        }
        private void MouseUpFrame(object sender, MouseEventArgs args) {
            if (args.Button == MouseButtons.Left) {
                _isDragging = false;
                _parent.Invalidate();
                _refreshTimer?.Dispose();
            }
        }
        private void ResizeFrame(int index, Point current) {
            Point offset = CalculateOffset(current);
            Rectangle newBounds = new Rectangle();
            switch (index) {
                case Indexes.TopLeft:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width - offset.X, _parent.Height - offset.Y));
                    break;
                case Indexes.BottomRight:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y),
                        new Size(_parent.Width + offset.X, _parent.Height + offset.Y));
                    break;
                case Indexes.Top:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width, _parent.Height - offset.Y));
                    break;
                case Indexes.Bottom:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y),
                        new Size(_parent.Width, _parent.Height + offset.Y));
                    break;
                case Indexes.TopRight:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width + offset.X, _parent.Height - offset.Y));
                    break;
                case Indexes.BottomLeft:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y),
                        new Size(_parent.Width - offset.X, _parent.Height + offset.Y));
                    break;
                case Indexes.Left:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y),
                        new Size(_parent.Width - offset.X, _parent.Height));
                    break;
                case Indexes.Right:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y),
                        new Size(_parent.Width + offset.X, _parent.Height));
                    break;
            }
            if (CheckResize(newBounds)) {
                _parent.Bounds = newBounds;
                _resizeStart = current;
            }
        }
        private void SetMouseEvents() {
            _parent.MouseMove += MouseMoveFrame;
            _parent.MouseDown += MouseDownFrame;
            _parent.MouseUp += MouseUpFrame;
            _parent.MouseLeave += MouseLeaveFrame;
        }
        private void SetParentEvents(Control parent, Flags flags = Flags.All) {
            _borderSize = Settings.BorderSize * 2;
            _parent = parent;
            _parent.Paint += BuildBorder;
            _flags = flags;
            _topLeft = FlagIsSet(Flags.TopLeft);
            _top = FlagIsSet(Flags.Top);
            _topRight = FlagIsSet(Flags.TopRight);
            _left = FlagIsSet(Flags.Left);
            _right = FlagIsSet(Flags.Right);
            _bottomLeft = FlagIsSet(Flags.BottomLeft);
            _bottom = FlagIsSet(Flags.Bottom);
            _bottomRight = FlagIsSet(Flags.BottomRight);
        }
        public void UpdateBorderSize(int bordersize) {
            _borderSize = bordersize;
        }

        [Flags]
        public enum Flags {
            TopLeft = 1,
            Top = 2,
            TopRight = 4,
            Left = 8,
            Right = 16,
            BottomLeft = 32,
            Bottom = 64,
            BottomRight = 128,
            All = 255
        }

        public static class Indexes {
            public const int Bottom = 6;
            public const int BottomLeft = 5;
            public const int BottomRight = 7;
            public const int Left = 3;
            public const int Right = 4;
            public const int Top = 1;
            public const int TopLeft = 0;
            public const int TopRight = 2;
        }
    }
}