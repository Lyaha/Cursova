using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UserApplication.AddProductForm;

namespace UserApplication
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, AutoSize = true};
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 90 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            prompt.ShowDialog();
            return textBox.Text;
        }
        public static string ShowDialog(string text, string text2, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 170,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Font = new Font("Arial", 9, FontStyle.Bold), AutoSize = true};
            Label textLabel2 = new Label() { Left = 50, Top = 40, Text = text2, Font = new Font("Arial", 7), AutoSize = true };
            TextBox textBox = new TextBox() { Left = 50, Top = 60, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 90 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel2);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            prompt.ShowDialog();
            return textBox.Text;
        }
        public static Supplier ShowDialog(string text, string text2, string text3, string text4, string caption )
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 300,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 200 };
            Label textLabel2 = new Label() { Left = 50, Top = 70, Text = text2 };
            TextBox textBox2 = new TextBox() { Left = 50, Top = 90, Width = 200 };
            Label textLabel3 = new Label() { Left = 50, Top = 120, Text = text3 };
            TextBox textBox3 = new TextBox() { Left = 50, Top = 140, Width = 200 };
            Label textLabel4 = new Label() { Left = 50, Top = 170, Text = text4 };
            TextBox textBox4 = new TextBox() { Left = 50, Top = 190, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 230 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(textLabel2);
            prompt.Controls.Add(textBox3);
            prompt.Controls.Add(textLabel3);
            prompt.Controls.Add(textBox4);
            prompt.Controls.Add(textLabel4);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            prompt.ShowDialog();
            var result = new Supplier
            {
                supplierName = textBox.Text,
                ContactPerson = textBox2.Text,
                ContactEmail = textBox3.Text,
                ContactPhone = textBox4.Text
            };
            return result;
        }
        public static Supplier ShowDialog(Supplier sup, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 300,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Supplier Name" };
            TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 200, Text = sup.supplierName };
            Label textLabel2 = new Label() { Left = 50, Top = 70, Text = "Contact person" };
            TextBox textBox2 = new TextBox() { Left = 50, Top = 90, Width = 200, Text = sup.ContactPerson };
            Label textLabel3 = new Label() { Left = 50, Top = 120, Text = "Contact email" };
            TextBox textBox3 = new TextBox() { Left = 50, Top = 140, Width = 200, Text = sup.ContactEmail };
            Label textLabel4 = new Label() { Left = 50, Top = 170, Text = "Contact phone" };
            TextBox textBox4 = new TextBox() { Left = 50, Top = 190, Width = 200, Text = sup.ContactPhone };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 230 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(textLabel2);
            prompt.Controls.Add(textBox3);
            prompt.Controls.Add(textLabel3);
            prompt.Controls.Add(textBox4);
            prompt.Controls.Add(textLabel4);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            prompt.ShowDialog();
            var result = new Supplier
            {
                supplierName = textBox.Text,
                ContactPerson = textBox2.Text,
                ContactEmail = textBox3.Text,
                ContactPhone = textBox4.Text
            };
            return result;
        }
    }
}
