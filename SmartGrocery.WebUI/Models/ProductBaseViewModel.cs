using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGrocery.WebUI.Models
{
    public class ProductBaseViewModel
    {
        public ProductBaseViewModel()
        {
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string ProductNumber { get; set; }
    }
}