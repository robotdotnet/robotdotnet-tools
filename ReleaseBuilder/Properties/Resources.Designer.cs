﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReleaseBuilder.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReleaseBuilder.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://github.com/robotdotnet/FRC-Extension.git.
        /// </summary>
        internal static string ExtensionRepository {
            get {
                return ResourceManager.GetString("ExtensionRepository", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe.
        /// </summary>
        internal static string MSBuildPath {
            get {
                return ResourceManager.GetString("MSBuildPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://www.nuget.org/api/v2/package/NetworkTablesDotNet/.
        /// </summary>
        internal static string NetworkTablesDotNetNuGet {
            get {
                return ResourceManager.GetString("NetworkTablesDotNetNuGet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://github.com/robotdotnet/NetworkTablesDotNet.git.
        /// </summary>
        internal static string NetworkTablesDotNetRepository {
            get {
                return ResourceManager.GetString("NetworkTablesDotNetRepository", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://www.nuget.org/api/v2/package/WPILib/.
        /// </summary>
        internal static string WPILibNuGet {
            get {
                return ResourceManager.GetString("WPILibNuGet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://github.com/robotdotnet/robotdotnet-wpilib.git.
        /// </summary>
        internal static string WPILibRepository {
            get {
                return ResourceManager.GetString("WPILibRepository", resourceCulture);
            }
        }
    }
}
