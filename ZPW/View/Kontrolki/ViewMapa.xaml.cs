using System; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Data; 
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using ZPW.ViewModel;
using ZPW.Model;

namespace ZPW.View.Kontrolki {
   /// <summary>
      /// Interaction logic for MyUserControl.xaml 
   /// </summary> 
	
   public partial class ViewMapa : UserControl 
   { 
	
        public ViewMapa() 
        { 
            InitializeComponent();
            StartCloseTimer();
        }  

        private void Pin_LeftClick(object sender, RoutedEventArgs e)
        {
          Image sImg = (Image)sender;
          int iImg = (int)sImg.Tag;
          ZPW.ViewModel.ZPW zpw = Application.Current.Resources["zpw"] as ZPW.ViewModel.ZPW;
          zpw.SetSelectedItem(iImg);
        }

        private void ButonPowiększ_Click(object sender, RoutedEventArgs e)
        {
          Zwiększ();
        }        
        private void ButonZmniejsz_Click(object sender, RoutedEventArgs e)
        {
          Zmniejsz();
        }

        void Zwiększ(double xTo=0, double yTo=0)
        {
          if(TheView.Width<=20000) 
          { 
            double HO = TheScroll.HorizontalOffset;
            double VO = TheScroll.VerticalOffset;
            double VPW = TheScroll.ViewportHeight/2;
            double VPH = TheScroll.ViewportWidth/2;
            double W = TheView.ActualWidth;
            double M = (W+W*0.4)/W;
            double Px1 = HO + VPH;
            double Py1 = VO + VPW;
            if(xTo!=0) Px1=xTo;
            if(yTo!=0) Py1=yTo;
            double Px2 = Px1 * M;
            double Py2 = Py1 * M;
            double H = Px2 - Px1;
            double V = Py2 - Py1;
            TheView.Width += W*0.4;
            TheScroll.ScrollToVerticalOffset(VO+V);
            TheScroll.ScrollToHorizontalOffset(HO+H);
          }
        }

        void Zmniejsz(double xTo=0, double yTo=0)
        {
          if(TheView.Width>=1000)
          {
            double HO = TheScroll.HorizontalOffset;
            double VO = TheScroll.VerticalOffset;
            double VPW = TheScroll.ViewportHeight/2;
            double VPH = TheScroll.ViewportWidth/2;
            double W = TheView.ActualWidth;
            double M = (W-W*0.4)/W;
            double Px1 = HO + VPH;
            double Py1 = VO + VPW;
            if(xTo!=0) Px1=xTo;
            if(yTo!=0) Py1=yTo;
            double Px2 = Px1 * M;
            double Py2 = Py1 * M;
            double H = Px2 - Px1;
            double V = Py2 - Py1;
            TheView.Width -= W*0.4;
            TheScroll.ScrollToVerticalOffset(VO+V);
            TheScroll.ScrollToHorizontalOffset(HO+H);
          }
        }

        private void ScrollAll_DragDeltaScroll(object sender, MouseWheelEventArgs e)
        {
            e.Handled=true;
            double xTo = e.MouseDevice.GetPosition(TheView).X;
            double yTo = e.MouseDevice.GetPosition(TheView).Y;

            if(e.Delta>0)
            {
              Zwiększ(xTo, yTo);
            }
            else if (e.Delta<0)
            {
              Zmniejsz(xTo, yTo);
            }
            

        }        
        private void ScrollAll_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double cW = e.VerticalChange;
            double eW = TheScroll.VerticalOffset;
            double cH = e.HorizontalChange;
            double eH = TheScroll.HorizontalOffset;
            TheScroll.ScrollToVerticalOffset(eW-cW);
            TheScroll.ScrollToHorizontalOffset(eH-cH);
        }
        private void YellowBorder_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double cW = e.VerticalChange*3;
            double eW = TheScroll.VerticalOffset;
            double cH = e.HorizontalChange*3;
            double eH = TheScroll.HorizontalOffset;
            TheScroll.ScrollToVerticalOffset(eW+cW);
            TheScroll.ScrollToHorizontalOffset(eH+cH);
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            hideMe.Visibility=System.Windows.Visibility.Hidden;
        }

    } 
}