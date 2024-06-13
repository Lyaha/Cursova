using System.Drawing;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class StockMovementDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Stock Movement Details";
            this.Size = new System.Drawing.Size(400, 450);

            lblMovementId = new Label() { Text = "Movement ID", Top = 30, Left = 20 };
            txtMovementId = new RoundedTextBox() { Top = 20, Left = 150, Width = 200};
            txtMovementId.textBox.ReadOnly = true;

            lblProductId = new Label() { Text = "Product ID", Top = 60, Left = 20 };
            txtProductId = new RoundedTextBox() { Top = 50, Left = 150, Width = 200 };
            txtProductId.textBox.ReadOnly = true;

            lblProductName = new Label() { Text = "Product Name", Top = 90, Left = 20 };
            txtProductName = new RoundedTextBox() { Top = 80, Left = 150, Width = 200};
            txtProductName.textBox.ReadOnly = true;

            lblMovementTypeId = new Label() { Text = "Movement Type ID", Top = 120, Left = 20 };
            txtMovementTypeId = new RoundedTextBox() { Top = 110, Left = 150, Width = 200};
            txtMovementTypeId.textBox.ReadOnly = true;

            lblMovementName = new Label() { Text = "Movement Name", Top = 150, Left = 20 };
            txtMovementName = new RoundedTextBox() { Top = 140, Left = 150, Width = 200 };
            txtMovementName.textBox.ReadOnly = true;

            lblQuantity = new Label() { Text = "Quantity", Top = 180, Left = 20 };
            txtQuantity = new RoundedTextBox() { Top = 170, Left = 150, Width = 200 };
            txtQuantity.textBox.ReadOnly = true;

            lblMovementDate = new Label() { Text = "Movement Date", Top = 210, Left = 20 };
            txtMovementDate = new RoundedTextBox() { Top = 200, Left = 150, Width = 200 };
            txtMovementDate.textBox.ReadOnly = true;

            lblBatchNumber = new Label() { Text = "Batch Number", Top = 240, Left = 20 };
            txtBatchNumber = new RoundedTextBox() { Top = 230, Left = 150, Width = 200 };
            txtBatchNumber.textBox.ReadOnly = true;

            lblNotes = new Label() { Text = "Notes", Top = 270, Left = 20 };
            txtNotes = new RoundedTextBox() { Top = 260, Left = 150, Width = 200 };
            txtNotes.textBox.ReadOnly = true;

            btnClose = new RoundedButton() { Text = "Close", Top = 300, Left = 150 , BackColor = Color.IndianRed, ForeColor = Color.White};
            btnClose.Click += (sender, e) => this.Close();

            this.Controls.Add(lblMovementId);
            this.Controls.Add(txtMovementId);
            this.Controls.Add(lblProductId);
            this.Controls.Add(txtProductId);
            this.Controls.Add(lblProductName);
            this.Controls.Add(txtProductName);
            this.Controls.Add(lblMovementTypeId);
            this.Controls.Add(txtMovementTypeId);
            this.Controls.Add(lblMovementName);
            this.Controls.Add(txtMovementName);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(txtQuantity);
            this.Controls.Add(lblMovementDate);
            this.Controls.Add(txtMovementDate);
            this.Controls.Add(lblBatchNumber);
            this.Controls.Add(txtBatchNumber);
            this.Controls.Add(lblNotes);
            this.Controls.Add(txtNotes);
            this.Controls.Add(btnClose);
        }

        #endregion
    }
}