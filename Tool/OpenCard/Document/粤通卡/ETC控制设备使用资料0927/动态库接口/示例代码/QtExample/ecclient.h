#ifndef ECCLIENT_H
#define ECCLIENT_H


typedef int (__stdcall *lpInitialize)(char* pResult, int* pLaneNum, char* pErrMsg);
typedef int (__stdcall *lpUninstall)(void);

typedef int (__stdcall *lpconnectserver)(int iLaneNo, const char* ip,int port);
typedef int (__stdcall *lpSingle)(int iLaneNo);
typedef int (__stdcall *lpNormal)(int iLaneNo,const char* pReq, char* pResp);

/**
 * @brief 动态加载DLL接口
 */
class ECClient
{
public:
    ECClient(void);
    ~ECClient(void);

public:

    lpInitialize Initialize;
    lpUninstall  Uninstall;
    lpconnectserver connectserver;
    lpSingle quitserver;
    lpSingle HeartBeat;
    lpNormal LaneLogin;
    lpNormal LaneQuit;

    lpNormal RSUOpen;
    lpNormal RSUClose;
    lpNormal OBUSearch;
    lpNormal GetCardNo;
    lpNormal GetOBUInfo;
    lpNormal GetOBUInfo_GD;
    lpNormal RSUWriteCard;
    lpNormal RSUWriteCard_GD;
    lpNormal RSUTransActionProve;

    lpNormal CardReaderOpen;
    lpNormal CardReaderClose;
    lpNormal CardSearch;
    lpNormal GetCardInfo;
    lpNormal GetCardInfo_GD;
    lpNormal CardReaderWriteCard;
    lpNormal CardReaderWriteCard_GD;
    lpNormal CardReaderTransActionProve;

    lpNormal ListUpLoad;
    lpNormal ListQuery;

    lpNormal SetParameter;

    lpNormal BlackListQuery;

    lpNormal StatusQuery;

    lpNormal SendNonCashPay;
    lpNormal StopNonCashPay;
    lpNormal QueryTransDetail;

private:

    struct Impl;
    Impl *m_pImpl;
};

#endif // ECCLIENT_H
