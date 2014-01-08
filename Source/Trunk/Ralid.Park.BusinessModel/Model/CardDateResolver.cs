using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.GeneralLibrary;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一个卡片数据的解析器
    /// </summary>
    public class CardDateResolver
    {
        #region 构造函数
        public CardDateResolver()
        { 
        }
        #endregion

        #region 静态属性
        private static CardDateResolver _instance;

        /// <summary>
        /// 获取一个卡片数据的解析器的实例
        /// </summary>
        public static CardDateResolver Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardDateResolver();
                }
                return _instance;
            }
        }
        #endregion

        #region 私有属性
        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime FromDate = new DateTime(2011, 1, 1, 0, 0, 0);
        #endregion


        #region 私有方法
        /// <summary>
        /// 获取数据的LRC校验（累加校验）
        /// </summary>
        /// <param name="data">为扇区数据时，48字节；为块数据时，16字节</param>
        /// <param name="block">块号，data为扇区数据时，块号为0~2；data为块数据时，块号为0</param>
        /// <returns></returns>
        private byte GetDataLRC(byte[] data, int block)
        {
            int lrc = 0;
            for (int i = block * 16; i < block * 16 + 15; i++)
            {
                lrc += data[i];
            }
            return (byte)(lrc & 0xFF);//0~14字节作LRC校验（累加校验）
        }

        /// <summary>
        /// 检查字节组是否全为0
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private bool BytesIsAllZero(byte[] bytes)
        { 
            foreach(byte b in bytes)
            {
                if (b != 0x00) return false;
            }
            return true;
        }

        /// <summary>
        /// 设置卡片信息
        /// </summary>
        /// <param name="card"></param>
        /// <param name="data"></param>
        /// <param name="keepStaus"></param>
        /// <returns></returns>
        private void SetCardInfo(CardInfo info, byte[] data,bool keepStaus)
        {
            int block0 = 0 * 16;
            int block1 = 1 * 16;
            int block2 = 2 * 16;

            byte[] tempbytes = new byte[4];

            #region 块0
            info.CardVersion = data[block0 + 0];

            if (!keepStaus)
            {
                info.Status = (data[block0 + 1] & 0x80) == 0x80 ? CardStatus.Enabled : CardStatus.Disabled;
            }

            //List<CardType> cardTypes = null;
            switch (data[block0 + 1] & 0x0F)
            {
                case 0x00:
                    info.CardType = CardType.VipCard;
                    break;
                case 0x01:
                    info.CardType = CardType.OwnerCard;
                    break;
                case 0x02:
                    info.CardType = CardType.MonthRentCard;
                    break;
                case 0x03:
                    info.CardType = CardType.PrePayCard;
                    break;
                case 0x04:
                    info.CardType = CardType.TempCard;
                    break;
                case 0x05:
                    info.CardType = CardType.UserDefinedCard1.GetFirstCardTypeFromBase;
                    break;
                case 0x06:
                    info.CardType = CardType.UserDefinedCard2.GetFirstCardTypeFromBase;
                    break;
                case 0x0E:
                    info.CardType = CardType.OperatorCard;
                    break;
                default:
                    info.CardType = CardType.TempCard;
                    break;
            }
            info.CarType = (byte)((data[block0 + 1] >> 4) & 0x03);

            info.OnlineHandleWhenOfflineMode = (data[block0 + 2] & 0x01) != 0x01;
            info.CanRepeatIn = (data[block0 + 2] & 0x04) != 0x04;
            info.CanRepeatOut = (data[block0 + 2] & 0x08) != 0x08;
            info.WithCount = (data[block0 + 2] & 0x10) == 0x10;
            info.CanEnterWhenFull = (data[block0 + 2] & 0x20) != 0x20;
            info.HolidayEnabled = (data[block0 + 2] & 0x40) == 0x40;
            info.EnableWhenExpired = (data[block0 + 2] & 0x80) != 0x80;

            info.AccessID = data[block0 + 3];

            if (data[block0 + 4] == 0x00 && data[block0 + 5] == 0x00 && data[block0 + 6] == 0x00)
            {
                info.ActivationDate = DateTime.MaxValue;
            }
            else if (data[block0 + 4] == 0xFF && data[block0 + 5] == 0xFF && data[block0 + 6] == 0xFF)
            {
                info.ActivationDate = DateTime.MinValue;
            }
            else
            {
                info.ActivationDate = new DateTime(2000 + BCDConverter.BCDtoInt(data[block0 + 4]), BCDConverter.BCDtoInt(data[block0 + 5]), BCDConverter.BCDtoInt(data[block0 + 6]));
            }

            if (data[block0 + 6] == 0x00 && data[block0 + 8] == 0x00 && data[block0 + 9] == 0x00)
            {
                info.ValidDate = DateTime.MinValue;
            }
            else if (data[block0 + 7] == 0xFF && data[block0 + 8] == 0xFF && data[block0 + 9] == 0xFF)
            {
                info.ValidDate = DateTime.MaxValue;
            }
            else
            {
                info.ValidDate = new DateTime(2000 + BCDConverter.BCDtoInt(data[block0 + 7]), BCDConverter.BCDtoInt(data[block0 + 8]), BCDConverter.BCDtoInt(data[block0 + 9]));
            }            

            #endregion

            #region 块1
            info.ParkingStatus = (data[block1 + 0] & 0x01) != 0x01 ? ParkingStatus.In : 0;
            info.ParkingStatus |= (data[block1 + 0] & 0x02) != 0x02 ? ParkingStatus.IndoorIn : 0;
            info.ParkingStatus |= (data[block1 + 0] & 0x08) != 0x08 ? ParkingStatus.PaidBill : 0;
            info.ParkingStatus |= (data[block1 + 0] & 0x40) != 0x40 ? ParkingStatus.NestedParkMarked : 0;

            if (data[block1 + 1] == 0x00 && data[block1 + 2] == 0x00 && data[block1 + 3] == 0x00 && data[block1 + 4] == 0x00)
            {
                //第1层车场入场时间无效
            }
            else
            {
                tempbytes[0] = data[block1 + 1];
                tempbytes[1] = data[block1 + 2];
                tempbytes[2] = data[block1 + 3];
                tempbytes[3] = data[block1 + 4];
                info.LastDateTime = FromDate.AddSeconds(SEBinaryConverter.BytesToLong(tempbytes));
            }

            if (data[block1 + 5] == 0x00 && data[block1 + 6] == 0x00 && data[block1 + 7] == 0x00 && data[block1 + 8] == 0x00)
            {
                //缴费时间无效
            }
            else
            {
                tempbytes[0] = data[block1 + 5];
                tempbytes[1] = data[block1 + 6];
                tempbytes[2] = data[block1 + 7];
                tempbytes[3] = data[block1 + 8];
                info.PaidDateTime = FromDate.AddSeconds(SEBinaryConverter.BytesToLong(tempbytes));
            }
            
            tempbytes[0] = data[block1 + 9];
            tempbytes[1] = data[block1 + 10];
            tempbytes[2] = data[block1 + 11];
            tempbytes[3] = 0x00;
            info.ParkFee = SEBinaryConverter.BytesToInt(tempbytes) / 100.00M;

            tempbytes[0] = data[block1 + 12];
            tempbytes[1] = data[block1 + 13];
            tempbytes[2] = data[block1 + 14];
            tempbytes[3] = 0x00;
            info.TotalPaidFee = SEBinaryConverter.BytesToInt(tempbytes) / 100.00M;
            //info.TotalFee = SEBinaryConverter.BytesToInt(tempbytes) / 100.00M;

            
            #endregion

            #region 块2
            tempbytes[0] = data[block2 + 0];
            tempbytes[1] = data[block2 + 1];
            tempbytes[2] = data[block2 + 2];
            tempbytes[3] = 0x00;
            info.Balance = SEBinaryConverter.BytesToInt(tempbytes) / 100.00M;
            
            Array.Resize(ref tempbytes, 9);
            Array.Copy(data, block2 + 6, tempbytes, 0, 9);
            if (BytesIsAllZero(tempbytes))
            {
                info.CarPlate = string.Empty;
            }
            else
            {
                info.CarPlate = Encoding.GetEncoding("gb2312").GetString(tempbytes);
                info.LastCarPlate = info.CarPlate;////脱机模式时，卡片的最后一次识别车牌保存在卡片里的车牌号码中
            }
            #endregion
        }

        
        
        #endregion

        #region 公共方法
        /// <summary>
        /// 卡片数据是否有效，检验数据为一个扇区的数据
        /// </summary>
        /// <param name="data">扇区数据1)前32扇区，48字节，3个块 2)后8扇区，240字节，15个块</param>
        /// <returns></returns>
        public bool IsValidData(byte[] data)
        {
            if (data == null || (data.Length != 48 && data.Length != 240) || data[0] != GlobalVariables.CurrentCardVersion) return false;//0字节为卡格式版本，1-255循环使用

            for (int i = 0; i < 3; i++)
            {
                if (data[i * 16 + 15] != GetDataLRC(data, i)) return false;
            }

            return true;
        }

        /// <summary>
        /// 从卡片扇区数据中获取卡片信息实体类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CardInfo GetCardInfoFromData(string cardID, byte[] data)
        {
            try
            {
                if (IsValidData(data))
                {

                    CardInfo info = new CardInfo();
                    info.CardID = cardID;

                    SetCardInfo(info, data, false);

                    return info;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// 从卡片扇区数据中获取并设置卡片信息
        /// </summary>
        /// <param name="card">卡片实体类</param>
        /// <param name="data">扇区信息</param>
        /// <param name="keepStatus">是否保持卡片实体类卡片状态，不从卡片扇区数据中获取卡片状态</param>
        /// <returns></returns>
        public bool SetCardInfoFromData(CardInfo card, byte[] data, bool keepStatus)
        {
            try
            {
                if (IsValidData(data))
                {

                    SetCardInfo(card, data, keepStatus);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }

            return false;
        }


        /// <summary>
        /// 根据卡片实体类生成卡片数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] CreateDateBytes(CardInfo info)
        {
            try
            {
                //if (info.CardVersion == 0x00) return null;//卡格式版本1-255循环使用

                int block0 = 0 * 16;
                int block1 = 1 * 16;
                int block2 = 2 * 16;

                byte[] tempbytes = new byte[4];
                byte[] data = new byte[48];

                #region 块0
                data[block0 + 0] = info.CardVersion;


                //这里需要使用CardType来判断，如使用IsMonthCard，业主卡也属于月租卡
                byte baseCardType = (byte)(info.CardType.ID & 0x0F);
                if (baseCardType == CardType.VipCard.ID) data[block0 + 1] |= 0x00;
                else if (baseCardType == CardType.OwnerCard.ID) data[block0 + 1] |= 0x01;
                else if (baseCardType == CardType.MonthRentCard.ID) data[block0 + 1] |= 0x02;
                else if (baseCardType == CardType.PrePayCard.ID) data[block0 + 1] |= 0x03;
                else if (baseCardType == CardType.TempCard.ID) data[block0 + 1] |= 0x04;
                else if (baseCardType == CardType.UserDefinedCard1.ID) data[block0 + 1] |= 0x05;
                else if (baseCardType == CardType.UserDefinedCard2.ID) data[block0 + 1] |= 0x06;
                else if (baseCardType == CardType.OperatorCard.ID) data[block0 + 1] |= 0x0E;
                else data[block0 + 1] |= 0x04;//其他设为临时卡

                data[block0 + 1] |= (byte)((info.CarType & 0x03) << 4);
                data[block0 + 1] |= 0x40;//预留，置1
                data[block0 + 1] |= info.Status == CardStatus.Enabled ? (byte)0x80 : (byte)0x00;

                data[block0 + 2] |= info.OnlineHandleWhenOfflineMode ? (byte)0x00 : (byte)0x01;
                data[block0 + 2] |= 0x02;//预留，置1
                data[block0 + 2] |= info.CanRepeatIn ? (byte)0x00 : (byte)0x04;
                data[block0 + 2] |= info.CanRepeatOut ? (byte)0x00 : (byte)0x08;
                data[block0 + 2] |= info.WithCount ? (byte)0x10 : (byte)0x00;
                data[block0 + 2] |= info.CanEnterWhenFull ? (byte)0x00 : (byte)0x20;
                data[block0 + 2] |= info.HolidayEnabled ? (byte)0x40 : (byte)0x00;
                data[block0 + 2] |= info.EnableWhenExpired ? (byte)0x00 : (byte)0x80;

                data[block0 + 3] = info.AccessID;

                if (info.ActivationDate != null && info.ActivationDate != DateTime.MaxValue)
                {
                    if (info.ActivationDate == DateTime.MinValue)
                    {
                        data[block0 + 4] = 0xFF;
                        data[block0 + 5] = 0xFF;
                        data[block0 + 6] = 0xFF;
                    }
                    else if (info.ActivationDate.Year - 2000 >= 0)
                    {
                        data[block0 + 4] = BCDConverter.IntToBCD(info.ActivationDate.Year % 100);
                        data[block0 + 5] = BCDConverter.IntToBCD(info.ActivationDate.Month);
                        data[block0 + 6] = BCDConverter.IntToBCD(info.ActivationDate.Day);
                    }
                }

                if (info.ValidDate != null && info.ValidDate != DateTime.MinValue)
                {
                    if (info.ValidDate == DateTime.MaxValue)
                    {
                        data[block0 + 7] = 0xFF;
                        data[block0 + 8] = 0xFF;
                        data[block0 + 9] = 0xFF;
                    }
                    else if (info.ValidDate.Year - 2000 >= 0)
                    {
                        data[block0 + 7] = BCDConverter.IntToBCD(info.ValidDate.Year % 100);
                        data[block0 + 8] = BCDConverter.IntToBCD(info.ValidDate.Month);
                        data[block0 + 9] = BCDConverter.IntToBCD(info.ValidDate.Day);
                    }
                }

                data[block0 + 10] = 0xFF;//预留（留作卡号等使用），默认0xFF

                data[block0 + 11] = 0xFF;//预留（留作卡号等使用），默认0xFF

                data[block0 + 12] = 0xFF;//预留（留作卡号等使用），默认0xFF

                data[block0 + 13] = 0xFF;//预留（留作卡号等使用），默认0xFF

                data[block0 + 14] = 0xFF;//预留（留作卡号等使用），默认0xFF

                data[block0 + 15] = GetDataLRC(data, 0);
                #endregion

                #region 块1
                data[block1 + 0] |= (byte)((info.ParkingStatus & ParkingStatus.In) == ParkingStatus.In ? 0x00 : 0x01);
                data[block1 + 0] |= (byte)((info.ParkingStatus & ParkingStatus.IndoorIn) == ParkingStatus.IndoorIn ? 0x00 : 0x02);
                data[block1 + 0] |= (byte)((info.ParkingStatus & ParkingStatus.PaidBill) == ParkingStatus.PaidBill ? 0x00 : 0x08);
                data[block1 + 0] |= (byte)((info.ParkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked ? 0x00 : 0x40);
                data[block1 + 0] |= 0xB4;//其余置1

                if (info.LastDateTime < FromDate) return null;//如果刷卡时间小于起始时间，返回空值
                tempbytes = SEBinaryConverter.UintToBytes((uint)(info.LastDateTime - FromDate).TotalSeconds);
                data[block1 + 1] = tempbytes[0];
                data[block1 + 2] = tempbytes[1];
                data[block1 + 3] = tempbytes[2];
                data[block1 + 4] = tempbytes[3];

                if (info.PaidDateTime.HasValue)
                {
                    if (info.PaidDateTime.Value < FromDate) return null;//如果缴费时间小于起始时间，返回空值
                    tempbytes = SEBinaryConverter.UintToBytes((uint)(info.PaidDateTime.Value - FromDate).TotalSeconds);
                    data[block1 + 5] = tempbytes[0];
                    data[block1 + 6] = tempbytes[1];
                    data[block1 + 7] = tempbytes[2];
                    data[block1 + 8] = tempbytes[3];
                }

                if (info.ParkFee > 167772.15M) return null;//如果余额大于最大金额167772.15元，返回空值
                tempbytes = SEBinaryConverter.IntToBytes((int)(info.ParkFee * 100));
                data[block1 + 9] = tempbytes[0];
                data[block1 + 10] = tempbytes[1];
                data[block1 + 11] = tempbytes[2];

                //if (info.TotalFee > 167772.15M) return null;//如果余额大于最大金额167772.15元，返回空值
                //tempbytes = SEBinaryConverter.IntToBytes((int)info.TotalFee * 100);
                if (info.TotalPaidFee > 167772.15M) return null;//如果已缴费用大于最大金额167772.15元，返回空值
                tempbytes = SEBinaryConverter.IntToBytes((int)(info.TotalPaidFee * 100));
                data[block1 + 12] = tempbytes[0];
                data[block1 + 13] = tempbytes[1];
                data[block1 + 14] = tempbytes[2];

                data[block1 + 15] = GetDataLRC(data, 1);
                #endregion

                #region 块2
                if (info.Balance > 167772.15M) return null;//如果余额大于最大金额167772.15元，返回空值

                tempbytes = SEBinaryConverter.IntToBytes((int)(info.Balance * 100));
                data[block2 + 0] = tempbytes[0];
                data[block2 + 1] = tempbytes[1];
                data[block2 + 2] = tempbytes[2];

                data[block2 + 3] = 0xFF;//预留，默认0xFF

                data[block2 + 4] = 0xFF;//预留，默认0xFF

                data[block2 + 5] = 0xFF;//预留，默认0xFF

                if (!string.IsNullOrEmpty(info.CarPlate))
                {
                    tempbytes = Encoding.GetEncoding("gb2312").GetBytes(info.CarPlate);
                    if (tempbytes.Length > 9)//最多9字节
                    {
                        Array.Resize(ref tempbytes, 9);
                    }
                    Array.Copy(tempbytes, 0, data, block2 + 6, tempbytes.Length);//9字节，不够9字节用0x00填充
                }

                data[block2 + 15] = GetDataLRC(data, 2);
                #endregion

                return data;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 生成充值后卡片的数据数组
        /// </summary>
        /// <param name="info">卡片</param>
        /// <param name="chargeAmount">充值金额</param>
        /// <param name="validDate">新有效期</param>
        /// <returns></returns>
        public byte[] CreateDateBytesWithCharge(CardInfo info, Decimal chargeAmount, DateTime validDate)
        {
            CardInfo card = info.Clone();
            card.Charge(chargeAmount);
            card.ValidDate = validDate;
            return CreateDateBytes(card);
        }

        /// <summary>
        /// 将卡片2的卡片数据复制到卡片1中
        /// </summary>
        /// <param name="card1">需复制卡片数据的卡片</param>
        /// <param name="card2">复制卡片数据的卡片</param>
        /// <returns></returns>
        public bool CopyCardDataToCard(CardInfo card1, CardInfo card2)
        {
            if (card1 == null || card2 == null) return false;

            #region 复制块0数据
            card1.CardVersion = card2.CardVersion;
            //只有card1或card2状态可用时才复制数据，当card1和card2状态都不可用时，保持card1状态，因为card2没有具体的不可用说明
            card1.Status = card1.Status == CardStatus.Enabled || card2.Status == CardStatus.Enabled ? card2.Status : card1.Status;
            card1.CardType = card2.CardType;
            card1.OnlineHandleWhenOfflineMode = card2.OnlineHandleWhenOfflineMode;
            card1.CanRepeatIn = card2.CanRepeatIn;
            card1.CanRepeatOut = card2.CanRepeatOut;
            card1.WithCount = card2.WithCount;
            card1.CanEnterWhenFull = card2.CanEnterWhenFull;
            card1.HolidayEnabled = card2.HolidayEnabled;
            card1.EnableWhenExpired = card2.EnableWhenExpired;
            card1.AccessID = card2.AccessID;
            card1.ActivationDate = card2.ActivationDate;
            card1.ValidDate = card2.ValidDate;
            #endregion

            #region 复制块1数据
            card1.ParkingStatus = card2.ParkingStatus;
            card1.LastDateTime = card2.LastDateTime;
            card1.PaidDateTime = card2.PaidDateTime;
            card1.ParkFee = card2.ParkFee;
            //card1.TotalFee = card2.TotalFee;
            card1.TotalPaidFee = card2.TotalPaidFee;
            #endregion

            #region 复制块2数据
            card1.Balance = card2.Balance;
            card1.CarPlate = card2.CarPlate;
            card1.LastCarPlate = card2.LastCarPlate;
            #endregion

            return true;
        }

        /// <summary>
        /// 将卡片2的缴费数据复制到卡片1中
        /// </summary>
        /// <param name="card1">需复制卡片数据的卡片</param>
        /// <param name="card2">复制卡片数据的卡片</param>
        /// <returns></returns>
        public bool CopyPaidDataToCard(CardInfo card1, CardInfo card2)
        {
            if (card1 == null || card2 == null) return false;

            if (!card1.CardType.IsManagedCard || !card2.CardType.IsManagedCard) card1.CardType = card2.CardType;//非免费卡时复制
            card1.CarType = card2.CarType;
            card1.Balance = card2.Balance;
            card1.ParkingStatus = card2.ParkingStatus;
            card1.LastDateTime = card2.LastDateTime;
            card1.PaidDateTime = card2.PaidDateTime;
            card1.ParkFee = card2.ParkFee;
            //card1.TotalFee = card2.TotalFee;
            card1.TotalPaidFee = card2.TotalPaidFee;

            return true;
        }

        #endregion
    }
}
