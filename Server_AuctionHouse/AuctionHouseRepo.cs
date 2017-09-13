using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_AuctionHouse
{
    public class AuctionHouseRepo
    {        
        private static readonly AuctionHouseRepo instance = new AuctionHouseRepo();
        public static AuctionHouseRepo Instance
        {
            get
            {             
                return instance;
            }
        }

        public List<Item> itemList { get; set; }
        public decimal CurrentBid { get; set; }
        public decimal NextBid { get; set; }
        public Bidder HighestBidder { get; set; }
        public Item CurrentItem { get; set; }
        
        private AuctionHouseRepo()
        {
            itemList = new List<Item>();
            itemList.Add(new Item("Hammer", "Old, Heavy, used by Smith", 100));
            itemList.Add(new Item("Generator", "2x200 Volt, In ok condition", 400));
            itemList.Add(new Item("Sofa", "Only used as extra bed", 800));
            itemList.Add(new Item("Lazy Chair", "Is good for relaxing, reading, tv and napping", 350));
            itemList.Add(new Item("Steel Sink", "In pretty condition", 100));
        }
    }
}
