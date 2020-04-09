using MediatR;
using System;

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