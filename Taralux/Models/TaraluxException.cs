using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class TaraluxException : Exception
    {
        public ErrorCode ErrorCode { get; set; }
    }
}