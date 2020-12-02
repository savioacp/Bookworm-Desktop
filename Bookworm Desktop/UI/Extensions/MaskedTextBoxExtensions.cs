using System.Linq;
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
