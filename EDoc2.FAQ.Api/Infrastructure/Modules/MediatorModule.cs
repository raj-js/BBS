using Autofac;
using MediatR;
using System.Reflection;

namespace EDoc2.FAQ.Api.Infrastructure.Modules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();
        }
    }
}
