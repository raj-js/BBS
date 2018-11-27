using Autofac;
using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Articles;
using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Infrastructure.Repositories;

namespace EDoc2.FAQ.Api.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder b)
        {
            b.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            b.RegisterType<ArticleRepository>()
                .As<IArticleRepository>()
                .InstancePerLifetimeScope();

            b.RegisterType<AccountAppService>()
                .As<IAccountAppService>()
                .InstancePerLifetimeScope();

            b.RegisterType<ArticleAppService>()
                .As<IArticleAppService>()
                .InstancePerLifetimeScope();

            b.RegisterType<MailService>()
                .As<IMailService>()
                .SingleInstance();
        }
    }
}
