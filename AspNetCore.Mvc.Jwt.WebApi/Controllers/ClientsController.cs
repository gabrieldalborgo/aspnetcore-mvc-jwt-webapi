using AspNetCore.Mvc.Jwt.WebApi.Models;
using AspNetCore.Mvc.Jwt.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
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

        /// <summary>
        /// Get user data filtered by user name
        /// </summary>
        /// <param name="username">User name</param>
        /// <response code="200">Succesful operation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        /// <returns>User</returns>
        [Authorize(Roles = "admin,user")]
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Client>> GetByName(string username)
        {
            var client = await this.clientService.GetByName(username);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Get user data filtered by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <response code="200">Succesful operation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        /// <returns>User</returns>
        [Authorize(Roles = "admin,user")]
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Client>> GetById(string userId)
        {
            var client = await this.clientService.GetById(userId);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Get the user linked to a policy number
        /// </summary>
        /// <param name="policyNumber">Policy number</param>
        /// <response code="200">Succesful operation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">Policy not found</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Client</returns>
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
