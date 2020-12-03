using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bookworm_Desktop.UI
{
    public class MaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                string val => parameter switch
                {
                    "RG" => val.ApplyRGMask(),
                    "CPF" => val.ApplyCPFMask(),
                    "Tel" => val.ApplyTelMask(),
                    _ => null
                },
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
