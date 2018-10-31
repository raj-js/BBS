using EDoc2.FAQ.Web.Data.Common;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    public class Category
    {
        public string Id { get; set; }

        public string Display { get; set; }

        public ArticleSubTypes SubCategory { get; set; }
    }
}
