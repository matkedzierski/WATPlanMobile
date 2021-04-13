using System;
using System.Globalization;
using Xamarin.Forms;

namespace WATPlanMobile.Main
{
    public class ShortcutConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ret = "";
            foreach (var str in ((string) value).Split(' '))
                if (str.Length > 1 && str[0] != '(')
                    ret += str.ToUpper()[0];

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}