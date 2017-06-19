using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class Item : BaseEntity
    {
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int IconId { get; set; }
        public virtual ImageBase Icon { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<ItemImage> Images { get; set; }
    }
}