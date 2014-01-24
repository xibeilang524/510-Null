#pragma once


class CPicView
{

public:
    CPicView();
    CPicView(CDC * pdc);
    CPicView(CDC * pdc,CString path);
    virtual ~CPicView();
public:
    BOOL drawDefault(CRect * rect,UINT idb);
    BOOL drawDefault(CRect * rect,CString str);
    BOOL drawDefault(CRect * rect,CRect * src,CString str);
    BOOL drawDefault(CRect * rect,CBitmap *bmp);
    BOOL drawDefault(CRect * rect);
    BOOL drawDefault(CRect * rect,CSize *size);
    int getWidth();
    int getHeight();
private:
    CDC memdc;
    CDC *pdc;
    CRect m_rectClient;
    int width;
    int height;

protected:
};


