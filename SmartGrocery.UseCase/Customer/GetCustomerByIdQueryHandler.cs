using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrocery.UseCase.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto>
    {
        public GetCustomerByIdQueryHandler()
        {

        }

        public CustomerDetailsDto Handle(GetCustomerByIdQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
