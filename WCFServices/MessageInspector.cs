using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    public class MessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Message result = null;
            HttpRequestMessageProperty reqProp = null;
            if (request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                reqProp = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            }

            if (reqProp != null)
            {
                //string bypassServer = reqProp.Headers["X-BypassServer"];
                //if (!string.IsNullOrEmpty(bypassServer))
                //{
                request.Properties.Add("SkipServer", "true");
                result = Message.CreateMessage(request.Version,MessageFault.CreateFault(new FaultCode("some Fault"),new FaultReason("some reason")),request.Headers.Action);
                //}
            }

            return result;
        }

        

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            
        }

       
    }
}
