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

namespace ZPW.View
{
    /// <summary>
    /// Interaction logic for StartSplash.xaml
    /// </summary>
    public partial class StartSplash : Window
    {
        public StartSplash()
        {
            SplashScreen splash = new("View\\Pictures\\startSplash.png");
            splash.Show(true);
            InitializeComponent();
        }

        private void ZamknijButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ZgłośButton_Click(object sender, RoutedEventArgs e)
        {
            ZPW.ViewModel.ZPW zpw = Application.Current.Resources["zpw"] as ZPW.ViewModel.ZPW;
            zpw.OpenNoweZgłoszenie();
            this.Close();
        }

        private void PrzeglądajButton_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new ZPWRepisitory();
            mainWindow.Show();
            this.Close();
        }

        private void ZgłośButton_MouseMove(object sender, MouseEventArgs e)
        {
            OpisTextBlock.Text = "Zgłoś zagrożenie, zdarzenie potencjalnie wypadkowe, pożar, poważną awarię...";
        }

        private void ZgłośButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OpisTextBlock.Text = "";
        }

        private void PrzeglądajButton_MouseMove(object sender, MouseEventArgs e)
        {
        
            OpisTextBlock.Text = "Przeglądaj zgłoszenia w rejestrze...";
        }

        private void ZamknijButton_MouseMove(object sender, MouseEventArgs e)
        {
            OpisTextBlock.Text = "Zamknij aplikację...";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
