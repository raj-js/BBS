using EDoc2.FAQ.Web.Data;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Web.Services
{
    public class SystemManager : ISystemManager
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ArticleManager> _logger;
        private readonly AppDbContext _appDbContext;

        public SystemManager(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IServiceProvider provider,
            IMemoryCache memoryCache,
            ILogger<ArticleManager> logger,
            AppDbContext appDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _memoryCache = memoryCache;

            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task AddReport(Report report)
        {
            if(report == null)
                throw new ArgumentNullException(nameof(report));

            _appDbContext.Reports.Add(report);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories(ArticleSubTypes subType)
        {
            return await _appDbContext.Categories.Where(c => c.SubCategory == subType).ToListAsync();
        }
    }
}
