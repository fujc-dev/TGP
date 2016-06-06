using System;
using System.ServiceModel;

namespace Service.Interface
{

    [ServiceContract(Name = "CalculatorService", Namespace = "https://github.com/dane55/TGP.git")]
    public interface ICalculator
    {

        [OperationContract]
        Double Add(Double x, Double y);

        [OperationContract]
        Double Subtract(Double x, Double y);

        [OperationContract]
        Double Multiply(Double x, Double y);

        [OperationContract]
        Double Divide(Double x, Double y);
    }
}
