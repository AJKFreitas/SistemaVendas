using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class OrdemCompra
    {
        public Guid Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Nota { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public Guid IdFornecedor { get; set; }
        public IEnumerable<ItemOrdemCompra> ItemsOrdemCompra { get; set; } = new List<ItemOrdemCompra>();
        public double ValorTotal { get; set; }

        public OrdemCompra()
        {
        }

        public OrdemCompra(Guid id, DateTime dataEntrada, string nota, Fornecedor fornecedor, Guid idFornecedor, IEnumerable<ItemOrdemCompra> itemsOrdemCompra, double valorTotal)
        {
            Id = id;
            DataEntrada = dataEntrada;
            Nota = nota;
            Fornecedor = fornecedor;
            IdFornecedor = idFornecedor;
            ItemsOrdemCompra = itemsOrdemCompra;
            ValorTotal = valorTotal;
        }

        public OrdemCompra( DateTime dataEntrada, string nota, Fornecedor fornecedor, Guid idFornecedor, IEnumerable<ItemOrdemCompra> itemsOrdemCompra, double valorTotal)
        {
            Id = Guid.NewGuid();
            DataEntrada = dataEntrada;
            Nota = nota;
            Fornecedor = fornecedor;
            IdFornecedor = idFornecedor;
            ItemsOrdemCompra = itemsOrdemCompra;
            ValorTotal = valorTotal;
        }

        public OrdemCompra(Guid id, DateTime dataEntrada, Guid idFornecedor, IEnumerable<ItemOrdemCompra> itemsOrdem, double valorTotal)
        {
            Id = id;
            DataEntrada = dataEntrada;
            IdFornecedor = idFornecedor;
            ItemsOrdemCompra = itemsOrdem;
            ValorTotal = valorTotal;
        }

        public OrdemCompra(DateTime dataEntrada, Guid idFornecedor, IEnumerable<ItemOrdemCompra> itemsOrdemCompra, double valorTotal)
        {
            DataEntrada = dataEntrada;
            IdFornecedor = idFornecedor;
            ItemsOrdemCompra = itemsOrdemCompra;
            ValorTotal = valorTotal;
        }

       
    }
}
