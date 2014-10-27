using System;
using PayU.Core;

namespace PayU.Core.Base
{
    public class DeliveryDetails
    {
        /* All parameters are optional */            
        [Parameter(Name = "DELIVERY_LNAME")]
        public string LastName { get; set; }
        
        [Parameter(Name = "DELIVERY_FNAME")]
        public string FirstName { get; set; }
        
        [Parameter(Name = "DELIVERY_EMAIL")]
        public string Email { get; set; }
        
        [Parameter(Name = "DELIVERY_PHONE")]
        public string PhoneNumber { get; set; }
        
        [Parameter(Name = "DELIVERY_COMPANY")]
        public string Company { get; set; }
        
        [Parameter(Name = "DELIVERY_ADDRESS")]
        public string Address { get; set; }
        
        [Parameter(Name = "DELIVERY_ADDRESS2")]
        public string Address2 { get; set; }
        
        [Parameter(Name = "DELIVERY_ZIPCODE")]
        public string ZipCode { get; set; }
        
        [Parameter(Name = "DELIVERY_CITY")]
        public string City { get; set; }
        
        [Parameter(Name = "DELIVERY_STATE")]
        public string State { get; set; }
        
        [Parameter(Name = "DELIVERY_COUNTRYCODE")]
        public string CountryCode { get; set; }
    }
}

