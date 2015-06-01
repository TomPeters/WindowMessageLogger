using System;
using Serilog;

namespace WindowMessageLogger
{
    public class WindowLogger
    {
        private readonly IWindowPropertiesFetcher _windowPropertiesFetcher;
        private readonly ILogger _logger;

        public WindowLogger(IWindowPropertiesFetcher windowPropertiesFetcher, ILogger logger)
        {
            _windowPropertiesFetcher = windowPropertiesFetcher;
            _logger = logger;
        }

        public void LogWindow(IntPtr hWnd)
        {
            var name = _windowPropertiesFetcher.GetWindowTitle(hWnd);
            var className = _windowPropertiesFetcher.GetWindowClassName(hWnd);

            _logger.Information("Window {WindowName} with class {ClassName} ({Hwnd})", name, className, hWnd);
        } 
    }
}