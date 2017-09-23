using BsodSimulator.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using static QRCoder.QRCodeGenerator;

namespace BsodSimulator.Service
{
    public class QRCodeService
    {
        public static async Task<WriteableBitmap> GenerateQRCodeAsync(string content, MyColor darkColor, MyColor lightColor = null)
        {
            if (darkColor==null)
            {
                throw new ArgumentNullException(nameof(darkColor));
            }
            var payload = new PayloadGenerator.Url(content ?? string.Empty);
            byte[] GetRGB(MyColor c) => new[] { c.R, c.G, c.B };
            using (var generator = new QRCodeGenerator())
            using (var data = generator.CreateQrCode(payload.ToString(), ECCLevel.Q))
            using (var code = new BitmapByteQRCode(data))
            using (var stream = new InMemoryRandomAccessStream())
            using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
            {
                var darkRGB = GetRGB(darkColor);
                var lightRGB = lightColor == null ?
                    new[] { Byte.MaxValue, Byte.MaxValue, Byte.MaxValue, }
                    : GetRGB(lightColor);
                writer.WriteBytes(code.GetGraphic(20, darkRGB, lightRGB));
                await writer.StoreAsync();
                var bitmap = new WriteableBitmap(1024, 1024);
                await bitmap.SetSourceAsync(stream);
                return bitmap;
            }
        }
    }
}
