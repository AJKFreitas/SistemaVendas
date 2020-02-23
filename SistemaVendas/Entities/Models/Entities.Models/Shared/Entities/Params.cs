﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Shared.Entities
{
    public class Params
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 50;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}