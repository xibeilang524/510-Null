//////////////////////////////////////////////////////////////////////////  
/// COPY RIGHT NOTICE  
/// Copyright(c) 2012，广州致远  
/// All rights reserved.  
///  
/// @file tk.h  
/// @brief 一些公用的函数和辅助函数  
///  
///   
///  
/// @date 2012/03/23  
///  
///
////////////////////////////////////////////////////////////////////////// 


#pragma once
#include <algorithm>
#include <vector>
using namespace std;


namespace TK
{

    //定义一个delete ptr的防函数
    struct DeletePtr
    {
        template<class T>
        void operator() (T* t)
        {
            delete t;
        }
    };

    void ShowFullScreen( HWND hwnd );

    //获得程序路径，带\符号
    CString GetAppPath();

    // 判断目录是否存在
    BOOL IsFolderExist( CString strDir );

    // 检测ID码和SN码是否正确
    // 长度最大为4，只能是字母和数字
    BOOL VertifyCodeString( const CString& str , HWND hwnd );

    void SetCombSel( CComboBox& comb, int data );

    CString GetComPort( int port );

    int U2M(LPSTR t,LPCWSTR f, UINT CodePage); //UnicodeToMultiByte
    void M2U(LPWSTR t,LPCSTR f ,UINT CodePage);//MultiByteToUnicode
    int Unicode2Ansi(LPSTR to,LPCWSTR from);
    int  GetLenUnicode2Ansi(LPCWSTR from);
    int  GetLenAnsi2Unicode(LPCSTR from);
    void Ansi2Unicode(LPWSTR to,LPCSTR from);
};
