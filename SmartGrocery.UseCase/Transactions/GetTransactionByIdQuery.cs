using MediatR;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public Guid Id { get; set; }
    }
}