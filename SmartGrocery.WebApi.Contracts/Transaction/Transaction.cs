using System;

namespace SmartGrocery.WebApi.Contracts.Transaction
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }
    }
}