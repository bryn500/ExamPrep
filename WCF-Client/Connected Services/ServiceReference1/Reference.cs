﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyData", Namespace="http://schemas.datacontract.org/2004/07/WCF_Pub")]
    [System.SerializableAttribute()]
    public partial class MyData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ITestWcfService")]
    public interface ITestWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetData", ReplyAction="http://tempuri.org/ITestWcfService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetData", ReplyAction="http://tempuri.org/ITestWcfService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITestWcfService/GetDataUsingDataContractResponse")]
        WCF_Client.ServiceReference1.MyData GetDataUsingDataContract(WCF_Client.ServiceReference1.MyData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestWcfService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITestWcfService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<WCF_Client.ServiceReference1.MyData> GetDataUsingDataContractAsync(WCF_Client.ServiceReference1.MyData data);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestWcfServiceChannel : WCF_Client.ServiceReference1.ITestWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestWcfServiceClient : System.ServiceModel.ClientBase<WCF_Client.ServiceReference1.ITestWcfService>, WCF_Client.ServiceReference1.ITestWcfService {
        
        public TestWcfServiceClient() {
        }
        
        public TestWcfServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestWcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestWcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestWcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public WCF_Client.ServiceReference1.MyData GetDataUsingDataContract(WCF_Client.ServiceReference1.MyData data) {
            return base.Channel.GetDataUsingDataContract(data);
        }
        
        public System.Threading.Tasks.Task<WCF_Client.ServiceReference1.MyData> GetDataUsingDataContractAsync(WCF_Client.ServiceReference1.MyData data) {
            return base.Channel.GetDataUsingDataContractAsync(data);
        }
    }
}
