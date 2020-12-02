using System;
using System.Globalization;
using System.Windows.Data;

namespace Bookworm_Desktop.UI
{
    class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return String.IsNullOrWhiteSpace(str);
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
