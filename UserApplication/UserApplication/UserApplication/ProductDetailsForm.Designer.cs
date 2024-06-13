using System.Drawing;
using System.Windows.Forms;

namespace UserApplication
{
    partial class ProductDetailsForm
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productPricesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionsDataGridView)).BeginInit();
            this.SuspendLayout();

            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(154, 9);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(35, 13);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Name";

            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(154, 32);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(49, 13);
            this.lblCategoryName.TabIndex = 1;
            this.lblCategoryName.Text = "Category";

            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Location = new System.Drawing.Point(154, 55);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(45, 13);
            this.lblSupplierName.TabIndex = 2;
            this.lblSupplierName.Text = "Supplier";

            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(154, 78);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(31, 13);
            this.lblUnitPrice.TabIndex = 3;
            this.lblUnitPrice.Text = "Price";

            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(154, 101);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(46, 13);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity";

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
            this.positionHeaderLabel.Location = new System.Drawing.Point(15, 375);
            this.positionHeaderLabel.Name = "positionHeaderLabel";
            this.positionHeaderLabel.Size = new System.Drawing.Size(600, 25);
            this.positionHeaderLabel.TabIndex = 14;
            this.positionHeaderLabel.Text = "Position";
            this.positionHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

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
            this.positionsDataGridView.Location = new System.Drawing.Point(15, 400);
            this.positionsDataGridView.Name = "positionsDataGridView";
            this.positionsDataGridView.Size = new System.Drawing.Size(600, 150);
            this.positionsDataGridView.TabIndex = 7;

            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Name";

            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(12, 32);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(49, 13);
            this.labelCategory.TabIndex = 9;
            this.labelCategory.Text = "Category";

            // 
            // labelSupplier
            // 
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Location = new System.Drawing.Point(12, 55);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(45, 13);
            this.labelSupplier.TabIndex = 10;
            this.labelSupplier.Text = "Supplier";

            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(12, 78);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(31, 13);
            this.labelPrice.TabIndex = 11;
            this.labelPrice.Text = "Price";

            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(12, 101);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(46, 13);
            this.labelQuantity.TabIndex = 12;
            this.labelQuantity.Text = "Quantity";

            // 
            // ProductDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 561);
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
            this.Name = "ProductDetailsForm";
            this.Text = "Product Details";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productPricesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label positionHeaderLabel;
        private Label productHeaderLabel;
        private Label labelName;
        private Label labelCategory;
        private Label labelSupplier;
        private Label labelPrice;
        private Label labelQuantity;
    }
}