﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECardInterface.ECardServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ECardServiceClient.IParkWebService")]
    public interface IParkWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParkWebService/SaveSheet", ReplyAction="http://tempuri.org/IParkWebService/SaveSheetResponse")]
        int SaveSheet(string sheetID, string employeeNum, string employeeName, string telPhone, string department, string carPlate, byte status, string activationDateTime, string places);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParkWebService/SheetStatus", ReplyAction="http://tempuri.org/IParkWebService/SheetStatusResponse")]
        int SheetStatus(string sheetID, byte status, string activationDateTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParkWebService/DeleteEmployee", ReplyAction="http://tempuri.org/IParkWebService/DeleteEmployeeResponse")]
        int DeleteEmployee(string employeeNum);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IParkWebServiceChannel : ECardInterface.ECardServiceClient.IParkWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ParkWebServiceClient : System.ServiceModel.ClientBase<ECardInterface.ECardServiceClient.IParkWebService>, ECardInterface.ECardServiceClient.IParkWebService {
        
        public ParkWebServiceClient() {
        }
        
        public ParkWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ParkWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParkWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParkWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int SaveSheet(string sheetID, string employeeNum, string employeeName, string telPhone, string department, string carPlate, byte status, string activationDateTime, string places) {
            return base.Channel.SaveSheet(sheetID, employeeNum, employeeName, telPhone, department, carPlate, status, activationDateTime, places);
        }
        
        public int SheetStatus(string sheetID, byte status, string activationDateTime) {
            return base.Channel.SheetStatus(sheetID, status, activationDateTime);
        }
        
        public int DeleteEmployee(string employeeNum) {
            return base.Channel.DeleteEmployee(employeeNum);
        }
    }
}
