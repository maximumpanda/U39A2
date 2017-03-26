using System;
using System.Drawing;
using System.Windows.Forms;
using Panda_Explorer.PandaSettings;

namespace Panda_Explorer.Core {
    internal class FrameManager {
        private readonly Rectangle[] _border = new Rectangle[8];
        private int _borderSize;
        private bool _canRefresh = true;
        private int _clickedFrame = -1;
        private FrameFlags _flags;
        private bool _isDragging;
        private Control _parent;
        private Timer _refreshTimer;
        private Point _resizeStart;
        private bool Bottom;
        private bool BottomLeft;
        private bool BottomRight;
        private bool Left;
        private bool Right;
        private bool Top;

        private bool TopLeft;
        private bool TopRight;
        public FrameManager(Control parent) {
            SetParentEvents(parent);
            SetMouseEvents();
        }

        public FrameManager(Control parent, FrameFlags flags) {
            SetParentEvents(parent, flags);
            SetMouseEvents();
        }

        private void BuildBorder(object sender, PaintEventArgs args) {
            //
            // top left corner
            //
            if (TopLeft)
                _border[FrameIndex.TopLeft] = new Rectangle {
                    Location = new Point(0, 0),
                    Width = _borderSize,
                    Height = _borderSize
                };
            //
            // top right corner
            //
            if (TopRight)
                _border[FrameIndex.TopRight] = new Rectangle {
                    Location = new Point(_parent.Bounds.Width - _borderSize, 0),
                    Width = _borderSize,
                    Height = _borderSize
                };
            //
            // bottom left corner
            //
            if (BottomLeft)
                _border[FrameIndex.BottomLeft] = new Rectangle {
                    Location = new Point(0, _parent.Height - _borderSize),
                    Height = _borderSize,
                    Width = _borderSize
                };
            //
            // bottom right
            //
            if (BottomRight)
                _border[FrameIndex.BottomRight] = new Rectangle {
                    Location = new Point(_parent.Width - _borderSize, _parent.Height - _borderSize),
                    Height = _borderSize,
                    Width = _borderSize
                };
            //
            // top
            //
            if (Top)
                _border[FrameIndex.Top] = new Rectangle {
                    Location = new Point(_border[FrameIndex.TopLeft].Right, 0),
                    Width = _parent.Width - (_border[FrameIndex.TopLeft].Width + _border[FrameIndex.TopRight].Width),
                    Height = _borderSize
                };

            //
            // left
            //
            if (Left)
                _border[FrameIndex.Left] = new Rectangle {
                    Location = new Point(0, _border[FrameIndex.TopLeft].Height),
                    Width = _borderSize,
                    Height =
                        _parent.Height - (_border[FrameIndex.TopLeft].Height + _border[FrameIndex.BottomLeft].Height)
                };
            //
            // right
            //
            if (Right) {
                _border[FrameIndex.Right] = new Rectangle {
                    Location = new Point(_parent.Width - _borderSize, 0 + _border[FrameIndex.TopRight].Height),
                    Width = _borderSize,
                    Height =
                        _parent.Height - (_border[FrameIndex.TopRight].Height + _border[FrameIndex.BottomRight].Height)
                };
                Debugger.Report(_parent.Name, _border[FrameIndex.Right].ToString());
            }

            //
            // bottom
            //
            if (Bottom)
                _border[FrameIndex.Bottom] = new Rectangle {
                    Location =
                        new Point(0 + _border[FrameIndex.BottomLeft].Width, _parent.Height - _borderSize),
                    Width =
                        _parent.Width - (_border[FrameIndex.BottomLeft].Width + _border[FrameIndex.BottomRight].Width),
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

        private bool FlagIsSet(FrameFlags f) {
            return (_flags & f) == f;
        }

        private void ModifyCursor(int index) {
            switch (index) {
                case FrameIndex.TopLeft:
                case FrameIndex.BottomRight:
                    _parent.Cursor = Cursors.SizeNWSE;
                    break;
                case FrameIndex.Top:
                case FrameIndex.Bottom:
                    _parent.Cursor = Cursors.SizeNS;
                    break;
                case FrameIndex.TopRight:
                case FrameIndex.BottomLeft:
                    _parent.Cursor = Cursors.SizeNESW;
                    break;
                case FrameIndex.Left:
                case FrameIndex.Right:
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
                case FrameIndex.TopLeft:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width - offset.X, _parent.Height - offset.Y));
                    break;
                case FrameIndex.BottomRight:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y),
                        new Size(_parent.Width + offset.X, _parent.Height + offset.Y));
                    break;
                case FrameIndex.Top:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width, _parent.Height - offset.Y));
                    break;
                case FrameIndex.Bottom:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y),
                        new Size(_parent.Width, _parent.Height + offset.Y));
                    break;
                case FrameIndex.TopRight:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X, _parent.Bounds.Y + offset.Y),
                        new Size(_parent.Width + offset.X, _parent.Height - offset.Y));
                    break;
                case FrameIndex.BottomLeft:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y),
                        new Size(_parent.Width - offset.X, _parent.Height + offset.Y));
                    break;
                case FrameIndex.Left:
                    newBounds = new Rectangle(new Point(_parent.Bounds.X + offset.X, _parent.Bounds.Y),
                        new Size(_parent.Width - offset.X, _parent.Height));
                    break;
                case FrameIndex.Right:
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

        private void SetParentEvents(Control parent, FrameFlags flags = FrameFlags.All) {
            _borderSize = Settings.BorderSize * 2;
            _parent = parent;
            _parent.Paint += BuildBorder;
            _flags = flags;
            TopLeft = FlagIsSet(FrameFlags.TopLeft);
            Top = FlagIsSet(FrameFlags.Top);
            TopRight = FlagIsSet(FrameFlags.TopRight);
            Left = FlagIsSet(FrameFlags.Left);
            Right = FlagIsSet(FrameFlags.Right);
            BottomLeft = FlagIsSet(FrameFlags.BottomLeft);
            Bottom = FlagIsSet(FrameFlags.Bottom);
            BottomRight = FlagIsSet(FrameFlags.BottomRight);
        }

        public void UpdateBorderSize(int bordersize) {
            _borderSize = bordersize;
        }
    }

    [Flags]
    public enum FrameFlags {
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

    public static class FrameIndex {
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