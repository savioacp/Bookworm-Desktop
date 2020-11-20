using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bookworm_Desktop.UI
{
    class BooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter switch 
            {
                null => values.Cast<bool?>().Any(value => value == true),

                IValueConverter converter => converter.Convert(values.Cast<bool?>().Any(value => value == true), targetType, null, culture),

                _ => throw new ArgumentException($"Argumento inválido: {parameter}")
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
