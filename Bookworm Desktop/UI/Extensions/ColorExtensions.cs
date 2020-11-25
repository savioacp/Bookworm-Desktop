using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Media;

namespace Bookworm_Desktop.UI.Extensions
{
    static class ColorExtensions
    {
        public static Color FromHex(this Color _, string hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }
    }
}
