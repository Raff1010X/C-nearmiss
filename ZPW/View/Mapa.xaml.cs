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
using System.Windows.Shapes;


namespace ZPW.View
{
    /// <summary>
    /// Interaction logic for Mapa.xaml
    /// </summary>
    public partial class Mapa : Window
    {
        public Mapa()
        {
            InitializeComponent();
        }

        private void MapaXY_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Mouse.GetPosition((IInputElement)sender);
            Image img = (Image)sender;
            Pin.Visibility = Visibility.Visible;
            double X = ((p.X / img.ActualWidth) * img.ActualWidth) - Pin.ActualWidth / 2;
            double Y = ((p.Y / img.ActualHeight) * img.ActualHeight) - Pin.ActualHeight;
            if (img.Name == "Pin")
            {
                Double left = Canvas.GetLeft(Pin);
                X += left;
                Double top = Canvas.GetTop(Pin);
                Y += top;
            }
            Canvas.SetLeft(Pin, X);
            Canvas.SetTop(Pin, Y);
        }

        //ViewModel.ZPW zpw = Application.Current.FindResource("zpw") as ViewModel.ZPW;

    }
}
