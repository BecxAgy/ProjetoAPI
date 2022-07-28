﻿namespace IWantApp.Domain.Products
{
    public class Product : Entity
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public bool HasStock { get; set; }
        public bool Active { get; set; } = true;

    }
}