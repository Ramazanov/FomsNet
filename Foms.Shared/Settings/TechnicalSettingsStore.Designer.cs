﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.225
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foms.Shared.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class TechnicalSettingsStore : global::System.Configuration.ApplicationSettingsBase {
        
        private static TechnicalSettingsStore defaultInstance = ((TechnicalSettingsStore)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new TechnicalSettingsStore())));
        
        public static TechnicalSettingsStore Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public string DATABASE_TIMEOUT {
            get {
                return ((string)(this["DATABASE_TIMEOUT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.1.0")]
        public string VERSION {
            get {
                return ((string)(this["VERSION"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ТФОМС Дагестан")]
        public string COMPANY {
            get {
                return ((string)(this["COMPANY"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ТФОМС Дагестан")]
        public string COPYRIGHT {
            get {
                return ((string)(this["COPYRIGHT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ТФОМС Дагестан")]
        public string TRADEMARK {
            get {
                return ((string)(this["TRADEMARK"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FOMS.NET")]
        public string PRODUCT {
            get {
                return ((string)(this["PRODUCT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DEBUG_MODE {
            get {
                return ((bool)(this["DEBUG_MODE"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FomsNet")]
        public string DATABASE_NAME {
            get {
                return ((string)(this["DATABASE_NAME"]));
            }
            set {
                this["DATABASE_NAME"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("server7")]
        public string DATABASE_SERVER_NAME {
            get {
                return ((string)(this["DATABASE_SERVER_NAME"]));
            }
            set {
                this["DATABASE_SERVER_NAME"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("nariman")]
        public string DATABASE_LOGIN_NAME {
            get {
                return ((string)(this["DATABASE_LOGIN_NAME"]));
            }
            set {
                this["DATABASE_LOGIN_NAME"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("letsgo")]
        public string DATABASE_PASSWORD {
            get {
                return ((string)(this["DATABASE_PASSWORD"]));
            }
            set {
                this["DATABASE_PASSWORD"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int DEBUG_LEVEL {
            get {
                return ((int)(this["DEBUG_LEVEL"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SENT_QUESTIONNAIRE {
            get {
                return ((bool)(this["SENT_QUESTIONNAIRE"]));
            }
            set {
                this["SENT_QUESTIONNAIRE"] = value;
            }
        }
    }
}
