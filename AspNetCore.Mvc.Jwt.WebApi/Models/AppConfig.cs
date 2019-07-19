using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Models
{
    public class AppConfig
    {
        public string UrlPolicies { get; set; }
        public string UrlUsers { get; set; }
        public string TokenSigningKey { get; set; }
    }
}
