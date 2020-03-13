using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Aplication.InterfaceServices.Produtos;
using System.Net.Mime;
using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Fornecedor,Funcionario,Vendedor")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        [HttpGet]
        public async Task<IEnumerable<Produto>> BuscarTodos()
        {

            return await   _produtoService.BuscarTodos();

        }

        [HttpGet]
        [Route("buscar-todos")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorFiltroComPaginacao([FromQuery]ProdutoParams parametros)
        {
            PagedList<Produto> data = await _produtoService.BuscarPorFiltroComPaginacao(parametros);
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

        [HttpGet]
        [Route("buscar")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorFiltroComPaginacaoList([FromQuery]ProdutoParams parametros)
        {
            PagedList<Produto> produtos = await _produtoService.BuscarPorFiltroComPaginacao(parametros);
            var pageData = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };
            List<Produto> list = new List<Produto>(produtos);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> BuscarPorId(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        [Route("estoque")]
        public async Task<ActionResult<dynamic>> CalculaEstoque(Produto produto)
        {
            if (produto != null)
            {
               
                return await _produtoService.CalcularEstoque(produto.Id);
            }
                return BadRequest("Produto inválido!");
        }


        [HttpPut]
        public async Task<IActionResult> Editar(Produto produto)
        {
            try
            {
                await _produtoService.Editar(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
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
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<Produto>> Inserir(ProdutoVM produtoVM)
        {
            await _produtoService.Inserir(produtoVM);

            return CreatedAtAction("GetProduto", new { id = produtoVM.Id }, produtoVM);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Produto>> Excluir(Guid id)
        {
            var produto = await _produtoService.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoService.Excluir(produto.Id);
            return produto;
        }

        private bool ProdutoExists(Guid id)
        {
            if (_produtoService.BuscarPorId(id) != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
