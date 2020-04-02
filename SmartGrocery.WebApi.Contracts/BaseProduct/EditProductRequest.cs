using System;

namespace SmartGrocery.WebApi.Contracts.BaseProduct
{
    public class EditProductRequest
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