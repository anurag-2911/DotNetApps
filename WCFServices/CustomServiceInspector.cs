using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace WCFServices
{
    public class CustomServiceInspector : IDispatchMessageInspector
    {
        #region IDispatchMessageInspector Members
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("IDispatchMessageInspector.AfterReceiveRequest called.");
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine("IDispatchMessageInspector.BeforeSendReply called.");
        }
        #endregion


    }
}