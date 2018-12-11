using Autofac;
using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Applications;
using EDoc2.FAQ.Core.Application.Articles;
using EDoc2.FAQ.Core.Application.Categories;
using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.Applications.Services;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Articles.Services;
using EDoc2.FAQ.Core.Domain.Categories;
using EDoc2.FAQ.Core.Domain.Categories.Services;
using EDoc2.FAQ.Core.Domain.Uow;
using EDoc2.FAQ.Core.Infrastructure;
using EDoc2.FAQ.Core.Infrastructure.Repositories;

namespace EDoc2.FAQ.Api.Infrastructure.Modules
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder b)
        {
            #region 工作单元

            b.RegisterType<CommunityContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

            #endregion

            #region 仓储

            b.RegisterType<AccountRepository>().PropertiesAutowired().As<IAccountRepository>().InstancePerLifetimeScope();
            b.RegisterType<ArticleRepository>().PropertiesAutowired().As<IArticleRepository>().InstancePerLifetimeScope();
            b.RegisterType<ApplicationRepository>().PropertiesAutowired().As<IApplicationRepository>().InstancePerLifetimeScope();
            b.RegisterType<CategoryRepository>().PropertiesAutowired().As<ICategoryRepository>().InstancePerLifetimeScope();

            #endregion


            #region 领域服务

            b.RegisterType<AccountService>().PropertiesAutowired().As<IAccountService>().InstancePerLifetimeScope();
            b.RegisterType<ArticleService>().PropertiesAutowired().As<IArticleService>().InstancePerLifetimeScope();
            b.RegisterType<ApplicationService>().PropertiesAutowired().As<IApplicationService>().InstancePerLifetimeScope();
            b.RegisterType<CategoryService>().PropertiesAutowired().As<ICategoryService>().InstancePerLifetimeScope();

            #endregion


            #region 应用服务

            b.RegisterType<AccountAppService>().PropertiesAutowired().As<IAccountAppService>().InstancePerLifetimeScope();
            b.RegisterType<ArticleAppService>().PropertiesAutowired().As<IArticleAppService>().InstancePerLifetimeScope();
            b.RegisterType<ApplicationAppService>().PropertiesAutowired().As<IApplicationAppService>().InstancePerLifetimeScope();
            b.RegisterType<CategoryAppService>().PropertiesAutowired().As<ICategoryAppService>().InstancePerLifetimeScope();
            b.RegisterType<MailService>().As<IMailService>().SingleInstance();

            #endregion
        }
    }
}
