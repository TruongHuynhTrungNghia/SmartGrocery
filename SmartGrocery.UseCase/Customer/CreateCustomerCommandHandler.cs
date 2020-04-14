using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using CustomerModel = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public CreateCustomerCommandHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Handle(CreateCustomerCommand command)
        {
            var existingCustomer = context.Set<CustomerModel>()
                .AsNoTracking()
                .FirstOrDefault(x => x.CustomerId == command.CustomerId);

            if (existingCustomer != null)
            {
                throw new DuplicateCustomerException(command.CustomerId);
            }

            var newCustomer = mapper.Map<CustomerModel>(command);

            context.Set<CustomerModel>().Add(newCustomer);

            context.SaveChanges();

            return newCustomer.CustomerId;
        }
    }
}