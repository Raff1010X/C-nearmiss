﻿#pragma checksum "..\..\..\..\..\View\StartSplash.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68011421520C731679B872875A652EA22F128C9B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

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
using ZPW.ViewModel;


namespace ZPW.View {
    
    
    /// <summary>
    /// StartSplash
    /// </summary>
    public partial class StartSplash : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\View\StartSplash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZPW.View.StartSplash Window;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\View\StartSplash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock OpisTextBlock;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\View\StartSplash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ZgłośButton;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\..\View\StartSplash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrzeglądajButton;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\..\View\StartSplash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ZamknijButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ZPW;V1.0.0.0;component/view/startsplash.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\StartSplash.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Window = ((ZPW.View.StartSplash)(target));
            return;
            case 2:
            
            #line 19 "..\..\..\..\..\View\StartSplash.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.OpisTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ZgłośButton = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZgłośButton.Click += new System.Windows.RoutedEventHandler(this.ZgłośButton_Click);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZgłośButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.ZgłośButton_MouseMove);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZgłośButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ZgłośButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PrzeglądajButton = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\..\..\View\StartSplash.xaml"
            this.PrzeglądajButton.Click += new System.Windows.RoutedEventHandler(this.PrzeglądajButton_Click);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\..\..\View\StartSplash.xaml"
            this.PrzeglądajButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.PrzeglądajButton_MouseMove);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\..\..\View\StartSplash.xaml"
            this.PrzeglądajButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ZgłośButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ZamknijButton = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZamknijButton.Click += new System.Windows.RoutedEventHandler(this.ZamknijButton_Click);
            
            #line default
            #line hidden
            
            #line 103 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZamknijButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.ZamknijButton_MouseMove);
            
            #line default
            #line hidden
            
            #line 103 "..\..\..\..\..\View\StartSplash.xaml"
            this.ZamknijButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ZgłośButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

