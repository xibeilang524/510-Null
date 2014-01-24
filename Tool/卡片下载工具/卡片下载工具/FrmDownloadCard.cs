using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading ;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;
using Ralid.Park.Hardware;

namespace Ralid.Park.DownloadCard
{
    public partial class FrmDownloadCard : Form
    {
        public FrmDownloadCard()
        {
            InitializeComponent();
        }

        #region 私有方法
        private Ralid.Park.Hardware.CardType ConvertCardType(Ralid.Park.BusinessModel.Enum.CardType cardType)
        {
            Ralid.Park.BusinessModel.Enum.CardType baseCardType = Ralid.Park.BusinessModel.Enum.CardType.GetBaseCardType(cardType);
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.VipCard)
            {
                return Hardware.CardType.FreeCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.OwnerCard)
            {
                return Hardware.CardType.OwnerCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard)
            {
                return Hardware.CardType.MonthCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.PrePayCard)
            {
                return Hardware.CardType.PrePayCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.TempCard)
            {
                return Hardware.CardType.TempCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1)
            {
                return Hardware.CardType.UserDefinedCard1;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2)
            {
                return Hardware.CardType.UserDefinedCard2;
            }
            else
            {
                return Hardware.CardType.TempCard;
            }
        }

        private Ralid.Park.BusinessModel.Enum.CardType ConvertCardType(Ralid.Park.Hardware.CardType cardType)
        {
            if (cardType == Hardware.CardType.FreeCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.VipCard;
            }
            else if (cardType == Hardware.CardType.OwnerCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.OwnerCard;
            }
            else if (cardType == Hardware.CardType.MonthCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard;
            }
            else if (cardType == Hardware.CardType.PrePayCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.PrePayCard;
            }
            else if (cardType == Hardware.CardType.TempCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.TempCard;
            }
            else if (cardType == Hardware.CardType.UserDefinedCard1)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1;
            }
            else if (cardType == Hardware.CardType.UserDefinedCard2)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2;
            }
            else
            {
                return Ralid.Park.BusinessModel.Enum.CardType.TempCard;
            }
        }

        private Ralid.Park.Hardware.CardOptions ConvertCardOptions(Ralid.Park.BusinessModel.Enum.CardOptions options)
        {
            Ralid.Park.Hardware.CardOptions ops = 0;
            if ((options & BusinessModel.Enum.CardOptions.OfflineHandleWhenOfflineMode) == BusinessModel.Enum.CardOptions.OfflineHandleWhenOfflineMode) ops |= Hardware.CardOptions.OfflineHandleWhenOfflineMode;
            ops |= (Ralid.Park.Hardware.CardOptions)0x02;
            if ((options & BusinessModel.Enum.CardOptions.ForbidRepeatIn) == BusinessModel.Enum.CardOptions.ForbidRepeatIn) ops |= Hardware.CardOptions.ForbidRepeatIn;
            if ((options & BusinessModel.Enum.CardOptions.ForbidRepeatOut) == BusinessModel.Enum.CardOptions.ForbidRepeatOut) ops |= Hardware.CardOptions.ForbidRepeatOut;
            if ((options & BusinessModel.Enum.CardOptions.WithCount) == BusinessModel.Enum.CardOptions.WithCount) ops |= Hardware.CardOptions.WithCount;
            if ((options & BusinessModel.Enum.CardOptions.ForbidWhenFull) == BusinessModel.Enum.CardOptions.ForbidWhenFull) ops |= Hardware.CardOptions.ForbidWhenFull;
            if ((options & BusinessModel.Enum.CardOptions.HolidayEnable) == BusinessModel.Enum.CardOptions.HolidayEnable) ops |= Hardware.CardOptions.HolidayEnabled;
            if ((options & BusinessModel.Enum.CardOptions.ForbidWhenExpired) == BusinessModel.Enum.CardOptions.ForbidWhenExpired) ops |= Hardware.CardOptions.ForbidWhenExpired;
            return ops;
        }

        private string GetString(Card card)
        {
            return string.Format("卡号:{0} 卡类型:{1} 车型:{2} 有效:{3} 生效日期：{6} 有效期:{4} 权限组:{5} 选项:{7} 车牌:{8}",
                card.CardID, card.CardType, card.CarType, card.IsValid, card.ValidDate, card.AccessID, card.ActivationDate, (int)card.Options,
                card.CarPlate == null ? string.Empty : card.CarPlate);
        }
        #endregion

        private void FrmDownloadCard_Load(object sender, EventArgs e)
        {
            this.hardwareTree1.Init();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<EntranceInfo> entrances = this.hardwareTree1.GetSelectedEntrances();
            if (entrances == null || entrances.Count == 0)
            {
                MessageBox.Show("请选择要下载卡片的通道");
                return;
            }
            List<CardInfo> allCards = (new CardBll(AppSettings.CurrentSetting.SQLConnect)).GetAllCards().QueryObjects;
            if (allCards == null || allCards.Count == 0)
            {
                MessageBox.Show("系统中没有卡片需要下载");
                return;
            }
            List<Card> deviceCards = new List<Card>();
            foreach (CardInfo card in allCards)
            {
                //if (ConvertCardType(card.CardType) != Hardware.CardType.UserDefinedCard1 && ConvertCardType(card.CardType) != Hardware.CardType.UserDefinedCard2)
                //{
                    Card c = new Card()
                                {
                                    CardID = uint.Parse(card.CardID),
                                    CardType = ConvertCardType(card.CardType),
                                    CarType = (Hardware.CarType)CarTypeSetting.Current.GetHardwareCarType(card.CarType),
                                    IsValid = true,
                                    ValidDate = card.ValidDate,
                                    AccessID = card.AccessID,
                                    Options = ConvertCardOptions(card.Options),
                                    ActivationDate = card.ActivationDate,
                                    CarPlate = card.CarPlate
                                };
                    deviceCards.Add(c);
               //}
            }
            DateTime dt = DateTime.Now;
            foreach (EntranceInfo entrance in entrances)
            {
                string log = dt.ToString("yyyyMMddHHmmss") + "_" + entrance.EntranceName + "_" + "下发失败";
                LANInfo lan = new LANInfo();
                lan.IPAddress = entrance.IPAddress;
                lan.IPMask = entrance.IPMask;
                lan.GateWay = entrance.Gateway;
                lan.ControlPort = entrance.ControlPort;
                ParkDevice device = new ParkDevice(lan);
                FrmProcessing frm = new FrmProcessing();
                frm.Text = "卡片下发 " + entrance.EntranceName;
                Action action = delegate()
                {
                    try
                    {
                        DateTime begin = DateTime.Now;
                        Thread.Sleep(1000);
                        frm.ShowProgress(entrance.EntranceName + " 正在清空卡片... ", 0);
                        bool ret1 = device.ClearCards();
                        if (!ret1)
                        {
                            frm.ShowProgress("清空卡片失败", 1.0m);
                            return;
                        }
                        frm.ShowProgress("清空卡片成功", .01m);
                        int count = 0;
                        int fail = 0;
                        while (count < deviceCards.Count)
                        {
                            int cc = deviceCards.Count - count > 16 ? 16 : deviceCards.Count - count;
                            Card[] cards = new Card[cc];
                            deviceCards.CopyTo(count, cards, 0, cc);
                            count += cc;
                            byte ret = device.SaveCards(cards);
                            if (ret != 0)
                            {
                                fail += cc;
                                foreach (Card c in cards)
                                {
                                    Ralid.GeneralLibrary.LOG.FileLog.Log(log, GetString(c));
                                }
                            }
                            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
                            frm.ShowProgress(string.Format("已经下发{0} 总共{1} 下发失败{2} 耗时{3}秒", count, deviceCards.Count, fail, (int)ts.TotalSeconds), (decimal)(count) / deviceCards.Count);
                        }
                    }
                    catch (ThreadAbortException ex)
                    {
                    }
                };
                Thread t = new Thread(new ThreadStart(action));
                t.Start();

                if (frm.ShowDialog() != DialogResult.OK)
                {
                    t.Abort();
                }
            }
        }
    }
}
