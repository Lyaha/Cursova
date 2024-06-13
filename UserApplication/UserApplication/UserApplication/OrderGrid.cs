using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    public partial class OrderGrid : Form
    {
        private CustomDataGridView orderGrid;
        private Label lblH1;
        private RoundButton AddBttn;
        private HttpClient httpClient;
        public OrderGrid(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadOrdersAsync();
        }
        public async Task LoadOrdersAsync()
        {
            List<Order> orders = new List<Order>();
            Order order = new Order();
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7287/api/Order");
                foreach (var item in response)
                {
                    var response2 = await httpClient.GetFromJsonAsync<List<Payments>>($"https://localhost:7287/api/Payment/order/{item.OrderId}");
                    var response3 = await httpClient.GetFromJsonAsync<Buyer>($"https://localhost:7287/api/Buyer/{item.BuyerId}");
                    var respons4 = await httpClient.GetFromJsonAsync<OrdersStatus>($"https://localhost:7287/api/OrderStatus/{item.StatusId}");
                    foreach (var item1 in response2)
                    {
                        order = new Order()
                        {
                            OrderId = item.OrderId,
                            OrderDate = item.OrderDate,
                            BuyerId = item.BuyerId,
                            BuyerName = response3.name,
                            StatusId = item.StatusId,
                            StatusName = respons4.StatusName,
                            Amounts = item1.Amount
                        };
                    }
                    orders.Add(order);
                }
                orderGrid.SetDataOrders(orders, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stocks: {ex.Message}");
            }
        }
        private void AddButtonClick(object sender, EventArgs e)
        {
            Order order = new Order();
            EditOrderForm detailsForm = new EditOrderForm(order, false, httpClient);
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                LoadOrdersAsync();
            }
        }
    }
}
