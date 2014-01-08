using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Security;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示目前系统的密钥设置
    /// </summary>
    [DataContract]
    public class KeySetting
    {
        #region 静态属性
        /// <summary>
        /// 系统当前密钥设置
        /// </summary>
        public static KeySetting Current { get; set; }
        #endregion

        #region 构造函数
        public KeySetting()
        {
        }
        #endregion

        #region 私有变量
        /// <summary>
        /// 存储在数据库中的停车场密钥值，经过加密
        /// </summary>
        [DataMember]
        private byte[] _ParkingKey;
        /// <summary>
        /// 存储停车场使用的卡片扇区，为0时表示没有设置
        /// </summary>
        [DataMember]
        private byte _ParkingSection;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场密钥值
        /// </summary>
        public byte[] ParkingKey
        {
            get
            {
                if (ParkingKeyIsValid)
                {
                    return GetParkingKey();
                }
                else
                {
                    //如果没有设置密钥，使用默认的停车密钥
                    return GlobalVariables.DefaultParkingKey;
                }
            }
            set
            {
                if (value.Length == 6)
                {
                    SetParkingKey(value);
                }
            }
        }
        /// <summary>
        /// 获取或设置停车场读写卡片的扇区，默认为扇区2
        /// </summary>
        public byte ParkingSection
        {
            get
            {
                if (_ParkingSection == 0) _ParkingSection = 2;
                return _ParkingSection;
            }
            set
            {
                if (value > 0 && value < 40)
                {
                    _ParkingSection = value;
                }
            }
        }
        #endregion

        #region 私有方法
        private byte[] GetParkingKey()
        {
            return _ParkingKey;
            //以下为解密的方法
            //byte[] key = new byte[8];
            //key = CryptoTripleDes.CreateDescryptByte(GlobalVariables.KeyFactor, _ParkingKey);
            //Array.Resize(ref key, 6);
            //return key;
        }
        private void SetParkingKey(byte[] key)
        {
            _ParkingKey = key;
            //以下为加密的方法
            //Array.Resize(ref key, 8);
            //_ParkingKey = CryptoTripleDes.CreateEncryptByte(GlobalVariables.KeyFactor, key);
        }
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 停车场密钥是否有效
        /// </summary>
        public bool ParkingKeyIsValid
        {
            get
            {
                return _ParkingKey != null && _ParkingKey.Length == 6;
                //使用加解密时，3Des加解密需要8字节
                //return _ParkingKey != null && _ParkingKey.Length == 8;
            }
        }
        #endregion

        #region 公共方法
        public void ClearKey()
        {
            _ParkingKey = null;
        }
        #endregion
    }
}
