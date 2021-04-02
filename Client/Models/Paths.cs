using System.Collections.Generic;

namespace Client.Models
{
    public class Paths
    {
        public static List<string> CarrerList()
        {
            return new List<string>() {"Logesh", "Palani", "Guna", "Meena", "Palani", "Guna", "Meena", "Logesh", "Palani", "Guna", "Meena" };
        }
    }
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
        public List<string> ImageUrls { get; set; }
        public int Likes { get; set; }
        public bool IsRecommended { get; set; }
    }
}