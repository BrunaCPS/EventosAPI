using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EventosAPI_2.DTOs
{
    public class ResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public T Dado { get; set; }
    }
}