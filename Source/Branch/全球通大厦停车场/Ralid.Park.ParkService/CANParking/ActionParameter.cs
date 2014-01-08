using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.ParkService.CANParking
{
    class ActionParameter
    {
        private byte _ActionParameter = 0;

        public ActionParameter(byte actionParameter)
        {
            _ActionParameter = actionParameter;
        }

        /// <summary>
        /// 获取卡片类型
        /// </summary>
        public CardType CardType
        {
            get
            {
                byte lFourBit = (byte)(_ActionParameter & 0x0f); //低四位表示卡片类型
                return (CardType)(lFourBit);
            }
        }

        /// <summary>
        /// 获取计费车型
        /// </summary>
        public byte CarType
        {
            get
            {
                byte hFourBit = (byte)(_ActionParameter / 0x0f); //高四位表示16种收费类型
                return (byte)(hFourBit % 4);
            }
        }

        /// <summary>
        /// 获取计费类型,分为普通收费，节假日收费，室内收费和室内节假日收费四种
        /// </summary>
        public TariffType TariffType
        {
            get
            {
                byte hFourBit = (byte)(_ActionParameter / 0x0f); //高四位表示16种收费类型
                switch (hFourBit / 4)
                {
                    case 0:
                        return TariffType.Normal;
                    case 1:
                        return TariffType.Holiday;
                    case 2:
                        return TariffType.InnerRoom;
                    case 3:
                        return TariffType.HolidayAndInnerRoom;
                    default:
                        return TariffType.Normal;
                }
            }
        }
    }
}
