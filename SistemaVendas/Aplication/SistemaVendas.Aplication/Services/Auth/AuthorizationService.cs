
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using SistemaVendas.Aplication.InterfaceServices.Auth;
using System.Security.Cryptography;

namespace SistemaVendas.Aplication.Services.Auth
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsuarioService _usuarioService;

        public AuthorizationService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<BaseResult<IUsuario>> AuthorizeAsync(LoginUser loginUser)
        {
            var hash = new CriptografiaHash(SHA512.Create());
            var loginOrEmail = loginUser?.Email ?? "";
            var password =  hash.CriptografarSenha(loginUser?.Senha ?? "");
            var result = new BaseResult<IUsuario>();
            var userResult = await _usuarioService.BuscarTodos();
            var user =
                userResult.ToList()
                .Where(u => u.Email.ToLower() == loginOrEmail.ToLower()
                           && u.Senha == password).FirstOrDefault();
            if (user != null )
            {

                result.Success = true;
                result.Message = "Usuário não Autorizado!";
                result.Data = new Usuario
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Role = user.Role,
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
