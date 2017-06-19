﻿using System;
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

        public virtual List<ElectricianRate> Rates { get; set; }
        public virtual List<ElectricianImage> Images { get; set; }
        public virtual List<ElectricianComment> Comments { get; set; }
    }
}