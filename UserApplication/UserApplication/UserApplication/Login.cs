using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;

namespace UserApplication
{
    public partial class Login : Form
    {
        private readonly HttpClient httpClient;
        private readonly ICurrentUserService currentUserService;

        public Login(ICurrentUserService currentUserService, HttpClient httpClients)
        {
            this.httpClient = httpClients;
            this.currentUserService = currentUserService;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.TextBoxText;
            var password = txtPassword.TextBoxText;
            var rememberMe = chkRememberMe.Checked;

            var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Auth/login", new { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();

                if (rememberMe)
                {
                    Properties.Settings.Default.UserID = result.User.UserId;
                    Properties.Settings.Default.Username = username;
                    Properties.Settings.Default.Password = password;
                    Properties.Settings.Default.RememberMe = true;
                }
                else
                {
                    Properties.Settings.Default.UserID = result.User.UserId;
                    Properties.Settings.Default.Username = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.RememberMe = false;
                }

                Properties.Settings.Default.Save();

                // Зберігаємо дані користувача в сервісі
                currentUserService.CurrentUser = result.User;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid login credentials");
            }
        }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
    public partial class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

    }
    public interface ICurrentUserService
    {
        User CurrentUser { get; set; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        public User CurrentUser { get; set; }
    }
}
