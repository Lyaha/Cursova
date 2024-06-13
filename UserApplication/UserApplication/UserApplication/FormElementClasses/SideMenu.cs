using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;

namespace UserApplication.FormElementClasses
{
    public class SideMenu : UserControl
    {
        private Panel menuPanel;
        private Button toggleButton;
        public Button productButton;
        private Button productExpandButton;
        private Button stockorderExpandButton, userExpandButton;
        public Button stockorderButton;
        public Button userButton;
        private Panel subMenuPanel, subMenuPanel2, subMenuPanel3;
        private Button subItem1Button;
        private Button subItem2Button;
        private Button subItem3Button;
        private Button subItem4Button;
        private Button subItem5Button;
        private Button subItem6Button;
        private Button subItem7Button;
        private Button currentButton;
        private bool isCollapsed = true;
        private Timer animationTimer;
        private bool isExpanded;
        private HttpClient httpClient;

        public event EventHandler MenuToggled;

        public int ExpandedWidth { get; private set; } = 200; // ширина бокового меню в развернутом состоянии

        public SideMenu(HttpClient httpClients)
        {
            httpClient = httpClients;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuPanel = new Panel();
            this.toggleButton = new Button();
            this.productButton = new Button();
            this.productExpandButton = new Button();
            this.stockorderExpandButton = new Button();
            this.userExpandButton = new Button();
            this.stockorderButton = new Button();
            this.userButton = new Button();
            this.subMenuPanel = new Panel();
            this.subMenuPanel2 = new Panel();
            this.subMenuPanel3 = new Panel();
            this.subItem1Button = new Button();
            this.subItem2Button = new Button();
            this.subItem3Button = new Button();
            this.subItem4Button = new Button();
            this.subItem5Button = new Button();
            this.subItem6Button = new Button();
            this.subItem7Button = new Button();
            this.animationTimer = new Timer();

            // Menu Panel
            this.menuPanel.BackColor = Color.Gray;
            this.menuPanel.Width = 60;
            this.menuPanel.Dock = DockStyle.Left;

            // Toggle Button
            this.toggleButton.Text = "☰";
            this.toggleButton.Dock = DockStyle.Top;
            this.toggleButton.Height = 40;
            this.toggleButton.FlatStyle = FlatStyle.Flat;
            this.toggleButton.FlatAppearance.BorderSize = 0;
            this.toggleButton.BackColor = Color.Gray;
            this.toggleButton.ForeColor = Color.White;
            this.toggleButton.Click += new EventHandler(this.ToggleButton_Click);

            // Home Button
            this.productButton.Text = "Product";
            this.productButton.Dock = DockStyle.Top;
            this.productButton.Height = 40;
            this.productButton.FlatStyle = FlatStyle.Flat;
            this.productButton.FlatAppearance.BorderSize = 0;
            this.productButton.BackColor = Color.Gray;
            this.productButton.ForeColor = Color.White;
            this.productButton.TextAlign = ContentAlignment.MiddleLeft;
            this.productButton.Padding = new Padding(10, 0, 0, 0);
            //this.productButton.Click += new EventHandler(this.productButton_Click);

            // Home Expand Button
            this.productExpandButton.Text = "▼"; // символ треугольника
            this.productExpandButton.Dock = DockStyle.Right;
            this.productExpandButton.Width = 30;
            this.productExpandButton.FlatStyle = FlatStyle.Flat;
            this.productExpandButton.FlatAppearance.BorderSize = 0;
            this.productExpandButton.BackColor = Color.Gray;
            this.productExpandButton.ForeColor = Color.White;
            this.productExpandButton.Click += new EventHandler(this.productExpandButton_Click);

            // Add productExpandButton to productButton
            this.productButton.Controls.Add(this.productExpandButton);

            // SubMenu Panel
            this.subMenuPanel.Dock = DockStyle.Top;
            this.subMenuPanel.BackColor = Color.DarkGray;
            this.subMenuPanel.Height = 80;
            this.subMenuPanel.Visible = false;

            // SubItem1 Button
            this.subItem1Button.Text = "Category";
            this.subItem1Button.Dock = DockStyle.Top;
            this.subItem1Button.Height = 40;
            this.subItem1Button.FlatStyle = FlatStyle.Flat;
            this.subItem1Button.FlatAppearance.BorderSize = 0;
            this.subItem1Button.BackColor = Color.Gray;
            this.subItem1Button.ForeColor = Color.White;
            this.subItem1Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem1Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem1Button.Click += new EventHandler(this.SubItem1Button_Click);

            // SubItem2 Button
            this.subItem2Button.Text = "Supplier";
            this.subItem2Button.Dock = DockStyle.Top;
            this.subItem2Button.Height = 40;
            this.subItem2Button.FlatStyle = FlatStyle.Flat;
            this.subItem2Button.FlatAppearance.BorderSize = 0;
            this.subItem2Button.BackColor = Color.Gray;
            this.subItem2Button.ForeColor = Color.White;
            this.subItem2Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem2Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem2Button.Click += new EventHandler(this.SubItem2Button_Click);

            // Animation Timer
            this.animationTimer.Interval = 15;
            this.animationTimer.Tick += new EventHandler(this.AnimationTimer_Tick);


            this.stockorderButton.Text = "Stocks/Orders";
            this.stockorderButton.Dock = DockStyle.Top;
            this.stockorderButton.Height = 40;
            this.stockorderButton.FlatStyle = FlatStyle.Flat;
            this.stockorderButton.FlatAppearance.BorderSize = 0;
            this.stockorderButton.BackColor = Color.Gray;
            this.stockorderButton.ForeColor = Color.White;
            this.stockorderButton.TextAlign = ContentAlignment.MiddleLeft;
            this.stockorderButton.Padding = new Padding(10, 0, 0, 0);

            this.subMenuPanel2.Dock = DockStyle.Top;
            this.subMenuPanel2.BackColor = Color.DarkGray;
            this.subMenuPanel2.Height = 80;
            this.subMenuPanel2.Visible = false;

            this.subMenuPanel3.Dock = DockStyle.Top;
            this.subMenuPanel3.BackColor = Color.DarkGray;
            this.subMenuPanel3.Height = 120;
            this.subMenuPanel3.Visible = false;

            this.stockorderExpandButton.Text = "▼";
            this.stockorderExpandButton.Dock = DockStyle.Right;
            this.stockorderExpandButton.Width = 30;
            this.stockorderExpandButton.FlatStyle = FlatStyle.Flat;
            this.stockorderExpandButton.FlatAppearance.BorderSize = 0;
            this.stockorderExpandButton.BackColor = Color.Gray;
            this.stockorderExpandButton.ForeColor = Color.White;
            this.stockorderExpandButton.Click += new EventHandler(this.stockorderExpandButton_Click);
            this.stockorderButton.Controls.Add(this.stockorderExpandButton);

            this.userExpandButton.Text = "▼";
            this.userExpandButton.Dock = DockStyle.Right;
            this.userExpandButton.Width = 30;
            this.userExpandButton.FlatStyle = FlatStyle.Flat;
            this.userExpandButton.FlatAppearance.BorderSize = 0;
            this.userExpandButton.BackColor = Color.Gray;
            this.userExpandButton.ForeColor = Color.White;
            this.userExpandButton.Click += new EventHandler(this.userExpandButton_Click);
            this.userButton.Controls.Add(this.userExpandButton);

            this.subItem3Button.Text = "Stock";
            this.subItem3Button.Dock = DockStyle.Top;
            this.subItem3Button.Height = 40;
            this.subItem3Button.FlatStyle = FlatStyle.Flat;
            this.subItem3Button.FlatAppearance.BorderSize = 0;
            this.subItem3Button.BackColor = Color.Gray;
            this.subItem3Button.ForeColor = Color.White;
            this.subItem3Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem3Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem3Button.Click += new EventHandler(this.SubItem3Button_Click);


            this.subItem4Button.Text = "Order";
            this.subItem4Button.Dock = DockStyle.Top;
            this.subItem4Button.Height = 40;
            this.subItem4Button.FlatStyle = FlatStyle.Flat;
            this.subItem4Button.FlatAppearance.BorderSize = 0;
            this.subItem4Button.BackColor = Color.Gray;
            this.subItem4Button.ForeColor = Color.White;
            this.subItem4Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem4Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem4Button.Click += new EventHandler(this.SubItem4Button_Click);
            this.menuPanel.Controls.Add(this.subMenuPanel2);

            this.userButton.Text = "User";
            this.userButton.Dock = DockStyle.Top;
            this.userButton.Height = 40;
            this.userButton.FlatStyle = FlatStyle.Flat;
            this.userButton.FlatAppearance.BorderSize = 0;
            this.userButton.BackColor = Color.Gray;
            this.userButton.ForeColor = Color.White;
            this.userButton.TextAlign = ContentAlignment.MiddleLeft;
            this.userButton.Padding = new Padding(10, 0, 0, 0);

            this.subItem5Button.Text = "Users";
            this.subItem5Button.Dock = DockStyle.Top;
            this.subItem5Button.Height = 40;
            this.subItem5Button.FlatStyle = FlatStyle.Flat;
            this.subItem5Button.FlatAppearance.BorderSize = 0;
            this.subItem5Button.BackColor = Color.Gray;
            this.subItem5Button.ForeColor = Color.White;
            this.subItem5Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem5Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem5Button.Click += new EventHandler(this.SubItem5Button_Click);

            this.subItem6Button.Text = "Access";
            this.subItem6Button.Dock = DockStyle.Top;
            this.subItem6Button.Height = 40;
            this.subItem6Button.FlatStyle = FlatStyle.Flat;
            this.subItem6Button.FlatAppearance.BorderSize = 0;
            this.subItem6Button.BackColor = Color.Gray;
            this.subItem6Button.ForeColor = Color.White;
            this.subItem6Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem6Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem6Button.Click += new EventHandler(this.subItem6Button_Click);


            this.subItem7Button.Text = "Role";
            this.subItem7Button.Dock = DockStyle.Top;
            this.subItem7Button.Height = 40;
            this.subItem7Button.FlatStyle = FlatStyle.Flat;
            this.subItem7Button.FlatAppearance.BorderSize = 0;
            this.subItem7Button.BackColor = Color.Gray;
            this.subItem7Button.ForeColor = Color.White;
            this.subItem7Button.TextAlign = ContentAlignment.MiddleLeft;
            this.subItem7Button.Padding = new Padding(30, 0, 0, 0);
            this.subItem7Button.Click += new EventHandler(this.subItem7Button_Click);


            this.subMenuPanel3.Controls.Add(this.subItem6Button);
            this.subMenuPanel3.Controls.Add(this.subItem7Button);
            this.subMenuPanel3.Controls.Add(this.subItem5Button);
            this.subMenuPanel2.Controls.Add(this.subItem4Button);
            this.subMenuPanel2.Controls.Add(this.subItem3Button);
            this.subMenuPanel.Controls.Add(this.subItem2Button);
            this.subMenuPanel.Controls.Add(this.subItem1Button);


            this.menuPanel.Controls.Add(this.subMenuPanel3);
            this.menuPanel.Controls.Add(this.userButton);
            this.menuPanel.Controls.Add(this.stockorderButton);
            this.menuPanel.Controls.Add(this.subMenuPanel2);
            this.menuPanel.Controls.Add(this.stockorderButton);
            this.menuPanel.Controls.Add(this.subMenuPanel);
            this.menuPanel.Controls.Add(this.productButton);
            
            this.menuPanel.Controls.Add(this.toggleButton);
            SetActiveButton(productButton);

            this.Controls.Add(this.menuPanel);
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            this.animationTimer.Start();
            if (IsExpanded)
            {
                this.Width = 50;
            }
            else
            {
                this.Width = ExpandedWidth;
            }
            IsExpanded = !IsExpanded;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (this.isCollapsed)
            {
                this.menuPanel.Width += 20;
                if (this.menuPanel.Width >= 200)
                {
                    this.animationTimer.Stop();
                    this.isCollapsed = false;
                    this.Invalidate();
                }
            }
            else
            {
                this.menuPanel.Width -= 20;
                if (this.menuPanel.Width <= 60)
                {
                    this.animationTimer.Stop();
                    this.isCollapsed = true;
                    this.subMenuPanel.Visible = false; // Скрываем подпункты при сворачивании
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.isCollapsed)
            {
                this.toggleButton.Text = "☰";
                this.productButton.Text = "P";
                this.productExpandButton.Visible = false;
                this.stockorderExpandButton.Visible = false;
                this.userExpandButton.Visible = false;
                this.stockorderButton.Text = "S/O";
                this.userButton.Text = "U";
            }
            else
            {
                this.toggleButton.Text = "✕";
                this.productButton.Text = "Product";
                this.productExpandButton.Visible = true;
                this.stockorderExpandButton.Visible = true;
                this.userExpandButton.Visible = true;
                this.stockorderButton.Text = "Stocks/Orders";
                this.userButton.Text = "User";
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            private set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnMenuToggled();
                }
            }
        }

        protected virtual void OnMenuToggled()
        {
            MenuToggled?.Invoke(this, EventArgs.Empty);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != currentButton)
            {
                button.BackColor = Color.DarkGray;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != currentButton)
            {
                button.BackColor = Color.Gray;
            }
        }

        public void SetActiveButton(Button button)
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.Gray;
            }

            currentButton = button;
            currentButton.BackColor = Color.OrangeRed;
        }

        private void stockorderExpandButton_Click(object sender, EventArgs e)
        {
            this.subMenuPanel2.Visible = !this.subMenuPanel2.Visible;
            if (this.subMenuPanel2.Visible)
            {
                this.stockorderExpandButton.Text = "▲";
                this.Controls.Clear();
                this.Controls.Add(this.menuPanel);
            }
            else
            {
                this.stockorderExpandButton.Text = "▼";
            }
        }

        private void productExpandButton_Click(object sender, EventArgs e)
        {
            this.subMenuPanel.Visible = !this.subMenuPanel.Visible;
            if (this.subMenuPanel.Visible)
            {
                this.productExpandButton.Text = "▲";
                this.Controls.Clear();
                this.Controls.Add(this.menuPanel);
            }
            else
            {
                this.productExpandButton.Text = "▼";
            }
        }
        private void userExpandButton_Click(object sender, EventArgs e)
        {
            this.subMenuPanel3.Visible = !this.subMenuPanel3.Visible;
            if (this.subMenuPanel3.Visible)
            {
                this.userExpandButton.Text = "▲";
                this.Controls.Clear();
                this.Controls.Add(this.menuPanel);
            }
            else
            {
                this.userExpandButton.Text = "▼";
            }
        }

        private void SubItem1Button_Click(object sender, EventArgs e)
        {
            var category = new CategoryForm(httpClient);
            category.ShowDialog();
        }
        private void SubItem2Button_Click(object sender, EventArgs e)
        {
            var supplier = new SupplierForm(httpClient);
            supplier.ShowDialog();
        }
        private void SubItem3Button_Click(object sender, EventArgs e)
        {
            var stock = new StockGridForm(httpClient);
            stock.ShowDialog();
        }
        private void SubItem4Button_Click(object sender, EventArgs e)
        {
            var order = new OrderGrid(httpClient);
            order.ShowDialog();
        }
        private void SubItem5Button_Click(object sender, EventArgs e)
        {
            var users = new UsersForm(httpClient);
            users.ShowDialog();
        }
        private void subItem6Button_Click(object sender, EventArgs e)
        {
            var users = new AccessRightsForm(httpClient);
            users.ShowDialog();
        }
        private void subItem7Button_Click(object sender, EventArgs e)
        {
            var users = new RoleGridForm(httpClient);
            users.ShowDialog();
        }
    }
}
