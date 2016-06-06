using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IISClient
{
    [ServiceContract]
    public interface IWebInvokieService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/HelloWorld", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]    
        void DoWork();
    }
}
