using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    internal class MyFormatter : IDispatchMessageFormatter
    {
        IDispatchMessageFormatter originalFormatter;
        public MyFormatter(IDispatchMessageFormatter originalFormatter)
        {
            this.originalFormatter = originalFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            if (message.Properties.ContainsKey(MyOperationBypasser.SkipServerMessageProperty))
            {
                Message returnMessage = message.Properties[MyOperationBypasser.SkipServerMessageProperty] as Message;
                OperationContext.Current.IncomingMessageProperties.Add(MyOperationBypasser.SkipServerMessageProperty, returnMessage);
                OperationContext.Current.OutgoingMessageProperties.Add(MyOperationBypasser.SkipServerMessageProperty, returnMessage);
            }
            else
            {
                this.originalFormatter.DeserializeRequest(message, parameters);
            }
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(MyOperationBypasser.SkipServerMessageProperty))
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