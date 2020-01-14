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
            ServiceHost serviceHost = new ServiceHost(typeof(MessageInterceptorDemoService));
            // serviceHost.Description.Behaviors.Add(new CustomServiceBehavior());
            ServiceEndpoint endPoint=serviceHost.Description.Endpoints.Find(typeof(WCFServices.IMessageIterceptorDemoService));
            endPoint.EndpointBehaviors.Add(new CustomOperationBypasser());

            foreach (var operation in endPoint.Contract.Operations)
            {
                operation.Behaviors.Add(new CustomOperationBypasser());
            }

            serviceHost.Open();

            Console.WriteLine("service is hosted successfully");
            
            Console.ReadKey();
            
        }

        
    }
    
}
