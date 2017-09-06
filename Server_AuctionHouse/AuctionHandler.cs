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
        public List<Item> itemList { get; set; }
        public decimal CurrentBid { get; set; }
        public decimal NextBid { get; set; }
        public Bidder HighestBidder { get; set; }
        public AuctionHandler()
        {
            this.itemList = new List<Item>();
            itemList.Add(new Item("Hammer", "Old, Heavy, used by Smith", 100));
            itemList.Add(new Item("Generator", "2x200 Volt, In ok condition", 400));
            itemList.Add(new Item("Sofa", "Only used as extra bed", 800));
            itemList.Add(new Item("Lazy Chair", "Is good for relaxing, reading, tv and napping", 350));
            itemList.Add(new Item("Steel Sink", "In pretty condition", 100));
        }

        public void AuctionStart(Socket client, StreamReader reader, StreamWriter writer, Bidder bidder)
        {
            new Thread(() => GetIncomingBids(reader, writer, bidder)).Start();          

            foreach (var item in itemList)
            {
                CurrentBid = item.StartingPrice;
                writer.WriteLine("Welcome to the Auction, current item is " + item.ToString());
                writer.WriteLine("Current bid is " + CurrentBid);
                writer.WriteLine("Enter your bid.");
                Gavel(reader, writer);
            }          
            
        }

        public void Gavel(StreamReader reader, StreamWriter writer)
        {
            Stopwatch timer = new Stopwatch();
            bool NotSold = true;
            while (NotSold)
            { 
                if(NextBid > CurrentBid)
                {
                    writer.WriteLine("New current Bid " + NextBid  + "  Highest Bidder is " + HighestBidder.Name+ " (" + HighestBidder.endPoint +")");
                    CurrentBid = NextBid;
                    timer.Restart();
                }
                if (timer.ElapsedMilliseconds == 10000)
                {
                    writer.WriteLine("First");
                    Thread.Sleep(1);
                }
                if (timer.ElapsedMilliseconds == 15000)
                {
                    writer.WriteLine("Second");
                    Thread.Sleep(1);
                }
                if (timer.ElapsedMilliseconds == 18000)
                {
                    writer.WriteLine("Third. Sold to " + HighestBidder.Name + " "+ HighestBidder.endPoint+ " For ddk " + CurrentBid);
                    Thread.Sleep(1);
                    NextBid = 0;
                    NotSold = false;
                }
            }
        }

        public void GetIncomingBids(StreamReader reader, StreamWriter writer, Bidder bidder)
        {
            try
            {
                
            decimal bid;
            while (true)
            {
               bool correctformat = decimal.TryParse(reader.ReadLine(), out bid);
                if(correctformat == false)
                {
                    writer.WriteLine("Not a Number!");
                }
                if(bid <= CurrentBid)
                {
                    writer.WriteLine("Bid must be higher than current bid.");
                }
                else
                {
                    HighestBidder = bidder;
                    NextBid = bid;
                }                
            }
            }
            catch (Exception)
            {
                writer.Close();
                reader.Close();                
            }
        }
    }
}
