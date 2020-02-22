﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles ="Admin,Fornecedor,Funcionario")]
        public HttpStatusCode InserirFornecedor([FromBody]Fornecedor fornecedor)
        {

            return _fornecedorService.Insert(fornecedor);

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Fornecedor,Funcionario")]
        public IEnumerable<Fornecedor> Get()
        {
            return _fornecedorService.GetAll();
        }
    }
}