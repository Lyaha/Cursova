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
    public partial class EditOrderForm : Form
    {
        private RoundedTextBox txtOrderId, txtOrderDate, txtStatusId, txtBuyerId, txtContactPhone, txtContactEmail, txtBuyerName;
        private RoundedButton buttonSave, deleteOrderDetailsButton, deletePaymentsButton;
        private Label labelOrderId, labelOrderDate, labelStatusId, labelBuyerId, labelContactPhone, labelContactEmail, labelByerName;
        private HttpClient httpClient;
        private Order ord;
        private Order responseOrder;
        private Buyer responseBuyer;
        private List<OrdersDeteils> responseOrderDetails;
        private List<Payments> responsePayments;
        private bool edit1;
        public EditOrderForm(Order order, bool edit, HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            responseOrderDetails = new List<OrdersDeteils>();
            responsePayments = new List<Payments>();
            ord = order;
            edit1 = edit;
            if(edit)
                LoadOrderData();
            else
            {
                this.Text = "Add new Order";
                this.buttonSave.Text = "Add";
                var order1 = new OrdersDeteils();
                responseOrderDetails.Add(order1);
                var pay = new Payments();
                responsePayments.Add(pay);
                this.OrderDeteilsGrid.DataSource = responseOrderDetails;
                this.PaymentsGrid.DataSource = responsePayments;
            }
        }
        private async void LoadOrderData()
        {
            try
            {
                responseOrder = await httpClient.GetFromJsonAsync<Order>($"https://localhost:7287/api/Order/{ord.OrderId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                responseBuyer = await httpClient.GetFromJsonAsync<Buyer>($"https://localhost:7287/api/Buyer/{responseOrder.BuyerId}");
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                responseOrderDetails = await httpClient.GetFromJsonAsync<List<OrdersDeteils>>($"https://localhost:7287/api/OrderDetail/order/{ord.OrderId}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                responsePayments = await httpClient.GetFromJsonAsync<List<Payments>>($"https://localhost:7287/api/Payment/order/{ord.OrderId}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

                if (responseOrder != null)
                {
                    txtOrderId.TextBoxText = responseOrder.OrderId.ToString();
                    txtOrderDate.TextBoxText = responseOrder.OrderDate.ToString();
                    txtStatusId.TextBoxText = responseOrder.StatusId.ToString();
                }

                if (responseBuyer != null)
                {
                    txtBuyerId.TextBoxText = responseBuyer.idBuyer.ToString();
                    txtBuyerName.TextBoxText = responseBuyer.name;
                    txtContactPhone.TextBoxText = responseBuyer.contactPhone;
                    txtContactEmail.TextBoxText = responseBuyer.contactEmail;
                }

                if (responseOrderDetails != null)
                {
                    responseOrderDetails.Add(new OrdersDeteils() { OrderId = ord.OrderId });
                    OrderDeteilsGrid.DataSource = responseOrderDetails;
                }

                if (responsePayments != null)
                {
                    responsePayments.Add(new Payments() { OrderId = ord.OrderId });
                    PaymentsGrid.DataSource = responsePayments;
                }
            
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (edit1)
            {
                if (string.IsNullOrEmpty(txtOrderDate.TextBoxText) ||
                    string.IsNullOrEmpty(txtStatusId.TextBoxText) ||
                    string.IsNullOrEmpty(txtBuyerId.TextBoxText) ||
                    string.IsNullOrEmpty(txtBuyerName.TextBoxText) ||
                    string.IsNullOrEmpty(txtContactPhone.TextBoxText) ||
                    string.IsNullOrEmpty(txtContactEmail.TextBoxText))
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                    return;
                }

                try
                {
                    int buyerId;
                    bool isNewBuyer = string.IsNullOrEmpty(txtBuyerId.TextBoxText) || txtBuyerId.TextBoxText.ToLower() == "new";

                    if (isNewBuyer)
                    {
                        var newBuyer = new Buyer
                        {
                            name = txtBuyerName.TextBoxText,
                            contactPhone = txtContactPhone.TextBoxText,
                            contactEmail = txtContactEmail.TextBoxText
                        };

                        var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Buyer", new { Name = newBuyer.name, ContactPhone = newBuyer.contactPhone, ContactEmail = newBuyer.contactEmail });
                        if (response.IsSuccessStatusCode)
                        {
                            var createdBuyer = await response.Content.ReadFromJsonAsync<Buyer>();
                            txtBuyerId.TextBoxText = createdBuyer.idBuyer.ToString();
                            buyerId = createdBuyer.idBuyer;
                        }
                        else
                        {
                            MessageBox.Show("Error create new Buyer");
                            return;
                        }
                    }
                    else if (int.TryParse(txtBuyerId.TextBoxText, out buyerId))
                    {
                        var response = await httpClient.GetAsync($"https://localhost:7287/api/Buyer/{buyerId}");
                        if (response.IsSuccessStatusCode)
                        {
                            var updatedBuyer = new Buyer
                            {
                                idBuyer = buyerId,
                                name = txtBuyerName.TextBoxText,
                                contactPhone = txtContactPhone.TextBoxText,
                                contactEmail = txtContactEmail.TextBoxText
                            };
                            await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Buyer/{buyerId}", new { idBuyer = updatedBuyer.idBuyer, name = updatedBuyer.name, contactPhone = updatedBuyer.contactPhone, contactEmail = updatedBuyer.contactEmail });
                        }
                        else
                        {
                            txtBuyerName.TextBoxText = "NotFound";
                            txtContactPhone.TextBoxText = "NotFound";
                            txtContactEmail.TextBoxText = "NotFound";
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error Buyer ID");
                        return;
                    }

                    var updatedOrder = new Order
                    {
                        OrderId = ord.OrderId,
                        OrderDate = DateTime.Parse(txtOrderDate.TextBoxText),
                        StatusId = int.Parse(txtStatusId.TextBoxText),
                        BuyerId = buyerId
                    };

                    await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Order/{ord.OrderId}", new { BuyerId = updatedOrder.BuyerId, OrderDate = updatedOrder.OrderDate, StatusId = updatedOrder.StatusId });

                    foreach (var detail in responseOrderDetails)
                    {
                        if (detail.OrderDetailId == -1)
                        {
                            detail.OrderId = ord.OrderId;
                            try
                            {
                                await httpClient.PostAsJsonAsync("https://localhost:7287/api/OrderDetail", new { OrderId = ord.OrderId, ProductId = detail.ProductId, Quantity = detail.Quantity, UnitPrice = detail.UnitPrice });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (detail.OrderDetailId != 0)
                        {
                            try
                            {
                                await httpClient.PutAsJsonAsync($"https://localhost:7287/api/OrderDetail/{detail.OrderDetailId}", new { OrderId = ord.OrderId, ProductId = detail.ProductId, Quantity = detail.Quantity, UnitPrice = detail.UnitPrice });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    foreach (var payment in responsePayments)
                    {
                        if (payment.PaymentId == -1)
                        {
                            payment.OrderId = ord.OrderId;
                            try
                            {
                                await httpClient.PostAsJsonAsync("https://localhost:7287/api/Payment", new { OrderId = ord.OrderId, PaymentMethodId = payment.PaymentMethodId, Amount = payment.Amount, PaymentDate = payment.PaymentDate });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (payment.PaymentId != 0)
                        {
                            try
                            {
                                await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Payment/{payment.PaymentId}", new { OrderId = ord.OrderId, PaymentMethodId = payment.PaymentMethodId, Amount = payment.Amount, PaymentDate = payment.PaymentDate });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }

                    MessageBox.Show("Data Saved");
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtOrderDate.TextBoxText) ||
                       string.IsNullOrEmpty(txtStatusId.TextBoxText) ||
                       string.IsNullOrEmpty(txtBuyerId.TextBoxText) ||
                       string.IsNullOrEmpty(txtBuyerName.TextBoxText) ||
                       string.IsNullOrEmpty(txtContactPhone.TextBoxText) ||
                       string.IsNullOrEmpty(txtContactEmail.TextBoxText))
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                    return;
                }
                try
                {
                    int buyerId;
                    bool isNewBuyer = string.IsNullOrEmpty(txtBuyerId.TextBoxText) || txtBuyerId.TextBoxText.ToLower() == "new";

                    if (isNewBuyer)
                    {
                        var newBuyer = new Buyer
                        {
                            name = txtBuyerName.TextBoxText,
                            contactPhone = txtContactPhone.TextBoxText,
                            contactEmail = txtContactEmail.TextBoxText
                        };

                        var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Buyer", new { Name = newBuyer.name, ContactPhone = newBuyer.contactPhone, ContactEmail = newBuyer.contactEmail });
                        if (response.IsSuccessStatusCode)
                        {
                            var createdBuyer = await response.Content.ReadFromJsonAsync<Buyer>();
                            txtBuyerId.TextBoxText = createdBuyer.idBuyer.ToString();
                            buyerId = createdBuyer.idBuyer;
                        }
                        else
                        {
                            MessageBox.Show("Error create new Buyer");
                            return;
                        }
                    }
                    else if (int.TryParse(txtBuyerId.TextBoxText, out buyerId))
                    {
                        var response = await httpClient.GetAsync($"https://localhost:7287/api/Buyer/{buyerId}");
                        if (response.IsSuccessStatusCode)
                        {
                            var updatedBuyer = new Buyer
                            {
                                idBuyer = buyerId,
                                name = txtBuyerName.TextBoxText,
                                contactPhone = txtContactPhone.TextBoxText,
                                contactEmail = txtContactEmail.TextBoxText
                            };
                            await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Buyer/{buyerId}", new { idBuyer = updatedBuyer.idBuyer, name = updatedBuyer.name, contactPhone = updatedBuyer.contactPhone, contactEmail = updatedBuyer.contactEmail });
                        }
                        else
                        {
                            txtBuyerName.TextBoxText = "NotFound";
                            txtContactPhone.TextBoxText = "NotFound";
                            txtContactEmail.TextBoxText = "NotFound";
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error Buyer ID");
                        return;
                    }

                    var AddOrder = new Order
                    {
                        OrderDate = DateTime.Parse(txtOrderDate.TextBoxText),
                        StatusId = int.Parse(txtStatusId.TextBoxText),
                        BuyerId = buyerId
                    };

                    var responSe = await httpClient.PostAsJsonAsync($"https://localhost:7287/api/Order", new { BuyerId = AddOrder.BuyerId, OrderDate = AddOrder.OrderDate, StatusId = AddOrder.StatusId });
                    ord = await responSe.Content.ReadFromJsonAsync<Order>();

                    foreach (var detail in responseOrderDetails)
                    {
                        if (detail.OrderDetailId == -1)
                        {
                            detail.OrderId = ord.OrderId;
                            try
                            {
                                await httpClient.PostAsJsonAsync("https://localhost:7287/api/OrderDetail", new { OrderId = ord.OrderId, ProductId = detail.ProductId, Quantity = detail.Quantity, UnitPrice = detail.UnitPrice });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    foreach (var payment in responsePayments)
                    {
                        if (payment.PaymentId == -1)
                        {
                            payment.OrderId = ord.OrderId;
                            try
                            {
                                await httpClient.PostAsJsonAsync("https://localhost:7287/api/Payment", new { OrderId = ord.OrderId, PaymentMethodId = payment.PaymentMethodId, Amount = payment.Amount, PaymentDate = payment.PaymentDate });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }

                    MessageBox.Show("Data Create");
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
        }


        private async void txtBuyerId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtBuyerId.TextBoxText, out int buyerId))
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://localhost:7287/api/Buyer/{buyerId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var buyer = await response.Content.ReadFromJsonAsync<Buyer>();
                        txtBuyerName.TextBoxText = buyer.name;
                        txtContactPhone.TextBoxText = buyer.contactPhone;
                        txtContactEmail.TextBoxText = buyer.contactEmail;
                    }
                    else
                    {
                        txtBuyerName.TextBoxText = "NotFound";
                        txtContactPhone.TextBoxText = "NotFound";
                        txtContactEmail.TextBoxText = "NotFound";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                txtBuyerName.TextBoxText = "";
                txtContactPhone.TextBoxText = "";
                txtContactEmail.TextBoxText = "";
            }
        }
        private async void buttonDeleteOrderDetails_Click(object sender, EventArgs e)
        {
            if (OrderDeteilsGrid.SelectedRows.Count > 0)
            {
                var selectedRow = OrderDeteilsGrid.SelectedRows[0];
                var detail = selectedRow.DataBoundItem as OrdersDeteils;

                if ((detail != null && detail.OrderDetailId != 0) || (detail != null && detail.OrderDetailId != -1))
                {
                    try
                    {
                        var response = await httpClient.DeleteAsync($"https://localhost:7287/api/OrderDetail/{detail.OrderDetailId}");
                        if (response.IsSuccessStatusCode)
                        {
                            responseOrderDetails.Remove(detail);
                            OrderDeteilsGrid.DataSource = null;
                            OrderDeteilsGrid.DataSource = responseOrderDetails;
                            MessageBox.Show("Delete success");
                        }
                        else
                        {
                            MessageBox.Show("Error delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (detail.OrderDetailId == 0 || detail.OrderDetailId == -1)
                {
                    responseOrderDetails.Remove(detail);
                    OrderDeteilsGrid.DataSource = null;
                    OrderDeteilsGrid.DataSource = responseOrderDetails;
                    MessageBox.Show("New row delete");
                }
            }
            else
            {
                MessageBox.Show("U need select row");
            }
        }

        private async void buttonDeletePayments_Click(object sender, EventArgs e)
        {
            if (PaymentsGrid.SelectedRows.Count > 0)
            {
                var selectedRow = PaymentsGrid.SelectedRows[0];
                var payments = selectedRow.DataBoundItem as Payments;

                if (payments != null && payments.PaymentId != 0 && payments.PaymentId != -1)
                {
                    try
                    {
                        var response = await httpClient.DeleteAsync($"https://localhost:7287/api/Payment/{payments.PaymentId}");
                        if (response.IsSuccessStatusCode)
                        {
                            responsePayments.Remove(payments);
                            PaymentsGrid.DataSource = null;
                            PaymentsGrid.DataSource = responsePayments;
                            MessageBox.Show("Delete success");
                        }
                        else
                        {
                            MessageBox.Show("Error delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (payments.PaymentId == 0 || payments.PaymentId == -1)
                {
                    responsePayments.Remove(payments);
                    PaymentsGrid.DataSource = null;
                    PaymentsGrid.DataSource = responsePayments;
                    MessageBox.Show("New row delete");
                }
            }
            else
            {
                MessageBox.Show("U need select row");
            }
        }

        private void orderDetailsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex == responseOrderDetails.Count - 1)
            {
                var detail = responseOrderDetails[e.RowIndex];
                if (detail.OrderDetailId != 0)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        responseOrderDetails.Add(new OrdersDeteils() { OrderId = ord.OrderId });
                        OrderDeteilsGrid.DataSource = null;
                        OrderDeteilsGrid.DataSource = responseOrderDetails;
                        OrderDeteilsGrid.ClearSelection();
                        OrderDeteilsGrid.Rows[OrderDeteilsGrid.Rows.Count - 1].Selected = false;
                    }));
                }
            }
        }

        private void PaymentsDetailsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex == responsePayments.Count - 1)
            {
                var detail = responsePayments[e.RowIndex];
                if (detail.PaymentId == -1)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        responsePayments.Add(new Payments() { OrderId = ord.OrderId });
                        PaymentsGrid.DataSource = null;
                        PaymentsGrid.DataSource = responsePayments;
                        PaymentsGrid.ClearSelection();
                        PaymentsGrid.Rows[PaymentsGrid.Rows.Count - 1].Selected = false;
                    }));
                }
            }
        }
    }
}
