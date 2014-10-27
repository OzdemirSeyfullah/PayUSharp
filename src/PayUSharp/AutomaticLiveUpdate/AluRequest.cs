using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace PayU.AutomaticLiveUpdate
{
    public class AluRequest
    {
        private AluRequest() {}

        public static AluResponse ProcessPayment(OrderDetails parameters)
        {
            if (Configuration.Instance.Environment == null)
                throw new InvalidOperationException("Cannot send request before a Configuration.SetEnvironment() call.");
            if (Configuration.Instance.SignatureKey == null)
                throw new InvalidOperationException("Cannot send request before a Configuration.SetSignatureKey() call.");

            var parameterHandler = new ParameterHandler(parameters);

            parameterHandler.CreateOrderRequestHash();

            var response = SendRequest(parameterHandler);

            //Console.WriteLine("Response: {0}", response);

            return AluResponse.FromString(response);
        }

        public static bool Validator (object sender, X509Certificate certificate, X509Chain chain, 
                                      SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static string SendRequest(ParameterHandler parameterHandler)
        {
            var address = Configuration.Instance.Environment + "alu.php";
            var webClient = new WebClient();
            var data = parameterHandler.GetRequestData();

            try
            {
                if (Configuration.Instance.IgnoreSSLCertificate) {
                    ServicePointManager.ServerCertificateValidationCallback = Validator;
                }
                return Encoding.UTF8.GetString(webClient.UploadValues(address, data));
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                }
                throw new PayuException(string.Format("Request to url {0} failed with status {1} and response {2}", (object)address, (object)ex.Status, (object)ex.Response), ex);
            }
            catch (Exception ex)
            {
                throw new PayuException("An exception occured during ALU request", ex);
            }
        }
    }
}