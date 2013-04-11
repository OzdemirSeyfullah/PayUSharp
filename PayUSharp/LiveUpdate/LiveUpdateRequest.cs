using System;
using System.Text;

namespace PayU.LiveUpdate
{
    public class LiveUpdateRequest
    {
        public OrderDetails Order { get; private set; }
        public string Address { get; private set; }
        private ParameterHandler ParameterHandler { get; set; }

        public LiveUpdateRequest(OrderDetails order) {
            if (Configuration.Instance.Environment == null)
                throw new InvalidOperationException("Cannot send request before a Configuration.SetEnvironment() call.");
            if (Configuration.Instance.SignatureKey == null)
                throw new InvalidOperationException("Cannot send request before a Configuration.SetSignatureKey() call.");

            Order = order;
            ParameterHandler = new ParameterHandler(order, false);
            Address = Configuration.Instance.Environment + "lu.php";
            ParameterHandler.CreateOrderRequestHash();
        }

        public string RenderPaymentForm(string buttonName)
        {
            return RenderPaymentForm(buttonName, "payForm");
        }

        public string RenderPaymentForm(string buttonName, string formId)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(@"<form action=""{0}"" method=""POST"" id=""{1}"" name=""{2}""/>", Address, formId, formId);
            sb.AppendLine();
            sb.Append(RenderPaymentInputs());
            sb.AppendFormat(@"<input type=""submit"" value=""{0}"">", buttonName);
            sb.AppendLine();
            sb.AppendLine("</form>");

            return sb.ToString();
        }

        public string RenderPaymentInputs() {
            var requestData = ParameterHandler.GetRequestData();

            var sb = new StringBuilder();

            foreach (var key in requestData.AllKeys) {
                sb.AppendFormat(@"<input type=""hidden"" name=""{0}"" value=""{1}"">", key, requestData[key]);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static bool VerifyControlSignature (System.Web.HttpRequest request)
        {
            var ctrl = request.QueryString["ctrl"];

            if (string.IsNullOrEmpty(ctrl)) {
                return false;
            }

            var url = request.Url.ToString().Replace("&ctrl=" + ctrl, "").Replace("?ctrl=" + ctrl, "");

            var hashString = url.Length.ToString() + url;
            var hash = hashString.HashWithSignature(Configuration.Instance.SignatureKey);

            return hash == ctrl.ToLowerInvariant();
        }
    }
}

