using System.Collections.Generic;

namespace AspNetCore.Mvc.Jwt.WebApi.Models
{
    public class PoliciesDTO
    {
        public IEnumerable<Policy> Policies { get; set; }
    }
}
