using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    public class ServiceFormatter : IDispatchMessageFormatter
    {
        IDispatchMessageFormatter originalFormatter;
        public ServiceFormatter(IDispatchMessageFormatter originalFormatter)
        {
            this.originalFormatter = originalFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            if (message.Properties.ContainsKey(ServiceBypasser.SkipServerMessageProperty))
            {
                Message returnMessage = message.Properties[ServiceBypasser.SkipServerMessageProperty] as Message;
                OperationContext.Current.IncomingMessageProperties.Add(ServiceBypasser.SkipServerMessageProperty, returnMessage);
                OperationContext.Current.OutgoingMessageProperties.Add(ServiceBypasser.SkipServerMessageProperty, returnMessage);
            }
            else
            {
                this.originalFormatter.DeserializeRequest(message, parameters);
            }
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(ServiceBypasser.SkipServerMessageProperty))
            {
                return null;
            }
            else
            {
                return this.originalFormatter.SerializeReply(messageVersion, parameters, result);
            }
        }
    }
}