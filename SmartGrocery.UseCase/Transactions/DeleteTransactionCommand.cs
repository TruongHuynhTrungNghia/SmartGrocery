using MediatR;

namespace SmartGrocery.UseCase.Transactions
{
    public class DeleteTransactionCommand : IRequest<string>
    {
        public string TransactionId { get; set; }
    }
}