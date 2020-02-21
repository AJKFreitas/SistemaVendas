using AutoMapper;
using SistemaVendas.Core.Domains.Produtos.VMs;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System.Linq;

namespace SistemaVendas.Core.AutoMapers
{
    public class AutoMapping : Profile

    {
        public AutoMapping()
        {
            CreateMap<ProdutoVM, Produto>();

            
        }
    }
}
