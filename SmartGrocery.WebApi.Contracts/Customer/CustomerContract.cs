﻿using System;

namespace SmartGrocery.WebApi.Contracts.Customer
{
    public class CustomerContract
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int Points { get; set; }
    }
}