using MediatR;
using System;

namespace SmartGrocery.UseCase.Customer
{
    public class EditCustomerCommand : IRequest<string>
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }
    }
}