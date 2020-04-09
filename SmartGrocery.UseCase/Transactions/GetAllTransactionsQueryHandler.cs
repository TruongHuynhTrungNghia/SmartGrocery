using MediatR;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, TransactionDto>
    {
        public TransactionDto Handle(GetAllTransactionsQuery message)
        {
            throw new NotImplementedException();
        }
    }
}