using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static UserApplication.AddProductForm;

namespace UserApplication
{
    public partial class UserProfileForm : Form
    {
        private HttpClient httpClient;
        public UserProfileForm(HttpClient httpClients)
        {
            this.httpClient = httpClients;
            InitializeComponent();
            this.sideMenu.SetActiveButton(this.sideMenu.userButton);
        }

        private async void UserProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(Properties.Settings.Default.UserID);
                var response = await httpClient.GetFromJsonAsync<User>($"https://localhost:7287/api/Users/{Properties.Settings.Default.UserID}");
                txtEmail.TextBoxText = response.Email;
                txtUsername.TextBoxText = response.Username;
                txtPassword.TextBoxText = response.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Users/{Properties.Settings.Default.UserID}", new { username = txtUsername.TextBoxText, password = txtPassword.TextBoxText, email = txtEmail.TextBoxText });
                if (response.IsSuccessStatusCode)
                {
                    CurrentUser.Username = txtUsername.TextBoxText;
                    CurrentUser.Email = txtEmail.TextBoxText;
                    CurrentUser.Password = txtPassword.TextBoxText;

                    MessageBox.Show("Changes saved successfully!");
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void stockorderButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.stockorderButton);
            StocksOrders stor = new StocksOrders(httpClient);
            FormNavigator.Navigate(this, stor);
        }
        private void productButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.productButton);
            Form1 product = new Form1(httpClient);
            FormNavigator.Navigate(this, product);
        }
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.textBox.UseSystemPasswordChar)
            {
                txtPassword.textBox.UseSystemPasswordChar = false;
                btnTogglePassword.Text = "👁️‍🗨️";
            }
            else
            {
                txtPassword.textBox.UseSystemPasswordChar = true;
                btnTogglePassword.Text = "👁";
            }
        }

        private void SideMenu_MenuToggled(object sender, EventArgs e)
        {
            if (sideMenu.IsExpanded)
            {
                this.lblUsername.Location = new Point(this.sideMenu.Width + 12, 15);
                this.txtUsername.Location = new Point(this.sideMenu.Width + 100, 12);
                this.lblPassword.Location = new Point(this.sideMenu.Width + 12, 67);
                this.txtPassword.Location = new Point(this.sideMenu.Width + 100, 64);
                this.lblEmail.Location = new Point(this.sideMenu.Width + 12, 41);
                this.txtEmail.Location = new Point(this.sideMenu.Width + 100, 38);
                this.btnLogout.Location = new Point(this.sideMenu.Width + 100, 154);
                this.btnSave.Location = new Point(this.sideMenu.Width + 100, 119);
            }
            else
            {
                this.lblUsername.Location = new Point(this.sideMenu.Width + 12, 15);
                this.txtUsername.Location = new Point(this.sideMenu.Width + 100, 12);
                this.lblPassword.Location = new Point(this.sideMenu.Width + 12, 67);
                this.txtPassword.Location = new Point(this.sideMenu.Width + 100, 64);
                this.lblEmail.Location = new Point(this.sideMenu.Width + 12, 41);
                this.txtEmail.Location = new Point(this.sideMenu.Width + 100, 38);
                this.btnLogout.Location = new Point(this.sideMenu.Width + 100, 154);
                this.btnSave.Location = new Point(this.sideMenu.Width + 100, 119);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.UserID = 0;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
    }
    public static class CurrentUser
    {
        public static string Username { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
    }
}
