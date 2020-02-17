using SistemaVendas.Core.Domains.Auth.Enums;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Fornecedores.Entities
{
    public class Fornecedor
    {
        public Guid Id { get ; set ; }
        public string Nome { get ; set ; }
        public string Telefone { get; set; }
        public long CNPJ { get; set; }

        public Fornecedor()
        {
        }

        public Fornecedor(string nome, string telefone, long cNPJ)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
        }

        public Fornecedor(Guid id, string nome, string telefone, long cNPJ)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
        }

       
    }
}
