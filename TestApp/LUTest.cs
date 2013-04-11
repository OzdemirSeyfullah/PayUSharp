using System;
using PayU.LiveUpdate;
using PayU;

namespace TestApp
{
    public static class LUTest
    {
        public static void Run()
        {
            Configuration.Instance
                .SetSignatureKey("P5@F8*3!m0+?^9s3&u8(")
                .SetEnvironment("https://secure.payuodeme.com/order/");

            var order = new OrderDetails();
            order.Merchant = "PAYUDEMO";
            order.OrderRef = "6112457";

            order.ProductDetails.Add(new ProductDetails
            {
                Code = "Product code",
                Name = "Product nameĞŞÇÖıİ",
                Quantity = 2,
                VAT = 67M,
                UnitPrice = 20M,
                Information = "Product info",
                PriceType = PriceType.GROSS
            });
            order.ShippingCosts = 47M;
            order.PricesCurrency = "TRY";
            order.PaymentMethod = "";
            order.Discount = 10M;
            order.DestinationCity = "Ankara";
            order.DestinationState = "Ankara";
            order.DestinationCountry = "TR";
            order.TestOrder = true;
            order.InstallmentOptions = "2,3,7,10,12";

            order.BillingDetails = new BillingDetails {
                FirstName = "Mehmet",
                LastName = "Coşkun",
                Email = "mehmet.coskun@payu.com.tr",
                City = "Kağıthane", //Ilce/Semt
                State = "Istanbul", //Sehir
                CountryCode = "TR"
            };

            var request = new LiveUpdateRequest(order);

            Console.WriteLine("{0}", request.RenderPaymentForm("Go to Payment Page"));
        }
    }
}

