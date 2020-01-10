using System.ServiceModel;

namespace WCFServices
{
    [ServiceContract(Name ="IService")]
    public interface IService
    {
        [OperationContract]
        string HelloWorld(string text);

        [OperationContract]
        string WebFaultMethod();

    }
}
