using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    public class CustomComboBox : Control
    {
        private List<string> items;
        private int selectedIndex;
        private bool isDroppedDown;
        private Rectangle dropdownRect;
        private Timer dropdownTimer;
        private int dropdownHeight;
        private const int MaxVisibleItems = 3;
        private int itemHeight = 30;
        private VScrollBar vScrollBar;

        public event EventHandler SelectedIndexChanged;

        public CustomComboBox()
        {
            items = new List<string>();
            selectedIndex = -1;
            isDroppedDown = false;
            dropdownHeight = 0;
            dropdownRect = new Rectangle(0, this.Height, this.Width, dropdownHeight);

            this.Height = 30;
            this.Width = 150;
            this.DoubleBuffered = true;

            this.MouseClick += CustomComboBox_MouseClick;
            this.LostFocus += CustomComboBox_LostFocus;

            dropdownTimer = new Timer();
            dropdownTimer.Interval = 15; // Change the interval for smoother/faster animation
            dropdownTimer.Tick += DropdownTimer_Tick;

            vScrollBar = new VScrollBar
            {
                Dock = DockStyle.None,
                Visible = false,
                SmallChange = itemHeight,
                LargeChange = itemHeight,
                Maximum = 0
            };
            vScrollBar.Scroll += VScrollBar_Scroll;
            this.Controls.Add(vScrollBar);
        }

        [Browsable(true)]
        [Category("Data")]
        public List<string> Items
        {
            get { return items; }
            set
            {
                items = value;
                vScrollBar.Maximum = Math.Max(0, items.Count * itemHeight);
                vScrollBar.Visible = items.Count > MaxVisibleItems;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public string SelectedItem
        {
            get { return (selectedIndex >= 0 && selectedIndex < items.Count) ? items[selectedIndex] : null; }
        }

        private void CustomComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            isDroppedDown = !isDroppedDown;
            dropdownHeight = 0;
            dropdownTimer.Start();
        }

        private void CustomComboBox_LostFocus(object sender, EventArgs e)
        {
            if (isDroppedDown)
            {
                isDroppedDown = false;
                dropdownHeight = 0;
                dropdownRect = Rectangle.Empty;
                dropdownTimer.Stop();
                Invalidate();
            }
        }

        private void DropdownTimer_Tick(object sender, EventArgs e)
        {
            if (isDroppedDown)
            {
                dropdownHeight += 10;
                if (dropdownHeight >= Math.Min(items.Count, MaxVisibleItems) * itemHeight)
                {
                    dropdownHeight = items.Count * itemHeight;
                    dropdownTimer.Stop();
                }
            }
            else
            {
                dropdownHeight -= 10;
                if (dropdownHeight <= 0)
                {
                    dropdownHeight = 0;
                    dropdownTimer.Stop();
                }
            }
            dropdownRect = new Rectangle(0, 30, this.Width - 12, dropdownHeight);
            if (isDroppedDown)
                this.Height = 30 + (Math.Min(items.Count, MaxVisibleItems) * itemHeight) + (dropdownHeight > 0 ? 1 : 0);
            else
                this.Height = 30 + dropdownHeight;
            vScrollBar.Maximum = Math.Max(0, items.Count * itemHeight);
            vScrollBar.LargeChange = itemHeight;
            Invalidate();
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(this.Width - 11 - 10, 0, 10, 10, 270, 90);
                path.AddArc(this.Width - 11 - 10, 30 - 11, 10, 10, 0, 90);
                path.AddArc(0, 30 - 11, 10, 10, 90, 90);
                path.CloseFigure();
                g.FillPath(Brushes.White, path);
                g.DrawPath(Pens.Gray, path);
            }

            if (selectedIndex >= 0 && selectedIndex < items.Count)
            {
                TextRenderer.DrawText(g, items[selectedIndex], this.Font, new Rectangle(10, 5, this.Width - 30, this.Height - 10), Color.Black);
            }

            Point[] arrow = {
                new Point(this.Width - 25, (30 / 2) - 2),
                new Point(this.Width - 15, (30 / 2) - 2),
                new Point(this.Width - 20, (30 / 2) + 2)
            };
            g.FillPolygon(Brushes.BlueViolet, arrow);

            if (dropdownHeight > 0)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddRectangle(dropdownRect);
                    g.FillPath(Brushes.White, path);
                    g.DrawPath(Pens.Gray, path);
                }

                int visibleItemCount = dropdownHeight / itemHeight;
                int startIndex = vScrollBar.Value / itemHeight;

                for (int i = 0; i < visibleItemCount; i++)
                {
                    if (startIndex + i >= items.Count) break;
                    Rectangle itemRect = new Rectangle(0, 30 + (i * itemHeight), this.Width - 13, itemHeight);
                    if (itemRect.Contains(PointToClient(MousePosition)))
                    {
                        g.FillRectangle(Brushes.LightGray, itemRect);
                    }
                    TextRenderer.DrawText(g, items[startIndex + i], this.Font, itemRect, Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }

                if (items.Count > MaxVisibleItems)
                {
                    vScrollBar.Bounds = new Rectangle(this.Width - 10, 30, 10, MaxVisibleItems*itemHeight);
                    vScrollBar.Show();
                }
            }
            else
            {
                vScrollBar.Hide();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDroppedDown && dropdownRect.Contains(e.Location))
            {
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (isDroppedDown && dropdownRect.Contains(e.Location))
            {
                int index = (e.Y - 30) / itemHeight;
                int startIndex = vScrollBar.Value / itemHeight;
                if (index >= 0 && startIndex + index < items.Count)
                {
                    SelectedIndex = startIndex + index;
                    Invalidate();
                }
            }
        }
    }
}
