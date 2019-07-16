using System;
using System.Collections.Generic;

namespace AspNetCore.Mvc.Jwt.WebApi.Models
{
    public class ClientsDTO
    {
        public IEnumerable<Client> Clients { get; set; }
    }
}
