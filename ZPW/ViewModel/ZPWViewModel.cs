using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using ZPW.Model;
using System.Windows.Input;
using System.Windows;
using ZPW.View;
using ZPW.View.Kontrolki;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Windows.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.DirectoryServices.AccountManagement;

using System.Drawing;
using System.Security.Cryptography;

namespace ZPW.ViewModel
{

    #region Binding proxy error handler
    // OBSERWUJ - Do sprawdzenia czy działa z datagrid ViewTabela.xaml 
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
            typeof(BindingProxy), new UIPropertyMetadata(null));
    }
    #endregion //Binding proxy error handler


    public class ZPW : ObservedObject
    {

        #region Info

        public static int LiczbaZgłoszeń { get => ZgłoszeniaRepository.LiczbaZgłoszeń(); }
        public static int LiczbaZgłoszeńWykonanych { get => ZgłoszeniaRepository.LiczbaZgłoszeńWykonanych(); }
        public static string LiczbaWypadków { get => Math.Round((double)((double)LiczbaZgłoszeńWykonanych / 100), 1).ToString(); }

        //private static readonly byte[] euBytes = Encoding.Unicode.GetBytes(Environment.UserName);
        //private static readonly string enviromentUserName = Convert.ToBase64String (
        //    ProtectedData.Protect(euBytes, euBytes, DataProtectionScope.LocalMachine));
        private static readonly string[] currentUserName = UserPrincipal.Current.DisplayName.Split(' ');
        public static string CurrentUserName { get => currentUserName[0]; }

        public static string EnviromentUserName { get => Shade(Environment.UserName, 200); }

        private static string Shade(string szPlainText, int szEncryptionKey)
        {
            StringBuilder szInputStringBuild = new(szPlainText);
            StringBuilder szOutStringBuild = new(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }

        #endregion /Info


        #region Repository

        private Zgłoszenie _zgłoszenie = new();

        public Zgłoszenie ZgłoszenieEdycja
        {
            get
            {
                return _zgłoszenie;
            }
            set
            {
                _zgłoszenie = value;
                OnPropertyChanged("ZgłoszenieEdycja");
            }
        }

        public static ObservableCollection<Zgłoszenie> ListaZgłoszeń
        {
            get
            {
                return new ObservableCollection<Zgłoszenie>(ZgłoszeniaRepository.GetZgłoszenia());
            }
        }

        private ListCollectionView MyCollectionView = new(ListaZgłoszeń);
        public ListCollectionView CollectionView
        {
            get
            {
                return MyCollectionView;
            }
            set
            {
                MyCollectionView = value;
                OnPropertyChanged("CollectionView");
            }
        }



        private Zgłoszenie _selectedZgłoszenie;
        public Zgłoszenie SelectedZgłoszenie
        {
            get
            {
                return _selectedZgłoszenie;
            }
            set
            {
                _selectedZgłoszenie = value;
                OnPropertyChanged("SelectedZgłoszenie");
                OnPropertyChanged("CanEdytujBool");
                OnPropertyChanged("CanEdytujBool");
            }
        }

        public double SelectedMiejsceX
        {
            get
            {
                double x = (_selectedZgłoszenie.MiejsceX);//point * 915) - 36 / 2;
                return x;
            }
        }
        public double SelectedMiejsceY
        {
            get
            {
                double y = (_selectedZgłoszenie.MiejsceY);//point * 496) - 36;
                return y;
            }
        }
        public bool SelectedWykonane
        {
            get
            {
                return _selectedZgłoszenie.Wykonane;
            }
        }

        public void SetSelectedItem(int sItem)
        {
            SelectedZgłoszenie = SelectZgłodzenie(sItem);
            //SelectedZgłoszenie = ListaZgłoszeń[ListaZgłoszeń.Where(i => i.Numer == sItem).FirstOrDefault().Numer = sItem-1];
            //SelectedZgłoszenie = (Zgłoszenie)CollectionView.GetItemAt(sItem-1);
        }
        Zgłoszenie SelectZgłodzenie(int sItem)
        {
            Zgłoszenie sZgł = null;
            foreach (Zgłoszenie z in CollectionView)
            {
                if (z.Numer == sItem)
                {
                    sZgł = z;
                    break;
                }
            }
            return (Zgłoszenie)(sZgł);
        }

        #endregion //Repository

        #region Zgłoszenie

        #region 1 Numer zgłoszenia
        public int Numer
        {
            get
            {
                return _zgłoszenie.Numer;
            }
            set
            {
                _zgłoszenie.Numer = value;
                OnPropertyChanged("Numer");
            }
        }
        #endregion //Numer zgłoszenia

        #region 2 Data zgłoszenia
        public DateTime DataZgł
        {
            get
            {
                return _zgłoszenie.DataZgł;
            }
            set
            {
                _zgłoszenie.DataZgł = value;
                OnPropertyChanged("DataZgł");
            }
        }
        #endregion //Data zgłoszenia

        #region 3 Imię
        public string Imię
        {
            get
            {
                return _zgłoszenie.Imię;
            }
            set
            {
                _zgłoszenie.Imię = value;
                OnPropertyChanged("Imię");
            }
        }
        #endregion //Imię

        #region 4 Dział - KTO ZGŁASZA
        public static List<string> DziałyKtoZgłasza => ListFill.CzytajDziały();

        public string DziałKtoZgłasza
        {
            get
            {
                return _zgłoszenie.DziałKtoZgłasza;
            }
            set
            {
                _zgłoszenie.DziałKtoZgłasza = value;
                OnPropertyChanged("DziałKtoZgłasza");
            }
        }
        #endregion //Lista działów KTO ZGŁASZA

        #region 5 Data zdarzenia
        public DateTime DataZdr
        {
            get
            {
                return _zgłoszenie.DataZdr;
            }
            set
            {
                _zgłoszenie.DataZdr = value;
                OnPropertyChanged("DataZdr");
            }
        }
        #endregion //Data zdarzenia

        #region 6 Godzina zdarzenia
        public static List<string> GodzinaZdz => ListFill.Godziny();

        public string GodzinaZdarzenia
        {
            get
            {
                if (_zgłoszenie.GodzinaZdarzenia == null)
                {
                    int _y = DateTime.Now.Hour;
                    int _x = _y - 6;
                    if (_x <= -1)
                    {
                        _x = 18 + _y;
                    }
                    GodzinaZdarzenia = GodzinaZdz[_x];
                    return GodzinaZdz[_x];
                }
                else
                    return _zgłoszenie.GodzinaZdarzenia;
            }
            set
            {
                _zgłoszenie.GodzinaZdarzenia = value;
                OnPropertyChanged("GodzinaZdarzenia");
            }
        }
        #endregion //Lista godzina zdarzenia

        #region 7 MIEJSCE ZDARZENIA
        public static List<string> DziałyMiejsce => ListFill.CzytajMiejsca();

        public string MiejsceZdarzenia
        {
            get
            {
                return _zgłoszenie.MiejsceZdarzenia;
            }
            set
            {
                _zgłoszenie.MiejsceZdarzenia = value;
                OnPropertyChanged("MiejsceZdarzenia");
            }
        }
        public double MiejsceX
        {
            get
            {
                return _zgłoszenie.MiejsceX;
            }
            set
            {
                _zgłoszenie.MiejsceX = value;
                OnPropertyChanged("MiejsceX");
            }
        }
        public double MiejsceY
        {
            get
            {
                return _zgłoszenie.MiejsceY;
            }
            set
            {
                _zgłoszenie.MiejsceY = value;
                OnPropertyChanged("MiejsceY");
            }
        }


        public double MiejsceX2
        {
            get
            {
                return _zgłoszenie.MiejsceX;
            }
        }
        public double MiejsceY2
        {
            get
            {
                return _zgłoszenie.MiejsceY;
            }
        }









        #endregion //MIEJSCE ZDARZENIA

        #region 8 Miejsce opis
        public string Miejsce
        {
            get
            {
                return _zgłoszenie.Miejsce;
            }
            set
            {
                _zgłoszenie.Miejsce = value;
                OnPropertyChanged("Miejsce");
            }
        }
        #endregion //Miejsce opis

        #region 9 Zagrożenie opis
        public string Zagrożenie
        {
            get
            {
                return _zgłoszenie.Zagrożenie;
            }
            set
            {
                _zgłoszenie.Zagrożenie = value;
                OnPropertyChanged("Zagrożenie");
            }
        }
        #endregion //Zagrożenie opis

        #region 10 Działania opis
        public string Działania
        {
            get
            {
                return _zgłoszenie.Działania;
            }
            set
            {
                _zgłoszenie.Działania = value;
                OnPropertyChanged("Działania");
            }
        }
        #endregion //Działania opie    

        #region 11 RODZAJ ZAGROŻENIA
        public static List<string> Zagrożenia => ListFill.CzytajZagrożenia();

        public string RodzajZagrożeń
        {
            get
            {
                return _zgłoszenie.RodzajZagrożeń;
            }
            set
            {
                _zgłoszenie.RodzajZagrożeń = value;
                OnPropertyChanged("RodzajZagrożeń");
            }
        }
        #endregion // RODZAJ ZAGROŻENIA

        #region 12 Skutki opis
        public string SkutkiOpis
        {
            get
            {
                return _zgłoszenie.SkutkiOpis;
            }
            set
            {
                _zgłoszenie.SkutkiOpis = value;
                OnPropertyChanged("SkutkiOpis");
            }
        }
        #endregion //Zagrożenie opis    

        #region 13 SKUTKI Skala
        public static List<string> Skutki => ListFill.CzytajSkutki();

        public string SkutkiSkala
        {
            get
            {
                return _zgłoszenie.SkutkiSkala;
            }
            set
            {
                _zgłoszenie.SkutkiSkala = value;
                double d = 0;
                if (SkutkiSkala == "Bardzo małe") d = 35;
                if (SkutkiSkala == "Małe") d = 28;
                if (SkutkiSkala == "Średnie") d = 21;
                if (SkutkiSkala == "Duże") d = 14;
                if (SkutkiSkala == "Bardzo duże") d = 7;
                _zgłoszenie.Termin = DateTime.Now.AddDays(d);
                OnPropertyChanged("SkutkiSkala");
            }
        }
        #endregion // Lista skutki

        #region 14 ODPOWIEDZIALNY
        public static List<string> Odpowiedzialni => ListFill.CzytajOdpowiedzialnych();

        public string Odpowiedzialny
        {
            get
            {
                return _zgłoszenie.Odpowiedzialny;
            }
            set
            {
                _zgłoszenie.Odpowiedzialny = value;
                OnPropertyChanged("Odpowiedzialny");
            }
        }
        #endregion //ODPOWIEDZIALNY

        #region 15 Enviromental user name
        public string EnvUserName
        {
            get
            {
                return _zgłoszenie.EnvUserName;
            }
            set
            {
                _zgłoszenie.EnvUserName = value;
                OnPropertyChanged("EnvUserName");
            }
        }
        #endregion //Enviromental user name

        #region 16 Zdjęcie
        public string ZdjęciePath
        {
            get
            {
                return _zgłoszenie.ZdjęciePath;
            }
            set
            {
                _zgłoszenie.ZdjęciePath = value;
                OnPropertyChanged("ZdjęciePath");
            }
        }
        #endregion //Zdjęcie

        #region 17 Termin wykonania
        public DateTime Termin
        {
            get
            {
                return _zgłoszenie.Termin;
            }
            set
            {
                _zgłoszenie.Termin = value;
                OnPropertyChanged("Termin");
            }
        }
        #endregion //Termin Wykonania

        #region 18 Wykonano
        public bool Wykonane
        {
            get
            {
                return _zgłoszenie.Wykonane;
            }
            set
            {
                _zgłoszenie.Wykonane = value;
                OnPropertyChanged("Wykonane");
            }
        }
        #endregion //Wykonano

        #region 19 Wykonano zgłaszający
        public string WykonanoZgłaszający
        {
            get
            {
                return _zgłoszenie.WykonanoZgłaszający;
            }
            set
            {
                _zgłoszenie.WykonanoZgłaszający = value;
                OnPropertyChanged("WykonanoZgłaszający");
            }
        }
        #endregion // Wykonano zgłaszający

        #endregion // Zgłoszenie

        #region Class Init
        public ZPW()
        {
            InitializeComands();
            _zgłoszenie.Numer = 0;
            _zgłoszenie.DataZgł = DateTime.Now;
            _zgłoszenie.DataZdr = DateTime.Now;
            _zgłoszenie.Wykonane = false;
            _zgłoszenie.EnvUserName = EnviromentUserName;
            _zgłoszenie.WykonanoZgłaszający = "";
            _zgłoszenie.ZdjęciePath = "";
        }
        #endregion //Class Init

        #region Commands
        private void InitializeComands()
        {
            SetLocation = new MyICommand(OnSetLocation, CanSetLocation);
            ShowMap = new MyICommand(OnShowMap, CanShowMap);
            ShowMiniMap = new MyICommand(OnShowMiniMap, CanShowMiniMap);
            CloseWindow = new MyICommand(OnCloseWindow, CanCloseWindow);
            WindowChangeState = new MyICommand(OnWindowChangeState, CanWindowChangeState);
            LoadPicture = new MyICommand(OnLoadPicture, CanLoadPicture);
            UpdateZgłoszenie = new MyICommand(OnUpdate, CanUpdate);
            Edytuj = new MyICommand(OnEdytuj);
            NoweZgłoszenie = new MyICommand(OnNoweZgłoszenie, CanNoweZgłoszenie);
            SaveZgłoszenie = new MyICommand(OnSave, CanSave);
            Szukaj = new MyICommand(OnSzukaj, CanSzukaj);
            Sortuj = new MyICommand(OnSortuj, CanSortuj);
            Oprogramie = new MyICommand(OnOprogramie, CanOprogramie);
            Grupuj = new MyICommand(OnGrupuj, CanGrupuj);
            Filtruj = new MyICommand(OnFiltruj, CanFiltruj);
            FiltrujWgDaty = new MyICommand(OnFiltrujWgDaty);
            ZapiszFiltr = new MyICommand(OnZapiszFiltr, CanZapiszFiltr);
            Mailem = new MyICommand(OnMailem, CanMailem);
            PokażObserwowane = new MyICommand(OnPokażObserwowane, CanPokażObserwowane);
            PokażMoje = new MyICommand(OnPokażMoje, CanPokażMoje);
            Opcje = new MyICommand(OnOpcje, CanOpcje);
            ViewChange = new MyICommand(OnViewChange, CanViewChange);
            ChartChange = new MyICommand(OnChartChange, CanChartChange);
            OpenImageViewer = new MyICommand(OnOpenImageViewer, CanOpenImageViewer);
        }

        #region SetLocation

        public MyICommand SetLocation { get; set; }

        private void OnSetLocation(object parameter)
        {
            System.Windows.Point p = Mouse.GetPosition((IInputElement)parameter);
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)parameter;
            MiejsceX = p.X * (1675.6363 / img.ActualWidth);////(p.X / img.ActualWidth);
            MiejsceY = p.Y * (942.5454 / img.ActualHeight);////(p.Y / img.ActualHeight);
            MessageBoxResult selectedOption = MessageBox.Show("Czy wybrane miejsce jest prawidłowe?", "Wybór miejsca...", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (selectedOption == MessageBoxResult.Yes)
            {
                Window parentWindow = Window.GetWindow((DependencyObject)parameter);
                parentWindow.Close();
            }
        }

        private bool CanSetLocation()
        {
            return true;
        }
        #endregion // ShowSelected

        #region ShowMap
        public MyICommand ShowMap { get; set; }

        private void OnShowMap(object parameter)
        {
            var selectedOption = MessageBox.Show("Aby wskazać miejsce kliknij lewym przyciskiem myszy na mapie.", "Wybór miejsca...", MessageBoxButton.OK, MessageBoxImage.Information);
            if (selectedOption == MessageBoxResult.OK)
            {
                ////ZPW.ViewModel.ZPW zpw = Resources["zpw"] as ZPW.ViewModel.ZPW;
                ////ZPW viewModel = ZPWView.DataContextProperty;
                Window mapWindow = new Mapa();
                ////mapWindow.DataContext = zpw;
                ////mapWindow.DataContext = viewModel;
                mapWindow.Show();
            }
        }

        private bool CanShowMap()
        {
            return true;
        }
        #endregion // ShowMap

        #region ShowMiniMap
        public MyICommand ShowMiniMap { get; set; }

        private void OnShowMiniMap(object parameter)
        {
            Window mapWindow = new MiniMap();
            mapWindow.Show();
        }

        private bool CanShowMiniMap()
        {
            return true;
        }
        #endregion // ShowMiniMap

        #region CloseWindow
        public MyICommand CloseWindow { get; set; }

        private void OnCloseWindow(object parameter)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)parameter);
            parentWindow.Close();
        }

        private bool CanCloseWindow()
        {
            return true;//SelectedItemKto != null;
        }
        #endregion // CloseWindow

        #region WindowChangeState
        public MyICommand WindowChangeState { get; set; }

        private void OnWindowChangeState(object parameter)
        {
            Window parentWindow = (Window)parameter;
            if (parentWindow.WindowState == System.Windows.WindowState.Normal)
            {
                parentWindow.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                parentWindow.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private bool CanWindowChangeState()
        {
            return true;
        }
        #endregion // WindowChangeState

        #region Load Picture
        public MyICommand LoadPicture { get; set; }

        private void OnLoadPicture(object parameter)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Pliki graficzne (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            string FilePathToCopy = openFileDialog.FileName;
            string AppPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Dane\\";
            string FileNameToCopy = System.IO.Path.GetFileName(FilePathToCopy);
            string FilePathToPasteLarge = AppPath + FileNameToCopy;
            string FilePathToPasteSmall = AppPath + "Miniatury\\" + FileNameToCopy;

            if (!String.IsNullOrEmpty(FilePathToCopy))
            {
                try
                {
                    Bitmap nBp = new(FilePathToCopy);
                    //ImageHandler imgH = new();
                    ImageHandler.Save(nBp, 400, 400, 70, FilePathToPasteSmall);
                    ImageHandler.Save(nBp, 1920, 1080, 80, FilePathToPasteLarge);
                    //File.Copy(FilePathToCopy, FilePathToPaste, true);
                }
                catch
                {
                    MessageBox.Show("Ten plik dodano już do zgłoszenia, lub jest problem z jego zapisem!");
                }
                ZdjęciePath = FileNameToCopy;
            }
        }

        private bool CanLoadPicture()
        {
            return true;
        }
        #endregion // ShowMap

        #region UpdateZgłoszenie
        public MyICommand UpdateZgłoszenie { get; set; }

        private void OnUpdate(object parameter)
        {
            MessageBoxResult selectedOption = MessageBox.Show("Czy chcesz zgłosić wykonanie działań?", "Zgłoszenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (selectedOption == MessageBoxResult.Yes)
            {
                SelectedZgłoszenie.Wykonane = true;
                SelectedZgłoszenie.WykonanoZgłaszający = Environment.UserName.ToString();
                ZgłoszeniaRepository.Update(SelectedZgłoszenie);
                ////MessageBox.Show(ListaZgłoszeń.Where(i => i.Numer == SelectedZgłoszenie.Numer).FirstOrDefault().Wykonane.ToString());
                ////ListaZgłoszeń.Where(i => i.Numer == SelectedZgłoszenie.Numer).FirstOrDefault().WykonanoZgłaszający = SelectedZgłoszenie.WykonanoZgłaszający;
                ////((DataGrid)parameter).GetBindingExpression(DataGrid.ItemsSourceProperty).UpdateTarget();
                CollectionView.Refresh();
            }
        }

        private bool CanUpdate()
        {
            return true; //!SelectedZgłoszenie.Wykonane;
        }

        #endregion // UpdateZgłoszenie

        #region Edytuj
        public MyICommand Edytuj { get; set; }

        private void OnEdytuj(object parameter)
        {
            ZgłoszenieEdycja = SelectedZgłoszenie;
            try { Application.Current.Windows[2].Close(); }
            catch { }
            Window ZPWWindow = new ZPWView();
            ////ComboBox cb = Application.Current.Windows[2].FindName("ComboKto") as ComboBox;
            ////cb.SelectedIndex = IndexOf(cb, SelectedZgłoszenie.DziałKtoZgłasza);
            ZPWWindow.Show();
        }

        public bool CanEdytujBool
        {
            get
            {
                if (SelectedZgłoszenie == null)
                    return false;

                if (SelectedZgłoszenie.Wykonane == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion // Edytuj

        #region NoweZgłoszenie
        public MyICommand NoweZgłoszenie { get; set; }

        private void OnNoweZgłoszenie(object parameter)
        {
            OpenNoweZgłoszenie();
        }

        public void OpenNoweZgłoszenie()
        {
            Zgłoszenie NoweZgłoszenie = new();
            NoweZgłoszenie.Numer = -1;
            NoweZgłoszenie.Imię = CurrentUserName;
            NoweZgłoszenie.DataZgł = DateTime.Now;
            NoweZgłoszenie.DataZdr = DateTime.Now;
            NoweZgłoszenie.Wykonane = false;
            NoweZgłoszenie.EnvUserName = EnviromentUserName;
            NoweZgłoszenie.WykonanoZgłaszający = "";
            NoweZgłoszenie.ZdjęciePath = "";
            ZgłoszenieEdycja = NoweZgłoszenie;
            Window ZPWWindow = new ZPWView();
            ZPWWindow.Show();
        }

        private bool CanNoweZgłoszenie()
        {
            return true;
        }

        #endregion // NoweZgłoszenie

        #region SaveZgłoszenie
        public MyICommand SaveZgłoszenie { get; set; }

        private void OnSave(object parameter)
        {
            if (FieldsFilled == true)
            {
                if (MiejsceX + MiejsceY == 0)
                {
                    MessageBox.Show("Aby wysłać zgłoszenie zaznacz miejsce zdarzenia na mapie!", "Brak wymaganych danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Window mapWindow = new Mapa();
                    mapWindow.Show();
                }
                else
                    InsertExit();
            }
            else
                MessageBox.Show("Aby wysłać zgłoszenie wypełnij wszystkie pola formularza!", "Brak wymaganych danych", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void InsertExit()
        {
            ZgłoszeniaRepository.InsertUpdate(_zgłoszenie);

            if (_zgłoszenie.Numer == 0)
                Environment.Exit(0);
            if (_zgłoszenie.Numer == -1)
            {
                try
                {
                    Application.Current.Windows[1].Close();
                }
                catch
                {
                    Application.Current.Windows[0].Close();
                }
                finally { }
                CollectionView = new ListCollectionView(ListaZgłoszeń);
                UseCurrentFilter();
                SortujKolekcje();
                GrupujKolekcje();
                FilterSearchString(SearchString);
            }
            else
            {
                Application.Current.Windows[1].Close();
            }
            CollectionView.Refresh();
        }

        private bool CanSave()
        {
            return true;
        }

        private bool FieldsFilled
        {
            get
            {
                if (String.IsNullOrEmpty(Imię))
                    return false;
                if (String.IsNullOrEmpty(DziałKtoZgłasza))
                    return false;
                if (String.IsNullOrEmpty(GodzinaZdarzenia))
                    return false;
                if (String.IsNullOrEmpty(MiejsceZdarzenia))
                    return false;
                if (String.IsNullOrEmpty(Miejsce))
                    return false;
                if (String.IsNullOrEmpty(Zagrożenie))
                    return false;
                if (String.IsNullOrEmpty(Działania))
                    return false;
                if (String.IsNullOrEmpty(RodzajZagrożeń))
                    return false;
                if (String.IsNullOrEmpty(SkutkiOpis))
                    return false;
                if (String.IsNullOrEmpty(SkutkiSkala))
                    return false;
                if (String.IsNullOrEmpty(Odpowiedzialny))
                    return false;
                return true;
            }
        }

        ////private void RefreshAll()
        ////{

        ////ContentControl cctrl = (ContentControl)Application.Current.Windows[0].FindName("ctrlContent");
        ////UserControl uctrl = (UserControl)cctrl.Content;
        ////try
        ////{
        ////    ((DataGrid)uctrl.FindName("ZgłoszeniaGrid")).Items.Refresh();
        ////}
        ////catch{}

        ////((DataGrid)Application.Current.Windows[0].FindName("ZgłoszeniaGrid")).Items.Refresh();
        ////((DataGrid)parameter).Items.Refresh();
        ////}


        #endregion // SaveZgłoszenie

        #region Szukaj
        public MyICommand Szukaj { get; set; }

        private void SzukajMnie()
        {
            string searchMe = SearchString.ToLower();
            FilterSearchString(searchMe);
            //CollectionView.Filter  = (e) =>
            //    {

            //    Zgłoszenie emp = e as Zgłoszenie;
            //    if (
            //            emp.Działania.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.Miejsce.ToString().ToLower().Contains(searchMe) ||        
            //            DateToStringCheck(emp).Contains(searchMe) ||
            //            emp.MiejsceZdarzenia.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.Numer.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.Odpowiedzialny.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.SkutkiOpis.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.Zagrożenie.ToString().ToLower().Contains(searchMe) ||                       
            //            emp.RodzajZagrożeń.ToString().ToLower().Contains(searchMe)
            //        )
            //            return true;
            //        return false;
            //    };

            if (CollectionView.Count == 0)
            {
                ////SearchString = SearchString.Substring(0,SearchString.Length-1);
                if (SearchString.Length > 0) SearchString = SearchString[0..^1];
                tp.Content = "Brak wyników wyszukiwania.";
                tp.IsOpen = true;
            }
            //CollectionView.Refresh();
            //OnPropertyChanged("CollectionView");
        }

        private static readonly ToolTip tp = new();

        private static string DateToStringCheck(Zgłoszenie parameter)
        {

            string strd = parameter.DataZgł.ToString("d", CultureInfo.CreateSpecificCulture("pl-PL"));
            if (strd.Contains("-"))
            {
                string[] strdSpl = strd.Split("-");
                strd = strdSpl[0] + "." + strdSpl[1] + "." + strdSpl[2];
                if (strd.IndexOf('.') == 4)
                {
                    strd = strdSpl[2] + "." + strdSpl[1] + "." + strdSpl[0];
                }
            }
            return strd;
        }

        private void OnSzukaj(object parameter)
        {
            if (SearchString == "") CollectionView.Refresh();
            HighLightString = SearchString;
            OnPropertyChanged("HighLightString");
            tp.IsOpen = false;
        }


        private bool CanSzukaj()
        {
            return true;
        }

        #endregion // Szukaj

        #region Sortuj
        public MyICommand Sortuj { get; set; }
        private void OnSortuj(object parameter)
        {
            if (parameter.ToString() == "Rosnąco")
                AscSort = true;

            if (parameter.ToString() == "Malejąco")
                AscSort = false;

            if (parameter.ToString() != "" && parameter.ToString() != "Rosnąco" && parameter.ToString() != "Malejąco")
                Sortowanie = parameter.ToString();
            SortujKolekcje();
        }

        private void SortujKolekcje()
        {
            CollectionView.SortDescriptions.Clear();
            if (coSortować != "") CollectionView.SortDescriptions.Add(new SortDescription(coSortować, kierunekSortowania));
            CollectionView.Refresh();
            OnPropertyChanged("CollectionView");
        }

        private bool CanSortuj()
        {
            return true;
        }

        #endregion // Sortuj

        #region Oprogramie
        public MyICommand Oprogramie { get; set; }

        private void OnOprogramie(object parameter)
        {
            Window OWindow = new OProgramie();
            OWindow.Show();
        }

        private bool CanOprogramie()
        {
            return true;
        }
        #endregion // Oprogramie

        #region Grupuj
        public MyICommand Grupuj { get; set; }

        private void OnGrupuj(object parameter)
        {
            Grupowanie = parameter.ToString();
            GrupujKolekcje();
        }

        void GrupujKolekcje()
        {
            CollectionView.GroupDescriptions.Clear();
            if (coGrupować != "") CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(coGrupować));
            CollectionView.Refresh();
            OnPropertyChanged("CollectionView");
        }

        private bool CanGrupuj()
        {
            return true;
        }
        #endregion // Grupuj



        #region Filtruj

        private readonly Dictionary<string, Predicate<Zgłoszenie>> filters = new();

        private void ClearFilters()
        {
            filters.Clear();
            CurrentFiltr = null;
            pokażTylkoMoje = false;
            SearchString = "";
            HighLightString = SearchString;
            PokażObserwowaneZgłoszenia = false;
            PokażMojeZgłoszenia = false;
            PokażNowyFiltr = false;
            OnPropertyChanged("HighLightString");
            CollectionView.Refresh();
            OnPropertyChanged("CollectionView");
        }

        private void RemoveFilter(string filterName)
        {
            //if (
            filters.Remove(filterName);//)
            //CollectionView.Refresh();
        }

        private bool FilterZgłoszenia(object obj)
        {
            Zgłoszenie c = (Zgłoszenie)obj;
            return filters.Values
                .Aggregate(true, (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilter(string name, Predicate<Zgłoszenie> predicate)
        {
            if (filters.Keys.Contains(name))
                RemoveFilter(name);
            filters.Add(name, predicate);
            CollectionView.Filter = FilterZgłoszenia;
        }

        private void FilterEnvUserName()
        {
            AddFilter("EnvUserName", zgłoszenie => zgłoszenie.EnvUserName.Contains(EnvUserName));
        }

        private void FilterSearchString(string val)
        {
            try
            {

                AddFilter("FilterSearchString", zgłoszenie => zgłoszenie.StringToSearch.ToLower().Contains(val));
            }
            catch { }
        }

        private void FilterDziałZgłaszający(List<string> val)
        {
            if (val != null)
                AddFilter("FilterDziałZgłaszający",
                    zgłoszenie =>
                    {
                        foreach (string item in val)
                            if (zgłoszenie.DziałKtoZgłasza == item) return true;
                        return false;
                    });
        }

        private void FilterDataOdDo(DateTime dataOd, DateTime dataDo)
        {
            AddFilter("FilterDataOdDo",
                zgłoszenie =>
                {
                    if (zgłoszenie.DataZdr >= dataOd && zgłoszenie.DataZdr <= dataDo) return true;
                    return false;
                });
        }

        private void FilterOstatnieXDni(int val)
        {
            AddFilter("FilterOstatnieXDni",
                zgłoszenie =>
                {
                    if (zgłoszenie.DataZdr >= DateTime.Now.AddDays(-val)) return true;
                    return false;
                });
        }
        private void FilterMiejsceZdarzenia(List<string> val)
        {
            if (val != null)
                AddFilter("FilterMiejsceZdarzenia",
                    zgłoszenie =>
                    {
                        foreach (string item in val)
                            if (zgłoszenie.MiejsceZdarzenia == item) return true;
                        return false;
                    });
        }

        private void FilterRodzajZagrożenia(List<string> val)
        {
            if (val != null)
                AddFilter("FilterRodzajZagrożenia",
                    zgłoszenie =>
                    {
                        foreach (string item in val)
                            if (zgłoszenie.RodzajZagrożeń == item) return true;
                        return false;
                    });
        }

        private void FilterOcena(List<string> val)
        {
            if (val != null)
                AddFilter("FilterOcena",
                    zgłoszenie =>
                    {
                        foreach (string item in val)
                            if (zgłoszenie.SkutkiSkala == item) return true;
                        return false;
                    });
        }

        private void FilterOdpowiedzialni(List<string> val)
        {
            if (val != null)
                AddFilter("FilterOdpowiedzialni",
                    zgłoszenie =>
                    {
                        foreach (string item in val)
                            if (zgłoszenie.Odpowiedzialny == item) return true;
                        return false;
                    });
        }

        private void UseCurrentFilter()
        {
            if (pokażTylkoMoje) FilterEnvUserName();
            else
            if (CurrentFiltr != null)
            {
                try{ if (CurrentFiltr.FiltrDział.Count > 0) FilterDziałZgłaszający(CurrentFiltr.FiltrDział);} catch{}
                try{ if (CurrentFiltr.FiltrWgDat) FilterDataOdDo(CurrentFiltr.FiltrDataOd, CurrentFiltr.FiltrDataDo);} catch{}
                try{ if (CurrentFiltr.FiltrWgDni) FilterOstatnieXDni(CurrentFiltr.FiltrLiczbaDni);} catch{}
                try{ if (CurrentFiltr.FiltrMiejsce.Count > 0) FilterMiejsceZdarzenia(CurrentFiltr.FiltrMiejsce);} catch{}
                try{ if (CurrentFiltr.FiltrRodzajZagrożenia.Count > 0) FilterRodzajZagrożenia(CurrentFiltr.FiltrRodzajZagrożenia);} catch{}
                try{ if (CurrentFiltr.FiltrSkutki.Count > 0) FilterOcena(CurrentFiltr.FiltrSkutki);} catch{}
                try{ if (CurrentFiltr.FiltrOdpowiedzialni.Count > 0) FilterOdpowiedzialni(CurrentFiltr.FiltrOdpowiedzialni);} catch{}
                //TODO-LATER null refrance exception?
            }
            CollectionView.Refresh();
            OnPropertyChanged("CollectionView");
        }

        // Zapis odczyt filtrów
        private readonly static string _nowyFiltr = "Nowy.ZPWfilter";
        private readonly static string _obserwowaneFiltr = "Obserwowane.ZPWfilter";
        private Filtr _NowyFiltr = Filtry.FiltrDeserialize(_nowyFiltr);
        private Filtr _ObserwowaneFiltr = Filtry.FiltrDeserialize(_obserwowaneFiltr);
        private Filtr _CurrentFiltr = Filtry.FiltrDeserialize(_nowyFiltr);

        public Filtr CurrentFiltr
        {
            get
            {
                return _CurrentFiltr;
            }
            set
            {
                _CurrentFiltr = value;
                OnPropertyChanged("CurrentFiltr");
            }
        }

        public MyICommand Filtruj { get; set; }

        private void OnFiltruj(object parameter)
        {
            if (((string)parameter) == "Nowy filtr")
            {
                CurrentFiltr = _NowyFiltr;
                Window OWindow = new EdycjaFiltra();
                OWindow.Show();
            }
            if (((string)parameter) == "Wyczyść filtrowanie")
            {
                ClearFilters();
            }
        }

        private bool CanFiltruj()
        {
            return true;
        }

        public MyICommand ZapiszFiltr { get; set; }

        private void OnZapiszFiltr(object parameter)
        {
            if (parameter.ToString() == "ZapiszJakoObserwowane")
            {
                Filtry.FiltrSerialize(CurrentFiltr, _obserwowaneFiltr);
                _ObserwowaneFiltr = Filtry.FiltrDeserialize(_obserwowaneFiltr);
                ClearFilters();
                VoidPokażObserwowaneZgłoszenia();
            }
            if (parameter.ToString() == "ZapiszJakoNowyFiltr")
            {
                Filtry.FiltrSerialize(CurrentFiltr, _nowyFiltr);
                ClearFilters();                 
                _NowyFiltr = Filtry.FiltrDeserialize(_nowyFiltr);
                CurrentFiltr = _NowyFiltr;
                PokażNowyFiltr = true;
                UseCurrentFilter();
            }
            Application.Current.Windows[1].Close();
        }



        private bool CanZapiszFiltr()
        {
            return true;
        }

        public MyICommand FiltrujWgDaty { get; set; }

        private void OnFiltrujWgDaty(object parameter)
        {
            if ((string)parameter == "Dni")
            {
                if (CurrentFiltr.FiltrWgDat) CurrentFiltr.FiltrWgDat = false;
            }
            if ((string)parameter == "Daty")
            {
                if (CurrentFiltr.FiltrWgDni) CurrentFiltr.FiltrWgDni = false;
            }
            OnPropertyChanged("CurrentFiltr");
        }

        public static List<int> LiczbaDni { get => new() { 7, 15, 30, 45, 90, 180 }; }

        
        private bool pokażNowyFiltr = false;
        
        public bool PokażNowyFiltr
        {
            get
            {
                return pokażNowyFiltr;
            }
            set
            {
                pokażNowyFiltr = value;
                FiltersPropertysChanged();
            }
        }

        public void FiltersPropertysChanged()
        {
            OnPropertyChanged("PokażMojeZgłoszenia");
            OnPropertyChanged("PokażObserwowaneZgłoszenia");
            OnPropertyChanged("PokażNowyFiltr");
        }

        #endregion // Filtruj


        #region Wyślij Mailem
        public MyICommand Mailem { get; set; }

        private void OnMailem(object parameter)
        {
            //TODO - wysyłanie mailem
        }

        private bool CanMailem()
        {
            return true;
        }
        #endregion // Wyślij mailem

        #region Obserwowane

        public bool PokażObserwowaneZgłoszenia
        {
            get
            {
                return pokażTylkoObserwowane;
            }
            set
            {
                if (PokażMojeZgłoszenia) PokażMojeZgłoszenia = false;
                pokażTylkoObserwowane = value;
                FiltersPropertysChanged();
            }
        }



        private bool pokażTylkoObserwowane = false;
        public MyICommand PokażObserwowane { get; set; }

        private void OnPokażObserwowane(object parameter)
        {
            VoidPokażObserwowaneZgłoszenia();
        }
        private void VoidPokażObserwowaneZgłoszenia()
        {
            if (PokażObserwowaneZgłoszenia)
            {
                ClearFilters();
                UseCurrentFilter();
            }
            else
            {
                string subdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Rejestr ZPW";
                string filePath = subdir + @"\" + _obserwowaneFiltr;
                if (File.Exists(filePath))
                {
                    ClearFilters();
                    PokażObserwowaneZgłoszenia = true;
                    CurrentFiltr = _ObserwowaneFiltr;
                    UseCurrentFilter();
                }
                else
                {
                    PokażObserwowaneZgłoszenia = false;
                    MessageBox.Show("Aby utworzyć filtr OBSERWOWANE ZGŁOSZENIA przejdź do menu:" + Environment.NewLine + " 'Dane/Filtruj/Nowy filtr'." + Environment.NewLine + "Następnie wybierz kryteria wyszukiwania i kliknij przycisk " + Environment.NewLine + "'Zapisz jako OBSERWOWANE'.", "Brak kryteriów wyszukiwania", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ClearFilters();
                }
            }
        }


        private bool CanPokażObserwowane()
        {
            return true;
        }
        #endregion // PokażObserwowane

        #region Pokaż Moje

        public bool PokażMojeZgłoszenia
        {
            get
            {
                return pokażTylkoMoje;
            }
            set
            {
                if (PokażObserwowaneZgłoszenia) PokażObserwowaneZgłoszenia = false;
                pokażTylkoMoje = value;
                FiltersPropertysChanged();
            }
        }
        private bool pokażTylkoMoje = false;
        public MyICommand PokażMoje { get; set; }

        private void OnPokażMoje(object parameter)
        {
            if (PokażMojeZgłoszenia)
            {
                ClearFilters();
                PokażMojeZgłoszenia = false;
                UseCurrentFilter();
            }
            else
            {
                var itm = ListaZgłoszeń.Cast<Zgłoszenie>().OrderBy(itm => itm.EnvUserName).FirstOrDefault(itm => itm.EnvUserName.Contains(EnviromentUserName));
                if (itm == null)
                {
                    tp.Content = "Brak wyników wyszukiwania.";
                    tp.IsOpen = true;
                }
                else
                {
                    ClearFilters();
                    PokażMojeZgłoszenia = true;
                    UseCurrentFilter();
                }
                tp.IsOpen = false;
            }
        }

        private bool CanPokażMoje()
        {
            return true;
        }
        #endregion // PokażMoje


        #region Opcje
        public MyICommand Opcje { get; set; }

        private void OnOpcje(object parameter)
        {
            //TODO-LATER- opcje
        }

        private bool CanOpcje()
        {
            return true;
        }
        #endregion // Opcje

        #region OpenImageViewer
        public MyICommand OpenImageViewer { get; set; }

        private void OnOpenImageViewer(object parameter)
        {
            //TODO - open image viewer
            MessageBox.Show(parameter.ToString());
            //CollectionView.IndexOf();
        }

        private bool CanOpenImageViewer()
        {
            return true;
        }
        #endregion // Opcje

        #region Search, filter, group, sort

        #region Search
        private string _searchString = "";
        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                SzukajMnie();
                OnPropertyChanged("SearchString");
            }
        }
        public string HighLightString { get; set; }

        #endregion //Search

        #region Group
        private string coGrupować = "";

        public string Grupowanie
        {
            get
            {
                return coGrupować;
            }
            set
            {
                coGrupować = value;
                OnPropertyChanged("Grupowanie");
            }
        }

        #endregion //Group

        #region Sort
        private System.ComponentModel.ListSortDirection kierunekSortowania = ListSortDirection.Ascending;
        private string coSortować = "";

        public string Sortowanie
        {
            get
            {
                return coSortować;
            }
            set
            {
                coSortować = value;
                OnPropertyChanged("Sortowanie");
            }
        }

        public bool AscSort
        {
            get
            {
                if (kierunekSortowania == ListSortDirection.Ascending)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    kierunekSortowania = ListSortDirection.Ascending;
                if (!value)
                    kierunekSortowania = ListSortDirection.Descending;
                OnPropertyChanged("AscSort");
            }
        }
        #endregion //Sort

        #endregion //Search, filter, group, sort

        #endregion //Commands

        #region Views



        private UserControl _userControlToShow = new ViewTabela();

        public UserControl UserControlToShow
        {
            get
            {
                return _userControlToShow;
            }
            set
            {
                _userControlToShow = value;
                OnPropertyChanged("UserControlToShow");
            }
        }

        public MyICommand ViewChange { get; set; }

        private void OnViewChange(object parameter)
        {
            UserControlToShow = null;
            UserControl UserControlToShow2 = new ViewMapa(); ;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            if ((string)parameter == "Tabela") UserControlToShow = new ViewTabela();
            if ((string)parameter == "Lista") UserControlToShow = new ViewLista();
            if ((string)parameter == "Karty") UserControlToShow = new ViewKafle();
            if ((string)parameter == "Zgłoszenie") UserControlToShow = new ViewZgłoszenie();
            if ((string)parameter == "Mapa") UserControlToShow = UserControlToShow2;
            if ((string)parameter == "Statystyki") {UserControlToShow = new ViewStatystyki(); UChart0 = _chart1;}
            SortujKolekcje();
            View = (string)parameter;
            // TODO - sprawdzić co po zmianie widoku?
        }
 
        private string _View = "Tabela";
        public string View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        private bool CanViewChange()
        {
            return true;
        }

        #endregion // Views

        #region Charts
        private UserControl _chart0 = new Chart0();
        private UserControl _chart1 = new Chart1();
        private UserControl _chart2 = new Chart2();
        private UserControl _chart3 = new Chart3();
        private UserControl _chart4 = new Chart4();
        public UserControl UChart0
        {
            get
            {
                return _chart0;
            }
            set
            {
                _chart0 = value;
                OnPropertyChanged("UChart0");
            }
        }

        public UserControl UChart1
        {
            get
            {
                return _chart1;
            }
            set
            {
                _chart1 = value;
                OnPropertyChanged("UChart1");
            }
        }
        public UserControl UChart2
        {
            get
            {
                return _chart2;
            }
            set
            {
                _chart2 = value;
                OnPropertyChanged("UChart2");
            }
        }
        public UserControl UChart3
        {
            get
            {
                return _chart3;
            }
            set
            {
                _chart3 = value;
                OnPropertyChanged("UChart3");
            }
        }
        public UserControl UChart4
        {
            get
            {
                return _chart4;
            }
            set
            {
                _chart4 = value;
                OnPropertyChanged("UChart4");
            }
        }

        public MyICommand ChartChange { get; set; }

        private void OnChartChange(object parameter)
        {
            UChart0 = null;
            if ((string)parameter == "1")
                UChart0 = new Chart1();
            if ((string)parameter == "2")
                UChart0 = new Chart2();
            if ((string)parameter == "3")
                UChart0 = new Chart3();
            if ((string)parameter == "4")
                UChart0 = new Chart4();
        }

        private bool CanChartChange()
        {
            return true;
        }

        #endregion // Charts

    }
}