using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.Dtos
{
   public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public ClienteDto()
        {

        }
    }
}
