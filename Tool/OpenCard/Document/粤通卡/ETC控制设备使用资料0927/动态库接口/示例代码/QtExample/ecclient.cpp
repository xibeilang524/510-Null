#include "ecclient.h"
#include <Windows.h>
#include <stdexcept>


struct DLLHandle
{
    HMODULE m_h;

    DLLHandle(const char* filename)
    {

        m_h = LoadLibraryA(filename);

        if(m_h == 0)
        {
            throw std::runtime_error("load DLL failed");
        }
    }

    ~DLLHandle()
    {
        FreeLibrary(m_h);
    }
};

struct ECClient::Impl
{
    DLLHandle	    m_DLL;

    template <typename T>
    inline void loadProc(T& dst, const char* name)
    {
        dst = reinterpret_cast<T>(GetProcAddress(m_DLL.m_h, name));

        if(dst == NULL)
        {
            throw std::runtime_error(name);
        }
    }

    Impl(ECClient& client) : m_DLL("EtcController.dll")
    {
#define load(name) loadProc(client.name, #name)
        load(Initialize);
        load(Uninstall);
        load(connectserver);
        load(quitserver);
        load(HeartBeat);
        load(LaneLogin);
        load(LaneQuit);

        load(RSUOpen);
        load(RSUClose);
        load(OBUSearch);
        load(GetCardNo);
        load(GetOBUInfo);
        load(GetOBUInfo_GD);
        load(RSUWriteCard);
        load(RSUWriteCard_GD);
        load(RSUTransActionProve);

        load(CardReaderOpen);
        load(CardReaderClose);
        load(CardSearch);
        load(GetCardInfo);
        load(GetCardInfo_GD);
        load(CardReaderWriteCard);
        load(CardReaderWriteCard_GD);
        load(CardReaderTransActionProve);

        load(ListUpLoad);
        load(ListQuery);

        load(SetParameter);

        load(BlackListQuery);

        load(StatusQuery);

        load(SendNonCashPay);
        load(StopNonCashPay);
        load(QueryTransDetail);
    }

    ~Impl()
    {
    }
};

ECClient::ECClient(void)
{
    m_pImpl = new Impl(*this);
}


ECClient::~ECClient(void)
{
    Uninstall();
    delete m_pImpl;
}
