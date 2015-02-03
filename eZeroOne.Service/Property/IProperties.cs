using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace eZeroOne.Service.Property
{
   
    public interface IProperties
    {
        void GetProperties();
    }

    //implementations
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode =AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Properties : IProperties
    {
        [WebGet(UriTemplate = "")] 
        public void GetProperties()
        {
            
        }
    }
}
