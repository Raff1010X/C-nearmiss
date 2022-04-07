using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace ZPW.Model
{
    public static class ConnectionString
    {
        public static readonly string ConStr = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ZPWdata2.db;Version=3;";
        public static readonly string ConStrRepository = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\ZPWdata1.db;Version=3;";
    }

    #region ListFill
    class ListFill
    {
        #region 4 Lista działów KTO ZGŁASZA
        public static List<string> CzytajDziały()
        {
            ListRead d = new("Dzialy", "Dzial");
            return d.Lista;
        }
        #endregion //Lista działów KTO ZGŁASZA

        #region 6 Lista godzina zdarzenia
        private static readonly List<string> lista = new();
        public static List<string> Godziny()
        {
            lista.Clear();
            lista.Add("6 - pierwsza zmiana");
            lista.Add("7 - pierwsza zmiana");
            lista.Add("8 - pierwsza zmiana");
            lista.Add("9 - pierwsza zmiana");
            lista.Add("10 - pierwsza zmiana");
            lista.Add("11 - pierwsza zmiana");
            lista.Add("12 - pierwsza zmiana");
            lista.Add("13 - pierwsza zmiana");
            lista.Add("14 - druga zmiana");
            lista.Add("15 - druga zmiana");
            lista.Add("16 - druga zmiana");
            lista.Add("17 - druga zmiana");
            lista.Add("18 - druga zmiana");
            lista.Add("19 - druga zmiana");
            lista.Add("20 - druga zmiana");
            lista.Add("21 - druga zmiana");
            lista.Add("22 - trzecia zmiana");
            lista.Add("23 - trzecia zmiana");
            lista.Add("24 - trzecia zmiana");
            lista.Add("1 - trzecia zmiana");
            lista.Add("2 - trzecia zmiana");
            lista.Add("3 - trzecia zmiana");
            lista.Add("4 - trzecia zmiana");
            lista.Add("5 - trzecia zmiana");
            return lista;
        }
        #endregion //Lista godzina zdarzenia

        #region 7 Lista działów MIEJSCE ZDARZENIA
        public static List<string> CzytajMiejsca()
        {
            ListRead d = new("Miejsca", "Miejsca");
            return d.Lista;
        }
        #endregion //Lista działów MIEJSCE ZDARZENIA

        #region 11 Lista działów RODZAJ ZAGROŻENIA
        public static List<string> CzytajZagrożenia()
        {
            ListRead d = new("Zagrożenia", "Rodzaj");
            return d.Lista;
        }
        #endregion //Lista działów RODZAJ ZAGROŻENIA

        #region 13 Lista SKUTKI
        private static readonly List<string> listas = new();
        public static List<string> CzytajSkutki()
        {
                listas.Clear();
                listas.Add("Bardzo małe");
                listas.Add("Małe");
                listas.Add("Średnie");
                listas.Add("Duże");
                listas.Add("Bardzo duże");
                return listas;
        }
        #endregion // Lista skutki 

        #region 14 Lista ODPOWIEDZIALNI
        public static List<string> CzytajOdpowiedzialnych()
        {
            ListRead d = new("Odpowiedzialni", "Odpowiedzialny");
            return d.Lista;
        }
        #endregion //Lista ODPOWIEDZIALNI
    }

    #region ListLoader       
    
    public class ListRead
    {
        private readonly List<string> lista = new();

        public List<string> Lista { get; set; }

        public ListRead(string tabela, string kolumna)
        {
            Load(tabela, kolumna);
        }

        public void Load(string tabela, string kolumna)
        {
            SQLiteConnection con = new(ConnectionString.ConStr);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + tabela;
            con.Open();
            SQLiteDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                lista.Add(myReader[kolumna].ToString());
            }
            con.Close();
            Lista = lista;
        }
    }
    #endregion // ListFill

    #endregion //ListData

    #region ZgłoszeniaRepository

    public class ZgłoszeniaRepository
    {
        public static List<Zgłoszenie> GetZgłoszenia()
        {
            // Simlified using
            using IDbConnection db = new SQLiteConnection(ConnectionString.ConStrRepository);
                string Sql = @"SELECT * FROM Zgłoszenia";
                var output = db.Query<Zgłoszenie>(Sql).ToList();
                return output;
        }

        public static int LiczbaZgłoszeńWykonanych()
        {
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new("SELECT COUNT(*) FROM Zgłoszenia WHERE Wykonane = true", con);
            con.Open();
            int count = (int)(long)cmd.ExecuteScalar();
            con.Close();
            return count;
        }        
        public static int LiczbaZgłoszeń()
        {
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new("SELECT COUNT(*) FROM Zgłoszenia", con);
            con.Open();
            int count = (int)(long)cmd.ExecuteScalar();
            con.Close();
            return count;
        }

        public static System.Collections.Generic.IEnumerable<double> LiczbaZgłoszeńNaMiesiąc()
        {
            DateTime now;    
            DateTime firstDayOfMonth;
            DateTime lastDayOfMonth;
            double[] countArray = new double[12]; 
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            con.Open();
            for (int i = 0; i < 12; i++) 
            {
                now = DateTime.Now.AddMonths(-i);    
                firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                using SQLiteCommand cmd = new("SELECT COUNT(*) FROM Zgłoszenia WHERE DataZgł BETWEEN '" + firstDayOfMonth.ToShortDateString() + "' AND '" + lastDayOfMonth.ToShortDateString() + "'", con);
                countArray[i] = (double)(long)cmd.ExecuteScalar();
            }
            con.Close();
            return countArray.Reverse();
        }  

        public static System.Collections.Generic.IEnumerable<double> LiczbaWykonanych()
        {
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new("SELECT COUNT(*) FROM Zgłoszenia WHERE Wykonane = false", con);
            con.Open();
            double[] count = new double[1];
            count[0]= (double)(long)cmd.ExecuteScalar();
            con.Close();
            return count;
        }
        public static System.Collections.Generic.IEnumerable<double> LiczbaNiewykonanych()
        {
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new("SELECT COUNT(*) FROM Zgłoszenia WHERE Wykonane = true", con);
            con.Open();
            double[] count = new double[1];
            count[0] = (double)(long)cmd.ExecuteScalar();
            con.Close();
            return count;
        }

        public static void InsertUpdate(Zgłoszenie obj)
        {
            try{
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Zgłoszenia SET DataZgł = ?, Imię = ?, DziałKtoZgłasza = ?, DataZdr = ?, GodzinaZdarzenia = ?, MiejsceZdarzenia = ?, MiejsceX = ?, MiejsceY = ?, Miejsce = ?, Zagrożenie = ?, Działania = ?, RodzajZagrożeń = ?, SkutkiOpis = ?, SkutkiSkala = ?, Odpowiedzialny = ?, EnvUserName = ?, ZdjęciePath = ?, Termin = ?, Wykonane = ?, WykonanoZgłaszający = ? WHERE [Numer] = " + obj.Numer;
            con.Open();
            
            int count = obj.Numer;
            if (count<=0)
            {
                using SQLiteCommand cmd2 = new("SELECT COUNT(*) FROM Zgłoszenia", con);
                count = (int)(long)cmd2.ExecuteScalar() + 1;
                cmd.CommandText = "INSERT INTO Zgłoszenia (Numer, DataZgł, Imię, DziałKtoZgłasza, DataZdr, GodzinaZdarzenia, MiejsceZdarzenia, MiejsceX, MiejsceY, Miejsce, Zagrożenie, Działania, RodzajZagrożeń, SkutkiOpis, SkutkiSkala, Odpowiedzialny, EnvUserName, ZdjęciePath, Termin, Wykonane, WykonanoZgłaszający) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                cmd.Parameters.Add(new SQLiteParameter("@Numer", count));
            }
            cmd.Parameters.Add(new SQLiteParameter("@DataZgł", obj.DataZgł));
            cmd.Parameters.Add(new SQLiteParameter("@Imię", obj.Imię));
            cmd.Parameters.Add(new SQLiteParameter("@DziałKtoZgłasza", obj.DziałKtoZgłasza));
            cmd.Parameters.Add(new SQLiteParameter("@DataZdr",  obj.DataZdr));
            cmd.Parameters.Add(new SQLiteParameter("@GodzinaZdarzenia", obj.GodzinaZdarzenia));
            cmd.Parameters.Add(new SQLiteParameter("@MiejsceZdarzenia", obj.MiejsceZdarzenia));
            cmd.Parameters.Add(new SQLiteParameter("@MiejsceX", obj.MiejsceX));
            cmd.Parameters.Add(new SQLiteParameter("@MiejsceY", obj.MiejsceY));
            cmd.Parameters.Add(new SQLiteParameter("@Miejsce", obj.Miejsce));
            cmd.Parameters.Add(new SQLiteParameter("@Zagrożenie", obj.Zagrożenie));
            cmd.Parameters.Add(new SQLiteParameter("@Działania", obj.Działania));
            cmd.Parameters.Add(new SQLiteParameter("@RodzajZagrożeń", obj.RodzajZagrożeń));
            cmd.Parameters.Add(new SQLiteParameter("@SkutkiOpis", obj.SkutkiOpis));
            cmd.Parameters.Add(new SQLiteParameter("@SkutkiSkala", obj.SkutkiSkala));
            cmd.Parameters.Add(new SQLiteParameter("@Odpowiedzialny", obj.Odpowiedzialny));
            cmd.Parameters.Add(new SQLiteParameter("@EnvUserName", obj.EnvUserName));
            cmd.Parameters.Add(new SQLiteParameter("@ZdjęciePath", obj.ZdjęciePath));
            cmd.Parameters.Add(new SQLiteParameter("@Termin", obj.Termin));
            cmd.Parameters.Add(new SQLiteParameter("@Wykonane", obj.Wykonane));
            cmd.Parameters.Add(new SQLiteParameter("@WykonanoZgłaszający", obj.WykonanoZgłaszający));

            cmd.ExecuteNonQuery();
            con.Close();
            _ = MessageBox.Show("Zgłoszenie zostało zapisane", "Zgłoszenie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SQLiteException sqle)
            {
                MessageBox.Show("SQLite : " + sqle.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exeption: " + ex.ToString());
            }
        }

        public static void Update(Zgłoszenie obj)
        {
            using SQLiteConnection con = new(ConnectionString.ConStrRepository);
            using SQLiteCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE [Zgłoszenia] SET [Wykonane] = ?, [WykonanoZgłaszający] = ? WHERE [Numer] = ?";
            cmd.Parameters.Add(new SQLiteParameter("@Wykonane", obj.Wykonane));
            cmd.Parameters.Add(new SQLiteParameter("@WykonanoZgłaszający", obj.WykonanoZgłaszający));
            cmd.Parameters.Add(new SQLiteParameter("@Numer", obj.Numer));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            _ = MessageBox.Show("Zgłoszenie zostało zaktualizowane", "Zgłoszenie", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        //private static int InsertSQL(Zgłoszenie obj)
        //{
        //    using IDbConnection db = new SqlConnection(Properties.Settings.Default.Connection_String);

        //    if (db.State == ConnectionState.Closed)
        //        db.Open();
        //    DynamicParameters p = new();
        //    p.Add("@Numer", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    p.AddDynamicParams(new
        //    {
        //        obj.DataZgł,
        //        obj.Imię,
        //        obj.DziałKtoZgłasza,
        //        obj.DataZdr,
        //        obj.GodzinaZdarzenia,
        //        obj.MiejsceZdarzenia,
        //        obj.MiejsceX,
        //        obj.MiejsceY,
        //        obj.Miejsce,
        //        obj.Zagrożenie,
        //        obj.Działania,
        //        obj.RodzajZagrożeń,
        //        obj.SkutkiOpis,
        //        obj.SkutkiSkala,
        //        obj.Odpowiedzialny,
        //        obj.EnvUserName, !bool
        //        obj.ZdjęciePath,
        //        obj.Termin,
        //        obj.Wykonane,
        //        obj.WykonanoZgłaszający
        //    });
        //    _ = db.Execute("zgloszenie_Insert", p, commandType: CommandType.StoredProcedure);
        //    _ = MessageBox.Show("Zgłoszenie zostało zapisane", "Zgłoszenie", MessageBoxButton.OK, MessageBoxImage.Information);
        //    return p.Get<int>("@Numer");
        //}
//<?xml version = '1.0' encoding='utf-8'?>
//<SettingsFile xmlns = "http://schemas.microsoft.com/VisualStudio/2004/01/settings" CurrentProfile="(Default)" GeneratedClassNamespace="ZPW.Properties" GeneratedClassName="Settings">
//  <Profiles />
//  <Settings>
//    <Setting Name = "Connection_String" Type="System.String" Scope="Application">
//      <Value Profile = "(Default)" > Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kowal\source\repos\WpfTest1\DatabaseZPW.mdf;Current Language = Polish; Integrated Security = True </ Value >
       
//           </ Setting >
       
//           < Setting Name="Connection_MDB" Type="System.String" Scope="Application">
//      <Value Profile = "(Default)" > Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\kowal\source\repos\WpfTest1\ZPWdata.mdb;Persist Security Info=True;</Value>
//    </Setting>
//    <Setting Name = "Connection_SQLite" Type="System.String" Scope="Application">
//      <Value Profile = "(Default)" > Data Source=C:\Users\kowal\source\repos\WpfTest1\ZPWdata2.db;Version=3;</Value>
//    </Setting>
//  </Settings>
//</SettingsFile>

    }
    #endregion
}