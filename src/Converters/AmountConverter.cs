using Avalonia.Data.Converters;
using System.Globalization;
using System;

namespace MoneyApp.Converters
{
    public class AmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => ConvertToString(value.ToString()!);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty((string)value))
                return 0;
            else return value;
        }

        public string ConvertToString(string value)
        {
            int amount = int.Parse(value);
            string result = amount.ToString();

            int ratio = CountDigitsByDivRecursive(amount);

            switch(ratio)
            {
                case 4: 
                    result = amount.ToString("# ###");
                break;
                case 5: 
                    result = amount.ToString("## ###");
                break;
                case 6: 
                    result = amount.ToString("### ###");
                break;
                case 7: 
                    result = amount.ToString("# ### ###");
                break;
                case 8: 
                    result = amount.ToString("## ### ###");
                break;
                case 9:
                    result = amount.ToString("### ### ###");
                break;
                case 10:
                    result = amount.ToString("# ### ### ###");
                break;
            }

            return result;
        }

        private int CountDigitsByDivRecursive(int n)
        {
            return (n <= 9) ? 1 : CountDigitsByDivRecursive(n / 10) + 1;
        }
    }
}