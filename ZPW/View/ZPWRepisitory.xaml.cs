using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;

namespace ZPW.View
{
    /// <summary>
    /// Interaction logic for ZPWRepisitory.xaml
    /// </summary>
    public partial class ZPWRepisitory : Window
    {
        public ZPWRepisitory()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            this.Language = XmlLanguage.GetLanguage("pl-PL");
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight-50;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth-20;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            WindowStateChange();
        }

        private void WindowStateChange()
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void DragDeltaNS(object sender, DragDeltaEventArgs e)
        {
            if(this.Height>this.MinHeight)
			    this.Height += e.VerticalChange;
		    else
            {
                this.Height = this.MinHeight + 4;
                BtmThumb.ReleaseMouseCapture();
            }
        }
        private void DragDeltaWER(object sender, DragDeltaEventArgs e)
        {
            if (this.Width > this.MinWidth)
                this.Width += e.HorizontalChange;
            else
            {
                this.Width = this.MinWidth + 4;
                RgtThumb.ReleaseMouseCapture();
            }
        }

        private void DragDeltaWEL(object sender, DragDeltaEventArgs e)
        {
            if (this.Width > this.MinWidth)
            {
                this.Width -= e.HorizontalChange;
                this.Left += e.HorizontalChange;
            }
            else
            {
                this.Width = this.MinWidth + 4;
                LftThumb.ReleaseMouseCapture();
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState=System.Windows.WindowState.Minimized;
        }
 
    }

}
