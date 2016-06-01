

namespace TGP.Host.Contracts
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICalculate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [OperationContract]
        double Add(double x, double y);
    }
}
