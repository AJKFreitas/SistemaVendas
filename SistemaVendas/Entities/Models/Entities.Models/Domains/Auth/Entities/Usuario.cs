using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Auth.Entities
{


    public class Usuario : IUsuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public virtual IEnumerable<OrdemCompra> OrdemCompras { get; set; } = new List<OrdemCompra>();
        public virtual IEnumerable<PedidoVenda> Pedidos { get; set; } = new List<PedidoVenda>();
        public Usuario()
        {
        }

        public Usuario(string nome, string email, string senha, string role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public Usuario(Guid id, string nome, string email, string senha, string role)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }
    }
}


