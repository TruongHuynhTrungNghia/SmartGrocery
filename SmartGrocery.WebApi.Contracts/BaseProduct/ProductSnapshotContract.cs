﻿namespace SmartGrocery.WebApi.Contracts.BaseProduct
{
    public class ProductSnapshotContract
    {
        public string ProductNumer { get; set; }

        public int NumberOfSoldProduct { get; set; }

        public decimal Price { get; set; }
    }
}