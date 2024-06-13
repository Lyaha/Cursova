using System.Drawing;
using System.Windows.Forms;
using System;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class AddProductForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.productNameLabel = new Label();
            this.productNameTextBox = new RoundedTextBox();
            this.descriptionLabel = new Label();
            this.descriptionTextBox = new RoundedTextBox();
            this.unitPriceLabel = new Label();
            this.unitPriceTextBox = new RoundedTextBox();
            this.categoryLabel = new Label();
            this.categoryComboBox = new CustomComboBox();
            this.addCategoryButton = new RoundButton();
            this.supplierLabel = new Label();
            this.supplierComboBox = new CustomComboBox();
            this.addSupplierButton = new RoundButton();
            this.positionLabel = new Label();
            this.rowTextBox = new RoundedTextBox();
            this.rackTextBox = new RoundedTextBox();
            this.placeTextBox = new RoundedTextBox();
            //this.productPriceLabel = new Label();
            //this.priceTextBox = new RoundedTextBox();
            this.startDateLabel = new Label();
            this.startDatePicker = new DateTimePicker();
            this.endDateLabel = new Label();
            this.endDatePicker = new DateTimePicker();
            this.quantityLabel = new Label();
            this.quantityTextBox = new RoundedTextBox();
            this.batchNumberLabel = new Label();
            this.batchNumberTextBox = new RoundedTextBox();
            this.saveButton = new RoundedButton();
            this.cancelButton = new RoundedButton();
            this.pictureBox = new PictureBox();
            this.btnUploadImage = new RoundedButton();
            this.SuspendLayout();

            // Form properties
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.productNameLabel);
            this.Controls.Add(this.productNameTextBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.unitPriceLabel);
            this.Controls.Add(this.unitPriceTextBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.addCategoryButton);
            this.Controls.Add(this.supplierLabel);
            this.Controls.Add(this.supplierComboBox);
            this.Controls.Add(this.addSupplierButton);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.rowTextBox);
            this.Controls.Add(this.rackTextBox);
            this.Controls.Add(this.placeTextBox);
            //this.Controls.Add(this.productPriceLabel);
            //this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.quantityTextBox);
            this.Controls.Add(this.batchNumberLabel);
            this.Controls.Add(this.batchNumberTextBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnUploadImage);
            this.Name = "AddProductForm";
            this.Text = "Add New Product";
            this.ResumeLayout(false);
            this.PerformLayout();

            // Product Name
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Location = new Point(12, 25);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new Size(78, 13);
            this.productNameLabel.Text = "Product Name:";
            this.productNameTextBox.Location = new Point(150, 22);
            this.productNameTextBox.Size = new Size(200, 25);

            // Description
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new Point(12, 60);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new Size(63, 13);
            this.descriptionLabel.Text = "Description:";
            this.descriptionTextBox.Location = new Point(150, 55);
            this.descriptionTextBox.Size = new Size(200, 25);

            // Stock Quantity
            /*this.stockQuantityLabel.AutoSize = true;
            this.stockQuantityLabel.Location = new Point(12, 90);
            this.stockQuantityLabel.Name = "stockQuantityLabel";
            this.stockQuantityLabel.Size = new Size(82, 13);
            this.stockQuantityLabel.Text = "Stock quantity:";
            this.stockQuantityTextBox.Location = new Point(150, 85);
            this.stockQuantityTextBox.Size = new Size(200, 25);*/

            // Unit Price
            this.unitPriceLabel.AutoSize = true;
            this.unitPriceLabel.Location = new Point(12, 90);        //(12, 120);
            this.unitPriceLabel.Name = "unitPriceLabel";
            this.unitPriceLabel.Size = new Size(56, 13);
            this.unitPriceLabel.Text = "Unit price";
            this.unitPriceTextBox.Location = new Point(150, 85);             //(150, 115);
            this.unitPriceTextBox.Size = new Size(200, 25);

            // Category
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new Point(12, 125);         //(12, 155);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new Size(52, 13);
            this.categoryLabel.Text = "Category";
            this.categoryComboBox.Location = new Point(150, 115);                              //(150, 145);
            this.addCategoryButton.Location = new Point(300,115);                                          //(300, 145);
            this.addCategoryButton.Size = new Size(30, 30);
            this.addCategoryButton.Text = "➕";
            this.addCategoryButton.BackColor = Color.Yellow;
            this.addCategoryButton.Click += new EventHandler(this.AddCategoryButton_Click);

            // Supplier
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Location = new Point(12, 166);         //(12, 196);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new Size(48, 13);
            this.supplierLabel.Text = "Supplier";
            this.supplierComboBox.Location = new Point(150, 156);
            this.addSupplierButton.Location = new Point(300, 156);
            this.addSupplierButton.Size = new Size(30, 30);
            this.addSupplierButton.Text = "➕";
            this.addSupplierButton.BackColor = Color.Yellow;
            this.addSupplierButton.Click += new EventHandler(this.AddSupplierButton_Click);

            // Position
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new Point(12, 200);     //(12, 230);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new Size(47, 13);
            this.positionLabel.Text = "Position (Row-Rack-Place)";
            this.rowTextBox.Location = new Point(150, 195);
            this.rowTextBox.Size = new Size(50, 20);
            this.rackTextBox.Location = new Point(210, 195);
            this.rackTextBox.Size = new Size(50, 20);
            this.placeTextBox.Location = new Point(270, 195);
            this.placeTextBox.Size = new Size(50, 20);

            // Product Price
            /*this.productPriceLabel.AutoSize = true;
            this.productPriceLabel.Location = new Point(12, 225);  //(12, 255);
            this.productPriceLabel.Name = "productPriceLabel";
            this.productPriceLabel.Size = new Size(73, 13);
            this.productPriceLabel.Text = "Product price";
            this.priceTextBox.Location = new Point(150, 220);
            this.priceTextBox.Size = new Size(200, 20);*/

            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new Point(12, 225);     //(12,250) //(12, 280);
            this.startDateLabel.Size = new Size(73,13);
            this.startDateLabel.Text = "Start date";
            this.startDatePicker.Location = new Point(150, 220);
            this.startDatePicker.Size = new Size(200, 20);

            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new Point(12, 250);      //(12, 305);
            this.endDateLabel.Size = new Size(73, 13);
            this.endDateLabel.Text = "End date";
            this.endDatePicker.Location = new Point(150, 245);
            this.endDatePicker.Size = new Size(200, 20);

            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new Point(12, 275);    //(12, 330);
            this.quantityLabel.Size = new Size(73, 13);
            this.quantityLabel.Text = "Quantity";
            this.quantityTextBox.Location = new Point(150, 270);
            this.quantityTextBox.Size = new Size(200, 20);


            this.batchNumberLabel.AutoSize = true;
            this.batchNumberLabel.Location = new Point(12, 300);  // Point(12, 355);
            this.batchNumberLabel.Size = new Size(73, 13);
            this.batchNumberLabel.Text = "Batch number";
            this.batchNumberTextBox.Location = new Point(150, 295);
            this.batchNumberTextBox.Size = new Size(200, 20);


            //picture box
            this.pictureBox.Location = new Point(425, 25);
            this.pictureBox.Size = new Size(150, 150);
            this.btnUploadImage.Location = new Point(425, 185);
            this.btnUploadImage.Size = new Size(100, 30);
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.Font = new Font("Arial", 9, FontStyle.Bold);
            this.btnUploadImage.BackColor = Color.DarkGoldenrod;
            this.btnUploadImage.Click += BtnUploadImage_Click;

            // Save button
            this.saveButton.Location = new Point(150, 350);
            this.saveButton.Size = new Size(75, 23);
            this.saveButton.Text = "Save";
            this.saveButton.Font = new Font("Arial", 9, FontStyle.Bold);
            this.saveButton.BackColor = Color.SeaGreen;
            this.saveButton.Click += new EventHandler(this.SaveButton_Click);

            // Cancel button
            this.cancelButton.Location = new Point(250, 350);
            this.cancelButton.Size = new Size(75, 23);
            this.cancelButton.BackColor = Color.IndianRed;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Font = new Font("Arial", 9, FontStyle.Bold);
            this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
        }

        private Label productNameLabel;
        private RoundedTextBox productNameTextBox;
        private Label descriptionLabel;
        private RoundedTextBox descriptionTextBox;
        private Label stockQuantityLabel;
        private RoundedTextBox stockQuantityTextBox;
        private Label unitPriceLabel;
        private RoundedTextBox unitPriceTextBox;
        private Label categoryLabel;
        private CustomComboBox categoryComboBox;
        private RoundButton addCategoryButton;
        private Label supplierLabel;
        private CustomComboBox supplierComboBox;
        private RoundButton addSupplierButton;
        private Label positionLabel;
        private RoundedTextBox rowTextBox;
        private RoundedTextBox rackTextBox;
        private RoundedTextBox placeTextBox;
        private Label productPriceLabel;
        private RoundedTextBox priceTextBox;
        private Label startDateLabel;
        private DateTimePicker startDatePicker;
        private Label endDateLabel;
        private DateTimePicker endDatePicker;
        private Label quantityLabel;
        private RoundedTextBox quantityTextBox;
        private Label batchNumberLabel;
        private RoundedTextBox batchNumberTextBox;
        private RoundedButton saveButton;
        private RoundedButton cancelButton;
        private PictureBox pictureBox;
        private RoundedButton btnUploadImage;
    }
}