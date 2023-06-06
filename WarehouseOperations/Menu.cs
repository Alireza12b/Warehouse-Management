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
        Product product;

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
        }
    }
}
