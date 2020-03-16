using System;
using System.Collections.Generic;

namespace SistemaVendas.Aplication.Dtos
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }

        public ProdutoDto()
        {

        }
    }
}