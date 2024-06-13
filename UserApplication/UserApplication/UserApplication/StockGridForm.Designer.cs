using System.Drawing;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    partial class StockGridForm
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
            this.lblH1 = new System.Windows.Forms.Label();
            this.AddBttn = new UserApplication.FormElementClasses.RoundButton();
            this.stockGrid = new UserApplication.FormElementClasses.CustomDataGridView(httpClient, "String1", "string2");
            this.SuspendLayout();
            // 
            // lblH1
            // 
            this.lblH1.Location = new System.Drawing.Point(0, 0);
            this.lblH1.Name = "lblH1";
            this.lblH1.Text = "Stocks";
            this.lblH1.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblH1.Size = new System.Drawing.Size(100, 23);
            this.lblH1.TabIndex = 0;
            // 
            // AddBttn
            // 
            this.AddBttn.Location = new System.Drawing.Point(0, 0);
            this.AddBttn.Name = "AddBttn";
            this.AddBttn.Size = new System.Drawing.Size(75, 23);
            this.AddBttn.TabIndex = 1;
            // 
            // stockGrid
            // 
            this.stockGrid.Location = new System.Drawing.Point(0, 25);
            this.stockGrid.Name = "stockGrid";
            this.stockGrid.Size = new System.Drawing.Size(800, 400);
            this.stockGrid.TabIndex = 2;
            // 
            // StockGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblH1);
            this.Controls.Add(this.AddBttn);
            this.Controls.Add(this.stockGrid);
            this.Name = "StockGridForm";
            this.Text = "StockGridForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}