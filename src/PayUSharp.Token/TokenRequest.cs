using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayU.Core;

namespace PayU.Token
{
  internal class TokenRequest
  {
    public enum MethodType
    {
      TOKEN_NEWSALE,
      TOKEN_GETINFO,
      TOKEN_CANCEL
    }

    internal TokenRequest(TokenService service)
    {
      this.Service = service;
    }

    public TokenService Service { get; private set; }

    public string Merchant { get; set; }

    public string ReferenceNumber { get; set; }

    public string ExternalReference { get; set; }

    public decimal? Amount { get; set; }

    public string Currency { get; set; }

    public string Timestamp { get { return DateTime.UtcNow.ToString("yyyyMMddHHmmss"); } }

    public MethodType Method { get; set; }

    public string CancelReason { get; set; }

    private string GetSignature(NameValueCollection data, string SignatureKey)
    {
      var builder = new StringBuilder();

      foreach (string key in data.AllKeys.OrderBy(k => k))
      {
        var value = data[key];
        builder.Append(Encoding.UTF8.GetByteCount(value));
        builder.Append(value);
      }

      var str = builder.ToString();

      return str.HashWithSignature(Service.SignatureKey);
    }

    private Dictionary<string, object> GetData()
    {
      return new Dictionary<string, object>()
      {
        { "MERCHANT"     , Merchant },
        { "REF_NO"       , ReferenceNumber },
        { "EXTERNAL_REF" , ExternalReference },
        { "AMOUNT"       , Amount },
        { "CURRENCY"     , Currency },
        { "TIMESTAMP"    , Timestamp },
        { "METHOD"       , Method },
        { "CANCEL_REASON", CancelReason }
      };
    }

    private NameValueCollection GetRequestData(string SignatureKey)
    {
      var result = new NameValueCollection();

      foreach (var pair in GetData())
      {
        if (pair.Value == null)
        {
          continue;
        }

        result.Add(pair.Key, string.Format(CultureInfo.InvariantCulture, "{0}", pair.Value));
      }

      result.Add("SIGN", GetSignature(result, SignatureKey));

      return result;
    }

    public TokenResponse SendRequest(string Endpoint, string SignatureKey)
    {
      var webClient = new WebClient();
      var data = GetRequestData(SignatureKey);

      try
      {
        if (Service.IgnoreSSLCertificate)
        {
          ServicePointManager.ServerCertificateValidationCallback = Validator;
        }
        var response = Encoding.UTF8.GetString(webClient.UploadValues(Endpoint, data));
        return ParseResponse(response);
      }
      catch (WebException ex)
      {
        if (ex.Status == WebExceptionStatus.ProtocolError)
        {
          var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
        }
        throw new PayuException(string.Format("Request to url {0} failed with status {1} and response {2}", (object)Endpoint, (object)ex.Status, (object)ex.Response), ex);
      }
      catch (Exception ex)
      {
        throw new PayuException("An exception occured during Token request", ex);
      }
    }

    static TokenResponse ParseResponse(string stringResponse)
    {
      var result = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
      result.History = ParseHistory(stringResponse);
      return result;
    }

    static IDictionary<int, TokenHistory> ParseHistory(string stringResponse)
    {
      var jsonResponse = JObject.Parse(stringResponse);
      var history = jsonResponse["HISTORY"];

      if (history == null)
      {
        // Sometimes there is no HISTORY element in returned data.
        return null;
      }

      switch (history.Type)
      {
        case JTokenType.Array:
          return history.Select((value, index) => new {
            value = JsonConvert.DeserializeObject<TokenHistory>(value.ToString()),
            index = index
          }).ToDictionary(v => v.index, v => v.value);
        case JTokenType.Object:
          return JsonConvert.DeserializeObject<Dictionary<int, TokenHistory>>(history.ToString());
      }

      return null;
    }

    public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
                                 SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

  }
}
