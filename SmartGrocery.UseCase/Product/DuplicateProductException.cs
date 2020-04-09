using System;
using System.Runtime.Serialization;

namespace SmartGrocery.UseCase.Product
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string productNumber, string message, Exception innerException)
            : base(message, innerException)
        {
            ProductNumber = productNumber;
        }

        public DuplicateProductException(string productNumber, string message)
            : this(productNumber, message, null)
        {
        }

        public DuplicateProductException(string productNumber)
            : this(productNumber, $"The product with number {productNumber} is existing.")
        {
        }

        public string ProductNumber { get; }
    }
}