﻿#pragma checksum "..\..\Viewer.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8E5E82141725513F3876141E2D8B486BB9F83FDA908C905E2E90177F8BFAC65B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace Change_electrical_system_parameters {
    
    
    /// <summary>
    /// Viewer
    /// </summary>
    public partial class Viewer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Change_electrical_system_parameters.Viewer main_window;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid main_grid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox select_method;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox voltage_loss;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox laying_method;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_okay;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/Change_electrical_system_parameters;component/viewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Viewer.xaml"
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
            this.main_window = ((Change_electrical_system_parameters.Viewer)(target));
            return;
            case 2:
            this.main_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.select_method = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.voltage_loss = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.laying_method = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.button_okay = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Viewer.xaml"
            this.button_okay.Click += new System.Windows.RoutedEventHandler(this.Button_click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.button_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\Viewer.xaml"
            this.button_cancel.Click += new System.Windows.RoutedEventHandler(this.Button_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
