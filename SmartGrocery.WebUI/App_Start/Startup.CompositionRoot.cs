using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using Owin;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartGrocery.WebUI
{
    public partial class Startup
    {
        private void ConfigurationCompositionRoot(IAppBuilder builder)
        {
            var containerBuilder = new ContainerBuilder();

            RegisterWebApiClient(containerBuilder);
            RegisterAutoMapper(containerBuilder);

            var container = containerBuilder.Build();

            IntergrateDIContainerWithFrameworks(builder, container);
        }

        private void RegisterAutoMapper(ContainerBuilder containerBuilder)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfiles(typeof(Startup));
            });

            configuration.AssertConfigurationIsValid();

            containerBuilder
                .RegisterInstance(configuration)
                .As<MapperConfiguration>()
                .As<IConfigurationProvider>();

            containerBuilder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve));
        }

        private void RegisterWebApiClient(ContainerBuilder builder)
        {
            //builder.RegisterInstance(new HttpClient()
            //{
            //    BaseAddress = new Uri("")
            //});

            builder.Register(x => new HttpClient() { BaseAddress = new Uri("https://localhost:44388/") })
                .Named<HttpClient>("SmartGroceryApi")
                .SingleInstance();
        }

        private void IntergrateDIContainerWithFrameworks(IAppBuilder builder, IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            builder.UseAutofacMiddleware(container);
            builder.UseAutofacMvc();

            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.AddImplicitRequiredValidator = false;
            });
        }
    }
}