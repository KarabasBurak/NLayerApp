using Autofac;
using AutoMapper;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();



        }
    }
}

/*
 AUTOFAC
Aslında Built-in DI Container (Core ile otomatik olarak geliyor zaten) ile AutoFac aynı görevi yapıyor. Fakat proje büyüdükçe program.cs'de tüm repo ve service'leri tanımlamamız program.cs'in şişmesine sebep olur ve bu istenmeyen durumdur. BestPratices olarak AutoFac kütüphanesinde daha kısa yoldan yapılabiliyor.

Ayrıca; Constructor ve Metod Injectio yapılıyor var ama Property Injection yapılamıyor. AutoFac'de Property Injection da yapılabiliyor. Autofac'de DI Container'a dinamik olarak nesne ekleme özelliği var. Örneğin; belirttiğin assembly'lerden sonu repository ile biten tüm interfaceleri ve buna karşılık gelen sınıfları ekle diyebiliyoruz
 
 */
