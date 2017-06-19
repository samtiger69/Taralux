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
        public ImageType Type { get; set; }
    }

    public enum ImageType
    {
        CategoryImage = 0,
        ItemImage = 1,
        ElectricianImage = 2
    }
}