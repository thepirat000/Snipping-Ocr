using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_OCR
{
    public class DeviceInfo
    {
        public bool IsPrimary { get; set; }
        public int VerticalResolution { get; set; }
        public int HorizontalResolution { get; set; }
        public string DeviceName { get; set; }
        public Rect MonitorArea { get; set; }
        public Rect WorkArea { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct MONITORINFOEX
    {
        public int Size;
        public Rect Monitor;
        public Rect WorkArea;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
    }

    public static class ScreenHelper
    {
        private const int DESKTOPVERTRES = 117;
        private const int DESKTOPHORZRES = 118;
        private static List<DeviceInfo> _result;
        private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);
        private static bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
        {
            MONITORINFOEX mi = new MONITORINFOEX();
            mi.Size = Marshal.SizeOf(typeof(MONITORINFOEX));
            bool success = GetMonitorInfo(hMonitor, ref mi);
            if (success)
            {
                var dc = CreateDC(mi.DeviceName, mi.DeviceName, null, IntPtr.Zero);
                var di = new DeviceInfo();
                di.DeviceName = mi.DeviceName;
                di.IsPrimary = (mi.Monitor.top == 0 && mi.Monitor.left == 0);
                di.MonitorArea = mi.Monitor;
                di.WorkArea = mi.WorkArea;
                di.VerticalResolution = GetDeviceCaps(dc, DESKTOPVERTRES);
                di.HorizontalResolution = GetDeviceCaps(dc, DESKTOPHORZRES);
                ReleaseDC(IntPtr.Zero, dc);
                _result.Add(di);
            }
            return true;
        }

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public static List<DeviceInfo> GetMonitorsInfo()
        {
            _result = new List<DeviceInfo>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);
            return _result;
        }

    }
}
