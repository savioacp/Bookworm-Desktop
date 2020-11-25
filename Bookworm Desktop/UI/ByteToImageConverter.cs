using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bookworm_Desktop.UI
{
	public class ByteToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || ((byte[])value).Length == 0)
				return (ImageSource) Application.Current.TryFindResource("DefaultProfileImage");

			var sourceBytes = (byte[])value;
			BitmapImage toReturn = new BitmapImage();
			using (var ms = new MemoryStream(sourceBytes))
			{
				toReturn.BeginInit();
				toReturn.CacheOption = BitmapCacheOption.OnLoad;
				toReturn.StreamSource = ms;
				toReturn.EndInit();
			}
			return toReturn;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is BitmapSource source)) return new byte[0];


            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(source));
				
            using var ms = new MemoryStream();
                
            encoder.Save(ms);

            return ms.ToArray();
        }
	}
}
