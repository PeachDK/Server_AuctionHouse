using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Server_AuctionHouse
{
    public class Program
    {
       static AuctionHandler handler = new AuctionHandler();
        
        static void Main(string[] args)
        {                        
            
            while (Server.Instance.Running)
            {
                Socket client = Server.Instance.TryAcceptClient();
                new Thread(() => Server.Instance.On_New_Connection(client)).Start();             

            }
        }
    }
}
