using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Collections;


namespace Server_AuctionHouse
{
    public class AuctionHandler
    {
        private Stopwatch timer { get; set; }

        public AuctionHandler()
        {
            timer = new Stopwatch();
        }

        public void AuctionStart(Bidder bidder)
        {
            bidder.Write("Welcome to the Auction, current status is: ");
            AuctionHouseRepo.Instance.itemList.ForEach(item => bidder.Write(item.ToString())); 
            
            bidder.Write($"Current item is {AuctionHouseRepo.Instance.CurrentItem.ToString()} Starting at  " +
                                         $"{AuctionHouseRepo.Instance.CurrentItem.StartingPrice.ToString()} " +
                 $"Current bid for Item is {AuctionHouseRepo.Instance.CurrentBid.ToString()}");

            GetIncomingBids(bidder);
        }

        public void Gavel(Item item)
        {
            AuctionHouseRepo.Instance.CurrentItem = item;
            Server.Instance.Broadcast($"New Item up for aution is {item.ToString()} Starting at {item.StartingPrice.ToString()}");

            
            while (item.Sold == false)
            {
                if (AuctionHouseRepo.Instance.NextBid > AuctionHouseRepo.Instance.CurrentBid)
                {
                    Server.Instance.Broadcast($"New current Bid on Item { item.ToString()} {AuctionHouseRepo.Instance.NextBid} Highest Bidder is " +
                                                                                         $"{AuctionHouseRepo.Instance.HighestBidder.ToString()}");

                    AuctionHouseRepo.Instance.CurrentBid = AuctionHouseRepo.Instance.NextBid;
                    timer.Restart();
                }
                if (timer.ElapsedMilliseconds == 10000)
                {
                    Server.Instance.Broadcast("First!");
                    Thread.Sleep(1);
                }
                if (timer.ElapsedMilliseconds == 15000)
                {
                    Server.Instance.Broadcast("Second!");
                    Thread.Sleep(1);
                }
                if (timer.ElapsedMilliseconds == 18000)
                {
                    Server.Instance.Broadcast($"Third! {item.ToString()} Sold to {AuctionHouseRepo.Instance.HighestBidder.ToString()}" +
                                                                     $" For ddk { AuctionHouseRepo.Instance.CurrentBid}");                   
                    Thread.Sleep(1);
                    AuctionHouseRepo.Instance.NextBid = 0;                    
                    item.AuctionEndPrice = AuctionHouseRepo.Instance.CurrentBid;
                    AuctionHouseRepo.Instance.CurrentBid = 0;
                    item.Sold = true;                  
                }
            }
        }       

        public void GetIncomingBids(Bidder bidder)
        {
            try
            {
                decimal bid;
                while (true)
                {
                    bool correctformat = decimal.TryParse(bidder.Read(), out bid);
                    if (correctformat == false)
                    {
                        bidder.Write("Not a Number!");
                    }
                    if (bid <= AuctionHouseRepo.Instance.CurrentBid && bid < AuctionHouseRepo.Instance.CurrentItem.StartingPrice)
                    {
                        bidder.Write("Bid must be higher than current bid.");
                    }
                    else
                    {
                        AuctionHouseRepo.Instance.HighestBidder = bidder;
                        AuctionHouseRepo.Instance.NextBid = bid;
                    }
                }
            }
            catch (Exception)
            {
                bidder = null;
            }
        }
    }
}
