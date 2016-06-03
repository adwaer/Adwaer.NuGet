using System;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Adwaer.Identity.Entitites;

namespace Adwaer.Identity.Config
{
    public static class IdentityConfig
    {
        public static void Ioc(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityUserStore>()
                .As<IUserStore<SimpleCustomerAccount, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityUserManager>()
                .As<UserManager<SimpleCustomerAccount, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SignInManager<SimpleCustomerAccount, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerLifetimeScope();

            builder
                .Register(c => IdentityMapper.Register())
                .Keyed<IMapper>("identityMapping")
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterApiControllers(typeof(IdentityConfig).Assembly);
        }
    }
}
