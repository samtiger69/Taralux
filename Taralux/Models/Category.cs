using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }

        public int IconId { get; set; }
        public virtual ImageBase Icon { get; set; }
        public virtual List<Category> Children { get; set; }
        public virtual List<CategoryImage> Images { get; set; }
        public virtual List<Item> Items { get; set; }

    }
}