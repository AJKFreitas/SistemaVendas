using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public HttpStatusCode RegisterUser(Usuario usuario)
        {

            return _usuarioService.Insert(usuario);

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<Usuario> Get()
        {
            return _usuarioService.GetAll();
        }
     
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public Usuario GetById(Guid id)
        {
            return _usuarioService.GetById(id);
        }
    }
}