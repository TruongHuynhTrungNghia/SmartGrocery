using MediatR;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class EditProductCommnandHandler : IRequestHandler<EditProductCommnand, string>
    {
        private readonly SmartGroceryContext context;

        public EditProductCommnandHandler(SmartGroceryContext context)
        {
            this.context = context;
        }

        public string Handle(EditProductCommnand commnand)
        {
            var existingProduct = context.Set<BaseProduct>()
                .SingleOrDefault(x => x.ProductNumber == commnand.ProductNumber);

            if (existingProduct == null)
            {
                throw new NullReferenceException($"The product with ID {commnand.ProductNumber} does not exist.");
            }

            existingProduct.Update(
                commnand.Name,
                commnand.Price,
                commnand.Quantity,
                commnand.ExpiryDate,
                commnand.ManufacturingDate);

            context.SaveChanges();

            return existingProduct.ProductNumber;
        }
    }
}