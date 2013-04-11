using System;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Globalization;

namespace PayU.IPN
{
    [XmlRoot(ROOT_ELEMENT_NAME)]
    public class IPNRequest 
    {
        private const string ROOT_ELEMENT_NAME = "EPAYMENT";
        private const string PRODUCTS_ELEMENT_NAME = "PRODUCTS";
        private const string PRODUCT_ELEMENT_NAME = "PRODUCT";

        /* Products */
        [XmlArray(PRODUCTS_ELEMENT_NAME)]
        [XmlArrayItem(PRODUCT_ELEMENT_NAME, typeof(IPNProduct))]
        public IPNProduct[] Products { get; protected set;}

        [XmlElement("SALEDATE")]
        public string SaleDateAsString { get; protected set; }

        [XmlIgnore]
        public DateTime SaleDate {
            get { return DateTime.ParseExact(SaleDateAsString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
        }

        [XmlElement("PAYMENTDATE")]
        public string PaymentDateAsString { get; protected set; }

        [XmlIgnore]
        public DateTime PaymentDate {
            get { return DateTime.ParseExact(PaymentDateAsString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
        }

        [XmlElement("COMPLETE_DATE")]
        public string CompleteDateAsString { get; protected set; }

        [XmlIgnore]
        public DateTime CompleteDate {
            get { return DateTime.ParseExact(CompleteDateAsString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
        }

        [XmlElement("REFNO")]
        public string ReferenceNumber { get; protected set; }

        [XmlElement("REFNOEXT")]
        public string SellerReferenceNumber { get; protected set; }

        [XmlElement("ORDERNO")]
        public string OrderNumber { get; protected set; }

        [XmlElement("ORDERSTATUS")]
        public IPNOrderStatus OrderStatus { get; protected set; }

        [XmlElement("PAYMETHOD")]
        public string PaymentMethod { get; protected set; }
        
        [XmlElement("PAYMETHOD_CODE")]
        public string PaymentMethodCode { get; protected set; }
        
        [XmlElement("IPN_PAID_AMOUNT")]
        public decimal TotalPaidAmount { get; protected set; }
        
        [XmlElement("IPN_INSTALLMENTS_PROGRAM")]
        public string InstallmentProgramName { get; protected set; }
        
        [XmlElement("IPN_INSTALLMENTS_NUMBER")]
        public int InstallmentNumber { get; protected set; }
        
        [XmlElement("IPN_INSTALLMENTS_PROFIT")]
        public string InstallmentProfit { get; protected set; }
        
        [XmlElement("FIRSTNAME")]
        public string FirstName { get; protected set; }
        
        [XmlElement("LASTNAME")]
        public string LastName { get; protected set; }
        
        [XmlElement("IDENTITY_NO")]
        public string IdentityNumber { get; protected set; }
        
        [XmlElement("IDENTITY_ISSUER")]
        public string IdentityIssuer { get; protected set; }
        
        [XmlElement("IDENTITY_CNP")]
        public string IdentityCNP { get; protected set; }
        
        [XmlElement("COMPANY")]
        public string Company { get; protected set; }
        
        [XmlElement("REGISTRATIONNUMBER")]
        public string RegistrationNumber { get; protected set; }
        
        [XmlElement("FISCALCODE")]
        public string FiscalCode { get; protected set; }
        
        [XmlElement("CBANKNAME")]
        public string CompanyBankName { get; protected set; }
        
        [XmlElement("CBANKACCOUNT")]
        public string CompanyBankAccount { get; protected set; }
        
        [XmlElement("ADDRESS1")]
        public string Address1 { get; protected set; }
        
        [XmlElement("ADDRESS2")]
        public string Address2 { get; protected set; }
        
        [XmlElement("CITY")]
        public string City { get; protected set; }
        
        [XmlElement("STATE")]
        public string State { get; protected set; }
        
        [XmlElement("ZIPCODE")]
        public string ZipCode { get; protected set; }
        
        [XmlElement("COUNTRY")]
        public string Country { get; protected set; }
        
        [XmlElement("PHONE")]
        public string PhoneNumber { get; protected set; }
        
        [XmlElement("FAX")]
        public string FaxNumber { get; protected set; }
        
        [XmlElement("CUSTOMEREMAIL")]
        public string CustomerEmail { get; protected set; }
        
        [XmlElement("FIRSTNAME_D")]
        public string DeliveryFirstName { get; protected set; }
        
        [XmlElement("LASTNAME_D")]
        public string DeliveryLastName { get; protected set; }
        
        [XmlElement("COMPANY_D")]
        public string DeliveryCompany { get; protected set; }
        
        [XmlElement("ADDRESS1_D")]
        public string DeliveryAddress1 { get; protected set; }
        
        [XmlElement("ADDRESS2_D")]
        public string DeliveryAddress2 { get; protected set; }
        
        [XmlElement("CITY_D")]
        public string DeliveryCity { get; protected set; }
        
        [XmlElement("STATE_D")]
        public string DeliveryState { get; protected set; }
        
        [XmlElement("ZIPCODE_D")]
        public string DeliveryZipCode { get; protected set; }
        
        [XmlElement("COUNTRY_D")]
        public string DeliveryCountry { get; protected set; }
        
        [XmlElement("PHONE_D")]
        public string DeliveryPhoneNumber { get; protected set; }
        
        [XmlElement("IPADDRESS")]
        public string IpAddress { get; protected set; }
        
        [XmlElement("CURRENCY")]
        public string Currency { get; protected set; }

        [XmlElement("IPN_TOTALGENERAL")]
        public decimal TotalGeneral { get; protected set; }
        
        [XmlElement("IPN_SHIPPING")]
        public decimal Shipping { get; protected set; }
        
        [XmlElement("IPN_GLOBALDISCOUNT")]
        public decimal GlobalDiscount { get; protected set; }
        
        [XmlElement("IPN_COMMISSION")]
        public decimal Commission { get; protected set; }
        
        [XmlElement("IPN_DATE")]
        public string Date { get; protected set; }
        
        [XmlElement("HASH")]
        public string Hash { get; protected set; }

        /* Public Methods */

        public static IPNRequest FromString(string response)
        {
            Console.WriteLine("Xml String is: '{0}'", response);
            using (var stringReader = new StringReader(response))
                return new XmlSerializer(typeof(IPNRequest)).Deserialize(stringReader) as IPNRequest;
        }

        public static IPNRequest FromHttpRequest(System.Web.HttpRequest request)
        {
            return FromNameValueCollection(request.Form);
        }

        public static IPNRequest FromNameValueCollection(NameValueCollection parameters)
        {
            var xmlStr = ConvertRequestFormToXml (parameters);
            return FromString (xmlStr);
        }

        /* Private Methods */

        private static string ConvertRequestFormToXml(NameValueCollection parameters) {
            StringBuilder xml = new StringBuilder ();
            var regex = new Regex(@"^([^\[]+)\[(\d+)\]$");
            var formData = parameters
                    .AllKeys
                    .Select(k => new { Key = k, Match = regex.Match(k)})
                    .Select(item => new { 
                        IsArray = item.Match.Success, // Does it match the array pattern
                        Key = item.Match.Success ? item.Match.Groups[1].Value : item.Key, // Sanitize the key 
                        Value = parameters[item.Key], // Extract the value
                        Index = item.Match.Success ? item.Match.Groups[2].Value : null // Grab the array index if it is an array
                    });

            var arrayPairGroups = formData
                .Where(item => item.IsArray)
                .GroupBy(item => item.Index);

            using (var writer = XmlWriter.Create(xml)) {
                writer.WriteStartElement(ROOT_ELEMENT_NAME);
                // First write out all the non-array key/values
                foreach (var item in formData.Where(item => !item.IsArray)) {
                    if (string.IsNullOrEmpty(item.Key))
                        continue;
                    writer.WriteStartElement(item.Key);
                    writer.WriteString(item.Value);
                    writer.WriteEndElement();
                }

                // Then write out array values (ie Products)
                // First the Products array tag
                writer.WriteStartElement(PRODUCTS_ELEMENT_NAME);
                // Look over each index group
                foreach (var grp in arrayPairGroups) {
                    // Write out a Product tag
                    writer.WriteStartElement(PRODUCT_ELEMENT_NAME);

                    // Write out all the items as tags
                    foreach (var item in grp)
                    {
                        if (string.IsNullOrEmpty(item.Key))
                            continue;
                        writer.WriteStartElement(item.Key);
                        writer.WriteString(item.Value);
                        writer.WriteEndElement();
                    }
                    // Close the Product tag
                    writer.WriteEndElement();
                }
                // Close the Products tag
                writer.WriteEndElement();
                
                writer.WriteEndElement();
            }
            return xml.ToString ();
        }

        private static void AppendToHashString(StringBuilder sb, object data) {
            var str = data.ToString();
            sb.Append(str.Length);
            sb.Append(str);
        }

        public string GenerateResponse() {
            var now = DateTime.Now.ToString("yyyyMMddHHmmss");
            var hashStr = new StringBuilder();

            AppendToHashString(hashStr, Products[0].Id);
            AppendToHashString(hashStr, Products[0].Name);
            AppendToHashString(hashStr,this.Date);
            AppendToHashString(hashStr, now);

            var hash = hashStr.ToString().HashWithSignature(Configuration.Instance.SignatureKey);

            return string.Format("<{0}>{1}|{2}</{0}>", ROOT_ELEMENT_NAME, now, hash);
        }
    }
}

