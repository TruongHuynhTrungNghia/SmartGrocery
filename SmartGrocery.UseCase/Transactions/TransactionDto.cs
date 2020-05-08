using SmartGrocery.UseCase.Product;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }

        public string TransactionNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerId { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public ProductSnapshotDto[] ProductSnapshotDtos { get; set; }
    }
}