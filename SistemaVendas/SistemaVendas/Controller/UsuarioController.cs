using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Shared.Entities;

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

        [HttpGet]
        [Route("buscar-todos")]
        [AllowAnonymous]
        public async Task<IActionResult> UsuariosPaginado([FromQuery]UsuarioParams uparams)
        {
            PagedList<Usuario> data = await _usuarioService.GetAll(uparams);
            var pageData = new
            {
                data.TotalCount,
                data.PageSize,
                data.CurrentPage,
                data.TotalPages,
                data.HasNext,
                data.HasPrevious
            };

            return Ok(new { data, pageData });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Usuario>> RegisterUser(Usuario usuario)
        {

            try
            {
                if (usuarioValido(usuario))
                {
                    if (_usuarioService.ExisteUsuario(usuario.Email))
                    {
                        return BadRequest("Já existe Usuário com esse email cadastrado");
                    }
                    await _usuarioService.Insert(usuario);
                    return Ok(usuario);

                }
                else
                {
                    return BadRequest("Usuário invalido, verifique os dados inseridos.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Erro ao cadastrar Usuário");
            }

        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {
            var produto = await _usuarioService.GetById(id);

            if (produto == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return produto;
        }
        [HttpPut]
        public async Task<IActionResult> Put(Usuario usuario)
        {
            if (!usuarioValido(usuario))
            {
                return BadRequest("Usuário invalido, verifique os dados inseridos.");
            }
            try
            {
                await _usuarioService.Update(usuario);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await ExisteUsuario(usuario.Id))
                {
                    return NotFound("Usuário não encontrado.");
                }
                else
                {
                    
                    return BadRequest(e.Message);
                }
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Usuario>> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound("Usuário não encontrado.");
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
        private async Task<bool> ExisteUsuario(Guid id)
        {
            Usuario user = null;
            try
            {
                user = await _usuarioService.GetById(id);
                return user != null;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }

    }
}