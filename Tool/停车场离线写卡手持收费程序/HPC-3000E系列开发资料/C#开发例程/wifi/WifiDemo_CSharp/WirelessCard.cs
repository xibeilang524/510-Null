using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using WZCSVC;
using System.Windows.Forms;

namespace WifiDemo_CSharp
{
    class WirelessCard
    {
        public char[] m_szWirelessCard1;
        public INTF_ENTRY_EX m_Intf;
        public uint m_dwOutFlags;
        public uint m_dwSSIDCounts;

        string[] GszPrivacyMode;
        public WirelessCard()
        {
            m_szWirelessCard1 = new char[NativeConstants.MAX_PATH];
            m_Intf.bInitialized = 0;
            m_dwSSIDCounts = 0;
            GszPrivacyMode = new string[]{
                "Ndis802_11WEPEnabled",
                "Ndis802_11WEPDisabled",
                "Ndis802_11WEPKeyAbsent",
                "Ndis802_11WEPNotSupported",
                "Ndis802_11Encryption2Enabled",
                "Ndis802_11Encryption2KeyAbsent",
                "Ndis802_11Encryption3Enabled",
                "Ndis802_11Encryption3KeyAbsent"
            };
        }
        ~WirelessCard()
        {
            if (0 != m_Intf.bInitialized)
            {
                NativeMethods.WZCDeleteIntfObjEx(ref m_Intf);
            }
        }
        public bool GetFirstWirelessNetworkCard(char[] szWirelessCard1, uint dwLen)
        {
            bool fStatus = true;
            INTFS_KEY_TABLE IntfsTable;
            IntfsTable.dwNumIntfs = 0;
            IntfsTable.pIntfs = IntPtr.Zero;
            try
            {
                uint dwStatus = NativeMethods.WZCEnumInterfaces(null, ref IntfsTable);
                if (NativeConstants.ERROR_SUCCESS != dwStatus)
                {
                    return false;
                }
                if (0 == IntfsTable.dwNumIntfs)
                {
                    return false;
                }
                INTF_KEY_ENTRY intfs = (INTF_KEY_ENTRY)Marshal.PtrToStructure(IntfsTable.pIntfs, typeof(INTF_KEY_ENTRY));

                if (null != szWirelessCard1 && NativeConstants.MAX_PATH <= dwLen)
                {

                    Array.Copy(intfs.wszGuid.ToCharArray(), szWirelessCard1, intfs.wszGuid.Length);
                    szWirelessCard1[intfs.wszGuid.Length] = '\0';
                }
                Array.Copy(intfs.wszGuid.ToCharArray(), m_szWirelessCard1, intfs.wszGuid.Length);
                m_szWirelessCard1[intfs.wszGuid.Length] = '\0';
//                 string s = Marshal.PtrToStringUni(intfs.wszGuid);
//                 if (null != szWirelessCard1 && NativeConstants.MAX_PATH <= dwLen)
//                 {
//                     Array.Copy(s.ToCharArray(), szWirelessCard1, s.Length);
//                     szWirelessCard1[s.Length] = '\0';
//                 }
//                 Array.Copy(s.ToCharArray(), m_szWirelessCard1, s.Length);
//                 m_szWirelessCard1[s.Length] = '\0';
                NativeMethods.LocalFree(IntfsTable.pIntfs);
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }

        public bool Query()
        {
            bool fStatus = true;
            try
            {
                if (0 != m_Intf.bInitialized)
                {
                    NativeMethods.WZCDeleteIntfObjEx(ref m_Intf);
                }
                if (false == GetFirstWirelessNetworkCard(null, 0))
                {
                    return false;
                }
                m_Intf.bInitialized = 0;
                m_Intf.wszGuid = new string(m_szWirelessCard1);
                uint dwStatus = NativeMethods.WZCQueryInterfaceEx(null, NativeConstants.INTF_ALL, ref m_Intf, ref m_dwOutFlags);
                if (NativeConstants.ERROR_SUCCESS != dwStatus)
                {
                    return false;
                }
                m_dwSSIDCounts = GetSSIDCounts();
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public uint GetSSIDCounts()
        {
            uint dwCounts = 0;
            try
            {
                do 
                {
                    if(0 == m_Intf.bInitialized)
                        break;
//                     WZC_802_11_CONFIG_LIST wzcConfigList = (WZC_802_11_CONFIG_LIST)Marshal.PtrToStructure(m_Intf.rdBSSIDList.pData, typeof(WZC_802_11_CONFIG_LIST));
//                     dwCounts = wzcConfigList.NumberOfItems;
                    dwCounts = (uint)Marshal.ReadInt32(m_Intf.rdBSSIDList.pData);
                } while (false);
            }
            catch
            {
            	
            }
            return dwCounts;
        }
        public bool IsAssociated(char[] pszSSID, uint dwLen)
        {
            bool fStatus = true;
            if (0 == m_Intf.bInitialized)
                return false;
            if (0 == (m_dwOutFlags & NativeConstants.INTF_BSSID))
                return false;
            IntPtr pAdapterInfo = IntPtr.Zero;
            try
            {
                pAdapterInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IP_ADAPTER_INFO)) * 10);
                //IP_ADAPTER_INFO[] adapterInfo = new IP_ADAPTER_INFO[10];
                uint uLen = (uint)Marshal.SizeOf(typeof(IP_ADAPTER_INFO))*10;
                uint ret = NativeMethods.GetAdaptersInfo(pAdapterInfo, ref uLen);
                for (int i = 0; i < uLen / Marshal.SizeOf(typeof(IP_ADAPTER_INFO)); i++)
                {
                    IP_ADAPTER_INFO adapterInfo = (IP_ADAPTER_INFO)Marshal.PtrToStructure(new IntPtr(pAdapterInfo.ToInt32()+i*Marshal.SizeOf(typeof(IP_ADAPTER_INFO))),typeof(IP_ADAPTER_INFO));
                    bool isThisAdapter = true;
                    for (int j = 0; adapterInfo.AdapterName[j] != 0 && m_szWirelessCard1[j] != '\0'; j++)
                    {
                        if ((char)adapterInfo.AdapterName[j] != m_szWirelessCard1[j])
                        {
                            isThisAdapter = false;
                            break;
                        }
                    }
                    if (isThisAdapter)
                    {
                        IP_ADDR_STRING CurrentIPAddress = (IP_ADDR_STRING)Marshal.PtrToStructure(new IntPtr(adapterInfo.CurrentIpAddress.ToInt32()), typeof(IP_ADDR_STRING));
                        char[] zero = "0.0.0.0".ToCharArray();
                        bool iszero = true;
                        for(int j=0;j<zero.Length;j++)
                        {
                            if ((char)CurrentIPAddress.IpAddress.Data4[j] != zero[j])
                            {
                                iszero = false;
                                break;
                            }
                        }
                        if (iszero)
                            return false;
                        else
                            break;
                    }
                }

                if (0 == m_Intf.rdSSID.dwDataLen)
                    return false;
                if (dwLen < NativeConstants.NDIS_802_11_LENGTH_SSID + 1)
                    return false;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] ssidData = new byte[m_Intf.rdSSID.dwDataLen + 1];
                for(int i=0;i<m_Intf.rdSSID.dwDataLen;i++)
                    pszSSID[i] = (char)(ssidData[i] = (byte)Marshal.PtrToStructure(new IntPtr(m_Intf.rdSSID.pData.ToInt32() + i), typeof(byte)));
                pszSSID[m_Intf.rdSSID.dwDataLen] = '\0';
            }
            catch
            {
                MessageBox.Show("IsAssociated exception");
                fStatus = false;
            }
            finally
            {
                if (pAdapterInfo != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pAdapterInfo);
                }
            }
            return fStatus;
        }
        public bool GetWzcContext(ref WZC_CONTEXT context)
        {
            bool fStatus = true;
            try
            {
                uint dwFlags = 0;
                uint dwStatus = NativeMethods.WZCQueryContext(null, 0, ref context, ref dwFlags);
                if (NativeConstants.ERROR_SUCCESS != dwStatus)
                    return false;
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool SetWzcContext(ref WZC_CONTEXT context)
        {
            bool fStatus = true;
            try
            {
                uint dwFlags = 0;
                uint dwStatus = NativeMethods.WZCSetContext(null, 0, ref context, ref dwFlags);
                if (NativeConstants.ERROR_SUCCESS != dwStatus)
                    return false;
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool GetWlanConfig(uint dwIndex, ref WZC_WLAN_CONFIG wlanConfig)
        {
            if (0 == m_Intf.bInitialized || dwIndex >= m_dwSSIDCounts)
                return false;
            bool fStatus = true;
            try
            {
                wlanConfig = (WZC_WLAN_CONFIG)Marshal.PtrToStructure(new IntPtr(m_Intf.rdBSSIDList.pData.ToInt32() + 8 + dwIndex * Marshal.SizeOf(wlanConfig)), typeof(WZC_WLAN_CONFIG));
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool GetWlanSSID(ref WZC_WLAN_CONFIG wlanConfig, ref string ssid)
        {
            bool fStatus = true;
            try
            {
                if (0 == wlanConfig.Ssid.SsidLength)
                {
                    ssid = "<隐形的AP>";
                }
                else
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    ssid = encoding.GetString(wlanConfig.Ssid.Ssid,0,(int)wlanConfig.Ssid.SsidLength);
                }
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool GetWlanMacAddress(ref WZC_WLAN_CONFIG wlanConfig,ref string szMac)
        {
            bool fStatus = true;
            try
            {
                szMac = String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                    wlanConfig.MacAddress[0],
                    wlanConfig.MacAddress[1],
                    wlanConfig.MacAddress[2],
                    wlanConfig.MacAddress[3],
                    wlanConfig.MacAddress[4],
                    wlanConfig.MacAddress[5]);
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool GetWlanRssi(ref WZC_WLAN_CONFIG wlanConfig, ref int rssi)
        {
            rssi = wlanConfig.Rssi;
            return true;
        }
        public bool GetWlanPrivacyMode(ref WZC_WLAN_CONFIG wlanConfig,ref string szPrivacyMode)
        {
            bool fStatus = true;
            try
            {
                uint mode = wlanConfig.Privacy;
                if (mode >= 8)
                {
                    szPrivacyMode = "<No Supported>";
                    return false;
                }
                szPrivacyMode = GszPrivacyMode[mode];
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool AddToPreferredNetworkList(ref WZC_WLAN_CONFIG wlanConfig)
        {
            if (0 == m_Intf.bInitialized)
                return false;
            bool fStatus = true;
            try
            {
                string szSSID = String.Empty;
                GetWlanSSID(ref wlanConfig,ref szSSID);
                if (IntPtr.Zero == m_Intf.rdStSSIDList.pData)
                {
                    uint dwDataLen = (uint)Marshal.SizeOf(typeof(WZC_WLAN_CONFIG))+8;
                    IntPtr newConfigList = NativeMethods.LocalAlloc(0x40/*LPTR*/, dwDataLen);
                    Marshal.WriteInt32(newConfigList, 1);
                    Marshal.WriteInt32(newConfigList, 4, 0);
                    Marshal.StructureToPtr(wlanConfig, new IntPtr(newConfigList.ToInt32() + 8), false);
                    m_Intf.rdStSSIDList.pData = newConfigList;
                    m_Intf.rdStSSIDList.dwDataLen = dwDataLen;
                }
                else
                {
                    IntPtr ptr = m_Intf.rdStSSIDList.pData;
                    uint uiNumberOfItems = (uint)Marshal.ReadInt32(ptr);
                    int size = Marshal.SizeOf(typeof(WZC_WLAN_CONFIG));
                    bool same = true;
                    for (uint i = 0; i < uiNumberOfItems; i++)
                    {
                        WZC_WLAN_CONFIG config = (WZC_WLAN_CONFIG)Marshal.PtrToStructure(new IntPtr(ptr.ToInt32() + 8 + i * size), typeof(WZC_WLAN_CONFIG));
                        if (wlanConfig.Ssid.SsidLength != config.Ssid.SsidLength)
                            continue;
                        for (int j = 0; j < wlanConfig.Ssid.SsidLength; j++)
                        {
                            if (wlanConfig.Ssid.Ssid[j] != config.Ssid.Ssid[j])
                            {
                                same = false;
                                break;
                            }
                        }
                        if (same)
                        {
                            Marshal.StructureToPtr(wlanConfig, new IntPtr(ptr.ToInt32() + 8 + i * size), false);

                            //清空首选列表，然后再添加，不然直接对密钥的修改不起作用
                            INTF_ENTRY_EX tmp = m_Intf;
                            tmp.rdStSSIDList.pData = IntPtr.Zero;
                            tmp.rdStSSIDList.dwDataLen = 0;
                            uint ret = NativeMethods.WZCSetInterfaceEx(null, 0x40000/*INTF_PREFLIST*/, ref tmp, ref m_dwOutFlags);
                            break;
                        }
                    }
                    if (!same)
                    {
                        uint dwDataLen = (uint)((uiNumberOfItems + 1) *size + 8);
                        IntPtr pNewConfigList = NativeMethods.LocalAlloc(0x40/*LPTR*/, dwDataLen);
                        Marshal.WriteInt32(pNewConfigList, (int)(uiNumberOfItems + 1));
                        Marshal.WriteInt32(pNewConfigList, 4, 0);
                        Marshal.StructureToPtr(wlanConfig, new IntPtr(pNewConfigList.ToInt32() + 8), false);
                        if (0 != uiNumberOfItems)
                        {
                            byte[] tmp = new byte[uiNumberOfItems * size];
                            Marshal.Copy(new IntPtr(ptr.ToInt32() + 8), tmp, 0, (int)(uiNumberOfItems * size));
                            Marshal.Copy(tmp, 0, new IntPtr(pNewConfigList.ToInt32() + 8 + size), (int)(uiNumberOfItems * size));
                        }
                        NativeMethods.LocalFree(ptr);
                        m_Intf.rdStSSIDList.pData = pNewConfigList;
                        m_Intf.rdStSSIDList.dwDataLen = dwDataLen;
                    }
                }
            }
            catch
            {
                fStatus = false;
            }
            uint dwStatus = NativeMethods.WZCSetInterfaceEx(null, 0x40000/*INTF_PREFLIST*/, ref m_Intf, ref m_dwOutFlags);
            fStatus = dwStatus == NativeConstants.ERROR_SUCCESS;
            return fStatus;
        }
        public bool RemoveAllPreferredNetworkList()
        {
            if (0 == m_Intf.bInitialized)
            {
                return false;
            }
            bool fStatus = true;
            try
            {
                IntPtr ptr = m_Intf.rdStSSIDList.pData;
                if (IntPtr.Zero != ptr)
                {
                    NativeMethods.LocalFree(ptr);
                }
                else
                {
                    return true;
                }
                m_Intf.rdStSSIDList.pData = IntPtr.Zero;
                m_Intf.rdStSSIDList.dwDataLen = 0;
                uint dwStatus = NativeMethods.WZCSetInterfaceEx(null, 0x40000/*INTF_PREFLIST*/, ref m_Intf, ref m_dwOutFlags);
                if (NativeConstants.ERROR_SUCCESS != dwStatus)
                    return false;
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        public bool InterpretEncryptionKeyValue(ref WZC_WLAN_CONFIG wlanConfig, char[] szEncryptionKey, uint ulKeyIndex, bool bNeed8021X)
        {
            if (null == szEncryptionKey)
                return false;
            bool fStatus = true;
            try
            {
                switch ((NDIS_802_11_WEP_STATUS)wlanConfig.Privacy)
                {
                    case NDIS_802_11_WEP_STATUS.Ndis802_11WEPEnabled:
                        if (!bNeed8021X)
                        {
                            wlanConfig.KeyIndex = ulKeyIndex;
                            wlanConfig.KeyLength = (uint)(new string(szEncryptionKey).Length);
                            if (5 == wlanConfig.KeyLength || 13 == wlanConfig.KeyLength)
                            {
                                for (int i = 0; i < wlanConfig.KeyLength; i++)
                                {
                                    wlanConfig.KeyMaterial[i] = (byte)(szEncryptionKey[i]);
                                }
                            }
                            else
                            {
                                return false;
                            }
                            EncryptWepKMaterial(ref wlanConfig);
                            wlanConfig.dwCtlFlags |= NativeConstants.WZCCTL_WEPK_PRESENT;
                        }
                        break;
                    case NDIS_802_11_WEP_STATUS.Ndis802_11Encryption2Enabled:
                    case NDIS_802_11_WEP_STATUS.Ndis802_11Encryption3Enabled:
                        if (!bNeed8021X)
                        {
                            wlanConfig.KeyLength = (uint) szEncryptionKey.Length;
                            if (wlanConfig.KeyLength < 8 || wlanConfig.KeyLength > 63)
                            {
                                return false;
                            }
                            byte[] szEncryptionKeyValue8 = Encoding.ASCII.GetBytes(szEncryptionKey);
                            NativeMethods.WZCPassword2Key(ref wlanConfig, szEncryptionKeyValue8);
                            EncryptWepKMaterial(ref wlanConfig);
                            wlanConfig.dwCtlFlags |= NativeConstants.WZCCTL_WEPK_XFORMAT
                                | NativeConstants.WZCCTL_WEPK_PRESENT
                                | NativeConstants.WZCCTL_ONEX_ENABLED;
                        }
                        wlanConfig.EapolParams.dwEapFlags = unchecked((uint)NativeConstants.EAPOL_ENABLED);
                        wlanConfig.EapolParams.dwEapType = NativeConstants.DEFAULT_EAP_TYPE;
                        wlanConfig.EapolParams.bEnable8021x = true;
                        wlanConfig.WPAMCastCipher = unchecked((uint)NDIS_802_11_WEP_STATUS.Ndis802_11Encryption2Enabled);
                        break;
                }
            }
            catch
            {
                fStatus = false;
            }
            return fStatus;
        }
        void EncryptWepKMaterial(ref WZC_WLAN_CONFIG wzcConfig)
        {
            byte[] chFakeKeyMaterial = { 0x56, 0x09, 0x08, 0x98, 0x4D, 0x08, 0x11, 0x66, 0x42, 0x03, 0x01, 0x67, 0x66 };
            for (int i = 0; i < NativeConstants.WZCCTL_MAX_WEPK_MATERIAL; i++)
            {
                wzcConfig.KeyMaterial[i] ^= chFakeKeyMaterial[(7*i)%13];
            }
        }
    }
}
