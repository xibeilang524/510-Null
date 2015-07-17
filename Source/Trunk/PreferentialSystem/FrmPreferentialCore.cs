using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;

namespace PreferentialSystem
{
    public partial class FrmPreferentialCore : Form
    {
        public FrmPreferentialCore()
        {
            InitializeComponent();
        }

        private BarCodeReader _TicketReader;

        private void FrmPreferentialCore_Load(object sender, EventArgs e)
        {
            this.preBusinessesComboBox1.Init();
            this.preBusinessesComboBox2.Init();
            this.preBusinessesComboBox3.Init();
            InitPreferentialHour(null);
            if (AppSettings.CurrentSetting.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(AppSettings.CurrentSetting.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
            }
        }

        /// <summary>
        /// 初始化优惠时数
        /// </summary>
        private void InitPreferentialHour(CardInfo card)
        {
            this.btnOK.Enabled = true;

            int maxHour = PRESysOptionSetting.Current.PRESysOption.MaxHour;
            int workHour = maxHour;
            if (this.lblCardWarning.Text.Length == 0 && card != null)
            {
                workHour = maxHour - card.DiscountHour;
            }
            this.cmbPreferentialHour.Text = string.Empty;
            this.cmbPreferentialHour.Items.Clear();
            for (int i = 0; i < workHour; i++)
            {
                this.cmbPreferentialHour.Items.Add(i+1);
            }
            if (workHour <= 0)
            {
                this.btnOK.Enabled = false;
                this.lblCardWarning.Text = "此卡优惠时数已达系统最大优惠时数，不能再进行优惠！";
            }
        }

        private void TicketReader_BarCodeRead(object sender, BarCodeReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.BarCode))
            {
                ClearCardInfo();
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

        private void ClearInput()
        {
            this.txtCardID.Text = string.Empty;
            this.txtCarPlate.Text = string.Empty;
            this.txtOwnerName.Text = string.Empty;
            this.txtEntranceTime.Text = string.Empty;
            this.lblCardWarning.Text = string.Empty;
            this.cmbPreferentialHour.Text = string.Empty;
            this.lbl_HasHour.Text = string.Empty;
            this.preBusinessesComboBox1.Text = string.Empty;
            this.preBusinessesComboBox2.Text = string.Empty;
            this.preBusinessesComboBox3.Text = string.Empty;
            this.txtCost1.Text = string.Empty;
            this.txtCost2.Text = string.Empty;
            this.txtCost3.Text = string.Empty;
            this.txtNotes.Text = string.Empty;
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
            this.lbl_HasHour.Text = string.Empty;//每次读卡都清空已录入优惠时数

            ShowCardInfo(card);

            
        }

        private void ShowCardInfo(CardInfo card)
        {
            this.txtCarPlate.Text = string.Empty;
            this.txtOwnerName.Text = string.Empty;
            this.txtEntranceTime.Text = string.Empty;
            this.txtEntranceTime.Tag = null;
            this.lblCardWarning.Text = string.Empty;

            if (card == null)
            {
                this.lblCardWarning.Text = "此卡未登记！";
            }
            //太古汇方要求没进场也能进行优惠，故这里不做卡片在场判断，By Jan 2015-03-05
            //else if (!card.IsInPark)
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
                #region 查询已录入的优惠时数
                this.lbl_HasHour.Text = card.DiscountHour.ToString();
                //PREPreferentialBll pBll = new PREPreferentialBll(AppSettings.CurrentSetting.ParkConnect);
                //CardSearchCondition con1 = new CardSearchCondition();
                //con1.CardID = card.CardID;
                //List<PREPreferentialInfo> list = pBll.GetPreferentials(con1).QueryObjects;
                //if (list.Count > 0)
                //    this.lbl_HasHour.Text = list.Sum(p => p.PreferentialHour).ToString();
                #endregion
                InitPreferentialHour(card);
            }
        }

        //对优惠时数可否编辑的控制
        private void cmbPreferentialHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (PRESysOptionSetting.Current.PRESysOption.IsAllowDefineHour != 1)
            {
                if ((int)e.KeyChar < 32)  // 控制键

                {
                    return;
                }
                e.Handled = true;
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPreferentialCore_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_TicketReader != null) _TicketReader.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                PREPreferentialInfo info = GetItemFromInput();
                //1.插入优惠信息
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(AppSettings.CurrentSetting.ParkConnect);
                IPREPreferentialProvider preProvider = ProviderFactory.Create<IPREPreferentialProvider>(AppSettings.CurrentSetting.ParkConnect);
                preProvider.Insert(info, unitWork);
                //2.更新卡片的优惠信息

                ICardProvider cardProvider = ProviderFactory.Create<ICardProvider>(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = cardProvider.GetByID(info.CardID).QueryObject;
                if (card == null)
                {
                    MessageBox.Show("没有此卡片！");
                    return;
                }
                CardInfo newCard = card.Clone();
                newCard.DiscountHour += info.PreferentialHour;
                newCard.PreferentialTime = info.PreferentialTime;
                cardProvider.Update(newCard, card, unitWork);
                //3.保存优惠操作记录
                IPREPreferentialLogProvider logProvider = ProviderFactory.Create<IPREPreferentialLogProvider>(AppSettings.CurrentSetting.ParkConnect);
                PREPreferentialLog log = info.CreateLog();
                log.OperatorTime = DateTime.Now;
                logProvider.Insert(log, unitWork);
                CommandResult result = unitWork.Commit();
                if (result.Result == ResultCode.Successful)
                {
                    MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInput();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        /// <summary>
        /// 获取录入的优惠信息
        /// </summary>
        /// <returns></returns>
        private PREPreferentialInfo GetItemFromInput()
        {
            PREPreferentialInfo info = new PREPreferentialInfo();
            info.PreferentialID = Guid.NewGuid();
            info.CardID = this.txtCardID.Text;
            info.PreferentialHour = Convert.ToInt32(this.cmbPreferentialHour.Text);
            info.BusinessesName1 = this.preBusinessesComboBox1.Text.Trim();
            info.BusinessesMoney1 = this.txtCost1.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(this.txtCost1.Text);
            info.BusinessesName2 = this.preBusinessesComboBox2.Text.Trim();
            info.BusinessesMoney2 = this.txtCost2.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(this.txtCost2.Text);
            info.BusinessesName3 = this.preBusinessesComboBox3.Text.Trim();
            info.BusinessesMoney3 = this.txtCost3.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(this.txtCost3.Text);
            info.Notes = this.txtNotes.Text;
            info.WorkstationID = PRESysOptionSetting.Current.PRESysOption.CurrentWorkstationID;
            info.WorkstationName = PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation;
            info.OperatorID = PREOperatorInfo.CurrentOperator.OperatorID;
            info.Operator = PREOperatorInfo.CurrentOperator;
            info.PreferentialTime = DateTime.Now;
            //if (!string.IsNullOrEmpty(this.txtEntranceTime.Text.Trim())) info.EntranceTime = Convert.ToDateTime(this.txtEntranceTime.Text.Trim());
            if (this.txtEntranceTime.Tag != null) info.EntranceTime = Convert.ToDateTime(this.txtEntranceTime.Tag);
            return info;
        }

        /// <summary>
        /// 验证输入的合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if(string.IsNullOrEmpty(this.txtCardID.Text.Trim()))
            {
                MessageBox.Show("没有卡片！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            if (this.lblCardWarning.Text.Length > 0)
            {
                MessageBox.Show(this.lblCardWarning.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.cmbPreferentialHour.Text.Length == 0)
            {
                MessageBox.Show("必须输入优惠时数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbPreferentialHour.Focus();
                return false;
            }
            int temp = 0;
            if (!string.IsNullOrEmpty(this.cmbPreferentialHour.Text.Trim()) && !int.TryParse(this.cmbPreferentialHour.Text, out temp))
            {
                MessageBox.Show("请输入正确的优惠时数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbPreferentialHour.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.txtCost1.Text.Trim()) && !int.TryParse(this.txtCost1.Text, out temp))
            {
                MessageBox.Show("请输入正确的消费金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtCost1.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.txtCost2.Text.Trim()) && !int.TryParse(this.txtCost2.Text, out temp))
            {
                MessageBox.Show("请输入正确的消费金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtCost1.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.txtCost3.Text.Trim()) && !int.TryParse(this.txtCost3.Text, out temp))
            {
                MessageBox.Show("请输入正确的消费金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtCost1.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.cmbPreferentialHour.Text.Trim()) && PRESysOptionSetting.Current.PRESysOption.IsAllowDefineHour == 1)
            {//当手动输入优惠时数时，阻止其超过当前最大可优惠时数
                int currentInputHour = Convert.ToInt32(this.cmbPreferentialHour.Text.Trim());
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = bll.GetCardByID(this.txtCardID.Text.Trim()).QueryObject;
                if (card != null)
                {
                    int hasPreferentialHour = card.DiscountHour;
                    if (currentInputHour + hasPreferentialHour > PRESysOptionSetting.Current.PRESysOption.MaxHour)
                    {
                        MessageBox.Show("当前优惠时数和已优惠时数之和已超过系统最大优惠时数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
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
                ShowCardInfo(card);
            }
        }

    }
}
