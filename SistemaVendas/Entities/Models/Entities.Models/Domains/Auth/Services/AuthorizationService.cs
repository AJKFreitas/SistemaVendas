
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
namespace SistemaVendas.Core.Domains.Auth.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsuarioService _usuarioService;

        public AuthorizationService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<BaseResult<IUsuario>> AuthorizeAsync(
            LoginUser loginUser)
        {

            var loginOrEmail = loginUser?.Email ?? "";
            var password = loginUser?.Senha ?? "";

            var result = new BaseResult<IUsuario>();
            var userResult =  _usuarioService.GetAll();
            var user =  userResult.ToList().Where(u => u.Email.ToLower() == loginOrEmail.ToLower()
                           && u.Senha == password).FirstOrDefault();
            if (user != null)
            {
                result.Success = true;
                result.Message = "User authorized!";
                result.Data = new Usuario
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Role = user.Role,
                    IsAdmin = false
                };
            }
            else
            {
                result.Success = false;
                result.Message = "Not authorized!";
            }

            return await Task.FromResult(result);
        }


    }
}
