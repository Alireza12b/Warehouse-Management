using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using WarehouseOperations.Domain;
using WarehouseOperations.Interface;
using WarehouseOperations.Services;

namespace WarehouseOperations
{
    public class Menu
    {
        IProductRepository productRepository = new ProductRepository();
        IStockRepository stockRepository = new StockRepository();
        Product product;
        Stock stock;

        public void MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                              Welcome to the Warehouse !                                                ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nSelect what do you want to do ?\n" +
                "1- Product Management\n" +
                "2- Stock Management\n");

            bool validMainMenuSelection = int.TryParse(Console.ReadLine(), out int mainMenuSelection);
            if (validMainMenuSelection)
            {
                if (mainMenuSelection == 1)
                {
                    ProductMenu();
                }
                else if (mainMenuSelection == 2)
                {
                    StockMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter valid menu item");
                    MainMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please enter valid menu item");
                MainMenu();
            }
        }

        public void ProductMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                                Product Management                                                  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nSelect what do you want to do ?\n" +
                "1- Add Product\n" +
                "2- Search Product by ID\n" +
                "3- Get List of All Products\n" +
                "0- Back to MainMenu\n");

            bool validProductMenuSelection = int.TryParse(Console.ReadLine(), out int productMenuSelection);
            if (validProductMenuSelection)
            {

                if (productMenuSelection==1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter new product name\n" +
                        "Use Correct Format eg : Aaaaa_123\n" +
                        "0- Back to Menu\n");
                    string addProductSelection = Console.ReadLine();

                    if (addProductSelection == "0")
                    {
                        Console.Clear();
                        ProductMenu();
                    }
                    else if (string.IsNullOrEmpty(addProductSelection))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter valid name");
                        ProductMenu();
                    }
                    else
                    {
                        Console.Clear();
                        product = new Product()
                        {
                            Name = addProductSelection
                        };
                        Console.WriteLine(productRepository.AddProduct(product));
                        Console.WriteLine("Please press any key");
                        Console.ReadKey();
                        Console.Clear();
                        ProductMenu();
                    }

                }

                else if (productMenuSelection == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter product ID\n" +
                        "0- Back to Menu\n");
                    bool validId = int.TryParse(Console.ReadLine(), out int id);
                    if (validId)
                    {
                        if (id == 0)
                        {
                            Console.Clear();
                            ProductMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(productRepository.GetProductById(id));
                            Console.WriteLine("Please press any key");
                            Console.ReadKey();
                            Console.Clear();
                            ProductMenu();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter valid id");
                        ProductMenu();
                    }
                }

                else if (productMenuSelection == 3)
                {
                    Console.Clear();
                    var productList = productRepository.GetProductList();
                    foreach (var item in productList)
                    {
                        Console.WriteLine($"ID = {item.ProductId} | Name = {item.Name} | Barcode = {item.Barcode}");
                    }
                    Console.WriteLine("\nPlease press any key");
                    Console.ReadKey();
                    Console.Clear();
                    ProductMenu();
                }

                else if (productMenuSelection == 0)
                {
                    Console.Clear();
                    MainMenu();
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter valid menu item");
                    ProductMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please enter valid menu item");
                ProductMenu();
            }
        }

        public void StockMenu()
        {
            Console.Clear();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                                 Stock Management                                                   ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nSelect what do you want to do ?\n" +
                "1- Buy Product\n" +
                "2- Sale Product\n" +
                "3- Get Stock List\n" +
                "0- Back to MainMenu\n");

            bool validStockMenuSelection = int.TryParse(Console.ReadLine(), out int StockMenuSelection);
            if (validStockMenuSelection)
            {

                if (StockMenuSelection == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter product ID or Enter a name to buy and add\n" +
                       "0- Back to Menu\n");
                    var productList = productRepository.GetProductList();
                    foreach (var item in productList)
                    {
                        Console.WriteLine($"ID = {item.ProductId} | Name = {item.Name} | Barcode = {item.Barcode}");
                    }
                    string input = Console.ReadLine();
                    bool validId = int.TryParse(input, out int id);
                    if (validId)
                    {
                        if (id == 0)
                        {
                            Console.Clear() ;
                            StockMenu();
                        }
                        else
                        {
                            Console.WriteLine("Please enter quantity that you want to buy");
                            bool validQuantity = int.TryParse(Console.ReadLine(),out int quantity);
                            if (validQuantity)
                            {
                                Console.WriteLine("Please enter the price of 1 item of the product");
                                bool validprice = int.TryParse(Console.ReadLine(), out int price);
                                if (validprice)
                                {
                                    stock = new Stock()
                                    {
                                        ProductId = id,
                                        ProductQuantity = quantity,
                                        ProductPrice = price
                                    };
                                    Console.WriteLine(stockRepository.BuyProduct(stock));
                                    Console.WriteLine("\nPlease press any key");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StockMenu();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Please enter correct price");
                                    StockMenu();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter correct quantity");
                                StockMenu();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter quantity that you want to buy");
                        bool validQuantity = int.TryParse(Console.ReadLine(), out int quantity);
                        if (validQuantity)
                        {
                            Console.WriteLine("Please enter the price of 1 item of the product");
                            bool validprice = int.TryParse(Console.ReadLine(), out int price);
                            if (validprice)
                            {
                                stock = new Stock()
                                {
                                    Name = input,
                                    ProductId = -1,
                                    ProductQuantity = quantity,
                                    ProductPrice = price
                                };
                                Console.WriteLine(stockRepository.BuyProduct(stock));
                                Console.WriteLine("\nPlease press any key");
                                Console.ReadKey();
                                Console.Clear();
                                StockMenu();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter correct price");
                                StockMenu();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter correct quantity");
                            StockMenu();
                        }
                    }
                }

                else if(StockMenuSelection == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter product ID or Enter a name to buy and add\n" +
                      "0- Back to Menu\n");
                    var productList = productRepository.GetProductList();
                    foreach (var item in productList)
                    {
                        Console.WriteLine($"ID = {item.ProductId} | Name = {item.Name} | Barcode = {item.Barcode}");
                    }

                    bool validId = int.TryParse(Console.ReadLine(), out int id);
                    if (validId)
                    {
                        Console.WriteLine("Enter quantity that you want to sell");
                        bool validQuantity = int.TryParse(Console.ReadLine(), out int quantity);
                        if (validQuantity)
                        {
                            Console.WriteLine(stockRepository.SaleProduct(id,quantity));
                            Console.WriteLine("\nPlease press any key");
                            Console.ReadKey();
                            Console.Clear();
                            StockMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter correct form of Quantity");
                            StockMenu();
                        }
                    }
                    else if (id == 0)
                    {
                        Console.Clear();
                        StockMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter correct form of ID");
                        StockMenu();
                    }
                }

                else if (StockMenuSelection == 3)
                {
                    Console.Clear();
                    var stocks = stockRepository.GetSalesProductList();
                    foreach(var item in stocks)
                    {
                        Console.WriteLine($"ID = {item.ProductId} | Name = {item.Name} | Quantity = {item.ProductQuantity} | Price = {item.ProductPrice} | StokID {item.StockId}");
                    }
                    Console.WriteLine("\nPlease press any key");
                    Console.ReadKey();
                    Console.Clear();
                    StockMenu();
                }

                else if (StockMenuSelection == 0)
                {
                    Console.Clear();
                    MainMenu();
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter valid menu item");
                    StockMenu();
                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please enter valid menu item");
                StockMenu();
            }
        }
    }
}
