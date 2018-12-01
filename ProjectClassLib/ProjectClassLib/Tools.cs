using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class Tools
    {
        public static string InitCap(string input)
        {
            return (input[0].ToString()).ToUpper() + input.Substring(1).ToLower();
        }

        public static string InitCap(string input, bool toLower)
        {
            string ret = "";
            if (toLower)
            {
                ret = (input[0].ToString()).ToUpper() + input.Substring(1).ToLower();
            }
            else
            {
                ret = (input[0].ToString()).ToUpper() + input.Substring(1);
            }
            return ret;
        }

        public static string getValueByKey(string[] list, string key)
        {
            string val = "";
            foreach (var item in list)
            {
                if (item.Split('=')[0] == key)
                {
                    val = item.Split('=')[1];
                }
            }
            return val;
        }
        
        public static string getValueByKey(object listObject, string key)
        {
            string[] list = ((string)listObject).Split(';');
            string val = null;
            foreach (var item in list)
            {
                if (item.Split('=')[0] == key)
                {
                    val = item.Split('=')[1];
                }
            }
            return val;
        }

        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gibt den MD5 hash einer Datei zurück
        /// </summary>
        /// <param name="filename">Der Dateipfad</param>
        /// <returns></returns>
        public static string CalculateMD5FromFile(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Gibt den MD5 hash eines Strings zurück
        /// </summary>
        /// <param name="filename">Der String</param>
        /// <returns></returns>
        public static string CalculateMD5(string text)
        {
            using (var md5 = MD5.Create())
            {
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(text);
                var hash = md5.ComputeHash(buffer);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
