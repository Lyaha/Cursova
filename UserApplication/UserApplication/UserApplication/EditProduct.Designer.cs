using System.Drawing;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class EditProduct
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
            this.lblProductName = new UserApplication.FormElementClasses.RoundedTextBox();
            this.lblCategoryName = new UserApplication.FormElementClasses.RoundedTextBox();
            this.lblSupplierName = new UserApplication.FormElementClasses.RoundedTextBox();
            this.lblUnitPrice = new UserApplication.FormElementClasses.RoundedTextBox();
            this.lblQuantity = new UserApplication.FormElementClasses.RoundedTextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.productPricesDataGridView = new System.Windows.Forms.DataGridView();
            this.positionsDataGridView = new System.Windows.Forms.DataGridView();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCategory = new System.Windows.Forms.Label();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.productHeaderLabel = new System.Windows.Forms.Label();
            this.positionHeaderLabel = new System.Windows.Forms.Label();
            this.roundedButton1 = new UserApplication.FormElementClasses.RoundedButton();
            this.roundedButton2 = new UserApplication.FormElementClasses.RoundedButton();
            this.roundedButton3 = new UserApplication.FormElementClasses.RoundedButton();
            this.roundedButton4 = new UserApplication.FormElementClasses.RoundedButton();
            this.roundedButton5 = new UserApplication.FormElementClasses.RoundedButton();
            this.label1 = new System.Windows.Forms.Label();
            this.roundedTextBox1 = new UserApplication.FormElementClasses.RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productPricesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(154, 9);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(70, 20);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Name";
            this.lblProductName.TextBoxText = "";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.Location = new System.Drawing.Point(154, 32);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(70, 20);
            this.lblCategoryName.TabIndex = 1;
            this.lblCategoryName.Text = "Category";
            this.lblCategoryName.TextBoxText = "";
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.Location = new System.Drawing.Point(154, 55);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(70, 20);
            this.lblSupplierName.TabIndex = 2;
            this.lblSupplierName.Text = "Supplier";
            this.lblSupplierName.TextBoxText = "";
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.Location = new System.Drawing.Point(154, 78);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(70, 20);
            this.lblUnitPrice.TabIndex = 3;
            this.lblUnitPrice.Text = "Price";
            this.lblUnitPrice.TextBoxText = "";
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(154, 101);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(70, 20);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.TextBoxText = "";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(335, 9);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 100);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // productPricesDataGridView
            // 
            this.productPricesDataGridView.Location = new System.Drawing.Point(15, 200);
            this.productPricesDataGridView.Name = "productPricesDataGridView";
            this.productPricesDataGridView.Size = new System.Drawing.Size(600, 150);
            this.productPricesDataGridView.TabIndex = 6;
            // 
            // positionsDataGridView
            // 
            this.positionsDataGridView.Location = new System.Drawing.Point(15, 404);
            this.positionsDataGridView.Name = "positionsDataGridView";
            this.positionsDataGridView.Size = new System.Drawing.Size(600, 150);
            this.positionsDataGridView.TabIndex = 7;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 12);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Name";
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(12, 35);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(49, 13);
            this.labelCategory.TabIndex = 9;
            this.labelCategory.Text = "Category";
            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Location = new System.Drawing.Point(12, 58);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(45, 13);
            this.labelSupplier.TabIndex = 10;
            this.labelSupplier.Text = "Supplier";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(12, 82);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(31, 13);
            this.labelPrice.TabIndex = 11;
            this.labelPrice.Text = "Price";
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(12, 107);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(46, 13);
            this.labelQuantity.TabIndex = 12;
            this.labelQuantity.Text = "Quantity";
            // 
            // productHeaderLabel
            // 
            this.productHeaderLabel.BackColor = System.Drawing.Color.Orange;
            this.productHeaderLabel.Location = new System.Drawing.Point(15, 175);
            this.productHeaderLabel.Name = "productHeaderLabel";
            this.productHeaderLabel.Size = new System.Drawing.Size(600, 25);
            this.productHeaderLabel.TabIndex = 13;
            this.productHeaderLabel.Text = "Product";
            this.productHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // positionHeaderLabel
            // 
            this.positionHeaderLabel.BackColor = System.Drawing.Color.Orange;
            this.positionHeaderLabel.Location = new System.Drawing.Point(15, 379);
            this.positionHeaderLabel.Name = "positionHeaderLabel";
            this.positionHeaderLabel.Size = new System.Drawing.Size(600, 25);
            this.positionHeaderLabel.TabIndex = 14;
            this.positionHeaderLabel.Text = "Position";
            this.positionHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.LawnGreen;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.ForeColor = System.Drawing.Color.White;
            this.roundedButton1.Icon = null;
            this.roundedButton1.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roundedButton1.IconPadding = 5;
            this.roundedButton1.Location = new System.Drawing.Point(442, 9);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Radius = 10;
            this.roundedButton1.Size = new System.Drawing.Size(99, 23);
            this.roundedButton1.TabIndex = 15;
            this.roundedButton1.Text = "Upload new image";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += this.buttonUploadImage_Click;
            // 
            // roundedButton2
            // 
            this.roundedButton2.BackColor = System.Drawing.Color.IndianRed;
            this.roundedButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton2.ForeColor = System.Drawing.Color.White;
            this.roundedButton2.Icon = null;
            this.roundedButton2.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roundedButton2.IconPadding = 5;
            this.roundedButton2.Location = new System.Drawing.Point(442, 39);
            this.roundedButton2.Name = "roundedButton2";
            this.roundedButton2.Radius = 10;
            this.roundedButton2.Size = new System.Drawing.Size(99, 23);
            this.roundedButton2.TabIndex = 16;
            this.roundedButton2.Text = "Delete";
            this.roundedButton2.UseVisualStyleBackColor = false;
            this.roundedButton2.Click += this.buttonDeleteImage_Click;
            // 
            // roundedButton3
            // 
            this.roundedButton3.BackColor = System.Drawing.Color.IndianRed;
            this.roundedButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton3.ForeColor = System.Drawing.Color.White;
            this.roundedButton3.Icon = null;
            this.roundedButton3.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roundedButton3.IconPadding = 5;
            this.roundedButton3.Location = new System.Drawing.Point(15, 350);
            this.roundedButton3.Name = "roundedButton3";
            this.roundedButton3.Radius = 10;
            this.roundedButton3.Size = new System.Drawing.Size(600, 23);
            this.roundedButton3.TabIndex = 17;
            this.roundedButton3.Text = "Delete selected row";
            this.roundedButton3.UseVisualStyleBackColor = false;
            this.roundedButton3.Click += this.buttonDeleteProductPrice_Click;
            // 
            // roundedButton4
            // 
            this.roundedButton4.BackColor = System.Drawing.Color.IndianRed;
            this.roundedButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton4.ForeColor = System.Drawing.Color.White;
            this.roundedButton4.Icon = null;
            this.roundedButton4.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roundedButton4.IconPadding = 5;
            this.roundedButton4.Location = new System.Drawing.Point(14, 554);
            this.roundedButton4.Name = "roundedButton4";
            this.roundedButton4.Radius = 10;
            this.roundedButton4.Size = new System.Drawing.Size(600, 23);
            this.roundedButton4.TabIndex = 18;
            this.roundedButton4.Text = "Delete selected row";
            this.roundedButton4.UseVisualStyleBackColor = false;
            this.roundedButton4.Click += this.buttonDeletePosition_Click;
            // 
            // roundedButton5
            // 
            this.roundedButton5.BackColor = System.Drawing.Color.LawnGreen;
            this.roundedButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton5.ForeColor = System.Drawing.Color.White;
            this.roundedButton5.Icon = null;
            this.roundedButton5.IconAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roundedButton5.IconPadding = 5;
            this.roundedButton5.Location = new System.Drawing.Point(233, 595);
            this.roundedButton5.Name = "roundedButton5";
            this.roundedButton5.Radius = 10;
            this.roundedButton5.Size = new System.Drawing.Size(158, 23);
            this.roundedButton5.TabIndex = 19;
            this.roundedButton5.Text = "Save";
            this.roundedButton5.UseVisualStyleBackColor = false;
            this.roundedButton5.Click += this.SaveChanges;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Description";
            // 
            // roundedTextBox1
            // 
            this.roundedTextBox1.Location = new System.Drawing.Point(154, 127);
            this.roundedTextBox1.Name = "roundedTextBox1";
            this.roundedTextBox1.Size = new System.Drawing.Size(125, 20);
            this.roundedTextBox1.TabIndex = 20;
            this.roundedTextBox1.Text = "Description";
            this.roundedTextBox1.TextBoxText = "";
            // 
            // EditProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 630);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.roundedTextBox1);
            this.Controls.Add(this.roundedButton5);
            this.Controls.Add(this.roundedButton4);
            this.Controls.Add(this.roundedButton3);
            this.Controls.Add(this.roundedButton2);
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.labelSupplier);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.lblSupplierName);
            this.Controls.Add(this.lblUnitPrice);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.productHeaderLabel);
            this.Controls.Add(this.positionHeaderLabel);
            this.Controls.Add(this.productPricesDataGridView);
            this.Controls.Add(this.positionsDataGridView);
            this.Name = "EditProduct";
            this.Text = "EditProduct";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productPricesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FormElementClasses.RoundedButton roundedButton1;
        private FormElementClasses.RoundedButton roundedButton2;
        private RoundedTextBox lblProductName, lblCategoryName, lblSupplierName, lblUnitPrice, lblQuantity;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton5;
        private System.Windows.Forms.Label label1;
        private RoundedTextBox roundedTextBox1;
    }
}