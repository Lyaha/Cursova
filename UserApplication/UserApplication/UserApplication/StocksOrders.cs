using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    public partial class StocksOrders : Form
    {
        private HttpClient httpClient;
        private ICurrentUserService _currentUserService;
        public StocksOrders(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            this.sideMenu.SetActiveButton(this.sideMenu.stockorderButton);
            LoadStockAsync();
            LoadOrdersAsync();
        }
        private void productButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.productButton);
            Form1 product = new Form1(httpClient);
            FormNavigator.Navigate(this, product);
        }
        private void userButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.userButton);
            UserProfileForm user = new UserProfileForm(httpClient);
            FormNavigator.Navigate(this, user);
        }
        private void SideMenu_MenuToggled(object sender, EventArgs e)
        {
            if (sideMenu.IsExpanded)
            {
                this.stockDataGridView.Location = new Point(this.sideMenu.ExpandedWidth, 30);
                this.StockH1.Location = new Point(this.sideMenu.Width, 0);
                this.OrdersH1.Location = new Point(this.sideMenu.Width, 200);
                this.ordersDataGridView.Location = new Point(this.sideMenu.Width, 230);
            }
            else
            {
                this.stockDataGridView.Location = new Point(this.sideMenu.Width, 30);
                this.StockH1.Location = new Point(this.sideMenu.Width, 0);
                this.OrdersH1.Location = new Point(this.sideMenu.Width, 200);
                this.ordersDataGridView.Location = new Point(this.sideMenu.Width, 230);
            }
        }
        public async Task LoadStockAsync()
        {
            List<Stock> stocks = new List<Stock>();
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Stock>>("https://localhost:7287/api/StockMovements");
                foreach (var item in response)
                {
                    var response2 = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{item.ProductId}");
                    var response3 = await httpClient.GetFromJsonAsync<MovementType>($"https://localhost:7287/api/MovemenTypes/{item.MovementTypeId}");
                    var stock = new Stock()
                    { 
                        MovementId = item.MovementId,
                        ProductId = item.ProductId,
                        ProductName = response2.ProductName,
                        MovementTypeId = item.MovementTypeId,
                        MovementName = response3.TypeName,
                        Quantity = item.Quantity,
                        MovementDate = item.MovementDate,
                        BatchNumber = item.BatchNumber,
                        Notes = item.Notes
                    };
                    stocks.Add(stock);

                }
                stockDataGridView.SetDataStocks(stocks, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stocks: {ex.Message}");
            }
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
                ordersDataGridView.SetDataOrders(orders, false);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error loading stocks: {ex.Message}");
            }
        }
    }
    public class Stock
    {
        public int MovementId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int MovementTypeId { get; set; }

        public string MovementName { get; set; }

        public int Quantity { get; set; }

        public DateTime MovementDate { get; set; }

        public string BatchNumber { get; set; }

        public string Notes { get; set; }
    }

    public partial class MovementType
    {
        public int MovementTypeId { get; set; }

        public string TypeName { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }

        public int BuyerId { get; set; }

        public string BuyerName { get; set; }

        public DateTime OrderDate { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public decimal Amounts { get; set; }
    }

    public class OrdersDeteils
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class OrdersStatus
    {
       public int OrdersStatusId { get; set; }
       
       public string StatusName { get; set; }

    }

    public class Payments
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        public int PaymentMethodId { get; set; }
        
        public string PaymentMethodName { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }

        public string MethodName { get; set; }
    }


    public class Buyer
    {
        public int idBuyer { get; set; }

        public string name { get; set; }

        public string contactPhone { get; set; }

        public string contactEmail { get; set; }
    }
}
