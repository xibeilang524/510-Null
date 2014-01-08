using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class CardGridView : CustomDataGridView
    {
        public CardGridView()
        {
            InitializeComponent();
        }

        public CardGridView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        DataGridViewTextBoxColumn colOwnerName;
        DataGridViewTextBoxColumn colCardID;
        DataGridViewTextBoxColumn colCarPlate;
        DataGridViewTextBoxColumn colCardCertificate;
        DataGridViewTextBoxColumn colCardType;
        DataGridViewTextBoxColumn colChargeType;
        DataGridViewTextBoxColumn colStatus;
        DataGridViewTextBoxColumn colActivationDate;
        DataGridViewTextBoxColumn colValidDate;
        DataGridViewCheckBoxColumn colRepeatIn;
        DataGridViewCheckBoxColumn colRepeatOut;
        DataGridViewCheckBoxColumn colWithCount;
        DataGridViewCheckBoxColumn colEnableWhenExpired;
        DataGridViewCheckBoxColumn colCanEnterWhenFull;
        DataGridViewCheckBoxColumn colHolidayEnabled;
        DataGridViewTextBoxColumn colBalance;
        DataGridViewTextBoxColumn colAccessLevel;
        DataGridViewTextBoxColumn colInPark;
        DataGridViewTextBoxColumn colFill;

        #region 公共方法和属性
        public void Init()
        {
            colOwnerName = new DataGridViewTextBoxColumn();
            colCardID = new DataGridViewTextBoxColumn();
            colCarPlate = new DataGridViewTextBoxColumn();
            colCardCertificate = new DataGridViewTextBoxColumn();
            colCardType = new DataGridViewTextBoxColumn();
            colChargeType = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActivationDate = new DataGridViewTextBoxColumn();
            colValidDate = new DataGridViewTextBoxColumn();
            colRepeatIn = new DataGridViewCheckBoxColumn();
            colRepeatOut = new DataGridViewCheckBoxColumn();
            colHolidayEnabled = new DataGridViewCheckBoxColumn();
            colWithCount = new DataGridViewCheckBoxColumn();
            colEnableWhenExpired = new DataGridViewCheckBoxColumn();
            colCanEnterWhenFull = new DataGridViewCheckBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            colAccessLevel = new DataGridViewTextBoxColumn();
            colInPark = new DataGridViewTextBoxColumn();
            colFill = new DataGridViewTextBoxColumn();
            // 
            // colOwnerName
            // 
            colOwnerName.HeaderText = Resources.Resource1.CardGridHeader_OwnerName;
            colOwnerName.Name = "colOwnerName";
            colOwnerName.Width = 80;
            // 
            // colCardID
            // 
            colCardID.HeaderText = Resources.Resource1.CardGridHeader_CardID;
            colCardID.Name = "colCardID";
            colCardID.ReadOnly = true;
            colCardID.Width = 70;
            // 
            // colCarNum
            // 
            colCarPlate.HeaderText = Resources.Resource1.CardGridHeader_CarPlate;
            colCarPlate.Name = "colCarPlate";
            colCarPlate.ReadOnly = true;
            colCarPlate.Width = 90;
            //
            //colCardCertificate
            //
            colCardCertificate.HeaderText = Resources.Resource1.CardGridHeader_Certificate;
            colCardCertificate.Name = "colCardCertificate";
            colCardCertificate.Width = 80;
            // 
            // colCardType
            // 
            colCardType.HeaderText = Resources.Resource1.CardGridHeader_CardType;
            colCardType.Name = "colCardType";
            colCardType.ReadOnly = true;
            colCardType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colCardType.Width = 80;
            // 
            // colChargeType
            // 
            colChargeType.HeaderText = Resources.Resource1.CardGridHeader_CarType;
            colChargeType.Name = "colChargeType";
            colChargeType.ReadOnly = true;
            colChargeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colChargeType.Width = 80;
            // 
            // colStatus
            // 
            colStatus.HeaderText = Resources.Resource1.CardGridHeader_CardStatus;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colStatus.Width = 80;
            // 
            // colActivationDate
            // 
            colActivationDate.HeaderText = Resources.Resource1.CardGridHeader_ActivationDate;
            colActivationDate.Name = "colActivationDate";
            colActivationDate.ReadOnly = true;
            // 
            // colValidDate
            // 
            colValidDate.HeaderText = Resources.Resource1.CardGridHeader_ValidDate;
            colValidDate.Name = "colValidDate";
            colValidDate.ReadOnly = true;
            // 
            // colRepeatIn
            // 
            colRepeatIn.HeaderText = Resources.Resource1.CardGridHeader_RepeatIn;
            colRepeatIn.Name = "colRepeatIn";
            colRepeatIn.ReadOnly = true;
            colRepeatIn.Width = 40;
            // 
            // colRepeatOut
            // 
            colRepeatOut.HeaderText = Resources.Resource1.CardGridHeader_RepeatOut;
            colRepeatOut.Name = "colRepeatOut";
            colRepeatOut.ReadOnly = true;
            colRepeatOut.Width = 40;
            // 
            // colWithCount
            // 
            colWithCount.HeaderText = Resources.Resource1.CardGridHeader_WithCount;
            colWithCount.Name = "colWithCount";
            colWithCount.ReadOnly = true;
            colWithCount.Width = 40;
            // 
            // colCanEnterWhenFull
            // 
            colCanEnterWhenFull.HeaderText = Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            colCanEnterWhenFull.Name = "colCanEnterWhenFull";
            colCanEnterWhenFull.ReadOnly = true;
            colCanEnterWhenFull.Width = 40;
            // 
            // colEnableWhenExpired
            // 
            colEnableWhenExpired.HeaderText = Resources.Resource1.CardGridHeader_EnableWhenExpired;
            colEnableWhenExpired.Name = "colEnableWhenExpired";
            colEnableWhenExpired.ReadOnly = true;
            colEnableWhenExpired.Width = 40;
            // 
            // colHolidayEnabled
            // 
            colHolidayEnabled.HeaderText = Resources.Resource1.CardGridHeader_HolidayEnabled;
            colHolidayEnabled.Name = "colHolidayEnabled";
            colHolidayEnabled.ReadOnly = true;
            colHolidayEnabled.Width = 40;
            // 
            // colBalance
            // 
            colBalance.HeaderText = Resources.Resource1.CardGridHeader_Balance;
            colBalance.Name = "colBalance";
            colBalance.ReadOnly = true;
            colBalance.Width = 80;
            // 
            // colAccessLevel
            // 
            colAccessLevel.HeaderText = Resources.Resource1.CardGridHeader_Access;
            colAccessLevel.Name = "colAccessLevel";
            colAccessLevel.ReadOnly = true;
            colAccessLevel.Width = 80;
            // 
            // colInPark
            // 
            colInPark.HeaderText = Resources.Resource1.CardGridHeader_ParkingFlag;
            colInPark.Name = "colInPark";
            colInPark.ReadOnly = true;
            colInPark.Width = 80;
            //
            //colFill
            //
            colFill.HeaderText = "";
            colFill.Name = "colFill";
            colFill.ReadOnly = true;
            colFill.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.Columns.Clear();
            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            colOwnerName,
            colCardID,
            colCardCertificate ,
            colCarPlate,
            colCardType,
            colChargeType,
            colStatus,
            colActivationDate ,
            colValidDate,
            colRepeatIn ,
            colRepeatOut ,
            colHolidayEnabled ,
            colWithCount ,
            colCanEnterWhenFull ,
            colEnableWhenExpired ,
            colBalance,
            colAccessLevel,
            colInPark,
            colFill});

            this.RowHeadersVisible = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn col in this.Columns)
            {
                col.ReadOnly = true;
            }
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// 设置或获取卡片
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CardInfo> CardSource
        {
            get
            {
                List<CardInfo> cards = new List<CardInfo>();
                foreach (DataGridViewRow row in this.Rows)
                {
                    cards.Add(row.Tag as CardInfo);
                }
                return cards;
            }
            set
            {
                if (value != null)
                {
                    ShowCardsOnGridView(value);
                }
            }
        }
        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="info"></param>
        public void AddCardInfo(CardInfo info)
        {
            int row = this.Rows.Add();
            ShowItemInGridViewRow(this.Rows[row], info);
        }
        /// <summary>
        /// 修改一行
        /// </summary>
        /// <param name="info"></param>
        public void UpdateCardInfo(CardInfo info)
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                CardInfo item = row.Tag as CardInfo;
                if (item != null && item.CardID == info.CardID)
                {
                    ShowItemInGridViewRow(row, info);
                }
            }
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="info"></param>
        public void DeleteCardInfo(CardInfo info)
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                CardInfo item = row.Tag as CardInfo;
                if (item != null && item.CardID == info.CardID)
                {
                    this.Rows.Remove(row);
                }
            }
        }

        /// <summary>
        /// 获取卡片的位置,不存在时返回-1
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public int IndexOfCard(string cardID)
        {
            int index = -1;
            foreach (DataGridViewRow row in this.Rows)
            {
                CardInfo item = row.Tag as CardInfo;
                if (item != null && item.CardID == cardID)
                {
                    index = row.Index;
                }
            }
            return index;
        }

        /// <summary>
        /// 选择卡片,卡片所在行会高亮显示
        /// </summary>
        /// <param name="cardID"></param>
        public void SelectedCard(string cardID)
        {
            int index = IndexOfCard(cardID);
            if (index > -1)
            {
                SelectNone();
                this.FirstDisplayedScrollingRowIndex = index;
                this.Rows[index].Selected = true;
            }
        }

        /// <summary>
        /// 全不选
        /// </summary>
        public void SelectNone()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                row.Selected = false;
            }
        }

        /// <summary>
        /// 获取当前选择的所有卡片信息
        /// </summary>
        /// <returns></returns>
        public List<CardInfo> GetSelectedCards()
        {
            List<CardInfo> cards = new List<CardInfo>();
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.Selected)
                {
                    cards.Add(row.Tag as CardInfo);
                }
            }
            return cards;
        }

        /// <summary>
        /// 通过网络索引号获取卡片信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CardInfo GetCardInfoAt(int index)
        {
            if (index >= 0 && this.Rows.Count > index)
            {
                return this.Rows[index].Tag as CardInfo;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存成CVS格式的文件
        /// </summary>
        /// <param name="file"></param>
        public void SaveToFile(string file)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                using (fs = new FileStream(file, FileMode.Create))
                {
                    using (writer = new StreamWriter(fs, Encoding.Unicode))
                    {
                        StringBuilder header = new StringBuilder();
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            header.Append(this.Columns[i].HeaderText + "\t");
                        }
                        writer.WriteLine(header.ToString());
                        foreach (DataGridViewRow row in this.Rows)
                        {
                            StringBuilder rowText = new StringBuilder();
                            for (int i = 0; i < this.Columns.Count; i++)
                            {
                                if (row.Cells[i].Value != null)
                                {
                                    rowText.Append(row.Cells[i].Value.ToString() + "\t");
                                }
                                else
                                {
                                    rowText.Append(string.Empty + "\t");
                                }
                            }
                            writer.WriteLine(rowText.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
            finally
            {
                if (writer != null) { writer.Close(); }
                if (fs != null) { fs.Close(); }
            }
        }

        /// <summary>
        /// 卡号列
        /// </summary>
        public DataGridViewColumn ColCardID { get { return colCardID; } }
        /// <summary>
        /// 车主姓名列
        /// </summary>
        public DataGridViewColumn ColOwnerName { get { return colOwnerName; } }
        /// <summary>
        /// 车牌号码列
        /// </summary>
        public DataGridViewColumn ColCarNum { get { return colCarPlate; } }
        /// <summary>
        /// 卡片类型列
        /// </summary>
        public DataGridViewColumn ColCardType { get { return colCardType; } }
        /// <summary>
        /// 收费车型列
        /// </summary>
        public DataGridViewColumn ColChargeType { get { return colChargeType; } }
        /// <summary>
        /// 卡片状态列
        /// </summary>
        public DataGridViewColumn ColStatus { get { return colStatus; } }
        /// <summary>
        /// 卡片有效期列
        /// </summary>
        public DataGridViewColumn ColValidDate { get { return colValidDate; } }
        /// <summary>
        /// 卡片余额列
        /// </summary>
        public DataGridViewColumn ColBalance { get { return colBalance; } }
        /// <summary>
        /// 卡片权限组列
        /// </summary>
        public DataGridViewColumn ColAccessLevel { get { return colAccessLevel; } }
        /// <summary>
        /// 卡片是否在场
        /// </summary>
        public DataGridViewColumn ColInPark { get { return colInPark; } }

        #endregion

        #region 私有方法
        private void ShowCardsOnGridView(List<CardInfo> cards)
        {
            this.Rows.Clear();
            foreach (CardInfo info in cards)
            {
                AddCardInfo(info);
            }
        }
        private void ShowItemInGridViewRow(DataGridViewRow row, CardInfo info)
        {
            row.Tag = info;
            row.Cells["colCardID"].Value = info.CardID;
            row.Cells["colOwnerName"].Value = info.OwnerName;
            row.Cells["colCardCertificate"].Value = info.CardCertificate;
            row.Cells["colCarPlate"].Value = info.CarPlate;
            row.Cells["colStatus"].Value = Ralid.Park.BusinessModel.Resouce.CardStatusDescription.GetDescription(info.Status);
            row.Cells["colCardType"].Value = info.CardType.ToString();
            row.Cells["colChargeType"].Value = CarTypeSetting.Current.GetDescription(info.CarType);
            row.Cells["colBalance"].Value = info.Balance;
            row.Cells["colActivationDate"].Value = info.ActivationDate.ToString("yyyy-MM-dd");
            row.Cells["colValidDate"].Value = info.ValidDate.ToString("yyyy-MM-dd");
            row.Cells["colHolidayEnabled"].Value = info.HolidayEnabled;
            row.Cells["colRepeatIn"].Value = info.CanRepeatIn;
            row.Cells["colRepeatOut"].Value = info.CanRepeatOut;
            row.Cells["colWithCount"].Value = info.WithCount;
            row.Cells["colCanEnterWhenFull"].Value = info.CanEnterWhenFull;
            row.Cells["colEnableWhenExpired"].Value = info.EnableWhenExpired;
            row.Cells["colAccessLevel"].Value = info.AccessID;
            row.Cells["colInPark"].Value = Ralid.Park.BusinessModel.Resouce.ParkingStatusDescription.GetDescription(info.ParkingStatus);
            if (info.Status != CardStatus.Enabled)
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        #endregion
    }
}
