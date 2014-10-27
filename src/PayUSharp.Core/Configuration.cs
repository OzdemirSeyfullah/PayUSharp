namespace PayU.Core
{
    public class Configuration
    {
        private static readonly Configuration instance = new Configuration();

        public static Configuration Instance
        {
            get
            {
                return Configuration.instance;
            }
        }

        public string Environment { get; private set; }

        public string SignatureKey { get; private set; }

        public bool IgnoreSSLCertificate { get; private set; }

        static Configuration()
        {
        }

        private Configuration() 
        {
            IgnoreSSLCertificate = false;
        }

        public Configuration SetEnvironment(string environment)
        {
            this.Environment = environment;
            return this;
        }

        public Configuration SetSignatureKey(string signatureKey)
        {
            this.SignatureKey = signatureKey;
            return this;
        }

        public Configuration SetIgnoreSSLCertificate(bool ignore)
        {
            this.IgnoreSSLCertificate = ignore;
            return this;
        }
    }
}