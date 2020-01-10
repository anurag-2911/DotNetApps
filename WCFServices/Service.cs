using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace WCFServices
{
    public class Service : IService
    {
        public string HelloWorld(string text)
        {
            return text;
        }
        public string WebFaultMethod()
        {
            throw new WebFaultException(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
