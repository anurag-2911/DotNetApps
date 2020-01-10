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
        
        static void Main(string[] args)
        {
            try
            {
                TestServiceClient.ServiceClient sc = new TestServiceClient.ServiceClient();
                string result = sc.HelloWorld("hello");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
            Console.ReadKey();
        }

        private static Binding GetBinding()
        {
            BasicHttpBinding result = new BasicHttpBinding();
            return result;
        }
    }
}
