using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.BusinessModel .Report ;
using Ralid.BusinessModel .Model ;
using Ralid.BusinessModel.Enum;

namespace Ralid.BusinessModel.Factory
{
    public class CardEventFactory
    {
        public static CardEventDetailReport CreateEnterEvent(CardInfo card, EntranceInfo entrance)
        {
            return null;
        }

        public static CardEventDetailReport CreateExitEvent(CardInfo card, EntranceInfo entrance, TariffSetting ts)
        {
            CardEventDetailReport report = new CardEventDetailReport();
            DateTime eventDateTime = DateTime.Now;
            report.ParkID = entrance.ParkID;
            report.Address = entrance.Address;
            report.Entrance = entrance;
            report.CardType = card.CardType;
            report.CarType = card.CarType;
            if (!card.CardType.IsMonthCard)
            {
                ParkAccountsInfo parkFee = TariffSetting.Current.CalculateCardParkFee(card, card.CarType, eventDateTime);
                report.Accounts = parkFee.Accounts;
                report.TariffType = parkFee.TariffType;
            }
            report.CardID = card.CardID;
            report.EventDateTime = eventDateTime;
            report.EventStatus = 0;
            report.LastAddress = card.LastAddress;
            report.LastDateTime = card.LastDateTime;
            report.CarPlate = card.LastCarPlate;
            if (card.CardType.IsPrepayCard)
            {
                if (card.Balance < report.Accounts) //余额不足,当成临时卡收费
                {
                    report.ParkingStatus = card.ParkingStatus | ParkingStatus.BIT_AsTempCard;
                    report.Balance = card.Balance;
                }
                else
                {
                    report.ParkingStatus = card.ParkingStatus;
                    report.Balance = card.Balance - report.Accounts;
                }
            }
            else
            {
                report.ParkingStatus = card.ParkingStatus;
            }
            return report;
        }
    }
}
