﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.1
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foms.GUI.AvailableReport {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.octopusnetwork.org/webservice/", ConfigurationName="AvailableReport.VersionServicePortType")]
    public interface VersionServicePortType {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.octopusnetwork.org/webservice/#GetAvailableReport", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="resultat")]
        string GetAvailableReport(string input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.octopusnetwork.org/webservice/#SetGuid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="resultat")]
        string SetGuid(string input);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface VersionServicePortTypeChannel : Foms.GUI.AvailableReport.VersionServicePortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VersionServicePortTypeClient : System.ServiceModel.ClientBase<Foms.GUI.AvailableReport.VersionServicePortType>, Foms.GUI.AvailableReport.VersionServicePortType {
        
        public VersionServicePortTypeClient() {
        }
        
        public VersionServicePortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VersionServicePortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VersionServicePortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VersionServicePortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetAvailableReport(string input) {
            return base.Channel.GetAvailableReport(input);
        }
        
        public string SetGuid(string input) {
            return base.Channel.SetGuid(input);
        }
    }
}