using SmartGrocery.UseCase.DAL;
using SmartGrocery.UseCase.Transactions;
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
            return context.Set<CustomerModel>()
                .FirstOrDefault(x => x.CustomerId == customerNumber);
        }

        public void UpdateCustomerEmotion(CustomerModel customer, CreateTransactionCommand command)
        {
            customer.LastestCustomerEmotion = command.CustomerEmotion;
            customer.EmotionProbability = command.CustomerEmotionProbability;

            context.Entry<CustomerModel>(customer).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }
    }
}