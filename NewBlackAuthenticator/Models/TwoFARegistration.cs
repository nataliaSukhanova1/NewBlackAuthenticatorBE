using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlackAuthenticator.Models
{
    public class TwoFARegistration
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
