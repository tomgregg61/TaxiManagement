using System;
using System.Collections.Generic;
namespace TaxiManagement
{
	public class UserUI
	{
		private RankManager rankMgr;
		private TaxiManager taxiMgr;
		private TransactionManager transactionMgr;
        public bool priceIsPaid = false;

        public UserUI(RankManager rkMgr, TaxiManager txMgr, TransactionManager trMgr)
		{
            rankMgr = rkMgr;
            taxiMgr = txMgr;
            transactionMgr = trMgr;
		}

		public List<string> TaxiDropsFare(int taxiNum, bool pricePaid)
		{
            List<string> expectedLines = new List<string>();
            Taxi t = taxiMgr.FindTaxi(taxiNum);
            if (t.CurrentFare == 0)
            {
                expectedLines.Add($"Taxi {taxiNum} has not dropped its fare.");
                return expectedLines;
            }
            transactionMgr.RecordDrop(taxiNum, pricePaid);

            if (!pricePaid)
            {
                expectedLines.Add($"Taxi {taxiNum} has dropped its fare and the price was not paid.");
            }
            else
            {
                expectedLines.Add($"Taxi {taxiNum} has dropped its fare and the price was paid.");
                priceIsPaid = true;
                t.DropFare(true);
            }
            return expectedLines;
		}

        public List<string> TaxiJoinsRank(int taxiNum, int rankId)
        {
            List<string> expectedLines = new List<string>();
            if (taxiMgr.FindTaxi(taxiNum) == null)
            {
                taxiMgr.CreateTaxi(taxiNum);
            }
            Taxi t = taxiMgr.FindTaxi(taxiNum);
            if (t.Rank != null)
            {
                expectedLines.Add($"Taxi {taxiNum} has not joined rank {rankId}.");
            }
            else
            {
                if(rankMgr.AddTaxiToRank(t,rankId) == false)
                {
                    expectedLines.Add($"Taxi {taxiNum} has not joined rank {rankId}.");
                }
                else
                {
                    rankMgr.AddTaxiToRank(t, rankId);
                    expectedLines.Add($"Taxi {taxiNum} has joined rank {rankId}.");
                    transactionMgr.RecordJoin(taxiNum, rankId);
                }
            }
            return expectedLines;
        }

        public List<string> TaxiLeavesRank(int rankId, string destination, double agreedPrice)
        {
            List<string> expectedLines = new List<string>();
            Taxi t = rankMgr.FrontTaxiInRankTakesFare(rankId, destination, agreedPrice);
            if(t == null)
            {
                expectedLines.Add($"Taxi has not left rank {rankId}.");
                return expectedLines;
            }
            rankMgr.taxiRanks.Remove(t.Number);
            transactionMgr.RecordLeave(rankId, t);
            expectedLines.Add($"Taxi {t.Number} has left rank {rankId} to take a fare to {destination} for £{agreedPrice}.");
            return expectedLines;
        }

		public List<string> ViewFinancialReport()
		{
            List<string> expectedLines = new List<string>
            {
                "Financial report",
                "================"
            };
            double taxiMoney = 0;
            if (taxiMgr.GetAllTaxis().Count == 0)
            {
                expectedLines.Add("No taxis, so no money taken");
            }
            else
            {
                foreach (KeyValuePair<int, Taxi> entry in taxiMgr.taxis)
                {
                    double CurrTaxi = entry.Value.TotalMoneyPaid + entry.Value.CurrentFare;
                    taxiMoney += CurrTaxi;
                    Console.WriteLine(entry.Value.TotalMoneyPaid);
                    expectedLines.Add($"Taxi {entry.Key}      {CurrTaxi:F2}");
                }
                expectedLines.Add("           ======");
                expectedLines.Add($"Total:       {taxiMoney:F2}");
                expectedLines.Add("           ======");
            }
            return expectedLines;
        }

        public List<string> ViewTaxiLocations()
        {
            List<string> expectedLines = new List<string>
            {
                "Taxi locations",
                "=============="
            };
            if (taxiMgr.taxis == null)
            {
                expectedLines.Add("No taxis");
            }
            else
            {
                foreach (KeyValuePair<int, Taxi> entry in taxiMgr.taxis)
                {
                    int taxiNum = entry.Value.Number;

                    if (rankMgr.taxiRanks.ContainsKey(taxiNum))
                    {
                        int rankId = rankMgr.taxiRanks[taxiNum];
                        expectedLines.Add($"Taxi {taxiNum} is in rank {rankId}");
                    }
                    else
                    {
                        string destination = taxiMgr.FindTaxi(taxiNum).Destination;
                        if(priceIsPaid)
                        {
                            expectedLines.Add($"Taxi {taxiNum} is on the road");
                        }
                        else
                        {
                            expectedLines.Add($"Taxi {taxiNum} is on the road to {destination}");
                        }
                    }
                }
            }
            return expectedLines;
        }

        public List<string> ViewTransactionLog()
        {
            List<string> expectedLines = new List<string>
            {
                "Transaction report",
                "=================="
            };
            if (transactionMgr.Transactions.Count == 0)
            {
                expectedLines.Add("No transactions");
            }
            else
            {
                foreach(Transaction entry in transactionMgr.Transactions)
                {
                    expectedLines.Add($"{entry}");
                }
            }
            return expectedLines;
        }
    }
}

