using System.Drawing;

namespace Panda_Explorer.PandaSettings {
    public static class Settings {
        internal static SizeF AutoScaleDimensions = new SizeF(6f, 13F);
        internal static int BorderSize = 6;
        internal static PandaColors Colors = new PandaColors();
        internal static SizeF DpiScaling = new SizeF(1f, 1f);
        internal static Font Font = SystemFonts.DefaultFont;
        internal static bool EnableDebugger = true;
    }

    internal class PandaColors {
        internal Color Background;
        internal Color Frame;
        internal Color Highlight;
        internal Color Menu;
        internal Color Text;
        internal Color TitleBackground;
        internal Color TitleText;
        public PandaColors() {
            GenerateDefaults();
        }

        private void GenerateDefaults() {
            Frame = SystemColors.Menu;
            TitleBackground = SystemColors.Menu;
            TitleText = SystemColors.WindowText;
            Highlight = SystemColors.Highlight;
            Text = SystemColors.ControlText;
            Background = SystemColors.Window;
            Menu = SystemColors.Menu;
        }
    }
}