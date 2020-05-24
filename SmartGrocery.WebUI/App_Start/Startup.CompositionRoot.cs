using Autofac;
using Autofac.Features.AttributeFilters;
using Autofac.Integration.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using Owin;
using SmartGrocery.Infrastructure;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartGrocery.WebUI
{
    public partial class Startup
    {
        private void ConfigurationCompositionRoot(IAppBuilder builder)
        {
            var containerBuilder = new ContainerBuilder();

            RegisterAutoMapper(containerBuilder);
            RegisterWebMVCComponet(containerBuilder);
            RegisterWebApiClient(containerBuilder);
            RegisterFluentValidator(containerBuilder);

            containerBuilder.RegisterType<EmotionalRPCClient>().AsSelf();

            var container = containerBuilder.Build();

            IntergrateDIContainerWithFrameworks(builder, container);
        }

        private void RegisterFluentValidator(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterAssemblyTypes(ThisAssembly)
            //       .Where(t => t.Name.EndsWith("Validator"))
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope();

            containerBuilder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
        }

        private void RegisterWebMVCComponet(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly).WithAttributeFiltering();

            builder.RegisterModule<AutofacWebTypesModule>();

            // Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterModelBinderProvider();
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
            builder.Register(x => new HttpClient() { BaseAddress = new Uri(ConfigurationManager.AppSettings["SmartGroceryWebApi"]) })
                .As<HttpClient>()
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