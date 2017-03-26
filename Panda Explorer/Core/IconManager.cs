using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Panda_Explorer.Properties;

namespace Panda_Explorer.Core {
    public static class IconManager {
        public static ImageList IconList = new ImageList();
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x1; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x0; // 'Small icon

        static IconManager() {
            //GenerateArrows(); 
            GetFolderIcon();
            IconList.Images.Add("error", SystemIcons.Error);
            CreateTransparentIcon();
        }

        public static void CreateTransparentIcon() {
            Bitmap transp = new Bitmap(1, 1);
            transp.MakeTransparent();
            IconList.Images.Add("blank", transp);
        }

        public static string DetermineIconKey(string fullName) {
            if (!File.Exists(fullName)) {
                if (!Directory.Exists(fullName)) return "blank";
                return Resources.DefaultNodeImageKey;
            }
            Icon icon = Icon.ExtractAssociatedIcon(fullName);
            if (!IconList.Images.ContainsKey(Path.GetExtension(fullName)))
                IconList.Images.Add(Path.GetExtension(fullName), icon);
            return Path.GetExtension(fullName);
        }
        private static void GenerateArrows() {
            IconList.Images.Add(new Bitmap(Resources.ArrowUp));
            IconList.Images.Add(new Bitmap(Resources.ArrowRight));
            IconList.Images.Add(new Bitmap(Resources.ArrowDown));
            IconList.Images.Add(new Bitmap(Resources.ArrowLeft));
            IconList.Images.Add(new Bitmap(Resources.ArrowUpMouseOver));
            IconList.Images.Add(new Bitmap(Resources.ArrowRightMouseOver));
            IconList.Images.Add(new Bitmap(Resources.ArrowDownMouseOver));
            IconList.Images.Add(new Bitmap(Resources.ArrowLeftMouseOver));
        }

        public static void GetFolderIcon() {
            if (Directory.Exists(Path.GetPathRoot(Application.ExecutablePath))) {
                DirectoryInfo folder = new DirectoryInfo(Application.ExecutablePath);
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(folder.Parent.FullName, 0, ref shinfo, (uint) Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_LARGEICON);
                Icon largeIcon = Icon.FromHandle(shinfo.hIcon);
                string key = Resources.DefaultNodeImageKey;
                Bitmap largeIconBitmap = largeIcon.ToBitmap();
                largeIconBitmap.MakeTransparent();
                if (!IconList.Images.ContainsKey(key))
                    IconList.Images.Add(key, largeIconBitmap);
            }
        }

        private static bool IsIdenticalImage(Image first, Bitmap second) {
            if (first.Size != second.Size) return false;
            Bitmap a = new Bitmap(first);
            for (int x = 0; x < first.Width; x++)
            for (int y = 0; y < first.Height; y++)
                if (a.GetPixel(x, y) != second.GetPixel(x, y)) return false;
            return true;
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi,
            uint cbSizeFileInfo, uint uFlags);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
    }
}