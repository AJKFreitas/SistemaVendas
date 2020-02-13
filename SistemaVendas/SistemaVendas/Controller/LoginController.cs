using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using System.Threading.Tasks;

namespace SistemaVendas.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Core.Domains.Auth.Services.Interfaces.IAuthorizationService _authorizationService;

        public LoginController(IAuthenticationService authenticationService, Core.Domains.Auth.Services.Interfaces.IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginUser loginUser)
        {
            var authorization = await _authorizationService.AuthorizeAsync(loginUser);
            if (!authorization.Success)
            {
                return Ok(authorization);
            }

            var authentication = await _authenticationService.AuthenticateAsync(authorization.Data);
            if (!authentication.Success)
            {
                return Ok(authentication);
            }

            return Ok(authentication);
        }
    }
}