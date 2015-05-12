using System.IO;
using System.Web;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System;

namespace PayU.AutomaticLiveUpdate
{
	public class WireAccount
	{
    [XmlElement("BANK_IDENTIFIER")] 
    public string BankIdentifier { get; set; }
    [XmlElement("BANK_ACCOUNT")]
    public string BankAccount { get; set; }
    [XmlElement("ROUTING_NUMBER")]
    public string RoutingNumber { get; set; }
    [XmlElement("IBAN_ACCOUNT")]
    public string IbanAccount { get; set; }
    [XmlElement("BANK_SWIFT")]
    public string BankSwift { get; set; }
    [XmlElement("COUNTRY")]
    public string Country { get; set; }
    [XmlElement("WIRE_RECIPIENT")]
    public string WireRecipient { get; set; }
    [XmlElement("WIRE_RECIPIENT_VAT_ID")]
    public string WireRecipientVatId { get; set; }
	}
}
