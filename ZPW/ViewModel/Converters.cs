using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;
using ZPW.ViewModel;
using ZPW.Model;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace ZPW.ViewModel
{
    public class StrechToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double ret = (Double)value-35;
            if (ret<=0) ret=35;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class FileToFullPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //OBSERWUJ string to file "" error
            string AppPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Dane\\";
            if ((string)parameter == "Small") AppPath += "Miniatury\\";
            var strings = (string)value;
            if (!String.IsNullOrEmpty(strings))
            {
                return AppPath + strings;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BoolToStringConverter : IValueConverter
    {
        public char Separator { get; set; } = ';';

        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            var strings = ((string)parameter).Split(Separator);
            var trueString = strings[0];
            var falseString = strings[1];

            var boolValue = (bool)value;
            if (boolValue == true)
            {
                return trueString;
            }
            else
            {
                return falseString;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            var strings = ((string)parameter).Split(Separator);
            var trueString = strings[0];

            var stringValue = (string)value;
            if (stringValue == trueString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class BoolToString2Converter : IValueConverter
    {
        public char Separator { get; set; } = ';';

        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            if(value.GetType().Equals(typeof(string)))
                return value;
            
            var strings = ((string)parameter).Split(Separator);
            var trueString = strings[0];
            var falseString = strings[1];
            var boolValue = (bool)value;
            if (boolValue == true)
            {
                return trueString;
            }
            else
            {
                return falseString;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            var strings = ((string)parameter).Split(Separator);
            var trueString = strings[0];

            var stringValue = (string)value;
            if (stringValue == trueString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class BoolReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = !(bool)value;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = !(bool)value;
            return ret;
        }
    }

    public class NameToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = false;
            if ((string)value==(string)parameter)
                ret=true;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }


    public class NoStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ret = "Szukaj...";
            if ((string)value!="")
                ret="";
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }

    public class MapPosConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(values[0].GetType().Equals(typeof(double)))
            {
                double pointXY=(double)values[0];
                double controlHW=(double)values[1];
                double viewWidth=(double)values[2];
                double pinHeight=(1500/(double)viewWidth)*35;
                if(((string)parameter).Contains("M")) 
                    pinHeight=12;
                double pinWidth=0.3125*pinHeight;
                if(((string)parameter).Contains("E"))
                {
                    pinWidth*=3.2;
                    pinHeight*=1.5;
                } 
                double ret = pointXY*controlHW-pinWidth;
                if(((string)parameter).Contains("Y")) 
                    ret = pointXY*controlHW-pinHeight;
                return ret;
            }
            else return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }

   public class VisConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Visibility ret = System.Windows.Visibility.Visible;
            ListCollectionView _LCV = (ListCollectionView)values[0];
            Zgłoszenie _sel = ((Zgłoszenie)values[1]);
            if(!_LCV.Contains(_sel)) ret = System.Windows.Visibility.Hidden;
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class Vis2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Visibility ret = System.Windows.Visibility.Visible;
            ListCollectionView _LCV = (ListCollectionView)value;
            if(_LCV.Count>0) ret = System.Windows.Visibility.Hidden;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }    
    public class Vis3Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = false;
            ListCollectionView _LCV = (ListCollectionView)value;
            if(_LCV.Count>0) ret = true;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
    public class PinSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double scale=(1500/(double)value)*35;
            if(((string)parameter).Contains("Ellipse")) scale *= 2;
            return scale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }

    public class TopLeftConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double scaleF=(double)values[0];
            double pointX=(double)values[1];
            double pointY=(double)values[2];
            double scale=447/scaleF;
            double ret=pointX*scale;
            if(((string)parameter).Contains("T")) ret=pointY*scale;
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    public class WidhtHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double scaleF=(double)values[0];
            double pointX=(double)values[1];
            double pointY=(double)values[2];
            double scale=447/scaleF;
            double ret=(pointX*scale)-2;
            if (ret>=250) ret=250;
            if(((string)parameter).Contains("H"))
            {
                ret=(pointY*scale);
                if (ret>=447) ret=447;
            }
            if (ret<0) ret=0;
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class RectConvertor : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            RectangleGeometry myRectangleGeometry = new ();
            
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return myRectangleGeometry.Rect = new Rect(0,0,10,10);
            }
            
            double wi=(double)values[0];
            double hi=(double)values[1];
            myRectangleGeometry.Rect = new Rect(0,0,wi,hi);
            return myRectangleGeometry.Rect;
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ListCollectionView)value).Count == 0 ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
    // Converts Bool to Visibility

    public class MarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret1;//X
            double ret2;//Y
            Thickness margin = new();
            if(value.GetType().Equals(typeof(double)))
            {
                double viewWidth=(double)value;
                double pinHeight=(1500/(double)viewWidth)*35;
                double pinWidth=0.3125*pinHeight;
                if(((string)parameter).Contains("E"))
                {
                    pinWidth*=3.2;
                    pinHeight*=1.5;
                } 
                ret1 = -pinWidth;
                ret2 = -pinHeight;
                margin.Left = ret1;
                margin.Right = 0;
                margin.Top = ret2;
                margin.Bottom = 0;
            }
            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    
}

