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
            
            using (ServiceHost mHost = new ServiceHost(typeof(CalculatorServiceImp)))
            {
                Uri uri = new Uri("http://localhost:8899/calculatorservice");

                /*
                 * 创建AddressHeader是 HeaderName不能包含特殊字符，
                 * 这个HeaderName在配置文件中其实是一个XML的节点名称
                 */
                AddressHeader addressHeader = AddressHeader.CreateAddressHeader("UserType", "http://www.baidu.com", "Licensed User");
                EndpointAddress endpointAddress = new EndpointAddress(uri, addressHeader);
                //
                ServiceEndpoint serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICalculator)),
                    new WSHttpBinding(),
                    endpointAddress);
                //添加终结点
                //mHost.Description.Endpoints.Add(serviceEndpoint);
                mHost.AddServiceEndpoint(serviceEndpoint);

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
