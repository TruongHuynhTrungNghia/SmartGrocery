﻿using SmartGrocery.Model.Common;
using System;
using System.Collections.Generic;

namespace SmartGrocery.Model.Product
{
    public class BaseProduct : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string ProductNumber { get; set; }

        public virtual ICollection<ProductSnapshot> ProductSnapshot { get; set; }

        public void Update(
            string name,
            decimal price,
            int quantity,
            DateTime expiryDate,
            DateTime manufacturingDate)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.ExpiryDate = expiryDate;
            this.ManufacturingDate = manufacturingDate;
        }

        public void UpdateQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public decimal CalculateTotalPrice(int numberOfProduct) => Price * numberOfProduct;
    }
}