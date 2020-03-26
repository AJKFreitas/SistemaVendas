using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Core.Domains.Dashboard;

namespace SistemaVendas.Api.Controller
{
    [Route("sVendas/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        private readonly IDashBoardRepository _repository;

        public DashBoardController(IDashBoardRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Produtos-Mais-Vendidos")]
        public async Task<ActionResult<IEnumerable<dynamic>>> CalculaEstoque()
        {
            var listResult = await _repository.ProdutosMaisVendidos();
            var resultado = listResult.Select(x => new[] {  x.Nome,  x.quantidadeVendida }).ToList();
            return Ok(resultado);
        }
    }
}
