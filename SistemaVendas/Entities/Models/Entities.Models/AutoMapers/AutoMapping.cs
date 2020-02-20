using AutoMapper;
using SistemaVendas.Core.Domains.Produtos.Dtos;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System.Linq;

namespace SistemaVendas.Core.AutoMapers
{
    public class AutoMapping : Profile

    {
        public AutoMapping()
        {
            CreateMap<Produto, ProdutoVM>()
                 .ForMember(dr => dr.Fornecedor, opt => opt
                 .MapFrom(d => d.ProdutoFornecedores
                 .Select(y => y.Fornecedor)
                 .ToList())).ReverseMap();
        }
    }
}
