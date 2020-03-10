using System;

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
            : this(productNumber, $"The product with number {productNumber} has been existed.")
        {
        }

        public string ProductNumber { get; }
    }
}