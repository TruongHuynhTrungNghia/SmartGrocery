using SmartGrocery.WebUI.Models.Products;
using System;
using System.ComponentModel;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class TransactionDetailsViewModel : TransactionViewModel
    {
        public TransactionDetailsViewModel()
        {
            CreatedAt = DateTime.Now;
            ProductSnapshots = new ProductSnapshotViewModel[4];
        }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public ProductSnapshotViewModel[] ProductSnapshots { get; set; }
    }
}