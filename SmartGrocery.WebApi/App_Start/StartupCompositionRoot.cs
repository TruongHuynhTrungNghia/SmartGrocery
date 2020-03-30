using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using FluentValidation.WebApi;
using MediatR;
using Owin;
using SmartGrocery.UseCase.DAL;
using SmartGrocery.UseCase.Product;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace SmartGrocery.WebApi
{
    public partial class Startup
    {
        private const string SmartGroceryConnectionString = "SmartGroceryDatabase";

        private void CompositionRoot(IAppBuilder builder, HttpConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();

            RegisterDbContext(containerBuilder);

            RegisterMediator(containerBuilder);

            RegisterAutoMapper(containerBuilder);

            RegisterWebApiControlers(containerBuilder, configuration);

            var container = containerBuilder.Build();

            IntergrateDIContainerWithFrameworks(builder, container, configuration);
        }

        private void RegisterDbContext(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<SmartGroceryContext>()
                .AsSelf()
                //.UsingConstructor(typeof(string), typeof(ILifetimeScope))
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(string), (pi, ctx) => SmartGroceryConnectionString);
        }

        private void RegisterWebApiControlers(ContainerBuilder containerBuilder, HttpConfiguration configuration)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterWebApiFilterProvider(configuration);

            containerBuilder.RegisterWebApiModelBinderProvider();
        }

        private void RegisterAutoMapper(ContainerBuilder containerBuilder)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfiles(
                    typeof(UseCase.DAL.SmartGroceryContext),
                    typeof(Startup));
            });

            configuration.AssertConfigurationIsValid();

            containerBuilder
                .RegisterInstance(configuration)
                .As<MapperConfiguration>()
                .As<IConfigurationProvider>();

            containerBuilder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve));
        }

        private void RegisterMediator(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>().As<IMediator>();

            containerBuilder
                .RegisterAssemblyTypes(typeof(SmartGroceryContext).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)))
                .AsImplementedInterfaces();

            containerBuilder
                .RegisterAssemblyTypes(typeof(SmartGroceryContext).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces();

            containerBuilder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            containerBuilder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });
        }

        private void IntergrateDIContainerWithFrameworks(IAppBuilder builder, IContainer container, HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            builder.UseAutofacMiddleware(container);
            builder.UseWebApi(configuration);

            FluentValidationModelValidatorProvider.Configure(configuration, provider =>
            {
                provider.DisableDiscoveryOfPropertyValidators = true;
                provider.ImplicitlyValidateChildProperties = true;
            });
        }
    }
}