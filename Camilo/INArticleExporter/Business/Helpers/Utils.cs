using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Helpers
{
    public class Utils
    {
        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new Exception("Impossible state reached");
        }
        public static string ImageToBase64(string imagePath)
        {
            try
            {
                var fileinfo = new FileInfo(imagePath);
                using (var image = System.Drawing.Image.FromFile(imagePath))
                {
                    using (var m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        var imageBytes = m.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);

                        return base64String;
                    }
                }
            }
            catch (Exception e)
            {                
                var imageArray = System.IO.File.ReadAllBytes(imagePath);
                var base64ImageRepresentation = Convert.ToBase64String(imageArray);

                return base64ImageRepresentation;
            }
        }


        public static IEnumerable<IEnumerable<T>> BuildChunksWithRange<T>(List<T> fullList, int batchSize)
        {
            List<List<T>> chunkedList = new List<List<T>>();
            int index = 0;

            while (index < fullList.Count)
            {
                int rest = fullList.Count - index;

                if (rest >= batchSize)
                    chunkedList.Add(fullList.GetRange(index, batchSize));
                else
                    chunkedList.Add(fullList.GetRange(index, rest));

                index += batchSize;
            }

            return chunkedList;
        }
    }
}
