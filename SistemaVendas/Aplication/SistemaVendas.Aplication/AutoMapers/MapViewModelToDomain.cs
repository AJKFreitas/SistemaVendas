using AutoMapper;
using SistemaVendas.Aplication.ViewModels;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
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

            CreateMap <LancarPedidoVendaVM, PedidoVenda>()
                .ForMember(pvm => pvm.ItemPedidos, opt => opt
                .MapFrom(pv => pv.ItemPedidosVM.Select(i => new ItemPedidoVenda {
                                Id = Guid.NewGuid(),
                                Quantidade = i.Quantidade,
                                Preco = i.Preco,
                                SubTotal = i.SubTotal,
                                IdProduto = i.IdProduto,
                                IdPedido = pv.Id
                                }
                ).ToList()));


            CreateMap<LancarItemPedidoVendaVM, ItemPedidoVenda>();
            CreateMap<ClienteVM, Cliente>();
            CreateMap<OrdemCompraVM, OrdemCompra>()
                .ForMember(pvm => pvm.ItemsOrdemCompra, opt => opt
                .MapFrom(pv => pv.ItemsOrdemCompraVM.Select(i => new ItemOrdemCompra
                {
                    Id = Guid.NewGuid(),
                    Quantidade = i.Quantidade,
                    Preco = i.Preco,
                    IdProduto = i.IdProduto,
                    IdOrdemCompra = pv.Id,
                    SubTotal = i.SubTotal
                }
                ).ToList()));
            CreateMap<ItemOrdemCompraVM, ItemOrdemCompra>();
            CreateMap<UsuarioVM, Usuario>();

        }
    }
}
