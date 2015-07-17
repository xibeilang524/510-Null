using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.UI.Resources;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardOut : Form
    {
        public FrmCardOut()
        {
            InitializeComponent();
        }

        #region 私有变量
        private bool _StartOut = false;
        /// <summary>
        /// 每个停车场直接出场的卡片数
        /// </summary>
        private Dictionary<ParkInfo, short> _OutCardsWitPark = new Dictionary<ParkInfo, short>();
        #endregion

        #region 私有方法
        private void ClearMsg()
        {
            this.listMsg.Items.Clear();
            this.listMsg.HorizontalExtent = 0;
        }
        private void CardReadHandler(object sender, CardReadEventArgs args)
        {
            Action action = delegate()
            {
                CardInfo readcard = CardDateResolver.Instance.GetCardInfoFromData(args.CardID, args[GlobalVariables.ParkingSection]);
                ReadCardHandler(args.CardID, readcard);
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ReadCardHandler(string cardID, CardInfo info)
        {
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CardInfo card = bll.GetCardByID(cardID).QueryObject;

            string msg = string.Empty;

            if (card == null)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_UnRegister);
            }
            else if (AppSettings.CurrentSetting.EnableWriteCard
                && !card.OnlineHandleWhenOfflineMode
                && !CardDateResolver.Instance.CopyPaidDataToCard(card, info))
            {
                //写卡模式时，卡片收费信息从扇区数据中获取
                msg = Resource1.FrmCardCenterCharge_CardDataErr;
            }
            else if (!card.IsInPark)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_StillOut);
            }
            else if (!card.IsTempCard)
            {
                msg = Resource1.FrmCardOut_NoTempCard;
            }
            else
            {
                msg = CardOut(bll, card);
            }

            if (!string.IsNullOrEmpty(msg))
            {
                this.listMsg.InsertMessage(msg);
            }
        }

        private string CardOut(CardBll bll, CardInfo card)
        {
            ParkInfo park = this.ucEntrance1.SelectedPark;
            EntranceInfo entrance = this.ucEntrance1.SelectedEntrance;
            //手动生成出场事件
            CardEventReport report = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, park.WorkMode, card.CarType, TariffSetting.Current, DateTime.Now);
            report.EventStatus = CardEventStatus.Valid;
            report.OperatorID = OperatorInfo.CurrentOperator.OperatorName ;
            report.StationID = WorkStationInfo.CurrentStation.StationName ;
            report.UpdateFlag = true;//先标识为已上传


            CardInfo origal = card.Clone();
            ////卡片出场
            //card.ParkingStatus = ParkingStatus.Out;
            //card.LastDateTime = DateTime.Now;

            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && card != null
                && !card.OnlineHandleWhenOfflineMode;


            //CommandResult result = bll.UpdateCardAll(card);
            CommandResult result = bll.SaveCardAndEvent(card, report);
            //写卡模式需要将收费信息写入卡片扇区
            if (result.Result == ResultCode.Successful && offlineHandleCard)
            {
                if (CardOperationManager.Instance.WriteCardLoop(card) != CardOperationResultCode.Success)
                {
                    //写入失败时，需将数据库卡片状态还原及删除出场事件
                    bll.UpdateCardAll(origal);
                    (new CardEventBll(AppSettings.CurrentSetting.ParkConnect)).Delete(new CardEventRecord(report));
                    result = new CommandResult(ResultCode.Fail);
                }
            }
            if (result.Result == ResultCode.Successful)
            {
                if (string.IsNullOrEmpty(AppSettings.CurrentSetting.CurrentStandbyConnect))
                {
                    //更新到备用数据库
                    CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    //standbybll.UpdateCardAll(card);
                    standbybll.SaveCardAndEvent(card, report);
                }
                string msg = string.Format("{0}:[{1}] {2}:[{3}]",Resource1.FrmCardOut_CardID, card.CardID,Resource1.FrmCardOut_InDateTime, origal.LastDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //插入卡片直接出场报警记录
                AlarmInfo alarm = new AlarmInfo();
                alarm.AlarmDateTime = card.LastDateTime;
                alarm.AlarmType = AlarmType.CardOutAnomaly;
                alarm.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                alarm.AlarmDescr = msg + string.Format(" {0}:[{1}]",Resource1.FrmCardOut_OutReason, this.txtMemo.Text.Trim());
                new AlarmBll(AppSettings.CurrentSetting.ParkConnect).Insert(alarm);

                if (!_OutCardsWitPark.ContainsKey(park))
                {
                    _OutCardsWitPark.Add(park, 0);
                }
                //该停车场出场卡片数加1
                _OutCardsWitPark[park] += 1;

                this.txtCount.Text = _OutCardsWitPark[park].ToString();

                return msg;
            }
            return result.Message;

        }

        private bool CheckInput()
        {
            if (this.ucEntrance1.SelectedPark == null)
            {
                MessageBox.Show(Resource1.FrmCardOut_SelectPark);
                return false;
            }
            if (this.ucEntrance1.SelectedEntrance == null)
            {
                MessageBox.Show(Resource1.FrmCardOut_SelectEntrance);
                return false;
            }
            return true;
        }
        #endregion

        #region 事件处理
        private void FrmCardOut_Load(object sender, EventArgs e)
        {
            this.txtMemo.Items.Clear();
            this.ucEntrance1.OnlyExit = true;
            this.ucEntrance1.Init();
            if (UserSetting.Current.PaymentComments != null && UserSetting.Current.PaymentComments.Count > 0)
            {
                foreach (string comment in UserSetting.Current.PaymentComments)
                {
                    this.txtMemo.Items.Add(comment);
                }
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                if (_StartOut)
                {
                    this.btnOut.Text = Resource1.FrmCardOut_StarOut;
                    _StartOut = false;
                    this.ucEntrance1.Enabled = true;
                    CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
                }
                else
                {
                    this.btnOut.Text =Resource1.FrmCardOut_StopOut;
                    _StartOut = true;
                    this.ucEntrance1.Enabled = false;
                    ParkInfo park = this.ucEntrance1.SelectedPark;
                    this.txtCount.Text = _OutCardsWitPark.ContainsKey(park) ? _OutCardsWitPark[park].ToString() : "0";
                    CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
                }
            }
        }
        private void FrmCardOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);

            if (_OutCardsWitPark.Keys.Count > 0)
            {
                MessageBox.Show(Resource1.FrmCardOut_UpdateVacant);

                this.Cursor = Cursors.WaitCursor;
                foreach (ParkInfo park in _OutCardsWitPark.Keys)
                {
                    bool ret = false;
                    IParkingAdapter pad = ParkingAdapterManager.Instance[park.ParkID];
                    if (pad != null)
                    {
                        ret = pad.ModifiedVacant(_OutCardsWitPark[park]);
                    }
                    //插入卡片直接出场报警记录，记录增加车位数
                    AlarmInfo alarm = new AlarmInfo();
                    alarm.AlarmDateTime = DateTime.Now;
                    alarm.AlarmType = AlarmType.CardOutAnomaly;
                    alarm.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    alarm.AlarmDescr = string.Format("[{0}] " + Resource1.FrmCardOut_OutCount + "：[{1}]，" + Resource1.FrmCardOut_VacantCount + "[{2}]", park.ParkName, _OutCardsWitPark[park].ToString(), ret ? Resource1.Form_Success : Resource1.Form_Fail);
                    new AlarmBll(AppSettings.CurrentSetting.ParkConnect).Insert(alarm);
                    if (!ret)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(string.Format("{0} ，{1}", alarm.AlarmDescr, Resource1.FrmCardOut_VacantManual));
                        this.Cursor = Cursors.WaitCursor;
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
        }
        #endregion


    }
}
