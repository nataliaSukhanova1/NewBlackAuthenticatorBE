using dotAPNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewBlackAuthenticator.Models
{
    public class ApnsJwtOptions
    {
        public string CertFilePath { get; set; }
        public string CertContent { get; set; }
        public string KeyId { get; set; }
        public string TeamId { get; set; }
        public string BundleId { get; set; }
    }
}

    


