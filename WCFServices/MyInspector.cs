using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace WCFServices
{
    public class MyInspector : IDispatchMessageInspector
    {
        ServiceEndpoint endpoint;
        public MyInspector(ServiceEndpoint endpoint)
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
            }

            if (reqProp != null)
            {
                string bypassServer = reqProp.Headers["X-BypassServer"];
                if (!string.IsNullOrEmpty(bypassServer))
                {
                    result = Message.CreateMessage(request.Version, this.FindReplyAction(request.Headers.Action), new OverrideBodyWriter(bypassServer));
                }
            }

            return result;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Message newResult = correlationState as Message;
            if (newResult != null)
            {
                reply = newResult;
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

        class OverrideBodyWriter : BodyWriter
        {
            string bypassServerHeader;
            public OverrideBodyWriter(string bypassServerHeader)
              : base(true)
            {
                this.bypassServerHeader = bypassServerHeader;
            }

            protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
            {
                writer.WriteStartElement("EchoResponse", "http://tempuri.org/");
                writer.WriteStartElement("EchoResult");
                writer.WriteString(this.bypassServerHeader);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            
        }
    }
}