
using System.Text;
using PayU.LiveUpdate;
using PayU;
using PayU.IPN;
using PayU.Core;

namespace TestWeb
{
    public partial class OrderComplete : System.Web.UI.Page
    {
        public void Page_Load() {
            Configuration.Instance
              .SetSignatureKey("4@ET=1()T=%y3S8b(r_]");

            // Verify the signature in the "ctrl" query string parameter
            var verification = LiveUpdateRequest.VerifyControlSignature(Request);
            // Grab the order id.
            var orderId = Request.QueryString["orderid"];

            var sb = new StringBuilder();
            sb.AppendFormat("<p>Thank you for your order. It has been recorded with Reference Number {0}</p>", orderId);
            sb.AppendLine();
            sb.AppendFormat("<p>Control signature verification result: {0}</p>", verification);

            ltrOutput.Text = sb.ToString ();
        }
    }
}

