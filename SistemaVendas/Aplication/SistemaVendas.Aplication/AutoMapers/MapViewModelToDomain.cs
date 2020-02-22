using AutoMapper;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System.Linq;

namespace SistemaVendas.Core.AutoMapers
{
    public class MapViewModelToDomain : Profile

    {
        public MapViewModelToDomain()
        {

            CreateMap<Produto, ProdutoVM>()
                .ForMember(pvm => pvm.Fornecedores, opt => opt
                    .MapFrom(p => p.ProdutoFornecedores
                    .Select(pf => pf.Fornecedor)
                    .ToList())).ReverseMap();

            CreateMap<Fornecedor, FornecedorVM>()
                .ForMember(fVM => fVM.Produtos, opt => opt
                    .MapFrom(f => f.ProdutosFornecidos
                    .Select(pf => pf.Produto)
                    .ToList())).ReverseMap();

            CreateMap<ProdutoVM, Produto>()
              .ForMember(pvm => pvm.ProdutoFornecedores, opt => opt
              .MapFrom(p => p.Fornecedores.Select(x => new ProdutoFornecedor { IdFornecedor = x.Id , IdProduto = p.Id})
              .ToList()));

            CreateMap<FornecedorVM, ProdutoFornecedor>()
                 .ForPath(x => x.Fornecedor, opt => opt.MapFrom(x => x))
                 .ForPath(x => x.IdFornecedor, opt => opt.MapFrom(x => x.Id));

            CreateMap<ProdutoVM, ProdutoFornecedor>()
                .ForPath(x => x.Produto, opt => opt.MapFrom(x => x))
                .ForPath(x => x.IdProduto, opt => opt.MapFrom(x => x.Id));

                




        }
    }
}
