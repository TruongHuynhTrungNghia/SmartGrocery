using AutoMapper;
using MediatR;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.DAL;
using SmartGrocery.UseCase.Product;
using System.Collections.Generic;
using System.Linq;

namespace SmartGrocery.UseCase.Transactions
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
    {
        private readonly IMapper mapper;
        private readonly SmartGroceryContext context;
        private readonly ProductUpdater productUpdater;

        public CreateTransactionCommandHandler(
            IMapper mapper,
            SmartGroceryContext context,
            ProductUpdater productUpdater)
        {
            this.mapper = mapper;
            this.context = context;
            this.productUpdater = productUpdater;
        }

        public string Handle(CreateTransactionCommand command)
        {
            var existingTransaction = context.Set<Transaction>()
                .FirstOrDefault(x => x.TransactionNumber == command.TransactionNumber);

            if (existingTransaction != null)
            {
                throw new DuplicateTransactionNumerException(command.TransactionNumber);
            }

            var transaction = mapper.Map<Transaction>(command);

            if (productUpdater.ValidListOfProductResult(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto)).IsValid)
            {
                transaction.ProductSnapshot = productUpdater
                    .CreateNewListofProductSnapshot(
                        mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto), 
                        transaction.Id);

                context.Set<Transaction>().Add(transaction);

                context.SaveChangesAsync();
                //TODO: Move this function after implementing background jobs. 
                productUpdater.UpdateProductQuantity(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto));
            }

            return transaction.TransactionNumber;
        }
    }
}