using System;
using System.ComponentModel;

namespace SmartGrocery.WebUI.Models.Customer
{
    public class CreateCustomerViewModel
    {
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
    }
}