using System.Drawing;
using System.Reflection.Emit;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class EditUserForm
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
            this.userNameTextBox = new RoundedTextBox();
            this.emailTextBox = new RoundedTextBox();
            this.passwordTextBox = new RoundedTextBox();
            this.roleIdTextBox = new RoundedTextBox();
            this.saveButton = new RoundedButton();
            this.cancelButton = new RoundedButton();

            System.Windows.Forms.Label userNameLabel = new System.Windows.Forms.Label();
            System.Windows.Forms.Label emailLabel = new System.Windows.Forms.Label();
            System.Windows.Forms.Label passwordLabel = new System.Windows.Forms.Label();
            System.Windows.Forms.Label roleIdLabel = new System.Windows.Forms.Label();

            userNameLabel.Text = "Username";
            userNameLabel.Location = new Point(20, 0);
            userNameLabel.Size = new Size(200, 20);

            emailLabel.Text = "Email";
            emailLabel.Location = new Point(20, 60);
            emailLabel.Size = new Size(200, 20);

            passwordLabel.Text = "Password";
            passwordLabel.Location = new Point(20, 120);
            passwordLabel.Size = new Size(200, 20);

            roleIdLabel.Text = "Role ID";
            roleIdLabel.Location = new Point(20, 180);
            roleIdLabel.Size = new Size(200, 20);

            // Настройка текстовых полей
            this.userNameTextBox.Location = new Point(20, 20);
            this.userNameTextBox.Size = new Size(200, 30);
            this.userNameTextBox.Name = "Username";

            this.emailTextBox.Location = new Point(20, 80);
            this.emailTextBox.Size = new Size(200, 30);
            this.emailTextBox.Name = "Email";

            this.passwordTextBox.Location = new Point(20, 140);
            this.passwordTextBox.Size = new Size(200, 30);
            this.passwordTextBox.Name = "Password";

            this.roleIdTextBox.Location = new Point(20, 200);
            this.roleIdTextBox.Size = new Size(200, 30);
            this.roleIdTextBox.Name = "Role ID";

            // Настройка кнопок
            this.saveButton.Text = "Save";
            this.saveButton.Location = new Point(20, 240);
            this.saveButton.Size = new Size(90, 30);
            this.saveButton.Click += SaveButton_Click;

            this.cancelButton.Text = "Cancel";
            this.cancelButton.Location = new Point(130, 240);
            this.cancelButton.Size = new Size(90, 30);
            this.cancelButton.Click += (s, e) => this.Close();

            // Добавление элементов управления на форму
            this.Controls.Add(userNameLabel);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(emailLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(roleIdLabel);
            this.Controls.Add(this.roleIdTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);

            this.Text = "Edit User";
            this.Size = new Size(260, 320);
        }
        
        #endregion
    }
}