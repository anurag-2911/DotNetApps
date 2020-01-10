using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCFServices;

namespace WCFClient
{
    class Client
    {
        private static string baseAddress = "http://" + Environment.MachineName + ":8000/Service";

        static void Main(string[] args)
        {
            ChannelFactory<IService> factory = new ChannelFactory<IService>(GetBinding(), new EndpointAddress(baseAddress));
            IService proxy = factory.CreateChannel();
            Console.WriteLine(proxy.Echo("Hello"));

            Console.WriteLine("And now with the bypass header");
            using (new OperationContextScope((IContextChannel)proxy))
            {
                HttpRequestMessageProperty httpRequestProp = new HttpRequestMessageProperty();
                httpRequestProp.Headers.Add("X-BypassServer", "This message will not reach the service operation");
                OperationContext.Current.OutgoingMessageProperties.Add(
                  HttpRequestMessageProperty.Name,
                  httpRequestProp);
                Console.WriteLine(proxy.Echo("Hello"));
            }

            ((IClientChannel)proxy).Close();
            factory.Close();

            Console.ReadKey();
        }

        private static Binding GetBinding()
        {
            BasicHttpBinding result = new BasicHttpBinding();
            return result;
        }
    }
}
