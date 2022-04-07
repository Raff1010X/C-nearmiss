using System; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Input;
using System.Windows.Threading;


namespace ZPW.View.Kontrolki {
   /// <summary>
      /// Interaction logic for MyUserControl.xaml 
   /// </summary> 
	
   public partial class ViewTabela : UserControl 
   { 
	
        public ViewTabela() 
        { 
            InitializeComponent();
            Loaded += OnLoaded;
        }  


        private void OnLoaded(object sender, EventArgs args)
        {
            StartCloseTimer();
            Loaded -= OnLoaded;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ZPW.ViewModel.ZPW zpw = Application.Current.FindResource("zpw") as ViewModel.ZPW;
            string ZdjęciePath = zpw.SelectedZgłoszenie.ZdjęciePath;
            if (ZdjęciePath != "")
            {
                Window IV = new ImageViewer();
                IV.Show();
            }
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromMilliseconds(500);
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