using WarehouseOperations.Domain;
using WarehouseOperations.Interface;
using WarehouseOperations.Services;

namespace WarehouseOperations
{
    public class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductRepository();
            Product product = new Product(1, "Aaaaa_123",Guid.NewGuid());
            productRepository.AddProduct(product);
        }
    }
}