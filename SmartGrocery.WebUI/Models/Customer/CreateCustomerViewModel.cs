using FluentValidation;
using System;
using System.ComponentModel;

namespace SmartGrocery.WebUI.Models.Customer
{
    public class CreateCustomerViewModel
    {
        public CreateCustomerViewModel()
        {
            DateOfBirth = DateTime.Now;
        }

        public int Age { get; set; }

        [DisplayName("First Name")]
        public string CustomerFirstName { get; set; }

        [DisplayName("Customer ID")]
        public string CustomerId { get; set; }

        [DisplayName("Last Name")]
        public string CustomerLastName { get; set; }

        [DisplayName("DOB")]
        public DateTime DateOfBirth { get; set; }

        public int Points { get; set; }

        public string IdNumber { get; set; }

        public string Email { get; set; }
    }

    public class CreateCustomerViewModelValidator : AbstractValidator<CreateCustomerViewModel>
    {
        public CreateCustomerViewModelValidator()
        {
            RuleFor(x => x.CustomerFirstName).NotEmpty();
            RuleFor(x => x.CustomerLastName).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Age)
                .NotEmpty()
                .InclusiveBetween(18, 100);
        }
    }
}