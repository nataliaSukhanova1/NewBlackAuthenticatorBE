using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlackAuthenticator.Models
{
    public class UpdateTwoFARegistrationStatusRequest
    {
        public string Token { get; set; }
        public string UserAnswer { get; set; }
    }
}
