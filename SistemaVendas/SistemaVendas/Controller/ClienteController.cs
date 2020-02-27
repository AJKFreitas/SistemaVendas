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
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _service.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
        [HttpPost]
        [Route("buscar-todos")]
        public async Task<IEnumerable<Cliente>> GetClientes([FromQuery]Params uparams)
        {
            return await _service.GetAll();
        }
        [HttpPut]
        public async Task<IActionResult> PutCliente( Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.Update(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
           
            await _service.Insert(cliente);

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Cliente>> DeleteCliente(Guid id)
        {
            var cliente = await _service.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _service.Delete(cliente.Id);

            return cliente;
        }

        private bool ClienteExists(Guid id)
        {
            if (_service.GetById(id) != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
