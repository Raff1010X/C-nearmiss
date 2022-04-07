using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Navigation;

namespace ZPW.View
{
    /// <summary>
    /// Interaction logic for StartSplash.xaml
    /// </summary>
    public partial class OProgramie : Window
    {
        public OProgramie()
        {
            InitializeComponent();
        }

        private void ZamknijButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) {UseShellExecute = true});
        e.Handled = true;
        }

    }

}
