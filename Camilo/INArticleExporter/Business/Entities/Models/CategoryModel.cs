using INPerformanceTest.Business.Entities.Json;

namespace Imprensa.Business
{
    public class CategoryModel
    {
        public CategoryModel(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public string INSortBy { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IdSiorg { get; set; }
        public string Name { get; internal set; }
        public string Path { get; internal set; }
    }
}