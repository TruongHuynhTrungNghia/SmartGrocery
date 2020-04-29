using MediatR;
using System;

namespace SmartGrocery.UseCase.Customer
{
    public class CreateCustomerCommand : IRequest<string>
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }
    }
}