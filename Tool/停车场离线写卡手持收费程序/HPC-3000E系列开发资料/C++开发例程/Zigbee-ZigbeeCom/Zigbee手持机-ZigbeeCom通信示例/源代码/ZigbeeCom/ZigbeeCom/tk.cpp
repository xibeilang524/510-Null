#include "stdafx.h"
#include "tk.h"


namespace TK
{
    void ShowFullScreen( HWND hwnd )
    {
        // 全屏显示
        int iFullWidth = GetSystemMetrics( SM_CXSCREEN );
        int iFullHeight = GetSystemMetrics( SM_CYSCREEN );
        ::SetWindowPos( hwnd, HWND_TOPMOST, 0, 0, iFullWidth, iFullHeight, 
            SWP_NOOWNERZORDER|SWP_SHOWWINDOW );
    }

    CString GetAppPath()
    {
        TCHAR Filename[MAX_PATH] = {0};
        GetModuleFileName(NULL, Filename, _MAX_PATH);
        CString str = Filename;
        int npos = str.ReverseFind('\\');
        if (npos < 0)
        {
            npos = str.ReverseFind('/');
        }
        CString strResult;
        if (npos >= 0)
        {
            strResult = str.Left(npos+1);
        }
        return strResult;
    }

    BOOL IsFolderExist( CString strPath )
    {
        WIN32_FIND_DATA   wfd;
        BOOL rValue = FALSE;
        HANDLE hFind = FindFirstFile( strPath, &wfd );
        if ( (hFind != INVALID_HANDLE_VALUE) && 
            (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) )
        {
            rValue = TRUE;   
        }

        FindClose(hFind);
        return rValue;
    }

    BOOL IsLetter( TCHAR ch )
    {
        return (( ch >= _T('a') && ch <= _T('f') )|| ( ch >= _T('A') && ch <= _T('F') ));
    }

    BOOL IsNumber( TCHAR ch )
    {
        return ( ch >= '0' && ch <= '9' );
    }

    BOOL VertifyCodeString( const CString& str , HWND hwnd )
    {
        if ( str.GetLength() != 4 )
        {
            ::MessageBox( hwnd, _T("长度不是4个字符！"), _T("提示"), MB_OK|MB_ICONINFORMATION);
            return FALSE;
        }

        for ( int i = 0; i < str.GetLength(); ++i )
        {
            if ( !IsLetter( str.GetAt(i) ) && !IsNumber( str.GetAt(i) ) )
            {
                ::MessageBox( hwnd, _T("字符必须是a~f或者A~F或者数字！"), _T("提示"), 
                    MB_OK|MB_ICONINFORMATION );

                return FALSE;
            }
        }

        return TRUE;
    }


    void SetCombSel( CComboBox& comb, int data )
    {
        comb.SetCurSel( 0 );
        for ( int i = 0; i < comb.GetCount(); ++i )
        {
            if ( comb.GetItemData( i ) == data )
            {
                comb.SetCurSel( i );
                break;
            }
        }
    }

    CString GetComPort( int port )
    {
        CString str;
        str.Format( _T("COM%d:"), port );
        return str;
    }

    void U2M(LPSTR t, LPCWSTR f, UINT CodePage)//UnicodeToMultiByte
    {
        int nLen = WideCharToMultiByte (CodePage,0,f,-1,NULL,0,NULL,NULL);
        LPSTR lpsz = new char[nLen];
        WideCharToMultiByte(CodePage,0,f,-1,lpsz,nLen,NULL,NULL);
        strcpy(t, lpsz);
        delete[] lpsz;
    } 

    void M2U(LPWSTR t, LPCSTR f , UINT CodePage)//MultiByteToUnicode
    {
        int nLen = MultiByteToWideChar(CodePage, 0, f, -1, NULL, NULL);
        LPWSTR lpszW = new WCHAR[nLen];
        MultiByteToWideChar(CodePage, 0,  f, -1, lpszW, nLen);
        wcscpy(t, lpszW);
        delete[] lpszW;
    } 

    void Unicode2Ansi(LPSTR to, LPCWSTR from)
    {//CP_ACP
        U2M(to, from, CP_ACP);
    }

    int GetLenUnicode2Ansi(LPCWSTR from)
    {
        return WideCharToMultiByte (CP_ACP, 0, from, -1, NULL, 0, NULL, NULL);
    }

    void Ansi2Unicode(LPWSTR to, LPCSTR from)
    {
        M2U(to, from, CP_ACP);
    }

    int GetLenAnsi2Unicode(LPCSTR from)
    {
        return MultiByteToWideChar(CP_ACP, 0, from, -1, NULL, NULL);
    }
};

