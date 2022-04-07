using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using ZPW.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Runtime.Serialization;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;




//using System.Collections.ObjectModel;

namespace ZPW.Model
{

    #region ZGŁOSZENIE
    public class Zgłoszenie
    {
        #region 1 Numer zgłoszenia
        public int Numer { get; set; }
        #endregion //Numer zgłoszenia

        #region 2 Data zgłoszenia
        public DateTime DataZgł { get; set; }
        #endregion //Data zgłoszenia

        #region 3 Imię
        public string Imię { get; set; }
        #endregion //Imię

        #region 4 Dział - KTO ZGŁASZA
        public string DziałKtoZgłasza { get; set; }
        #endregion //Lista działów KTO ZGŁASZA

        #region 5 Data zdarzenia
        public DateTime DataZdr { get; set; }
        #endregion //Data zdarzenia

        #region 6 Godzina zdarzenia
        public string GodzinaZdarzenia { get; set; }
        #endregion //Lista godzina zdarzenia

        #region 7 MIEJSCE ZDARZENIA
        public string MiejsceZdarzenia { get; set; }
        public double MiejsceX { get; set; }
        public double MiejsceY { get; set; }
        #endregion //MIEJSCE ZDARZENIA

        #region 8 Miejsce opis
        public string Miejsce { get; set; }
        #endregion //Miejsce opis

        #region 9 Zagrożenie opis
        public string Zagrożenie { get; set; }
        #endregion //Zagrożenie opis

        #region 10 Działania opis
        public string Działania { get; set; }
        #endregion //Działania opie    

        #region 11 RODZAJ ZAGROŻENIA
        public string RodzajZagrożeń { get; set; }
        #endregion // RODZAJ ZAGROŻENIA

        #region 12 Skutki opis
        public string SkutkiOpis { get; set; }
        #endregion //Zagrożenie opis    

        #region 13 Lista SKUTKI
            public string SkutkiSkala { get; set; }
        #endregion // Lista skutki

        #region 14 ODPOWIEDZIALNY
        public string Odpowiedzialny { get; set; }
        #endregion //ODPOWIEDZIALNY

        #region 15 Enviromental user nameR
        public string EnvUserName { get; set; }
        #endregion //Enviromental user name

        #region 16 Zdjęcie
        public string ZdjęciePath { get; set; }
        #endregion //Zdjęcie

        #region 17 Termin wykonania
        public DateTime Termin { get; set; }
        #endregion //Termin Wykonania

        #region 18 Wykonano
        public bool Wykonane { get; set; }
        #endregion //Wykonano

        #region 19 Wykonano zgłaszający
        public string WykonanoZgłaszający { get; set; }
        #endregion // Wykonano zgłaszający

        #region 20 String do wyszukiwania
        
        public string StringToSearch
        {
            get
            {
                return Działania.ToLower() +                       
                Miejsce.ToLower() +        
                //DateToStringCheck(_zgłoszenie) +
                MiejsceZdarzenia.ToLower() +                       
                Numer.ToString().ToLower() +                       
                Odpowiedzialny.ToLower() +                       
                SkutkiOpis.ToLower() +                       
                Zagrożenie.ToLower() +                       
                RodzajZagrożeń.ToLower();
            }
        }
        #endregion // String do wyszukiwania

    }
    #endregion //ZGŁOSZENIE

}