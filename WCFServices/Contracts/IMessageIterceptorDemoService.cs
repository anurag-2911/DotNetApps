using System.ServiceModel;

namespace WCFServices
{
    [ServiceContract(Name ="IService")]
    public interface IMessageIterceptorDemoService
    {
        [OperationContract]
        string HelloWorld(string text);

        [OperationContract]
        string WebFaultMethod();

        [OperationContract]
        string TestMessage();

    }
}
