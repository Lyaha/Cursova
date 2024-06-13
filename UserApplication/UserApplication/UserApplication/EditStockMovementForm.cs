using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace UserApplication
{
    public partial class EditStockMovementForm : Form
    {
        private Stock stock1;
        private HttpClient httpClient;
        private bool Edit;
        public EditStockMovementForm(HttpClient httpClients, Stock stock, bool edit)
        {
            stock1 = new Stock();
            stock1 = stock;
            httpClient = httpClients;
            InitializeComponent();
            Edit = edit;
            if (Edit)
                LoadInfo();
            else
                this.Text = "Add Stock Movement";
        }
        private void LoadInfo()
        {
            this.textBoxProductId.TextBoxText = stock1.ProductId.ToString();
            this.textBoxMovementTypeId.TextBoxText = stock1.MovementTypeId.ToString();
            this.textBoxQuantity.TextBoxText = stock1.Quantity.ToString();
            this.textBoxBatchNumber.TextBoxText = stock1.BatchNumber.ToString();
            this.textBoxNotes.TextBoxText = stock1.Notes.ToString();
            this.dateTimePickerMovementDate.Value = stock1.MovementDate;
        }
        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (Edit)
            {
                try
                {
                    int ProductID = ConvertTry("Error(check ProductID(int: 123)): ", this.textBoxProductId);
                    int MovementTypeID = ConvertTry("Error(check Movement Type Id(int: 123)): ", this.textBoxMovementTypeId);
                    int QuantitY = ConvertTry("Error(check Quantity(int: 123)): ", this.textBoxQuantity);
                    var newstock = new Stock()
                    {
                        ProductId = ProductID,
                        MovementTypeId = MovementTypeID,
                        Quantity = QuantitY,
                        MovementDate = this.dateTimePickerMovementDate.Value,
                        BatchNumber = this.textBoxBatchNumber.TextBoxText,
                        Notes = this.textBoxNotes.TextBoxText
                    };

                    var response2 = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/StockMovements/{stock1.MovementId}", new { ProductId = newstock.ProductId, MovementTypeId = newstock.MovementTypeId, Quantity = newstock.Quantity, MovementDate = newstock.MovementDate, BatchNumber = newstock.BatchNumber, Notes = newstock.Notes });
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    int ProductID = ConvertTry("Error(check ProductID(int: 123)): ", this.textBoxProductId);
                    int MovementTypeID = ConvertTry("Error(check Movement Type Id(int: 123)): ", this.textBoxMovementTypeId);
                    int QuantitY = ConvertTry("Error(check Quantity(int: 123)): ", this.textBoxQuantity);

                    if (ProductID == 0)
                    {
                        MessageBox.Show("ProductID is required.");
                        return;
                    }

                    if (MovementTypeID == 0)
                    {
                        MessageBox.Show("Quantity is required.");
                        return;
                    }

                    if (MovementTypeID == 0)
                    {
                        MessageBox.Show("MovementTypeID is required.");
                        return;
                    }

                    if (string.IsNullOrEmpty(this.textBoxBatchNumber.TextBoxText))
                    {
                        MessageBox.Show("Please enter BatchNumber.");
                        return;
                    }
                    if (string.IsNullOrEmpty(this.textBoxNotes.TextBoxText))
                    {
                        MessageBox.Show("Please enter Notes.");
                        return;
                    }
                    var newstock = new Stock()
                    {
                        ProductId = ProductID,
                        MovementTypeId = MovementTypeID,
                        Quantity = QuantitY,
                        MovementDate = this.dateTimePickerMovementDate.Value,
                        BatchNumber = this.textBoxBatchNumber.TextBoxText,
                        Notes = this.textBoxNotes.TextBoxText
                    };

                    var response2 = await httpClient.PostAsJsonAsync($"https://localhost:7287/api/StockMovements", new { ProductId = newstock.ProductId, MovementTypeId = newstock.MovementTypeId, Quantity = newstock.Quantity, MovementDate = newstock.MovementDate, BatchNumber = newstock.BatchNumber, Notes = newstock.Notes });
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
        }

        private int ConvertTry(string text, RoundedTextBox box)
        {
            int item;
            try
            {
                item = Convert.ToInt32(box.TextBoxText);
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(text + ex);
                return item = 0;
            }
        }

    }
}
