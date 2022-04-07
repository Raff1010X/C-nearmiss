using System; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Input;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace ZPW.View.Kontrolki {
   /// <summary>
      /// Interaction logic for MyUserControl.xaml 
   /// </summary> 
	
   public partial class ViewStatystyki : UserControl 
   { 
	
        public ViewStatystyki() 
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
            SeriesCollection1=SeriesCollection0;
            SeriesCollection2=SeriesCollection0;
            SeriesCollection3=SeriesCollection0;
            SeriesCollection4=SeriesCollection0;
            SeriesCollection5=SeriesCollection0;
            //adding series will update and animate the chart automatically


            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);
 
            Labels0 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Labels1 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Labels2 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Labels3 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Labels4 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Labels5 = new[] {"sty", "lut", "mar", "kwi", "maj", "ccze", "lip", "sie", "wrz", "paź", "lis", "gru"};
            Formatter = value => value.ToString("N0");
 
            DataContext = this;

        }
 
        public SeriesCollection SeriesCollection0 { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public SeriesCollection SeriesCollection3 { get; set; }
        public SeriesCollection SeriesCollection4 { get; set; }
        public SeriesCollection SeriesCollection5 { get; set; }
        public string[] Labels0 { get; set; }
        public string[] Labels1 { get; set; }
        public string[] Labels2 { get; set; }
        public string[] Labels3 { get; set; }
        public string[] Labels4 { get; set; }
        public string[] Labels5 { get; set; }
        public Func<double, string> Formatter { get; set; }
 
    } 
}