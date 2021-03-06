﻿using SmartGrocery.WebApi.Contracts.Transaction;
using System;

namespace SmartGrocery.WebApi.Contracts.Customer
{
    public class CustomerDetailsContract
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }

        public string Email { get; set; }

        public string IdNumber { get; set; }

        public TransactionDetails[] TransactionContracts { get; set; }
    }
}