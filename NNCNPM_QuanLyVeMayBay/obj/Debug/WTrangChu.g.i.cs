﻿#pragma checksum "..\..\WTrangChu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4050BB5ABE24734CDF52BB80AB283F403CBC142A02A8D4C41D03220C3B0B8A74"
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
using NNCNPM_QuanLyVeMayBay.MyUserControls;
using NNCNPM_QuanLyVeMayBay.UserControls;
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
    /// WTrangChu
    /// </summary>
    public partial class WTrangChu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_DoiMatKhau;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_ThemTaiKhoan;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label L_VeDaBan;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label L_ChuyenBay;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label L_DoanhSo;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\WTrangChu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox L_name;
        
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
            System.Uri resourceLocater = new System.Uri("/NNCNPM_QuanLyVeMayBay;component/wtrangchu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WTrangChu.xaml"
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
            this.BTN_DoiMatKhau = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\WTrangChu.xaml"
            this.BTN_DoiMatKhau.Click += new System.Windows.RoutedEventHandler(this.BTN_DoiMatKhau_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BTN_ThemTaiKhoan = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\WTrangChu.xaml"
            this.BTN_ThemTaiKhoan.Click += new System.Windows.RoutedEventHandler(this.BTN_ThemTaiKhoan_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.L_VeDaBan = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.L_ChuyenBay = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.L_DoanhSo = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.L_name = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

