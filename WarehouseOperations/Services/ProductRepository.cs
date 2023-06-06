using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseOperations.Domain;
using WarehouseOperations.Interface;
using static System.Reflection.Metadata.BlobBuilder;

namespace WarehouseOperations.Services
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products;
        private List<Stock> stocks;
        private string productRelativePath;
        private string stockRelativePath;

        public ProductRepository()
        {
            string? Directorypath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string dataBaseFolderPath = Path.Combine(Directorypath, "DataBase");
            productRelativePath = Path.Combine(dataBaseFolderPath, "ProductJson.json");
            stockRelativePath = Path.Combine(dataBaseFolderPath, "StockJson.json");

            products = ProductJsonRead();
            stocks = StockJsonRead();
        }

        public string AddProduct(Product product)
        {
            int newProductId = products.Count > 0 ? products.Max(u => u.ProductId) + 1 : 1;

            if (CheckProductName(product.Name) == true)
            {
                var newProduct = new Product()
                {
                    ProductId = newProductId,
                    Name = product.Name,
                    Barcode = Guid.NewGuid(),
                };
                var newStock = new Stock()
                {
                    StockId = Guid.NewGuid(),
                    Name = product.Name,
                    ProductId = newProductId,
                    ProductQuantity = 0,
                    ProductPrice = 0
                };

                products.Add(newProduct);
                stocks.Add(newStock);

                ProductJsonWrite(products);
                StockJsonWrite(stocks);

                return $"Product {product.Name} added to DB";
            }
            else
            {
                return "Couldn't add product to DB because name you entered is not match our limits";
            }
        }

        public string GetProductById(int id)
        {
            var productById = products.FirstOrDefault(product => product.ProductId == id);
            if (productById != null)
            {
                return "ID = " + productById.ProductId + " | " + "Name = " + productById.Name + " | " + "Barcode = " + productById.Barcode;
            }
            else
            {
                return "Entered Id not found";
            }
        }

        public List<Product> GetProductList()
        {
            return products.ToList();
        }

        public bool CheckProductName(string productName)
        {
            string nameCheck = @"^[A-Z][a-z]{3}[A-z a-z]_[0-9]{3}$";
            bool isValid = Regex.IsMatch(productName, nameCheck);
            return isValid;
        }


        private List<Product> ProductJsonRead()
        {
            string productJson = File.ReadAllText(productRelativePath);
            var products = JsonConvert.DeserializeObject<List<Product>>(productJson);
            return products ?? new List<Product>();
        }

        private void ProductJsonWrite(List<Product> products)
        {
            string jsonConvert = JsonConvert.SerializeObject(products);
            File.WriteAllText(productRelativePath, jsonConvert);
        }

        private List<Stock> StockJsonRead()
        {
            string stockJson = File.ReadAllText(stockRelativePath);
            var stocks = JsonConvert.DeserializeObject<List<Stock>>(stockJson);
            return stocks ?? new List<Stock>();
        }

        private void StockJsonWrite(List<Stock> stocks)
        {
            string jsonConvert = JsonConvert.SerializeObject(stocks);
            File.WriteAllText(stockRelativePath, jsonConvert);
        }
    }
}
