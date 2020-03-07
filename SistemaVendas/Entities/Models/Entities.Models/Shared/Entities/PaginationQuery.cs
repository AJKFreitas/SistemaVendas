using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Shared.Entities
{
public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 50;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
