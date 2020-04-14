using System;

namespace SmartGrocery.UseCase.Customer
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string customerId, string message)
            : base(message)
        {
            CustomerId = customerId;
        }

        public CustomerNotFoundException(string customerId)
            : this(customerId, $"Customer with {customerId} doesn't exist.")
        {
        }

        private string CustomerId { get; }
    }
}