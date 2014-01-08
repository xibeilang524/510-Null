using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ralid.Park.WebService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IParkWebService”。
    [ServiceContract]
    public interface IParkWebService
    {
        // TODO: 在此添加您的服务操作
        
        /// <summary>
        /// 车单推送接口
        /// </summary>
        /// <param name="sheetID">车单编号</param>
        /// <param name="employeeNum">员工编号</param>
        /// <param name="employeeName">员工姓名</param>
        /// <param name="telPhone">手机号</param>
        /// <param name="department">部门名称</param>
        /// <param name="carPlate">车牌号（多个车牌号以逗号分开）</param>
        /// <param name="status">状态（=0 无效，=1 有效）</param>
        /// <param name="activationDateTime">生效日期</param>
        /// <param name="places">停车地点</param>
        /// <param name="enableLimitation">是否启用限时停车</param>
        /// <returns>=0 成功，=其他 错误代码</returns>
        [OperationContract]
        int SaveSheet(string sheetID, string employeeNum, string employeeName, string telPhone, string department, string carPlate, byte status, string activationDateTime, string places,bool enableLimitation,double limitation);

        /// <summary>
        /// 车单状态接口
        /// </summary>
        /// <param name="sheetID">车单编号</param>
        /// <param name="status">状态（=0 无效，=1 有效）</param>
        /// <param name="activationDateTime">生效日期</param>
        /// <returns>=0 成功，=其他 错误代码</returns>
        [OperationContract]
        int SheetStatus(string userID, byte status, string activationDateTime);

        /// <summary>
        /// 删除员工接口
        /// </summary>
        /// <param name="employeeNum">员工编号</param>
        /// <returns>=0 成功，=其他 错误代码</returns>
        [OperationContract]
        int DeleteEmployee(string userID);
    }

}
