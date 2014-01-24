using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Parking.POS.Model;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.UI
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

        private DateTime ReadDate(byte[] data)
        {
            if (data.Length == 3)
            {
                if (data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x00)
                {
                    return FromDate;
                }
                else if (data[0] == 0xFF && data[1] == 0xFF && data[2] == 0xFF)
                {
                    return FromDate;
                }
                else
                {
                    return new DateTime(2000 + BCDConverter.BCDtoInt(data[0]), BCDConverter.BCDtoInt(data[1]), BCDConverter.BCDtoInt(data[2]));
                }
            }
            else
            {
                throw new InvalidCastException("指定的转换无效，字节数组必需为三个字节");
            }
        }


        private CardType GetCardType(byte b)
        {
            switch (b)
            {
                case 0x00:
                    return CardType.VipCard;
                case 0x01:
                    return CardType.OwnerCard;
                case 0x02:
                    return CardType.MonthRentCard;
                case 0x03:
                    return CardType.PrePayCard;
                case 0x04:
                    return CardType.TempCard;
                case 0x05:
                    return CardType.UserDefinedCard1;
                case 0x06:
                    return CardType.UserDefinedCard2;
                case 0x0E:
                    return CardType.OperatorCard;
                default:
                    return CardType.TempCard;
            }
        }

        private byte GetByte(CardType c)
        {
            if (c == CardType.VipCard) return 0x00;
            else if (c == CardType.OwnerCard) return 0x01;
            else if (c == CardType.MonthRentCard) return 0x02;
            else if (c == CardType.PrePayCard) return 0x03;
            else if (c == CardType.TempCard) return 0x04;
            else if (c == CardType.UserDefinedCard1) return 0x05;
            else if (c == CardType.UserDefinedCard2) return 0x06;
            else if (c == CardType.OperatorCard) return 0x0E;
            else return 0x04;//其他设为临时卡
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 卡片数据是否有效，检验数据为一个扇区的数据
        /// </summary>
        /// <param name="data">扇区数据，48字节，3个块</param>
        /// <returns></returns>
        public bool IsValidData(byte[] data)
        {
            if (data == null || data.Length != 48) return false;//0字节为卡格式版本，1-255循环使用

            for (int i = 0; i < 3; i++)
            {
                if (data[i * 16 + 15] != GetDataLRC(data, i)) return false;
            }
            return true;
        }

        /// <summary>
        /// 从卡片扇区数据中获取并设置卡片信息
        /// </summary>
        /// <param name="card">卡片实体类</param>
        /// <param name="data">扇区信息</param>
        /// <param name="keepStatus">是否保持卡片实体类卡片状态，不从卡片扇区数据中获取卡片状态</param>
        /// <returns></returns>
        public CardInfo GetCardInfoFromData(byte[] data)
        {
            CardInfo card = null;
            try
            {
                if (IsValidData(data))
                {
                    card = new CardInfo();
                    #region 块0
                    byte[] block0 = new byte[16];
                    Array.Copy(data, 0, block0, 0, block0.Length);
                    card.CardVersion = block0[0];

                    card.Status = (block0[1] & 0x80) == 0x80 ? CardStatus.Enabled : CardStatus.Disabled;
                    card.CardType = GetCardType((byte)(block0[1] & 0x0F));
                    card.CarType = (byte)((block0[1] >> 4) & 0x03);

                    card.OnlineHandleWhenOfflineMode = (block0[2] & 0x01) != 0x01;
                    card.CanRepeatIn = (block0[2] & 0x04) != 0x04;
                    card.CanRepeatOut = (block0[2] & 0x08) != 0x08;
                    card.WithCount = (block0[2] & 0x10) == 0x10;
                    card.CanEnterWhenFull = (block0[2] & 0x20) != 0x20;
                    card.HolidayEnabled = (block0[2] & 0x40) == 0x40;
                    card.EnableWhenExpired = (block0[2] & 0x80) != 0x80;

                    card.AccessID = block0[3];
                    card.ActivationDate = ReadDate(new byte[] { block0[4], block0[5], block0[6] });
                    card.ValidDate = ReadDate(new byte[] { block0[7], block0[8], block0[9] });
                    #endregion

                    #region 块1
                    byte[] block1 = new byte[16];
                    Array.Copy(data, 16, block1, 0, block1.Length);

                    card.ParkingStatus |= (block1[0] & 0x01) != 0x01 ? ParkingStatus.In : 0;
                    card.ParkingStatus |= (block1[0] & 0x02) != 0x02 ? ParkingStatus.IndoorIn : 0;
                    card.ParkingStatus |= (block1[0] & 0x08) != 0x08 ? ParkingStatus.PaidBill : 0;
                    card.ParkingStatus |= (block1[0] & 0x40) != 0x40 ? ParkingStatus.NestedParkMarked : 0;

                    long seconds = SEBinaryConverter.BytesToLong(new byte[] { block1[1], block1[2], block1[3], block1[4] });
                    if (seconds > 0) card.LastDateTime = FromDate.AddSeconds(seconds);

                    seconds = SEBinaryConverter.BytesToLong(new byte[] { block1[5], block1[6], block1[7], block1[8] });
                    if (seconds > 0) card.PaidDateTime = FromDate.AddSeconds(seconds);

                    card.ParkFee = SEBinaryConverter.BytesToInt(new byte[] { block1[9], block1[10], block1[11] }) / 100.00M;
                    card.TotalPaidFee = SEBinaryConverter.BytesToInt(new byte[] { block1[12], block1[13], block1[14] }) / 100.00M;
                    #endregion

                    #region 块2
                    byte[] block2 = new byte[16];
                    Array.Copy(data, 32, block2, 0, block2.Length);
                    card.Balance = SEBinaryConverter.BytesToInt(new byte[] { block2[0], block2[1], block2[2] }) / 100.00M;

                    if (block2[3] == 0x00 && block2[4] == 0x00 && block2[5] == 0x00)
                    {
                        //免费时间点无效
                    }
                    else
                    {
                        card.FreeDateTime = FromDate.AddMinutes(SEBinaryConverter.BytesToInt(new byte[] { block2[3], block2[4], block2[5], 0x0 }));
                    }

                    byte[] tempbytes = new byte[9];
                    Array.Copy(block2, 6, tempbytes, 0, 9);
                    if (BytesIsAllZero(tempbytes))
                    {
                        card.CarPlate = string.Empty;
                    }
                    else
                    {
                        card.CarPlate = Encoding.GetEncoding("gb2312").GetString(tempbytes, 0, tempbytes.Length);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return card;
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
                int block0 = 0 * 16;
                int block1 = 1 * 16;
                int block2 = 2 * 16;

                byte[] tempbytes = new byte[4];
                byte[] data = new byte[48];

                #region 块0
                data[block0 + 0] = info.CardVersion;


                //这里需要使用CardType来判断，如使用IsMonthCard，业主卡也属于月租卡
                data[block0 + 1] |= GetByte(info.CardType);
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

                if (info.FreeDateTime.HasValue)
                {
                    if (info.FreeDateTime.Value < FromDate) return null;//如果免费时间点小于起始时间，返回空值
                    TimeSpan ts = info.FreeDateTime.Value - FromDate;
                    if (ts.TotalMinutes < 0xFFFFFF)
                    {
                        tempbytes = SEBinaryConverter.IntToBytes((int)ts.TotalMinutes);
                        data[block2 + 3] = tempbytes[0];
                        data[block2 + 4] = tempbytes[1];
                        data[block2 + 5] = tempbytes[2];
                    }
                    else
                    {
                        //超过最大值时设为最大值
                        data[block2 + 3] = 0xFF;
                        data[block2 + 4] = 0xFF;
                        data[block2 + 5] = 0xFF;
                    }
                }

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
                ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}
