using System;

namespace WindowMessageLogger
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            HiddenForm form = new HiddenForm() { Visible = false, ShowInTaskbar = false };
            IntPtr forwardingWindowPtr = form.Handle;
            var shellHookManager = new ShellHookManager(forwardingWindowPtr);
        } 
    }
}