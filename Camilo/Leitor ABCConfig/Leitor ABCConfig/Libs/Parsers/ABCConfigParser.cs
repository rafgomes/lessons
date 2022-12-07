using Leitor_ABCConfig.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Libs.Parsers
{
    public class ABCConfigParser
    {
        public ABCConfig GetABCConfig(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            string xml = xmlDoc.OuterXml;

            XmlSerializer serializer = new XmlSerializer(typeof(ABCConfig));
            using (StringReader reader = new StringReader(xml))
            {
                var abcConfig = (ABCConfig)serializer.Deserialize(reader);
                return abcConfig;
            }
        }
    }
}
