using System; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Input;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using ZPW.Model;

namespace ZPW.View.Kontrolki {
   /// <summary>
      /// Interaction logic for MyUserControl.xaml 
   /// </summary> 
	
   public partial class Chart1 : UserControl 
   { 
	
        public Chart1() 
        { 
            InitializeComponent();
            
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
  
            Wykonane = ZgłoszeniaRepository.LiczbaWykonanych();

            Niewykonane = ZgłoszeniaRepository.LiczbaNiewykonanych();            
            
            SeriesCollection0 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Niewykonane",
                    Values = new ChartValues<double>(Niewykonane),  
                    DataLabels = true, 
                    LabelPoint = PointLabel,
                    FontSize = 16

                },
                new PieSeries
                {
                    Title = "Wykonane",
                    Values = new ChartValues<double>(Wykonane),
                    DataLabels = true, 
                    LabelPoint = PointLabel,
                    FontSize = 16
                }
            };
                               
            DataContext = this;
        }
 
        public Func<ChartPoint, string> PointLabel { get; set; }
 
        public SeriesCollection SeriesCollection0 { get; set; }

        public System.Collections.Generic.IEnumerable<double> Wykonane { get; set; }

        public System.Collections.Generic.IEnumerable<double> Niewykonane { get; set; }
        
        public void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart) chartpoint.ChartView;
            
            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;
 
            var selectedSeries = (PieSeries) chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
 

    } 
}