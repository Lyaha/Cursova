using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    public partial class StockMovementDetailsForm : Form
    {
        private Label lblMovementId, lblProductId, lblProductName, lblMovementTypeId, lblMovementName, lblQuantity, lblMovementDate, lblBatchNumber, lblNotes;
        private RoundedTextBox txtMovementId, txtProductId, txtProductName, txtMovementTypeId, txtMovementName, txtQuantity, txtMovementDate, txtBatchNumber, txtNotes;
        private RoundedButton btnClose;

        public StockMovementDetailsForm(Stock stock)
        {
            InitializeComponent();
            DisplayStockDetails(stock);
        }

        private void DisplayStockDetails(Stock stock)
        {
            txtMovementId.TextBoxText = stock.MovementId.ToString();
            txtProductId.TextBoxText = stock.ProductId.ToString();
            txtProductName.TextBoxText = stock.ProductName;
            txtMovementTypeId.TextBoxText = stock.MovementTypeId.ToString();
            txtMovementName.TextBoxText = stock.MovementName;
            txtQuantity.TextBoxText = stock.Quantity.ToString();
            txtMovementDate.TextBoxText = stock.MovementDate.ToString();
            txtBatchNumber.TextBoxText = stock.BatchNumber;
            txtNotes.TextBoxText = stock.Notes;
        }
    }
}
