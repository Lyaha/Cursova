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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static UserApplication.AddProductForm;

namespace UserApplication.FormElementClasses
{
    public partial class SupplierForm : Form
    {
        private CustomDataGridView supplierDataGridView;
        private CustomComboBox searchTypeComboBox;
        private CustomComboBox searchColumnComboBox;
        private RoundedTextBoxWithIcon searchTextBox;
        private RoundButton clearSearchButton;
        private RoundButton addSupplierButton;
        private Panel searchPanel;
        private HttpClient httpClient;
        public SupplierForm(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadSuppliersAsync();
        }
        private async void LoadSuppliersAsync()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7287/api/Suppliers");
                supplierDataGridView.SetDataSup(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier: {ex.Message}");
            }
        }
        private async void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.TextBoxText;
            string selectedSearchColumn = searchColumnComboBox.SelectedItem?.ToString();
            string selectedSearchType = searchTypeComboBox.SelectedItem?.ToString();
            var mappings = new Dictionary<string, string>
                        {
                            {"Contains", "contains"},
                            {"Starts with", "startswith"}
                        };
            var mappping = new Dictionary<string, string>
                        {
                            {"Supplier Name", "supplierName"},
                            {"Contact person", "contactPerson"},
                            {"Contact email", "contactEmail" },
                            {"Contact phone", "contactPhone"}
                        };
            mappings.TryGetValue(selectedSearchType, out var shortenedValue);
            mappping.TryGetValue(selectedSearchColumn, out var shortenedValue2);
            var searchresult = await httpClient.GetFromJsonAsync<List<Supplier>>($"https://localhost:7287/api/Suppliers/searchtext?fieldName={shortenedValue2}&value={searchText}&comparison={shortenedValue}");
            supplierDataGridView.SetDataSup(searchresult);
        }

        private void ClearSearchButton_Click(object sender, EventArgs e)
        {
            // Implement clear search logic
            this.searchTextBox.TextBoxText = string.Empty;
            this.searchColumnComboBox.SelectedIndex = -1;
            this.searchTypeComboBox.SelectedIndex = -1;
            LoadSuppliersAsync();
        }

        private async void addSupplierButton_Click(object sender, EventArgs e)
        {
            var newSupplier = Prompt.ShowDialog("Supplier Name", "Contact person", "Contact email", "Contact phone", "Add supplier");
            if (newSupplier != null)
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Suppliers", new { supplierName = newSupplier.supplierName, contactPerson = newSupplier.ContactPerson, contactEmail = newSupplier.ContactEmail, contactPhone = newSupplier.ContactPhone});
                if (response.IsSuccessStatusCode)
                {
                    LoadSuppliersAsync();
                }
            }
        }

    }
}
