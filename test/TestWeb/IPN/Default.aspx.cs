
using PayU;
using System.Text;
using PayU.IPN;
using NLog;
using PayU.Core;

namespace TestWeb.IPN
{
    public partial class Default : System.Web.UI.Page
    {
    private static readonly Logger logger = LogManager.GetLogger("IPN");

        public void Page_Load() {

            Configuration.Instance
                .SetSignatureKey("P5@F8*3!m0+?^9s3&u8(");

            foreach (var key in Request.Form.AllKeys) {
              logger.Debug("Key: '{0}' - Value: '{1}'", key, Request.Form[key]);
            }

            var ipn = IPNRequest.FromHttpRequest(Request);

            // Do something with the data in the IPNRequest object.
            Response.ContentType = "text/xml";
            Response.Write(ipn.GenerateResponse());
            Response.End();
        }

    }


}

