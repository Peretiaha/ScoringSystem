using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.Autorization
{
    public class ApiAuthSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public int ExpirationTimeInSeconds { get; set; }
    }
}
