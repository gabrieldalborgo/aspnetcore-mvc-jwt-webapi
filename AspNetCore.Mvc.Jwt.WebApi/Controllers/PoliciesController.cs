using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Mvc.Jwt.WebApi.Models;
using AspNetCore.Mvc.Jwt.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Mvc.Jwt.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
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

        /// <summary>
        /// Get the list of policies linked to a user name
        /// </summary>
        /// <param name="username">User name</param>
        /// <response code="200">Succesful operation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        /// <returns>List of policies</returns>
        [Authorize(Roles = "admin")]
        [HttpGet("client/{username}")]
        public async Task<ActionResult<IEnumerable<Policy>>> GetByClientName(string username)
        {
            var client = await this.clientService.GetByName(username);
            if (client == null)
                return NotFound();
            var policies = await this.policyService.GetByClientId(client.Id);
            return Ok(policies);
        }
    }
}
