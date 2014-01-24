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
