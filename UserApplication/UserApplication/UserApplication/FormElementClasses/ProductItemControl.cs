using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UserApplication.FormElementClasses
{
    public class ProductItemControl : UserControl
    {
        private PictureBox productPictureBox;
        private Label productNameLabel;
        private Label productIdLabel;
        private Label lastUpdatedLabel;
        private Label quantityLabel;
        private Button detailButton;
        private Button editButton;
        private HttpClient httpClient;
        private int Id;

        public ProductItemControl(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.productPictureBox = new PictureBox();
            this.productNameLabel = new Label();
            this.productIdLabel = new Label();
            this.lastUpdatedLabel = new Label();
            this.quantityLabel = new Label();
            this.detailButton = new Button();
            this.editButton = new Button();


            this.productPictureBox.Size = new Size(100, 100);
            this.productPictureBox.Location = new Point(10, 10);
            this.productPictureBox.SizeMode = PictureBoxSizeMode.Zoom;


            this.productNameLabel.Location = new Point(10, 120);
            this.productNameLabel.Size = new Size(200, 25);
            this.productNameLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            this.productNameLabel.Text = "Product Name";

            this.productIdLabel.Location = new Point(10, 145);
            this.productIdLabel.Font = new Font("Arial", 9, FontStyle.Bold);
            this.productNameLabel.Size = new Size(200, 15);
            this.productIdLabel.AutoSize = true;


            this.lastUpdatedLabel.Location = new Point(10, 170);
            this.lastUpdatedLabel.Size = new Size(200, 20);
            this.lastUpdatedLabel.Font = new Font("Arial", 9);
            this.lastUpdatedLabel.Text = "Last Updated: 01/01/2024";


            this.quantityLabel.Location = new Point(10, 190);
            this.quantityLabel.Size = new Size(200, 20);
            this.quantityLabel.Font = new Font("Arial", 9);
            this.quantityLabel.Text = "Quantity: 100";


            this.detailButton = new RoundedButton();
            this.detailButton.Location = new Point(10, 210);
            this.detailButton.Size = new Size(120, 30);
            this.detailButton.FlatStyle = FlatStyle.Flat;
            this.detailButton.FlatAppearance.BorderSize = 0;
            this.detailButton.BackColor = Color.DodgerBlue;
            this.detailButton.ForeColor = Color.White;
            this.detailButton.Text = "Details";
            this.detailButton.Font = new Font("Arial", 9, FontStyle.Bold);
            this.detailButton.TextAlign = ContentAlignment.MiddleCenter;
            this.detailButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.detailButton.Click += DetailButton_Click;


            this.editButton = new RoundedButton
            {

                Icon = Image.FromFile("RegularImage/edit_icon.png"),
                IconAlign = ContentAlignment.MiddleCenter
            };
            this.editButton.Location = new Point(140, 210);
            this.editButton.Size = new Size(50, 30);
            this.editButton.FlatStyle = FlatStyle.Flat;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.BackColor = Color.Orange;
            this.editButton.ForeColor = Color.White;
            this.editButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.editButton.Click += EditButton_Click;

            this.Controls.Add(this.productPictureBox);
            this.Controls.Add(this.productIdLabel);
            this.Controls.Add(this.productNameLabel);
            this.Controls.Add(this.lastUpdatedLabel);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.detailButton);
            this.Controls.Add(this.editButton);


            this.Size = new Size(210, 250);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            var editProduct = new EditProduct(Id, httpClient);
            if (editProduct.ShowDialog() == DialogResult.OK)
            {
                var response = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{Id}");
                Image productImage = null;
                try
                {
                    var imageUrl = $"https://localhost:7287/api/Product/image/{response.ProductId}";
                    productImage = Image.FromStream(await httpClient.GetStreamAsync(imageUrl));
                }
                catch
                {
                    productImage = Image.FromFile("RegularImage/NoPhoto.png");
                }
                this.productNameLabel.Text = response.ProductName;
                this.lastUpdatedLabel.Text = $"Last Updated: {response.LastUpdated.ToShortDateString()}";
                this.quantityLabel.Text = $"Stock Quantity: {response.StockQuantity}";
                this.productIdLabel.Text = $"ID: {response.ProductId}";
                this.productPictureBox.Image = productImage;
            }
        }

        private void DetailButton_Click(object sender, EventArgs e)
        {
            var addProductForm = new ProductDetailsForm(Id, httpClient);
            addProductForm.ShowDialog();
        }

        public void SetProductData(string name, DateTime lastUpdated, int stockQuantity, int productId, Image productImage)
        {
            this.Id = productId;
            this.productNameLabel.Text = name;
            this.lastUpdatedLabel.Text = $"Last Updated: {lastUpdated.ToShortDateString()}";
            this.quantityLabel.Text = $"Stock Quantity: {stockQuantity}";
            this.productIdLabel.Text = $"ID: {productId}";
            this.productPictureBox.Image = productImage;
        }
    }
}
