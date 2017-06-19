using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class Response
    {
        public ErrorCode ErrorCode { get; set; }
    }
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}