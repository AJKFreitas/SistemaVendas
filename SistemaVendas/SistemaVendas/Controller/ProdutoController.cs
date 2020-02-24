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

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario,Fornecedor")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        [HttpGet]
        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _produtoService.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(Guid id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }


        [HttpPut]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            try
            {
                await _produtoService.Update(produto);
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
        public async Task<ActionResult<Produto>> PostProduto(ProdutoVM produtoVM)
        {
            await _produtoService.Insert(produtoVM);
            
            return CreatedAtAction("GetProduto", new { id = produtoVM.Id }, produtoVM);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Produto>> DeleteProduto(Guid id)
        {
            var produto = await _produtoService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoService.Delete(produto.Id);
            return produto;
        }

        private bool ProdutoExists(Guid id)
        {
            if (_produtoService.GetById(id) != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
