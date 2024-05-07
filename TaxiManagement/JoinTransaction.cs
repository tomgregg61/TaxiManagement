using System;
namespace TaxiManagement
{
    public class JoinTransaction : Transaction //Inheritance
    {
        private int taxiNum;
        private int rankId;

        public JoinTransaction(DateTime transactionDatetime, int taxiNum, int rankId) : base("Join", transactionDatetime)
        {
            this.taxiNum = taxiNum;
            this.rankId = rankId;
        }
        //Overriden method
        public override string ToString()
        {
            string dateStr = TransactionDatetime.ToString("dd/MM/yyyy HH:mm");
            return $"{dateStr} {TransactionType,-9} - Taxi {taxiNum} in rank {rankId}";
        }
    }
}

