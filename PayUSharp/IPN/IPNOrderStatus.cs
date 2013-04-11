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
    public enum IPNOrderStatus
    {
        [XmlEnum("PAYMENT_AUTHORIZED")]
        PaymentAuthorized,
        [XmlEnum("PAYMENT_RECEIVED")]
        PaymentReceived,
        [XmlEnum("TEST")]
        Test,
        [XmlEnum("CASH")]
        Cash,
        [XmlEnum("COMPLETE")]
        Complete,
        [XmlEnum("REVERSED")]
        Reversed,
        [XmlEnum("REFUND")]
        Refund
    }
    
}
