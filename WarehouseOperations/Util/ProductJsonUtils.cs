using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseOperations.Domain;

namespace WarehouseOperations.NewFolder
{
    public static class ProductJsonUtils
    {
        public static string ProductPath()
        {
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string relativePath = Path.Combine(path, "ProductJson.json");
            return relativePath;
        }

        public static List<Product> ProductJsonRead()
        {
            string productJson = File.ReadAllText(ProductPath());
            var products = JsonConvert.DeserializeObject<List<Product>>(productJson);
            return products;
        }

        public static void ProductJsonWrite(List<Product> products)
        {
            string productSrialize = JsonConvert.SerializeObject(products);
            File.WriteAllText(ProductPath(), productSrialize);
        }
    }
}
