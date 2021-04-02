using System.Collections.Generic;
using System.IO;

namespace Api
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int ProductRating { get; set; }
        public int OriginalPrice { get; set; }
        public int OfferPrice { get; set; }
        public List<string> Categories { get; set; }
        public string OrderedCount { get; set; }
        public List<ImageFile> ImageFiles { get; set; }
        public List<string> ImageUrls { get; set; }
        public int Likes { get; set; }
        public bool IsRecommended { get; set; }
    }
    public class ImageFile
    {
        public FileStream FileStream { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}