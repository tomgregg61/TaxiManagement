using System;

namespace TaxiManagement
{
    public class LeaveTransaction : Transaction //Inheritance
    {
        int taxiNum;
        int rankId;
        string destination;
        double agreedPrice;

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi t) : base("Leave", transactionDatetime)
        {
            this.rankId = rankId;
            taxiNum = t.Number;
            destination = t.Destination;
            agreedPrice = t.CurrentFare;
        }
        //Overriden method
        public override string ToString()
        {
            string dateStr = TransactionDatetime.ToString("dd/MM/yyyy HH:mm");
            return $"{dateStr} {TransactionType,-9} - Taxi {taxiNum} from rank {rankId} to {destination} for £{agreedPrice}";
        }
    }
}

