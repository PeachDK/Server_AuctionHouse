using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_AuctionHouse
{
   public class Shared_resource_repo
    {
        private static readonly Shared_resource_repo staticinstance = new Shared_resource_repo();
        public static Shared_resource_repo StaticInstanceSharedResource
        {
            get
            {
                return staticinstance;
            }
        }

        private Shared_resource_repo() { }      
        static Shared_resource_repo() { }
       






    }
}
