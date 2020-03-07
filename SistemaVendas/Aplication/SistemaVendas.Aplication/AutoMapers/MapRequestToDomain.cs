using AutoMapper;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.AutoMapers
{
   public class MapRequestToDomain : Profile
    {
        public MapRequestToDomain()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
