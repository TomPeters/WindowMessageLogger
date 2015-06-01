using System;
using System.Windows.Forms;
using Serilog;

namespace WindowMessageLogger
{
    public class HiddenForm : Form
    {
        private readonly ILogger _logger;
        private readonly WindowRepository _windowRepository;
        private readonly WindowLogger _windowLogger;

        public HiddenForm(ILogger logger, WindowRepository windowRepository, WindowLogger windowLogger)
        {
            _logger = logger;
            _windowRepository = windowRepository;
            _windowLogger = windowLogger;
        }

        protected override void DefWndProc(ref Message m)
        {
            IntPtr hWnd = m.WParam;
            bool isNewWindow = _windowRepository.WindowCreated(hWnd);
            if (isNewWindow)
            {
                _windowLogger.LogWindow(hWnd);
            }
            _logger.Information("Window message {Message} received for {Hwnd}: {FullMessage}", GetMessageType(m.LParam.ToInt32()), hWnd, m);

            base.DefWndProc(ref m);
        }

        private string GetMessageType(int message)
        {
            switch (message)
            {
                case 1:
                    return "WM_CREATE";
                default:
                    return "OTHER";
            }
        }
    }
}