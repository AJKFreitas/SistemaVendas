﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Aplication.ViewModels
{
    public class ItemPedidoVendaVM
    {
        //public Guid Id { get; set; } 
        public long Quantidade { get; set; }
        public double Preco { get; set; }
        public double SubTotal { get; set; }
        public Guid IdProduto { get; set; }
        //public Guid IdPedido { get; set; }

    }
}
