using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApplication
{
    public partial class RoundedTextBoxWithIcon : Control
    {
        private TextBox textBox;
        private Button searchButton;

        public RoundedTextBoxWithIcon()
        {
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(10, -5),
                Width = this.Width - 40,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            searchButton = new Button
            {
                Text = "🔍",
                Size = new Size(24, 24),
                Location = new Point(this.Width - 30, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                FlatAppearance = { BorderSize = 0 }
            };

            searchButton.Click += (sender, e) => OnSearchButtonClick(e);

            this.Controls.Add(textBox);
            this.Controls.Add(searchButton);
            this.Padding = new Padding(8);
            this.Height = textBox.Height + this.Padding.Top + this.Padding.Bottom;
            this.Resize += (sender, e) =>
            {
                textBox.Width = this.Width - 40;
                searchButton.Location = new Point(this.Width - 30, 3);
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(this.Width - 11, 0, 10, 10, 270, 90);
                path.AddArc(this.Width - 11, this.Height - 11, 10, 10, 0, 90);
                path.AddArc(0, this.Height - 11, 10, 10, 90, 90);
                path.CloseFigure();
                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(Pens.Gray, path);
            }
        }

        public string TextBoxText
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        public event EventHandler SearchButtonClick;

        protected virtual void OnSearchButtonClick(EventArgs e)
        {
            SearchButtonClick?.Invoke(this, e);
        }
    }
}
