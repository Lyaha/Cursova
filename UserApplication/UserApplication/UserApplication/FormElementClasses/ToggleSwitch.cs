using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApplication.FormElementClasses
{
    public class ToggleSwitch : CheckBox
    {
        public ToggleSwitch()
        {
            this.Appearance = Appearance.Button;
            this.AutoSize = false;
            this.Size = new Size(50, 25);
            this.FlatStyle = FlatStyle.Flat;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Text = "OFF";
            this.BackColor = Color.LightGray;
            this.CheckedChanged += ToggleSwitch_CheckedChanged;
        }

        private void ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                this.Text = "ON";
                this.BackColor = Color.LightGreen;
            }
            else
            {
                this.Text = "OFF";
                this.BackColor = Color.LightGray;
            }
        }
    }
}
