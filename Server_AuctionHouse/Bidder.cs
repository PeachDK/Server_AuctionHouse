using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server_AuctionHouse
{
    public class Bidder
    {
        public string Name { get; set; }
        private EndPoint EndPoint { get; set; }
        private NetworkStream Netstream { get; set; }
        private StreamWriter Writer { get; set; }
        private StreamReader Reader { get; set; }
        public bool Active { get; set; }

        public Bidder(Socket socket)
        {
            this.Netstream = new NetworkStream(socket);
            this.Reader = new StreamReader(Netstream);
            this.Writer = new StreamWriter(Netstream);
            this.Writer.AutoFlush = true;
            this.EndPoint = socket.RemoteEndPoint;
            this.Active = true;
        }

        public string Read()
        {
            return Reader.ReadLine();
        }

        public void Write(string message)
        {
            try
            {
                Writer.WriteLine(message);
            }
            catch (Exception)
            {
                
            }

        }

        public override string ToString()
        {
            return Name + " " + EndPoint;
        }

        public void Disconnect()
        {
            Active = false;
            Writer.Close();
            Reader.Close();
            Netstream.Close();
        }
    }
}
