namespace Hosting
{
    using System;
    using Service;
    using System.ServiceModel;
    using Service.Interface;
    using System.ServiceModel.Description;
    using System.ServiceModel.Channels;

    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHost mHost = new ServiceHost("Service.CalculatorServiceImp");
            using (ServiceHost mHost = new ServiceHost(typeof(CalculatorServiceImp)))
            {
                //如何为服务指定AddressHeader
                String headerValue = "Licensed User";
                String headerName = "UserType";
                String headerNameSpace = "http://www.baidu.om";
                AddressHeader addressHeader = AddressHeader.CreateAddressHeader(headerName, headerNameSpace, headerValue);
                //
                EndpointAddress endpointAddress = new EndpointAddress(
                    new Uri("http://127.0.0.1:3721/calcutorservice"),
                    addressHeader);
                //
                ServiceEndpoint serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICalculator)), new WSHttpBinding(), endpointAddress);
                //添加终结点
                //mHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(SecurityMode.None), "http://127.0.0.1:3721/calcutorservice");
                mHost.Description.Endpoints.Add(serviceEndpoint);
                
                //检测是否为当前的ServiceHost服务承载了元数据行为
                if (mHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    //为终结点添加元数据行为（包含事务，安全，模式等）
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://127.0.0.1:3721/calcutorservice/metadata");
                    mHost.Description.Behaviors.Add(behavior);
                }
                //mHost.Opened += (sender, e) => { };
                mHost.Opened += delegate
                {
                    Console.WriteLine("CalculatorService已启动，按任意键终止服务");
                };
                mHost.Open();
                Console.ReadKey();
            };
        }
    }
}
