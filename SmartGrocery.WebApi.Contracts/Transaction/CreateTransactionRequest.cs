using SmartGrocery.WebApi.Contracts.BaseProduct;
using System;

namespace SmartGrocery.WebApi.Contracts.Transaction
{
    public class CreateTransactionRequest
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public string CustomerId { get; set; }

        public ProductSnapshotContract[] ProductSnapshotContracts { get; set; }
    }
}