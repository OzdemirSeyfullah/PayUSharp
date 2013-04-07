using System;

namespace PayU
{
    public class PayuException: ApplicationException
    {
        public PayuException (string message, Exception innerException): base(message, innerException)
        {
        }
    }
}

