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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Imprensa
{
    namespace Business
    {
        public class JsonMerge
        {
            private JObject jsonObj;
            public JsonMerge(string json)
            {
                this.jsonObj = JObject.Parse(json);
            }

            public string Merge()
            {
                var mergesElem = this.jsonObj.SelectToken("merges");
                var children = mergesElem.Children();

                var merged = string.Empty;

                foreach (var child in children)
                {
                    var text = child.ToString();
                    merged = merged + text;
                }

                return string.Empty;
            }
        }
    }
}
