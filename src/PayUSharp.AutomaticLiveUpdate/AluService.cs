using System;
using PayU.Core;

namespace PayU.AutomaticLiveUpdate
{
  public class ALUService
  {
    private readonly string DefaultEndpoint = "https://secure.payu.com.tr/order/alu/v3";

    public string SignatureKey { get; private set; }

    public string EndpointUrl { get; private set; }

    public bool IgnoreSSLCertificate { get; private set; }

    public ALUService(string signatureKey, string endpointUrl = null, bool ignoreSSLCertificate = false)
    {
      if (string.IsNullOrEmpty(signatureKey))
        throw new InvalidOperationException("Cannot instantiate with a null or empty SignatureKey.");
      this.SignatureKey = signatureKey;
      this.EndpointUrl = string.IsNullOrEmpty(endpointUrl) ? DefaultEndpoint : endpointUrl;
      this.IgnoreSSLCertificate = ignoreSSLCertificate;
    }

    public ALUResponse ProcessPayment(OrderDetails parameters)
    {
      var parameterHandler = new ParameterHandler(parameters);
      parameterHandler.CreateOrderRequestHash(this.SignatureKey);
      var requestData = parameterHandler.GetRequestData();

      var response = ALURequest.SendRequest(this, requestData);

      //Console.WriteLine("Response: {0}", response);

      return ALUResponse.FromString(response);
    }
  }
}
