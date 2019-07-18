using AspNetCore.Mvc.Jwt.WebApi.Models;
using AspNetCore.Mvc.Jwt.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Authenticate the user by name
        /// </summary>
        /// <remarks>In this particulary case password is not required, its only necessary to fill in the username because we dont have a pasword to check</remarks>
        /// <param name="user">User</param>
        /// <response code="200">Succesful operation</response>
        /// <response code="400">Incorrect user name</response>
        /// <response code="500">Internal server error</response>
        /// <returns>User</returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User user)
        {
            var token = await accountService.Authenticate(user.Username, user.Password);

            if (token == null)
                return BadRequest(new { message = "Incorrect username" });

            return Ok(token);
        }
    }
}