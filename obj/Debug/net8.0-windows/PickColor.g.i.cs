﻿#pragma checksum "..\..\..\PickColor.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F1D74E376A4737ADD621D2D7C9260395D68AE675"
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


namespace GrafikaProjektMR {
    
    
    /// <summary>
    /// PickColor
    /// </summary>
    public partial class PickColor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderR;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtR;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderG;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtG;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderB;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtB;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtH;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtS;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtV;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\PickColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle colorPreview;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GrafikaProjektMR;V1.0.0.0;component/pickcolor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PickColor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.sliderR = ((System.Windows.Controls.Slider)(target));
            
            #line 11 "..\..\..\PickColor.xaml"
            this.sliderR.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtR = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\..\PickColor.xaml"
            this.txtR.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtR_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.sliderG = ((System.Windows.Controls.Slider)(target));
            
            #line 17 "..\..\..\PickColor.xaml"
            this.sliderG.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtG = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\PickColor.xaml"
            this.txtG.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtG_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.sliderB = ((System.Windows.Controls.Slider)(target));
            
            #line 23 "..\..\..\PickColor.xaml"
            this.sliderB.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtB = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\PickColor.xaml"
            this.txtB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtB_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtS = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtV = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.colorPreview = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 11:
            
            #line 40 "..\..\..\PickColor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

