using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    public class CustomCheckBox : CheckBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Height - 1, this.Height - 1);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect, 0, 360);
                this.Region = new Region(path);
            }

            ControlPaint.DrawBorder(e.Graphics, rect, Color.Gray, ButtonBorderStyle.Solid);

            if (this.Checked)
            {
                e.Graphics.FillEllipse(Brushes.LightGreen, rect);
            }
            else
            {
                e.Graphics.FillEllipse(Brushes.White, rect);
            }
        }
    }
}
