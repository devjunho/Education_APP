﻿#pragma checksum "..\..\..\..\View\ECHAT.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C17F8B5CE4663A0ADDA613D5E5FFDD3FEA2ED373"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using PlantTemp.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PlantTemp.View {
    
    
    /// <summary>
    /// ECHAT
    /// </summary>
    public partial class ECHAT : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 112 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ChatListBox;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Search_btn;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputTextBox;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Logout_btn;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Quiz1_btn;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Qna1_btn;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\View\ECHAT.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Chat_btn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PlantTemp;component/view/echat.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\ECHAT.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ChatListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 117 "..\..\..\..\View\ECHAT.xaml"
            this.ChatListBox.Loaded += new System.Windows.RoutedEventHandler(this.ChatListBox_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Search_btn = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.InputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Logout_btn = ((System.Windows.Controls.Button)(target));
            
            #line 157 "..\..\..\..\View\ECHAT.xaml"
            this.Logout_btn.Click += new System.Windows.RoutedEventHandler(this.Logout_btn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 164 "..\..\..\..\View\ECHAT.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Quiz1_btn = ((System.Windows.Controls.Button)(target));
            
            #line 171 "..\..\..\..\View\ECHAT.xaml"
            this.Quiz1_btn.Click += new System.Windows.RoutedEventHandler(this.Quiz1_btn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Qna1_btn = ((System.Windows.Controls.Button)(target));
            
            #line 178 "..\..\..\..\View\ECHAT.xaml"
            this.Qna1_btn.Click += new System.Windows.RoutedEventHandler(this.Qna1_btn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Chat_btn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
