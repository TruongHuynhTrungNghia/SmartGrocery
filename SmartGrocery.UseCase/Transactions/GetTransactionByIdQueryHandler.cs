using AutoMapper;
using MediatR;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using System.Data.Entity;

namespace SmartGrocery.UseCase.Transactions
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly IMapper mapper;
        private readonly SmartGroceryContext context;

        public GetTransactionByIdQueryHandler(IMapper mapper, SmartGroceryContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public TransactionDto Handle(GetTransactionByIdQuery query)
        {
            var transaction = context.Set<Transaction>()
                .Include(x => x.ProductSnapshot)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == query.Id);

            var transactionDto = mapper.Map<TransactionDto>(transaction);

            return transactionDto;
        }
    }
}