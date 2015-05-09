#include <windows.h>
#include <memory.h>

//
// Capture the application instance of this module to pass to
// hook initialization.
//
extern HINSTANCE g_appInstance;
extern UINT g_customWindowMessage;
extern HWND g_destinationWindow;


BOOL APIENTRY DllMain(HINSTANCE hinstDLL, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        //
        // Capture the application instance of this module to pass to
        // hook initialization.
        //
        if (g_appInstance == NULL)
        {
            g_appInstance = hinstDLL;
            g_customWindowMessage = RegisterWindowMessage("PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2");
            g_destinationWindow = (HWND)GetProp(GetDesktopWindow(), "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6");
        }
        break;

    case DLL_THREAD_ATTACH:
        break;

    case DLL_THREAD_DETACH:
        break;

    case DLL_PROCESS_DETACH:
        break;

    default:
        break;
    }

    return TRUE;
}