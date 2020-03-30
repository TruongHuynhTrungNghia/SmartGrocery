using System;

namespace SmartGrocery.WebUI.Models.Products
{
    public class ProductBaseViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string ProductNumber { get; set; }
    }
}