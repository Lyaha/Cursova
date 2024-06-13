using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;
using static UserApplication.AddProductForm;

namespace UserApplication
{
    public partial class AddProductForm : Form
    {
        private HttpClient httpClient;
        private List<SupplierComBox> suppliers;
        private List<CategoryComBox> categories;

        public AddProductForm(HttpClient httpClients) 
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadCategories();
            LoadSuppliers();


        }

        private async void LoadCategories()
        {
            int i = 0;
            categories = new List<CategoryComBox>();
            var response = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7287/api/category");
            categoryComboBox.Items.Clear();
            foreach (var category in response)
            {
                var cat = new CategoryComBox()
                {
                    categoryId = category.categoryId,
                    CategoryName = category.CategoryName,
                    IndexCategory = i

                };
                categories.Add(cat);
                i++;
                categoryComboBox.Items.Add(category.CategoryName);
            }
        }

        private async void LoadSuppliers()
        {
            int i = 0;
            var response = await httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7287/api/Suppliers");
            suppliers = new List<SupplierComBox>();
            supplierComboBox.Items.Clear();
            foreach (var supplier in response)
            {
                var sup = new SupplierComBox()
                {
                    supplierId = supplier.supplierId,
                    supplierName = supplier.supplierName,
                    ContactPerson = supplier.ContactPerson,
                    ContactEmail = supplier.ContactEmail,
                    ContactPhone = supplier.ContactPhone,
                    IndexSupplier = i
                };
                i++;
                suppliers.Add(sup);
                supplierComboBox.Items.Add(supplier.supplierName);
            }
        }

        private async void AddCategoryButton_Click(object sender, EventArgs e)
        {
            string newCategoryName = Prompt.ShowDialog("Enter new category name:", "Add Category");
            if (!string.IsNullOrEmpty(newCategoryName))
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Category", new { CategoryName = newCategoryName });
                if (response.IsSuccessStatusCode)
                {
                    LoadCategories();
                }
            }
        }

        private async void AddSupplierButton_Click(object sender, EventArgs e)
        {
            var newSupplier = Prompt.ShowDialog("Enter new supplier name:", "Contact person:", "Contact email:", "Contact phone:", "Add Supplier");
            if (newSupplier != null)
            {
                try
                {
                    var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Suppliers", new { SupplierName = newSupplier.supplierName, ContactPerson = newSupplier.ContactPerson, ContactEmail = newSupplier.ContactEmail, ContactPhone = newSupplier.ContactPhone });
                    if (response.IsSuccessStatusCode)
                    {
                        LoadSuppliers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SomeError:" + ex);
                }
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            string productName = this.productNameTextBox.TextBoxText;
            int categoryIndex = this.categoryComboBox.SelectedIndex;
            int supplierIndex = this.supplierComboBox.SelectedIndex;
            string batchnum = this.batchNumberTextBox.TextBoxText;
            decimal price = 0;
            int quantity = 0;
            int row = 0; int rack = 0; int place = 0;


            price = ConvertTry(price, "Error(check Unit Price(decimal: 1.64)): ", this.unitPriceTextBox);
            quantity = ConvertTry(quantity, "Error(check Quantity(int: 123)): ", this.quantityTextBox);
            if (this.rowTextBox.TextBoxText != "" && this.placeTextBox.TextBoxText != "" && this.rackTextBox.TextBoxText != "")
            {
                row= ConvertTry(row, "Error(check Row(int: 123)): ", this.rowTextBox);
                rack = ConvertTry(rack, "Error(check Rack(int: 123)): ", this.rackTextBox);
                place = ConvertTry(place, "Error(check Place(int: 123)): ", this.placeTextBox);
            }

            Image productImage = this.pictureBox.Image;
            var cat = new CategoryComBox();
            var sup = new SupplierComBox();
            foreach (var cats in categories)
            {
                if (cats.IndexCategory == categoryIndex)
                    cat = cats;
            }
            foreach (var sups in suppliers)
            {
                if (sups.IndexSupplier == supplierIndex)
                    sup = sups;
            }
            // Валидация данных
            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Product name is required.");
                return;
            }

            if (string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Product name is required.");
                return;
            }

            if (quantity == 0)
            {
                MessageBox.Show("Quantity is required.");
                return;
            }

            if (cat == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (sup == null)
            {
                MessageBox.Show("Please select a supplier.");
                return;
            }
            if (string.IsNullOrEmpty(this.descriptionTextBox.TextBoxText))
            {
                MessageBox.Show("Please enter description.");
                return;
            }

            // Создание объекта продукта
            var newProduct = new Product
            {
                ProductName = productName,
                categoryId = cat.categoryId,
                supplierId = sup.supplierId,
                StockQuantity = quantity,
                UnitPrice = price,
                Description = this.descriptionTextBox.TextBoxText
            };
            
            try
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Product", new { productName = newProduct.ProductName, description = newProduct.Description, categoryId = newProduct.categoryId, supplierId = newProduct.supplierId, stockQuantity  = newProduct.StockQuantity, unitPrice = newProduct.UnitPrice});
                if (response.IsSuccessStatusCode)
                {
                    var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
                    int productId = createdProduct.ProductId;
                    if (productImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            productImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] imageBytes = memoryStream.ToArray();
                            var content = new MultipartFormDataContent();

                            var byteArrayContent = new ByteArrayContent(imageBytes);
                            byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                            content.Add(byteArrayContent, "file", $"prod{productId}.png");

                            var imageResponse = await httpClient.PostAsync($"https://localhost:7287/api/Product/upload/{productId}", content);
                            if (!imageResponse.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Product saved, but failed to upload image.");
                            }
                        }
                    }
                    var newProductPrice = new ProductPrice()
                    {
                        BatchNumber = batchnum,
                        Quantity = quantity,
                        Price = price * quantity,
                        ProductId = productId,
                        EndDate = this.endDatePicker.Value,
                        StartDate = this.startDatePicker.Value
                    };
                    var response1 = await httpClient.PostAsJsonAsync("https://localhost:7287/api/ProductPrice", new { productId = newProductPrice.ProductId, price = newProductPrice.Price, startDate = newProductPrice.StartDate, endDate = newProductPrice.EndDate, quantity = newProductPrice.Quantity, batchNumber = newProductPrice.BatchNumber});
                    if (response1.IsSuccessStatusCode)
                    {
                        var idProd = await response1.Content.ReadFromJsonAsync<ProductPrice>();
                        if (!string.IsNullOrEmpty(this.rowTextBox.TextBoxText) && !string.IsNullOrEmpty(this.placeTextBox.TextBoxText) && !string.IsNullOrEmpty(this.rackTextBox.TextBoxText))
                        {
                            var newPosition = new Position()
                            {
                                Row = row,
                                Rack = rack,
                                Place = place,
                            };

                            var response2 = await httpClient.PostAsJsonAsync("https://localhost:7287/api/Position", new { row = newPosition.Row, rack = newPosition.Rack, place = newPosition.Place});
                            if (response2.IsSuccessStatusCode)
                            {
                                var createdPosition = await response2.Content.ReadFromJsonAsync<Position>();
                                var newPosProd = new PositionProduct()
                                {
                                    IdPos = createdPosition.IdPosition,
                                    IdProd = idProd.PriceId
                                };

                                var response3 = await httpClient.PostAsJsonAsync("https://localhost:7287/api/PositionProduct", new { idPos = newPosProd.IdPos, idProd = newPosProd.IdProd });
                                if (!response3.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Failed to create PositionProduct.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Failed to create Position.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to create ProductPrice.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save product.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            this.DialogResult = DialogResult.OK;
        }


        private async void BtnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // Проверка файла
                        if (System.IO.File.Exists(filePath))
                        {
                            // Загрузка изображения
                            Image image = Image.FromFile(filePath);
                            this.pictureBox.Image = image;
                            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MessageBox.Show("The selected file does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}");
                    }
                }
            }
        }
        private decimal ConvertTry(decimal item, string text, RoundedTextBox box)
        {
            try
            {
                item = Convert.ToDecimal(box.TextBoxText);
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(text + ex);
                return item = 0;
            }
        }

        private int ConvertTry(int item, string text, RoundedTextBox box)
        {
            try
            {
                item = Convert.ToInt32(box.TextBoxText);
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(text + ex);
                return item = 0;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class Category
        {
            public int categoryId { get; set; }
            public string CategoryName { get; set; }
        }
        public class CategoryComBox
        {
            public int categoryId { get; set; }
            public string CategoryName { get; set; }
            public int IndexCategory { get; set; } }
    }
    public class SupplierComBox
    {
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int IndexSupplier { get; set; }
    }
    public class Supplier
    {
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
    }
    public class ProductPrice
    {
        public int PriceId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Quantity { get; set; }

        public string BatchNumber { get; set; }
    }
    public class Position
    {
        public int IdPosition { get; set; }

        public int Row { get; set; }

        public int Rack { get; set; }

        public int Place { get; set; }
    }
    public class PositionProduct
    {
        public int IdPosProd { get; set; }

        public int IdProd { get; set; }

        public int IdPos { get; set; }
    }
}
