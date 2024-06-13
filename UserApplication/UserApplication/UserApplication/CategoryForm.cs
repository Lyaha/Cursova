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
using static UserApplication.AddProductForm;
using static UserApplication.Form1;


namespace UserApplication.FormElementClasses
{
    public partial class CategoryForm : Form
    {
        private CustomDataGridView categoryDataGridView;
        private CustomComboBox searchTypeComboBox;
        private RoundedTextBoxWithIcon searchTextBox;
        private RoundButton clearSearchButton;
        private RoundButton addCategoryButton;
        private Panel searchPanel;
        private HttpClient httpClient;

        public CategoryForm(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadCategoriesAsync();
        }
        private async void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.TextBoxText;
            string selectedSearchType = searchTypeComboBox.SelectedItem?.ToString();
            var mappings = new Dictionary<string, string>
                        {
                            {"Contains", "contains"},
                            {"Starts with", "startswith"}
                        };
            mappings.TryGetValue(selectedSearchType, out var shortenedValue);
            var searchresult = await httpClient.GetFromJsonAsync<List<Category>>($"https://localhost:7287/api/Category/Search?name={searchText}&comparison={shortenedValue}");
            categoryDataGridView.SetData(searchresult);
        }

        private void ClearSearchButton_Click(object sender, EventArgs e)
        {
            // Implement clear search logic
            this.searchTextBox.TextBoxText = string.Empty;
            this.searchTypeComboBox.SelectedIndex = -1;
            LoadCategoriesAsync();
        }

        private async void AddCategoryButton_Click(object sender, EventArgs e)
        {
            string newCategoryName = Prompt.ShowDialog("Enter new category name:", "Add Category");
            if (!string.IsNullOrEmpty(newCategoryName))
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Category", new { CategoryName = newCategoryName });
                if (response.IsSuccessStatusCode)
                {
                    LoadCategoriesAsync();
                }
            }
        }
        private async void LoadCategoriesAsync()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7287/api/Category");
                categoryDataGridView.SetData(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }
    }
}
