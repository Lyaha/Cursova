using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    public partial class RoundedTextBox : Control
    {
        public TextBox textBox;

        public RoundedTextBox()
        {
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(5, -6),
                Width = this.Width-5,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            this.Controls.Add(textBox);
            this.Height = textBox.Height + 10;
            this.Resize += (sender, e) =>
            {
                textBox.Width = this.Width-15;
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
    }
}