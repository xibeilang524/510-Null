using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;

namespace PreferentialSystem
{
    public partial class FrmPreferentialCancel : Form
    {
        public FrmPreferentialCancel()
        {
            InitializeComponent();
        }
        private BarCodeReader _TicketReader;
        private PREPreferentialInfo _CurrentPreInfo;

        private void FrmPreferentialCancel_Load(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = false;
            if (AppSettings.CurrentSetting.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(AppSettings.CurrentSetting.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
            }
        }

        /// <summary>
        /// 读取到卡号处理
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="info">从卡片扇区数据中读取到的卡片信息</param>
        private void ReadCardIDHandler(string cardID, CardInfo info)
        {
            this.txtCardID.Text = cardID;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CardInfo card = bll.GetCardByID(cardID).QueryObject;
            ShowCardInfo(card, cardID);
        }

        private void ShowCardInfo(CardInfo card,string cardID)
        {
            this.txtCarPlate.Text = string.Empty;
            this.txtOwnerName.Text = string.Empty;
            this.txtEntranceTime.Text = string.Empty;
            this.txtEntranceTime.Tag = null;
            this.lblCardWarning.Text = string.Empty;

            
            if(card==null)
            {
                this.lblCardWarning.Text = "此卡未登记！";
                ClearInput();
            }
            //太古汇方要求没进场也能进行优惠，故这里不做卡片在场判断，By Jan 2015-03-05
            //else if(!card.IsInPark)
            //{
            //    //已出场的卡片不允许进行优惠
            //    this.lblCardWarning.Text = "此卡已出场！";
            //}
            else
            {
                this.txtCarPlate.Text = card.LastCarPlate;
                this.txtOwnerName.Text = card.OwnerName;
                this.txtEntranceTime.Text = card.LastDateTime.ToString();
                this.txtEntranceTime.Tag = card.LastDateTime;
                PREPreferentialBll preBll = new PREPreferentialBll(AppSettings.CurrentSetting.ParkConnect);
                CardSearchCondition con = new CardSearchCondition();
                con.CardID = cardID;
                con.LastDateTime = new DateTimeRange();
                //由于之前的版本保存入场时间时，都是由转换成string的时间字符串再转换成DateTime，导致号码部分丢失，所以这里的开始时间需求去掉毫秒部分才能查到以前的优惠记录
                con.LastDateTime.Begin = Convert.ToDateTime(card.LastDateTime.ToString());
                //con.LastDateTime.Begin = card.LastDateTime;
                con.LastDateTime.End = card.LastDateTime;
                List<PREPreferentialInfo> list = preBll.GetPreferentials(con).QueryObjects;
                PREPreferentialInfo workInfo = list.OrderByDescending(p => p.PreferentialTime).FirstOrDefault();
                if (workInfo != null)
                {
                    _CurrentPreInfo = workInfo;
                    this.txtHour.Text = workInfo.PreferentialHour.ToString();
                    this.txtTime.Text = workInfo.PreferentialTime.ToString();
                    this.txtBueiness1.Text = workInfo.BusinessesName1;
                    this.txtCost1.Text = workInfo.BusinessesMoney1.ToString();
                    this.txtBusiness2.Text = workInfo.BusinessesName2;
                    this.txtCost2.Text = workInfo.BusinessesMoney2.ToString();
                    this.txtBusiness3.Text = workInfo.BusinessesName3;
                    this.txtCost3.Text = workInfo.BusinessesMoney3.ToString();
                    this.txtNotes.Text = workInfo.Notes;
                    this.btnCancel.Enabled = true;
                    this.lblCardWarning.Text = string.Empty;
                }
                else
                {
                    this.lblCardWarning.Text = "此卡无优惠记录！";
                }
            }
        }

        private void ClearInput()
        {
            this.txtHour.Text = string.Empty;
            this.txtTime.Text = string.Empty;
            this.txtBueiness1.Text = string.Empty;
            this.txtCost1.Text = string.Empty;
            this.txtBusiness2.Text = string.Empty;
            this.txtCost2.Text = string.Empty;
            this.txtBusiness3.Text = string.Empty;
            this.txtCost3.Text = string.Empty;
            this.txtNotes.Text = string.Empty;
            //this.txtCancelReason.Text = string.Empty;
        }

        private void TicketReader_BarCodeRead(object sender, BarCodeReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.BarCode))
            {
                ClearCardInfo();
                ClearInput();
                string cardID = GetCardIDFromBarCode(e.BarCode);
                ReadCardIDHandler(cardID, null);
            }
        }

        private void ClearCardInfo()
        {
            this.lblCardWarning.Text = string.Empty;
            this.txtCardID.Text = string.Empty;
            this.txtCarPlate.Text = string.Empty;
            this.txtOwnerName.Text = string.Empty;
            this.txtEntranceTime.Text = string.Empty;
            this.txtEntranceTime.Tag = null;
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        private string GetCardIDFromBarCode(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode))
            {
                if (barcode.Length == 7)
                {
                    return barcode;
                }
                if (barcode.Length == 8)
                {
                    string ck = Ralid.GeneralLibrary.ITFCheckCreater.Create(barcode.Substring(0, barcode.Length - 1));
                    if (!string.IsNullOrEmpty(ck) && ck == barcode.Substring(barcode.Length - 1, 1))
                    {
                        return barcode.Substring(0, barcode.Length - 1);
                    }
                }
            }
            if (!string.IsNullOrEmpty(barcode) && AppSettings.CurrentSetting.Debug)
            {
                Ralid.GeneralLibrary.LOG.FileLog.Log("丢弃条码", barcode);
            }
            return string.Empty;
        }

        private void FrmPreferentialCancel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_TicketReader != null) _TicketReader.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {//取消优惠的代码逻辑
            if (CheckInput())
            {
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(AppSettings.CurrentSetting.ParkConnect);
                //1.删除优惠信息表的此项数据
                IPREPreferentialProvider preProvider = ProviderFactory.Create<IPREPreferentialProvider>(AppSettings.CurrentSetting.ParkConnect);
                preProvider.Delete(_CurrentPreInfo, unitWork);
                //2.将Card表的优惠时数减去
                ICardProvider cardProvider = ProviderFactory.Create<ICardProvider>(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = cardProvider.GetByID(_CurrentPreInfo.CardID).QueryObject;
                CardInfo newVal = card.Clone();
                newVal.DiscountHour -= _CurrentPreInfo.PreferentialHour;
                if (newVal.DiscountHour < 0) newVal.DiscountHour = 0;
                cardProvider.Update(newVal, card, unitWork);
                //3.保存优惠操作记录
                IPREPreferentialLogProvider preLogProvider = ProviderFactory.Create<IPREPreferentialLogProvider>(AppSettings.CurrentSetting.ParkConnect);
                PREPreferentialLog log = _CurrentPreInfo.CreateLog();
                log.OperatorTime = DateTime.Now;
                log.IsCancel = 1;
                log.CancelReason = this.txtCancelReason.Text.Trim();
                log.WorkstationID = PRESysOptionSetting.Current.PRESysOption.CurrentWorkstationID;
                log.WorkstationName = PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation;
                log.OperatorID = PREOperatorInfo.CurrentOperator.OperatorID;
                preLogProvider.Insert(log, unitWork); 
                CommandResult result = unitWork.Commit();
                if (result.Result == ResultCode.Successful)
                {
                    MessageBox.Show("取消成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCardInfo();
                    ClearInput();
                    btnCancel.Enabled = false;
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtCardID.Text.Trim()))
            {
                MessageBox.Show("没有卡号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.lblCardWarning.Text.Length > 0)
            {
                MessageBox.Show(this.lblCardWarning.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (_CurrentPreInfo == null)
            {
                MessageBox.Show("未找到最近一次优惠记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtCancelReason.Text.Trim()))
            {
                MessageBox.Show("请输入取消原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            string cardID = this.txtCardID.Text.Trim();

            #region 如果扫描枪使用模拟键盘输入
            if (AppSettings.CurrentSetting.TicketReaderCOMPort == 0)
            {
                if (cardID.Length == 8)
                {
                    string ck = Ralid.GeneralLibrary.ITFCheckCreater.Create(cardID.Substring(0, cardID.Length - 1));
                    if (!string.IsNullOrEmpty(ck) && ck == cardID.Substring(cardID.Length - 1, 1))
                    {
                        cardID = cardID.Substring(0, cardID.Length - 1);
                        this.txtCardID.TextChanged -= txtCardID_TextChanged;
                        this.txtCardID.Text = cardID;
                        this.txtCardID.TextChanged += txtCardID_TextChanged;
                    }
                }
            }
            #endregion
            if (!string.IsNullOrEmpty(cardID))
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = bll.GetCardByID(cardID).QueryObject;
                ShowCardInfo(card, cardID);
            }
        }

    }
}
