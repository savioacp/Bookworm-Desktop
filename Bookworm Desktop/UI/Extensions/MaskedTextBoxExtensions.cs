using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace Bookworm_Desktop.UI.Extensions
{
    static class MaskedTextBoxExtensions
    {
        public static string StripMask(this MaskedTextBox masked)
        {
            return new string(masked.Text.Where(c => !",.:- ()".Contains(c)).ToArray());
        }
    }
}
