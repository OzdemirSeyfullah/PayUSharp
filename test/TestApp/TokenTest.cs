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
        .SetSignatureKey("4@ET=1()T=%y3S8b(r_]")
        .SetEnvironment("https://secure.payuodeme.com/order/");

      var service = new TokenService("TOKENTES");

      var token = "3257913";
      var orderRef = "EXT_" + new Random().Next(100000, 999999).ToString();

      var newSaleResponse = service.NewSale(token, orderRef, 1.15M);

      Console.WriteLine("New Sale Response: {0} - {1}", newSaleResponse.Code, newSaleResponse.Message);

      var infoResponse = service.GetInfo(token);

      Console.WriteLine("Token Info Response: {0} - {1}", infoResponse.Code, infoResponse.Message);

      Console.WriteLine("Token: '{0}' - Status: '{1}'", infoResponse.Token, infoResponse.TokenStatus);

      foreach (var entry in infoResponse.History)
      {
        Console.WriteLine("Token History: '{0}' - Ref No: '{1}' - Amount: '{2}'", entry.Key, entry.Value.ReferenceNumber, entry.Value.Amount);
      }

//      var cancelResponse = service.Cancel(token, "not needed anymore");

//      Console.WriteLine("Cancel Response: {0} - {1}", cancelResponse.Code, cancelResponse.Message);
    }
  }
}

