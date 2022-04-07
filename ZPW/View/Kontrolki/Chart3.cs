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
	
   public partial class Chart3 : UserControl 
   { 
	
        public Chart3() 
        {
            InitializeComponent();
 
            SeriesCollection0 = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Title = "Wykonane",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 50, 39, 50, 50, 39, 50, 50, 39 },
                    DataLabels = true,
                    FontSize = 16 
                },
                new StackedRowSeries
                {
                    Title = "Niewykonane",
                    Values = new ChartValues<double> { 11, 56, 42, 56, 42, 56, 42, 56, 42, 56, 42, 56},
                    DataLabels = true,
                    FontSize = 16 
                }
            };
 
            Labels = new[] {"Kierownik dzialu technika -", "Kierownik magazynu opakowań -", "Kierownik magazynu surowców -", "Kierownik magazynu wyrobów -", "Kierownik malarni -", "Kierownik produkcji -", "Kierownik sitodruku -", "Kierownik sortowni -", "Kierownik szlifierni -", "Kierownik utrzymania ruchu -", "Kierownik warsztatu -", "Kierownik zestawiarni i topienia -"};

            Formatter = value => value.ToString("N0");
 
            DataContext = this;
        }
 
        public SeriesCollection SeriesCollection0 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
 

    } 
}