using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SistemaVendas.Core.Domains.Auth.Services
{
    public sealed class JwtIdentityAuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;

        public JwtIdentityAuthenticationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(IUsuario user) { 
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim("Data", ToJson(user))
        };

            var identity = new ClaimsIdentity(claims);

            var created = DateTime.UtcNow.AddDays(1);
            var expiration = created + TimeSpan.FromSeconds(60000);
            var handler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Security:SecretKeyJWT"]));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "FSL",
                Audience = "FSL",
                SigningCredentials = signingCredentials,
                Subject = identity,
                NotBefore = created,
                Expires = expiration
            });

            var dateFormat = "yyyy-MM-dd HH:mm:ss";
            var result = new AuthenticationResult
            {
                Success = true,
                Authenticated = true,
                Created = created.ToString(dateFormat),
                Expiration = expiration.ToString(dateFormat),
                AccessToken = handler.WriteToken(securityToken),
                Message = "OK"
            };

            return await Task.FromResult(result);
        }

      
        private string ToJson<T>(
            T obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj,
                 new JsonSerializerSettings()
                 {
                     NullValueHandling = NullValueHandling.Ignore
                 });
        }
    }
}
