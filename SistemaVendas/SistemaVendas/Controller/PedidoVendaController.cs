using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoVendaController : ControllerBase
    {
        private readonly IPedidoVendaService _pedidoVendaService;

        public PedidoVendaController(IPedidoVendaService pedidoVenda)
        {
            _pedidoVendaService = pedidoVenda;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoVenda>>> BuscarTodos()
        {
            return Ok(await _pedidoVendaService.BuscarTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoVenda>> BuscarPorId(Guid id)
        {
            var pedidoVenda = await _pedidoVendaService.BuscarPorId(id);

            if (pedidoVenda == null)
            {
                return NotFound();
            }

            return pedidoVenda;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(PedidoVenda pedidoVenda)
        {
            
            try
            {
              return Ok(await _pedidoVendaService.Editar(pedidoVenda));
            }
            catch (DbUpdateConcurrencyException)
            {
                
            return NoContent();
                    throw;
             
            }

        }

        [HttpPost]
        public async Task<ActionResult<PedidoVenda>> Inserir(PedidoVendaVM pedidoVendaVM)
        {


            return Ok(await _pedidoVendaService.Inserir(pedidoVendaVM));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PedidoVenda>> DeletePedidoVenda(Guid id)
        {
            var pedidoVenda = await _pedidoVendaService.BuscarPorId(id);
            if (pedidoVenda == null)
            {
                return NotFound();
            }

            return Ok(_pedidoVendaService.Excluir(id));
        }

        private bool PedidoVendaExists(Guid id)
        {
            return _pedidoVendaService.ExistePedidoVenda(id);
        }
    }
}
