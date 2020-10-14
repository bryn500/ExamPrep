using System.ServiceModel;

namespace WCF_Pub
{
    [ServiceContract]
    public interface ITestWcfService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        bool NotAContractmethod();
    }    
}
