
using PayU;
using System.Text;
using PayU.IPN;

namespace TestWeb.IPN
{
    public partial class Default : System.Web.UI.Page
    {
        public void Page_Load() {
            Configuration.Instance
                .SetSignatureKey("P5@F8*3!m0+?^9s3&u8(");
            
            var ipn = IPNRequest.FromHttpRequest(Request);

            // Do something with the data in the IPNRequest object.
            Response.ContentType = "text/xml";
            Response.Write(ipn.GenerateResponse());
            Response.End();
        }

    }


}

