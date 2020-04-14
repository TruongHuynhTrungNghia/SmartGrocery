using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class TransactionDto
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }
    }
}