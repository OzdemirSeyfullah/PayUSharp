using System;
using System.Text;
using PayU.Core;

namespace PayU.LiveUpdate
{
  public class LiveUpdateService
  {
    private readonly string DefaultEndpoint = "https://secure.payuodeme.com/order/lu.php";

    public string SignatureKey { get; private set; }
    public string EndpointUrl { get; private set; }

    public LiveUpdateService(string signatureKey, string endpointUrl = null)
    {
      if (string.IsNullOrEmpty(signatureKey))
        throw new InvalidOperationException("Cannot instantiate with a null or empty SignatureKey.");
      this.SignatureKey = signatureKey;
      this.EndpointUrl = string.IsNullOrEmpty(endpointUrl) ? DefaultEndpoint : endpointUrl;
    }

    public string RenderPaymentForm(OrderDetails order, string buttonName)
    {
      return RenderPaymentForm(order, buttonName, "payForm");
    }

    public string RenderPaymentForm(OrderDetails order, string buttonName, string formId)
    {
      var sb = new StringBuilder();

      sb.AppendFormat(@"<form action=""{0}"" method=""POST"" id=""{1}"" name=""{2}"">", EndpointUrl, formId, formId);
      sb.AppendLine();
      sb.Append(RenderPaymentInputs(order));
      sb.AppendFormat(@"<input type=""submit"" value=""{0}"">", buttonName);
      sb.AppendLine();
      sb.AppendLine("</form>");

      return sb.ToString();
    }

    public string RenderPaymentInputs(OrderDetails order) {
      var parameterHandler = new ParameterHandler(order, false);
      parameterHandler.CreateOrderRequestHash(this.SignatureKey);
      var requestData = parameterHandler.GetRequestData();

      var sb = new StringBuilder();

      foreach (var key in requestData.AllKeys) {
        sb.AppendFormat(@"<input type=""hidden"" name=""{0}"" value=""{1}"">", key, requestData[key]);
        sb.AppendLine();
      }

      return sb.ToString();
    }

    public bool VerifyControlSignature (System.Web.HttpRequest request)
    {
      var ctrl = request.QueryString["ctrl"];

      if (string.IsNullOrEmpty(ctrl)) {
        return false;
      }

      var url = request.Url.ToString().Replace("&ctrl=" + ctrl, "").Replace("?ctrl=" + ctrl, "");

      var hashString = url.Length.ToString() + url;
      var hash = hashString.HashWithSignature(this.SignatureKey);

      return hash == ctrl.ToLowerInvariant();
    }
  }
}
