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



            try
            {
                Uri uri = new Uri("http://localhost:8899/calculatorservice");
                //创建SOAP消息报头
                AddressHeader addressHeader = AddressHeader.CreateAddressHeader("UserType", "http://www.baidu.com", "Licensed User");
                //创建通信唯一地址
                EndpointAddress endpointAddress = new EndpointAddress(uri, addressHeader);
                //创建终结点对象
                ServiceEndpoint serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICalculator)),
                    new WSHttpBinding(),
                    endpointAddress);

                ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(serviceEndpoint);

                ICalculator calculatorService = channelFactory.CreateChannel();

                using (OperationContextScope contentScope = new OperationContextScope(calculatorService as IContextChannel))
                {
                    //OperationContext.Current.OutgoingMessageHeaders.Add(addressHeader.ToMessageHeader());
                    Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, calculatorService.Add(1, 1));
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally {

                Console.ReadKey();
            }
        }
    }
}
