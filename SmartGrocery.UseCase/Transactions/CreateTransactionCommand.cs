using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrocery.UseCase.Transactions
{
    public class CreateTransactionCommand :IRequest<string>
    {
    }
}
