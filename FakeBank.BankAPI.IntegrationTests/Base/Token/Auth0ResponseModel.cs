﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.IntegrationTests.Base.Token
{
    public class Auth0ResponseModel
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
