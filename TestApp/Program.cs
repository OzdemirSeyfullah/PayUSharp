using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayU;
using PayU.LiveUpdate;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
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
                FirstName = "Ufuk",
                LastName = "Coşkun",
                Email = "mehmet.coskun@payu.com.tr",
                City = "Kağıthane", //Ilce/Semt
                State = "Istanbul", //Sehir
                CountryCode = "TR"
            };

            var request = new LiveUpdateRequest(order);

            Console.WriteLine("{0}", request.RenderPaymentForm("Go to Payment Page"));
//            var parameters = new OrderDetails();
//
//            parameters.Merchant = "OPU_TEST";
//            parameters.OrderRef = "EXT_" + new Random().Next(100000, 999999).ToString();
//            parameters.OrderDate = DateTime.Now;
//            parameters.ProductDetails.Add(new ProductDetails
//            {
//                Code = "SPTHAR031092",
//                Name = "HARDLINE SOY PRO (90SOYAPROTEINI)",
//                Quantity = 1,
//                UnitPrice = 1M
//            });
//			parameters.ProductDetails.Add(new ProductDetails
//			                           {
//				Code = "SPTHAR031093",
//				Name = "HARDLINE SOY",
//				Quantity = 1,
//				UnitPrice = 2M
//			});
//			parameters.PricesCurrency = "TRY";
//            parameters.CardDetails = new CardDetails
//                {
//                    CardNumber = "5289391262083011",
//                    ExpiryMonth = "06",
//                    ExpiryYear = "2015",
//                    CVV = "099",
//                    CardOwnerName = "Ufuk Kayserilioglu"
//                };
//            parameters.BillingDetails = new BillingDetails
//                {
//                    LastName = "Kayserilioglu",
//                    FirstName = "Ufuk",
//                    Email = "ufuk@paralaus.com",
//                    PhoneNumber = "00905326644030",
//                    CountryCode = "TR"
//                };
//            parameters.ReturnUrl = "http://178.18.33.6/direct.php";
//            parameters.ClientIpAddress = "10.10.10.5";
//            parameters.ClientTime = DateTime.UtcNow;
//            parameters.SelectedInstallmentNumber = 1;
//
//            var response = AluRequest.ProcessPayment(parameters);
//
//            Console.WriteLine("Successful: {0}", response.IsSuccess);
//            Console.WriteLine("Response: {0}-{1}-{2}-{3}", response.RefNo, response.Status, response.ReturnCode, response.ReturnMessage);

            Console.ReadKey();


            // 8PAYUDEMO76112457192013-04-07 14:04:4918Product nameĞŞÇÖıİ12Product code12Product info220122672473TRY000005GROSS0112,3,7,10,12
            // 8PAYUDEMO76112457192013-04-07 14:04:4924Product nameĞŞÇÖıİ12Product code12Product info220122672473TRY000005GROSS0112,3,7,10,12
            // 8PAYUDEMO76112457192013-04-07 14:04:4924Product nameĞŞÇÖıİ12Product code12Product info220122672473TRY000005GROSS112,3,7,10,12
        }
    }
}
