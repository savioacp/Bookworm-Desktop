using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Bookworm_Desktop.UI
{
    public class ControlToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var element = (FrameworkElement)value;
            return new Size(element.ActualWidth, element.ActualHeight);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
