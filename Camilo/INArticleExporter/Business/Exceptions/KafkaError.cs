using System;

namespace Imprensa
{
    namespace Business
    {
        namespace Exceptions
        {
            public class KafkaError : Exception
            {
                public KafkaError(string msg) : base(msg)
                {
                }
            }
        }
    }
}
