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

namespace UserApplication.FormElementClasses
{
    public partial class StockGridForm : Form
    {
        private CustomDataGridView stockGrid;
        private Label lblH1;
        private RoundButton AddBttn;
        private HttpClient httpClient;
        public StockGridForm(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
            LoadStockAsync();
        }
        private void AddButtonClick(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            EditStockMovementForm from = new EditStockMovementForm(httpClient, stock, false);
            if(from.ShowDialog() == DialogResult.OK)
            {
                LoadStockAsync();
            }

        }
        public async Task LoadStockAsync()
        {
            List<Stock> stocks = new List<Stock>();
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Stock>>("https://localhost:7287/api/StockMovements");
                foreach (var item in response)
                {
                    var response2 = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7287/api/Product/{item.ProductId}");
                    var response3 = await httpClient.GetFromJsonAsync<MovementType>($"https://localhost:7287/api/MovemenTypes/{item.MovementTypeId}");
                    var stock = new Stock()
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
                    stocks.Add(stock);

                }
                stockGrid.SetDataStocks(stocks, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stocks: {ex.Message}");
            }
        }
    }
}
