﻿using SmartGrocery.WebUI.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class TransactionDetailsViewModel : TransactionViewModel
    {
        public TransactionDetailsViewModel()
        {
            CreatedAt = DateTime.Now;
        }

        [DisplayName("Customer Id")]
        public string CustomerId { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Customer Emotion")]
        public string CustomerEmotion { get; set; }

        public string CustomerEmotionProbability { get; set; }

        public ProductSnapshotViewModel[] ProductSnapshots { get; set; }

        internal void CreateNewProductSnapshot()
        {
            var product = new List<ProductSnapshotViewModel>();

            product.Add(new ProductSnapshotViewModel());

            this.ProductSnapshots = product.ToArray();
        }

        internal void CalculateProductPrice()
        {
            foreach (var snapshot in this.ProductSnapshots)
            {
                snapshot.UpdateProductPriceByQuantity();
            }
        }

        internal void RemoveEmptyProduct()
        {
            this.ProductSnapshots = this.ProductSnapshots.Where(x => !string.IsNullOrEmpty(x.ProductNumber)).ToArray();
        }
    }
}