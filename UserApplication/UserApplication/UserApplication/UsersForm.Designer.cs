using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class UsersForm
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
            this.Size = new Size(800, 600);

            var user = new List<GridUser>();
            this.addButton = new RoundButton();
            this.H1userlbl = new System.Windows.Forms.Label() { Top = 10, Left = 0, Font = new Font("Arial", 12, FontStyle.Bold), Text = "Users" };

            this.addButton.Location = new Point(700, 5);
            this.addButton.Size = new Size(30, 30);
            this.addButton.Text = "➕";
            this.addButton.BackColor = Color.LightGreen;
            this.addButton.Click += addButton_Click;

            this.usersDataGridView = new CustomDataGridView(httpClient, user);
            this.usersDataGridView.Location = new Point(0, 40);
            this.usersDataGridView.Size = new Size(this.Width, this.Height - 20);

            this.Controls.Add(this.H1userlbl);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.usersDataGridView);
            this.Text = "Users";
            
        }
        private System.Windows.Forms.Label H1userlbl;
        private RoundButton addButton;
        #endregion
    }
}