using System;

namespace Pazar
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsNew { get; set; }
        public int SellerId { get; set; }
        public int? BuyerId { get; set; }  // Alıcı ID'si (null olabilir)
        public DateTime CreateDate { get; set; }
        public bool IsSold { get; set; }
        public string ImagePath { get; set; }
    }
} 