using System;
using System.Diagnostics;

namespace WindowMessageLogger
{
    public class ShellHookManager : IShellHookManager
    {
        private readonly IntPtr _forwardingWindowPtr;
        private const int ShellHookTerminationTimeout = 250;

        public ShellHookManager(IntPtr forwardingWindowPtr)
        {
            _forwardingWindowPtr = forwardingWindowPtr;
        }

        public void RegisterHooks()
        {
            StoreWindowPtr();
            Process.Start(Identifiers.HookLauncher32);
            if (Is64BitArch)
            {
                Process.Start(Identifiers.HookLauncher64);
            }
        }

        private void StoreWindowPtr()
        {
            var result = WinApi.SetProp(WinApi.GetDesktopWindow(), Identifiers.PanelessWindowPropertyId, _forwardingWindowPtr);
            if (result == false)
            {
                throw new WinApiException("Unable to store hidden window handle on desktop window");
            }
        }

        public void UnregisterHooks()
        {
            UnregisterShellHook(Identifiers.NamedPipe32);
            if (Is64BitArch)
                UnregisterShellHook(Identifiers.NamedPipe64);
        }

        private static void UnregisterShellHook(string namedPipe)
        {
            new NamedPipeClient().ConnectToPipeServer(namedPipe, ShellHookTerminationTimeout);
        }

        private bool? _arch64;
        private bool Is64BitArch
        {
            get
            {
                if (_arch64 == null)
                {
                    _arch64 = Is64Bit();
                }
                return _arch64.Value;
            }
        }

        private static bool Is64Bit()
        {
            if (IntPtr.Size == 8)
            {
                return true;
            }
            using (Process proc = Process.GetCurrentProcess())
            {
                bool returnValue;
                if (!WinApi.IsWow64Process(proc.Handle, out returnValue))
                {
                    return false;
                }
                return returnValue;
            }
        }
    }
}