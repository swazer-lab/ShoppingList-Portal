﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Swazer.ShoppingList.WebApp.API.Resources.ChangePassword {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ChangePasswordErrorStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ChangePasswordErrorStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Swazer.ShoppingList.WebApp.API.Resources.ChangePassword.ChangePasswordErrorString" +
                            "s", typeof(ChangePasswordErrorStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Doğrulama şifresi yeni şifre ile eşleşmiyor.
        /// </summary>
        public static string ComparePasswordMatching {
            get {
                return ResourceManager.GetString("ComparePasswordMatching", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pasword doğrulama girilmelidir.
        /// </summary>
        public static string ConfirmPasswordRequired {
            get {
                return ResourceManager.GetString("ConfirmPasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yeni password girilmelidir.
        /// </summary>
        public static string NewPasswordRequired {
            get {
                return ResourceManager.GetString("NewPasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Eski password girilmelidir.
        /// </summary>
        public static string OldPasswordRequired {
            get {
                return ResourceManager.GetString("OldPasswordRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password en az 6 haneli olmalıdır.
        /// </summary>
        public static string PasswordLengthError {
            get {
                return ResourceManager.GetString("PasswordLengthError", resourceCulture);
            }
        }
    }
}