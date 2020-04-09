using MediatR;
using SmartGrocery.UseCase.Product;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class UpdateTransactionCommand : IRequest<string>
    {
        public string TransactionNumber { get; set; }

        public string Amount { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedAt { get; set; }

        public Guid CustomerId { get; set; }

        public ProductSnapshotDto[] ProductSnapshotDto { get; set; }
    }
}