﻿#pragma checksum "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "502F372DBDE0135CE47163C6A225E2D77F58FEDF3AF8D84708DD772FA7F20601"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using NNCNPM_QuanLyVeMayBay;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace NNCNPM_QuanLyVeMayBay {
    
    
    /// <summary>
    /// WChonChuyenBay
    /// </summary>
    public partial class WChonChuyenBay : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DG_DSChuyenBay;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_TimNoiDen;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_TimNoiDi;
        
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
            System.Uri resourceLocater = new System.Uri("/NNCNPM_QuanLyVeMayBay;component/b%c3%a1n%20v%c3%a9%20m%c3%a1y%20bay/wchonchuyenb" +
                    "ay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
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
            this.DG_DSChuyenBay = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
            this.DG_DSChuyenBay.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DG_DSChuyenBay_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TB_TimNoiDen = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
            this.TB_TimNoiDen.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TB_TimNoiDen_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TB_TimNoiDi = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\Bán vé máy bay\WChonChuyenBay.xaml"
            this.TB_TimNoiDi.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TB_TimNoiDi_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

