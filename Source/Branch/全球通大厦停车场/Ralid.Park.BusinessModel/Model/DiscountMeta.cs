using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 优惠信息元数据
    /// </summary>
    public class DiscountMeta
    {
        #region 构造函数
        public DiscountMeta(byte[] metaData)
        {
            _MetaData = metaData;
        }
        #endregion

        #region 私有变量
        private byte[] _MetaData;
        #endregion

        #region 私有方法
        /**************************************************
         * 校验码生成采用ISO 7064:1983.MOD 11-2校验码计算法
         * 按照中华人民共和国国家标准GB11643-1999规定中华人民共和国公民身份号码校验码的计算方法即为ISO 7064:1983.MOD 11-2校验码计算法。 
         * 具体生成过程如下：
         * 假设某一17位数字是 　　
         * 17位数字 1 2 3  4 5 6 7 8 9 0 1 2 3  4 5 6 7 
         * 加权因子 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2　　
         * 计算17位数字各位数字与对应的加权因子的乘积的和
         * S：1×7+2×9+3×10+4×5+5×8+6×4+7×2+8×1+9×6+0×3+1×7+2×9+3×10+4×5+5×8+6×4+7×2=368； 
         * 计算S÷11的余数T：368 mod 11=5； 计算（12-T）÷11的余数R；
         * 如果R=10，校验码为字母“X”(由于字节为十六进制，所以函数设定为如果R=10，校验码为10的十六进制，即为“A”)；
         * 如果R≠10，校验码为数字“R”：（12-5）mod 11=7。 
         * 该17位数字的校验码就是7，聚合在一为123456789012345677。
        ******************************************************/
        /// <summary>
        /// 校验码生成函数
        /// </summary>
        /// <param name="sourcebyte">要生成校验码的字节组</param>
        /// <returns>int,用于赋值字节组的第十六个字节</returns>
        private static int checkcode(byte[] sourcebyte)
        {
            int[] weight = { 4, 5, 2, 10, 9, 4, 5, 6, 8, 3, 5, 6, 8, 1, 7 };
            int backcode;
            int sum = 0;
            int remainder;
            for (int i = 0; i < sourcebyte.Length - 1; i++)
            {
                sum += Convert.ToInt32(sourcebyte[i].ToString()) * weight[i];
            }

            if (sum == 0)
            {
                backcode = 0;
            }
            else
            {
                remainder = sum % 11;
                backcode = (12 - remainder) % 11;
            }
            return backcode;
        }

        /// <summary>
        /// 返回记录数：0,1,2,3,3表示已满
        /// </summary>
        /// <param name="cardbyte">块数据</param>
        /// <returns></returns>
        private static int recordNum(byte[] cardbyte)
        {
            int i = 0;
            while (i < 3)
            {
                if (Convert.ToInt32(cardbyte[i * 5])
                    + Convert.ToInt32(cardbyte[i * 5 + 1])
                    + Convert.ToInt32(cardbyte[i * 5 + 2])
                    + Convert.ToInt32(cardbyte[i * 5 + 3])
                    + Convert.ToInt32(cardbyte[i * 5 + 4]) == 0)
                {
                    break;
                }
                i++;
            }
            return i;
        }
        #endregion

        #region 公共方法
        public int Discount
        {
            get
            {
                int totaldiscount = 0;
                byte[] readbytes = new byte[16];
                Array.Copy(_MetaData, readbytes, 16);
                int code = checkcode(readbytes);
                if (code != int.Parse(readbytes[15].ToString()))
                {
                    totaldiscount = -1;
                }
                else
                {
                    totaldiscount = int.Parse(readbytes[0].ToString());
                }
                return totaldiscount;
            }
        }

        public List<List<string>> DiscountRecord
        {
            get
            {
                List<List<string>> discountrecord = new List<List<string>>();
                for (int j = 1; j < 3; j++)
                {
                    byte[] readbytes = new byte[16];
                    Array.Copy(_MetaData, 16 * j, readbytes, 0, 16);
                    int code = checkcode(readbytes);
                    if (code == int.Parse(readbytes[15].ToString()))
                    {
                        DateTime baseTime = DateTime.Parse("2000-1-1 00:00:00");
                        int minutes = 0;
                        List<string> dreord = new List<string>();
                        for (int i = 0; i < recordNum(readbytes); i++)
                        {
                            baseTime = DateTime.Parse("2000-1-1 00:00:00");
                            dreord = new List<string>();
                            dreord.Add(Convert.ToInt32(readbytes[i * 5]).ToString());
                            dreord.Add(Convert.ToInt32(readbytes[i * 5 + 1]).ToString());
                            minutes = Convert.ToInt32(readbytes[i * 5 + 2]) * 256 * 256;
                            minutes += Convert.ToInt32(readbytes[i * 5 + 3]) * 256;
                            minutes += Convert.ToInt32(readbytes[i * 5 + 4]);
                            baseTime = baseTime.AddMinutes(double.Parse(minutes.ToString()));
                            baseTime = baseTime.AddSeconds(double.Parse(i.ToString()));
                            dreord.Add(baseTime.ToString());
                            discountrecord.Add(dreord);
                        }
                    }
                }
                return discountrecord;
            }
        }
        #endregion
    }
}
