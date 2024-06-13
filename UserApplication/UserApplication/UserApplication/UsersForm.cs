using Microsoft.VisualBasic.ApplicationServices;
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
using UserApplication.FormElementClasses;

namespace UserApplication
{
    public partial class UsersForm : Form
    {
        private CustomDataGridView usersDataGridView;
        private HttpClient httpClient;

        public UsersForm(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadUsers();
        }
        private async void LoadUsers()
        {
            var us = new List<GridUser>();
            try
            {
                var users = await httpClient.GetFromJsonAsync<List<User>>("https://localhost:7287/api/Users");
                foreach (var item in users)
                {
                    var rolename = await httpClient.GetFromJsonAsync<Role>($"https://localhost:7287/api/Roles/{item.RoleId}");
                    var a = new GridUser()
                    { 
                        UserId = item.UserId,
                        Username = item.Username,
                        Password = item.Password,
                        Email = item.Email,
                        RoleId = item.RoleId,
                        RoleName = rolename.RoleName
                    };
                    us.Add(a);
                }
                this.usersDataGridView.SetDataUsers(us);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            EditUserForm userssForm = new EditUserForm(0, httpClient, false);
            if (userssForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        public class Role
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
        public class GridUser
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
