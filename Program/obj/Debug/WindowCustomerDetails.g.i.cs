﻿#pragma checksum "..\..\WindowCustomerDetails.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3B2EC3B01F376267C6DF205206CE937E"
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


namespace Program {
    
    
    /// <summary>
    /// WindowCustomerDetails
    /// </summary>
    public partial class WindowCustomerDetails : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCustName;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCustNumber;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCustName;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblHeader;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCustAddress;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCustAddress;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblHeader_Copy;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstCustomers;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\WindowCustomerDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCustNumberValue;
        
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
            System.Uri resourceLocater = new System.Uri("/Program;component/windowcustomerdetails.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowCustomerDetails.xaml"
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
            this.lblCustName = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblCustNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txtCustName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lblHeader = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\WindowCustomerDetails.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblCustAddress = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.txtCustAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.lblHeader_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.lstCustomers = ((System.Windows.Controls.ListBox)(target));
            
            #line 16 "..\..\WindowCustomerDetails.xaml"
            this.lstCustomers.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstCustomers_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblCustNumberValue = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

