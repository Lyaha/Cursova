using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UserApplication.UsersForm;
using UserApplication.FormElementClasses;
using System.Net.Http.Json;

namespace UserApplication
{
    public partial class AccessRightsForm : Form
    {
        private List<Role> roles;
        private List<AccessRight> accessRights;
        private Dictionary<string, ToggleSwitch> switches = new Dictionary<string, ToggleSwitch>();
        private HttpClient httpClient;

        public AccessRightsForm(HttpClient httpClients)
        {
            this.httpClient = httpClients;
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            roles = await httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7287/api/Roles");
            accessRights = await httpClient.GetFromJsonAsync<List<AccessRight>>("https://localhost:7287/api/AccessRights");

            if (roles != null && accessRights != null)
            {
                PopulateForm();
            }
            else
            {
                MessageBox.Show("Failed to load data from API.");
            }

        }

        private void PopulateForm()
        {
            var rights = new string[]
            {
            "CanCreateProduct", "CanEditProduct", "CanDeleteProduct",
            "CanCreateStockMovement", "CanEditStockMovement", "CanCreateOrder",
            "CanEditOrder", "CanCreatePayment", "CanEditPayment",
            "CanManageUsers", "CanManageRoles", "CanManageAccessRights"
            };
            for (int i = 0; i < rights.Length; i++)
            {
                Label rightLabel = new Label();
                rightLabel.Text = rights[i];
                rightLabel.Location = new Point(150 + i * 100, 20);
                rightLabel.Size = new Size(80, 20);
                this.Controls.Add(rightLabel);
            }

            for (int i = 0; i < roles.Count; i++)
            {
                Label roleLabel = new Label();
                roleLabel.Text = roles[i].RoleName;
                roleLabel.Location = new Point(20, 50 + i * 40);
                roleLabel.Size = new Size(120, 20);
                this.Controls.Add(roleLabel);
                for (int j = 0; j < rights.Length; j++)
                {
                    ToggleSwitch toggleSwitch = new ToggleSwitch();
                    toggleSwitch.Location = new Point(150 + j * 100, 50 + i * 40);
                    toggleSwitch.Size = new Size(80, 30);
                    toggleSwitch.Name = $"{roles[i].RoleId}_{rights[j]}";

                    var accessRight = accessRights.FirstOrDefault(ar => ar.RoleId == roles[i].RoleId);
                    if (accessRight != null)
                    {
                        var property = accessRight.GetType().GetProperty(rights[j]);
                        if (property != null)
                        {
                            toggleSwitch.Checked = (bool)property.GetValue(accessRight);
                        }
                    }

                    this.Controls.Add(toggleSwitch);
                    switches.Add(toggleSwitch.Name, toggleSwitch);
                }
            }

            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Location = new Point(20, roles.Count * 40 + 70);
            saveButton.Size = new Size(90, 30);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(120, roles.Count * 40 + 70);
            cancelButton.Size = new Size(90, 30);
            cancelButton.Click += (s, e) => this.Close();
            this.Controls.Add(cancelButton);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            List<AccessRight> updatedAccessRights = new List<AccessRight>();

            foreach (var role in roles)
            {
                AccessRight existingAccessRight = accessRights.FirstOrDefault(ar => ar.RoleId == role.RoleId);
                if (existingAccessRight == null)
                {
                    existingAccessRight = new AccessRight { RoleId = role.RoleId };
                }

                foreach (var right in accessRights.First().GetType().GetProperties().Where(p => p.PropertyType == typeof(bool)))
                {
                    string switchName = $"{role.RoleId}_{right.Name}";
                    if (switches.ContainsKey(switchName))
                    {
                        right.SetValue(existingAccessRight, switches[switchName].Checked);
                    }
                }

                updatedAccessRights.Add(existingAccessRight);
            }
            foreach (var item in updatedAccessRights)
            {
                try
                {
                    var res = await httpClient.GetFromJsonAsync<Role>($"https://localhost:7287/api/Roles/{item.RoleId}");
                    var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/AccessRights/{item.AccessId}", new
                    {
                        AccessId = item.AccessId,
                        RoleId = item.RoleId,
                        CanCreateProduct = item.CanCreateProduct,
                        CanEditProduct = item.CanEditProduct,
                        CanDeleteProduct = item.CanDeleteProduct,
                        CanCreateStockMovement = item.CanCreateStockMovement,
                        CanEditStockMovement = item.CanEditStockMovement,
                        CanCreateOrder = item.CanCreateOrder,
                        CanEditOrder = item.CanEditOrder,
                        CanCreatePayment = item.CanCreatePayment,
                        CanEditPayment = item.CanEditPayment,
                        CanManageUsers = item.CanManageUsers,
                        CanManageRoles = item.CanManageRoles,
                        CanManageAccessRights = item.CanManageAccessRights,
                        Role = res
                    });

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Failed to save settings. Error Access {item.AccessId}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving settings: {ex.Message}");
                }
            }
            MessageBox.Show("Settings have been saved successfully.");
        }
    }
    public partial class AccessRight
    {
        public int AccessId { get; set; }

        public int RoleId { get; set; }

        public bool CanCreateProduct { get; set; }

        public bool CanEditProduct { get; set; }

        public bool CanDeleteProduct { get; set; }

        public bool CanCreateStockMovement { get; set; }

        public bool CanEditStockMovement { get; set; }

        public bool CanCreateOrder { get; set; }

        public bool CanEditOrder { get; set; }

        public bool CanCreatePayment { get; set; }

        public bool CanEditPayment { get; set; }

        public bool CanManageUsers { get; set; }

        public bool CanManageRoles { get; set; }

        public bool CanManageAccessRights { get; set; }
    }
}
