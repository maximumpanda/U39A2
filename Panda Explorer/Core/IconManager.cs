using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Panda_Explorer.Properties;

namespace Panda_Explorer.Core {
    public static class IconManager {
        public static ImageList IconList = new ImageList();
        public const uint ShgfiIcon = 0x100;
        public const uint ShgfiLargeicon = 0x1; // 'Large icon
        public const uint ShgfiSmallicon = 0x0; // 'Small icon

        static IconManager() {
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
                if (icon != null) IconList.Images.Add(Path.GetExtension(fullName), icon);
            return Path.GetExtension(fullName);
        }
        public static void GetFolderIcon() {
            if (Directory.Exists(Path.GetPathRoot(Application.ExecutablePath))) {
                DirectoryInfo folder = new DirectoryInfo(Application.ExecutablePath);
                Shfileinfo shinfo = new Shfileinfo();
                if (folder.Parent != null)
                    SHGetFileInfo(folder.Parent.FullName, 0, ref shinfo, (uint) Marshal.SizeOf(shinfo),
                        ShgfiIcon | ShgfiLargeicon);
                Icon largeIcon = Icon.FromHandle(shinfo.hIcon);
                string key = Resources.DefaultNodeImageKey;
                Bitmap largeIconBitmap = largeIcon.ToBitmap();
                largeIconBitmap.MakeTransparent();
                if (!IconList.Images.ContainsKey(key))
                    IconList.Images.Add(key, largeIconBitmap);
            }
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref Shfileinfo psfi,
            uint cbSizeFileInfo, uint uFlags);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Shfileinfo {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
    }
}