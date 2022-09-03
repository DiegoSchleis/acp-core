using System.Drawing;

namespace acp_core.Util
{
    public static class ImageHelper
    {
        public static byte[] GenerateRandomImageAsBytes(int width, int height)
        {
            //bitmap
            Bitmap bmp = new Bitmap(width, height);

            //random number
            Random rand = new Random();

            //create random pixels
            int a = rand.Next(256);
            int r = rand.Next(256);
            int g = rand.Next(256);
            int b = rand.Next(256);
            for (int y = 0; y < height; y++)
            {
                if (y == height / 2)
                {
                    a = rand.Next(256);
                    r = rand.Next(256);
                    g = rand.Next(256);
                    b = rand.Next(256);
                }
                for (int x = 0; x < width; x++)
                {
                    //set ARGB value
                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            //load bmp in picturebox1
            var imageConvert = new ImageConverter();
            return (byte[])imageConvert.ConvertTo(bmp, typeof(byte[]));
        }
    }
}
