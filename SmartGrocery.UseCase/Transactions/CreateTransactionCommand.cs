using MediatR;
using SmartGrocery.UseCase.Product;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class CreateTransactionCommand : IRequest<string>
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public string CustomerId { get; set; }

        public string CustomerEmotion { get; set; }

        public decimal CustomerEmotionProbability { get; set; }

        public ProductSnapshotDto[] ProductSnapshotDto { get; set; }
    }
}