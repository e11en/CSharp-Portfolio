using System;
using WorkflowWorker;
using WorkflowWorker.OrderProductServiceReference;

namespace WorkflowConsoleClient
{
    class Program
    {
        /// <summary>
        /// Execute the Workflow console client application.
        /// The OrderProduct workflow is started.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("== OrderProduct Workflow ==");

            var run = Run();
            while (run)
            {
                run = Run();
            }
        }

        /// <summary>
        /// Run the application.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Start the workflow for a new order.
        /// </summary>
        private static void NewOrder()
        {
            var customer = GetCustomer();
            var products = OrderProductWorkflow.GetDummyProducts();
            var order = new Order
            {
                Customer = customer,
                Products = products
            };

            OrderProductWorkflow.RunSubmitOrder(ref order);
            Console.WriteLine("========================================================");
            Console.WriteLine($"Order {order.Id} is {order.Status}");
            Console.WriteLine("========================================================");
        }

        /// <summary>
        /// Continue the workflow of an order.
        /// </summary>
        private static void ApproveOrder()
        {
            Console.WriteLine("Enter orderId:");
            var orderId = Console.ReadLine();

            var managerResponse = new ManagerResponse
            {
                ItemIdentifier = short.Parse(orderId),
                Approved = true
            };
            var order = OrderProductWorkflow.RunApproveOrder(managerResponse);
            Console.WriteLine("========================================================");
            Console.WriteLine($"Order {order.Id} of customer {order.Customer.Name} is {order.Status}");
            Console.WriteLine("========================================================");
        }

        /// <summary>
        /// Collect user input to create a new customer object.
        /// </summary>
        /// <returns></returns>
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
    }
}
