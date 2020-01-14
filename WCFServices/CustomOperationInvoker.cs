using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    public class CustomOperationInvoker : IOperationInvoker
    {
        IOperationInvoker originalInvoker;

        public CustomOperationInvoker(IOperationInvoker originalInvoker)
        {
            if (!originalInvoker.IsSynchronous)
            {
                throw new NotSupportedException("This implementation only supports synchronous invokers");
            }

            this.originalInvoker = originalInvoker;
        }

        public object[] AllocateInputs()
        {
            return this.originalInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            if (OperationContext.Current.IncomingMessageProperties.ContainsKey(CustomOperationBypasser.SkipServerMessageProperty))
            {
                outputs = null;
                return OperationContext.Current.IncomingMessageProperties[CustomOperationBypasser.SkipServerMessageProperty]; // message is stored in the context
            }
            else
            {
                return this.originalInvoker.Invoke(instance, inputs, out outputs);
            }
        }


        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            throw new NotSupportedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotSupportedException();
        }

        public bool IsSynchronous
        {
            get { return true; }
        }
    }
}
