#include "stdafx.h"
#include "MyString.h"

MyString::MyString(void)
{
}

MyString::~MyString(void)
{
}

void MyString::ByteToString(const BYTE *pData, CString *sString, int Length)
{
	CString strSingleByte;
	for (int i = 0; i < Length; i++)
	{
		strSingleByte.Format(_T("%02X"), pData[i]);

		// 如果不是最后一个数据，则添加一个填充字符，用来隔开两个字节数据
		if (i < (Length - 1))
		{
			*sString += strSingleByte;
			*sString += _T(" ");
		}
		else
		{
			*sString += strSingleByte;
		}
	}
	return;
}


int MyString::StringToByte(CString sString, BYTE *pData, int Length)
{
	CString strSingleByte;
	int nLength = sString.GetLength();
	int i;

	if (nLength > (Length * 3))
	{
		nLength = Length * 3;
	}

	for (i = 0; i <= nLength/3; i++)
	{
		strSingleByte = sString.Mid(3*i, 2);

		// 如果字符串为空，则数据填为0
		if (strSingleByte.IsEmpty())
		{
			pData[i] = 0;
		}
		else
		{
			pData[i] = 0;
			strSingleByte.MakeUpper();

			for (int j = 0; j < 2; j++)
			{
				pData[i] <<= 4;

				if (isdigit(strSingleByte[j]))
				{// 为数字 '0' ~ '9'
					pData[i] |= strSingleByte[j] - '0';
				}
				else
				{// 为数字 'A' ~ 'F'
					pData[i] |= strSingleByte[j] - 'A' + 0x0A;
				}
			}
		}
	}
	return i;
}
void MyString::StringSplit(CString str,CString splitchars,vector<CString> &strVec)
{
	strVec.clear();
	while (true)
	{
		CString n = str.SpanExcluding(splitchars);
		strVec.push_back(n);
		str = str.Right(str.GetLength()-n.GetLength()-1);
		if (str.IsEmpty())
		{
			break;
		}
	}
	return ;
}
void MyString::BCDToDecimal(const CString * bcd,CString *dec)
{
	double value=0;
	int bei=1;
	int num=0;
	for(int i=0;i<bcd->GetLength();i++)
	{
		char c=(char)bcd->GetAt(i);
		if(c == ' ')
		{
			value+=num*bei;
			num=0;
			bei*=100;
		}
		else
		{
			num*=10;
			num+=c-'0';
		}
	}
	value+=num*bei;
	value/=100;
	dec->Format(L"%.2lf",value);
}