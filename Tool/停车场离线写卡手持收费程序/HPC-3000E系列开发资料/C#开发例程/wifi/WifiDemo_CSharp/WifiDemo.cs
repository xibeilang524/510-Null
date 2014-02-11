using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WZCSVC;

namespace WifiDemo_CSharp
{
    public partial class WifiDemo : Form
    {
        WirelessCard m_WirelessCard;
        Timer timer = new Timer();
        public WifiDemo()
        {
            InitializeComponent();
            m_WirelessCard = new WirelessCard();
            if (m_WirelessCard.GetFirstWirelessNetworkCard(null, 0))
            {
                txtNICName.Text = new string(m_WirelessCard.m_szWirelessCard1);
            }
            DataTable dt = new DataTable("SSID");
            DataColumn cname = new DataColumn("网络名称");
            dt.Columns.Add(cname);
            dgWirelessAP.DataSource = dt;
            DataGridTableStyle dgts = new DataGridTableStyle();
            dgts.MappingName = dt.TableName;
            dgWirelessAP.TableStyles.Clear();
            dgWirelessAP.TableStyles.Add(dgts);
            dgts.GridColumnStyles[0].Width = dgWirelessAP.Width;

            timer.Interval = 3000;
            timer.Tick += new EventHandler(onTick);
            timer.Enabled = true;
        }


        private void onTick(object sender, EventArgs e)
        {
            if (false == RefreshNIC())
                Application.Exit();
        }

        private void bnRefresh_Click(object sender, EventArgs e)
        {
            bnRefresh.Enabled = false;
            if (false == RefreshNIC())
            {
                Application.Exit();
            }
            bnRefresh.Enabled = true;
        }
        private bool RefreshNIC()
        {
            char[] szWirelessCardGuid = new char[NativeConstants.MAX_PATH];
            if (false == m_WirelessCard.GetFirstWirelessNetworkCard(szWirelessCardGuid, NativeConstants.MAX_PATH))
            {
                return false;
            }
            txtNICName.Text = new string(szWirelessCardGuid);
            if (false == m_WirelessCard.Query())
            {
                return false;
            }
            txtAPCnt.Text = String.Format("{0:d}", m_WirelessCard.GetSSIDCounts());
            char[] szAssociatedSSID = new char[33];
            if (true == m_WirelessCard.IsAssociated(szAssociatedSSID, 33))
            {
                txtAssociatedSSID.Text = new string(szAssociatedSSID);
            }
            else
            {
                txtAssociatedSSID.Text = "未连接";
            }
            UpdateSSIDList();
            return true;
        }
        void UpdateSSIDList()
        {
            int iSel = dgWirelessAP.CurrentRowIndex;
            DataTable dt = (DataTable)dgWirelessAP.DataSource;
            dt.Clear();
            uint dwCounts= m_WirelessCard.GetSSIDCounts();
            WZC_WLAN_CONFIG cfg = new WZC_WLAN_CONFIG();
            string ssid = String.Empty;
            for(uint i=0;i<dwCounts;i++)
            {
                m_WirelessCard.GetWlanConfig(i,ref cfg);
                m_WirelessCard.GetWlanSSID(ref cfg,ref ssid);
                DataRow row = dt.NewRow();
                row[0] = ssid;
                dt.Rows.Add(row);
            }
            if (iSel != -1 && iSel < dt.Rows.Count)
            {
                dgWirelessAP.CurrentRowIndex = iSel;
            }
        }

        private void bnConfig_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            WZC_CONTEXT wzcContex = new WZC_CONTEXT();
            if (false == m_WirelessCard.GetWzcContext(ref wzcContex))
            {
                MessageBox.Show("获取WZC配置失败", "错误");
                timer.Enabled = true;
                return;
            }
            WZCConfig form = new WZCConfig();
            form.txtRefreshInterval.Text = wzcContex.tmTr.ToString();
            form.txtAssociateTimeout.Text = wzcContex.tmTp.ToString();
            form.txtIntervalOnConn.Text = wzcContex.tmTc.ToString();
            form.txtIntervalOnDisconn.Text = wzcContex.tmTf.ToString();
            form.Owner = this;
            form.Left = (this.Width - form.Width) / 2;
            form.Top = (this.Height - form.Height) / 2;
            if (DialogResult.OK == form.ShowDialog())
            {
                wzcContex.tmTr = uint.Parse(form.txtRefreshInterval.Text);
                wzcContex.tmTp = uint.Parse(form.txtAssociateTimeout.Text);
                wzcContex.tmTc = uint.Parse(form.txtIntervalOnConn.Text);
                wzcContex.tmTf = uint.Parse(form.txtIntervalOnDisconn.Text);
                if (false == m_WirelessCard.SetWzcContext(ref wzcContex))
                {
                    MessageBox.Show("WZC配置失败", "错误");
                }
            }
            timer.Enabled = true;
        }

        private void bnDisconnect_Click(object sender, EventArgs e)
        {
            m_WirelessCard.RemoveAllPreferredNetworkList();
            RefreshNIC();
        }

        private void dgWirelessAP_DoubleClick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            int iSel = dgWirelessAP.CurrentRowIndex;
            if (-1 == iSel)
            {
                timer.Enabled = true;
                return;
            }
            NetInfo dlg = new NetInfo();
            WZC_WLAN_CONFIG cfg = new WZC_WLAN_CONFIG();
            if(false == m_WirelessCard.GetWlanConfig((uint)iSel,ref cfg))
            {
                return;
            }
            string szSSID = String.Empty;
            string szMAC = String.Empty;
            string szPrivacyMode = String.Empty;
            int rssi = 0;
            m_WirelessCard.GetWlanSSID(ref cfg, ref szSSID);
            m_WirelessCard.GetWlanMacAddress(ref cfg,ref szMAC);
            m_WirelessCard.GetWlanRssi(ref cfg, ref rssi);
            m_WirelessCard.GetWlanPrivacyMode(ref cfg,ref szPrivacyMode);
            dlg.txtSSID.Text = szSSID;
            dlg.txtMAC.Text = szMAC;
            dlg.txtRSSI.Text = String.Format("{0:D}", rssi);
            dlg.txtPrivacy.Text = szPrivacyMode;
//             if (NDIS_802_11_WEP_STATUS.Ndis802_11WEPEnabled != (NDIS_802_11_WEP_STATUS)cfg.Privacy)
//             {
//                 dlg.txtKey.Text = "本程序不支持的加密方式";
//             }
            dlg.Owner = this;
            dlg.Left = (this.Width - dlg.Width) / 2;
            dlg.Top = (this.Height - dlg.Height) / 2;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                char[] key = dlg.txtKey.Text.ToCharArray();
                int keyLength = key.Length;
                if(m_WirelessCard.InterpretEncryptionKeyValue(ref cfg, key, 0,false)
                    && m_WirelessCard.AddToPreferredNetworkList(ref cfg))
                {
                    
                }
                else
                {
                    MessageBox.Show("错误密钥","错误");
                }
            }
            timer.Enabled = true;
        }
    }
}