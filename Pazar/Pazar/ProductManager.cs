using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Pazar
{
    public class ProductManager
    {
        private static List<Product> products = new List<Product>();
        private static readonly string ImageFolder = Path.Combine(Application.StartupPath, "ProductImages");

        static ProductManager()
        {
            // Create image folder if it doesn't exist
            if (!Directory.Exists(ImageFolder))
            {
                Directory.CreateDirectory(ImageFolder);
            }

            // Initialize with sample products
            products.Add(new Product
            {
                Id = 1,
                Name = "iPhone 13",
                Description = "128GB, Mavi, Yeni",
                Price = 25000.00m,
                Category = "Elektronik",
                IsNew = true,
                SellerId = 1,
                CreateDate = DateTime.Now,
                IsSold = false,
                ImagePath = GetImagePath(1)
            });

            products.Add(new Product
            {
                Id = 2,
                Name = "Samsung Galaxy S21",
                Description = "256GB, Siyah, Kullanılmış",
                Price = 15000.00m,
                Category = "Elektronik",
                IsNew = false,
                SellerId = 2,
                CreateDate = DateTime.Now,
                IsSold = false,
                ImagePath = GetImagePath(2)
            });

            products.Add(new Product
            {
                Id = 3,
                Name = "Nike Spor Ayakkabı",
                Description = "42 numara, Siyah",
                Price = 1200.00m,
                Category = "Giyim",
                IsNew = true,
                SellerId = 3,
                CreateDate = DateTime.Now,
                IsSold = false,
                ImagePath = GetImagePath(3)
            });

            products.Add(new Product
            {
                Id = 4,
                Name = "Adidas T-Shirt",
                Description = "L beden, Beyaz",
                Price = 350.00m,
                Category = "Giyim",
                IsNew = true,
                SellerId = 1,
                CreateDate = DateTime.Now,
                IsSold = false,
                ImagePath = GetImagePath(4)
            });

            products.Add(new Product
            {
                Id = 5,
                Name = "Philips Kahve Makinesi",
                Description = "Otomatik, Siyah",
                Price = 2500.00m,
                Category = "Ev & Yaşam",
                IsNew = true,
                SellerId = 2,
                CreateDate = DateTime.Now,
                IsSold = false,
                ImagePath = GetImagePath(5)
            });
        }

        private static string GetImagePath(int productId)
        {
            return Path.Combine(ImageFolder, $"product_{productId}.jpg");
        }

        public static bool SaveProductImage(int productId, string sourceImagePath)
        {
            try
            {
                string targetPath = GetImagePath(productId);
                File.Copy(sourceImagePath, targetPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProductImage(int productId)
        {
            try
            {
                string imagePath = GetImagePath(productId);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddProduct(Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            newProduct.CreateDate = DateTime.Now;
            newProduct.IsSold = false;
            newProduct.ImagePath = GetImagePath(newProduct.Id);
            products.Add(newProduct);
            return true;
        }

        public static bool UpdateProduct(Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.Category = updatedProduct.Category;
                product.IsNew = updatedProduct.IsNew;
                product.ImagePath = GetImagePath(product.Id);
                return true;
            }
            return false;
        }

        public static bool DeleteProduct(int productId)
        {
            try
            {
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    // First delete the product image
                    DeleteProductImage(productId);
                    
                    // Then remove the product from the list
                    products.Remove(product);
                    
                    // Reorder IDs
                    for (int i = 0; i < products.Count; i++)
                    {
                        products[i].Id = i + 1;
                    }
                    
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Product> GetAllProducts()
        {
            return products;
        }

        public static List<Product> GetAllAvailableProducts()
        {
            return products.Where(p => !p.IsSold).ToList();
        }

        public static List<Product> GetProductsBySeller(int sellerId)
        {
            return products.Where(p => p.SellerId == sellerId).ToList();
        }

        public static List<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => p.Category == category).ToList();
        }

        public static bool MarkAsSold(int productId, int buyerId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.IsSold = true;
                product.BuyerId = buyerId;
                return true;
            }
            return false;
        }

        public static Product GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.Id == productId);
        }

        public static List<Product> SearchProducts(string searchTerm)
        {
            return products.Where(p => 
                p.Name.Contains(searchTerm) || 
                p.Description.Contains(searchTerm) ||
                p.Category.Contains(searchTerm)).ToList();
        }

        public static List<string> GetCategories()
        {
            return products.Select(p => p.Category).Distinct().ToList();
        }

        public static string[] GetAllCategories()
        {
            return products.Select(p => p.Category).Distinct().ToArray();
        }

        public int GetSoldProductsCount()
        {
            return products.Where(p =>p.IsSold).ToList().Count;
        }


        public int GetProductsCount()
        {
            return products.Count;
        }
    }
} 