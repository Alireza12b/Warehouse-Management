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
        IProductRepository productRepository = new ProductRepository();

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
                var newQuantity = existingProduct.ProductQuantity + productInStock.ProductQuantity;
                var newPrice = ((existingProduct.ProductPrice * existingProduct.ProductQuantity) + (productInStock.ProductPrice * productInStock.ProductQuantity)) / newQuantity;
                existingProduct.ProductQuantity = newPrice;
                existingProduct.ProductPrice = newQuantity;

                StockJsonWrite(stocks);
                return "Product quantity and price updated successfully.";
            }
            else
            {
                int newProductId = products.Count > 0 ? products.Max(u => u.ProductId) + 1 : 1;
                var newProduct = new Product()
                {
                    ProductId = newProductId,
                    Name = productInStock.Name,
                    Barcode = Guid.NewGuid(),
                };
                var newStock = new Stock()
                {
                    StockId = Guid.NewGuid(),
                    Name = productInStock.Name,
                    ProductId = newProductId,
                    ProductQuantity = productInStock.ProductQuantity,
                    ProductPrice = productInStock.ProductPrice
                };

                products.Add(newProduct);
                stocks.Add(newStock);

                ProductJsonWrite(products);
                StockJsonWrite(stocks);

                return productRepository.GetProductById(newProductId) + "\n added to DB";
            }
        }

        public List<Stock> GetSalesProductList()
        {
            throw new NotImplementedException();
        }

        public string SaleProduct(int productId, int cnt)
        {
            var validProduct = (from product in stocks
                                where product.ProductId == productId && product.ProductQuantity != 0
                                select product).FirstOrDefault();
            if (validProduct != null)
            {
                if (GetProductQuantity(productId) >= cnt)
                {
                    validProduct.ProductQuantity -= cnt;
                    return validProduct.Name + "sold and quantity decreased";
                }
                else
                {
                    return productRepository.GetProductById(productId) + "\n Product quantity is lower than your request";
                }
            }
            else
            {
                return "Product not found or out of stock";
            }
        }

        int GetProductQuantity(int productId)
        {
            var product = stocks.FirstOrDefault(s => s.ProductId == productId);
            return product.ProductQuantity;
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
