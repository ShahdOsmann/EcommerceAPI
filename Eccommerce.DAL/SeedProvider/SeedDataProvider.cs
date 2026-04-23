using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.SeedProvider
{
    public class SeedDataProvider
    {
        public static List<Product> GetProducts()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);

            return new List<Product>
            {

             new Product(){Title="Iphone 14 Pro Max",Price=1500,Description="Apple'ın en yeni telefonu",ExpiryDate=new DateOnly(2026, 12, 31),Count=10,CategoryId=1, CreatedAt=createdDate, UpdatedAt=createdDate},
                new Product(){Title="Samsung Galaxy S23 Ultra",Price=1400,Description="Samsung'un en yeni telefonu",ExpiryDate=new DateOnly(2026, 12, 31),Count=15,CategoryId=1, CreatedAt=createdDate, UpdatedAt=createdDate},
                new Product(){Title="Xiaomi Mi 12 Pro",Price=1200,Description="Xiaomi'nin en yeni telefonu",Count=20,ExpiryDate=new DateOnly(2026, 12, 31),CategoryId=1, CreatedAt=createdDate, UpdatedAt=createdDate},
                new Product(){Title="Sony WH-1000XM4",Price=350,Description="Sony'nin en iyi kablosuz kulaklığı",ExpiryDate=new DateOnly(2026, 12, 31),Count=30,CategoryId=2, CreatedAt=createdDate, UpdatedAt=createdDate},
                new Product(){Title="Bose QuietComfort 35 II",Price=300,Description="Bose'un en iyi kablosuz kulaklığı",ExpiryDate=new DateOnly(2026, 12, 31),Count=25,CategoryId=2, CreatedAt=createdDate, UpdatedAt=createdDate},
                new Product(){Title="Apple AirPods Pro",Price=250,Description="Apple'ın en iyi kablosuz kulaklığı",ExpiryDate=new DateOnly(2026, 12, 31),Count=40,CategoryId=2, CreatedAt=createdDate, UpdatedAt=createdDate}
            };
        }
        public static List<Category> GetCategories()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);

            return new List<Category>
            {
                 new Category(){Name="Electronics"},
                new Category(){Name="phone"}
            };
        }
    }
}
