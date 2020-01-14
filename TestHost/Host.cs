using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFServices;

namespace TestHost
{
    class Host
    {
        static void Main(string[] args)
        {
            HostWCFSoapServices();

            HostWCFRestServices();

           
            Console.ReadKey();

        }

        private static void HostWCFRestServices()
        {
            try
            {
                //rest wcf services host
                ServiceHost restServiceHost = new ServiceHost(typeof(CommonRestOperations));
                restServiceHost.Open();
                Console.WriteLine("rest services hosted successfully");
            }
            catch (Exception exception)
            {
                Console.WriteLine("exception in hosting rest services  " +exception.ToString());
            }
        }

        private static void HostWCFSoapServices()
        {
            try
            {
                ServiceHost serviceHost = new ServiceHost(typeof(MessageInterceptorDemoService));

                ServiceEndpoint endPoint = serviceHost.Description.Endpoints.Find(typeof(IMessageIterceptorDemoService));

                endPoint.EndpointBehaviors.Add(new CustomOperationBypasser());

                foreach (var operation in endPoint.Contract.Operations)
                {
                    operation.Behaviors.Add(new CustomOperationBypasser());
                }
                //soap based wcf services host
                serviceHost.Open();
                Console.WriteLine("wcf soap based services hosted successfully");
            }
            catch (Exception exception)
            {
                Console.WriteLine("exception in hosting wcf soap services  " +exception.ToString());
            }
        }

    }
    
}
