﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Swazer.ShoppingList.WebApp.API.Resources.Register {
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
    public class RegisterApiStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegisterApiStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Swazer.ShoppingList.WebApp.API.Resources.Register.RegisterApiStrings", typeof(RegisterApiStrings).Assembly);
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
        ///   Looks up a localized string similar to الكود مطلوب.
        /// </summary>
        public static string CodeRequiredError {
            get {
                return ResourceManager.GetString("CodeRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تأكيد كلمة المرور يجب أن يطابق كلمة المرور.
        /// </summary>
        public static string ComparePassword {
            get {
                return ResourceManager.GetString("ComparePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حقل {0} يجب أن يكون بطول {2} على الأقل.
        /// </summary>
        public static string EmailLengthError {
            get {
                return ResourceManager.GetString("EmailLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to البريد الالكتروني غير صحيح.
        /// </summary>
        public static string EmailNotCorrect {
            get {
                return ResourceManager.GetString("EmailNotCorrect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حقل {0} يجب أن يكون بطول {2} على الأقل.
        /// </summary>
        public static string FullNameLengthError {
            get {
                return ResourceManager.GetString("FullNameLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حقل الجنس مطلوب.
        /// </summary>
        public static string GenderRequiredError {
            get {
                return ResourceManager.GetString("GenderRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حقل الجوال مطلوب.
        /// </summary>
        public static string MobileRequiredError {
            get {
                return ResourceManager.GetString("MobileRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حقل {0} يجب أن يكون بطول {2} على الأقل.
        /// </summary>
        public static string PasswordLengthError {
            get {
                return ResourceManager.GetString("PasswordLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مزود  مطلوب.
        /// </summary>
        public static string Provider {
            get {
                return ResourceManager.GetString("Provider", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رمز  مطلوب.
        /// </summary>
        public static string Token {
            get {
                return ResourceManager.GetString("Token", resourceCulture);
            }
        }
    }
}
