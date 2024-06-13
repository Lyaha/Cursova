using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class OrderDetailsForm
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
            this.Text = "Order Details";
            this.Size = new System.Drawing.Size(890, 450);

            lblOrderId = new Label() { Text = "Order ID", Top = 30, Left = 20 };
            txtOrderId = new RoundedTextBox() { Top = 20, Left = 150, Width = 200};
            txtOrderId.textBox.ReadOnly = true;

            

            lblOrderDate = new Label() { Text = "Order Date", Top = 60, Left = 20 };
            txtOrderDate = new RoundedTextBox() { Top = 50, Left = 150, Width = 200 };
            txtOrderDate.textBox.ReadOnly = true;

            lblStatusId = new Label() { Text = "Status ID", Top = 90, Left = 20 };
            txtStatusId = new RoundedTextBox() { Top = 80, Left = 150, Width = 200 };
            txtStatusId.textBox.ReadOnly = true;

            lblStatusName = new Label() { Text = "Status Name", Top = 120, Left = 20 };
            txtStatusName = new RoundedTextBox() { Top = 110, Left = 150, Width = 200 };
            txtStatusName.textBox.ReadOnly = true;

            lableH1Buyer = new Label() { Top = 150, Left = 20, Font = new Font("Arial", 12, FontStyle.Bold), Text = "Buyer" };

            lblBuyerId = new Label() { Text = "Buyer ID", Top = 180, Left = 20 };
            txtBuyerId = new RoundedTextBox() { Top = 170, Left = 150, Width = 200 };
            txtBuyerId.textBox.ReadOnly = true;

            lblBuyerName = new Label() { Text = "Buyer Name", Top = 210, Left = 20 };
            txtBuyerName = new RoundedTextBox() { Top = 200, Left = 150, Width = 200 };
            txtBuyerName.textBox.ReadOnly = true;

            lblContactPhone = new Label() { Text = "Contact phone", Top = 240, Left = 20 };
            txtContactPhone = new RoundedTextBox() { Top = 230, Left = 150, Width = 200 };
            txtContactPhone.textBox.ReadOnly = true;

            lblContactEmail = new Label() { Text = "Contact email", Top = 270, Left = 20 };
            txtContactEmail = new RoundedTextBox() { Top = 260, Left = 150, Width = 200 };
            txtContactEmail.textBox.ReadOnly = true;

            labelH1OrderDeteil = new Label() { Top = 10, Left = 370, Font = new Font("Arial", 12, FontStyle.Bold), Text = "Order detail" };
            orederDeteilGrid = new CustomDataGridView(httpClient,"","","","") { Top = 35, Left = 370, Width =500, Height = 160};


            LabelH1Payments = new Label() { Top = 195, Left = 370, Font = new Font("Arial", 12, FontStyle.Bold), Text = "Payments"};
            var pay = new Payments();
            paymentsDeteilGrid = new CustomDataGridView(pay, httpClient) { Top = 225, Left = 370, Width = 500, Height = 160 };

            btnClose = new RoundedButton() { Text = "Close", Top = 350, Left = 150, BackColor = Color.IndianRed, ForeColor = Color.White };
            btnClose.Click += (sender, e) => this.Close();

            this.Controls.Add(lblOrderId);
            this.Controls.Add(txtOrderId);
            this.Controls.Add(lblBuyerId);
            this.Controls.Add(txtBuyerId);
            this.Controls.Add(lblBuyerName);
            this.Controls.Add(txtBuyerName);
            this.Controls.Add(lblOrderDate);
            this.Controls.Add(txtOrderDate);
            this.Controls.Add(lblStatusId);
            this.Controls.Add(txtStatusId);
            this.Controls.Add(lblStatusName);
            this.Controls.Add(txtStatusName);
            this.Controls.Add(lableH1Buyer);
            this.Controls.Add(lblContactPhone);
            this.Controls.Add(txtContactPhone);
            this.Controls.Add(lblContactEmail);
            this.Controls.Add(txtContactEmail);
            this.Controls.Add(labelH1OrderDeteil);
            this.Controls.Add(orederDeteilGrid);
            this.Controls.Add(LabelH1Payments);
            this.Controls.Add(paymentsDeteilGrid);
            this.Controls.Add(btnClose);
        }

        #endregion
    }
}