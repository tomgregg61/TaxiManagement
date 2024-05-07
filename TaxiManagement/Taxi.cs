using System;
namespace TaxiManagement
{
    public class Taxi
    {
        public const string IN_RANK = "in rank";
        public const string ON_ROAD = "on the road";

        public double CurrentFare { get; private set; }
        public string Destination { get; private set; }
        public string Location { get; private set; }
        public int Number { get; }
        public double TotalMoneyPaid { get; private set; }
        //Public getter, setter method
        private Rank rank;
        public Rank Rank
        {
            //custom set so that a Taxi can only have a rank when the fare has been dropped and the Rank isnt null
            get { return rank; }
            set
            {
                if (Destination != null && Destination != "")
                {
                    throw new Exception("Cannot join rank if fare has not been dropped");
                }
                if (value == null)
                {
                    throw new Exception("Rank cannot be null");
                }
                rank = value;
                Location = IN_RANK;
            }
        }

        public Taxi(int taxiNum)
        {
            Number = taxiNum;
            CurrentFare = 0;
            Destination = "";
            Location = ON_ROAD;
            TotalMoneyPaid = 0; 
        }

        public void AddFare(string destination, double agreedPrice)
        {
            CurrentFare = agreedPrice; 
            Destination = destination; 
            rank = null;
        }

        public void DropFare(bool priceWasPaid)
        {
            if (priceWasPaid)
            {
                TotalMoneyPaid += CurrentFare;
            }
            CurrentFare = 0;
            Destination = "";
        }
    }
}

