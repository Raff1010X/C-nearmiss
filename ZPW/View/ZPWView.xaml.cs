using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZPW.ViewModel;
using System.Threading;
using System.Globalization;
using System.Windows.Markup;

namespace ZPW.View
{
    /// <summary>
    /// Interaction logic for ZPWView.xaml
    /// </summary>
    public partial class ZPWView : Window
    {


        public ZPWView()
        {
            InitializeComponent();
            ////DziałViewModel da = new();
            ////DziałCombo.ItemsSource = da.Działy;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL"); 
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            this.Language = XmlLanguage.GetLanguage("pl-PL");
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            ////ZPW.Model.ComboBoxTextItem typeItem = (ZPW.Model.ComboBoxTextItem)DziałCombo.SelectedItem;
            ////string value = typeItem.TextItem;
            ////MessageBox.Show(value);
            ////ZPW.ViewModel.ZPWZgłoszenie ZPWz = this.Resources["zpwZ"] as ZPWZgłoszenie;
            ////string value = ZPWz.Miejsce;
            ////MessageBox.Show(value);
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////var selectedOption = MessageBox.Show("Aby wskazać miejsce kliknij lewym przyciskiem myszy na mapie.", "Wybór miejsca...", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            ////if (selectedOption == MessageBoxResult.OK)
            ////{
            ////    ZPW.ViewModel.ZPW zpw = Resources["zpw"] as ZPW.ViewModel.ZPW;
            ////    Window mapWindow = new Mapa();
            ////    mapWindow.DataContext = zpw;
            ////    mapWindow.Show();
            ////}
        }

    }
}
