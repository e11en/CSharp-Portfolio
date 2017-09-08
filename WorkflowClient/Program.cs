using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

            var run = Run();
            while (run)
            {
                run = Run();
            }
        }

        private static bool Run()
        {
            Console.WriteLine();
            Console.WriteLine("Choose action: ");
            Console.WriteLine("Make new order(1), Approve order(2), Exit with any other key");
            var action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    NewOrder();
                    return true;
                case "2":
                    ApproveOrder();
                    return true;
                default:
                    return false;
            }
        }

        private static void NewOrder()
        {
            var customer = GetCustomer();
            var products = GetProducts();
            var order = new Order
            {
                Customer = customer,
                Products = products
            };

            using (var client = new OrderProductServiceReference.ServiceClient())
            {
                client.SubmitOrder(ref order);
                Console.WriteLine("========================================================");
                Console.WriteLine($"Order {order.Id} is {order.Status}");
                Console.WriteLine("========================================================");
            }
        }

        private static void ApproveOrder()
        {
            Console.WriteLine("Enter orderId:");
            var orderId = Console.ReadLine();

            using (var client = new OrderProductServiceReference.ServiceClient())
            {
                var managerResponse = new ManagerResponse
                {
                    ItemIdentifier = Int16.Parse(orderId),
                    Approved = true
                };
                var order = client.ApproveOrder(managerResponse);
                Console.WriteLine("========================================================");
                Console.WriteLine($"Order {order.Id} of customer {order.Customer.Name} is {order.Status}");
                Console.WriteLine("========================================================");
            }
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
