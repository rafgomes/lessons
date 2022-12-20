using INPerformanceTest.Business.Entities.Models;
using System;
using System.Collections.Generic;

namespace Imprensa.Business
{
    public class ArticleModel
    {
        internal string bodyTextH;

        public ArticleModel(int id)
        {
            Id = id;
            Contents = new List<LayerContentModel>();
        }
        public string INMateriaId { get; set; }
        public string INArticleStatus { get; set; }
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string INParentId { get; set; }
        public string INTitDescription { get; set; }
        public string INSortBy { get; set; }
        public string INTitCode { get; set; }
        public string INTitIdSiorg { get; set; }
        public string INMateriaSeq { get; set; }
        public bool HasParent
        {
            get
            {
                return !String.IsNullOrEmpty(INParentId) && INParentId != "0";
            }
        }
        public string INOficioId { get; set; }
        public string INPortalCategoryId { get; set; }
        public string INMateriaType { get; set; }
        public string INHighLight { get; set; }
        public string INColumnSizeName { get; set; }
        public string Notes { get; set; }
        public string INMateriaTypeId { get; set; }
        public List<LayerContentModel> Contents { get; set; }
        public string INRootCategoryId { get; set; }
        public string INCategoryId { get; set; }
        public string INCategoryPath { get; set; }
        public string INCategoryDescription { get; set; }
        public List<int> PageNumbers { get; set; }
        public string INArticleSubType { get; set; }
        public string INColumnSize { get; set; }
        public string INChildPosition { get; set; }
        public string Title { get; set; }
    }
}