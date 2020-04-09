using SmartGrocery.Model.Common;
using System;
using System.Collections.Generic;

namespace SmartGrocery.Model.Transaction
{
    public class Transaction : Entity
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Customer.Customer Customer { get; set; }

        public virtual ICollection<Product.ProductSnapshot> ProductSnapshot { get; set; }

        public void Update(UpdatedTransaction transaction)
        {
            TransactionNumber = transaction.TransactionNumber;
            Amount = transaction.Amount;
            LastUpdatedAt = transaction.LastUpdatedAt;
            LastUpdatedBy = transaction.LastUpdatedBy;
            CustomerId = transaction.CustomerId;
        }
    }

    public class UpdatedTransaction
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public Guid CustomerId { get; set; }
    }
}