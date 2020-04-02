using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly SmartGroceryContext context;

        public DeleteProductCommandHandler(SmartGroceryContext context)
        {
            this.context = context;
        }

        public void Handle(DeleteProductCommand command)
        {
            var matchedProduct = context.Set<BaseProduct>()
                .FirstOrDefault(x => x.Id == command.Id);

            if (matchedProduct == null)
            {
                throw new NullReferenceException();
            }

            context.Set<BaseProduct>().Remove(matchedProduct);

            context.SaveChanges();
        }
    }
}