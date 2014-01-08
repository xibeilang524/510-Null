using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一个卡片读卡写卡等操作的管理类
    /// </summary>
    public class CardOperationManager
    {
        #region 构造函数
        public CardOperationManager()
        { 
        }
        #endregion

        #region 静态属性
        private static CardOperationManager _instance;

        /// <summary>
        /// 获取实例
        /// </summary>
        public static CardOperationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardOperationManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 私有方法
        #endregion

        #region 公共方法
        /// <summary>
        /// 显示卡片操作结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool ShowResultMessage(CardOperationResultCode result)
        {
            switch (result)
            {
                case CardOperationResultCode.Success:
                    return true;
                case CardOperationResultCode.NoCard:
                    MessageBox.Show(Resouce.Resource1.CardOperationManager_NoCard);
                    return false;
                case CardOperationResultCode.CardIDError:
                    MessageBox.Show(Resouce.Resource1.CardOperationManager_CardIDError);
                    return false;
                case CardOperationResultCode.InitKeyFail:                    
                    MessageBox.Show(Resouce.Resource1.CardOperationManager_InitKeyFail);
                    return false;
                default:
                    MessageBox.Show(Resouce.Resource1.CardOperationManager_CardInvalid);
                    return false;
            }
        }
        /// <summary>
        /// 初始化卡片
        /// </summary>
        /// <param name="cardID">卡片卡号（为空时不检查卡号是否一致）</param>
        /// <returns></returns>
        public CardOperationResultCode InitCard(string cardID)
        {
            try
            {
                //修改扇区2密钥
                //return CardReaderManager.GetInstance(UserSetting.Current.WegenType).SetSectionKey(cardID, (int)ICCardSection.Parking, false, true);
                return CardReaderManager.GetInstance(UserSetting.Current.WegenType).SetSectionKey(cardID, (int)ICCardSection.Parking, GlobalVariables.DefaultKey, GlobalVariables.ParkingKey, false, true);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

        /// <summary>
        /// 初始化卡片
        /// </summary>
        /// <returns></returns>
        public CardOperationResultCode InitCard()
        {
            return InitCard(null);
        }

        /// <summary>
        /// 将卡片信息写入卡片扇区
        /// </summary>
        /// <param name="card">卡片实体类</param>
        /// <returns></returns>
        public CardOperationResultCode WriteCard(CardInfo card)
        {
            try
            {
                if (card != null)
                {
                    byte[] data = CardDateResolver.Instance.CreateDateBytes(card);
                    if (data != null)
                    {
                        return CardReaderManager.GetInstance(UserSetting.Current.WegenType).WriteSection(card.CardID, (int)ICCardSection.Parking, 0, 3, data, GlobalVariables.ParkingKey, true, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

        /// <summary>
        /// 将卡片充值后的信息写入卡片扇区，充值金额只会生成数据，不会充值到card实体类中
        /// </summary>
        /// <param name="card">卡片实体类</param>
        /// <param name="chargeAmount">充值金额</param>
        /// <param name="validDate">新有效日期</param>
        /// <returns></returns>
        public CardOperationResultCode WriteCardWithCharge(CardInfo card, Decimal chargeAmount, DateTime validDate)
        {
            try
            {
                if (card != null)
                {
                    byte[] data = CardDateResolver.Instance.CreateDateBytesWithCharge(card, chargeAmount, validDate);
                    if (data != null)
                    {
                        return CardReaderManager.GetInstance(UserSetting.Current.WegenType).WriteSection(card.CardID, (int)ICCardSection.Parking, 0, 3, data, GlobalVariables.ParkingKey, true, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

        /// <summary>
        /// 循环将卡片信息写入卡片扇区，直到写入成功或取消
        /// </summary>
        /// <param name="card"></param>
        public CardOperationResultCode WriteCardLoop(CardInfo card)
        {
            return WriteCardLoop(card, false);
        }

        /// <summary>
        /// 循环将卡片信息写入卡片扇区，直到写入成功或取消
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="initKey">密钥验证失败后，是否初始化密钥</param>
        /// <returns></returns>
        public CardOperationResultCode WriteCardLoop(CardInfo card,bool initKey)
        {
            try
            {
                if (card != null)
                {
                    byte[] data = CardDateResolver.Instance.CreateDateBytes(card);
                    if (data != null)
                    {
                        return CardReaderManager.GetInstance(UserSetting.Current.WegenType).WriteSection(card.CardID, (int)ICCardSection.Parking, 0, 3, data, GlobalVariables.ParkingKey, true, true, true, initKey);
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }

        /// <summary>
        /// 恢复卡片数据
        /// </summary>
        /// <param name="cardID">卡片号（为空时不检查卡号是否一致）</param>
        /// <param name="data">恢复到卡片的数据</param>
        /// <returns></returns>
        public CardOperationResultCode RecoverCard(string cardID,byte[] data)
        {
            try
            {
                if (data != null && data.Length == 48)
                {
                    return CardReaderManager.GetInstance(UserSetting.Current.WegenType).WriteSection(cardID, (int)ICCardSection.Parking, 0, 3, data, GlobalVariables.ParkingKey, true, true);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return CardOperationResultCode.Fail;
        }


        /// <summary>
        /// 检查卡片有效性
        /// </summary>
        /// <param name="cardID">卡号（为空时不检查卡号是否一致）</param>
        /// <returns></returns>
        public CardOperationResultCode CheckCard(string cardID)
        {
            ReadCardResult result = CardReaderManager.GetInstance(UserSetting.Current.WegenType).ReadSection(cardID, (int)ICCardSection.Parking, 0, 3, GlobalVariables.ParkingKey, false, false, false);

            return result.ResultCode;
        } 

        /// <summary>
        /// 检查卡片有效性并返回提示信息
        /// </summary>
        /// <param name="cardID">卡号（为空时不检查卡号是否一致）</param>
        /// <returns></returns>
        public bool CheckCardWithMessage(string cardID)
        {
            CardOperationResultCode result = CheckCard(cardID);

            return ShowResultMessage(result);
        }

        /// <summary>
        /// 检查读到的卡号与检验卡号是否一致
        /// </summary>
        /// <param name="verifyID">检验卡号</param>
        /// <param name="readID">读到的卡号</param>
        /// <returns></returns>
        public bool CheckReadCardIDWithMessage(string verifyID, string readID)
        {
            if (verifyID != readID)
            {
                MessageBox.Show(Resouce.Resource1.CardOperationManager_NoCurrentCard);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查读取数据是否有效
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CheckReadDateWithMessage(byte[] data)
        {
            if (data == null)
            {
                MessageBox.Show(Resouce.Resource1.CardOperationManager_NoCardData);
                return false;
            }
            else if (!CardDateResolver.Instance.IsValidData(data))
            {
                MessageBox.Show(Resouce.Resource1.CardOperationManager_CardDataError);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取卡片
        /// </summary>
        /// <param name="cardID">卡号（为空时不检查卡号是否一致）</param>
        /// <returns></returns>
        public CardInfo ReadCardWithMessage(string cardID)
        {
            CardInfo card = null;
            ReadCardResult result = CardReaderManager.GetInstance(UserSetting.Current.WegenType).ReadSection(cardID, (int)ICCardSection.Parking, 0, 3, GlobalVariables.ParkingKey, true, true, false);

            if (ShowResultMessage(result.ResultCode))
            {
                card = CardDateResolver.Instance.GetCardInfoFromData(result.CardID, result.ParkingDate);
            }
            return card;
        }


        #endregion

        #region 测试
        /// <summary>
        /// 进场
        /// </summary>
        /// <param name="indoor">是否进内车场</param>
        public void Enter(bool indoor)
        {
            CardReaderManager manager = CardReaderManager.GetInstance(UserSetting.Current.WegenType);
            ReadCardResult result = manager.ReadSection(string.Empty, 2, 0, 3, GlobalVariables.ParkingKey, true, true, false);
            if (result.ResultCode == CardOperationResultCode.Success)
            {
                CardInfo card = CardDateResolver.Instance.GetCardInfoFromData(result.CardID, result.ParkingDate);
                if (card != null)
                {
                    Enter(card, indoor);
                }
            }
        }

        /// <summary>
        /// 进场
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="indoor">是否进内车场</param>
        public void Enter(CardInfo card, bool indoor)
        {
            card.LastDateTime = new DateTime(2013, 5, 16, 10, 13, 10);
            card.ParkingStatus = ParkingStatus.In;
            if (indoor) card.ParkingStatus |= ParkingStatus.NestedParkMarked;
            card.ParkFee = 0;
            //card.TotalFee = 0;
            card.TotalPaidFee = 0;

            WriteCardLoop(card);
        }
        #endregion
    }
}
