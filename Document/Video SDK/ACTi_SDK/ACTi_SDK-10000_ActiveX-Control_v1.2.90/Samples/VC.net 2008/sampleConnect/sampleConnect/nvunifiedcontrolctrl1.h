#pragma once

// Machine generated IDispatch wrapper class(es) created by Microsoft Visual C++

// NOTE: Do not modify the contents of this file.  If this class is regenerated by
//  Microsoft Visual C++, your modifications will be overwritten.

/////////////////////////////////////////////////////////////////////////////
// CNvunifiedcontrolctrl1 wrapper class

class CNvunifiedcontrolctrl1 : public CWnd
{
protected:
	DECLARE_DYNCREATE(CNvunifiedcontrolctrl1)
public:
	CLSID const& GetClsid()
	{
		static CLSID const clsid
			= { 0xF8E691A0, 0xC92E, 0x4E42, { 0x9C, 0xDA, 0x62, 0xFC, 0x7, 0xA9, 0x48, 0x3B } };
		return clsid;
	}
	virtual BOOL Create(LPCTSTR lpszClassName, LPCTSTR lpszWindowName, DWORD dwStyle,
						const RECT& rect, CWnd* pParentWnd, UINT nID, 
						CCreateContext* pContext = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID); 
	}

    BOOL Create(LPCTSTR lpszWindowName, DWORD dwStyle, const RECT& rect, CWnd* pParentWnd, 
				UINT nID, CFile* pPersist = NULL, BOOL bStorage = FALSE,
				BSTR bstrLicKey = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID,
		pPersist, bStorage, bstrLicKey); 
	}

// Attributes
public:
typedef enum
{
    QUAD_MODE = 0,
    SINGLE_MODE = 1,
    SEQUENTIAL_MODE = 2,
    AUTO_DETECT = 3
}DeviceType;

typedef enum
{
    _SINGLE_CHANNEL_VIDEO_SERVER = 0,
    _ACD2000Q_VIDEO_SERVER = 1,
    _SED2300Q_VIDEO_SERVER = 2,
    _AUTO_DETECT = 3
}QuadDeviceMode;


// Operations
public:

// _DnvUnifiedControl

// Functions
//

	long Connect(long AsyncConnection)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x11, DISPATCH_METHOD, VT_I4, (void*)&result, parms, AsyncConnection);
		return result;
	}
	void Play()
	{
		InvokeHelper(0x12, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void Disconnect()
	{
		InvokeHelper(0x13, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long GetAudioToken()
	{
		long result;
		InvokeHelper(0x14, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void FreeAudioToken()
	{
		InvokeHelper(0x15, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long StartAudioTransfer()
	{
		long result;
		InvokeHelper(0x16, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void StopAudioTransfer()
	{
		InvokeHelper(0x17, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long DigitalZoomIn(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x18, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	long DigitalZoomOut(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x19, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	long DigitalMoveLeft(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	long DigitalMoveRight(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1b, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	long DigitalMoveUp(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1c, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	long DigitalMoveDown(long nSteps)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1d, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nSteps);
		return result;
	}
	void DigitalZoomOutMax()
	{
		InvokeHelper(0x1e, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void Stop()
	{
		InvokeHelper(0x1f, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long StartMDSetup()
	{
		long result;
		InvokeHelper(0x20, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void StopMDSetup()
	{
		InvokeHelper(0x21, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long GetMotionSensitive(long nRegionNumber)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x22, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	long GetMotionStartX(long nRegionNumber)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x23, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	long GetMotionStartY(long nRegionNumber)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x24, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	long GetMotionEndX(long nRegionNumber)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x25, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	long GetMotionEndY(long nRegionNumber)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x26, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	void SetMotionSetting(long nMotionRegionNumber, long nStartX, long nStartY, long nEndX, long nEndY, long nSensitive)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x27, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nMotionRegionNumber, nStartX, nStartY, nEndX, nEndY, nSensitive);
	}
	void SetMotionSensitivity(long nMotionRegionNumber, long nSensitive)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 ;
		InvokeHelper(0x28, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nMotionRegionNumber, nSensitive);
	}
	long EnableMotionDetection()
	{
		long result;
		InvokeHelper(0x29, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void DisableMotionDetection()
	{
		InvokeHelper(0x2a, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void SetTitleBarTextLayout(long n1, long n2, long n3, long n4, long n5, long n6, long n7, long n8, long n9, long n10, long n11, long n12, long n13, long n14, long n15, long n16, long n17, long n18, long n19, long n20)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x2b, DISPATCH_METHOD, VT_EMPTY, NULL, parms, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20);
	}
	void EnableMouseDigitalPTZ()
	{
		InvokeHelper(0x2f, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void DisableMouseDigitalPTZ()
	{
		InvokeHelper(0x30, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void DisplayTitleBar(long Display)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x31, DISPATCH_METHOD, VT_EMPTY, NULL, parms, Display);
	}
	void EnableFullScreen()
	{
		InvokeHelper(0x32, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long SnapShot(long IFormat, LPCTSTR FileName, long ReduplicateTitleBarText, long R_TextColor, long G_TextColor, long B_TextColor)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_BSTR VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x33, DISPATCH_METHOD, VT_I4, (void*)&result, parms, IFormat, FileName, ReduplicateTitleBarText, R_TextColor, G_TextColor, B_TextColor);
		return result;
	}
	void SetControlActive(long nActive)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x37, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nActive);
	}
	void DigitalOutput(long nDO1, long nDO2, long nDO3, long nDO4)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x38, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nDO1, nDO2, nDO3, nDO4);
	}
	long ExecuteURLCommand(LPCTSTR szCommand)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x39, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szCommand);
		return result;
	}
	long SyncMDInfoFromDevice()
	{
		long result;
		InvokeHelper(0x3d, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long EnableDigitalInput()
	{
		long result;
		InvokeHelper(0x3e, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void DisableDigitalInput()
	{
		InvokeHelper(0x3f, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	CString SendURLCmdToSE(LPCTSTR szCommand)
	{
		CString result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x47, DISPATCH_METHOD, VT_BSTR, (void*)&result, parms, szCommand);
		return result;
	}
	CString SendURLCmd(LPCTSTR szCommand)
	{
		CString result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x48, DISPATCH_METHOD, VT_BSTR, (void*)&result, parms, szCommand);
		return result;
	}
	void SetCurrentTime(long nCurrentTime)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x4b, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nCurrentTime);
	}
	void SetPlayDirection(short shDirection)
	{
		static BYTE parms[] = VTS_I2 ;
		InvokeHelper(0x4c, DISPATCH_METHOD, VT_EMPTY, NULL, parms, shDirection);
	}
	void SetPlayRate(long nPlayRate)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x4d, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nPlayRate);
	}
	void Pause()
	{
		InvokeHelper(0x4e, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void PlayByFrame(short nDirection)
	{
		static BYTE parms[] = VTS_I2 ;
		InvokeHelper(0x4f, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nDirection);
	}
	void SetDecodeI(short DecodeIOnly)
	{
		static BYTE parms[] = VTS_I2 ;
		InvokeHelper(0x50, DISPATCH_METHOD, VT_EMPTY, NULL, parms, DecodeIOnly);
	}
	long EnablePTZ()
	{
		long result;
		InvokeHelper(0x57, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void DisablePTZ()
	{
		InvokeHelper(0x58, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long EnableMousePTZ()
	{
		long result;
		InvokeHelper(0x59, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void DisableMousePTZ()
	{
		InvokeHelper(0x5a, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void PTZMove(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x5e, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZZoom(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x5f, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZFocus(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x60, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZIris(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x61, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZOSD(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x62, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZPreset(LPCTSTR szInputCommand, long nPresetIndex)
	{
		static BYTE parms[] = VTS_BSTR VTS_I4 ;
		InvokeHelper(0x63, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand, nPresetIndex);
	}
	void SendPTZCommand(LPCTSTR szHEXStringCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x64, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szHEXStringCommand);
	}
	long StartDecodeMode()
	{
		long result;
		InvokeHelper(0x65, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long DecodeFrame(unsigned char * pFrameData, long nDataLen, long nDataType)
	{
		long result;
		static BYTE parms[] = VTS_PUI1 VTS_I4 VTS_I4 ;
		InvokeHelper(0x66, DISPATCH_METHOD, VT_I4, (void*)&result, parms, pFrameData, nDataLen, nDataType);
		return result;
	}
	void StopDecodeMode()
	{
		InvokeHelper(0x67, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void EnableMoudeMoveEvent(long nEnable)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x68, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nEnable);
	}
	long StartRecord(LPCTSTR szFileName)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x6a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szFileName);
		return result;
	}
	void StopRecord()
	{
		InvokeHelper(0x6b, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long SetQuadMotionDetection(unsigned char byEnable)
	{
		long result;
		static BYTE parms[] = VTS_UI1 ;
		InvokeHelper(0x78, DISPATCH_METHOD, VT_I4, (void*)&result, parms, byEnable);
		return result;
	}
	long SendAudio(unsigned char * pbyAudioBuffer, long nLength)
	{
		long result;
		static BYTE parms[] = VTS_PUI1 VTS_I4 ;
		InvokeHelper(0x79, DISPATCH_METHOD, VT_I4, (void*)&result, parms, pbyAudioBuffer, nLength);
		return result;
	}
	void StartStream()
	{
		InvokeHelper(0x7a, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void StopStream()
	{
		InvokeHelper(0x7b, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long StartAlarmRecord(LPCTSTR szFileName)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x7d, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szFileName);
		return result;
	}
	void StopAlarmRecord()
	{
		InvokeHelper(0x7e, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	CString EnumerateVendor()
	{
		CString result;
		InvokeHelper(0x80, DISPATCH_METHOD, VT_BSTR, (void*)&result, NULL);
		return result;
	}
	CString EnumerateProtocol(LPCTSTR szVendor)
	{
		CString result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x81, DISPATCH_METHOD, VT_BSTR, (void*)&result, parms, szVendor);
		return result;
	}
	void PTZBLC(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x82, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	void PTZDayNight(LPCTSTR szInputCommand)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x83, DISPATCH_METHOD, VT_EMPTY, NULL, parms, szInputCommand);
	}
	CString GetBeginTimeString()
	{
		CString result;
		InvokeHelper(0x84, DISPATCH_METHOD, VT_BSTR, (void*)&result, NULL);
		return result;
	}
	CString GetEndTimeString()
	{
		CString result;
		InvokeHelper(0x85, DISPATCH_METHOD, VT_BSTR, (void*)&result, NULL);
		return result;
	}
	void RequestsToCheckDeviceChipType(long nCheckIt)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x87, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nCheckIt);
	}
	long DecodeFrameEx(long Int32BufferPointer, long nDataLen, long nDataType)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x89, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Int32BufferPointer, nDataLen, nDataType);
		return result;
	}
	long SendPTZCmd(LPCTSTR szCommand, long nParam1, long nParam2)
	{
		long result;
		static BYTE parms[] = VTS_BSTR VTS_I4 VTS_I4 ;
		InvokeHelper(0x8a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szCommand, nParam1, nParam2);
		return result;
	}
	long SetTextOut(long nX, long nY, LPCTSTR szText, LPCTSTR szFontName, short nBold, long nFontWidth, long nFontHeight, long nFontR, long nFontG, long nFontB)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_BSTR VTS_BSTR VTS_I2 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x8b, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nX, nY, szText, szFontName, nBold, nFontWidth, nFontHeight, nFontR, nFontG, nFontB);
		return result;
	}
	void MirrorImage(long nEnable)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x8c, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nEnable);
	}
	void FlipImage(long nEnable)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x8d, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nEnable);
	}
	void EnablePrivacyMask(long nEnable, long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x8e, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nEnable, R, G, B);
	}
	void SetPrivacyMask(long nIndex, long nXStart, long nYStart, long nXEnd, long nYEnd)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x8f, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nIndex, nXStart, nYStart, nXEnd, nYEnd);
	}
	long EnableAbsPosition()
	{
		long result;
		InvokeHelper(0x90, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void DisableAbsPosition()
	{
		InvokeHelper(0x91, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long AddMultiplePlaybackFile(LPCTSTR szFileName)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x95, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szFileName);
		return result;
	}
	void RemoveAllMultiplePlaybackFile()
	{
		InvokeHelper(0x96, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	long PlayAudioFileToDevice(LPCTSTR szFileName)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x97, DISPATCH_METHOD, VT_I4, (void*)&result, parms, szFileName);
		return result;
	}
	void SetTitleActiveColor(long nIndex, long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x99, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nIndex, R, G, B);
	}
	void SetTitleNonActiveColor(long nIndex, long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x9a, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nIndex, R, G, B);
	}
	void SetTitleEventColor(long nIndex, long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x9b, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nIndex, R, G, B);
	}
	void SetPlayingBackgroundColor(long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x9c, DISPATCH_METHOD, VT_EMPTY, NULL, parms, R, G, B);
	}
	void SetDefaultBackgroundColor(long R, long G, long B)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0x9d, DISPATCH_METHOD, VT_EMPTY, NULL, parms, R, G, B);
	}
	long SetAutoDropFrame(long nEnable, long nCPUPerformance)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_I4 ;
		InvokeHelper(0x9e, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nEnable, nCPUPerformance);
		return result;
	}
	void StopPlayingAudioFile()
	{
		InvokeHelper(0x9f, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}
	void EnableFullScreenEx(long nUseSecondMonitor)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xa0, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nUseSecondMonitor);
	}
	long TxRS232Data(long pData, long numBytes)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_I4 ;
		InvokeHelper(0xa3, DISPATCH_METHOD, VT_I4, (void*)&result, parms, pData, numBytes);
		return result;
	}
	long AsyncDisconnect()
	{
		long result;
		InvokeHelper(0xa6, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long AsyncSendURLCmd(long nIndex, LPCTSTR szCommand)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_BSTR ;
		InvokeHelper(0xa7, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nIndex, szCommand);
		return result;
	}
	long EnableOnNewImageEvent(long nEnable)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xa9, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nEnable);
		return result;
	}
	long GetCurrentImage(unsigned char * pBuffer, long nBufferLen)
	{
		long result;
		static BYTE parms[] = VTS_PUI1 VTS_I4 ;
		InvokeHelper(0xaa, DISPATCH_METHOD, VT_I4, (void*)&result, parms, pBuffer, nBufferLen);
		return result;
	}
	long GetCurrentImageEx(long Int32BufferPointer, long nBufferLen)
	{
		long result;
		static BYTE parms[] = VTS_I4 VTS_I4 ;
		InvokeHelper(0xab, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Int32BufferPointer, nBufferLen);
		return result;
	}
	long EnableDecoder(long nEnable)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xac, DISPATCH_METHOD, VT_I4, (void*)&result, parms, nEnable);
		return result;
	}
	void EnableMouseMoveEvent(long nEnable)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xad, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nEnable);
	}
	unsigned long GetMotionRegionEnable(long nRegionNumber)
	{
		unsigned long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xb4, DISPATCH_METHOD, VT_UI4, (void*)&result, parms, nRegionNumber);
		return result;
	}
	void SetMotionRegionEnable(long nRegionNumber, unsigned long ulEnable)
	{
		static BYTE parms[] = VTS_I4 VTS_UI4 ;
		InvokeHelper(0xb5, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nRegionNumber, ulEnable);
	}
	void SetGlobalMotionEnable(unsigned long bEnable)
	{
		static BYTE parms[] = VTS_UI4 ;
		InvokeHelper(0xb9, DISPATCH_METHOD, VT_EMPTY, NULL, parms, bEnable);
	}
	unsigned long GetGlobalMotionEnable()
	{
		unsigned long result;
		InvokeHelper(0xba, DISPATCH_METHOD, VT_UI4, (void*)&result, NULL);
		return result;
	}
	void SnapshotWithTitleBar(unsigned long bEnable)
	{
		static BYTE parms[] = VTS_UI4 ;
		InvokeHelper(0xbb, DISPATCH_METHOD, VT_EMPTY, NULL, parms, bEnable);
	}
	void SetVideoTransformConfig(unsigned short bTransfer, long nVideoOutType, long BitRate, long nFpsNum, unsigned short bReSize, long nResolution)
	{
		static BYTE parms[] = VTS_UI2 VTS_I4 VTS_I4 VTS_I4 VTS_UI2 VTS_I4 ;
		InvokeHelper(0xbc, DISPATCH_METHOD, VT_EMPTY, NULL, parms, bTransfer, nVideoOutType, BitRate, nFpsNum, bReSize, nResolution);
	}
	void SetBorderWidth(long value)
	{
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xbd, DISPATCH_METHOD, VT_EMPTY, NULL, parms, value);
	}
	void SetBorderColor(long colorR, long colorG, long colorB)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0xbe, DISPATCH_METHOD, VT_EMPTY, NULL, parms, colorR, colorG, colorB);
	}
	long GetNumberOfMonitors()
	{
		long result;
		InvokeHelper(0xbf, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void ShowRealFPS(long nX, long nY, short bShow)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I2 ;
		InvokeHelper(0xc0, DISPATCH_METHOD, VT_EMPTY, NULL, parms, nX, nY, bShow);
	}
	void SetRealFpsFontColor(long bBold, long bItalic, long bUnderLine, LPCTSTR pFontName, long nFontSize, long nTextColorR, long nTextColorG, long nTextColorB, long nBKMode, long nBkR, long nBkG, long nBkB)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_BSTR VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0xc1, DISPATCH_METHOD, VT_EMPTY, NULL, parms, bBold, bItalic, bUnderLine, pFontName, nFontSize, nTextColorR, nTextColorG, nTextColorB, nBKMode, nBkR, nBkG, nBkB);
	}
	void SetTextOutEx(long index, long nX, long nY, LPCTSTR Text, long bBold, long bItalic, long bUnderLine, LPCTSTR pFontName, long nFontSize, long nTextColorR, long nTextColorG, long nTextColorB, long nBKMode, long nBkR, long nBkG, long nBkB)
	{
		static BYTE parms[] = VTS_I4 VTS_I4 VTS_I4 VTS_BSTR VTS_I4 VTS_I4 VTS_I4 VTS_BSTR VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 VTS_I4 ;
		InvokeHelper(0xc2, DISPATCH_METHOD, VT_EMPTY, NULL, parms, index, nX, nY, Text, bBold, bItalic, bUnderLine, pFontName, nFontSize, nTextColorR, nTextColorG, nTextColorB, nBKMode, nBkR, nBkG, nBkB);
	}
	long GetFullScreenStatus()
	{
		long result;
		InvokeHelper(0xc5, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	void CloseFullScreenWindow()
	{
		InvokeHelper(0xc6, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
	}

// Properties
//

long GetID()
{
	long result;
	GetProperty(0x1, VT_I4, (void*)&result);
	return result;
}
void SetID(long propVal)
{
	SetProperty(0x1, VT_I4, propVal);
}
CString GetMediaSource()
{
	CString result;
	GetProperty(0x2, VT_BSTR, (void*)&result);
	return result;
}
void SetMediaSource(CString propVal)
{
	SetProperty(0x2, VT_BSTR, propVal);
}
CString GetMediaUsername()
{
	CString result;
	GetProperty(0x3, VT_BSTR, (void*)&result);
	return result;
}
void SetMediaUsername(CString propVal)
{
	SetProperty(0x3, VT_BSTR, propVal);
}
CString GetMediaPassword()
{
	CString result;
	GetProperty(0x4, VT_BSTR, (void*)&result);
	return result;
}
void SetMediaPassword(CString propVal)
{
	SetProperty(0x4, VT_BSTR, propVal);
}
long GetHttpPort()
{
	long result;
	GetProperty(0x5, VT_I4, (void*)&result);
	return result;
}
void SetHttpPort(long propVal)
{
	SetProperty(0x5, VT_I4, propVal);
}
long GetResolution()
{
	long result;
	GetProperty(0x6, VT_I4, (void*)&result);
	return result;
}
void SetResolution(long propVal)
{
	SetProperty(0x6, VT_I4, propVal);
}
long GetVolume()
{
	long result;
	GetProperty(0x7, VT_I4, (void*)&result);
	return result;
}
void SetVolume(long propVal)
{
	SetProperty(0x7, VT_I4, propVal);
}
long GetStretchToFit()
{
	long result;
	GetProperty(0x8, VT_I4, (void*)&result);
	return result;
}
void SetStretchToFit(long propVal)
{
	SetProperty(0x8, VT_I4, propVal);
}
long GetBrightness()
{
	long result;
	GetProperty(0x9, VT_I4, (void*)&result);
	return result;
}
void SetBrightness(long propVal)
{
	SetProperty(0x9, VT_I4, propVal);
}
long GetContrast()
{
	long result;
	GetProperty(0xa, VT_I4, (void*)&result);
	return result;
}
void SetContrast(long propVal)
{
	SetProperty(0xa, VT_I4, propVal);
}
long GetHue()
{
	long result;
	GetProperty(0xb, VT_I4, (void*)&result);
	return result;
}
void SetHue(long propVal)
{
	SetProperty(0xb, VT_I4, propVal);
}
long GetSaturation()
{
	long result;
	GetProperty(0xc, VT_I4, (void*)&result);
	return result;
}
void SetSaturation(long propVal)
{
	SetProperty(0xc, VT_I4, propVal);
}
long GetRegisterPort()
{
	long result;
	GetProperty(0xd, VT_I4, (void*)&result);
	return result;
}
void SetRegisterPort(long propVal)
{
	SetProperty(0xd, VT_I4, propVal);
}
long GetControlPort()
{
	long result;
	GetProperty(0xe, VT_I4, (void*)&result);
	return result;
}
void SetControlPort(long propVal)
{
	SetProperty(0xe, VT_I4, propVal);
}
long GetStreamingPort()
{
	long result;
	GetProperty(0xf, VT_I4, (void*)&result);
	return result;
}
void SetStreamingPort(long propVal)
{
	SetProperty(0xf, VT_I4, propVal);
}
long GetAutoReconnect()
{
	long result;
	GetProperty(0x10, VT_I4, (void*)&result);
	return result;
}
void SetAutoReconnect(long propVal)
{
	SetProperty(0x10, VT_I4, propVal);
}
CString GetCaption()
{
	CString result;
	GetProperty(0x2c, VT_BSTR, (void*)&result);
	return result;
}
void SetCaption(CString propVal)
{
	SetProperty(0x2c, VT_BSTR, propVal);
}
long GetMotionDetectionInterval()
{
	long result;
	GetProperty(0x2d, VT_I4, (void*)&result);
	return result;
}
void SetMotionDetectionInterval(long propVal)
{
	SetProperty(0x2d, VT_I4, propVal);
}
long GetDigitalInputInterval()
{
	long result;
	GetProperty(0x2e, VT_I4, (void*)&result);
	return result;
}
void SetDigitalInputInterval(long propVal)
{
	SetProperty(0x2e, VT_I4, propVal);
}
long GetMute()
{
	long result;
	GetProperty(0x34, VT_I4, (void*)&result);
	return result;
}
void SetMute(long propVal)
{
	SetProperty(0x34, VT_I4, propVal);
}
long GetMediaChannel()
{
	long result;
	GetProperty(0x35, VT_I4, (void*)&result);
	return result;
}
void SetMediaChannel(long propVal)
{
	SetProperty(0x35, VT_I4, propVal);
}
long GetStreamType()
{
	long result;
	GetProperty(0x36, VT_I4, (void*)&result);
	return result;
}
void SetStreamType(long propVal)
{
	SetProperty(0x36, VT_I4, propVal);
}
long GetMediaType()
{
	long result;
	GetProperty(0x3a, VT_I4, (void*)&result);
	return result;
}
void SetMediaType(long propVal)
{
	SetProperty(0x3a, VT_I4, propVal);
}
long GetMulticastPort()
{
	long result;
	GetProperty(0x3b, VT_I4, (void*)&result);
	return result;
}
void SetMulticastPort(long propVal)
{
	SetProperty(0x3b, VT_I4, propVal);
}
CString GetMulticastIP()
{
	CString result;
	GetProperty(0x3c, VT_BSTR, (void*)&result);
	return result;
}
void SetMulticastIP(CString propVal)
{
	SetProperty(0x3c, VT_BSTR, propVal);
}
long GetDebugInfoPassword()
{
	long result;
	GetProperty(0x40, VT_I4, (void*)&result);
	return result;
}
void SetDebugInfoPassword(long propVal)
{
	SetProperty(0x40, VT_I4, propVal);
}
BOOL GetEnableASE()
{
	BOOL result;
	GetProperty(0x41, VT_BOOL, (void*)&result);
	return result;
}
void SetEnableASE(BOOL propVal)
{
	SetProperty(0x41, VT_BOOL, propVal);
}
CString GetASEMediaSource()
{
	CString result;
	GetProperty(0x42, VT_BSTR, (void*)&result);
	return result;
}
void SetASEMediaSource(CString propVal)
{
	SetProperty(0x42, VT_BSTR, propVal);
}
CString GetASEMediaUserName()
{
	CString result;
	GetProperty(0x43, VT_BSTR, (void*)&result);
	return result;
}
void SetASEMediaUserName(CString propVal)
{
	SetProperty(0x43, VT_BSTR, propVal);
}
CString GetASEMediaPassword()
{
	CString result;
	GetProperty(0x44, VT_BSTR, (void*)&result);
	return result;
}
void SetASEMediaPassword(CString propVal)
{
	SetProperty(0x44, VT_BSTR, propVal);
}
long GetASEControlPort()
{
	long result;
	GetProperty(0x45, VT_I4, (void*)&result);
	return result;
}
void SetASEControlPort(long propVal)
{
	SetProperty(0x45, VT_I4, propVal);
}
long GetASEStreamingPort()
{
	long result;
	GetProperty(0x46, VT_I4, (void*)&result);
	return result;
}
void SetASEStreamingPort(long propVal)
{
	SetProperty(0x46, VT_I4, propVal);
}
long GetBeginTime()
{
	long result;
	GetProperty(0x49, VT_I4, (void*)&result);
	return result;
}
void SetBeginTime(long propVal)
{
	SetProperty(0x49, VT_I4, propVal);
}
long GetEndTime()
{
	long result;
	GetProperty(0x4a, VT_I4, (void*)&result);
	return result;
}
void SetEndTime(long propVal)
{
	SetProperty(0x4a, VT_I4, propVal);
}
CString GetVendor()
{
	CString result;
	GetProperty(0x51, VT_BSTR, (void*)&result);
	return result;
}
void SetVendor(CString propVal)
{
	SetProperty(0x51, VT_BSTR, propVal);
}
CString GetProtocol()
{
	CString result;
	GetProperty(0x52, VT_BSTR, (void*)&result);
	return result;
}
void SetProtocol(CString propVal)
{
	SetProperty(0x52, VT_BSTR, propVal);
}
CString GetPTZFile()
{
	CString result;
	GetProperty(0x53, VT_BSTR, (void*)&result);
	return result;
}
void SetPTZFile(CString propVal)
{
	SetProperty(0x53, VT_BSTR, propVal);
}
long GetAddressID()
{
	long result;
	GetProperty(0x54, VT_I4, (void*)&result);
	return result;
}
void SetAddressID(long propVal)
{
	SetProperty(0x54, VT_I4, propVal);
}
CString GetParity()
{
	CString result;
	GetProperty(0x55, VT_BSTR, (void*)&result);
	return result;
}
void SetParity(CString propVal)
{
	SetProperty(0x55, VT_BSTR, propVal);
}
long GetBaudRate()
{
	long result;
	GetProperty(0x56, VT_I4, (void*)&result);
	return result;
}
void SetBaudRate(long propVal)
{
	SetProperty(0x56, VT_I4, propVal);
}
long GetMotionDetectionAlertDuration()
{
	long result;
	GetProperty(0x5b, VT_I4, (void*)&result);
	return result;
}
void SetMotionDetectionAlertDuration(long propVal)
{
	SetProperty(0x5b, VT_I4, propVal);
}
long GetPTZTiltSpeed()
{
	long result;
	GetProperty(0x5c, VT_I4, (void*)&result);
	return result;
}
void SetPTZTiltSpeed(long propVal)
{
	SetProperty(0x5c, VT_I4, propVal);
}
long GetPTZPanSpeed()
{
	long result;
	GetProperty(0x5d, VT_I4, (void*)&result);
	return result;
}
void SetPTZPanSpeed(long propVal)
{
	SetProperty(0x5d, VT_I4, propVal);
}
long GetRecordType()
{
	long result;
	GetProperty(0x69, VT_I4, (void*)&result);
	return result;
}
void SetRecordType(long propVal)
{
	SetProperty(0x69, VT_I4, propVal);
}
long GetNetworkStatus()
{
	long result;
	GetProperty(0x6c, VT_I4, (void*)&result);
	return result;
}
void SetNetworkStatus(long propVal)
{
	SetProperty(0x6c, VT_I4, propVal);
}
long GetPTZStatus()
{
	long result;
	GetProperty(0x6d, VT_I4, (void*)&result);
	return result;
}
void SetPTZStatus(long propVal)
{
	SetProperty(0x6d, VT_I4, propVal);
}
long GetContentStatus()
{
	long result;
	GetProperty(0x6e, VT_I4, (void*)&result);
	return result;
}
void SetContentStatus(long propVal)
{
	SetProperty(0x6e, VT_I4, propVal);
}
long GetCodecType()
{
	long result;
	GetProperty(0x6f, VT_I4, (void*)&result);
	return result;
}
void SetCodecType(long propVal)
{
	SetProperty(0x6f, VT_I4, propVal);
}
BOOL GetEnableBorder()
{
	BOOL result;
	GetProperty(0x71, VT_BOOL, (void*)&result);
	return result;
}
void SetEnableBorder(BOOL propVal)
{
	SetProperty(0x71, VT_BOOL, propVal);
}
CString GetVersion()
{
	CString result;
	GetProperty(0x72, VT_BSTR, (void*)&result);
	return result;
}
void SetVersion(CString propVal)
{
	SetProperty(0x72, VT_BSTR, propVal);
}
long GetPreRecordTime()
{
	long result;
	GetProperty(0x73, VT_I4, (void*)&result);
	return result;
}
void SetPreRecordTime(long propVal)
{
	SetProperty(0x73, VT_I4, propVal);
}
long GetAutoReconnectInterval()
{
	long result;
	GetProperty(0x74, VT_I4, (void*)&result);
	return result;
}
void SetAutoReconnectInterval(long propVal)
{
	SetProperty(0x74, VT_I4, propVal);
}
long GetBitRate()
{
	long result;
	GetProperty(0x75, VT_I4, (void*)&result);
	return result;
}
void SetBitRate(long propVal)
{
	SetProperty(0x75, VT_I4, propVal);
}
long GetFrameRateMode()
{
	long result;
	GetProperty(0x76, VT_I4, (void*)&result);
	return result;
}
void SetFrameRateMode(long propVal)
{
	SetProperty(0x76, VT_I4, propVal);
}
long GetFps()
{
	long result;
	GetProperty(0x77, VT_I4, (void*)&result);
	return result;
}
void SetFps(long propVal)
{
	SetProperty(0x77, VT_I4, propVal);
}
long GetPostRecordingTime()
{
	long result;
	GetProperty(0x7c, VT_I4, (void*)&result);
	return result;
}
void SetPostRecordingTime(long propVal)
{
	SetProperty(0x7c, VT_I4, propVal);
}
long GetDuration()
{
	long result;
	GetProperty(0x7f, VT_I4, (void*)&result);
	return result;
}
void SetDuration(long propVal)
{
	SetProperty(0x7f, VT_I4, propVal);
}
long GetVariableFPS()
{
	long result;
	GetProperty(0x86, VT_I4, (void*)&result);
	return result;
}
void SetVariableFPS(long propVal)
{
	SetProperty(0x86, VT_I4, propVal);
}
long GetDeviceChipType()
{
	long result;
	GetProperty(0x88, VT_I4, (void*)&result);
	return result;
}
void SetDeviceChipType(long propVal)
{
	SetProperty(0x88, VT_I4, propVal);
}
unsigned __int64 GetLastRecFileSize()
{
	unsigned __int64 result;
	GetProperty(0x92, VT_EMPTY, (void*)&result);
	return result;
}
void SetLastRecFileSize(unsigned __int64 propVal)
{
	SetProperty(0x92, VT_EMPTY, propVal);
}
long GetlongLastRecFileSize()
{
	long result;
	GetProperty(0x93, VT_I4, (void*)&result);
	return result;
}
void SetlongLastRecFileSize(long propVal)
{
	SetProperty(0x93, VT_I4, propVal);
}
CString GetstrLastRecFileSize()
{
	CString result;
	GetProperty(0x94, VT_BSTR, (void*)&result);
	return result;
}
void SetstrLastRecFileSize(CString propVal)
{
	SetProperty(0x94, VT_BSTR, propVal);
}
long GetRTSPPort()
{
	long result;
	GetProperty(0x98, VT_I4, (void*)&result);
	return result;
}
void SetRTSPPort(long propVal)
{
	SetProperty(0x98, VT_I4, propVal);
}
long GetRenderInterface()
{
	long result;
	GetProperty(0xa1, VT_I4, (void*)&result);
	return result;
}
void SetRenderInterface(long propVal)
{
	SetProperty(0xa1, VT_I4, propVal);
}
long GetReplaceTimeCodeByLocalTime()
{
	long result;
	GetProperty(0xa4, VT_I4, (void*)&result);
	return result;
}
void SetReplaceTimeCodeByLocalTime(long propVal)
{
	SetProperty(0xa4, VT_I4, propVal);
}
long GetConnectTimeout()
{
	long result;
	GetProperty(0xa5, VT_I4, (void*)&result);
	return result;
}
void SetConnectTimeout(long propVal)
{
	SetProperty(0xa5, VT_I4, propVal);
}
DeviceType GetDeviceType()
{
	DeviceType result;
	GetProperty(0xae, VT_I4, (void*)&result);
	return result;
}
void SetDeviceType(DeviceType propVal)
{
	SetProperty(0xae, VT_I4, propVal);
}
QuadDeviceMode GetQuadDeviceMode()
{
	QuadDeviceMode result;
	GetProperty(0xaf, VT_I4, (void*)&result);
	return result;
}
void SetQuadDeviceMode(QuadDeviceMode propVal)
{
	SetProperty(0xaf, VT_I4, propVal);
}
unsigned long GetTCPVideoStreamID()
{
	unsigned long result;
	GetProperty(0xb0, VT_UI4, (void*)&result);
	return result;
}
void SetTCPVideoStreamID(unsigned long propVal)
{
	SetProperty(0xb0, VT_UI4, propVal);
}
unsigned long GetRTPVideoTrackNumber()
{
	unsigned long result;
	GetProperty(0xb1, VT_UI4, (void*)&result);
	return result;
}
void SetRTPVideoTrackNumber(unsigned long propVal)
{
	SetProperty(0xb1, VT_UI4, propVal);
}
unsigned long GetRTPAudioTrackNumber()
{
	unsigned long result;
	GetProperty(0xb2, VT_UI4, (void*)&result);
	return result;
}
void SetRTPAudioTrackNumber(unsigned long propVal)
{
	SetProperty(0xb2, VT_UI4, propVal);
}
CString GetPlayFileName()
{
	CString result;
	GetProperty(0xb3, VT_BSTR, (void*)&result);
	return result;
}
void SetPlayFileName(CString propVal)
{
	SetProperty(0xb3, VT_BSTR, propVal);
}
unsigned long GetRealFPS()
{
	unsigned long result;
	GetProperty(0xc3, VT_UI4, (void*)&result);
	return result;
}
void SetRealFPS(unsigned long propVal)
{
	SetProperty(0xc3, VT_UI4, propVal);
}
long GetRecordLedStatus()
{
	long result;
	GetProperty(0xc4, VT_I4, (void*)&result);
	return result;
}
void SetRecordLedStatus(long propVal)
{
	SetProperty(0xc4, VT_I4, propVal);
}


};
