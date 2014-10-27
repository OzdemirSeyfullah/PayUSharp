using System;
using PayU;
using System.Collections.Specialized;
using PayU.IPN;

namespace TestApp
{
    public static class IPNTest
    {
        public static void Run()
        {
            Configuration.Instance
                .SetSignatureKey("P5@F8*3!m0+?^9s3&u8(")
                .SetEnvironment("https://secure.payuodeme.com/order/");
            
            /* IPN */
            var form = new NameValueCollection(){
                {"SALEDATE", "2012-04-26 12:22:09"},
                {"REFNO", "1000037"},
                {"ORDERNO", "13"},
                {"ORDERSTATUS", "PAYMENT_RECEIVED"},
                {"PAYMETHOD", "CCVISAMC"},
                {"FIRSTNAME", "Test"},
                {"LASTNAME", "PayU"},
                {"ADDRESS1", "Some Street 21"},
                {"CITY", "İstanbul"},
                {"STATE", "İstanbul"},
                {"ZIPCODE", "90210"},
                {"COUNTRY", "Türkiye"},
                {"PHONE", "0268/121212"},
                {"CUSTOMEREMAIL", "test@payu.com"},
                {"FIRSTNAME_D", "Test"},
                {"LASTNAME_D", "PayU"},
                {"ADDRESS1_D", "Some Street 21"},
                {"CITY_D", "Ankara"},
                {"STATE_D", "Ankara"},
                {"ZIPCODE_D", "90210"},
                {"COUNTRY_D", "Türkiye"},
                {"PHONE_D", "0268/121212"},
                {"IPADDRESS", "node11"},
                {"CURRENCY", "TRY"},
                {"IPN_PID[]", "1"},
                {"IPN_PNAME[]", "Apple MacBook Air 13 inç"},
                {"IPN_PCODE[]", "AMBA13I"},
                {"IPN_QTY[]", "1"},
                {"IPN_PRICE[]", "50000.00"},
                {"IPN_VAT[]", "9500.00"},
                {"IPN_DISCOUNT[]", "0.00"},
                {"IPN_TOTAL[]", "59500.00"},
                {"IPN_TOTALGENERAL", "60095.00"},
                {"IPN_SHIPPING", "595.00"},
                {"IPN_COMMISSION", "0.00"},
                {"IPN_DATE", "2012042612343"},
                {"IPN_PAID_AMOUNT", "1223.29"},
                {"IPN_INSTALLMENTS_PROGRAM", "BONUS"},
                {"IPN_INSTALLMENTS_NUMBER", "12"},
                {"IPN_INSTALLMENTS_PROFIT", "3.26"},
                {"IPN_CC_TOKEN", "6121191"},
                {"IPN_CC_MASK", "xxxx-xxxx-xxxx-1111"},
                {"IPN_CC_EXP_DATE", "2012-07-18"}
            };
            
            var ipn = IPNRequest.FromNameValueCollection(form);
            
            Console.WriteLine("Response is: {0}", ipn.GenerateResponse());
        }
    }
}

