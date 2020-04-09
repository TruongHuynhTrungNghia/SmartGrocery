using MediatR;

namespace SmartGrocery.UseCase.Transactions
{
    public class GetAllTransactionsQuery : IRequest<TransactionDto>
    {
    }
}