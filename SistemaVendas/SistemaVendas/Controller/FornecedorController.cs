using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendas.Aplication.InterfaceServices.Fornecedores;
using SistemaVendas.Core.Domains.Fornecedores.Entities;

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

    
        [HttpPost]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<ActionResult<Fornecedor>> RegisterFornecedor(Fornecedor fornecedor)
        {
            if (fornecedorValido(fornecedor))
            {
                if (_fornecedorService.ExisteFornecedor(fornecedor.CNPJ))
                {
                    return BadRequest("Já existe Usuário com esse email cadastrado");
                }
                await _fornecedorService.Insert(fornecedor);
                return CreatedAtAction("GetFornecedor", new { id = fornecedor.Id }, fornecedor);

            }
            else
            {
                return BadRequest("Usuário invalido, verifique os dados inseridos.");
            }

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        [Route("buscar-todos")]
        public async Task<IEnumerable<Fornecedor>> GetProdutos([FromQuery]FornecedorParams uparams)
        {
            return await _fornecedorService.GetAll(uparams);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<ActionResult<Fornecedor>> Get(Guid id)
        {
            var produto = await _fornecedorService.GetById(id);

            if (produto == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return produto;
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public async Task<IActionResult> Put(Fornecedor fornecedor)
        {
            if (!fornecedorValido(fornecedor))
            {
                return BadRequest("Usuário invalido, verifique os dados inseridos.");
            }
            try
            {
                await _fornecedorService.Update(fornecedor);
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
        public async Task<ActionResult<Fornecedor>> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            try
            {

                return Ok(await _fornecedorService.Delete(id));
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
            Fornecedor user = null;
            try
            {
                user = await _fornecedorService.GetById(id);
                return user != null;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}