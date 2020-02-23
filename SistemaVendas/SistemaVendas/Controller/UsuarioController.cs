using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<Usuario>> RegisterUser(Usuario usuario)
        {
            if (usuarioValido(usuario))
            {
                 await _usuarioService.Insert(usuario);
                return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);

            }
            else
            {
                  return BadRequest();
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("buscar-todos")]
        public async Task<IEnumerable<Usuario>> GetProdutos([FromQuery]UsuarioParams uparams)
        {
            return await _usuarioService.GetAll(uparams);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {
            var produto = await _usuarioService.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }
        [HttpPut]
        public async Task<IActionResult> Put(Usuario usuario)
        {
            if (!usuarioValido(usuario))
            {
                return BadRequest();
            }
            try
            {
                await _usuarioService.Update(usuario);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ExisteUsuario(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw e;
                }
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Usuario>> DeleteProduto(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {

            return Ok(await _usuarioService.Delete(id));
            }
            catch (Exception e)
            {

              return BadRequest(e);
            }
        }
        private bool usuarioValido(Usuario usuario)
        {
            return usuario.Email != null && usuario.Nome != null && usuario.Senha != null & usuario.Role != null;
        }
        private bool ExisteUsuario(Guid id)
        {
            if (_usuarioService.GetById(id) != null)
            {
                return true;
            }
            else
                return false;
        }

    }
}