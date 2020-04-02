using MediatR;
using System;

namespace SmartGrocery.UseCase.Product
{
    public class EditProductCommnand : IRequest<string>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string ProductNumber { get; set; }

        public Guid Id { get; set; }
    }
}