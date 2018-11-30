using Autofac;
using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Articles;
using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Uow;
using EDoc2.FAQ.Core.Infrastructure;
using EDoc2.FAQ.Core.Infrastructure.Repositories;

namespace EDoc2.FAQ.Api.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder b)
        {
            #region 工作单元

            b.RegisterType<CommunityContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

            #endregion

            #region 仓储

            b.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            b.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();

            #endregion


            #region 领域服务

            b.RegisterType<AccountService>().PropertiesAutowired().As<IAccountService>().InstancePerLifetimeScope();

            #endregion


            #region 应用服务

            b.RegisterType<AccountAppService>().PropertiesAutowired().As<IAccountAppService>().InstancePerLifetimeScope();
            b.RegisterType<ArticleAppService>().PropertiesAutowired().As<IArticleAppService>().InstancePerLifetimeScope();
            b.RegisterType<MailService>().As<IMailService>().SingleInstance();

            #endregion
        }
    }
}
