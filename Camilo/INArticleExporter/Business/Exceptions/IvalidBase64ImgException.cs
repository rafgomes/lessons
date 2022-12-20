using System;

namespace Imprensa
{
    namespace Business
    {
        namespace Exceptions
        {
            public class IvalidBase64ImgException : Exception
            {
                public IvalidBase64ImgException(string base64) : base(string.Format("Invalid Base64 image: {0}", base64))
                {
                }
            }
        }
    }
}
