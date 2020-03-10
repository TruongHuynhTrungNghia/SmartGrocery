using AutoMapper;
using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class CreateBaseProductCommandHandler : IRequestHandler<CreateBaseProductCommand, string>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public CreateBaseProductCommandHandler(
            SmartGroceryContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Handle(CreateBaseProductCommand command)
        {
            var existingProduct = context.Set<BaseProduct>()
                .FirstOrDefault(x => x.ProductNumber == command.ProductNumber);

            if (existingProduct != null)
            {
                throw new DuplicateProductException(command.ProductNumber);
            }

            var newProduct = mapper.Map<BaseProduct>(command);

            context.Set<BaseProduct>().Add(newProduct);

            context.SaveChangesAsync();

            return newProduct.ProductNumber;
        }
    }
}