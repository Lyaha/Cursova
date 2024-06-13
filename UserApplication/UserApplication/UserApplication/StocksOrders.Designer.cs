using System.Drawing;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class StocksOrders
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
        private SideMenu sideMenu;
        private CustomDataGridView stockDataGridView;
        private CustomDataGridView ordersDataGridView;
        private Label StockH1;
        private Label OrdersH1;
        private void InitializeComponent()
        {
            this.sideMenu = new UserApplication.FormElementClasses.SideMenu(httpClient);
            this.stockDataGridView = new UserApplication.FormElementClasses.CustomDataGridView(httpClient, "String1", "string2");
            this.ordersDataGridView = new CustomDataGridView(httpClient, "1","1","1");
            this.StockH1 = new System.Windows.Forms.Label();
            this.OrdersH1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1050, 450);
            // 
            // sideMenu
            // 
            this.sideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideMenu.Location = new System.Drawing.Point(0, 0);
            this.sideMenu.Name = "sideMenu";
            this.sideMenu.Size = new System.Drawing.Size(50, 450);
            this.sideMenu.TabIndex = 1;
            this.sideMenu.MenuToggled += SideMenu_MenuToggled;
            this.sideMenu.productButton.Click += productButton_Click;
            this.sideMenu.userButton.Click += userButton_Click;
            // 
            // stockDataGridView
            // 
            this.stockDataGridView.Location = new System.Drawing.Point(50, 30);
            this.stockDataGridView.Name = "stockDataGridView";
            this.stockDataGridView.Size = new System.Drawing.Size(850, 160);
            this.stockDataGridView.TabIndex = 0;
            // 
            // StockH1
            // 
            this.StockH1.Location = new System.Drawing.Point(50, 0);
            this.StockH1.Name = "StockH1";
            this.StockH1.Size = new System.Drawing.Size(100, 25);
            this.StockH1.Text = "Stocks";
            this.StockH1.Font =new Font("Arial", 12, FontStyle.Bold);
            this.StockH1.TabIndex = 0;
            this.Controls.Add(StockH1);
            //
            // OrdersH1
            //
            this.OrdersH1.Location = new System.Drawing.Point(50, 200);
            this.OrdersH1.Name = "OrdersH1";
            this.OrdersH1.Size = new System.Drawing.Size(100, 25);
            this.OrdersH1.Text = "Orders";
            this.OrdersH1.Font = new Font("Arial", 12, FontStyle.Bold);
            this.OrdersH1.TabIndex = 0;
            this.Controls.Add(OrdersH1);
            //
            // OrderGrid
            //
            this.ordersDataGridView.Location = new System.Drawing.Point(50, 230);
            this.ordersDataGridView.Name = "ordersDataGridView";
            this.ordersDataGridView.Size = new System.Drawing.Size(850, 160);
            this.ordersDataGridView.TabIndex = 0;
            this.Controls.Add(ordersDataGridView);
            // 
            // StocksOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            this.Controls.Add(this.stockDataGridView);
            this.Controls.Add(this.sideMenu);
            this.Name = "StocksOrders";
            this.Text = "StocksOrders";
            this.ResumeLayout(false);

        }

        #endregion
    }
}