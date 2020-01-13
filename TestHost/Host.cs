using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WCFServices;

namespace TestHost
{
    class Host
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(Service));
           // serviceHost.Description.Behaviors.Add(new CustomServiceBehavior());

            serviceHost.Open();

            Console.WriteLine("service is hosted successfully");
            
            Console.ReadKey();
            
        }

        
    }
    
}
