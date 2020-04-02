using MediatR;
using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
    {
        public string Handle(CreateTransactionCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
