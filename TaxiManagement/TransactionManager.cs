using System;
using System.Collections.Generic;
namespace TaxiManagement
{
    public class TransactionManager
    {
        public List<Transaction> Transactions { get; }

        public TransactionManager()
        {
            Transactions = new List<Transaction>();
        }

        public void RecordDrop(int taxiNum, bool pricePaid)
        {
            Transactions.Add(new DropTransaction(DateTime.Now, taxiNum, pricePaid));
        }

        public void RecordJoin(int taxiNum, int rankId)
        {
            Transactions.Add(new JoinTransaction(DateTime.Now, taxiNum, rankId));
        }

        public void RecordLeave(int rankId, Taxi t)
        {
            Transactions.Add(new LeaveTransaction(DateTime.Now, rankId, t));
        }
    }
}

