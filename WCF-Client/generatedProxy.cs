﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Pub
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WCF_Pub")]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool BoolValueField;
        
        private string StringValueField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue
        {
            get
            {
                return this.BoolValueField;
            }
            set
            {
                this.BoolValueField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue
        {
            get
            {
                return this.StringValueField;
            }
            set
            {
                this.StringValueField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="ITestWcfService")]
public interface ITestWcfService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetData", ReplyAction="http://tempuri.org/ITestWcfService/GetDataResponse")]
    string GetData(int value);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetData", ReplyAction="http://tempuri.org/ITestWcfService/GetDataResponse")]
    System.Threading.Tasks.Task<string> GetDataAsync(int value);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITestWcfService/GetDataUsingDataContractResponse")]
    WCF_Pub.CompositeType GetDataUsingDataContract(WCF_Pub.CompositeType composite);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITestWcfService/GetDataUsingDataContractResponse")]
    System.Threading.Tasks.Task<WCF_Pub.CompositeType> GetDataUsingDataContractAsync(WCF_Pub.CompositeType composite);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface ITestWcfServiceChannel : ITestWcfService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class TestWcfServiceClient : System.ServiceModel.ClientBase<ITestWcfService>, ITestWcfService
{
    
    public TestWcfServiceClient()
    {
    }
    
    public TestWcfServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public TestWcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public TestWcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public TestWcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string GetData(int value)
    {
        return base.Channel.GetData(value);
    }
    
    public System.Threading.Tasks.Task<string> GetDataAsync(int value)
    {
        return base.Channel.GetDataAsync(value);
    }
    
    public WCF_Pub.CompositeType GetDataUsingDataContract(WCF_Pub.CompositeType composite)
    {
        return base.Channel.GetDataUsingDataContract(composite);
    }
    
    public System.Threading.Tasks.Task<WCF_Pub.CompositeType> GetDataUsingDataContractAsync(WCF_Pub.CompositeType composite)
    {
        return base.Channel.GetDataUsingDataContractAsync(composite);
    }
}
