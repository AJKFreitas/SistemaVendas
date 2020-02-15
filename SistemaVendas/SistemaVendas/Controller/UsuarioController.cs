using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuarioController : ControllerBase
    {
       private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public  HttpStatusCode RegisterUser(Usuario usuario)
        {
            
          return  _usuarioService.Insert(usuario);

        }
        [HttpGet]
        public  IEnumerable<Usuario> Get()
        {
            return  _usuarioService.GetAll();
        }
    }
}