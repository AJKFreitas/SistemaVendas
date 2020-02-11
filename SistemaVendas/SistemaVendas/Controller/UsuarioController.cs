using System;
using System.Collections.Generic;
using System.Linq;
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(Usuario usuario)
        {
            if (usuario is null)
            {
                return BadRequest();
            }
            _usuarioService.Insert(usuario);
            return CreatedAtAction(nameof(_usuarioService.GetById), new { id = usuario.Id }, usuario);

        }

    }
}