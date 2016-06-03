using System;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace Artech.WcfServices.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
               
                host.Open();
                Console.Read();
            }
        }
    }
}
