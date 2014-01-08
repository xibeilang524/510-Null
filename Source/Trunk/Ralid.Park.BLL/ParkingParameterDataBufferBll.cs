using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 表示停车场参数设置缓存逻辑处理类
    /// </summary>
    public class ParkingParameterDataBufferBll
    {
        #region 构造函数
        public ParkingParameterDataBufferBll(string repoUri)
        {
            _repoUri = repoUri;
        }
        #endregion

        #region 私有变量
        private string _repoUri;
        #endregion

        #region 公共方法
        /// <summary>
        /// 导出停车场参数设置为xml文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public CommandResult ExportParameter(string path)
        {
            CommandResult result = null;

            try
            {
                OperatorBll obll = new OperatorBll(_repoUri);
                QueryResultList<OperatorInfo> operators = obll.GetAllOperators();

                WorkstationBll wbll = new WorkstationBll(_repoUri);
                QueryResultList<WorkStationInfo> workstations = wbll.GetAllWorkstations();

                APMBll abll = new APMBll(_repoUri);
                QueryResultList<APM> apms = abll.GetAllItems();

                if (operators.Result == ResultCode.Successful
                    && workstations.Result == ResultCode.Successful
                    && apms.Result == ResultCode.Successful)
                {
                    ParkingParameterDataBuffer parameter = new ParkingParameterDataBuffer();
                    parameter.UserSetting = UserSetting.Current;
                    parameter.HolidaySetting = HolidaySetting.Current;
                    parameter.AccessSetting = AccessSetting.Current;
                    parameter.TariffSetting = TariffSetting.Current;
                    parameter.CarTypeSetting = CarTypeSetting.Current;
                    parameter.CustomCardTypeSetting = CustomCardTypeSetting.Current;
                    parameter.BaseCardTypeSetting = BaseCardTypeSetting.Current;
                    parameter.KeySetting = KeySetting.Current;
                    parameter.Operators = operators.QueryObjects;
                    parameter.WorkStations = workstations.QueryObjects;
                    parameter.APMs = apms.QueryObjects;

                    DataContractSerializer ser = new DataContractSerializer(typeof(ParkingParameterDataBuffer));
                    using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        ser.WriteObject(stream, parameter);
                    }
                    result = new CommandResult(ResultCode.Successful);
                }
                else
                {
                    result = new CommandResult(ResultCode.Fail, "获取系统参数设置失败！");
                }
            }
            catch (Exception ex)
            {
                result = new CommandResult(ResultCode.Fail, ex.Message);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// 从xml文件中导入停车场参数设置
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public QueryResult<ParkingParameterDataBuffer> ImportParameter(string path)
        {
            QueryResult<ParkingParameterDataBuffer> result = new QueryResult<ParkingParameterDataBuffer>();
            try
            {
                if (!File.Exists(path))
                {
                    result.Result = ResultCode.Fail;
                    result.Message = "停车场参数文件不存在！";
                    return result;
                }

                DataContractSerializer ser = new DataContractSerializer(typeof(ParkingParameterDataBuffer));
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    result.QueryObject = ser.ReadObject(stream) as ParkingParameterDataBuffer;
                }
                result.Result = ResultCode.Successful;
            }
            catch (Exception ex)
            {
                result.Result=ResultCode.Fail;
                result.Message=ex.Message;
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }
        #endregion
    }
}
