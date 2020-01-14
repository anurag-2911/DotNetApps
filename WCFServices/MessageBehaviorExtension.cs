using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    public class MessageBehaviourExtension : BehaviorExtensionElement, IServiceBehavior
    {

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
           
        }

        public override Type BehaviorType
        {
            get { return typeof(MessageBehaviourExtension); }
        }

        protected override object CreateBehavior()
        {
            return this;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
         System.ServiceModel.ServiceHostBase serviceHostBase,
         System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
         BindingParameterCollection bindingParameters)
        {
           
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
         System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            
            for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
            {
                ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                    {
                        MessageInspector inspector = new MessageInspector();
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription,
          System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }

        
    }
}
