using MediatR;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.DAL;
using SmartGrocery.UseCase.Product;
using System.Data.Entity;
using System.Linq;

namespace SmartGrocery.UseCase.Transactions
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, string>
    {
        private readonly SmartGroceryContext context;
        private readonly ProductUpdater productUpdater;

        public DeleteTransactionCommandHandler(SmartGroceryContext context, ProductUpdater productUpdater)
        {
            this.context = context;
            this.productUpdater = productUpdater;
        }

        public string Handle(DeleteTransactionCommand command)
        {
            var existingTransaction = context.Set<Transaction>()
                .Include(x => x.ProductSnapshot)
                .FirstOrDefault(x => x.TransactionNumber == command.TransactionId);

            if (existingTransaction == null)
            {
                throw new TransactionIsNotExistException(command.TransactionId);
            }

            productUpdater.DeleteExistingProductSnapshot(existingTransaction.Id);

            context.Set<Transaction>().Remove(existingTransaction);

            context.SaveChanges();

            return command.TransactionId;
        }
    }
}