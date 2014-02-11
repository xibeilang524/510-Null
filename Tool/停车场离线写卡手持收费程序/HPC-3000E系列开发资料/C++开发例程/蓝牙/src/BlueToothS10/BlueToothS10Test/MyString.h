#pragma once
#include <vector>
using std::vector;

class MyString
{
public:
	MyString(void);
	~MyString(void);
	static int StringToByte(CString sString, BYTE *pData, int Length);
	static void ByteToString(const BYTE *pData, CString *sString, int Length);
	static void StringSplit(CString str,CString splitchars,vector<CString> &strVec);
	static void BCDToDecimal(const CString * bcd,CString *dec);
};
