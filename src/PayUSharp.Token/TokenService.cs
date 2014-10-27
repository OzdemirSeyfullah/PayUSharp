using System;
using PayU.Token;

namespace PayU.Token
{
  public class TokenService
  {
    private readonly string DefaultEndpoint = "https://secure.payu.com.tr/order/tokens/";

    public string Merchant { get; private set; }
    public string SignatureKey { get; private set; }
    public string EndpointUrl { get; private set; }

    public TokenService(string Merchant, string SignatureKey = null, string EndpointUrl = null)
    {
      this.Merchant = Merchant;
      this.SignatureKey = SignatureKey ?? Configuration.Instance.SignatureKey;
      this.EndpointUrl = EndpointUrl ?? DefaultEndpoint;
    }

    public TokenResponse NewSale(string Token, string OrderRef, decimal Amount, string Currency = null) {
      var request = new TokenRequest()
        {
          Method = PayU.Token.TokenRequest.MethodType.TOKEN_NEWSALE,
          Merchant = Merchant,
          ReferenceNumber = Token,
          ExternalReference = OrderRef,
          Amount = Amount,
          Currency = Currency ?? "TRY"
        };

      return request.SendRequest(EndpointUrl, SignatureKey);
    }

    public TokenResponse GetInfo(string Token) {
      var request = new TokenRequest()
        {
          Method = PayU.Token.TokenRequest.MethodType.TOKEN_GETINFO,
          Merchant = Merchant,
          ReferenceNumber = Token,
        };

      return request.SendRequest(EndpointUrl, SignatureKey);
    }

    public TokenResponse Cancel(string Token, string Reason = null) {
      var request = new TokenRequest()
        {
          Method = PayU.Token.TokenRequest.MethodType.TOKEN_CANCEL,
          Merchant = Merchant,
          ReferenceNumber = Token,
          CancelReason = Reason
        };

      return request.SendRequest(EndpointUrl, SignatureKey);
    }
  }
}

