using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class ImageBase
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public bool IsDefault { get; set; }
        public int SourceId { get; set; }
    }

    public class ItemImage : ImageBase
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }

    public class CategoryImage : ImageBase
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

    public class ElectricianImage: ImageBase
    {
        public int ElectricianId { get; set; }
        public virtual Electrician Electrician { get; set; }
    }
}