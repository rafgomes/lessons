using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Entity
{
    public class INArticle
    {
        public int s_id { get; set; }
        public int s_bodyTextH { get; set; }
        public int s_bodyNParas { get; set; }
        public int s_bodyNLines { get; set; }
        public int s_bodyNWords { get; set; }
        public int s_bodyNChars { get; set; }
        public DateTime s_publicationDate { get; set; }
        public int s_publicationRef { get; set; }
        public int s_authorRef { get; set; }
        public string s_notes { get; set; }
        public short s_INArticleType { get; set; }
        public string s_INArticleStatus { get; set; }
        public int s_INColumnSize { get; set; }
        public short s_INRejected { get; set; }
        public short s_INAutoPaginated { get; set; }  
        public int s_INRootCategory { get; set; }
        public int s_INCategory { get; set; }
        public string s_INSortBy { get; set; }
        public int s_INColumnSizeName { get; set; }
        public string s_INArticleSubType { get; set; }
        public int s_INMateriaSeq { get; set; }
        public int s_INAssignedUserRef { get; set; }
        public string s_INMateriaType { get; set; }
        public string s_headline { get; set; }
        public string s_INHighlight { get; set; }
        public string s_INSection { get; set; }
        public string s_INHighlightUser { get; set; }
        public short s_INHasHighlight { get; set; }
        public int s_INOficioId { get; set; }
        public int s_INMateriaId { get; set; }
        public int s_INMateriaTypeId { get; set; }
        public int s_INHighlightPriority { get; set; }
        public string s_INParentId { get; set; }
        public string s_INChildPosition { get; set; }
        public string s_INRelationship { get; set; }
        public int s_INTitIdSiorg { get; set; }
        public string s_INTitDescription { get; set; }
        public int s_INTitCode { get; set; }
   }
}
