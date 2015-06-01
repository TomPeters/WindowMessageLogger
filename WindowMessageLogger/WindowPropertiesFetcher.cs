using System;
using System.Text;

namespace WindowMessageLogger
{
    public interface IWindowPropertiesFetcher
    {
        string GetWindowTitle(IntPtr windowPtr);
        string GetWindowClassName(IntPtr windowPtr);
    }

    public class WindowPropertiesFetcher : IWindowPropertiesFetcher
    {
        public string GetWindowTitle(IntPtr windowPtr)
        {
            int titleLength = WinApi.GetWindowTextLength(windowPtr);
            StringBuilder titleStringBuilder = new StringBuilder(titleLength);
            WinApi.GetWindowText(windowPtr, titleStringBuilder, titleLength + 1);
            return titleStringBuilder.ToString();
        }

        public string GetWindowClassName(IntPtr windowPtr)
        {
            const int classNameLength = 256; // Maximum possible length is 256 chars http://msdn.microsoft.com/en-us/library/windows/desktop/ms633576(v=vs.85).aspx
            StringBuilder titleStringBuilder = new StringBuilder(classNameLength);
            WinApi.GetClassName(windowPtr, titleStringBuilder, classNameLength);
            return titleStringBuilder.ToString();
        }
    }
}