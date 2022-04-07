using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ZPW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class Application : System.Windows.Application
    {

        private void ComboTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ComboBox _cB = (ComboBox)sender;
            if (!_cB.IsDropDownOpen) 
            {
                _cB.IsDropDownOpen=true;
                e.Handled=true;
            }
        }

    }

}
