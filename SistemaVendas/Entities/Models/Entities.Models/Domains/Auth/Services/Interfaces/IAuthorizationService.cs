
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using System.Threading.Tasks;

namespace SistemaVendas.Core.Domains.Auth.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<BaseResult<IUsuario>> AuthorizeAsync(LoginUser loginUser);
    }
}
