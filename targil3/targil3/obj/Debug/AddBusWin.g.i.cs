﻿#pragma checksum "..\..\AddBusWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7042D1CB4A042D36D10E1C6DE7BA953729B29B8FAC27C364A55ADAE2AEADBF4D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using targil3B;


namespace targil3B {
    
    
    /// <summary>
    /// AddBusWin
    /// </summary>
    public partial class AddBusWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label addbus;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DP;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Sdate;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label license_num_text;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox license_num;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AddBusWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addbus1;
        
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
            System.Uri resourceLocater = new System.Uri("/targil3;component/addbuswin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddBusWin.xaml"
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
            this.addbus = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.DP = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.Sdate = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.license_num_text = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.license_num = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\AddBusWin.xaml"
            this.license_num.KeyDown += new System.Windows.Input.KeyEventHandler(this.AddBusEnter);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addbus1 = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\AddBusWin.xaml"
            this.addbus1.Click += new System.Windows.RoutedEventHandler(this.addbus1_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

