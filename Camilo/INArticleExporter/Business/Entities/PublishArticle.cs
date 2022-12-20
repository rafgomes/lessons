using Imprensa.Business;

public class PublishArticle
{
    public bool isPublished { get; set; } = false;
    public string Key { get; set; }
    public string Json { get; set; }
    public ArticleModel Article { get; set; }
}
