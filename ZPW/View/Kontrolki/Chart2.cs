using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using ZPW.Model;

namespace ZPW.View.Kontrolki
{
    /// <summary>
    /// Interaction logic for MyUserControl.xaml 
    /// </summary> 

    public partial class Chart2 : UserControl
    {

        public Chart2()
        {
            InitializeComponent();

            SeriesCollection0 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Zgłoszone",
                    Values = new ChartValues<double>(ZgłoszeniaRepository.LiczbaZgłoszeńNaMiesiąc()),
                    DataLabels = true,
                    FontSize = 16               
                }
            };
            SeriesCollection0.Add(new ColumnSeries
            {
                Title = "Wykonane",
                Values = new ChartValues<double>(ZgłoszeniaRepository.LiczbaZgłoszeńNaMiesiąc()),
                DataLabels = true,
                FontSize = 16

            });
            //SeriesCollection[1].Values.Add(48d);

            Labels0 = Miesiące();

            Formatter = value => value.ToString("N0");

            DataContext = this;

        }

        private static string[] Miesiące()
        {
            string[] Miesiąc = new[] { "styczeń", "luty", "marzec", "kwiecień", "maj", "czerwiec", "lipiec", "sierpień", "wrzesień", "październik", "listopad", "grudzień" };
            DateTime now = DateTime.Now;
            int NowMonth = (int)now.Month;
            string[] miesiące = new string[12];
            int j;
            for (int i = 0; i < 12; i++)
            {
                j = i + NowMonth;
                if (j > 11) { j -= 12; }
                miesiące[i] = Miesiąc[j];
            }
            return miesiące;
        }


        public SeriesCollection SeriesCollection0 { get; set; }

        public string[] Labels0 { get; set; }

        public Func<double, string> Formatter { get; set; }


    }
}