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
        public class JsonConvertTypes
        {
            private JObject jsonObj;

            public JsonConvertTypes(string json)
            {
                this.jsonObj = JObject.Parse(json);
            }
            public JsonConvertTypes(JObject jObj)
            {
                this.jsonObj = jObj;
            }
            public string GetJson()
            {
                ConvertStrings();

                var json = this.jsonObj.ToString(Formatting.None);
                return json;
            }

            public JObject GetJsonObj()
            {
                ConvertStrings();

                return this.jsonObj;
            }

            private void ConvertStrings()
            {
                var values = from a in this.jsonObj.Descendants().OfType<JToken>()
                             where a.Type == JTokenType.String
                             select a;


                foreach (JToken value in values)
                {
                    try
                    {
                        if (StringToInteger(value)) continue;                       

                        if (StringToBollean(value)) continue;

                        if (EmptyStringToNull(value)) continue;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }


            private bool EmptyStringToNull(JToken token)
            {
                var value = token.Value<JValue>();

                if ((value.Type == JTokenType.String))
                {
                    if (String.IsNullOrEmpty((string) value.Value))
                    {
                        value.Value = null;
                        return true;
                    }
                }

                return false;
            }
            private bool StringToInteger(JToken token)
            {
                int intValue;

                if (token.Path.Contains("ordem") | token.Path.Contains("numeroEdicao") | token.Path.Contains("ano"))
                    return true;

                var value = token.Value<JValue>();
                string stringValue = value.Value.ToString();

                if ((int.TryParse(stringValue, out intValue)))
                {
                    value.Value = intValue;
                    return true;
                }
                return false;
            }

            private bool StringToBollean(JToken token)
            {
                bool boolValue;
                var value = token.Value<JValue>();
                string stringValue = value.Value.ToString();

                if ((bool.TryParse(stringValue, out boolValue)))
                {
                    value.Value = boolValue;
                    return true;
                }

                return false;
            }
        }
    }
}
