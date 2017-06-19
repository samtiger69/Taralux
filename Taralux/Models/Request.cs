using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class Request
    {
        public Settings Settings { get; set; }
    }

    public class Request<T> : Request
    {
        public T Data { get; set; }
    }
}