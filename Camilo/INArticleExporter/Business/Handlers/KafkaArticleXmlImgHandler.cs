using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace Imprensa
{
    namespace Business
    {
        public class KafkaArticleXmlImgHandler : IKafkaArticleXmlImgHandler
        {
            private IBase64Handler base64Handler;

            public KafkaArticleXmlImgHandler(string destFolder)
            {
                this.base64Handler = new Base64Handler(destFolder);
            }
            public KafkaArticleXmlImgHandler(IBase64Handler base64Handler)
            {
                this.base64Handler = base64Handler;
            }
            private List<string> ExtractData(string htmlString)
            {
                var data = new List<string>();
                try
                {
                    var pattern = @"<\s*img[^>]*>(.*?)<\s*/\s*img>";

                    var rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                    var matches = rgx.Matches(htmlString);


                    for (int i = 0; i <= matches.Count; i += 1)
                        data.Add(matches[i].Value);
                }
                catch
                {
                }

                return data;
            }

            public void AddBase64Img(XDocument xdoc)
            {
                var textoHtmlElmntAsString = xdoc.Element("materia").Element("textoHTML").Value;

                var imgs = ExtractData(textoHtmlElmntAsString);

                if ((imgs.Count == 0))
                    return;

                foreach (var img in imgs)
                {
                    var imgOutput = string.Empty;
                    var imgTagAsXDoc = XElement.Parse(img);

                    var name = imgTagAsXDoc.Element("name").Value;
                    var base64 = this.base64Handler.ConvertToBase64(name);
                    if (string.IsNullOrEmpty(base64.Trim()))
                        throw new Exception("Empty base64 Image");
                    var src = string.Format("data:image/jpeg;base64,{0}", base64);

                    var imgElem = new XElement("img");
                    imgElem.Add(new XAttribute("name", name));
                    imgElem.Add(new XAttribute("src", src));

                    imgOutput = imgElem.ToString();
                    textoHtmlElmntAsString = textoHtmlElmntAsString.Replace(img, imgOutput);
                }
                xdoc.Element("materia").Element("textoHTML").ReplaceNodes(new XCData(textoHtmlElmntAsString));
            }
        }
    }
}
