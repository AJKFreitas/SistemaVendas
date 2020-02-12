using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;

namespace SistemaVendas.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
       private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<HttpStatusCode> RegisterUser(Usuario usuario)
        {
            
          return   await  _usuarioService.Insert(usuario);

        }
        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            return Ok("ok");
        }
    }
}