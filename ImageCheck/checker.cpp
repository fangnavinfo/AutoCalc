// ImageCheck.cpp : 定义 DLL 应用程序的导出函数。
//


#pragma once
#pragma once

#include "stdafx.h"
#include "ImageCheck.h"
#include "checker.h"

//ImageCheck* pcheck = NULL;

void ImageCheckInit()
{
	//pcheck = new ImageCheck();
}

void ImageCheckExit()
{
	//delete pcheck;
}

void CheckExposure(const char* path, int& total, int& over, int& under)
{
	ImageCheck* pcheck = new ImageCheck();

	pcheck->Load(path);

	over = pcheck->GetOver();
	under = pcheck->GetUnder();
	total = pcheck->GetTotal();

	delete pcheck;
}
