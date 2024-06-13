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
using static UserApplication.UsersForm;

namespace UserApplication.FormElementClasses
{
    public partial class RoleGridForm : Form
    {
        private CustomDataGridView roleDataGridView;
        private Label H1Role;
        private RoundButton addButton;
        private HttpClient httpClient;
        public RoleGridForm(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadRole();
        }
        private async void LoadRole()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Role>>($"https://localhost:7287/api/Roles");
                this.roleDataGridView.SetDataRoles(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }
        }
        private async void addButton_Click(object sender, EventArgs e)
        {
            string newRoleyName = Prompt.ShowDialog("Enter new role name:", "Add Role");
            if (!string.IsNullOrEmpty(newRoleyName))
            {
                var response = await httpClient.PostAsJsonAsync($"https://localhost:7287/api/Roles/", new { RoleName = newRoleyName });
                if (response.IsSuccessStatusCode)
                {
                    var response2 = await httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7287/api/Roles");
                    this.roleDataGridView.SetDataRoles(response2);
                }
            }
        }
    }
}
