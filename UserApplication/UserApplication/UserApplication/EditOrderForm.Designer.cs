using System.Drawing;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class EditOrderForm
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
            this.labelOrderId = new System.Windows.Forms.Label();
            this.labelOrderDate = new System.Windows.Forms.Label();
            this.labelStatusId = new System.Windows.Forms.Label();
            this.labelBuyerId = new System.Windows.Forms.Label();
            this.labelByerName = new System.Windows.Forms.Label();
            this.labelContactPhone = new System.Windows.Forms.Label();
            this.labelContactEmail = new System.Windows.Forms.Label();
            this.productHeaderLabel = new System.Windows.Forms.Label();
            this.OrderDeteilsGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.PaymentsGrid = new System.Windows.Forms.DataGridView();
            this.txtOrderId = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtOrderDate = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtStatusId = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtBuyerId = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtBuyerName = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtContactPhone = new UserApplication.FormElementClasses.RoundedTextBox();
            this.txtContactEmail = new UserApplication.FormElementClasses.RoundedTextBox();
            this.deleteOrderDetailsButton = new RoundedButton();
            this.deletePaymentsButton = new RoundedButton();
            this.buttonSave = new UserApplication.FormElementClasses.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDeteilsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // labelOrderId
            // 
            this.labelOrderId.Location = new System.Drawing.Point(12, 12);
            this.labelOrderId.Name = "labelOrderId";
            this.labelOrderId.Size = new System.Drawing.Size(200, 20);
            this.labelOrderId.TabIndex = 0;
            this.labelOrderId.Text = "Order ID:";
            // 
            // labelOrderDate
            // 
            this.labelOrderDate.Location = new System.Drawing.Point(12, 58);
            this.labelOrderDate.Name = "labelOrderDate";
            this.labelOrderDate.Size = new System.Drawing.Size(200, 20);
            this.labelOrderDate.TabIndex = 1;
            this.labelOrderDate.Text = "Order Date:";
            // 
            // labelStatusId
            // 
            this.labelStatusId.Location = new System.Drawing.Point(12, 104);
            this.labelStatusId.Name = "labelStatusId";
            this.labelStatusId.Size = new System.Drawing.Size(200, 20);
            this.labelStatusId.TabIndex = 2;
            this.labelStatusId.Text = "Status ID:";
            // 
            // labelBuyerId
            // 
            this.labelBuyerId.Location = new System.Drawing.Point(12, 150);
            this.labelBuyerId.Name = "labelBuyerId";
            this.labelBuyerId.Size = new System.Drawing.Size(200, 20);
            this.labelBuyerId.TabIndex = 3;
            this.labelBuyerId.Text = "Buyer ID:";
            // 
            // labelByerName
            // 
            this.labelByerName.Location = new System.Drawing.Point(12, 196);
            this.labelByerName.Name = "labelByerName";
            this.labelByerName.Size = new System.Drawing.Size(200, 20);
            this.labelByerName.TabIndex = 3;
            this.labelByerName.Text = "Buyer Name:";
            // 
            // labelContactPhone
            // 
            this.labelContactPhone.Location = new System.Drawing.Point(12, 242);
            this.labelContactPhone.Name = "labelContactPhone";
            this.labelContactPhone.Size = new System.Drawing.Size(200, 20);
            this.labelContactPhone.TabIndex = 4;
            this.labelContactPhone.Text = "Contact Phone:";
            // 
            // labelContactEmail
            // 
            this.labelContactEmail.Location = new System.Drawing.Point(12, 288);
            this.labelContactEmail.Name = "labelContactEmail";
            this.labelContactEmail.Size = new System.Drawing.Size(200, 20);
            this.labelContactEmail.TabIndex = 5;
            this.labelContactEmail.Text = "Contact Email:";
            // 
            // OrderDetailsHeaderLabel
            // 
            this.productHeaderLabel.BackColor = System.Drawing.Color.Orange;
            this.productHeaderLabel.Location = new System.Drawing.Point(244, 12);
            this.productHeaderLabel.Name = "OrderDetailsHeaderLabel";
            this.productHeaderLabel.Size = new System.Drawing.Size(543, 25);
            this.productHeaderLabel.TabIndex = 14;
            this.productHeaderLabel.Text = "Order Details";
            this.productHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderDeteilsGrid
            // 
            this.OrderDeteilsGrid.Location = new System.Drawing.Point(244, 37);
            this.OrderDeteilsGrid.Name = "OrderDeteilsGrid";
            this.OrderDeteilsGrid.Size = new System.Drawing.Size(543, 107);
            this.OrderDeteilsGrid.TabIndex = 15;
            this.OrderDeteilsGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderDetailsGrid_CellEndEdit);

            this.deleteOrderDetailsButton.Location = new Point(244,144);
            this.deleteOrderDetailsButton.Size = new Size(543, 25);
            this.deleteOrderDetailsButton.Text = "Delete selected row";
            this.deleteOrderDetailsButton.BackColor = Color.Red;
            this.deleteOrderDetailsButton.ForeColor = Color.White;
            this.deleteOrderDetailsButton.Click += buttonDeleteOrderDetails_Click;

            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(244, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(644, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Payments";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PaymentsGrid
            // 
            this.PaymentsGrid.Location = new System.Drawing.Point(244, 200);
            this.PaymentsGrid.Name = "PaymentsGrid";
            this.PaymentsGrid.Size = new System.Drawing.Size(644, 107);
            this.PaymentsGrid.TabIndex = 17;
            this.PaymentsGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentsDetailsGrid_CellEndEdit);

            this.deletePaymentsButton.Location = new Point(244, 307);
            this.deletePaymentsButton.Size = new Size(644, 25);
            this.deletePaymentsButton.Text = "Delete selected row";
            this.deletePaymentsButton.BackColor = Color.Red;
            this.deletePaymentsButton.ForeColor = Color.White;
            this.deletePaymentsButton.Click += buttonDeletePayments_Click;
            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(12, 32);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(200, 20);
            this.txtOrderId.TabIndex = 0;
            this.txtOrderId.TextBoxText = "";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(12, 78);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(200, 20);
            this.txtOrderDate.TabIndex = 1;
            this.txtOrderDate.TextBoxText = "";
            // 
            // txtStatusId
            // 
            this.txtStatusId.Location = new System.Drawing.Point(12, 124);
            this.txtStatusId.Name = "txtStatusId";
            this.txtStatusId.Size = new System.Drawing.Size(200, 20);
            this.txtStatusId.TabIndex = 2;
            this.txtStatusId.TextBoxText = "";
            // 
            // txtBuyerId
            // 
            this.txtBuyerId.Location = new System.Drawing.Point(12, 173);
            this.txtBuyerId.Name = "txtBuyerId";
            this.txtBuyerId.Size = new System.Drawing.Size(200, 20);
            this.txtBuyerId.TabIndex = 3;
            this.txtBuyerId.TextBoxText = "";
            this.txtBuyerId.textBox.TextChanged += new System.EventHandler(this.txtBuyerId_TextChanged);
            // 
            // txtBuyerName
            // 
            this.txtBuyerName.Location = new System.Drawing.Point(12, 219);
            this.txtBuyerName.Name = "txtBuyerName";
            this.txtBuyerName.Size = new System.Drawing.Size(200, 20);
            this.txtBuyerName.TabIndex = 3;
            this.txtBuyerName.TextBoxText = "";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(12, 262);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(200, 20);
            this.txtContactPhone.TabIndex = 4;
            this.txtContactPhone.TextBoxText = "";
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(12, 308);
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(200, 20);
            this.txtContactEmail.TabIndex = 5;
            this.txtContactEmail.TextBoxText = "";
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.LightSeaGreen;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Icon = null;
            this.buttonSave.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.IconPadding = 5;
            this.buttonSave.Location = new System.Drawing.Point(208, 340);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Radius = 10;
            this.buttonSave.Size = new System.Drawing.Size(200, 40);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += buttonSave_Click;
            // 
            // EditOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 395);
            this.Controls.Add(this.PaymentsGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderDeteilsGrid);
            this.Controls.Add(this.productHeaderLabel);
            this.Controls.Add(this.labelOrderId);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.labelOrderDate);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.deleteOrderDetailsButton);
            this.Controls.Add(this.deletePaymentsButton);
            this.Controls.Add(this.labelStatusId);
            this.Controls.Add(this.txtStatusId);
            this.Controls.Add(this.labelBuyerId);
            this.Controls.Add(this.txtBuyerId);
            this.Controls.Add(this.labelByerName);
            this.Controls.Add(this.txtBuyerName);
            this.Controls.Add(this.labelContactPhone);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.labelContactEmail);
            this.Controls.Add(this.txtContactEmail);
            this.Controls.Add(this.buttonSave);
            this.Name = "EditOrderForm";
            this.Text = "Редактирование заказа";
            ((System.ComponentModel.ISupportInitialize)(this.OrderDeteilsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label productHeaderLabel;
        private DataGridView OrderDeteilsGrid;
        private Label label1;
        private DataGridView PaymentsGrid;
    }
}