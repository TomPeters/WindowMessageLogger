using System;
using System.Windows.Forms;
using Serilog;

namespace WindowMessageLogger
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            HiddenForm form = new HiddenForm(logger) { Visible = false, ShowInTaskbar = false };
            IntPtr forwardingWindowPtr = form.Handle;
            Console.WriteLine("Pointer: " + forwardingWindowPtr);
            ShellHookManager shellHookManager = new ShellHookManager(forwardingWindowPtr);
            shellHookManager.RegisterHooks();
            try
            {
                ApplicationContext context = new WindowsMessageLoggerApplicationContext();
                Application.Run(context);
            }
            finally
            {
                shellHookManager.UnregisterHooks();
            }
        } 
    }
}