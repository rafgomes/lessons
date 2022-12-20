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
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Imprensa
{
    namespace Business
    {
        public class JsonElementFix
        {
            private readonly ILogger logger;

            public JsonElementFix(ILogger logger)
            {
                this.logger = logger;
            }

            public string GetJson(string json)
            {
                JObject jsonObj = JObject.Parse(json);
                var jsonConvertTypes = new JsonConvertTypes(jsonObj);

                jsonObj = jsonConvertTypes.GetJsonObj();

                // REM CleanHtml("materia.textoHTML")
                // REM ConvertStringToJsonObj("materia.textoEstruturado")

                ConvertFieldsToArray(jsonObj,"materia.estrutura", "assinaturas");
                ConvertFieldsToArray(jsonObj,"materia.metadados", "materiasRelacionadas");
                ConvertFieldsToArray(jsonObj,"materia.metadados", "materiasComposicaoTexto");
                jsonObj = (JObject)jsonObj["materia"];


                return jsonObj.ToString();
            }
            private void ConvertFieldsToArray(JObject jsonObj, string parentElement, string element)
            {
                var elements = jsonObj.SelectToken(parentElement);

                List<string> processetedElements = new List<string>();

                foreach (JProperty ele in elements)
                {
                    var name = ele.Name;

                    if (name.Contains(element))
                    {
                        var propertyToken = ele.Value;

                        if (propertyToken.Type == JTokenType.Object)
                        {
                            var jarrayObj = new JArray();
                            jarrayObj.Add(ele.Value);

                            ele.Value = jarrayObj;
                        }

                        if (propertyToken.Type == JTokenType.Array)
                        {
                            var assinaturaChildren = propertyToken.Children();


                            var newArray = new JArray();

                            foreach (var child in assinaturaChildren)
                            {
                                if (element == "assinaturas")
                                {
                                    try
                                    {
                                        var assinante = child.SelectToken("assinante");

                                        if (assinante == null)
                                            continue;

                                        if (processetedElements.Contains(assinante.ToString()))
                                            continue;

                                        processetedElements.Add(assinante.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }


                                if (child.Type == JTokenType.Object)
                                    newArray.Add(child);
                            }

                            ele.Value = newArray;
                        }
                    }
                }
            }

        }
    }
}
