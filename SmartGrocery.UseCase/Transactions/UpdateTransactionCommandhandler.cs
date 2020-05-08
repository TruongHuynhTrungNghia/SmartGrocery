using AutoMapper;
using MediatR;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.DAL;
using SmartGrocery.UseCase.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartGrocery.UseCase.Transactions
{
    public class UpdateTransactionCommandhandler : IRequestHandler<UpdateTransactionCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly SmartGroceryContext context;
        private readonly ProductUpdater productUpdater;

        public UpdateTransactionCommandhandler(
            IMapper mapper,
            SmartGroceryContext context,
            ProductUpdater productUpdater)
        {
            this.mapper = mapper;
            this.context = context;
            this.productUpdater = productUpdater;
        }

        public Guid Handle(UpdateTransactionCommand command)
        {
            var existingtransaction = context.Set<Transaction>()
                .FirstOrDefault(x => x.TransactionNumber == command.TransactionNumber);

            if (existingtransaction == null)
            {
                throw new TransactionIsNotExistException(command.TransactionNumber);
            }

            existingtransaction.Update(mapper.Map<UpdatedTransaction>(command));

            UpdateProduct(existingtransaction, command);

            context.SaveChanges();

            return existingtransaction.Id;
        }

        private void UpdateProduct(Transaction existingtransaction, UpdateTransactionCommand command)
        {
            if (productUpdater.ValidListOfProductResult(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto)).IsValid)
            {
                productUpdater.RevertQuantiyOfBaseProduct(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(existingtransaction.ProductSnapshot));

                productUpdater.DeleteExistingProductSnapshot(existingtransaction.Id);

                var productSnapshots = productUpdater.CreateNewListofProductSnapshot(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto), existingtransaction.Id).ToList();
                foreach (var snapshot in productSnapshots)
                {
                    existingtransaction.ProductSnapshot.Add(snapshot);
                }

                productUpdater.UpdateProductQuantity(mapper.Map<IEnumerable<UpdatedProductSnapshot>>(command.ProductSnapshotDto));
            }
        }
    }
}