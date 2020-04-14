using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using CustomerBase = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, CustomerDetailsDto[]>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public GetAllCustomerQueryHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CustomerDetailsDto[] Handle(GetAllCustomerQuery query)
        {
            var customers = context.Set<CustomerBase>().Where(x => x.Id != null).ToArray();
            var customerDtos = mapper.Map<CustomerDetailsDto[]>(customers);

            return customerDtos;
        }
    }
}