using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using Customerbase = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetCustomerByIdQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CustomerDetailsDto Handle(GetCustomerByIdQuery query)
        {
            var customer = context.Set<Customerbase>()
                .AsNoTracking()
                .FirstOrDefault(x => x.CustomerId == query.CustomerId);

            var customerDto = mapper.Map<CustomerDetailsDto>(customer);

            return customerDto;
        }
    }
}