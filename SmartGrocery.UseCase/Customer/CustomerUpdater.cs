using SmartGrocery.UseCase.DAL;
using System.Linq;
using CustomerModel = SmartGrocery.Model.Customer.Customer;

namespace SmartGrocery.UseCase.Customer
{
    public class CustomerUpdater
    {
        private readonly SmartGroceryContext context;

        public CustomerUpdater(SmartGroceryContext context)
        {
            this.context = context;
        }

        public CustomerModel GetCustomerByCustomerNumber(string customerNumber)
        {
            var model = context.Set<CustomerModel>()
                .FirstOrDefault(x => x.CustomerId == customerNumber);

            return model;
        }
    }
}