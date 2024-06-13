using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    public partial class RoundedButton : Button
    {
        private StringFormat SF = new StringFormat();
        private bool MouseEntered = false;
        private bool MousePressed = false;

        public int Radius { get; set; } = 10;
        public Image Icon { get; set; }
        public ContentAlignment IconAlign { get; set; } = ContentAlignment.MiddleLeft;
        public int IconPadding { get; set; } = 5;

        public RoundedButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                true
            );

            DoubleBuffered = true;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            if (Radius <= 0) Radius = 1;
            GraphicsPath rectGp = RoundedRectangle(rect, Radius);

            graph.FillPath(new SolidBrush(BackColor), rectGp);

            // Рисуем иконку
            if (Icon != null)
            {
                Rectangle iconRect = GetIconRectangle();
                graph.DrawImage(Icon, iconRect);
            }

            // Рисуем текст с учетом отступов для иконки
            Rectangle textRect = rect;
            if (Icon != null)
            {
                int iconWidth = Icon.Width + IconPadding;
                if (IconAlign == ContentAlignment.MiddleLeft)
                    textRect = new Rectangle(rect.X + iconWidth, rect.Y, rect.Width - iconWidth, rect.Height);
                else if (IconAlign == ContentAlignment.MiddleRight)
                    textRect = new Rectangle(rect.X, rect.Y, rect.Width - iconWidth, rect.Height);
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), textRect, SF);

            // Эффекты наведения и нажатия
            if (MouseEntered)
            {
                graph.FillPath(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), rectGp);
            }
            if (MousePressed)
            {
                graph.FillPath(new SolidBrush(Color.FromArgb(50, Color.AliceBlue)), rectGp);
            }
        }

        private Rectangle GetIconRectangle()
        {
            Size iconSize = Icon.Size;
            Point iconLocation = new Point(0, 0);

            switch (IconAlign)
            {
                case ContentAlignment.TopLeft:
                    iconLocation = new Point(IconPadding, IconPadding);
                    break;
                case ContentAlignment.TopCenter:
                    iconLocation = new Point((Width - iconSize.Width) / 2, IconPadding);
                    break;
                case ContentAlignment.TopRight:
                    iconLocation = new Point(Width - iconSize.Width - IconPadding, IconPadding);
                    break;
                case ContentAlignment.MiddleLeft:
                    iconLocation = new Point(IconPadding, (Height - iconSize.Height) / 2);
                    break;
                case ContentAlignment.MiddleCenter:
                    iconLocation = new Point((Width - iconSize.Width) / 2, (Height - iconSize.Height) / 2);
                    break;
                case ContentAlignment.MiddleRight:
                    iconLocation = new Point(Width - iconSize.Width - IconPadding, (Height - iconSize.Height) / 2);
                    break;
                case ContentAlignment.BottomLeft:
                    iconLocation = new Point(IconPadding, Height - iconSize.Height - IconPadding);
                    break;
                case ContentAlignment.BottomCenter:
                    iconLocation = new Point((Width - iconSize.Width) / 2, Height - iconSize.Height - IconPadding);
                    break;
                case ContentAlignment.BottomRight:
                    iconLocation = new Point(Width - iconSize.Width - IconPadding, Height - iconSize.Height - IconPadding);
                    break;
            }

            return new Rectangle(iconLocation, iconSize);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseEntered = true;

            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseEntered = false;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;

            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MousePressed = true;

            Invalidate();
        }

        private GraphicsPath RoundedRectangle(Rectangle rect, int RoundSize)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, RoundSize, RoundSize, 180, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y, RoundSize, RoundSize, 270, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 90, 90);
            gp.CloseFigure();

            return gp;
        }
    }
}
