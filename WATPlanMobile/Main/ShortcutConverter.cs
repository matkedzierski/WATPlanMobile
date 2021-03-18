using System;
using System.Globalization;
using Xamarin.Forms;

namespace WATPlanMobile.Main
{
    public class ShortcutConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value; return s.Substring(0, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}