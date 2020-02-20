using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Clientes.Entities
{
   public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public virtual IEnumerable<PedidoVenda> Pedidos { get; set; } = new List<PedidoVenda>();

        public Cliente()
        {
                
        }

        public Cliente(string nome, long cPF, string telefone, string endereco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CPF = cPF;
            Telefone = telefone;
            Endereco = endereco;
        }

        public Cliente(Guid id, string nome, long cPF, string telefone, string endereco)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            Telefone = telefone;
            Endereco = endereco;
        }

        public Cliente(Guid id, string nome, long cPF, string telefone, string endereco, IEnumerable<PedidoVenda> pedidos) : this(id, nome, cPF, telefone, endereco)
        {
            Pedidos = pedidos;
        }
    }
}
