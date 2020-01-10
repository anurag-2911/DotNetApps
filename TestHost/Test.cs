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
    class Test
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://" + Environment.MachineName + ":8000/Service";
            ServiceHost host = new ServiceHost(typeof(Service), new Uri(baseAddress));
            ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IService), GetBinding(), "");
            endpoint.Behaviors.Add(new MyOperationBypasser());
            foreach (var operation in endpoint.Contract.Operations)
            {
                operation.Behaviors.Add(new MyOperationBypasser());
            }

            host.Open();
            Console.WriteLine("Host opened");

            Console.ReadKey();

            
        }

        private static Binding GetBinding()
        {
            BasicHttpBinding result = new BasicHttpBinding();
            return result;
        }
    }
    
}
