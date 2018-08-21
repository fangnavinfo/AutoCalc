#pragma once
#pragma once
#include <windows.h>

#ifdef HOOK_EXPORTS
#define HOOKDLL_API __declspec(dllexport)
#else
#define HOOKDLL_API __declspec(dllimport)
#endif
extern "C" 
{
	HOOKDLL_API BOOL SetHook();
	HOOKDLL_API BOOL EndHook();
	HOOKDLL_API void RegeditHideWindows(const char* winText);
	HOOKDLL_API void RegeditHideWindowsContains(const char* winText);
}