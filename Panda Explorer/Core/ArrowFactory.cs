using System.Drawing;
using System.Drawing.Imaging;

namespace Panda_Explorer.Core {
    public class ArrowFactory {
        private static Image BuildArrow(int direction, int size, Color color, bool isMouseover) {
            switch (direction) {
                case 1:
                    return buildUpArrow(size, color);
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

            return null;
        }

        private static Image buildUpArrow(int size, Color color) {
            Point start = new Point(size / 2, -1);
            Point offset = new Point(0, 0);
            int thickness = size / 3;
            Image newUpArrow = new Bitmap(size, size, PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(newUpArrow)) {
                Pen pen = new Pen(color);
                g.Clear(Color.Transparent);
                while (start.X - offset.X != -1 && start.Y + offset.Y != -1 ||
                       start.X + offset.X <= size && start.Y + offset.Y <= size) {
                    g.DrawLine(pen, start.X + offset.X, start.Y - offset.Y, start.X + offset.X, start.Y - offset.Y + 4);
                    g.DrawLine(pen, start.X - offset.X, start.Y - offset.Y, start.X - offset.X, start.Y - offset.Y + 4);
                    offset.X++;
                    offset.Y--;
                }
                g.FillRectangle(Brushes.Transparent, 0, offset.Y + 5, newUpArrow.Width, newUpArrow.Height);
            }
            return newUpArrow;
        }
        public static Image[] MakeArrows(int size, Color normalColor, Color mouseoverColor) {
            Image[] arrows = new Image[8];
            for (int i = 0; i <= 4; i++) {
                arrows[i] = BuildArrow(i + 1, size, normalColor, false);
                arrows[i + 1] = BuildArrow(i + 1, size, mouseoverColor, true);
            }
            return arrows;
        }
    }
}