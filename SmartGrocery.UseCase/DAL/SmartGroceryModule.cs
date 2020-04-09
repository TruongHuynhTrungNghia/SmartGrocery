using Autofac;
using SmartGrocery.UseCase.Product;

namespace SmartGrocery.UseCase.DAL
{
    public class SmartGroceryModule : Module
    {
        private const string SmartGroceryConnectionString = "SmartGroceryDatabase";

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDbContext(builder);

            builder.RegisterType<ProductUpdater>().AsSelf();
        }

        private void RegisterDbContext(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<SmartGroceryContext>()
                .AsSelf()
                //.UsingConstructor(typeof(string), typeof(ILifetimeScope))
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(string), (pi, ctx) => SmartGroceryConnectionString);
        }
    }
}