using Artech.WcfServices.Service.Interface;
using System.ServiceModel;
namespace Artech.WcfServices.Service
{
   // [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }
    }
}
