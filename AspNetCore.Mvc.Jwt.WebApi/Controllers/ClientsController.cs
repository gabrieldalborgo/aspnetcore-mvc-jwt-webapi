using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Mvc.Jwt.WebApi.Models;
using AspNetCore.Mvc.Jwt.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Mvc.Jwt.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientService clientService;
        private IPolicyService policyService;

        public ClientsController(IClientService clientService, IPolicyService policyService)
        {
            this.clientService = clientService;
            this.policyService = policyService;
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Client>> GetByName(string name)
        {
            var client = await this.clientService.GetByName(name);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            var client = await this.clientService.GetById(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("policy/{policyNumber}")]
        public async Task<ActionResult<Client>> GetByPolicyNumber(string policyNumber)
        {
            var policy = await this.policyService.GetById(policyNumber);
            if (policy == null)
                return NotFound();
            return await this.GetById(policy.ClientId);
        }
    }
}
