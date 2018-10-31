using System.Collections.Generic;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;

namespace EDoc2.FAQ.Web.Services
{
    public interface ISystemManager
    {
        Task AddReport(Report report);

        Task<List<Category>> GetCategories(ArticleSubTypes subType);
    }
}
