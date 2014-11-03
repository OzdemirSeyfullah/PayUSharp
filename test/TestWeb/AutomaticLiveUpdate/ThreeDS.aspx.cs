
using System;
using System.Web;
using System.Web.UI;
using System.Text;
using PayU.AutomaticLiveUpdate;
using System.Xml.Serialization;
using System.Xml;

namespace TestWeb
{
    public partial class ThreeDS : System.Web.UI.Page
    {
        public void Page_Load() {
            var response = ALUResponse.FromHttpRequest (Request);
            var sb = new StringBuilder();
            sb.AppendLine ("<ul>");
            sb.AppendFormat("<li><b>{0}:</b> {1}", "RefNo", response.RefNo);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "Alias", response.Alias);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "Status", response.Status);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "ReturnCode", response.ReturnCode);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "ReturnMessage", response.ReturnMessage);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "Date", response.Date);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "Url3DS", response.Url3DS);
            sb.AppendFormat("<li><b>{0}:</b> {1}", "Hash", response.Hash);
            sb.AppendLine ("</ul>");

            ltrOutput.Text = sb.ToString ();
        }
    }
}
