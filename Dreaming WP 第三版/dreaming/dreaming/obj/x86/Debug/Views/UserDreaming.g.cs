﻿

#pragma checksum "E:\WP源码\dreaming\dreaming\Views\UserDreaming.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "50284A9479C1ECF786791B87154C0DC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dreaming.Views
{
    partial class UserDreaming : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 184 "..\..\..\Views\UserDreaming.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ListView_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 217 "..\..\..\Views\UserDreaming.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.song_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 244 "..\..\..\Views\UserDreaming.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.praise_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 253 "..\..\..\Views\UserDreaming.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.comment_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 274 "..\..\..\Views\UserDreaming.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


