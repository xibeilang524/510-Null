using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class UCCardTypeProperty : UserControl
    {
        public UCCardTypeProperty()
        {
            InitializeComponent();
        }

        #region 私有变量
        private CardType _CardType;
        private ushort[] _CardTypeProperty;
        private bool? _IsExit;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置卡片类型属性
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] CardTypeProperty
        {
            get
            {
                SaveCardTypeProperty();
                return _CardTypeProperty;
            }
            set
            {
                _CardTypeProperty = value;
                ShowCardTypeProperty();
            }
        }
        /// <summary>
        /// 获取或设置当前的卡片类型
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardType SelectedCardType
        {
            get
            {
                return _CardType;
            }
            set
            {
                _CardType = value;
                ShowCardTypeProperty();
            }
        }

        /// <summary>
        /// 获取或设置是否出口控制器
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool? IsExit
        {
            get
            {
                return _IsExit;
            }
            set
            {
                _IsExit = value;
            }
        }
        #endregion

        #region 私有方法
        private void ShowCardTypeProperty()
        {
            EntranceCardTypeProperty property = EntranceCardTypeProperty.Default;

            if (_CardType == CardType.VipCard)
            {
                property=(EntranceCardTypeProperty)_CardTypeProperty[0];
            }
            else if (_CardType == CardType.OwnerCard)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[1];
            }
            else if (_CardType == CardType.MonthRentCard)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[2];
            }
            else if (_CardType == CardType.PrePayCard)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[3];
            }
            else if (_CardType == CardType.TempCard)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[4];
            }
            else if (_CardType == CardType.UserDefinedCard1)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[5];
            }
            else if (_CardType == CardType.UserDefinedCard2)
            {
                property = (EntranceCardTypeProperty)_CardTypeProperty[6];
            }

            ShowProperty(property);
        }

        private void SaveCardTypeProperty()
        {
            EntranceCardTypeProperty property = GetProperty();

            if (_CardType == CardType.VipCard)
            {
                _CardTypeProperty[0] = (ushort)property;
            }
            else if (_CardType == CardType.OwnerCard)
            {
                _CardTypeProperty[1] = (ushort)property;
            }
            else if (_CardType == CardType.MonthRentCard)
            {
                _CardTypeProperty[2] = (ushort)property;
            }
            else if (_CardType == CardType.PrePayCard)
            {
                _CardTypeProperty[3] = (ushort)property;
            }
            else if (_CardType == CardType.TempCard)
            {
                _CardTypeProperty[4] = (ushort)property;
            }
            else if (_CardType == CardType.UserDefinedCard1)
            {
                _CardTypeProperty[5] = (ushort)property;
            }
            else if (_CardType == CardType.UserDefinedCard2)
            {
                _CardTypeProperty[6] = (ushort)property;
            }

        }

        private void ShowProperty(EntranceCardTypeProperty property)
        {
            this.chkEnterNotWriteCarPlate.Checked = (property & EntranceCardTypeProperty.EnterNotWriteCarPlate) == EntranceCardTypeProperty.EnterNotWriteCarPlate;
            this.chkNotCompareCarPlate.Checked = (property & EntranceCardTypeProperty.NotCompareCarPlate) == EntranceCardTypeProperty.NotCompareCarPlate;
            this.chkCompareFailOpenGate.Checked = (property & EntranceCardTypeProperty.CompareFailOpenGate) == EntranceCardTypeProperty.CompareFailOpenGate;
            this.chkWriteCardHandle.Checked = (property & EntranceCardTypeProperty.WriteCardHandle) == EntranceCardTypeProperty.WriteCardHandle;
            this.chkEnabledWiegand.Checked = (property & EntranceCardTypeProperty.EnabledWiegandReader) == EntranceCardTypeProperty.EnabledWiegandReader;
        }

        private EntranceCardTypeProperty GetProperty()
        {
            EntranceCardTypeProperty property = EntranceCardTypeProperty.Default;
            if (!this.chkEnterNotWriteCarPlate.Checked) property ^= EntranceCardTypeProperty.EnterNotWriteCarPlate;
            if (!this.chkNotCompareCarPlate.Checked) property ^= EntranceCardTypeProperty.NotCompareCarPlate;
            if (!this.chkCompareFailOpenGate.Checked) property ^= EntranceCardTypeProperty.CompareFailOpenGate;
            if (!this.chkWriteCardHandle.Checked) property ^= EntranceCardTypeProperty.WriteCardHandle;
            if (!this.chkEnabledWiegand.Checked) property ^= EntranceCardTypeProperty.EnabledWiegandReader;

            return property;
        }                
        #endregion

        #region 公共方法
        public virtual void Init()
        {
            this.comCardType.Items.Clear();
            this.comCardType.Items.AddRange(
                new CardType[]{
                CardType.VipCard,
                CardType.OwnerCard ,
                CardType.MonthRentCard ,
                CardType.PrePayCard ,
                CardType.TempCard ,
                CardType.UserDefinedCard1,
                CardType.UserDefinedCard2
            });
            this.comCardType.DropDownStyle = ComboBoxStyle.DropDownList;

            //卡类型包括：0免费卡；1业主卡；2月租卡；3储值卡；4临时卡；5自定义卡1；6自定义卡2；其余预留。总共16种卡型
            _CardTypeProperty = new ushort[16];
            for (int i = 0; i < 16; i++)
            {
                ushort p = 0xFFFF;//默认设为全1
                if (i == 3)//储值卡默认不允许在韦根读卡器刷卡，其余卡类型默认允许在韦根读卡器刷卡。
                {
                    p ^= (byte)EntranceCardTypeProperty.EnabledWiegandReader;
                }
                else if (i == 4)//临时卡默认不允许在韦根读卡器刷卡，入口车牌写卡，其余卡类型默认允许在韦根读卡器刷卡，入口车牌不写卡。
                {
                    p ^= (byte)EntranceCardTypeProperty.EnabledWiegandReader;
                    p ^= (byte)EntranceCardTypeProperty.EnterNotWriteCarPlate;
                }
                _CardTypeProperty[i] = p;
            }

            this.comCardType.SelectedItem = CardType.VipCard;
        }
        #endregion

        #region 控件事件
        private void comCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCardTypeProperty();
            _CardType = (CardType)this.comCardType.SelectedItem;
            ShowCardTypeProperty();
        }
        #endregion
    }
}
