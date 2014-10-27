using System;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Globalization;

namespace PayU.IPN
{
    public class IPNProduct {
        [XmlElement("IPN_PID")]
        public string Id { get; set; }

        [XmlElement("IPN_PNAME")]
        public string Name { get; set; }
        
        [XmlElement("IPN_PCODE")]
        public string Code { get; set; }
        
        [XmlElement("IPN_INFO")]
        public string Information { get; set; }
        
        [XmlElement("IPN_QTY")]
        public int Quantity { get; set; }
        
        [XmlElement("IPN_PRICE")]
        public decimal Price { get; set; }
        
        [XmlElement("IPN_VAT")]
        public decimal Vat { get; set; }
        
        [XmlElement("IPN_VER")]
        public string Version { get; set; }
        
        [XmlElement("IPN_DISCOUNT")]
        public decimal Discount { get; set; }
        
        [XmlElement("IPN_PROMONAME")]
        public string PromotionName { get; set; }
        
        [XmlElement("IPN_DELIVEREDCODES")]
        public string DeliveredCodes { get; set; }
        
        [XmlElement("IPN_TOTAL")]
        public string Total { get; set; }

    }
    
}
