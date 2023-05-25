using System;
using Avalonia.Data.Converters;

namespace Avalonia.NETCoreMVVMApp1.Converter;

public class WameConverter:IValueConverter
{
    
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "https://wa.me/" + value;
        }  
  
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var x = value as string;
            var y = "https://wa.me/".ToCharArray();
            var t = x.Trim(y);
            return t;
        }  
    
}