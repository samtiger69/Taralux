using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class Electrician : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double AverageRate { get; set; }

        public int IconId { get; set; }
        public virtual ImageBase Icon { get; set; }
        public virtual List<ImageBase> Images { get; set; }
        public virtual List<ElectricianComment> Comments { get; set; }
    }
}