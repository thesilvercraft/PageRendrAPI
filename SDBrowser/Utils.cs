using System.Drawing;
using System.IO;

namespace SDBrowser
{
    internal class Utils

    {
        protected Utils()
        {
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static Bitmap ByteArrayToImage(byte[] imageIn)
        {
            using var ms = new MemoryStream(imageIn);
            return new Bitmap(ms);
        }
    }
}