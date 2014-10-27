using System;
using PayU.AutomaticLiveUpdate;
using PayU;
using PayU.Core;

namespace TestApp
{
    public static class ALUTest
    {
        public static void Run()
        {
            var order = new OrderDetails();

            order.Merchant = "TOKENTES";

            order.TokenEnable = true;
            order.TokenType = PayU.Core.Base.TokenType.PAY_ON_TIME;

            order.OrderRef = "EXT_" + new Random().Next(100000, 999999).ToString();
            order.OrderDate = DateTime.UtcNow;
            order.ProductDetails.Add(new ProductDetails
            {
                Code = "SPTHAR031092",
                Name = "HARDLINE SOY PRO (90SOYAPROTEINI)",
                Quantity = 1,
                UnitPrice = 1M
            });

            order.ProductDetails.Add(new ProductDetails
            {
                Code = "SPTHAR031093",
                Name = "HARDLINE SOY",
                Quantity = 1,
                UnitPrice = 1M
            });
            order.PricesCurrency = "TRY";

            order.CardDetails = new CardDetails
            {
                CardNumber = "4242424242424242",
                ExpiryMonth = "12",
                ExpiryYear = "2015",
                CVV = "000",
                CardOwnerName = "Mehmet Coşkun"
            };
            order.BillingDetails = new BillingDetails
            {
                FirstName = "Mehmet",
                LastName = "Coşkun",
                Email = "mehmet.coskun@payu.com.tr",
                PhoneNumber = "2122223344",
                City = "Kağıthane", //Ilce/Semt
                State = "Istanbul", //Sehir
                CountryCode = "TR"
            };
            order.ReturnUrl = "http://178.18.33.6/direct.php";
            order.ClientIpAddress = "10.10.10.5";
            order.ClientTime = DateTime.UtcNow;
            order.SelectedInstallmentNumber = 1;

            var service = new AluService("4@ET=1()T=%y3S8b(r_]", "https://secure.payuodeme.com/order/alu.php");
            var response = service.ProcessPayment(order);

            Console.WriteLine("Successful: {0}", response.IsSuccess);
            Console.WriteLine("Response: {0}-{1}-{2}-{3}", response.RefNo, response.Status, response.ReturnCode, response.ReturnMessage);
        }
    }
}

