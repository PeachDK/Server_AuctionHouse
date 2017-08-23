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
    class Program
    {
        static int port = 20001;
        static AuctionHandler autionHandler = new AuctionHandler();


        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();

            while (true)
            {
                Socket client = tcpListener.AcceptSocket();
                new Thread(() => On_New_Connection(client)).Start();
            }
        }

        public static void On_New_Connection(Socket client)            
        {
            Bidder bidder = new Bidder();
            bidder.endPoint = client.RemoteEndPoint;

            NetworkStream networkStream = new NetworkStream(client);
            StreamReader streamReader = new StreamReader(networkStream);
            StreamWriter streamWriter = new StreamWriter(networkStream);
            streamWriter.AutoFlush = true;

            streamWriter.WriteLine("Enter Name to join Auction: ");
            bidder.Name = streamReader.ReadLine();
            autionHandler.AuctionStart(client, streamReader, streamWriter, bidder);
            
        }

      



      
    }
}
