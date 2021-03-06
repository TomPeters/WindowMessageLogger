#include <Windows.h>

HINSTANCE g_appInstance = NULL;
UINT g_customWindowMessage = 0;
HWND g_destinationWindow = 0;

HHOOK hookCallWndProc = NULL;
HHOOK hookCallGetMsg = NULL;

static LRESULT CALLBACK WndProcCallback(int code, WPARAM wparam, LPARAM lparam) {
    if(code >= 0) {
        CWPSTRUCT* pCwpStruct = (CWPSTRUCT*)lparam;
        UINT message = pCwpStruct->message;
        if (message == WM_CREATE) {
            HWND hwnd = pCwpStruct->hwnd;
            SendNotifyMessage(g_destinationWindow, g_customWindowMessage, (WPARAM)pCwpStruct->hwnd, pCwpStruct->message);
        }
    }
    return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
}

static LRESULT CALLBACK GetMsgCallback(int code, WPARAM wparam, LPARAM lparam) {
    if (code >= 0) {
        // Move this out of the callback
        //UINT g_customWindowMessage = RegisterWindowMessage("PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2");
        //HWND g_destinationWindow = (HWND)GetProp(GetDesktopWindow(), "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6");
        if (g_customWindowMessage != 0 && g_destinationWindow != 0) {
            MSG* msg = (MSG*)lparam;
            //SendNotifyMessage(destinationWindow, g_customWindowMessage, (WPARAM)msg->hwnd, msg->message);
        }
    }
    return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
}

extern "C"
__declspec(dllexport)
HHOOK SetupWndProcHook() {
    hookCallWndProc = SetWindowsHookEx(WH_CALLWNDPROC, (HOOKPROC)WndProcCallback, g_appInstance, 0);
    return hookCallWndProc;
}

// Also need to hook into GetMessage to ensure we get all messages (WndProc may not be not enough)
extern "C"
__declspec(dllexport)
HHOOK SetupGetMsgHook() {
    hookCallGetMsg = SetWindowsHookEx(WH_GETMESSAGE, (HOOKPROC)GetMsgCallback, g_appInstance, 0);
    return hookCallWndProc;
}

