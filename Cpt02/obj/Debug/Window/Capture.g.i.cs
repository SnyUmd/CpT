﻿#pragma checksum "..\..\..\Window\Capture.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4BF0A3AB7AAD664495689C0C03E50B2A4A442907F7A27BBD304FD81D3499AF65"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using CpT;
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


namespace CpT {
    
    
    /// <summary>
    /// Capture
    /// </summary>
    public partial class Capture : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\Window\Capture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CpT.Capture Window_drug;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Window\Capture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas dCanvas;
        
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
            System.Uri resourceLocater = new System.Uri("/CpT;component/window/capture.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Window\Capture.xaml"
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
            this.Window_drug = ((CpT.Capture)(target));
            
            #line 14 "..\..\..\Window\Capture.xaml"
            this.Window_drug.KeyDown += new System.Windows.Input.KeyEventHandler(this.Key_down);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Window\Capture.xaml"
            this.Window_drug.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Mouse_down);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Window\Capture.xaml"
            this.Window_drug.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Mouse_Up);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 23 "..\..\..\Window\Capture.xaml"
            this.dCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.CanvasMouseMove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

