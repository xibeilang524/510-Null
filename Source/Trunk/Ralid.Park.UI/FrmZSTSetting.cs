using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary .CardReader ;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park .BusinessModel .Report ;
using Ralid.Park .BusinessModel .Interface ;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.ParkAdapter;

namespace Ralid.Park.UI
{
    public partial class FrmZSTSetting : Form, IReportHandler
    {
        #region 静态方法
        private static FrmZSTSetting _Instance;

        public static FrmZSTSetting GetInstance()
        {
            if (_Instance == null) _Instance = new FrmZSTSetting();
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmZSTSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private ZSTReader _Reader = new ZSTReader();
        ZSTSetting _ZSTSetting;

        private List<ZSTHandlerInfo> _HandlerInfoes = new List<ZSTHandlerInfo>();
        private object _HandlerLocker = new object();
        #endregion

        #region 私有方法
        private bool SaveZSTCard(string cardID, CardType cardType, decimal balance)
        {
            CardInfo card = new CardInfo()
            {
                CardID = cardID,
                CardType = cardType,
                CarType = CarTypeSetting.DefaultCarType,
                CardNum = 1000,
                OwnerName = "中山通",
                Options = CardOptions.ForbidRepeatIn | CardOptions.ForbidRepeatOut | CardOptions.HolidayEnable | CardOptions.WithCount,
                Status = CardStatus.Enabled,
                ParkingStatus = ParkingStatus.Out,
                LastDateTime = DateTime.Now,
                LastEntrance = 0,
                ActivationDate = DateTime.Now,
                ValidDate = new DateTime(2099, 12, 31),
                Balance = 0,
            };
            return (new CardBll(AppSettings.CurrentSetting.ParkConnect)).AddCard(card).Result == ResultCode.Successful;
        }

        private void ShowItemOnRow(DataGridViewRow row, string ip, string entranceName, int entranceID,string memo)
        {
            row.Cells["colReaderIP"].Value = ip;
            row.Cells["colEntrance"].Value = entranceName;
            row.Cells["colEntrance"].Tag = entranceID;
            row.Cells["colMemo"].Value = memo;
        }

        /// <summary>
        /// 获取某读卡器IP在网格中所在行的行号,如果没有找到,返回-1
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private int FindRow(string ip)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["colReaderIP"].Value.ToString() == ip)
                {
                    return row.Index;
                }
            }
            return -1;
        }

        private ZSTHandlerInfo GetZSTHandlerInstance(string ip)
        {
            lock (_HandlerLocker)
            {
                ZSTHandlerInfo item = _HandlerInfoes.SingleOrDefault(it => it.ReaderIP == ip);
                if (item == null)
                {
                    item = new ZSTHandlerInfo();
                    item.ReaderIP = ip;
                    _HandlerInfoes.Add(item);
                }
                return item;
            }
        }

        private ZSTHandlerInfo GetZSTHandlerInstance(int entranceID)
        {
            if (_ZSTSetting != null && _ZSTSetting.Items != null && _ZSTSetting.Items.Count > 0)
            {
                foreach (ZSTItem item in _ZSTSetting.Items)
                {
                    if (item.EntranceID == entranceID)
                    {
                        lock (_HandlerLocker)
                        {
                            return _HandlerInfoes.FirstOrDefault(it => it.ReaderIP == item.ReaderIP);
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        #region 实现 IReportHandler 接口
        public void ProcessReport(BusinessModel.Report.ReportBase report)
        {
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report.EntranceID);
            if (entrance == null) return;
            if (entrance.IsExitDevice)  //处理出口
            {
                if (report is CardEventReport)
                {
                    CardEventReport cer = report as CardEventReport;
                    if (cer.EventStatus == CardEventStatus.Pending)
                    {
                        ZSTHandlerInfo zst = GetZSTHandlerInstance(cer.EntranceID);
                        if (zst != null && !string.IsNullOrEmpty(zst.CardID) && zst.CardID == cer.CardID)
                        {
                            if (zst.Balance >= cer.CardPaymentInfo.Accounts)
                            {
                                zst.ProcessingEvent = cer;
                                _Reader.Consumption(zst.ReaderIP, cer.CardPaymentInfo.Accounts);  //余额足够的话就直接扣款
                                IParkingAdapter pad = ParkingAdapterManager.Instance[cer.ParkID];
                                if (pad != null)
                                {
                                    SetLedDisplayNotify notify = new SetLedDisplayNotify(entrance.ParkID, CanAddress.TicketBoxLed,
                                        string.Format("扣款{0}元", cer.CardPaymentInfo.Accounts), false, 0);
                                    pad.LedDisplay(notify);
                                }
                            }
                            else
                            {
                                _Reader.MessageConfirm(zst.ReaderIP); //发送中山通读卡确认消息
                                zst.ClearCardInfo();  //清空读卡器内的卡片信息
                                IParkingAdapter pad = ParkingAdapterManager.Instance[cer.ParkID];
                                if (pad != null)
                                {
                                    EventInvalidNotify n = new EventInvalidNotify()
                                    {
                                        InvalidType = EventInvalidType.INV_Balance,
                                        Balance = zst.Balance,
                                        CardEvent = cer
                                    };
                                    pad.EventInvalid(n);
                                }
                            }
                        }
                    }
                    else
                    {
                        ZSTHandlerInfo zst = GetZSTHandlerInstance(cer.EntranceID);
                        if (zst != null && !string.IsNullOrEmpty(zst.CardID) && zst.CardID == cer.CardID)
                        {
                            _Reader.MessageConfirm(zst.ReaderIP); //发送中山通读卡确认消息
                            zst.ClearCardInfo();  //清空读卡器内的卡片信息
                        }
                    }
                }
                else if (report is CardInvalidEventReport)
                {
                    CardInvalidEventReport cier = report as CardInvalidEventReport;
                    ZSTHandlerInfo zst = GetZSTHandlerInstance(report.EntranceID);
                    if (zst != null && !string.IsNullOrEmpty(zst.CardID) && zst.CardID == cier.CardID)
                    {
                        _Reader.MessageConfirm(zst.ReaderIP); //发送中山通读卡确认消息
                        zst.ClearCardInfo();  //清空读卡器内的卡片信息
                    }
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取中山通读卡器
        /// </summary>
        public ZSTReader ZSTReader
        {
            get
            {
                return _Reader;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _Reader.MessageRecieved += reader_MessageRecieved;
            _Reader.Init();
            _ZSTSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<ZSTSetting>();
        }
        #endregion

        #region 事件处理程序
        private void FrmZSTSetting_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (_ZSTSetting != null && _ZSTSetting.Items != null && _ZSTSetting.Items.Count > 0)
            {
                foreach (ZSTItem item in _ZSTSetting.Items)
                {
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(item.EntranceID);
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], item.ReaderIP, entrance != null ? entrance.EntranceName : string.Empty, entrance != null ? entrance.EntranceID : 0, item.Memo);
                }
            }
        }

        private void reader_MessageRecieved(object sender, ZSTReaderEventArgs e)
        {
            if (e.MessageType == "1")
            {
                reader_CardReadHandler(sender, e);
            }
            else if (e.MessageType == "2")//扣款成功
            {
                reader_PaymentOk(sender, e);
            }
            else if (e.MessageType == "3") //扣款失败
            {
                reader_PaymentFail(sender, e);
            }
        }

        private void reader_PaymentFail(object sender, ZSTReaderEventArgs e)
        {
            ZSTHandlerInfo zst = _HandlerInfoes.SingleOrDefault(item => item.ReaderIP == e.ReaderIP);
            if (zst != null && zst.ProcessingEvent != null)
            {
                _Reader.MessageConfirm(zst.ReaderIP); //发送中山通读卡确认消息
                IParkingAdapter pad = ParkingAdapterManager.Instance[zst.ProcessingEvent.ParkID];
                if (pad != null)
                {
                    EventInvalidNotify n = new EventInvalidNotify()
                    {
                        InvalidType = EventInvalidType.INV_ReadCard,
                        Balance = zst.Balance,
                        CardEvent = zst.ProcessingEvent
                    };
                    pad.EventInvalid(n);
                }

                zst.ClearCardInfo();  //清空读卡器内的卡片信息
            }
        }

        private void reader_PaymentOk(object sender, ZSTReaderEventArgs e)
        {
            ZSTHandlerInfo zst = _HandlerInfoes.SingleOrDefault(item => item.ReaderIP == e.ReaderIP);
            if (zst != null && zst.ProcessingEvent != null)
            {
                if (zst.ProcessingEvent.CardPaymentInfo != null)
                {
                    zst.ProcessingEvent.CardPaymentInfo.Paid = zst.ProcessingEvent.CardPaymentInfo.Accounts;
                    zst.ProcessingEvent.CardPaymentInfo.Discount = zst.ProcessingEvent.CardPaymentInfo.Accounts - zst.ProcessingEvent.CardPaymentInfo.Paid;
                    zst.ProcessingEvent.CardPaymentInfo.PaymentMode = PaymentMode.ZhongShanTong;
                    zst.ProcessingEvent.CardPaymentInfo.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    zst.ProcessingEvent.CardPaymentInfo.StationID = WorkStationInfo.CurrentStation.StationName;
                    zst.ProcessingEvent.CardPaymentInfo.IsCenterCharge = false;
                    CommandResult ret = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).PayParkFee(zst.ProcessingEvent.CardPaymentInfo);
                }

                IParkingAdapter pad = ParkingAdapterManager.Instance[zst.ProcessingEvent.ParkID];
                if (pad != null)
                {
                    EventValidNotify n = new EventValidNotify(zst.ProcessingEvent.EntranceID,
                                                             OperatorInfo.CurrentOperator,
                                                             WorkStationInfo.CurrentStation.StationName,
                                                             0);

                    pad.EventValid(n);
                }

                zst.ClearCardInfo();  //清空读卡器内的卡片信息
            }
        }

        private void reader_CardReadHandler(object sender, ZSTReaderEventArgs e)
        {
            if (_ZSTSetting == null || _ZSTSetting.Items == null || !_ZSTSetting.HasReader(e.ReaderIP))
            {
                _Reader.MessageConfirm(e.ReaderIP);
                return;
            }
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(_ZSTSetting.GetReader(e.ReaderIP).EntranceID);
            if (entrance == null)
            {
                _Reader.MessageConfirm(e.ReaderIP);
                return;
            }

            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
            if (!entrance.IsExitDevice)
            {
                _Reader.MessageConfirm(e.ReaderIP);
                if (card == null) //入口,如果卡片不在系统中，则先在系统中增加此中山通卡片
                {
                    //寻找系统中是否存在名称为"中山通"的自定义卡片类型
                    CardType ct = null;
                    if (CustomCardTypeSetting.Current != null) ct = CustomCardTypeSetting.Current.GetCardType("中山通");
                    if (ct == null) return;
                    if (!SaveZSTCard(e.CardID, ct, e.Balance)) return;
                }
            }

            ZSTHandlerInfo zst = GetZSTHandlerInstance(e.ReaderIP);
            zst.CardID = e.CardID;
            zst.Balance = e.Balance;

            //通过远程读卡方式
            IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
            if (pad != null)
            {
                pad.RemoteReadCard(new RemoteReadCardNotify(entrance.RootParkID, entrance.EntranceID, e.CardID));
            }
        }

        private void FrmZSTSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> ips = _Reader.SearchReaders();
                if (ips != null && ips.Count > 0)
                {
                    foreach (string ip in ips)
                    {
                        if (FindRow(ip) == -1)
                        {
                            int row = dataGridView1.Rows.Add();
                            ShowItemOnRow(dataGridView1.Rows[row], ip, string.Empty, 0,string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmZSTDetail frm = new FrmZSTDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (FindRow(frm.ReaderIP) >= 0)
                {
                    MessageBox.Show("IP 地址为 " + frm.ReaderIP + " 的读卡器已经存在");
                }
                else
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], frm.ReaderIP, frm.EntranceName, frm.EntranceID,frm.Memo );
                }
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmZSTDetail frm = new FrmZSTDetail();
                frm.ReaderIP = dataGridView1.SelectedRows[0].Cells["colReaderIP"].Value as string;
                frm.EntranceID = (int)dataGridView1.SelectedRows[0].Cells["colEntrance"].Tag;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (FindRow(frm.ReaderIP) != dataGridView1.SelectedRows[0].Index)
                    {
                        MessageBox.Show("IP 地址为 " + frm.ReaderIP + " 的读卡器已经存在");
                    }
                    else
                    {
                        ShowItemOnRow(dataGridView1.SelectedRows[0], frm.ReaderIP, frm.EntranceName, frm.EntranceID, frm.Memo);
                    }
                }
            }
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Resources.Resource1.FrmMasterBase_DeleteQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ZSTSetting setting = new ZSTSetting();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["colEntrance"].Value.ToString()))
                {
                    ZSTItem item = new ZSTItem()
                    {
                        ReaderIP =(string)row.Cells["colReaderIP"].Value,
                        EntranceID =(int)row.Cells["colEntrance"].Tag,
                        Memo =(string)row.Cells ["colMemo"].Value
                    };
                    setting.Items.Add(item);
                }
                else
                {
                    ZSTItem item = new ZSTItem()
                    {
                        ReaderIP = (string)row.Cells["colReaderIP"].Value,
                        EntranceID = 0,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    setting.Items.Add(item);
                }
            }
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).SaveSetting<ZSTSetting>(setting);
            if (ret.Result == ResultCode.Successful)
            {
                _ZSTSetting = setting;
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show(ret.Message);
            }
        }
        #endregion

        private void mnu_ParaSetting_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmZSTParameter frm = new FrmZSTParameter();
                frm.ReaderIP = dataGridView1.SelectedRows[0].Cells["colReaderIP"].Value as string;
                frm.Reader = _Reader;
                frm.ShowDialog();
            }
        }
    }

    /// <summary>
    /// 一个内部类,用于存储中山通处理时的信息和状态
    /// </summary>
    internal class ZSTHandlerInfo
    {
        public string ReaderIP { get; set; }

        public string CardID { get; set; }

        public decimal Balance { get; set; }

        public CardEventReport ProcessingEvent { get; set; }

        public void ClearCardInfo()
        {
            CardID = string.Empty;
            Balance = 0;
            ProcessingEvent = null;
        }
    }
}
