using MediatR;
using System;

namespace SmartGrocery.UseCase.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
    }
}