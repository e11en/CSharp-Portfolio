using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using WorkflowClient.OrderProductServiceReference;

namespace WorkflowClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== OrderProduct Workflow ==");

            var order = new Order
            {
                Customer = GetCustomer(),
                Products = GetProducts()
            };


            using (var client = new OrderProductServiceReference.ServiceClient())
            {
                client.SubmitOrder(ref order);
                Console.WriteLine($"Order {order.Id} is {order.Status}");
            }
            Console.ReadKey();
        }

        private static Customer GetCustomer()
        {
            var customer = new Customer
            {
                Address = new Address()
            };
            Console.WriteLine("Please enter your name:");
            customer.Name = Console.ReadLine();
            Console.WriteLine("Please enter your city:");
            customer.Address.City = Console.ReadLine();

            return customer;
        }

        private static IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Paper cup",
                    SKU = "PC1234",
                    Description = "Medium sized paper cup."
                },
                new Product
                {
                    Name = "Plastic Plate",
                    SKU = "PP5678",
                    Description = "Round white plastic plate."
                },
                new Product
                {
                    Name = "Balloon",
                    SKU = "BA1234",
                    Description = "Red plain balloon."
                }
            };


            return products;
        }
    }
}
