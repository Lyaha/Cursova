using System.Drawing;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class OrderGrid
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
            this.ClientSize = new System.Drawing.Size(800, 450);
            lblH1 = new Label() { Top = 10, Left = 0, Font = new Font("Arial", 12, FontStyle.Bold), Text = "Order" };
            this.Controls.Add(lblH1);
            AddBttn = new RoundButton() { Top = 5, Left = this.Width - 135, BackColor = Color.Green, Text = "➕", Size = new Size(30, 30) };
            this.Controls.Add(AddBttn);
            AddBttn.Click += AddButtonClick;
            orderGrid = new CustomDataGridView(httpClient,"", "","") { Top = 40, Left = 0, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Width = this.Width, Height = this.Height - 40 };
            this.Controls.Add(orderGrid);

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "OrderGridForm";
        }

        #endregion
    }
}