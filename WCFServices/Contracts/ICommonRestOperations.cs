using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    [ServiceContract]
    public interface ICommonRestOperations
    {
        [OperationContract]
        [WebInvoke(Method ="GET",UriTemplate ="/CommonRestOperation/getCurrentDate",
            BodyStyle =WebMessageBodyStyle.Wrapped,RequestFormat =WebMessageFormat.Json,ResponseFormat =WebMessageFormat.Json)]
        string getCurrentDate();
    }
}
