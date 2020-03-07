using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Shared.Entities
{
   public class Response<T>
    {
        public T Data { get; set; }

        public Response()
        {

        }

        public Response(T response)
        {
            Data = response;
        }
    }
}
