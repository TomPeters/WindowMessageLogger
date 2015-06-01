using System;
using System.Windows.Forms;
using Serilog;

namespace WindowMessageLogger
{
    public class HiddenForm : Form
    {
        private readonly ILogger _logger;

        public HiddenForm(ILogger logger)
        {
            _logger = logger;
        }

        protected override void DefWndProc(ref Message m)
        {
            Console.WriteLine("Message received");
            Console.WriteLine(m.HWnd + ": " + m.LParam);
            _logger.Information("Window message {Message} received for {Hwnd}: {FullMessage}", m.LParam, m.WParam, m);
            base.DefWndProc(ref m);
        }
    }
}