﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScreamControl.Client.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US")]
        public global::System.Globalization.CultureInfo CurrentLanguage {
            get {
                return ((global::System.Globalization.CultureInfo)(this["CurrentLanguage"]));
            }
            set {
                this["CurrentLanguage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int MicCaptureBoost {
            get {
                return ((int)(this["MicCaptureBoost"]));
            }
            set {
                this["MicCaptureBoost"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AlarmVolume {
            get {
                return ((int)(this["AlarmVolume"]));
            }
            set {
                this["AlarmVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayBeforeAlarm {
            get {
                return ((int)(this["DelayBeforeAlarm"]));
            }
            set {
                this["DelayBeforeAlarm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayBeforeOverlay {
            get {
                return ((int)(this["DelayBeforeOverlay"]));
            }
            set {
                this["DelayBeforeOverlay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsSoundAlertEnabled {
            get {
                return ((bool)(this["IsSoundAlertEnabled"]));
            }
            set {
                this["IsSoundAlertEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public float Threshold {
            get {
                return ((float)(this["Threshold"]));
            }
            set {
                this["Threshold"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AlarmSystemVolume {
            get {
                return ((int)(this["AlarmSystemVolume"]));
            }
            set {
                this["AlarmSystemVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsOverlayAlertEnabled {
            get {
                return ((bool)(this["IsOverlayAlertEnabled"]));
            }
            set {
                this["IsOverlayAlertEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsStealthMode {
            get {
                return ((bool)(this["IsStealthMode"]));
            }
            set {
                this["IsStealthMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsUpgraded {
            get {
                return ((bool)(this["IsUpgraded"]));
            }
            set {
                this["IsUpgraded"] = value;
            }
        }
    }
}
