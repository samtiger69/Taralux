using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class ElectricianComment
    {
        public int Id { get; set; }
        public int ElectricianId { get; set; }
        public string By { get; set; }
        public string Comment { get; set; }

        public double RateValue { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Electrician Electrician { get; set; }
    }
}