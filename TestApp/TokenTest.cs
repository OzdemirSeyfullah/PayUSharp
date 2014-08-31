using System;
using PayU;
using PayU.Token;
using Newtonsoft.Json;
using System.Linq;

namespace TestApp
{
  public static class TokenTest
  {
    public static void Run()
    {
      Configuration.Instance
        .SetSignatureKey("SECRET_KEY")
        .SetEnvironment("https://secure.payuodeme.com/order/");

      var service = new TokenService("OPU_TEST");

      var newSaleResponse = service.NewSale("11930179", 1.15M);

      Console.WriteLine("New Sale Response: {0} - {1}", newSaleResponse.Code, newSaleResponse.Message);

      var infoResponse = service.GetInfo("11930179");

      Console.WriteLine("Token Info Response: {0} - {1}", infoResponse.Code, infoResponse.Message);

      var cancelResponse = service.Cancel("11930179", "not needed anymore");

      Console.WriteLine("Cancel Response: {0} - {1}", cancelResponse.Code, cancelResponse.Message);
    }
  }
}

