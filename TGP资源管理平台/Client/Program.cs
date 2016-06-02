using System;
using System.ServiceModel;
using Service.Interface;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            AddressHeader addressHeader = AddressHeader.CreateAddressHeader("Licensed User", "http://www.baidu.om", "UserType");
            //
            EndpointAddress endpointAddress = new EndpointAddress(new Uri("http://127.0.0.1:3721/calcutorservice"), addressHeader);
            //
            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICalculator)), 
                new WSHttpBinding(),
                endpointAddress);

            ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(serviceEndpoint);

            ICalculator calculatorService = channelFactory.CreateChannel();

            using (OperationContextScope contentScope = new OperationContextScope(calculatorService as IContextChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(addressHeader.ToMessageHeader());
                Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, calculatorService.Add(1, 1));
            }
            Console.ReadKey();

        }
    }
}
