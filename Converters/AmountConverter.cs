using Avalonia.Data.Converters;
using System.Globalization;
using System;

namespace MoneyApp.Converters
{
    public class AmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            if(parameter != null && parameter.ToString() == "RU")
                return ((int)value).ToString("# ### ### ###", culture) + "р.";
            else return ((int)value).ToString("# ### ### ###", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture){
            throw new NotSupportedException("Two way bindings are not supported with a string format");
        }
    }
}