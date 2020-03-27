using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Fornecedores;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Shared.Entities;

namespace SistemaVendas.Api.Controller
{
    [Route("svendas/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        [Route("buscar-todos")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarFornecedorPaginado([FromQuery]FornecedorParams parametros)
        {
            PagedList<Fornecedor> data = await _fornecedorService.BuscarPorFiltroComPaginacao(parametros);
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
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<ActionResult<Fornecedor>> Inserir(Fornecedor fornecedor)
        {
            if (fornecedorValido(fornecedor))
            {
                if (_fornecedorService.ExisteFornecedor(fornecedor.CNPJ))
                {
                    return BadRequest("Já existe Fornecedor com esse email cadastrado");
                }
                return Ok( await _fornecedorService.Inserir(fornecedor));

            }
            else
            {
                return BadRequest("Fornecedor invalido, verifique os dados inseridos.");
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        [Route("buscar")]
        public async Task<IEnumerable<Fornecedor>> BuscarPorFiltroComPaginacao([FromQuery]FornecedorParams uparams)
        {
            return await _fornecedorService.BuscarPorFiltroComPaginacao(uparams);
        }  


        [HttpGet]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<IEnumerable<Fornecedor>> BuscarTodos()
        {
            return await _fornecedorService.BuscarTodos();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<ActionResult<Fornecedor>> BuscarPorId(Guid id)
        {
            var fornecedor = await _fornecedorService.BuscarPorId(id);

            if (fornecedor == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }

            return fornecedor;
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<IActionResult> Editar(Fornecedor fornecedor)
        {
            if (!fornecedorValido(fornecedor))
            {
                return BadRequest("Usuário invalido, verifique os dados inseridos.");
            }
            try
            {
                await _fornecedorService.Editar(fornecedor);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await ExisteFornecedor(fornecedor.Id))
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
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<ActionResult<Fornecedor>> Excluir(Guid id)
        {
            if (id == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }
            try
            {

                return Ok(await _fornecedorService.Excluir(id));
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
        private bool fornecedorValido(Fornecedor fornecedor)
        {
            return null != fornecedor.CNPJ && fornecedor.Nome != null && fornecedor.Telefone != null ;
        }
        private async Task<bool> ExisteFornecedor(Guid id)
        {
            try
            {
                Fornecedor user = null;
                user = await _fornecedorService.BuscarPorId(id);
                return user != null;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}