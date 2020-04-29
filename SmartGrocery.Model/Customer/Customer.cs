﻿using SmartGrocery.Model.Common;
using System;
using System.Collections.Generic;

namespace SmartGrocery.Model.Customer
{
    public class Customer : Entity
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }

        public virtual ICollection<Transaction.Transaction> Transactions { get; set; }

        public void Update(UpdatedCustomer customer)
        {
            LastName = customer.LastName;
            FirstName = customer.FirstName;
            CustomerId = customer.CustomerId;
            DateOfBirth = customer.DateOfBirth;
            Age = customer.Age;
            Points = customer.Points;
        }

        public string CustomerFullName => $"{LastName} {FirstName}";
    }

    public class UpdatedCustomer
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }
    }
}