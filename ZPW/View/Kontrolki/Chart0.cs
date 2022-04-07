using System; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Input;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace ZPW.View.Kontrolki 
{

      /// TODO - Widoki wykresów

	
   public partial class Chart0 : UserControl 
   { 
	
        public Chart0() 
        { 
            InitializeComponent();
            
            SeriesCollection0 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> {10,12,14,15,17,19,26,24,35,45,56,66}
                }
            };
            //adding series will update and animate the chart automatically


            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);
 
            Labels0 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};

            Formatter = value => value.ToString("N0");
 
            DataContext = this;

        }
 
        public SeriesCollection SeriesCollection0 { get; set; }

        public string[] Labels0 { get; set; }

        public Func<double, string> Formatter { get; set; }
 

    } 
}