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
    public class PoliciesController : ControllerBase
    {
        private IClientService clientService;
        private IPolicyService policyService;

        public PoliciesController(IClientService clientService, IPolicyService policyService)
        {
            this.clientService = clientService;
            this.policyService = policyService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("client/{clientName}")]
        public async Task<ActionResult<Client>> GetByPolicyNumber(string clientName)
        {
            var client = await this.clientService.GetByName(clientName);
            if (client == null)
                return NotFound();
            var policies = await this.policyService.GetByClientId(client.Id);
            return Ok(policies);
        }
    }
}
