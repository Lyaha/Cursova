using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UserApplication.AddProductForm;

namespace UserApplication
{
    public partial class ProductDetailsForm : Form
    {
        private int _productId;
        private HttpClient _httpClient;
        private Label lblProductName;
        private Label lblCategoryName;
        private Label lblSupplierName;
        private Label lblUnitPrice;
        private Label lblQuantity;
        private DataGridView productPricesDataGridView;
        private DataGridView positionsDataGridView;
        private PictureBox pictureBox;

        public ProductDetailsForm(int productId, HttpClient httpClients)
        {
            _productId = productId;
            _httpClient = httpClients;
            InitializeComponent();
            LoadProductDetails();
        }

        private async void LoadProductDetails()
        {
            var response = await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{_productId}");
            Category response1 = new Category();
            Supplier response2 = new Supplier();
            List<ProductPrice> response3 = new List<ProductPrice>();
            List<PositionProduct> response4 = new List<PositionProduct>();
            var imageUrl = $"https://localhost:7287/api/Product/image/{response.ProductId}";
            try
            {
                response1 = await _httpClient.GetFromJsonAsync<Category>($"https://localhost:7287/api/Category/{response.categoryId}");
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                response2 = await _httpClient.GetFromJsonAsync<Supplier>($"https://localhost:7287/api/Suppliers/{response.supplierId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            List<Position> pos = new List<Position>();
            try
            {
                response3 = await _httpClient.GetFromJsonAsync<List<ProductPrice>>($"https://localhost:7287/api/ProductPrice/product/{_productId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (response3 != null)
            {
                foreach (var poss in response3)
                {
                    try
                    {
                        if (response4 != null)
                            response4.Clear();
                        response4 = await _httpClient.GetFromJsonAsync<List<PositionProduct>>($"https://localhost:7287/api/PositionProduct/product/{poss.PriceId}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    if (response4 != null)
                    {
                        foreach (var item in response4)
                        {
                            pos.Add(await _httpClient.GetFromJsonAsync<Position>($"https://localhost:7287/api/Position/{item.IdPos}"));
                        }
                    }
                }
            }
            if (response != null)
            {
                lblProductName.Text = response.ProductName;
                if (response1 != null)
                    lblCategoryName.Text = response1.CategoryName;
                if (response2 != null)
                    lblSupplierName.Text = response2.supplierName;
                lblUnitPrice.Text = response.UnitPrice.ToString("C");
                lblQuantity.Text = response.StockQuantity.ToString();

                try
                {
                    pictureBox.Image = Image.FromStream(await _httpClient.GetStreamAsync(imageUrl));
                }
                catch
                {
                    pictureBox.Image = Image.FromFile("RegularImage/NoPhoto.png");
                }
                if (response3 != null)
                {
                    productPricesDataGridView.DataSource = response3;
                    positionsDataGridView.DataSource = pos;
                }
            }
        }
    }
}
