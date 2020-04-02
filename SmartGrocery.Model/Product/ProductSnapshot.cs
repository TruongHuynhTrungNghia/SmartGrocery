using SmartGrocery.Model.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGrocery.Model.Product
{
    public class ProductSnapshot : Entity
    {
        public Guid ProductId { get; set; }

        public Guid TransactionId { get; set; }

        public int NumberOfSoldProduct { get; set; }

        public int RemainingProduct { get; set; }

        public string Status { get; set; }

        public virtual BaseProduct Product { get; set; }

        public virtual Transaction.Transaction Transaction { get; set; }
    } 
}