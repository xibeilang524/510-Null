// PicView.cpp : 实现文件
//

#include "stdafx.h"
#include "PicView.h"
#include "resource.h"


CPicView::CPicView()
{
}

CPicView::CPicView(CDC * pdc)
{
    memdc.CreateCompatibleDC(pdc);
    this->pdc=pdc;
}

CPicView::CPicView(CDC * pdc,CString path)
{
    //AfxMessageBox(L"开始加载背景图片"+path);
    memdc.CreateCompatibleDC(pdc);
    this->pdc=pdc;
    CBitmap *bmp=NULL;
    HBITMAP hbmp;
    hbmp=SHLoadDIBitmap(path);
    if(!hbmp)
    {
        AfxMessageBox(L"界面图片不存在或者位置不正确");
        exit(-1);
    }else
    {
        //bmp=CBitmap::FromHandle(hbmp);
        bmp=new CBitmap();
        bmp->Attach(hbmp);
    }
    BITMAP bmpinfo;
    bmp->GetBitmap(&bmpinfo);
    width = bmpinfo.bmWidth;
    height = bmpinfo.bmHeight;
    memdc.SelectObject(bmp);
    bmp->Detach();
    delete bmp;
}
CPicView::~CPicView()
{
    memdc.DeleteDC();
}

BOOL CPicView::drawDefault(CRect * rect,UINT idb)
{
    CBitmap bmp,*pOldBmp;
    bmp.LoadBitmap(idb);
    pOldBmp=memdc.SelectObject(&bmp);
    BITMAP bmpinfo;
    bmp.GetBitmap(&bmpinfo);
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,0,0,bmpinfo.bmWidth,bmpinfo.bmHeight,SRCCOPY);
    memdc.SelectObject(pOldBmp);
    DeleteObject(bmp);
    return TRUE;
}

BOOL CPicView::drawDefault(CRect * rect,CString str)
{
    if(str.IsEmpty())
    {
        drawDefault(rect,IDB_BKG);
        return FALSE;
    }
    CBitmap *bmp,*pOldBmp;
    HBITMAP hbmp;
    hbmp=SHLoadDIBitmap(str);
    if(!hbmp)
    {
        drawDefault(rect,IDB_BKG);
        return FALSE;
    }
    bmp=CBitmap::FromHandle(hbmp);
    pOldBmp=memdc.SelectObject(bmp);
    BITMAP bmpinfo;
    bmp->GetBitmap(&bmpinfo);
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,0,0,bmpinfo.bmWidth,bmpinfo.bmHeight,SRCCOPY);
    memdc.SelectObject(pOldBmp);
    ::DeleteObject(hbmp);
    return TRUE;
}

BOOL CPicView::drawDefault(CRect * rect,CRect * src,CString str)
{
    if(str.IsEmpty())
    {
        drawDefault(rect,IDB_BKG);
        return FALSE;
    }
    CBitmap *bmp,*pOldBmp;
    HBITMAP hbmp;
    hbmp=SHLoadDIBitmap(str);
    if(!hbmp)
    {
        drawDefault(rect,IDB_BKG);
        return FALSE;
    }
    bmp=CBitmap::FromHandle(hbmp);
    pOldBmp=memdc.SelectObject(bmp);
    BITMAP bmpinfo;
    bmp->GetBitmap(&bmpinfo);
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,src->left,src->top,src->Width(),src->Height(),SRCCOPY);
    memdc.SelectObject(pOldBmp);
    ::DeleteObject(hbmp);
    return TRUE;
}
BOOL CPicView::drawDefault(CRect * rect,CBitmap *bmp)
{
    if(bmp==NULL)
    {
        drawDefault(rect,IDB_BKG);
        return FALSE;
    }
    CBitmap *pOldBmp;
    pOldBmp=memdc.SelectObject(bmp);
    BITMAP bmpinfo;
    bmp->GetBitmap(&bmpinfo);
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,0,0,bmpinfo.bmWidth,bmpinfo.bmHeight,SRCCOPY);
    memdc.SelectObject(pOldBmp);
    return TRUE;
}
BOOL CPicView::drawDefault(CRect * rect)
{
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,0,0,width,height,SRCCOPY);
    return TRUE;
}

BOOL CPicView::drawDefault( CRect * rect,CSize *size )
{
    pdc->StretchBlt(rect->left,rect->top,rect->Width(),rect->Height(),&memdc,
        rect->left*width/size->cx,rect->top*height/size->cy,rect->Width()*width/size->cx,rect->Height()*height/size->cy,SRCCOPY);
    return TRUE;
}
int CPicView::getWidth()
{
    return width;
}

int CPicView::getHeight()
{
    return height;
}