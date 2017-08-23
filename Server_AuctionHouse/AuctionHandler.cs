using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server_AuctionHouse
{
   public class AuctionHandler
    {
        static Item Item = new Item("Hammer", "Old, Heavy, used by Smith", 300);
        public decimal CurrentBid { get; set; }
        public decimal NextBid { get; set; }
        

        public AuctionHandler()
        {
            


        }

        public void AuctionStart(Socket client, StreamReader reader, StreamWriter writer, Bidder bidder)
        {
            new Thread(() => GetIncomingBids(reader, writer)).Start();
            CurrentBid = Item.StartingPrice;
            writer.WriteLine("Welcome to the Auction, current item is " + Item.ToString());
            writer.WriteLine("Current bid is " + CurrentBid);
            writer.WriteLine("Enter your bid.");           
            Gavel(client, reader, writer, bidder);
            
        }

        public void Gavel(Socket client, StreamReader reader, StreamWriter writer, Bidder bidder)
        {

            bool NotSold = true;
            while (NotSold)
            {
                if(NextBid > CurrentBid)
                {
                    writer.WriteLine("New current Bid " + NextBid  + "Highest Bidder is " + bidder.Name+ " (" + bidder.endPoint +")");
                    CurrentBid = NextBid;
                }
                
                

            }




        }

        public void GetIncomingBids(StreamReader reader, StreamWriter writer)
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
                    NextBid = bid;
                }
                
            }





        }


    }
}
