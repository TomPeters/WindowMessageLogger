using System;

namespace WindowMessageLogger
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            HiddenForm form = new HiddenForm() { Visible = false, ShowInTaskbar = false };
            IntPtr forwardingWindowPtr = form.Handle;
            ShellHookManager shellHookManager = new ShellHookManager(forwardingWindowPtr);
            shellHookManager.RegisterHooks();
            Console.ReadKey();
            shellHookManager.UnregisterHooks();
        } 
    }
}