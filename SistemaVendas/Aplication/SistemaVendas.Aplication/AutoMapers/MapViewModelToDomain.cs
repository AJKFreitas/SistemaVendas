using AutoMapper;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System.Linq;

namespace SistemaVendas.Core.AutoMapers
{
    public class MapViewModelToDomain : Profile

    {
        public MapViewModelToDomain()
        {
            CreateMap<ProdutoVM, Produto>();

            
        }
    }
}
