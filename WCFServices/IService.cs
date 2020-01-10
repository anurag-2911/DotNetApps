using System.ServiceModel;

namespace WCFServices
{
    [ServiceContract(Name ="IService")]
    public interface IService
    {
        [OperationContract]
        string Echo(string text);
    }
}
