using System;
using System.Windows.Forms;

namespace WindowMessageLogger
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            HiddenForm form = new HiddenForm() { Visible = false, ShowInTaskbar = false };
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