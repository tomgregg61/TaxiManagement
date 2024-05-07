using System;
using System.Collections.Generic;
namespace TaxiManagement
{
    public class TaxiManager
    {
        public SortedDictionary<int, Taxi> taxis;

        public Taxi CreateTaxi(int taxiNum)
        {
            if (!GetAllTaxis().ContainsKey(taxiNum))
            {
                Taxi newTaxi = new Taxi(taxiNum);
                GetAllTaxis().Add(taxiNum, newTaxi);
                return newTaxi;
            }
            else
            {
                return FindTaxi(taxiNum);
            }
        }

        public Taxi FindTaxi(int taxiNum)
        {
            if (taxis == null)
            {
                return null;
            }
            else if (!taxis.ContainsKey(taxiNum))
            {
                return null;
            }
            else
            {
                return taxis[taxiNum];
            }
        }

        public SortedDictionary<int, Taxi> GetAllTaxis()
        {
            if (taxis == null)
            {
                taxis = new SortedDictionary<int, Taxi>();
            }
            return taxis;
        }
    }
}

