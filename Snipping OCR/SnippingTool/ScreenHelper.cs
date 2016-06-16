using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Snipping_OCR
{
    public class DeviceInfo
    {
        public string DeviceName { get; set; }
        public int VerticalResolution { get; set; }
        public int HorizontalResolution { get; set; }
        public Rect MonitorArea { get; set; }
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
        private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);
        [DllImport("User32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        private static List<DeviceInfo> _result;

        public static List<DeviceInfo> GetMonitorsInfo()
        {
            _result = new List<DeviceInfo>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);
            return _result;
        }

        private static bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
        {
            var mi = new MONITORINFOEX();
            mi.Size = Marshal.SizeOf(typeof(MONITORINFOEX));
            bool success = GetMonitorInfo(hMonitor, ref mi);
            if (success)
            {
                var dc = CreateDC(mi.DeviceName, mi.DeviceName, null, IntPtr.Zero);
                var di = new DeviceInfo
                {
                    DeviceName = mi.DeviceName,
                    MonitorArea = mi.Monitor,
                    VerticalResolution = GetDeviceCaps(dc, DESKTOPVERTRES),
                    HorizontalResolution = GetDeviceCaps(dc, DESKTOPHORZRES)
                };
                ReleaseDC(IntPtr.Zero, dc);
                _result.Add(di);
            }
            return true;
        }
    }
}
