using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowMessageLogger
{
    public class WindowRepository
    {
        private readonly List<IntPtr> _windows = new List<IntPtr>();

        public WindowRepository()
        {
            WinApi.EnumWindows(WindowsEnumProcess, IntPtr.Zero);
        }

        private bool WindowsEnumProcess(int hWnd, int lParam)
        {
            _windows.Add((IntPtr)hWnd);
            return true;
        }

        public bool WindowCreated(IntPtr hwnd)
        {
            if (!_windows.Contains(hwnd))
            {
                _windows.Add(hwnd);
                return true;
            }
            return false;
        }

        public IReadOnlyList<IntPtr> AllWindows { get {  return _windows.ToList(); } } 
    }
}