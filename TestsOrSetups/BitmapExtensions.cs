using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOrSetups
{
    static class BitmapExtensions
    {
        public static byte[] ToBytes(this Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
