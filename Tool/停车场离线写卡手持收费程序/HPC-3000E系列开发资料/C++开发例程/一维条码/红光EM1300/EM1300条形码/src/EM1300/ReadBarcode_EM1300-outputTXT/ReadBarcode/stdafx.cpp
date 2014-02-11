// stdafx.cpp : 只包括标准包含文件的源文件
// ReadBarcode.pch 将作为预编译头
// stdafx.obj 将包含预编译类型信息

#include "stdafx.h"
//增加打印log信息
void Log(const char * s,int length)
{
    FILE *pFile = fopen("\\Windows\\log.txt","a");
    fwrite(s,1,length,pFile);
    fclose(pFile);
}

