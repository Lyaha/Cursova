using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace UserApplication.FormElementClasses
{
    partial class SupplierForm
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

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryForm));
            this.supplierDataGridView = new CustomDataGridView(httpClient,"string");
            this.searchColumnComboBox = new UserApplication.FormElementClasses.CustomComboBox();
            this.searchTypeComboBox = new UserApplication.FormElementClasses.CustomComboBox();
            this.searchTextBox = new UserApplication.RoundedTextBoxWithIcon();
            this.clearSearchButton = new UserApplication.FormElementClasses.RoundButton();
            this.addSupplierButton = new UserApplication.FormElementClasses.RoundButton();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // supplierDataGridView
            // 

            this.supplierDataGridView.Dock = DockStyle.Fill;
            this.supplierDataGridView.AutoScroll = true;
            this.supplierDataGridView.Location = new Point(0, 0);
            this.supplierDataGridView.Size = new Size(784, 501);

            // 
            // searchTypeComboBox
            // 
            this.searchColumnComboBox.Items.AddRange(new string[] { "Supplier Name", "Contact person", "Contact email", "Contact phone" });
            this.searchColumnComboBox.Location = new System.Drawing.Point(10, 15);
            this.searchColumnComboBox.Name = "searchColumnComboBox";
            this.searchColumnComboBox.SelectedIndex = -1;
            this.searchColumnComboBox.Size = new System.Drawing.Size(150, 30);
            this.searchColumnComboBox.TabIndex = 0;

            // 
            // searchTypeComboBox
            // 
            this.searchTypeComboBox.Items.AddRange(new string[] { "Contains", "Starts with" });
            this.searchTypeComboBox.Location = new System.Drawing.Point(170, 15);
            this.searchTypeComboBox.Name = "searchTypeComboBox";
            this.searchTypeComboBox.SelectedIndex = -1;
            this.searchTypeComboBox.Size = new System.Drawing.Size(150, 30);
            this.searchTypeComboBox.TabIndex = 1;

            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(330, 15);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Padding = new System.Windows.Forms.Padding(8);
            this.searchTextBox.Size = new System.Drawing.Size(250, 29);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.TextBoxText = "";
            this.searchTextBox.SearchButtonClick += new System.EventHandler(this.SearchButton_Click);
            // 
            // clearSearchButton
            // 
            this.clearSearchButton.Location = new System.Drawing.Point(595, 15);
            this.clearSearchButton.Name = "clearSearchButton";
            this.clearSearchButton.Size = new System.Drawing.Size(75, 23);
            this.clearSearchButton.TabIndex = 3;
            this.clearSearchButton.Text = "✖️";
            this.clearSearchButton.Click += new System.EventHandler(this.ClearSearchButton_Click);
            // 
            // addSupplierButton
            // 
            this.addSupplierButton.Location = new System.Drawing.Point(685, 15);
            this.addSupplierButton.Name = "addSupplierButton";
            this.addSupplierButton.Size = new System.Drawing.Size(75, 23);
            this.addSupplierButton.TabIndex = 4;
            this.addSupplierButton.Text = "➕";
            this.addSupplierButton.Click += new System.EventHandler(this.addSupplierButton_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.LightGray;
            this.searchPanel.Controls.Add(this.searchColumnComboBox);
            this.searchPanel.Controls.Add(this.searchTypeComboBox);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.clearSearchButton);
            this.searchPanel.Controls.Add(this.addSupplierButton);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(10);
            this.searchPanel.Size = new System.Drawing.Size(784, 120);
            this.searchPanel.TabIndex = 1;
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.supplierDataGridView);
            this.Controls.Add(this.searchPanel);
            this.Name = "CategoryForm";
            this.Text = "Category Management";
            this.searchPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}