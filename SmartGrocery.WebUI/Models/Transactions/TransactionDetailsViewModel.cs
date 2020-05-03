﻿using SmartGrocery.WebUI.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class TransactionDetailsViewModel : TransactionViewModel
    {
        public TransactionDetailsViewModel()
        {
            CreatedAt = DateTime.Now;
        }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public ProductSnapshotViewModel[] ProductSnapshots { get; set; }

        internal void CreateNewProductSnapshot()
        {
            var product = new List<ProductSnapshotViewModel>();

            product.Add(new ProductSnapshotViewModel());

            this.ProductSnapshots = product.ToArray();
        }
    }
}