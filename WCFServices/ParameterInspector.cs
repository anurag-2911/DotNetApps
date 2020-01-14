using System;
using System.ServiceModel.Dispatcher;

namespace WCFServices
{
    internal class ParameterInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            return null;
        }
    }
}