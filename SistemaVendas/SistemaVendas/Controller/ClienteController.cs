using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Clientes;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario,Vendedor")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            return await _service.BuscarTodos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> BuscarPorId(Guid id)
        {
            var cliente = await _service.BuscarPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet]
        [Route("buscar-todos")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorFiltroComPaginacao([FromQuery]ClienteParams cliParams)
        {
            PagedList<Cliente> data = await _service.BuscarPorFiltroComPaginacao(cliParams);
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

        [HttpPut]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest();
                }
                if (!ExisteCliente(cliente.Id))
                {
                    return NotFound();
                }
                return Ok(await _service.Editar(cliente));
            }
            catch (DbUpdateConcurrencyException e)
            {

                return BadRequest(new Exception(e.Message));

            }

        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Inserir(Cliente cliente)
        {

            await _service.Inserir(cliente);

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Cliente>> Excluir(Guid id)
        {
            var cliente = await _service.BuscarPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _service.Excluir(cliente.Id);

            return cliente;
        }

        private bool ExisteCliente(Guid id)
        {
            if (_service.BuscarPorId(id) != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
