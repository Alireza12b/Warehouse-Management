using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseOperations.Domain;

namespace WarehouseOperations.Util
{
    public class StockJsonUtils
    {
        public static string StockPath()
        {
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string relativePath = Path.Combine(path, "StockJson.json");
            return relativePath;
        }

        public static List<Stock> StockJsonRead()
        {
            string stockJson = File.ReadAllText(StockPath());
            var stocks = JsonConvert.DeserializeObject<List<Stock>>(stockJson);
            return stocks;
        }

        public static void StockJsonWrite(List<Stock> stocks)
        {
            string stockSrialize = JsonConvert.SerializeObject(stocks);
            File.WriteAllText(StockPath(), stockSrialize);
        }
    }
}
