using PayU.Core;

namespace PayU.AutomaticLiveUpdate
{
    public class CardDetails
    {
        [Parameter(Name = "CC_NUMBER")]
        public string CardNumber { get; set; }
        [Parameter(Name = "EXP_MONTH")]
        public string ExpiryMonth { get; set; }
        [Parameter(Name = "EXP_YEAR")]
        public string ExpiryYear { get; set; }
        [Parameter(Name = "CC_TYPE")]
        public string CardType { get; set; }
        [Parameter(Name = "CC_CVV")]
        public string CVV { get; set; }
        [Parameter(Name = "CC_OWNER")]
        public string CardOwnerName { get; set; }
    }
}