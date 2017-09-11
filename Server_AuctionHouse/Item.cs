using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_AuctionHouse
{
   public class Item
    {
        public string Name { get; set; }
        public string Desciption { get; set; }      
        public decimal StartingPrice { get; set; }
        public decimal AuctionEndPrice { get; set; }
        public bool Sold { get; set; }
        

        public Item(string name, string desciption, decimal startingPrice)
        {
            this.Name = name;
            this.Desciption = desciption;
            this.StartingPrice = startingPrice;
            this.Sold = false;
        }

        public override string ToString()
        {
            return "Name = " + Name + "  Item Desciption = " + Desciption;
        }







    }
}
