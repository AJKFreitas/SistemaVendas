using AutoMapper;
using SistemaVendas.Aplication.Dtos;
using SistemaVendas.Core.Domains.Clientes.Entities;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.AutoMapers
{
    class MapDomainToDto : Profile
    {
        public MapDomainToDto()
        {
            CreateMap<PedidoVenda, PedidoVendaDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<ItemPedidoVenda, ItemPedidoVendaDto>().ReverseMap();


        }
    }
}
