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
    #region FILTRY

    [Serializable]
    public class Filtr : ISerializable
    {
        public Filtr(){}

        private string _FiltrNazwa;
        private List<string> _FiltrDział;
        private bool _FiltrWgDat;
        private DateTime _FiltrDataOd;
        private DateTime _FiltrDataDo;
        private bool _FiltrWgDni;
        private int _FiltrLiczbaDni;
        private List<string> _FiltrMiejsce;
        private List<string> _FiltrRodzajZagrożenia;
        private List<string> _FiltrSkutki;
        private List<string> _FiltrOdpowiedzialni;

        public string FiltrNazwa { get => _FiltrNazwa; set => _FiltrNazwa = value; }
        public List<string> FiltrDział { get => _FiltrDział; set => _FiltrDział = value; }
        public bool FiltrWgDat { get => _FiltrWgDat; set => _FiltrWgDat = value; }
        public DateTime FiltrDataOd { get => _FiltrDataOd; set => _FiltrDataOd = value; }
        public DateTime FiltrDataDo { get => _FiltrDataDo; set => _FiltrDataDo = value; }
        public bool FiltrWgDni { get => _FiltrWgDni; set => _FiltrWgDni = value; }
        public int FiltrLiczbaDni { get => _FiltrLiczbaDni; set => _FiltrLiczbaDni = value; }
        public List<string> FiltrMiejsce { get => _FiltrMiejsce; set => _FiltrMiejsce = value; }
        public List<string> FiltrRodzajZagrożenia { get => _FiltrRodzajZagrożenia; set => _FiltrRodzajZagrożenia = value; }
        public List<string> FiltrSkutki { get => _FiltrSkutki; set => _FiltrSkutki = value; }
        public List<string> FiltrOdpowiedzialni { get => _FiltrOdpowiedzialni; set => _FiltrOdpowiedzialni = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("1", FiltrNazwa, typeof(string));
            info.AddValue("2", FiltrDział, typeof(List<string>));
            info.AddValue("3", FiltrWgDat, typeof(bool));
            info.AddValue("4", FiltrDataOd, typeof(DateTime));
            info.AddValue("5", FiltrDataDo, typeof(DateTime));
            info.AddValue("6", FiltrWgDni, typeof(bool));
            info.AddValue("7", FiltrLiczbaDni, typeof(int));
            info.AddValue("8", FiltrMiejsce, typeof(List<string>));
            info.AddValue("9", FiltrRodzajZagrożenia, typeof(List<string>));
            info.AddValue("10", FiltrSkutki, typeof(List<string>));
            info.AddValue("11", FiltrOdpowiedzialni, typeof(List<string>));
        }

        public Filtr(SerializationInfo info, StreamingContext context)
        {
            FiltrNazwa = (string) info.GetValue("1", typeof(string));
            FiltrDział = (List<string>) info.GetValue("2", typeof(List<string>));
            FiltrWgDat = (bool) info.GetValue("3", typeof(bool));
            FiltrDataOd = (DateTime) info.GetValue("4", typeof(DateTime));
            FiltrDataDo = (DateTime) info.GetValue("5", typeof(DateTime));
            FiltrWgDni = (bool) info.GetValue("6", typeof(bool));
            FiltrLiczbaDni = (int) info.GetValue("7", typeof(int));
            FiltrMiejsce = (List<string>) info.GetValue("8", typeof(List<string>));
            FiltrRodzajZagrożenia = (List<string>) info.GetValue("9", typeof(List<string>));
            FiltrSkutki = (List<string>) info.GetValue("10", typeof(List<string>));
            FiltrOdpowiedzialni = (List<string>) info.GetValue("11", typeof(List<string>));
        }

    }


    public static class Filtry
    {
        
        private readonly static string subdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Rejestr ZPW";
        private readonly static IFormatter formatter = new BinaryFormatter();
        private static FileStream s;
        public static void FiltrSerialize(Filtr t, string fileName)
        {
            if (!Directory.Exists(subdir))  
                Directory.CreateDirectory(subdir);  
            string filePath = subdir + @"\" + fileName;
            s = new FileStream(filePath , FileMode.Create);
            formatter.Serialize(s, t);
            s.Close();
        }

        public static Filtr FiltrDeserialize(string fileName)
        {
            ////List<string> files = (System.IO.Directory.GetFiles(subdir, "*.ZPWfilter")).ToList();
            string filePath = subdir + @"\" + fileName;
            Filtr t = new ();
            if (File.Exists(filePath))
            {            
                try {
                s = new FileStream(filePath, FileMode.Open);
                t = (Filtr)formatter.Deserialize(s);
                s.Close();
                }
                catch {}
            }
            return t;
        }
    }

    #endregion //FILTRY
}