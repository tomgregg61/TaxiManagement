using System;
namespace TaxiManagement
{
    //Parent class to DropTransaction, JoinTransaction, and LeaveTransaction
    public abstract class Transaction
    {
        public DateTime TransactionDatetime { get; }
        public string TransactionType { get; }

        public Transaction(string type, DateTime dt)
        {
            TransactionDatetime = dt;
            TransactionType = type;
        }

        public abstract override string ToString();
    }
}

