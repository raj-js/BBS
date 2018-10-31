namespace EDoc2.FAQ.Web.Data.Discuss
{
    public class ArticleCategory
    {
        public int Id { get; set; }

        public string ArticleId { get; set; }

        public string CategoryId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Category  Category { get; set; }
    }
}
