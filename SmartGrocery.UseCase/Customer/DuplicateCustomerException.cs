using System;

namespace SmartGrocery.UseCase.Customer
{
    [Serializable]
    public class DuplicateCustomerException : Exception
    {

        protected DuplicateCustomerException(string customerId, string message, Exception innerException) 
            : base(message, innerException)
        {
            CustomerId = customerId;
        }

        protected DuplicateCustomerException(string customerId, string message)
            : this(customerId, message, null)
        {
        }

        public DuplicateCustomerException(string customerId)
            : this(customerId, $"Customer with {customerId} has been already existed.")
        {
        }

        private string CustomerId { get; }
    }
}