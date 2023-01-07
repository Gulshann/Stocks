using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using StocksApp.Filters;
using StocksApp.Services.Cache;
using StocksApp.Services.SerializeService;
using StocksApp.Services.StocksAPI;
using StocksApp.Session;

namespace StocksApp.Modules
{
    public class DependencyInjectionModule  : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomBaseFilter>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SessionData>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<StocksAPIClient>().As<IStocksAPIClient>().InstancePerLifetimeScope();
            builder.RegisterType<JSONSerializerService>().As<ISerializer>().InstancePerLifetimeScope(); 
            builder.RegisterType<LocalCacheService>().As<ICacheService>().InstancePerLifetimeScope();

        }
    }
}
