#include "StdAfx.h"
#include "MyBeep.h"
#include "epcBuzzerLib.h"

CMyBeep::CMyBeep(void)
{
}

CMyBeep::~CMyBeep(void)
{
}

void CMyBeep::BeepOk(void)
{
	epcBuzzerBeeps(1,300,0);
}

void CMyBeep::BeepError(void)
{
	epcBuzzerBeeps(3,100,50);
}