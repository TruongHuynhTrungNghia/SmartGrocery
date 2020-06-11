using System;

namespace SmartGrocery.WebApi.Contracts.Customer
{
    public class EditCustomerRequest
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }

        public string Email { get; set; }

        public string IdNumber { get; set; }
    }
}