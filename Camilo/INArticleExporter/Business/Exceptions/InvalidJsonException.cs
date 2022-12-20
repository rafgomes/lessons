using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Imprensa
{
    namespace Business
    {
        namespace Exceptions
        {
            public class InvalidJsonException : Exception
            {
                public InvalidJsonException(ICollection<NJsonSchema.Validation.ValidationError> msgs, string json) : base()
                {
                    this.Json = json;

                    _errors = "Json Inválido: " + Environment.NewLine;

                    foreach (var msg in msgs)

                        _errors = string.Concat(_errors, $" Campo {msg.Path} está errado", Environment.NewLine);


                    _errors = string.Concat(_errors, @"  Verifique o Log 'ValidationErrror' em C:\tera\logs");

                }
                public string Json { get; set; }

                private string _errors = string.Empty;


                public override string Message
                {
                    get
                    {
                        return _errors;
                    }
                }
            }
        }
    }
}
