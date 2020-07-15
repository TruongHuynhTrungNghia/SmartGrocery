using AutoMapper;
using MediatR;
using SmartGrocery.Model.Transaction;
using SmartGrocery.UseCase.DAL;
using System.Linq;

namespace SmartGrocery.UseCase.Transactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, TransactionDto[]>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetAllTransactionsQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TransactionDto[] Handle(GetAllTransactionsQuery query)
        {
            var transactions = context.Set<Transaction>()
                .Where(x => x.Id != null).ToArray()
                .OrderByDescending(x => x.CreatedAt);

            return mapper.Map<TransactionDto[]>(transactions);
        }
    }
}