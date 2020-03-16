using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
using System.Threading.Tasks;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Aplication.InterfaceServices.Auth.IAuthorizationService _authorizationService;

        public LoginController(IAuthenticationService authenticationService, Aplication.InterfaceServices.Auth.IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Loguin([FromBody] LoginUser loginUser)
        {
            var authorization = await _authorizationService.AuthorizeAsync(loginUser);
            if (!authorization.Success)
            {
                return Unauthorized(authorization);
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