using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace WCFServices
{
    public class CustomMessageInspector : IDispatchMessageInspector
    {
        ServiceEndpoint endpoint;
        public CustomMessageInspector(ServiceEndpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Message result = null;
            HttpRequestMessageProperty reqProp = null;
            if (request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                reqProp = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                if (reqProp != null)
                {
                    // just an example to show that HelloWorld Method will not reach to the end point and 
                    if (request.Headers.Action.Contains("Hello")) 
                    {
                        result = Message.CreateMessage(request.Version, MessageFault.CreateFault(new FaultCode("faultcode1"), 
                            new FaultReason("faultreason1")), 
                            this.FindReplyAction(request.Headers.Action));
                        request.Properties.Add("SkipServer", result);
                    }

                }
            }
            
            return result;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Message updatedReply = correlationState as Message;
            if (updatedReply != null)
            {
                reply = updatedReply;
            }
        }

        private string FindReplyAction(string requestAction)
        {
            foreach (var operation in this.endpoint.Contract.Operations)
            {
                if (operation.Messages[0].Action == requestAction)
                {
                    return operation.Messages[1].Action;
                }
            }

            return null;
        }

        
    }
}