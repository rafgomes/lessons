using System;
using System.IO;
using System.Drawing;
using Imprensa.Business.Exceptions;

namespace Imprensa
{
    namespace Business
    {
        public class Base64ImgValidator
        {
            public void validate(string base64)
            {
                var bytes = System.Convert.FromBase64String(base64);
                using (var MS = new MemoryStream(bytes))
                {
                    try
                    {
                        IsValidImage(MS);
                        var Bitmap = new Bitmap(MS);
                    }
                    catch (Exception e)
                    {                       
                        throw new IvalidBase64ImgException(base64);
                    }
                }
            }

            public void IsValidImage(Stream stream)
            {
                try
                {
                    stream.Position = 0;
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var img = Image.FromStream(stream))
                    {
                        img.Dispose();
                    }
                }
                catch (Exception e)
                {                   
                    throw e;
                }
            }
        }
    }
}
