using System.Drawing;
using System.Windows.Forms;
using System;
using System.Net.Http;

namespace UserApplication.FormElementClasses
{
    partial class CategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryForm));
            this.categoryDataGridView = new CustomDataGridView(httpClient);
            this.searchTypeComboBox = new UserApplication.FormElementClasses.CustomComboBox();
            this.searchTextBox = new UserApplication.RoundedTextBoxWithIcon();
            this.clearSearchButton = new UserApplication.FormElementClasses.RoundButton();
            this.addCategoryButton = new UserApplication.FormElementClasses.RoundButton();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoryDataGridView
            // 

            this.categoryDataGridView.Dock = DockStyle.Fill;
            this.categoryDataGridView.AutoScroll = true;
            this.categoryDataGridView.Location = new Point(0, 0);
            this.categoryDataGridView.Size = new Size(784, 501);

           

            // 
            // searchTypeComboBox
            // 
            this.searchTypeComboBox.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("searchTypeComboBox.Items")));
            this.searchTypeComboBox.Location = new System.Drawing.Point(10, 15);
            this.searchTypeComboBox.Name = "searchTypeComboBox";
            this.searchTypeComboBox.SelectedIndex = -1;
            this.searchTypeComboBox.Size = new System.Drawing.Size(150, 30);
            this.searchTypeComboBox.TabIndex = 0;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(170, 15);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Padding = new System.Windows.Forms.Padding(8);
            this.searchTextBox.Size = new System.Drawing.Size(250, 29);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.TextBoxText = "";
            this.searchTextBox.SearchButtonClick += new System.EventHandler(this.SearchButton_Click);
            // 
            // clearSearchButton
            // 
            this.clearSearchButton.Location = new System.Drawing.Point(445, 15);
            this.clearSearchButton.Name = "clearSearchButton";
            this.clearSearchButton.Size = new System.Drawing.Size(75, 23);
            this.clearSearchButton.TabIndex = 3;
            this.clearSearchButton.Text = "✖️";
            this.clearSearchButton.Click += new System.EventHandler(this.ClearSearchButton_Click);
            // 
            // addCategoryButton
            // 
            this.addCategoryButton.Location = new System.Drawing.Point(540, 15);
            this.addCategoryButton.Name = "addCategoryButton";
            this.addCategoryButton.Size = new System.Drawing.Size(75, 23);
            this.addCategoryButton.TabIndex = 4;
            this.addCategoryButton.Text = "➕";
            this.addCategoryButton.Click += new System.EventHandler(this.AddCategoryButton_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.LightGray;
            this.searchPanel.Controls.Add(this.searchTypeComboBox);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.clearSearchButton);
            this.searchPanel.Controls.Add(this.addCategoryButton);
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
            this.Controls.Add(this.categoryDataGridView);
            this.Controls.Add(this.searchPanel);
            this.Name = "CategoryForm";
            this.Text = "Category Management";
            this.searchPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}