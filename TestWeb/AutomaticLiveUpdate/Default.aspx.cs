
using System;
using System.Web;
using System.Web.UI;
using PayU;
using PayU.AutomaticLiveUpdate;
using System.Text;

namespace TestWeb
{
    public partial class Default : System.Web.UI.Page
    {
        public virtual void button1Clicked (object sender, EventArgs args)
        {
            Configuration.Instance
                .SetSignatureKey("SECRET_KEY")
                .SetEnvironment("https://secure.payuodeme.com/order/")
                .SetIgnoreSSLCertificate(true); // Don't do this on production.
            
            var parameters = new OrderDetails();
            
            parameters.Merchant = "OPU_TEST";
            parameters.OrderRef = "EXT_" + new Random().Next(100000, 999999).ToString();
            parameters.OrderDate = DateTime.Now;
            parameters.ProductDetails.Add(new ProductDetails
                                       {
                Code = "TCK1",
                Name = "Ticket1",
                Quantity = 1,
                UnitPrice = 1M,
                Information = "Barcelona flight"
            });
            parameters.ProductDetails.Add(new ProductDetails
                                       {
                Code = "TCK2",
                Name = "Ticket2",
                Quantity = 1,
                UnitPrice = 1M,
                Information = "London Flight"
            });
            parameters.PricesCurrency = "TRY";
            parameters.CardDetails = new CardDetails
            {
                CardNumber = textBoxCardNumber.Text,
                ExpiryMonth = textBoxExpiryMonth.Text,
                ExpiryYear = textBoxExpiryYear.Text,
                CVV = textBoxCVV.Text,
                CardOwnerName = textBoxCardHolderName.Text
            };
            parameters.BillingDetails = new BillingDetails
            {
                LastName = "Doe",
                FirstName = "John",
                Email = textBoxEmail.Text,
                PhoneNumber = "1234567890",
                CountryCode = "TR",
                ZipCode = "12345", //optional
                Address = "Billing address", //optional
                Address2 = "Billing address ", //optional
                City = "City", //optional
                State = "State / Dept.", //optional
                Fax = "1234567890" //optional
            };
            parameters.DeliveryDetails = new DeliveryDetails
            {
                LastName = "John", //optional
                FirstName = "Doe", //optional
                Email = "shopper@payu.ro", //optional
                PhoneNumber = "1234567890", //optional
                Company = "Company Name", //optional
                Address = "Delivery Address", //optional
                Address2 = "Delivery Address", //optional
                ZipCode = "12345", //optional
                City = "City", //optional
                State = "State / Dept.", //optional
                CountryCode = "TR" //optional
            };
            parameters.ReturnUrl = "~/AutomaticLiveUpdate/ThreeDS.aspx".ToAbsoluteUrl();
            parameters.ClientIpAddress = Request.UserHostAddress;
            //request.ClientTime = DateTime.UtcNow; // optional
            parameters.SelectedInstallmentNumber = Convert.ToInt32(ddlInstallmentCount.SelectedValue);

            var sb = new StringBuilder();

            try {
                var response = AluRequest.ProcessPayment(parameters);

                if (response.Is3DSResponse) {
                    Response.Redirect (response.Url3DS);
                    Response.End();
                }

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
            }
            catch (PayuException ex) 
            {
                sb.AppendFormat("Exception: {0}", ex);
            }

            ltrOutput.Text = sb.ToString();
        }
    }
}

