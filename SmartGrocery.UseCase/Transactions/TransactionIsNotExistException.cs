using System;

namespace SmartGrocery.UseCase.Transactions
{
    public class TransactionIsNotExistException : Exception
    {
        public TransactionIsNotExistException(string transactionNumber, string message)
            : base(message)
        {
            TransactionNumber = transactionNumber;
        }

        public TransactionIsNotExistException(string transactionNumber)
            : this(transactionNumber, $"Transaction number {transactionNumber} does not exist in this current context.")
        {
        }

        private string TransactionNumber { get; }
    }
}