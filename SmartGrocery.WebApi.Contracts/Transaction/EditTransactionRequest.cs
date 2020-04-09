using SmartGrocery.WebApi.Contracts.BaseProduct;
using System;

namespace SmartGrocery.WebApi.Contracts.Transaction
{
    public class EditTransactionRequest
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public Guid CustomerId { get; set; }

        public ProductSnapshotContract[] ProductSnapshotContracts { get; set; }
    }
}