using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static UserApplication.Form1;

namespace UserApplication
{
    public partial class Form1 : Form
    {
        private SideMenu sideMenu;
        private FlowLayoutPanel productPanel;
        private HttpClient httpClient;
        private RoundedTextBoxWithIcon searchBox;
        private RoundButton addButton;
        private CustomComboBox columnComboBox;
        private CustomComboBox searchTypeComboBox;
        private List<Column> columns;
        private RoundButton clearButton;
        private Timer animationClearButtonTimer;

        public Form1(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();

            this.sideMenu = new SideMenu(httpClient);
            this.sideMenu.Dock = DockStyle.Left;
            this.sideMenu.Width = 50;
            this.sideMenu.MenuToggled += SideMenu_MenuToggled;
            this.sideMenu.stockorderButton.Click += stockorderButton_Click;
            this.sideMenu.userButton.Click += userButton_Click;
            this.Controls.Add(this.sideMenu);

            columnComboBox = new CustomComboBox
            {
                Location = new Point(this.sideMenu.Width+10, 20),
                Width = 150
            };

            searchTypeComboBox = new CustomComboBox
            {
                Location = new Point(this.columnComboBox.Right + 10, 20),
                Width = 150,
                Enabled = false
            };

            searchBox = new RoundedTextBoxWithIcon
            {
                Location = new Point(searchTypeComboBox.Right+10, 20),
                Width = 300
            };
            searchBox.SearchButtonClick += SearchBox_SearchButtonClick;

            clearButton = new RoundButton
            {
                Text = "🗑️",
                Location = new Point(searchBox.Right+1, 20),
                Size = new Size(30, 30),
                BackColor = Color.LightPink,
                Visible = false
            };
            clearButton.Click += ClearSearchButtton_Click;

            addButton = new RoundButton
            {
                Text = "➕",
                Location = new Point(searchBox.Right + 10, 20),
                Size = new Size(30, 30),
                BackColor = Color.LightGreen
            };
            addButton.Click += AddButton_Click;
            this.Controls.Add(columnComboBox);
            this.Controls.Add(searchTypeComboBox);
            this.Controls.Add(searchBox);
            this.Controls.Add(clearButton);
            this.Controls.Add(addButton);


            this.productPanel = new FlowLayoutPanel();
            this.productPanel.AutoScroll = true;
            this.productPanel.Location = new Point(this.sideMenu.Width, 60);
            this.productPanel.Size = new Size(this.ClientSize.Width - this.sideMenu.Width, this.ClientSize.Height-60);
            this.productPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(this.productPanel);

           
            animationClearButtonTimer = new Timer();
            animationClearButtonTimer.Interval = 15;
            animationClearButtonTimer.Tick += AnimationTimer_Tick;
            LoadProductsAsync();
            LoadColumns();
            
        }
        private async void LoadColumns()
        {
            try
            {
                var columns = await httpClient.GetFromJsonAsync<List<Column>>("https://localhost:7287/api/product/columns");

                columnComboBox.Items.Clear();
                foreach (var column in columns)
                {
                    columnComboBox.Items.Add(column.Name);
                }
                columnComboBox.SelectedIndexChanged += ColumnsComboBox_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ColumnsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!searchTypeComboBox.Enabled)
                searchTypeComboBox.Enabled = true;
            searchTypeComboBox.Items.Clear();
            columns = await httpClient.GetFromJsonAsync<List<Column>>("https://localhost:7287/api/product/columns");
            var selectedColumn = columns.FirstOrDefault(c => c.Name == columnComboBox.SelectedItem);
            if (selectedColumn != null)
            {
                switch (selectedColumn.DataType)
                {
                    case "Int32":
                    case "Decimal":
                    case "DateTime":
                        searchTypeComboBox.Items.AddRange(new string[] { "Greater than or equal", "Less than or equal", "Equal", "Less than", "Greater than" });
                        break;
                    case "String":
                        searchTypeComboBox.Items.AddRange(new string[] { "Contains", "Starts with" });
                        break;
                }
            }

            searchTypeComboBox.SelectedIndex = -1;
        }

        private void stockorderButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.stockorderButton);
            StocksOrders stor = new StocksOrders(httpClient);
            FormNavigator.Navigate(this, stor);
        }
        private void userButton_Click(object sender, EventArgs e)
        {
            this.sideMenu.SetActiveButton(this.sideMenu.userButton);
            UserProfileForm user = new UserProfileForm(httpClient);
            FormNavigator.Navigate(this, user);
        }
        public class Column
        {
            public string Name { get; set; }
            public string DataType { get; set; }
        }
        public async Task LoadProductsAsync()
        {
            try
            {
                this.productPanel.Controls.Clear();

                var products = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7287/api/Product");
                foreach (var product in products)
                {
                    var productItem = new ProductItemControl(httpClient);
                    Image productImage = null;

                    try
                    {
                        var imageUrl = $"https://localhost:7287/api/Product/image/{product.ProductId}";
                        productImage = Image.FromStream(await httpClient.GetStreamAsync(imageUrl));
                    }
                    catch
                    {
                        productImage = Image.FromFile("RegularImage/NoPhoto.png");
                    }

                    productItem.SetProductData(product.ProductName, product.LastUpdated, product.StockQuantity, product.ProductId, productImage);
                    this.productPanel.Controls.Add(productItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        private async void SearchBox_SearchButtonClick(object sender, EventArgs e)
        {
            string searchText = searchBox.TextBoxText;
            var searchresult = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7287/api/Product");
            string selectedColumn = columnComboBox.SelectedItem?.ToString();
            string selectedSearchType = searchTypeComboBox.SelectedItem?.ToString();
            var types = new[] { "Greater than or equal", "Less than or equal", "Equal", "Less than", "Greater than" };
            this.productPanel.Controls.Clear();
            
            try 
            {
                if (selectedColumn != null || selectedSearchType != null)
                {
                    if (types.Contains(selectedSearchType))
                    {
                        var mappings = new Dictionary<string, string>
                        {
                            {"Greater than or equal", "gte"},
                            {"Less than or equal", "lte"},
                            {"Equal", "eq"},
                            {"Less than", "lt"},
                            {"Greater than", "gt"}
                        };
                        mappings.TryGetValue(selectedSearchType, out var shortenedValue);
                        searchresult = await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7287/api/Product/search?fieldName={selectedColumn}&value={searchText}&comparison={shortenedValue}");
                    }
                    else
                    {
                        var mappings = new Dictionary<string, string>
                        {
                            {"Contains", "contains"},
                            {"Starts with", "startswith"}
                        };
                        mappings.TryGetValue(selectedSearchType, out var shortenedValue);
                        searchresult = await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7287/api/Product/searchtext?fieldName={selectedColumn}&value={searchText}&comparison={shortenedValue}");
                    }
                    if (searchresult.Count != 0)
                    {
                        foreach (var product in searchresult)
                        {
                            var productItem = new ProductItemControl(httpClient);
                            Image productImage = null;
                            try
                            {
                                var imageUrl = $"https://localhost:7287/api/Product/image/{product.ProductId}";
                                productImage = Image.FromStream(await httpClient.GetStreamAsync(imageUrl));
                            }
                            catch
                            {
                                productImage = Image.FromFile("RegularImage/NoPhoto.png");
                            }

                            productItem.SetProductData(product.ProductName, product.LastUpdated, product.StockQuantity, product.ProductId, productImage);
                            this.productPanel.Controls.Add(productItem);
                        }
                        clearButton.Visible = true;
                        animationClearButtonTimer.Start();
                    }
                    else
                    {
                        MessageBox.Show("Search result null");
                        LoadProductsAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Some items equals null");
                    LoadProductsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (clearButton.Visible)
            {
                if (clearButton.Left > searchBox.Right + 10)
                {
                    clearButton.Left += 5;
                    addButton.Left += 5;
                }
                else
                {
                    clearButton.Left = searchBox.Left + searchBox.Width + 10;
                    addButton.Left = clearButton.Left + clearButton.Width+ 10;
                    animationClearButtonTimer.Stop();
                }
            }
            else
            {
                if (addButton.Left < searchBox.Right + 10)
                {
                    addButton.Left += 5;
                }
                else
                {
                    addButton.Left = searchBox.Right + 10;
                    animationClearButtonTimer.Stop();
                }
            }
        }

        private void ClearSearchButtton_Click(object sender, EventArgs e)
        {

            columnComboBox.SelectedIndex = -1;
            searchTypeComboBox.SelectedIndex = -1;
            searchBox.TextBoxText = string.Empty;

            clearButton.Visible = false;
            animationClearButtonTimer.Start();
            LoadProductsAsync();
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            // Логика создания нового продукта
            var addProductForm = new AddProductForm(httpClient);
            if (addProductForm.ShowDialog() == DialogResult.OK)
            {
                LoadProductsAsync();
            }
        }

        private void SideMenu_MenuToggled(object sender, EventArgs e)
        {
            if (sideMenu.IsExpanded)
            {
                // Обновить расположение и размер панели с продуктами при развертывании меню [completed]
                this.productPanel.Location = new Point(this.sideMenu.ExpandedWidth, 60);
                this.columnComboBox.Location = new Point(this.sideMenu.Width + 10, 20);
                this.searchTypeComboBox.Location = new Point(this.columnComboBox.Right + 10, 20);
                this.searchBox.Location = new Point(searchTypeComboBox.Right + 10, 20);
                this.clearButton.Location = new Point(searchBox.Right + 10, 20);
                if (clearButton.Visible)
                    this.addButton.Location = new Point(clearButton.Right + 10, 20);
                else
                    this.addButton.Location = new Point(searchBox.Right + 10, 20);


                this.productPanel.Size = new Size(this.ClientSize.Width - this.sideMenu.ExpandedWidth, this.ClientSize.Height);
            }
            else
            {
                // Обновить расположение и размер панели с продуктами при свертывании меню [completed]
                this.productPanel.Location = new Point(this.sideMenu.Width, 60);
                this.columnComboBox.Location = new Point(this.sideMenu.Width + 10, 20);
                this.searchTypeComboBox.Location = new Point(this.columnComboBox.Right + 10, 20);
                this.searchBox.Location = new Point(searchTypeComboBox.Right + 10, 20);
                this.clearButton.Location = new Point(searchBox.Right + 10, 20);
                if (clearButton.Visible)
                    this.addButton.Location = new Point(clearButton.Right + 10, 20);
                else
                    this.addButton.Location = new Point(searchBox.Right + 10, 20);

                this.productPanel.Size = new Size(this.ClientSize.Width - this.sideMenu.Width, this.ClientSize.Height);
            }
        }
    }
    public static class FormNavigator
    {
        public static void Navigate(Form currentForm, Form newForm)
        {
            newForm.FormClosed += (s, args) => currentForm.Close();
            newForm.Show();
            currentForm.Hide();
        }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
        public int supplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime LastUpdated { get; set; }
        public int StockQuantity { get; set; }
        public string ProductImage { get; set; }
    }
}
