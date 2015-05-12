using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using PayU.Core;
using System.Collections.Specialized;

namespace PayU.AutomaticLiveUpdate
{
  internal class ALURequest
  {
    private ALURequest()
    {
    }

    public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
                                      SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

    public static string SendRequest(ALUService service, NameValueCollection requestData)
    {
      var webClient = new WebClient();

      try
      {
        if (service.IgnoreSSLCertificate)
        {
          ServicePointManager.ServerCertificateValidationCallback = Validator;
        }
        return Encoding.UTF8.GetString(webClient.UploadValues(service.EndpointUrl, requestData));
      }
      catch (WebException ex)
      {
        if (ex.Status == WebExceptionStatus.ProtocolError)
        {
          var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
        }
        throw new PayuException(string.Format("Request to url {0} failed with status {1} and response {2}", (object)service.EndpointUrl, (object)ex.Status, (object)ex.Response), ex);
      }
      catch (Exception ex)
      {
        throw new PayuException("An exception occured during ALU request", ex);
      }
    }
  }
}
