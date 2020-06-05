using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using CustomerMaster = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class SearchCustomerByCustomerNumberQueryHandler : IRequestHandler<SearchCustomerByCustomerNumberQuery, CustomerDetailsDto>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public SearchCustomerByCustomerNumberQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CustomerDetailsDto Handle(SearchCustomerByCustomerNumberQuery query)
        {
            var customer = context.Set<CustomerMaster>().SingleOrDefault(c => c.CustomerId.Contains(query.CustomerNumber));

            return mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}