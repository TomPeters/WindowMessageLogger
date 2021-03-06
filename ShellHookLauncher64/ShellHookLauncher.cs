﻿using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace ShellHookLauncher
{
    public static class ShellHookLauncher
    {
        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                File.WriteAllText("log - " + ShellHookHelper.DllFileName + ".txt", ex.Message + ex.StackTrace);
            }
        }

        private static void Run()
        {
            SetDllDirectory(Directory.GetCurrentDirectory());
            File.WriteAllText("log - " + ShellHookHelper.DllFileName + ".txt", "setup hook");
            IntPtr wndProcHook = ShellHookHelper.SetupWndProcHook();
            //ShellHookHelper.SetupGetMsgHook();
            try
            {
                WaitForTerminationMessage();
            }
            finally
            {
                UnhookWindowsHookEx(wndProcHook);
            }
        }

        private static void WaitForTerminationMessage()
        {
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(ShellHookHelper.PanelessNamedPipe, PipeDirection.In))
            {
                pipeServer.WaitForConnection();
            }
        }

        //http://msdn.microsoft.com/en-us/library/ms686203%28VS.85%29.aspx
        //http://www.pinvoke.net/default.aspx/kernel32.setdlldirectory
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms644993(v=vs.85).aspx
        //http://www.pinvoke.net/default.aspx/user32.unhookwindowshookex
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhook);
    }
}
