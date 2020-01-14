using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    public class TestRequestInterceptor : IDispatchMessageInspector, IServiceBehavior
    {
        private string _providerName;


        public TestRequestInterceptor(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentException("ProviderName cannot be null or an empty string !", "providerName");
            }
            _providerName = providerName;
            
        }


        public object AfterReceiveRequest(ref Message request, 
            IClientChannel channel, InstanceContext instanceContext)
        {
            object correlationState = null;
            HttpRequestMessageProperty requestMessage = request.Properties["httpRequest"] as HttpRequestMessageProperty;
            if (request == null)
            {
                throw new InvalidOperationException("HttpRequestMessageProperty 'httpRequest' property not found !");
            }
            string authHeader = requestMessage.Headers["MyHeader"];
            if (string.IsNullOrEmpty(authHeader)|| !Authenicate(authHeader))
            {
                WcfErrorResponseData error = new WcfErrorResponseData(HttpStatusCode.Forbidden);
                correlationState = error;
                request = null;
            }

            return correlationState;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            WcfErrorResponseData error = correlationState as WcfErrorResponseData;
            if (error != null)
            {
                HttpResponseMessageProperty responseProperty = new HttpResponseMessageProperty();
                reply.Properties["httpResponse"] = responseProperty;
                responseProperty.StatusCode = error.StatusCode;

                IList<KeyValuePair<string, string>> headers = error.Headers;
                if (headers != null)
                {
                    for (int i = 0; i < headers.Count; i++)
                    {
                        responseProperty.Headers.Add(headers[i].Key, headers[i].Value);
                    }
                }

                if (error.Body != null)
                {
                    // Override the body how?
                }
            }
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    // Is this a good assumption??
                    if (endpointDispatcher.EndpointAddress.Uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                        endpointDispatcher.EndpointAddress.Uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
                    }
                }
            }
        }


        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {   
        }

        private static bool Authenicate(string authHeader)
        {
            return false;
        }

    }


    internal class WcfErrorResponseData
    {
        public WcfErrorResponseData(HttpStatusCode status)
            : this(status, string.Empty, null)
        {
        }
        public WcfErrorResponseData(HttpStatusCode status, string body)
            : this(status, body, null)
        {
        }
        public WcfErrorResponseData(HttpStatusCode status, string body, params KeyValuePair<string, string>[] headers)
        {
            StatusCode = status;
            Body = body;
            Headers = headers;
        }


        public HttpStatusCode StatusCode
        {
            private set;
            get;
        }

        public string Body
        {
            private set;
            get;
        }

        public IList<KeyValuePair<string, string>> Headers
        {
            private set;
            get;
        }
    }

    public class RequestInterceptorBehaviorExtension : BehaviorExtensionElement
    {
        [ConfigurationProperty("providerName", IsRequired = true)]
        public virtual string ProviderName
        {
            get
            {
                return this["providerName"] as string;
            }
            set
            {

                this["providerName"] = value;
            }
        }
        public override Type BehaviorType
        {
            get
            {
                return typeof(TestRequestInterceptor);
            }
        }

        protected override object CreateBehavior()
        {
            return new TestRequestInterceptor(this.ProviderName);
        }
    }
}
