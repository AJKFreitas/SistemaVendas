using SistemaVendas.Core.Domains.Auth.Enums;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Fornecedores.Entities
{
    public class Fornecedor : IUsuario
    {
        public Guid Id { get ; set ; }
        public string Nome { get ; set ; }
        public string Telefone { get; set; }
        public int CNPJ { get; set; }
        public EnumTipoUsuario TipoUsuario { get; } = EnumTipoUsuario.Fornecedor;
        public string Email { get ; set ; }
        public string Senha { get ; set ; }

        public Fornecedor()
        {
        }

        public Fornecedor(string nome, string telefone, int cNPJ)
        {
            Id = new Guid();
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
        }

        public Fornecedor(Guid id, string nome, string telefone, int cNPJ)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
        }

        public Fornecedor(string nome, string telefone, int cNPJ, string email, string senha) : this(nome, telefone, cNPJ)
        {
            Id = new Guid();
            Email = email;
            Senha = senha;
        }
    }
}
