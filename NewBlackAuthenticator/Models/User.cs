﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlackAuthenticator.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceToken1 { get; set; }
        public string DeviceToken2 { get; set; }
    }

}
