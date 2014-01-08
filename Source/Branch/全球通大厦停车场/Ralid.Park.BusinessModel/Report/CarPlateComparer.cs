using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 车牌比较器
    /// </summary>
    public class CarPlateComparer
    {
        /// <summary>
        /// 车牌相似度对比
        /// </summary>
        /// <param name="carPlate1">待比对的车牌号</param>
        /// <param name="carPlate2">参照的车牌号</param>
        /// <param name="maxCarPlateErrorChar">车牌比对的误差，即最多允许车牌中有多少个字符不一样</param>
        /// <returns>相似度符合则返回真，否则返回假</returns>
        public static bool CarPlateComparison(string carPlate1, string carPlate2, int maxCarPlateErrorChar)
        {
            bool success = false;
            int errChar = 0;
            if (!string.IsNullOrEmpty(carPlate1) && !string.IsNullOrEmpty(carPlate2))
            {
                if (carPlate1 == carPlate2)
                {
                    success = true;
                }
                else
                {
                    int len = carPlate1.Length > carPlate2.Length ? carPlate1.Length : carPlate2.Length;
                    for (int i = 0; i < len; i++)
                    {
                        if (carPlate2.Length > i && carPlate1.Length > i)
                        {
                            if (carPlate1[i] != carPlate2[i])
                            {
                                errChar++;
                            }
                        }
                        else
                        {
                            errChar++;
                        }
                    }
                    success = (errChar <= maxCarPlateErrorChar);
                }
            }
            return success;
        }
    }
}
