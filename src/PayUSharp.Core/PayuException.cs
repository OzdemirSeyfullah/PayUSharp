using System;

namespace PayU.Core
{
    public class PayuException: ApplicationException
    {
        public PayuException (string message, Exception innerException): base(message, innerException)
        {
        }
    }
}

