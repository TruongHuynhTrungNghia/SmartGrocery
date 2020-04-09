using System;

namespace SmartGrocery.UseCase.Transactions
{
    [Serializable]
    internal class DuplicateTransactionNumerException : Exception
    {
        public DuplicateTransactionNumerException(string transactionNumber, string message)
            : base(message)
        {
            TransactionNumber = transactionNumber;
        }

        public DuplicateTransactionNumerException(string transactionNumber)
            : this(transactionNumber, $"The transaction with numbber {transactionNumber} is existing.")
        {
        }

        public string TransactionNumber { get; }
    }
}