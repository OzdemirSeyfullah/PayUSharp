using System.IO;
using System.Web;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace PayU.AutomaticLiveUpdate
{
	public enum Status {
		[XmlEnum("SUCCESS")]
		Success,
	    [XmlEnum("FAILED")]
	    Failed,
	    [XmlEnum("INPUT_ERROR")]
	    InputError
	}

	public enum ReturnCode {
		[XmlEnum("AUTHORIZED")]
		Authorized,
		[XmlEnum("3DS_ENROLLED")]
		ThreeDSEnrolled,
		[XmlEnum("ALREADY_AUTHORIZED")]
		AlreadyAuthorized,
		[XmlEnum("AUTHORIZATION_FAILED")]
		AuthorizationFailed,
		[XmlEnum("INVALID_CUSTOMER_INFO")]
		InvalidCustomerInfo,
		[XmlEnum("INVALID_PAYMENT_INFO")]
		InvalidPaymentInfo,
		[XmlEnum("INVALID_ACCOUNT")]
		InvalidAccount,
		[XmlEnum("INVALID_PAYMENT_METHOD_CODE")]
		InvalidPaymentMethodCode,
		[XmlEnum("INVALID_CURRENCY")]
		InvalidCurrency,
		[XmlEnum("REQUEST_EXPIRED")]
		RequestExpired,
		[XmlEnum("HASH_MISMATCH")]
		HashMismatch
	}

    [XmlRoot(ROOT_ELEMENT_NAME)]
    public class AluResponse
    {
        private const string ROOT_ELEMENT_NAME = "EPAYMENT";

        [XmlElement("REFNO")]
        public string RefNo { get; set; }

        [XmlElement("ALIAS")]
        public string Alias { get; set; }

        [XmlElement("STATUS")]
        public Status Status { get; set; }

        [XmlElement("RETURN_CODE")]
        public ReturnCode ReturnCode { get; set; }

        [XmlElement("RETURN_MESSAGE")]
        public string ReturnMessage { get; set; }

        [XmlElement("DATE")]
        public string Date { get; set; }

        [XmlElement("CURRENCY")]
        public string Currency { get; set; }

        [XmlElement("INSTALLMENTS_NO")]
        public string InstallmentNumber { get; set; }

        [XmlElement("CARD_PROGRAM_NAME")]
        public string CardProgramName { get; set; }

        [XmlElement("URL_3DS")]
        public string Url3DS { get; set; }

        [XmlElement("HASH")]
        public string Hash { get; set; }

        public bool IsSuccess
        {
            get { return Status == Status.Success; }
        }

        public bool Is3DSReponse {
            get 
            { 
                return (ReturnCode == ReturnCode.ThreeDSEnrolled &&
                        Url3DS != null && 
                        Url3DS != string.Empty);
            }
        }

        public static AluResponse FromString(string response)
        {
            using (var stringReader = new StringReader(response))
                return new XmlSerializer(typeof(AluResponse)).Deserialize(stringReader) as AluResponse;
        }

        public static AluResponse FromHttpRequest(HttpRequest request)
        {
            var xmlStr = ConvertRequestFormToXml (request);
            return FromString (xmlStr);
        }

        private static string ConvertRequestFormToXml(HttpRequest request) {
            StringBuilder xml = new StringBuilder ();
            using (var writer = XmlWriter.Create(xml)) {
                writer.WriteStartElement(ROOT_ELEMENT_NAME);
                foreach (var key in request.Form.AllKeys) {
                    writer.WriteStartElement(key);
                    writer.WriteString(request.Form[key]);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return xml.ToString ();
        }
    }
}