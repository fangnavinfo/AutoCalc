#pragma once

extern "C"
{
	__declspec(dllexport) void ImageCheckInit();
	__declspec(dllexport) void ImageCheckExit();
	__declspec(dllexport) void CheckExposure(const char* path, int& total, int& over, int& under);
}