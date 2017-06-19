using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class ElectricianRate
    {
        public int Id { get; set; }
        public int ElectricianId { get; set; }
        public string RatedBy { get; set; }

        public int RateValue { get; set; }

        public virtual Electrician Electrician { get; set; }
    }
}