using System;
namespace TaxiManagement
{
    public class DropTransaction : Transaction //Inheritance
    {
        int taxiNum;
        bool priceWasPaid;

        public DropTransaction(DateTime transactionDatetime, int taxiNum, bool priceWasPaid) : base("Drop fare", transactionDatetime)
        {
            this.taxiNum = taxiNum;
            this.priceWasPaid = priceWasPaid;
        }
        //Overriden method
        public override string ToString()
        {
            string dateStr = TransactionDatetime.ToString("dd/MM/yyyy HH:mm");
            return $"{dateStr} {TransactionType,-9} - Taxi {taxiNum}, {(priceWasPaid ? "price was paid" : "price was not paid")}";
        }
    }
}

