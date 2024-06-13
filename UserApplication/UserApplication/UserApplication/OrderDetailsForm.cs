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
    public partial class OrderDetailsForm : Form
    {
        private Label lblOrderId, lblBuyerId, lblBuyerName, lblOrderDate, lblStatusId, lblStatusName, lableH1Buyer, lblContactPhone, lblContactEmail, labelH1OrderDeteil, LabelH1Payments;
        private RoundedTextBox txtOrderId, txtBuyerId, txtBuyerName, txtOrderDate, txtStatusId, txtStatusName, txtContactEmail, txtContactPhone;
        private RoundedButton btnClose;
        private CustomDataGridView orederDeteilGrid, paymentsDeteilGrid;
        private HttpClient httpClient;

        public OrderDetailsForm(Order order, HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            DisplayOrderDetails(order);
        }
        private async void DisplayOrderDetails(Order order)
        {
            List<Payments> response2 = new List<Payments>();
            Buyer response3 = new Buyer();
            List<OrdersDeteils> response1 = new List<OrdersDeteils>();
            try
            {
                response2 = await httpClient.GetFromJsonAsync<List<Payments>>($"https://localhost:7287/api/Payment/order/{order.OrderId}");
                response3 = await httpClient.GetFromJsonAsync<Buyer>($"https://localhost:7287/api/Buyer/{order.BuyerId}");
                response1 = await httpClient.GetFromJsonAsync<List<OrdersDeteils>>($"https://localhost:7287/api/OrderDetail/order/{order.OrderId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if(response1 != null)
                orederDeteilGrid.SetDataOrdersDet(response1);
            if (response3 != null)
            {
                txtContactPhone.TextBoxText = response3.contactPhone;
                txtContactEmail.TextBoxText = response3.contactEmail;
            }
            var pays = new List<Payments>();
            if(response2 != null)
            {
                foreach (var item in response2)
                {
                    var response5 = await httpClient.GetFromJsonAsync<PaymentMethod>($"https://localhost:7287/api/PaymentMethod/{item.PaymentMethodId}");
                    var pay = new Payments()
                    { 
                        PaymentId = item.PaymentId,
                        OrderId = item.OrderId,
                        PaymentMethodId = item.PaymentMethodId,
                        PaymentMethodName = response5.MethodName,
                        Amount = item.Amount,
                        PaymentDate = item.PaymentDate
                    };
                    pays.Add(pay);
                }
                paymentsDeteilGrid.SetDataPayments(pays);
            }

            txtOrderId.TextBoxText = order.OrderId.ToString();
            txtBuyerId.TextBoxText = order.BuyerId.ToString();
            txtBuyerName.TextBoxText = order.BuyerName;
            txtOrderDate.TextBoxText = order.OrderDate.ToString();
            txtStatusId.TextBoxText = order.StatusId.ToString();
            txtStatusName.TextBoxText = order.StatusName;
        }
    }
}
