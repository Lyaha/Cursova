using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static UserApplication.AddProductForm;
using static UserApplication.UsersForm;

namespace UserApplication.FormElementClasses
{
    public class CustomDataGridView : Panel
    {
        private List<Category> categories;
        private List<Supplier> supplier;
        private Color headerColor = Color.Orange;
        private Color evenRowColor = Color.LightGray;
        private Color oddRowColor = Color.Gray;
        private int rowHeight = 40;
        private HttpClient httpClient;
        private Panel rowPanel1;

        public CustomDataGridView(HttpClient httpClients)
        {
            this.categories = new List<Category>();
            this.AutoScroll = true;
            this.Paint += CustomDataGridView_Paint;
            httpClient = httpClients;
        }


        private void InitializeComponent()
        {
        }

        public void SetData(List<Category> data)
        {
            this.categories = data;
            this.Controls.Clear();
            AddRows();
        }
        private void CustomDataGridView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            // Draw header
            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("Category ID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Category Name", this.Font, Brushes.White, new PointF(110, y + 5));
        }

        private void AddRows()
        {
            int y = rowHeight;

            for (int i = 0; i < categories.Count; i++)
            {
                var rowPanel = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight),
                    BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor
                };

                var idLabel = new Label
                {
                    Text = categories[i].categoryId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(idLabel);

                var categoryLabel = new Label
                {
                    Text = categories[i].CategoryName,
                    Location = new Point(110, 5),
                    Size = new Size(200, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(categoryLabel);

                var editButton = new RoundButton
                {
                    Location = new Point(this.Width - 140, 5),
                    Size = new Size(30, 30),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    BackColor = Color.DodgerBlue,
                    ForeColor = Color.White,
                    Text = "✏️",
                    Tag = categories[i]
                };
                editButton.Click += EditButton_Click;
                rowPanel.Controls.Add(editButton);

                var deleteButton = new RoundButton
                {
                    Location = new Point(this.Width - 70, 5),
                    Size = new Size(30, 30),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    Text = "🗑️",
                    Tag = categories[i]
                };
                deleteButton.Click += DeleteButton_Click;
                rowPanel.Controls.Add(deleteButton);

                this.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button.Tag as Category;
            string newCategoryName = Prompt.ShowDialog("Enter edit category name:", "Edit Category");
            if (!string.IsNullOrEmpty(newCategoryName))
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Category/{category.categoryId}", new { CategoryName = newCategoryName });
                if (response.IsSuccessStatusCode)
                {
                    var response2 = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7287/api/Category");
                    SetData(response2);
                }
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button.Tag as Category;
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7287/api/Product/category/{category.categoryId}");
                if (response.Count != 0)
                {
                    string newCategoryName = Prompt.ShowDialog("Enter new category id for products:", $"({response.Count} product(-s) need new category)", "Edit Category");
                    if (!string.IsNullOrEmpty(newCategoryName))
                    {
                        try
                        {
                            foreach (var item in response)
                            {
                                await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Product/{item.ProductId}", new { ProductName = item.ProductName, categoryId = newCategoryName });
                            }
                            await httpClient.DeleteAsync($"https://localhost:7287/api/Category/{category.categoryId}");
                            var response2 = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7287/api/Category");
                            SetData(response2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            catch (HttpRequestException ex) when (ex.Message == "Код состояния ответа не указывает на успешное выполнение: 404 (Not Found).")
            {
                var confirmResult = MessageBox.Show(
                                    $"Are you sure you want to delete the category '{category.CategoryName}'?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    await httpClient.DeleteAsync($"https://localhost:7287/api/Category/{category.categoryId}");
                    var response2 = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7287/api/Category");
                    SetData(response2);
                }
            }
        }

        ///---------------------------------------------------------------------------Supplier-------------------------------------------------
        public CustomDataGridView(HttpClient httpClients, string Supplier)
        {
            this.supplier = new List<Supplier>();
            this.AutoScroll = true;
            this.Paint += CustomDataGridView_PaintSup;
            httpClient = httpClients;
        }

        public void SetDataSup(List<Supplier> data)
        {
            this.supplier = data;
            this.Controls.Clear();
            AddRowsSup();
        }

        private void CustomDataGridView_PaintSup(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("Supplier ID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Name", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Contact Person", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Email", this.Font, Brushes.White, new PointF(400, y + 5));
            g.DrawString("Phone", this.Font, Brushes.White, new PointF(550, y + 5));
        }

        private void AddRowsSup()
        {
            int y = rowHeight;

            for (int i = 0; i < supplier.Count; i++)
            {
                var rowPanel = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight),
                    BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor
                };

                var supplierIdLabel = new Label
                {
                    Text = supplier[i].supplierId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(supplierIdLabel);

                var supplierNameLabel = new Label
                {
                    Text = supplier[i].supplierName,
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(supplierNameLabel);

                var contactPersonLabel = new Label
                {
                    Text = supplier[i].ContactPerson,
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(contactPersonLabel);

                var contactEmailLabel = new Label
                {
                    Text = supplier[i].ContactEmail,
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(contactEmailLabel);

                var contactPhoneLabel = new Label
                {
                    Text = supplier[i].ContactPhone,
                    Location = new Point(550, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(contactPhoneLabel);

                var editButton = new RoundButton
                {
                    Location = new Point(690, 5),
                    Size = new Size(60, 30),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    BackColor = Color.DodgerBlue,
                    ForeColor = Color.White,
                    Text = "✏️",
                    Tag = supplier[i]
                };
                editButton.Click += EditButton_ClickSup;
                rowPanel.Controls.Add(editButton);

                var deleteButton = new RoundButton
                {
                    Location = new Point(730, 5),
                    Size = new Size(60, 30),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    Text = "🗑️",
                    Tag = supplier[i]
                };
                deleteButton.Click += DeleteButton_ClickSup;
                rowPanel.Controls.Add(deleteButton);

                this.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }
        private async void EditButton_ClickSup(object sender, EventArgs e)
        {
            var button = sender as Button;
            var supplier = button.Tag as Supplier;
            var newSupplier = Prompt.ShowDialog(supplier, "Edit supplier");
            if (newSupplier != null)
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Suppliers/{supplier.supplierId}", new { supplierName = newSupplier.supplierName, contactPerson = newSupplier.ContactPerson, contactEmail = newSupplier.ContactEmail, contactPhone = newSupplier.ContactPhone});
                if (response.IsSuccessStatusCode)
                {
                    var response2 = await httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7287/api/Suppliers");
                    SetDataSup(response2);
                }
            }
        }

        private async void DeleteButton_ClickSup(object sender, EventArgs e)
        {
            var button = sender as Button;
            var supplier = button.Tag as Supplier;
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7287/api/Product/supplier/{supplier.supplierId}");
                if (response.Count != 0)
                {
                    string newCategoryName = Prompt.ShowDialog("Enter new supplier id for products:", $"({response.Count} product(-s) need new supplier)", "Edit supplier");
                    if (!string.IsNullOrEmpty(newCategoryName))
                    {
                        try
                        {
                            foreach (var item in response)
                            {
                                await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Product/{item.ProductId}", new { ProductName = item.ProductName, supplierId = newCategoryName });
                            }
                            await httpClient.DeleteAsync($"https://localhost:7287/api/Suppliers/{supplier.supplierId}");
                            var response2 = await httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7287/api/Suppliers");
                            SetDataSup(response2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            catch (HttpRequestException ex) when (ex.Message == "Код состояния ответа не указывает на успешное выполнение: 404 (Not Found).")
            {
                var confirmResult = MessageBox.Show(
                                    $"Are you sure you want to delete the supplier '{supplier.supplierName}'?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    await httpClient.DeleteAsync($"https://localhost:7287/api/Suppliers/{supplier.supplierId}");
                    var response2 = await httpClient.GetFromJsonAsync<List<Supplier>>("https://localhost:7287/api/Suppliers");
                    SetDataSup(response2);
                }
            }
        }


        ///---------------------------------------------------------------------------Stocks-------------------------------------------------
        ///
        private List<Stock> stock;
        public CustomDataGridView(HttpClient httpClients, string Stock, string Stocks)
        {
            this.stock = new List<Stock>();
            this.Paint += CustomDataGridView_PaintStocks;
            httpClient = httpClients;
        }

        public void SetDataStocks(List<Stock> data, bool edit)
        {
            this.stock = data;
            this.Controls.Clear();
            AddRowsStocks(edit);
        }

        private void CustomDataGridView_PaintStocks(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("MovementId", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Product name", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Status", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Quantity", this.Font, Brushes.White, new PointF(400, y + 5));
            g.DrawString("MovementDate", this.Font, Brushes.White, new PointF(550, y + 5));
        }
        private void AddRowsStocks(bool edit)
        {
            int y = rowHeight;
            if (!edit)
            { 
                rowPanel1 = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight * 3),
                    AutoScrollPosition = new Point(this.Width + 20, 40),
                    AutoScroll = true

                };
                this.Controls.Add(rowPanel1);
                y = 0;
            }
            else
            {
                rowPanel1 = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, this.Height - rowHeight),
                    AutoScrollPosition = new Point(this.Width + 20, 40),
                    AutoScroll = true

                };
                this.Controls.Add(rowPanel1);
                y = 0;
            }
            for (int i = 0; i < stock.Count; i++)
            {
                Panel rowPanel;
                if (stock.Count > 3 && !edit)
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width - 20, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }
                else
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }

                var MovementIdLabel = new Label
                {
                    Text = stock[i].MovementId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(MovementIdLabel);

                var productNameLabel = new Label
                {
                    Text = stock[i].ProductName,
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(productNameLabel);

                var StatusLabel = new Label
                {
                    Text = stock[i].MovementName,
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(StatusLabel);

                var QuantityLabel = new Label
                {
                    Text = stock[i].Quantity.ToString(),
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(QuantityLabel);

                var MovementDateLabel = new Label
                {
                    Text = stock[i].MovementDate.ToString(),
                    Location = new Point(550, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(MovementDateLabel);
                if (!edit)
                {
                    var deteilButton = new RoundedButton()
                    {
                        Location = new Point(700, 5),
                        Size = new Size(100, rowHeight - 5),
                        BackColor = Color.CornflowerBlue,
                        ForeColor = Color.White,
                        Tag = stock[i],
                        Text = "Deteils"
                    };
                    deteilButton.Click += DeteilButtonStock;
                    rowPanel.Controls.Add(deteilButton);
                    this.rowPanel1.Controls.Add(rowPanel);
                }
                else
                {
                    var editButton = new RoundButton
                    {
                        Location = new Point(690, 5),
                        Size = new Size(60, 30),
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        BackColor = Color.DodgerBlue,
                        ForeColor = Color.White,
                        Text = "✏️",
                        Tag = stock[i]
                    };
                    editButton.Click += EditButton_ClickStock;
                    rowPanel.Controls.Add(editButton);

                    var deleteButton = new RoundButton
                    {
                        Location = new Point(730, 5),
                        Size = new Size(60, 30),
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        BackColor = Color.Red,
                        ForeColor = Color.White,
                        Text = "🗑️",
                        Tag = stock[i]
                    };
                    deleteButton.Click += DeleteButton_ClickStock;
                    rowPanel.Controls.Add(deleteButton);
                    this.rowPanel1.Controls.Add(rowPanel);
                }

                
                y += rowHeight;
            }
        }
        private async void DeteilButtonStock(object sender, EventArgs e)
        {
            var button = sender as Button;
            var stock = button.Tag as Stock;
            StockMovementDetailsForm detailsForm = new StockMovementDetailsForm(stock);
            detailsForm.ShowDialog();
        }
        private async void EditButton_ClickStock(object sender, EventArgs e)
        {
            var button = sender as Button;
            var stock = button.Tag as Stock;
            this.stock.Clear();
            EditStockMovementForm detailsForm = new EditStockMovementForm(httpClient, stock, true);
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                List<Stock> stocks2 = new List<Stock>();
                try
                {
                    var response = await httpClient.GetFromJsonAsync<List<Stock>>("https://localhost:7287/api/StockMovements");
                    foreach (var item in response)
                    {
                        var response2 = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{item.ProductId}");
                        var response3 = await httpClient.GetFromJsonAsync<MovementType>($"https://localhost:7287/api/MovemenTypes/{item.MovementTypeId}");
                        var stock2 = new Stock()
                        {
                            MovementId = item.MovementId,
                            ProductId = item.ProductId,
                            ProductName = response2.ProductName,
                            MovementTypeId = item.MovementTypeId,
                            MovementName = response3.TypeName,
                            Quantity = item.Quantity,
                            MovementDate = item.MovementDate,
                            BatchNumber = item.BatchNumber,
                            Notes = item.Notes
                        };
                        this.stock.Add(stock2);

                    }
                    SetDataStocks(this.stock, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async void DeleteButton_ClickStock(object sender, EventArgs e)
        {
            var button = sender as Button;
            var stock = button.Tag as Stock;
            this.stock.Clear();
            var confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete the Stock Movements '{stock.MovementId.ToString()}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
                await httpClient.DeleteAsync($"https://localhost:7287/api/StockMovements/{stock.MovementId}");

            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Stock>>("https://localhost:7287/api/StockMovements");
                foreach (var item in response)
                {
                    var response2 = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{item.ProductId}");
                    var response3 = await httpClient.GetFromJsonAsync<MovementType>($"https://localhost:7287/api/MovemenTypes/{item.MovementTypeId}");
                    var stock2 = new Stock()
                    {
                        MovementId = item.MovementId,
                        ProductId = item.ProductId,
                        ProductName = response2.ProductName,
                        MovementTypeId = item.MovementTypeId,
                        MovementName = response3.TypeName,
                        Quantity = item.Quantity,
                        MovementDate = item.MovementDate,
                        BatchNumber = item.BatchNumber,
                        Notes = item.Notes
                    };
                    this.stock.Add(stock2);

                }
                SetDataStocks(this.stock, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ///---------------------------------------------------------------------------Orders-------------------------------------------------
        ///
        private List<Order> orders;
        public CustomDataGridView(HttpClient httpClients, string Orders, string Order, string Orders1)
        {
            this.orders = new List<Order>();
            this.Paint += CustomDataGridView_PaintOrders;
            httpClient = httpClients;
        }

        public void SetDataOrders(List<Order> data, bool edit)
        {
            this.orders = data;
            this.Controls.Clear();
            AddRowsOrders(edit);
        }

        private void CustomDataGridView_PaintOrders(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("OrderID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Buyer Name", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Amount", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Status", this.Font, Brushes.White, new PointF(400, y + 5));
            g.DrawString("Order Date", this.Font, Brushes.White, new PointF(550, y + 5));
        }
        private void AddRowsOrders(bool edit)
        {
            int y = rowHeight;
            if (!edit)
            {
                rowPanel1 = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight * 3),
                    AutoScrollPosition = new Point(this.Width + 20, 40),
                    AutoScroll = true

                };
                this.Controls.Add(rowPanel1);
                y = 0;
            }
            else
            {
                rowPanel1 = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, this.Width - rowHeight),
                    AutoScrollPosition = new Point(this.Width + 20, 40),
                    AutoScroll = true
                };
                this.Controls.Add(rowPanel1);
                y = 0;
            }
            for (int i = 0; i < orders.Count; i++)
            {
                Panel rowPanel;
                if (orders.Count > 3 && !edit)
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width - 20, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }
                else
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }

                var OrderIDLabel = new Label
                {
                    Text = orders[i].OrderId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(OrderIDLabel);

                var BuyerLabel = new Label
                {
                    Text = orders[i].BuyerName,
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(BuyerLabel);

                var AmountsLabel = new Label
                {
                    Text = orders[i].Amounts.ToString(),
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(AmountsLabel);

                var StatusLabel = new Label
                {
                    Text = orders[i].StatusName,
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(StatusLabel);

                var OrderDateLabel = new Label
                {
                    Text = orders[i].OrderDate.ToString(),
                    Location = new Point(550, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(OrderDateLabel);

                if (!edit)
                {
                    var deteilButton = new RoundedButton()
                    {
                        Location = new Point(700, 5),
                        Size = new Size(100, rowHeight - 5),
                        BackColor = Color.CornflowerBlue,
                        ForeColor = Color.White,
                        Tag = orders[i],
                        Text = "Deteils"
                    };
                    deteilButton.Click += DeteilButtonOrder;
                    rowPanel.Controls.Add(deteilButton);
                    this.rowPanel1.Controls.Add(rowPanel);
                }
                else
                {
                    var editButton = new RoundButton
                    {
                        Location = new Point(690, 5),
                        Size = new Size(60, 30),
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        BackColor = Color.DodgerBlue,
                        ForeColor = Color.White,
                        Text = "✏️",
                        Tag = orders[i]
                    };
                    editButton.Click += EditButton_ClickOrder;
                    rowPanel.Controls.Add(editButton);
                    this.rowPanel1.Controls.Add(rowPanel);
                }
                y += rowHeight;
            }
        }
        private async void DeteilButtonOrder(object sender, EventArgs e)
        {
            var button = sender as Button;
            var order = button.Tag as Order;
            OrderDetailsForm detailsForm = new OrderDetailsForm(order, httpClient);
            detailsForm.ShowDialog();
        }
        private async void EditButton_ClickOrder(object sender, EventArgs e)
        {
            var button = sender as Button;
            var order = button.Tag as Order;
            if (order.StatusName != "Finished")
            {
                this.orders.Clear();
                EditOrderForm detailsForm = new EditOrderForm(order, true, httpClient);
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {
                    List<Order> orders = new List<Order>();
                    Order order1 = new Order();
                    try
                    {
                        var response = await httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7287/api/Order");
                        foreach (var item in response)
                        {
                            var response2 = await httpClient.GetFromJsonAsync<List<Payments>>($"https://localhost:7287/api/Payment/order/{item.OrderId}");
                            var response3 = await httpClient.GetFromJsonAsync<Buyer>($"https://localhost:7287/api/Buyer/{item.BuyerId}");
                            var respons4 = await httpClient.GetFromJsonAsync<OrdersStatus>($"https://localhost:7287/api/OrderStatus/{item.StatusId}");
                            foreach (var item1 in response2)
                            {
                                order1 = new Order()
                                {
                                    OrderId = item.OrderId,
                                    OrderDate = item.OrderDate,
                                    BuyerId = item.BuyerId,
                                    BuyerName = response3.name,
                                    StatusId = item.StatusId,
                                    StatusName = respons4.StatusName,
                                    Amounts = item1.Amount
                                };
                            }
                            orders.Add(order1);
                        }
                        SetDataOrders(orders, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading orders: {ex.Message}");
                    }
                }
            }
        }

        ///------------------------------------------------------------OrderDeteils--------------------------------------------------------
        ///
        private List<OrdersDeteils> orderdet;
        public CustomDataGridView(HttpClient httpClients, string Orders, string Order, string Orders1, string Order2)
        {
            this.orderdet = new List<OrdersDeteils>();
            this.Paint += CustomDataGridView_PaintOrdersDet;
            httpClient = httpClients;
        }

        public void SetDataOrdersDet(List<OrdersDeteils> data)
        {
            this.orderdet = data;
            this.Controls.Clear();
            AddRowsOrdersDet();
        }

        private void CustomDataGridView_PaintOrdersDet(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;
            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("Order detail ID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Product ID", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Quantity", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Unit Price", this.Font, Brushes.White, new PointF(400, y + 5));
        }
        private void AddRowsOrdersDet()
        {
            int y = rowHeight;
            rowPanel1 = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(this.Width, rowHeight * 3),
                AutoScrollPosition = new Point(this.Width + 20, 40),
                AutoScroll = true

            };
            this.Controls.Add(rowPanel1);
            y = 0;
            for (int i = 0; i < orderdet.Count; i++)
            {
                Panel rowPanel;
                if (orderdet.Count > 3)
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width - 20, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }
                else
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }

                var OrderDetailIdLabel = new Label
                {
                    Text = orderdet[i].OrderDetailId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(OrderDetailIdLabel);

                var ProductIdLabel = new Label
                {
                    Text = orderdet[i].ProductId.ToString(),
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(ProductIdLabel);

                var QuantityLabel = new Label
                {
                    Text = orderdet[i].Quantity.ToString(),
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(QuantityLabel);

                var UnitPriceLabel = new Label
                {
                    Text = orderdet[i].UnitPrice.ToString(),
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(UnitPriceLabel);

                this.rowPanel1.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }
        ///
        ///--------------------------------------Paymentss-------
        ///
        private List<Payments> payments;
        public CustomDataGridView(Payments payments, HttpClient httpClients)
        {
            this.payments = new List<Payments>();
            this.Paint += CustomDataGridView_PaintPayments;
            httpClient = httpClients;
        }

        public void SetDataPayments(List<Payments> data)
        {
            this.payments = data;
            this.Controls.Clear();
            AddRowsPayment();
        }

        private void CustomDataGridView_PaintPayments(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;
            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("Payment ID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Amount", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Payment Data", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Metod", this.Font, Brushes.White, new PointF(400, y + 5));
        }
        private void AddRowsPayment()
        {
            int y = rowHeight;
            rowPanel1 = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(this.Width, rowHeight * 3),
                AutoScrollPosition = new Point(this.Width + 20, 40),
                AutoScroll = true

            };
            this.Controls.Add(rowPanel1);
            y = 0;
            for (int i = 0; i < payments.Count; i++)
            {
                Panel rowPanel;
                if (payments.Count > 3)
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width - 20, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }
                else
                {
                    rowPanel = new Panel
                    {
                        Location = new Point(0, y),
                        Size = new Size(this.Width, rowHeight),
                        BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                    };
                }

                var PaymentsIdLabel = new Label
                {
                    Text = payments[i].PaymentId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(PaymentsIdLabel);

                var AmountLabel = new Label
                {
                    Text = payments[i].Amount.ToString(),
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(AmountLabel);

                var PaymentDataLabel = new Label
                {
                    Text = payments[i].PaymentDate.ToString(),
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(PaymentDataLabel);

                var UnitPriceLabel = new Label
                {
                    Text = payments[i].PaymentMethodName.ToString(),
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(UnitPriceLabel);

                this.rowPanel1.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }
        //
        ///
        ///
        private List<GridUser> users;
        public CustomDataGridView(HttpClient httpClients, List<GridUser> user)
        {
            this.users = user;
            this.Paint += CustomDataGridView_PaintUsers;
            httpClient = httpClients;
        }

        public void SetDataUsers(List<GridUser> data)
        {
            this.users = data;
            this.Controls.Clear();
            AddRowsUsers();
        }

        private void CustomDataGridView_PaintUsers(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("UserID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("User Name", this.Font, Brushes.White, new PointF(100, y + 5));
            g.DrawString("Email", this.Font, Brushes.White, new PointF(250, y + 5));
            g.DrawString("Role", this.Font, Brushes.White, new PointF(400, y + 5));
        }
        private void AddRowsUsers()
        {
            int y = rowHeight;

            rowPanel1 = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(this.Width, this.Width - rowHeight),
                AutoScrollPosition = new Point(this.Width + 20, 40),
                AutoScroll = true
            };
            this.Controls.Add(rowPanel1);
            y = 0;
            for (int i = 0; i < users.Count; i++)
            {
                Panel rowPanel;

                rowPanel = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight),
                    BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                };

                var UserIdLabel = new Label
                {
                    Text = users[i].UserId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(UserIdLabel);

                var UsernameLabel = new Label
                {
                    Text = users[i].Username,
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(UsernameLabel);

                var EmailLabel = new Label
                {
                    Text = users[i].Email,
                    Location = new Point(250, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(EmailLabel);

                var RoleNameLabel = new Label
                {
                    Text = users[i].RoleName,
                    Location = new Point(400, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(RoleNameLabel);

                var editButton = new RoundButton
                {
                    Location = new Point(690, 5),
                    Size = new Size(60, 30),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.DodgerBlue,
                    ForeColor = Color.White,
                    Text = "✏️",
                    Tag = users[i]
                };
                editButton.Click += EditButton_ClickUser;
                rowPanel.Controls.Add(editButton);

                var deleteButton = new RoundButton
                {
                    Location = new Point(730, 5),
                    Size = new Size(60, 30),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    Text = "🗑️",
                    Tag = users[i]
                };
                deleteButton.Click += DeleteButtonUser;
                rowPanel.Controls.Add(deleteButton);
                this.rowPanel1.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }
        private async void EditButton_ClickUser(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button.Tag as GridUser;
            EditUserForm userssForm = new EditUserForm(user.UserId, httpClient, true);
            if (userssForm.ShowDialog() == DialogResult.OK)
            {
                List<GridUser> users = new List<GridUser>();
                try 
                {
                    users = await httpClient.GetFromJsonAsync<List<GridUser>>("https://localhost:7287/api/Users");
                    foreach (var item in users)
                    {
                        var rolename = await httpClient.GetFromJsonAsync<Role>($"https://localhost:7287/api/Roles/{item.RoleId}");
                        item.RoleName = rolename.RoleName;
                    }
                    SetDataUsers(users);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading users: {ex.Message}");
                }
            }
        }
        private async void DeleteButtonUser(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button.Tag as GridUser;
            var confirmResult = MessageBox.Show(
                                    $"Are you sure you want to delete the user '{user.Username}'?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                await httpClient.DeleteAsync($"https://localhost:7287/api/Users/{user.UserId}");
                List<GridUser> users = new List<GridUser>();
                try
                {
                    users = await httpClient.GetFromJsonAsync<List<GridUser>>("https://localhost:7287/api/Users");
                    foreach (var item in users)
                    {
                        var rolename = await httpClient.GetFromJsonAsync<Role>($"https://localhost:7287/api/Roles/{item.RoleId}");
                        item.RoleName = rolename.RoleName;
                    }
                    SetDataUsers(users);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading users: {ex.Message}");
                }
            }
        }



        ////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////
        ///
        private List<Role> roles;
        public CustomDataGridView(HttpClient httpClients, List<Role> role)
        {
            this.roles = role;
            this.Paint += CustomDataGridView_PaintRoles;
            httpClient = httpClients;
        }

        public void SetDataRoles(List<Role> data)
        {
            this.roles = data;
            this.Controls.Clear();
            AddRowsRoles();
        }

        private void CustomDataGridView_PaintRoles(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 0;

            g.FillRectangle(new SolidBrush(headerColor), 0, y, this.Width, rowHeight);
            g.DrawString("Role ID", this.Font, Brushes.White, new PointF(10, y + 5));
            g.DrawString("Role Name", this.Font, Brushes.White, new PointF(100, y + 5));
        }
        private void AddRowsRoles()
        {
            int y = rowHeight;

            rowPanel1 = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(this.Width, this.Width - rowHeight),
                AutoScrollPosition = new Point(this.Width + 20, 40),
                AutoScroll = true
            };
            this.Controls.Add(rowPanel1);
            y = 0;
            for (int i = 0; i < roles.Count; i++)
            {
                Panel rowPanel;

                rowPanel = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(this.Width, rowHeight),
                    BackColor = (i % 2 == 0) ? evenRowColor : oddRowColor

                };

                var RoleIdLabel = new Label
                {
                    Text = roles[i].RoleId.ToString(),
                    Location = new Point(10, 5),
                    Size = new Size(50, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(RoleIdLabel);

                var RoleNameLabel = new Label
                {
                    Text = roles[i].RoleName,
                    Location = new Point(100, 5),
                    Size = new Size(100, rowHeight - 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };
                rowPanel.Controls.Add(RoleNameLabel);

                var editButton = new RoundButton
                {
                    Location = new Point(690, 5),
                    Size = new Size(60, 30),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.DodgerBlue,
                    ForeColor = Color.White,
                    Text = "✏️",
                    Tag = roles[i]
                };
                editButton.Click += EditButton_ClickRole;
                rowPanel.Controls.Add(editButton);

                this.rowPanel1.Controls.Add(rowPanel);
                y += rowHeight;
            }
        }
        private async void EditButton_ClickRole(object sender, EventArgs e)
        {
            var button = sender as Button;
            var role = button.Tag as Role;
            string newRoleyName = Prompt.ShowDialog("Enter edit role name:", "Edit Role");
            if (!string.IsNullOrEmpty(newRoleyName))
            {
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7287/api/Roles/{role.RoleId}", new { RoleId = role.RoleId, RoleName = newRoleyName });
                if (response.IsSuccessStatusCode)
                {
                    var response2 = await httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7287/api/Roles");
                    SetDataRoles(response2);
                }
            }
        }
    }
}