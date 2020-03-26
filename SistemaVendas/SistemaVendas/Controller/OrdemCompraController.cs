using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Pedidos;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Infra.Data;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario,Vendedor")]
    public class OrdemCompraController : ControllerBase
    {
        private readonly IOrdemCompraService _ordemCompraService;

        public OrdemCompraController(IOrdemCompraService ordemCompraService)
        {
            _ordemCompraService = ordemCompraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemCompra>>> BuscarTodos()
        {
            return Ok(await _ordemCompraService.BuscarTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemCompra>> BuscarPorId(Guid id)
        {
            var ordemCompra = await _ordemCompraService.BuscarPorId(id);

            if (ordemCompra == null)
            {
                return NotFound();
            }

            return ordemCompra;
        }
        [HttpGet]
        [Route("buscar-pagina")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorFiltroComPaginacao([FromQuery]OrdemCompraParams parametros)
        {
            PagedList<OrdemCompra> pagina = await _ordemCompraService.BuscarPorFiltroComPaginacao(parametros);


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
        public async Task<IActionResult> Editar(OrdemCompraVM ordemCompraVM)
        {
            try
            {
                return Ok(await _ordemCompraService.Editar(ordemCompraVM));
            }
            catch (DbUpdateConcurrencyException e)
            {

                return BadRequest(new Exception(e.Message));

            }

        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<OrdemCompra>> Inserir([FromBody]LancarOrdemCompraVM ordemCompraVM)
        {
            return Ok(await _ordemCompraService.Inserir(ordemCompraVM));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrdemCompra>> Excluir(Guid id)
        {
            var ordemCompra = await _ordemCompraService.BuscarPorId(id);
            if (ordemCompra == null)
            {
                return NotFound();
            }

            return Ok(await _ordemCompraService.Excluir(ordemCompra.Id));

        }

        
    }
}
