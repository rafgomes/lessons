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
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Imprensa
{
    namespace Business
    {
        namespace Entities
        {
            public class KafkaArticleDocument
            {
                private PageArticle _pageArticle;

                public PageArticle PageArticle
                {
                    get
                    {
                        return _pageArticle;
                    }
                    set
                    {
                        _pageArticle = value;
                    }
                }

                private XDocument _kafkaXDcocument;
                public XDocument KafkaXDocument
                {
                    get
                    {
                        return _kafkaXDcocument;
                    }
                    set
                    {
                        _kafkaXDcocument = value;
                    }
                }
            }
        }
    }
}
