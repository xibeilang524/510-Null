using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Ralid.Parking .POS .Device ;
using Ralid.Parking .POS .Model ;

namespace Ralid.Parking.POS.UI
{
    /// <summary>
    /// 表示卡片扇区信息读写的类
    /// </summary>
    public class CardInfoReader
    {
        #region 构造函数
        public CardInfoReader()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 密钥值
        /// </summary>
        public byte[] ParkingKey { get; set; }

        /// <summary>
        /// 获取停车场读写的扇区
        /// </summary>
        public byte ParkingSection { get; set; }

        /// <summary>
        /// 获取或设置读写设备
        /// </summary>
        public POSDeviceBase Device { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开读写设备
        /// </summary>
        public void OpenDevice()
        {
            if (Device != null)
            {
                Device.OpenRFID();
               // Device.buzctrl();
            }
        }
        /// <summary>
        /// 关闭读写设备
        /// </summary>
        public void CloseDevice()
        {
            if (Device != null)
            {
                Device.CloseRFID();
            }
        }
        /// <summary>
        /// 获取卡片信息
        /// </summary>
        /// <returns></returns>
        public CardInfo Read()
        {
            CardInfo card = null;
            if (Device != null && Device.IsRFIDOpened)
            {
                string cardid = Device.ReadCardID();
                if (!string.IsNullOrEmpty(cardid))
                {
                    if (ParkingKey != null && ParkingSection >= 0)
                    {
                        bool ret = false;
                        ret = Device.CheckKey(ParkingSection, ParkingKey); //验证密码
                        if (ret)
                        {
                            List<byte> data = new List<byte>();
                            for (int i = 0; i < 3; i++)
                            {
                                byte[] block = Device.ReadBlock(ParkingSection, i);
                                if (block != null)
                                {
                                    data.AddRange(block);
                                }
                            }
                            card = (new CardDateResolver()).GetCardInfoFromData(data.ToArray());
                            if (card != null) card.CardID = cardid;
                            Device.Buzz_OK();
                        }
                    }
                }
            }
            return card;
        }

        /// <summary>
        /// 写入卡片信息
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool Write(CardInfo card)
        {
            if (Device != null && Device.IsRFIDOpened)
            {
                if (card != null)
                {
                    byte[] data = (new CardDateResolver()).CreateDateBytes(card);
                    bool ret = false;
                    ret = Device.CheckKey(ParkingSection, ParkingKey); //验证密码
                    if (ret)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            byte[] blk = new byte[16];
                            Array.Copy(data, i * 16, blk, 0, 16);
                            if (!Device.WriteBlock(blk, ParkingSection, i))
                            {
                                Device.Buzz_Fail();
                                return false;
                            }
                        }
                        Device.Buzz_OK();
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
