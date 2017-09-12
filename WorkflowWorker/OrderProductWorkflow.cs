using System.Collections.Generic;
using WorkflowWorker.OrderProductServiceReference;

namespace WorkflowWorker
{
    public static class OrderProductWorkflow
    {
        /// <summary>
        /// Start the workflow for a new order.
        /// </summary>
        /// <param name="order">A reference to an Order object.</param>
        public static void RunSubmitOrder(ref Order order)
        {
            using (var client = new OrderProductServiceReference.ServiceClient())
            {
                client.SubmitOrder(ref order);
            }
        }

        /// <summary>
        /// Continue the workflow of an order.
        /// </summary>
        /// <param name="managerResponse">The managers response to an order.</param>
        /// <returns></returns>
        public static Order RunApproveOrder(ManagerResponse managerResponse)
        {
            using (var client = new OrderProductServiceReference.ServiceClient())
            {
                return client.ApproveOrder(managerResponse);
            }
        }

        /// <summary>
        /// Get a list with dummy products.
        /// </summary>
        /// <returns></returns>
        public static Product[] GetDummyProducts()
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

            return products.ToArray();
        }
    }
}
