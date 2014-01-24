#pragma once
#include "epcBuzzerLib.h"

class BeepUtil
{
public:
	static void Init(CString &str);
	static void BeepOk(void);
	static void BeepError(void);
	static void BeepClose(void);
	static void End();
private:
	static HMODULE handle;
	static EpcBuzzerOn epcBuzzerOn;
	static EpcBuzzerOff epcBuzzerOff;
	static EpcBuzzerBeeps epcBuzzerBeeps;
	static CString sCurPath;
};
