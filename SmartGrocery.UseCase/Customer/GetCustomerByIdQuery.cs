using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrocery.UseCase.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
    }
}
