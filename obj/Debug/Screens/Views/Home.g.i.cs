﻿#pragma checksum "..\..\..\..\Screens\Views\Home.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "26C6913A8C85A0AEC88800E96FD546BD88EAE53274C70CB488E98145AF77E55B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SimpleScada.Screens.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SimpleScada.Screens.Views {
    
    
    /// <summary>
    /// Home
    /// </summary>
    public partial class Home : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 92 "..\..\..\..\Screens\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgUV1;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\Screens\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar TankLvlBar;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\Screens\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Test;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Screens\Views\Home.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox testValue;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SimpleScada;component/screens/views/home.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Screens\Views\Home.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ImgUV1 = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.TankLvlBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 3:
            
            #line 105 "..\..\..\..\Screens\Views\Home.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UV1_Open_Station);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Test = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\..\Screens\Views\Home.xaml"
            this.Test.Click += new System.Windows.RoutedEventHandler(this.onClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.testValue = ((System.Windows.Controls.TextBox)(target));
            
            #line 107 "..\..\..\..\Screens\Views\Home.xaml"
            this.testValue.KeyDown += new System.Windows.Input.KeyEventHandler(this.enter);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

