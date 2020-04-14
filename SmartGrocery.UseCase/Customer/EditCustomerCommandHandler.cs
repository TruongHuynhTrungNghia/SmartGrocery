using AutoMapper;
using MediatR;
using SmartGrocery.UseCase.DAL;
using System.Linq;
using CustomerBase = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, string>
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public EditCustomerCommandHandler(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Handle(EditCustomerCommand command)
        {
            var existingCustomer = context.Set<CustomerBase>()
                .FirstOrDefault(x => x.CustomerId == command.CustomerId);

            if (existingCustomer == null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            existingCustomer.Update(mapper.Map<Model.Customer.UpdatedCustomer>(command));

            context.SaveChanges();

            return existingCustomer.CustomerId;
        }
    }
}