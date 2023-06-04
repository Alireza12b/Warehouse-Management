using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseOperations.Domain;
using WarehouseOperations.Interface;
using WarehouseOperations.NewFolder;
using WarehouseOperations.Util;

namespace WarehouseOperations.Services
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products;

        public ProductRepository()
        {
            ProductJsonUtils.ProductJsonRead();
        }

        public string AddProduct(Product product)
        {
            
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }

        public bool CheckProductName(string productName)
        {
            string nameCheck = "^[a-z]{1}[a-z_]{3}[0-9]{3}$";
            bool isValid = Regex.IsMatch(productName, nameCheck);
            if (!isValid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
