using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Entities.Json
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic;

    [DataContract]
    public class CategoryXPageModel
    {
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string SortBy { get; set; }
        [DataMember]
        public int PageNumber { get; set; }
        [DataMember]
        public string INCategoryId { get; set; }
        [DataMember]
        public string INIdSiorg { get; set; }
        [DataMember]
        public string Section { get; set; }
    }

    [DataContract]
    public class JsonJornal
    {
        [DataMember]
        public string versao { get; set; } = "1.0.0";
        [DataMember]
        public Publication publicacao { get; set; }
    }

    [DataContract]
    public class Publication
    {
        [DataMember]
        public string dataPublicacao { get; set; } = "";
        [DataMember]
        public int quantidadeMaterias { get; set; } = 0;
        [DataMember]
        public int quantidadeMateriasFilhas { get; set; } = 0;
        [DataMember]
        public Jornal jornal { get; set; }
        [DataMember]
        public List<int> idMaterias { get; set; }
    }

    [DataContract]
    public class Jornal
    {      

        [DataMember]
        public int idSecao { get; set; } = 1;
        [DataMember]
        public int idJornal { get; set; } = 0;
        [DataMember]
        public string ano { get; set; } = "";
        [DataMember]
        public string numeroEdicao { get; set; } = "";
        [DataMember]
        public bool isExtra { get; set; } = false;
        [DataMember]
        public bool isSuplemento { get; set; } = false;
    }



    [DataContract]
    public class PdfPublication
    {
        [DataMember]
        public string dataPublicacao { get; set; } = "";
        [DataMember]
        public Jornal jornal { get; set; }
        [DataMember]
        public string pdf { get; set; } = "";
    }



    [DataContract]
    public class Sumario
    {
        [DataMember]
        public string versao { get; set; } = "1.0.1";
        [DataMember]
        public DateTime dataPublicacao { get; set; } = DateTime.Now;
        [DataMember]
        public List<Grade> grades { get; set; }
    }



    [DataContract]
    public class Grade
    {
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string idJornal { get; set; }
        [DataMember]
        public string numPagina { get; set; }
        [DataMember]
        public Siorg siorg { get; set; }
    }


    [DataContract]
    public class Siorg
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string idPai { get; set; }
    }

}
