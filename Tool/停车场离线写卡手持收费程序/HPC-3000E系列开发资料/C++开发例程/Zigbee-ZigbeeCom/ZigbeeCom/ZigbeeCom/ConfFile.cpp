#include "stdafx.h"
#include "ConfFile.h"
#include "MyString.h"
#include "tk.h"

CConfFile::CConfFile(CString path)
{
	this->path=path;
}
bool CConfFile::Read(int & panid,vector<int> &channel,vector<int> &speed)
{
	panid=0x1001;
	channel.clear();
	speed.clear();
	CFile file;
	try
	{
		CFileException ex;
		if(!file.Open(path,CFile::modeRead|CFile::shareDenyWrite,&ex))
		{
			return false;
		}
		int len=(int)file.GetLength();
		char *pbuf=new char[len+1];
		file.Read(pbuf,len);
		WCHAR * pwch=new WCHAR[len+1];
		TK::Ansi2Unicode(pwch,pbuf);
		CString content(pwch);
		delete[] pbuf;
		delete[] pwch;
		vector<CString> ss;
		MyString::StringSplit(content,_T("\n"),ss);
		for(int i=0;i<(int)ss.size();i++)
		{
			if(ss[i].GetLength() != 0)
			{
				ss[i] = ss[i].Left(ss[i].GetLength()-1+(ss[i].Right(1).Compare(_T("\r"))==0?0:1));
			}
		}
		char buf[1024];
		if(ss.size() >= 1)
		{
			TK::Unicode2Ansi(buf,ss[0]);
			panid = strtoul(buf,0,10);
		}
		vector<CString> items;
		if(ss.size() >= 2)
		{
			MyString::StringSplit(ss[1],_T(","),items);
			for(int i=0;i<items.size();i++)
			{
				TK::Unicode2Ansi(buf,items[i]);
				channel.push_back(strtoul(buf,0,10));
			}
		}
		if(ss.size() >= 3)
		{
			MyString::StringSplit(ss[2],_T(","),items);
			for(int i=0;i<items.size();i++)
			{
				TK::Unicode2Ansi(buf,items[i]);
				speed.push_back(strtoul(buf,0,10));
			}
		}
		file.Close();
	}
	catch (CException* e)
	{
		file.Close();
		return false;
	}
	return true;
}