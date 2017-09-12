using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Server_AuctionHouse
{
    public class Server
    {
        private static Server instance;        
        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();
                }

                return instance;
            }
        }

        private TcpListener tcpListener { get; set; }        
        public bool Running { get; set; }
        private List<Bidder> bidders { get; set; }
        private AuctionHandler handler { get; set; }

        private Server()
        {
            tcpListener = new TcpListener(IPAddress.Any, 20001);
            tcpListener.Start();
            Running = true;
            bidders = new List<Bidder>();
            handler = new AuctionHandler();
            new Thread(() => AuctionHouseRepo.Instance.itemList.ForEach(item => handler.Gavel(item))).Start();
        }

        public void On_New_Connection(Socket socket)
        {
            Bidder bidder = new Bidder(socket);
            bidder.Write("Enter Name to join Auction: ");
            bidder.Name = bidder.Read();
            bidders.Add(bidder);
            handler.AuctionStart(bidder);
            
        }

        public Socket TryAcceptClient()
        {
            return tcpListener.AcceptSocket();
        }

        public void Broadcast(string message)
        {
            foreach (var bidder in bidders)
            {
                try
                {
                    if (bidder.Active)
                    {
                        bidder.Write(message);
                    }                    
                }
                catch (Exception)
                {
                    Console.WriteLine($"{bidder.ToString()} Disconnected");
                    bidder.Disconnect();                  
                                        
                }
            }
            
        }

    }
}
