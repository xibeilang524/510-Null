#include "StdAfx.h"
#include "BeepUtil.h"
#include "epcBuzzerLib.h"
#include "resource.h"

HMODULE BeepUtil::handle = NULL;
EpcBuzzerOn BeepUtil::epcBuzzerOn = NULL;
EpcBuzzerOff BeepUtil::epcBuzzerOff = NULL;
EpcBuzzerBeeps BeepUtil::epcBuzzerBeeps = NULL;
CString BeepUtil::sCurPath = _T("");

void BeepUtil::BeepOk(void)
{
	if(handle)
		epcBuzzerBeeps(1,200,0);
	else
		PlaySound(sCurPath + _T("ok.wav"),NULL,SND_FILENAME|SND_SYNC);
}

void BeepUtil::BeepError(void) 
{
	if(handle)
		epcBuzzerBeeps(3,100,50);
	else
		PlaySound(sCurPath + _T("err.wav"),NULL,SND_FILENAME|SND_SYNC);
}

void BeepUtil::BeepClose(void)
{
	if(handle)
		epcBuzzerOff();
}

void BeepUtil::Init(CString &str)
{
	handle = LoadLibrary(_T("epcBuzzerLib.dll"));
	if(handle)
	{
		epcBuzzerOn = (EpcBuzzerOn)GetProcAddress(handle,_T("epcBuzzerOn"));
		epcBuzzerOff = (EpcBuzzerOff)GetProcAddress(handle,_T("epcBuzzerOff"));
		epcBuzzerBeeps = (EpcBuzzerBeeps)GetProcAddress(handle,_T("epcBuzzerBeeps"));
	}
	sCurPath = str + _T("music\\");
}

void BeepUtil::End()
{
	handle && FreeLibrary(handle);
}