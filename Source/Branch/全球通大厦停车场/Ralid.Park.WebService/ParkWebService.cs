using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Ralid.Park.SQLHelper;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.WebService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ParkWebService”。
    public class ParkWebService : IParkWebService
    {

        #region 私有方法
        private InterfaceReturnCode CreateInterfaceReturnCode(SQLResultCode code)
        {
            switch (code)
            {
                case SQLResultCode.Successful:
                     return InterfaceReturnCode.Success;//成功
                case SQLResultCode.CannotConnectServer:
                    return InterfaceReturnCode.CannotConnectDatabase;//连接数据库失败
                case SQLResultCode.NoRecord:
                    return InterfaceReturnCode.DatabaseNoRecord;//未找到记录
                case SQLResultCode.SaveDataError:
                    return InterfaceReturnCode.DatabaseSaveDataError;//数据写入数据库失败
                default:
                    return InterfaceReturnCode.DatabaseError;//失败
            }
        }

        private InterfaceReturnCode CreateInterfaceReturnCode(ResultCode code)
        {
            switch (code)
            {
                case ResultCode.Successful:
                    return InterfaceReturnCode.Success;//成功
                case ResultCode.CannotConnectServer:
                    return InterfaceReturnCode.CannotConnectDatabase;//连接数据库失败
                case ResultCode.NoRecord:
                    return InterfaceReturnCode.DatabaseNoRecord;//未找到记录
                case ResultCode.SaveDataError:
                    return InterfaceReturnCode.DatabaseSaveDataError;//数据写入数据库失败
                default:
                    return InterfaceReturnCode.DatabaseError;//失败
            }
        }

        /// <summary>
        /// 通过权限组名称获取权限组ID，权限组名称为空时表示所有权限，返回0，没有找到的返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetAccessID(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;

            SysParaSettingsBll sbll = new SysParaSettingsBll(AppConifg.Current.ParkingConnection);
            AccessSetting accessSetting = sbll.GetOrCreateSetting<AccessSetting>();
            AccessInfo access = null;
            if (accessSetting.Accesses != null) access = accessSetting.Accesses.FirstOrDefault(item => item.Name == name);
            if (access != null) return access.ID;

            return -1;
        }


        /// <summary>
        /// 保存车单
        /// </summary>
        /// <param name="sheetID"></param>
        /// <param name="employeeNum"></param>
        /// <param name="employeeName"></param>
        /// <param name="telPhone"></param>
        /// <param name="department"></param>
        /// <param name="carPlate"></param>
        /// <param name="status"></param>
        /// <param name="activationDate"></param>
        /// <param name="accessID"></param>
        /// <returns></returns>
        private InterfaceReturnCode _SaveSheet(string sheetID, string employeeNum, string employeeName, string telPhone, string department, string carPlate,
             byte status, string activationDate, string places, bool enableLimitation, double limitation)
        {
            DateTime activation;
            if (string.IsNullOrEmpty(employeeNum)
                || !DateTime.TryParse(activationDate, out activation))
            {
                return InterfaceReturnCode.ParameterError;
            }

            //卡片状态  = 1 Enabled 已发行, = 3 Disabled 禁用,
            byte cardStatus = (byte)(status == 0 ? 3 : 1);

            //先查询停车地点的权限组
            int accessID = GetAccessID(places);
            if (accessID == -1)
            {
                accessID = 0;
            }

            ICardProvider provider = ProviderFactory.Create<ICardProvider>(AppConifg.Current.ParkingConnection);
            CardInfo info = null;
            QueryResult<CardInfo> find = provider.GetByID(employeeNum);
            if (find.Result == ResultCode.Successful || find.Result == ResultCode.NoRecord)
            {
                if (find.QueryObject != null)
                    info = find.QueryObject.Clone();
            }
            else
            {
                return CreateInterfaceReturnCode(find.Result);
            }
            if (info == null)
            {
                info = new CardInfo();

                //以下为卡片默认设置
                info.CardType = Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard;
                info.CanRepeatOut = false;
                info.CanRepeatIn = false;
                info.HolidayEnabled = true;
                info.WithCount = true;
                info.OnlineHandleWhenOfflineMode = false;
                info.CanEnterWhenFull = true;
                info.EnableWhenExpired = true;
                info.ValidDate = new DateTime(2099, 12, 31);
            }
            info.CardID = employeeNum;
            info.OwnerName = employeeName;
            info.Telphone = telPhone;
            info.CardCertificate = department;
            info.CarPlate = carPlate;
            info.Status = (Ralid.Park.BusinessModel.Enum.CardStatus)cardStatus;
            info.ActivationDate = activation;
            info.EnableLimitation = enableLimitation;
            info.LimitationRemain = (decimal)limitation;
            info.AccessID = (byte)accessID;
            info.SheetID = sheetID;

            CommandResult result = null;
            if (find.QueryObject == null)
            {
                result = provider.Insert(info);
            }
            else
            {
                result = provider.Update(info, find.QueryObject);
            }

            return CreateInterfaceReturnCode(result.Result);
        }
        #endregion

        #region IParkWebService接口实现方法
        /// <summary>
        /// 保存车单接口
        /// </summary>
        /// <param name="sheetID">车单编号</param>
        /// <param name="employeeNum">员工编号</param>
        /// <param name="employeeName">员工姓名</param>
        /// <param name="telPhone">手机号</param>
        /// <param name="department">部门名称</param>
        /// <param name="carPlate">车牌号（多个车牌号以逗号分开）</param>
        /// <param name="status">状态（=0 无效，=1 有效）</param>
        /// <param name="activationDate">生效日期（格式yyyy-MM-dd）</param>
        /// <param name="places">停车地点</param>
        /// <returns>=0 成功，=其他 错误代码</returns>
        public int SaveSheet(string sheetID, string employeeNum, string employeeName, string telPhone, string department, string carPlate, byte status, string activationDate, string places, bool enableLimitation,double limitation)
        {
            try
            {
                InterfaceReturnCode result = _SaveSheet(sheetID, employeeNum, employeeName, telPhone, department, carPlate, status, activationDate, places, enableLimitation,limitation );
                return (int)result;
            }
            catch (Exception)
            {
                return (int)InterfaceReturnCode.InterfaceException;//接口执行期间发生错误
            }
        }

        /// <summary>
        /// 车单状态接口
        /// </summary>
        /// <param name="userID">车单编号（不能为空）</param>
        /// <param name="status">状态（=0 无效，=1 有效）</param>
        /// <param name="activationDate">生效日期（为空时不修改，格式yyyy-MM-dd）</param>
        /// <returns>=0 成功，=其他 错误代码</returns>
        public int SheetStatus(string userID, byte status, string activationDate)
        {
            try
            {
                if ((status != 0 && status != 1)
                    || string.IsNullOrEmpty(userID))
                {
                    return (int)InterfaceReturnCode.ParameterError;
                }
                //卡片状态  = 1 Enabled 已发行, = 3 Disabled 禁用,
                byte cardStatus = (byte)(status == 0 ? 3 : 1);
                SQLHelperProvider sqlHelper = new SQLHelperProvider(AppConifg.Current.ParkingConnection);
                string cmdstr;
                if (!string.IsNullOrEmpty(activationDate))
                {
                    cmdstr = @"update Card set Status=@status,ActivationDate=@activationDate where CardID=@cardID";
                }
                else
                {
                    cmdstr = @"update Card set Status=@status where CardID=@cardID";
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = cmdstr;
                cmd.Parameters.AddWithValue("@status", cardStatus);
                if (!string.IsNullOrEmpty(activationDate))
                {
                    cmd.Parameters.AddWithValue("@activationDate", activationDate);
                }
                cmd.Parameters.AddWithValue("@cardID", userID);
                SQLQueryResult<int> result = sqlHelper.SQLExecuteNonQuery(cmd);
                InterfaceReturnCode code = CreateInterfaceReturnCode(result.Result);
                if (code == InterfaceReturnCode.Success && result.QueryObject == 0)
                {
                    //返回更新行数为0时，标识为无记录
                    code = InterfaceReturnCode.DatabaseNoRecord;
                }
                return (int)code;
            }
            catch (Exception)
            {
                return (int)InterfaceReturnCode.InterfaceException;//接口执行期间发生错误
            }
        }

        /// <summary>
        /// 通过员工编号删除员工记录接口
        /// </summary>
        /// <param name="employeeNum"></param>
        /// <returns></returns>
        public int DeleteEmployee(string employeeNum)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeNum))
                {
                    return (int)InterfaceReturnCode.ParameterError;
                }
                SQLHelperProvider sqlHelper = new SQLHelperProvider(AppConifg.Current.ParkingConnection);
                string cmdstr = @"delete from Card where CardID=@cardID";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = cmdstr;
                cmd.Parameters.AddWithValue("@cardID", employeeNum);
                SQLResultCode code = sqlHelper.SQLExecuteNonQuery(cmd).Result;
                InterfaceReturnCode result = CreateInterfaceReturnCode(code);
                return (int)result;
            }
            catch (Exception)
            {
                return (int)InterfaceReturnCode.InterfaceException;//接口执行期间发生错误
            }
        }
        #endregion
    }
}
