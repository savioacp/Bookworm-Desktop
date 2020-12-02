using System;
using System.Globalization;
using System.Windows.Data;

namespace Bookworm_Desktop.UI.MainPages
{
    class JoinArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? string.Join(", ", (string[])value) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
