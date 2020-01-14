using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    public class CustomMessageFormatter : IDispatchMessageFormatter
    {
        IDispatchMessageFormatter originalFormatter;
        public CustomMessageFormatter(IDispatchMessageFormatter originalFormatter)
        {
            this.originalFormatter = originalFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            if (message.Properties.ContainsKey(CustomOperationBypasser.SkipServerMessageProperty))
            {
                Message returnMessage = message.Properties[CustomOperationBypasser.SkipServerMessageProperty] as Message;
                if (!OperationContext.Current.IncomingMessageProperties.ContainsKey(CustomOperationBypasser.SkipServerMessageProperty))
                {
                    OperationContext.Current.IncomingMessageProperties.Add(CustomOperationBypasser.SkipServerMessageProperty, returnMessage);
                }
                
                if (!OperationContext.Current.OutgoingMessageProperties.ContainsKey(CustomOperationBypasser.SkipServerMessageProperty))
                {
                    OperationContext.Current.OutgoingMessageProperties.Add(CustomOperationBypasser.SkipServerMessageProperty, returnMessage);
                }
                
            }
            else
            {
                this.originalFormatter.DeserializeRequest(message, parameters);
            }
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(CustomOperationBypasser.SkipServerMessageProperty))
            {
                Message message = OperationContext.Current.OutgoingMessageProperties[CustomOperationBypasser.SkipServerMessageProperty] as Message;
                return message;
            }
            else
            {
                return this.originalFormatter.SerializeReply(messageVersion, parameters, result);
            }
        }
    }
}