﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario,Vendedor")]
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
        [HttpGet]
        [Route("buscar-pagina")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorFiltroComPaginacao([FromQuery]PedidoVendaParams parametros)
        {
            PagedList<PedidoVenda> pagina = await _pedidoVendaService.BuscarPorFiltroComPaginacao(parametros);
       

            var pageData = new
            {
                pagina.TotalCount,
                pagina.PageSize,
                pagina.CurrentPage,
                pagina.TotalPages,
                pagina.HasNext,
                pagina.HasPrevious
            };

            return Ok(new { pagina, pageData });
        }
        [HttpPut]
        public async Task<IActionResult> Editar(PedidoVendaVM pedidoVenda)
        {
            try
            {
              return Ok(await _pedidoVendaService.Editar(pedidoVenda));
            }
            catch (DbUpdateConcurrencyException e)
            {
                
                return BadRequest( new Exception( e.Message));
             
            }

        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<PedidoVenda>> Inserir([FromBody]LancarPedidoVendaVM pedidoVendaVM)
        {
            string Token = Request.Headers["authorization"];
            return Ok(await _pedidoVendaService.Inserir(pedidoVendaVM, Token));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PedidoVenda>> Excluir(Guid id)
        {
            var pedidoVenda = await _pedidoVendaService.BuscarPorId(id);
            if (pedidoVenda == null)
            {
                return NotFound();
            }

            return Ok(await _pedidoVendaService.Excluir(pedidoVenda.Id));
             
        }

        private bool PedidoVendaExists(Guid id)
        {
            return _pedidoVendaService.ExistePedidoVenda(id);
        }
    }
}
