using System.Windows;
using WorkflowWorker;
using WorkflowWorker.OrderProductServiceReference;

namespace OrderSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <remarks>TODO: Add threading to the submit methods.</remarks>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            SubmitOrder();
        }

        private void buttonSubmitResponse_Click(object sender, RoutedEventArgs e)
        {
            SubmitResponse();
        }

        private void SubmitOrder()
        {
            var order = GetOrder();
            OrderProductWorkflow.RunSubmitOrder(ref order);
            labelOrderId.Content = order.Id.ToString();
        }

        private void SubmitResponse()
        {
            var managerResponse = GetManagerResponse();
            var order = OrderProductWorkflow.RunApproveOrder(managerResponse);
            labelStatus.Content = order.Status;
        }

        private Order GetOrder()
        {
            return new Order
            {
                Customer = GetCustomer(),
                Products = OrderProductWorkflow.GetDummyProducts()
            };
        }

        private Customer GetCustomer()
        {
            return new Customer
            {
                Name = textBoxName.Text,
                Address = new Address
                {
                    City = textBoxCity.Text
                }
            };
        }

        private ManagerResponse GetManagerResponse()
        {
            return new ManagerResponse
            {
                ItemIdentifier = short.Parse(textBoxOrderId.Text),
                Approved = checkBoxApprove.IsChecked ?? false
            };
        }
    }
}
