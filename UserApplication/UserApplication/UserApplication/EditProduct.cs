using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApplication.FormElementClasses;
using static UserApplication.AddProductForm;

namespace UserApplication
{
    public partial class EditProduct : Form
    {
        private Label positionHeaderLabel;
        private Label productHeaderLabel;
        private Label labelName;
        private Label labelCategory;
        private Label labelSupplier;
        private Label labelPrice;
        private Label labelQuantity;
        private int _productId;
        private HttpClient _httpClient;
        private DataGridView productPricesDataGridView;
        private DataGridView positionsDataGridView;
        private PictureBox pictureBox;
        private Product responseprod;
        private List<ProductPrice> responseprodprice;
        private List<PositionProduct> responsepositionprod;
        private List<PositionProductGrid> pos;
        private Position responsepos;
        private bool editimage = false;

        public EditProduct(int productId, HttpClient httpClients)
        {
            _httpClient = httpClients;
            _productId = productId;
            pos = new List<PositionProductGrid>();
            responseprodprice = new List<ProductPrice>();
            InitializeComponent();
            LoadProductDetails();
        }
        private async void LoadProductDetails()
        {
            try
            {
                responseprod = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{_productId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}");
            }
            var imageUrl = $"https://localhost:7287/api/Product/image/{responseprod.ProductId}";
            try
            {
                responseprodprice = await _httpClient.GetFromJsonAsync<List<ProductPrice>>($"https://localhost:7287/api/ProductPrice/product/{_productId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}");
            }
            if (responseprodprice != null)
            {
                foreach (var poss in responseprodprice)
                {
                    try
                    {
                        if(responsepositionprod != null)
                            responsepositionprod.Clear();
                        responsepositionprod = await _httpClient.GetFromJsonAsync<List<PositionProduct>>($"https://localhost:7287/api/PositionProduct/product/{poss.PriceId}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading product details: {ex.Message}");
                    }
                    if (responsepositionprod != null)
                    {
                        foreach (var item in responsepositionprod)
                        {
                            try
                            {
                                responsepos = await _httpClient.GetFromJsonAsync<Position>($"https://localhost:7287/api/Position/{item.IdPos}");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error loading product details: {ex.Message}");
                            }
                            var po = new PositionProductGrid()
                            {
                                IdPosProd = item.IdPosProd,
                                IdProd = item.IdProd,
                                PositionId = responsepos.IdPosition,
                                Row = responsepos.Row,
                                Rack = responsepos.Rack,
                                Place = responsepos.Place
                            };
                            pos.Add(po);
                        }
                    }
                }
            }
            if (responseprod != null)
            {
                lblProductName.TextBoxText = responseprod.ProductName;
                lblCategoryName.TextBoxText = responseprod.categoryId.ToString();
                lblSupplierName.TextBoxText = responseprod.supplierId.ToString();
                lblUnitPrice.TextBoxText = responseprod.UnitPrice.ToString("C");
                lblQuantity.TextBoxText = responseprod.StockQuantity.ToString();
                roundedTextBox1.TextBoxText = responseprod.Description;
                try
                {
                    pictureBox.Image = Image.FromStream(await _httpClient.GetStreamAsync(imageUrl));
                }
                catch
                {
                    pictureBox.Image = Image.FromFile("RegularImage/NoPhoto.png");
                }
                if (responseprodprice != null)
                {
                    pos.Add(new PositionProductGrid());
                    responseprodprice.Add(new ProductPrice() { ProductId = responseprod.ProductId });
                    positionsDataGridView.DataSource = null;
                    positionsDataGridView.DataSource = pos;
                    productPricesDataGridView.DataSource = null;
                    productPricesDataGridView.DataSource = responseprodprice;
                }
                else
                {
                    pos.Add(new PositionProductGrid());
                    responseprodprice.Add(new ProductPrice() { ProductId = responseprod.ProductId });
                    positionsDataGridView.DataSource = null;
                    positionsDataGridView.DataSource = pos;
                    productPricesDataGridView.DataSource = null;
                    productPricesDataGridView.DataSource = responseprodprice;
                }
            }
           
        }
        private async void buttonDeleteProductPrice_Click(object sender, EventArgs e)
        {
            if (productPricesDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = productPricesDataGridView.SelectedRows[0];
                var detail = selectedRow.DataBoundItem as ProductPrice;

                if ((detail != null && detail.PriceId != 0) || (detail != null && detail.PriceId != -1))
                {
                    try
                    { 
                        var response = await _httpClient.DeleteAsync($"https://localhost:7287/api/ProductPrice/{detail.PriceId}");
                        
                        if (response.IsSuccessStatusCode)
                        {
                            responseprodprice.Remove(detail);
                            productPricesDataGridView.DataSource = null;
                            productPricesDataGridView.DataSource = responseprodprice;
                            foreach (var item in pos)
                            {
                                var response3 = await _httpClient.DeleteAsync($"https://localhost:7287/api/PositionProduct/{item.IdPosProd}");
                                var response4 = await _httpClient.DeleteAsync($"https://localhost:7287/api/Position/{item.PositionId}");
                                if (response3.IsSuccessStatusCode && response4.IsSuccessStatusCode)
                                {
                                    pos.Remove(item);
                                    positionsDataGridView.DataSource = null;
                                    positionsDataGridView.DataSource = pos;
                                }
                            }
                            MessageBox.Show("Delete success");
                        }
                        else
                        {
                            MessageBox.Show("Error delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (detail.PriceId == 0 || detail.PriceId == -1)
                {
                    responseprodprice.Remove(detail);
                    productPricesDataGridView.DataSource = null;
                    productPricesDataGridView.DataSource = responseprodprice;
                    MessageBox.Show("New row delete");
                }
            }
            else
            {
                MessageBox.Show("U need select row");
            }
        }

        private async void buttonUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            Image image = Image.FromFile(filePath);
                            this.pictureBox.Image = image;
                            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            this.editimage = true;
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
        private void buttonDeleteImage_Click(object sender, EventArgs e)
        {
            if (this.pictureBox.Image != null)
            {
                this.pictureBox.Image = null;
                this.editimage = true;
            }
            else
            {
                MessageBox.Show("No image to delete.");
            }
        }

        private async void buttonDeletePosition_Click(object sender, EventArgs e)
        {
            if (positionsDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = positionsDataGridView.SelectedRows[0];
                var position = selectedRow.DataBoundItem as PositionProductGrid;

                if (position != null && position.IdPosProd != 0 && position.IdPosProd != -1)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"https://localhost:7287/api/PositionProduct/{position.IdPosProd}");
                        if (response.IsSuccessStatusCode)
                        {
                            pos.Remove(position);
                            positionsDataGridView.DataSource = null;
                            positionsDataGridView.DataSource = pos;
                            MessageBox.Show("Delete success");
                        }
                        else
                        {
                            MessageBox.Show("Error delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (position.IdPosProd == 0 || position.IdPosProd == -1)
                {
                    pos.Remove(position);
                    positionsDataGridView.DataSource = null;
                    positionsDataGridView.DataSource = pos;
                    MessageBox.Show("New row delete");
                }
            }
            else
            {
                MessageBox.Show("U need select row");
            }
        }
        private void productPriceDetailsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex == responseprodprice.Count - 1)
            {
                var detail = responseprodprice[e.RowIndex];
                if (detail.PriceId != 0)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        responseprodprice.Add(new ProductPrice() { ProductId = responseprod.ProductId });
                        productPricesDataGridView.DataSource = null;
                        productPricesDataGridView.DataSource = responseprodprice;
                        productPricesDataGridView.ClearSelection();
                        productPricesDataGridView.Rows[productPricesDataGridView.Rows.Count - 1].Selected = false;
                    }));
                }
            }
        }

        private void PaymentsDetailsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex == pos.Count - 1)
            {
                var detail = pos[e.RowIndex];
                if (detail.IdPosProd == -1)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        pos.Add(new PositionProductGrid());
                        positionsDataGridView.DataSource = null;
                        positionsDataGridView.DataSource = pos;
                        positionsDataGridView.ClearSelection();
                        positionsDataGridView.Rows[positionsDataGridView.Rows.Count - 1].Selected = false;
                    }));
                }
            }
        }

        private async void SaveChanges(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblProductName.TextBoxText) ||
                string.IsNullOrWhiteSpace(lblCategoryName.TextBoxText) ||
                string.IsNullOrWhiteSpace(lblSupplierName.TextBoxText) ||
                string.IsNullOrWhiteSpace(lblUnitPrice.TextBoxText) ||
                string.IsNullOrWhiteSpace(lblQuantity.TextBoxText) ||
                string.IsNullOrWhiteSpace(roundedTextBox1.TextBoxText)) 
            {
                MessageBox.Show("Please fill in all product fields.");
                return;
            }
            try
            {
                var res = await _httpClient.PutAsJsonAsync($"https://localhost:7287/api/Product/{responseprod.ProductId}", new
                {
                    ProductName = lblProductName.TextBoxText,
                    Description = roundedTextBox1.TextBoxText,
                    CategoryId = lblCategoryName.TextBoxText,
                    SupplierId = lblSupplierName.TextBoxText,
                    StockQuantity = lblQuantity.TextBoxText,
                    UnitPrice = lblUnitPrice.TextBoxText
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            foreach (var detail in responseprodprice)
            {
                if (detail.PriceId != 0)
                {
                    if (detail.Price <= 0 || detail.Quantity <= 0 || string.IsNullOrWhiteSpace(detail.BatchNumber))
                    {
                        MessageBox.Show("Please fill in all product price fields.");
                        return;
                    }
                }
            }
            foreach (var position in pos)
            {
                if (position.IdPosProd != 0)
                {
                    if (position.Row <= 0 || position.Rack <= 0 || position.Place <= 0)
                    {
                        MessageBox.Show("Please fill in all position fields.");
                        return;
                    }
                }
            }
            foreach (var detail in responseprodprice)
            {
                if (detail.PriceId == -1)
                {
                    try
                    {
                        var res = await _httpClient.PostAsJsonAsync($"https://localhost:7287/api/ProductPrice", new
                        {
                            ProductId = responseprod.ProductId,
                            Price = detail.Price,
                            StartDate = detail.StartDate,
                            EndDate = detail.EndDate,
                            Quantity = detail.Quantity,
                            BatchNumber = detail.BatchNumber
                        });
                        if (res.IsSuccessStatusCode)
                        {
                            var newDetail = await res.Content.ReadFromJsonAsync<ProductPrice>();
                            detail.PriceId = newDetail.PriceId;
                            foreach (var item in pos)
                            {
                                if(item.IdProd == 0)
                                {
                                    item.IdProd = newDetail.PriceId;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error creating new product price.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (detail.PriceId != 0)
                {
                    try
                    {
                        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7287/api/ProductPrice/{detail.PriceId}", new
                        {
                            ProductId = responseprod.ProductId,
                            Price = detail.Price,
                            StartDate = detail.StartDate,
                            EndDate = detail.EndDate,
                            Quantity = detail.Quantity,
                            BatchNumber = detail.BatchNumber
                        });
                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error updating product price.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            foreach (var position in pos)
            {
                if (position.IdPosProd == -1)
                {
                    if (responseprodprice.Any(p => p.PriceId == -1))
                    {
                        MessageBox.Show("Error creating position product. Create new product price first.");
                        return;
                    }

                    try
                    {
                        var response1 = await _httpClient.PostAsJsonAsync($"https://localhost:7287/api/Position", new
                        {
                            Row = position.Row,
                            Rack = position.Rack,
                            Place = position.Place
                        });
                        foreach (var detail in responseprodprice)
                        {
                            if (response1.IsSuccessStatusCode)
                            {
                                var newPosition = await response1.Content.ReadFromJsonAsync<Position>();
                                position.PositionId = newPosition.IdPosition;

                                var response2 = await _httpClient.PostAsJsonAsync($"https://localhost:7287/api/PositionProduct", new
                                {
                                    IdProd = position.IdProd,
                                    IdPos = position.PositionId
                                });

                                if (response2.IsSuccessStatusCode)
                                {
                                    var newPositionProduct = await response2.Content.ReadFromJsonAsync<PositionProductGrid>();
                                    position.IdPosProd = newPositionProduct.IdPosProd;
                                }
                                else
                                {
                                    MessageBox.Show("Error creating new position product.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error creating new position.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (position.IdProd == -1)
                {
                    var newProductPrice = responseprodprice.FirstOrDefault(p => p.PriceId != -1);
                    if (newProductPrice == null)
                    {
                        MessageBox.Show("Error updating position product. Create new product price first.");
                        return;
                    }
                    position.IdProd = newProductPrice.PriceId;
                }
            }
            if (this.editimage)
            {
                if (pictureBox.Image != null)
                {
                    Image productImage = this.pictureBox.Image;
                    using (var memoryStream = new MemoryStream())
                    {
                        productImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes = memoryStream.ToArray();
                        var content = new MultipartFormDataContent();

                        var byteArrayContent = new ByteArrayContent(imageBytes);
                        byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                        content.Add(byteArrayContent, "file", $"prod{responseprod.ProductId}.png");

                        var imageResponse = await _httpClient.PostAsync($"https://localhost:7287/api/Product/upload/{responseprod.ProductId}", content);
                        if (!imageResponse.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Product saved, but failed to upload image.");
                        }
                    }
                }
                else if (pictureBox.Image == null)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"https://localhost:7287/api/Product/image/{responseprod.ProductId}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product image: {ex.Message}");
                    }
                }
            }

            MessageBox.Show("Save changes completed successfully.");
            this.DialogResult = DialogResult.OK;
        }

        public class PositionProductGrid
        { 
            public int IdPosProd {  get; set; }
            
            public int IdProd { get; set; }
            
            public int PositionId { get; set; }

            public int Row {  get; set; }

            public int Rack {  get; set; }

            public int Place { get; set; }
        }

    }
}
