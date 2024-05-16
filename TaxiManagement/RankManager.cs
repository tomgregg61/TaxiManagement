using System;
using System.Collections.Generic;
namespace TaxiManagement
{
    public class RankManager
    {
        public Dictionary<int, int> taxiRanks;
        int defaultNumberOfTaxiSpaces = 5;
        private Dictionary<int, Rank> ranks;

        public RankManager()
        {
            ranks = new Dictionary<int, Rank>
            {
                { 1, new Rank(1, defaultNumberOfTaxiSpaces) },
                { 2, new Rank(2, defaultNumberOfTaxiSpaces - 3) },
                { 3, new Rank(3, defaultNumberOfTaxiSpaces - 1) }
            };
            taxiRanks = new Dictionary<int, int>();
        }

        public bool AddTaxiToRank(Taxi t, int rankId)
        {
            if (ranks.ContainsKey(rankId))
            {
                if (taxiRanks.ContainsKey(t.Number))
                {
                    return false;
                }
                if (t.Destination != "")
                {
                    return false;
                }
                Rank rank = ranks[rankId];
                if (rank.AddTaxi(t))
                {
                    taxiRanks[t.Number] = rankId;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Rank FindRank(int rankId)
        {
            if (ranks != null && ranks.ContainsKey(rankId))
            {
                return ranks[rankId];
            }
            return null;
        }

        public Taxi FrontTaxiInRankTakesFare(int rankId, string destination, double agreedPrice)
        {
            Rank rank = FindRank(rankId);

            if (rank == null)
            {
                return null;
            }

            Taxi frontTaxi = rank.FrontTaxiTakesFare(destination, agreedPrice);

            if (frontTaxi == null)
            {
                return null;
            }

            return frontTaxi;
        }
    }
}

