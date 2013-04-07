using System;

namespace PayU.Base
{
    public class BillingDetails
    {
        [Parameter(Name = "BILL_LNAME")]
        public string LastName { get; set; }
        
        [Parameter(Name = "BILL_FNAME")]
        public string FirstName { get; set; }
        
        [Parameter(Name = "BILL_EMAIL")]
        public string Email { get; set; }
        
        [Parameter(Name = "BILL_PHONE")]
        public string PhoneNumber { get; set; }
        
        [Parameter(Name = "BILL_COUNTRYCODE")]
        public string CountryCode { get; set; }

        /* Optional Parameters */
        
        [Parameter(Name = "BILL_FAX")]
        public string Fax { get; set; }
        
        [Parameter(Name = "BILL_ADDRESS")]
        public string Address { get; set; }
        
        [Parameter(Name = "BILL_ADDRESS2")]
        public string Address2 { get; set; }
        
        [Parameter(Name = "BILL_ZIPCODE")]
        public string ZipCode { get; set; }
        
        [Parameter(Name = "BILL_CITY")]
        public string City { get; set; }
        
        [Parameter(Name = "BILL_STATE")]
        public string State { get; set; }
    }
}

