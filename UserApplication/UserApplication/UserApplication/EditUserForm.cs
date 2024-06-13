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
    public partial class EditUserForm : Form
    {
        private HttpClient httpClient;
        private int userId;
        private RoundedTextBox userNameTextBox, emailTextBox, passwordTextBox, roleIdTextBox;
        private RoundedButton saveButton;
        private RoundedButton cancelButton;
        private bool edit;

        public EditUserForm(int userId, HttpClient httpClients, bool e)
        {
            this.userId = userId;
            httpClient = httpClients;
            InitializeComponent();
            edit = e;
            if (edit)
                LoadUserDetails();
            else
                this.Text = "Create User";
        }
        private async void LoadUserDetails()
        {
            try
            {
                var user = await httpClient.GetFromJsonAsync<User>($"https://localhost:7287/api/Users/{userId}");
                if (user != null)
                {
                    this.userNameTextBox.TextBoxText = user.Username;
                    this.emailTextBox.TextBoxText = user.Email;
                    this.roleIdTextBox.TextBoxText = user.RoleId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user details: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            int role = 0;
            string password = "";
            if (edit)
            {
                if (string.IsNullOrWhiteSpace(this.roleIdTextBox.TextBoxText) ||
                    string.IsNullOrWhiteSpace(this.userNameTextBox.TextBoxText) ||
                    string.IsNullOrWhiteSpace(this.emailTextBox.TextBoxText))
                {
                    MessageBox.Show("Please fill in all users fields.");
                    return;
                }
                try
                {
                    role = Convert.ToInt32(this.roleIdTextBox.TextBoxText);
                }
                catch
                {
                    MessageBox.Show("Error role ID");
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.passwordTextBox.TextBoxText))
                {
                    try
                    {
                        var user = await httpClient.GetFromJsonAsync<User>($"https://localhost:7287/api/Users/{userId}");
                        password = user.Password;
                    }
                    catch
                    {
                        MessageBox.Show("Error password");
                        return;
                    }
                }
                else
                {
                    password = this.passwordTextBox.TextBoxText;
                }
                try
                {
                    var user = new User { UserId = this.userId, Username = this.userNameTextBox.TextBoxText, Password = password, RoleId = role, Email = this.emailTextBox.TextBoxText };
                    var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Users/{userId}", user);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("User updated successfully.");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating user: {ex.Message}");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.roleIdTextBox.TextBoxText) ||
                   string.IsNullOrWhiteSpace(this.userNameTextBox.TextBoxText) ||
                   string.IsNullOrWhiteSpace(this.emailTextBox.TextBoxText)||
                   string.IsNullOrWhiteSpace(this.passwordTextBox.TextBoxText))
                {
                    MessageBox.Show("Please fill in all users fields.");
                    return;
                }
                try
                {
                    role = Convert.ToInt32(this.roleIdTextBox.TextBoxText);
                }
                catch
                {
                    MessageBox.Show("Error role ID");
                    return;
                }
                try
                {
                    var user = new User { UserId = this.userId, Username = this.userNameTextBox.TextBoxText, Password = this.passwordTextBox.TextBoxText, RoleId = role, Email = this.emailTextBox.TextBoxText };
                    var response = await httpClient.PostAsJsonAsync($"https://localhost:7287/api/Users", new { Username = user.Username, Password = user.Password, RoleId = user.RoleId, Email = user.Email });

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("User create successfully.");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Failed to create user.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error create user: {ex.Message}");
                }
            }
        }
    }
}
