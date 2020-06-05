using System;
using System.ComponentModel;

namespace SmartGrocery.WebUI.Models.Customer
{
    public class CustomerViewModel
    {
        [DisplayName("Full Name")]
        public string CustomerFullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DisplayName("Customer ID")]
        public string CustomerId { get; set; }

        [DisplayName("DOB")]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }

        public string Email { get; set; }

        public string Idnumber { get; set; }
    }
}