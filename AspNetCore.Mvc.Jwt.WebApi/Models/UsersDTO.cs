using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspNetCore.Mvc.Jwt.WebApi.Models
{
    public class UsersDTO
    {
        [JsonProperty("clients")]
        public IEnumerable<User> Users { get; set; }
    }
}
