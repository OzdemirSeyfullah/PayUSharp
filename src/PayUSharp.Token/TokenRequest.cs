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

    internal TokenRequest() {}

    public string Merchant { get; set; }
    public string ReferenceNumber { get; set; }
    public string ExternalReference { get; set; }
    public decimal? Amount { get; set; }
    public string Currency { get; set; }
    public string Timestamp { get { return DateTime.UtcNow.ToString("yyyyMMddHHmmss"); } }
    public MethodType Method { get; set; }
    public string CancelReason { get; set; }

    private string GetSignature(NameValueCollection data, string SignatureKey) {
      var builder = new StringBuilder();

      foreach (string key in data.AllKeys.OrderBy(k => k))
      {
        var value = data[key];
        builder.Append(Encoding.UTF8.GetByteCount(value));
        builder.Append(value);
      }

      var str = builder.ToString();

      return str.HashWithSignature(SignatureKey);
    }

    private Dictionary<string, object> GetData() {
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

    private NameValueCollection GetRequestData(string SignatureKey) {
      var result = new NameValueCollection();

      foreach (var pair in GetData())
      {
        if (pair.Value == null)
        {
          continue;
        }

        result.Add(pair.Key, string.Format (CultureInfo.InvariantCulture, "{0}", pair.Value));
      }

      result.Add("SIGN", GetSignature(result, SignatureKey));

      return result;
    }

    public TokenResponse SendRequest(string Endpoint, string SignatureKey) {
      var webClient = new WebClient();
      var data = GetRequestData(SignatureKey);
        
      try
      {
        if (Configuration.Instance.IgnoreSSLCertificate) {
          ServicePointManager.ServerCertificateValidationCallback = Validator;
        }
//        Console.WriteLine("Posting data: {0}", string.Join(", ", data.AllKeys.Select(key => key + ": " + data[key]).ToArray()));
        var response = Encoding.UTF8.GetString(webClient.UploadValues(Endpoint, data));
//        Console.WriteLine("Response is: {0}", response);
        return JsonConvert.DeserializeObject<TokenResponse>(response);
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
        throw new PayuException("An exception occured during ALU request", ex);
      }
    }

    public static bool Validator (object sender, X509Certificate certificate, X509Chain chain, 
      SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

  }
}

