using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class ImageBase
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public bool IsDefault { get; set; }
        public int SourceId { get; set; }
        public ImageType Type { get; set; }
        public string Base64 { get; set; }
    }

    public enum ImageType
    {
        Unspecified = 0,
        CategoryImage = 1,
        ItemImage = 2,
        ElectricianImage = 3
    }
}