using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseOperations.Domain;
using WarehouseOperations.Interface;

namespace WarehouseOperations.Services
{
    public class StockRepository : IStockRepository
    {
        private List<Product> products;
        private List<Stock> stocks;
        private string productRelativePath;
        private string stockRelativePath;

        public StockRepository()
        {
            string? Directorypath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string dataBaseFolderPath = Path.Combine(Directorypath, "DataBase");
            productRelativePath = Path.Combine(dataBaseFolderPath, "ProductJson.json");
            stockRelativePath = Path.Combine(dataBaseFolderPath, "StockJson.json");

            products = ProductJsonRead();
            stocks = StockJsonRead();
        }

        public string BuyProduct(Stock productInStock)
        {
            var existingProduct = stocks.FirstOrDefault(product => product.ProductId == productInStock.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductQuantity += productInStock.ProductQuantity;
                StockJsonWrite(stocks);
                return "Product quantity updated successfully.";
            }
            else
            {
                return "Product quantity updated successfully.";
            }
        }

        public List<Stock> GetSalesProductList()
        {
            throw new NotImplementedException();
        }

        public string SaleProduct(int productId, int cnt)
        {
            throw new NotImplementedException();
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
