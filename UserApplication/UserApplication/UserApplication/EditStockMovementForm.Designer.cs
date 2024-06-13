using System.Drawing;
using System.Net.Http.Headers;
using System.Windows.Forms;
using UserApplication.FormElementClasses;

namespace UserApplication
{
    partial class EditStockMovementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        ///        
        private RoundedTextBox textBoxProductId, textBoxMovementTypeId, textBoxQuantity, textBoxBatchNumber, textBoxNotes;
        private Label labelProductId, labelMovementTypeId, labelQuantity, labelMovementDate, labelBatchNumber, labelNotes;
        private DateTimePicker dateTimePickerMovementDate;
        private RoundedButton buttonSave;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.textBoxProductId = new RoundedTextBox();
            this.textBoxMovementTypeId = new RoundedTextBox();
            this.textBoxQuantity = new RoundedTextBox();
            this.dateTimePickerMovementDate = new DateTimePicker();
            this.textBoxBatchNumber = new RoundedTextBox();
            this.textBoxNotes = new RoundedTextBox();
            this.buttonSave = new RoundedButton();
            this.SuspendLayout();

            this.labelProductId = new Label();
            this.labelMovementTypeId = new Label();
            this.labelQuantity = new Label();
            this.labelMovementDate = new Label();
            this.labelBatchNumber = new Label();
            this.labelNotes = new Label();

            this.SuspendLayout();

            // labelProductId
            this.labelProductId.Location = new System.Drawing.Point(12, 12);
            this.labelProductId.Name = "labelProductId";
            this.labelProductId.Size = new System.Drawing.Size(200, 20);
            this.labelProductId.Text = "Product ID:";

            // textBoxProductId
            this.textBoxProductId.Location = new System.Drawing.Point(12, 32);
            this.textBoxProductId.Name = "textBoxProductId";
            this.textBoxProductId.Size = new System.Drawing.Size(200, 20);
            this.textBoxProductId.TabIndex = 0;

            // labelMovementTypeId
            this.labelMovementTypeId.Location = new System.Drawing.Point(12, 58);
            this.labelMovementTypeId.Name = "labelMovementTypeId";
            this.labelMovementTypeId.Size = new System.Drawing.Size(200, 20);
            this.labelMovementTypeId.Text = "Movement Type ID:";

            // textBoxMovementTypeId
            this.textBoxMovementTypeId.Location = new System.Drawing.Point(12, 78);
            this.textBoxMovementTypeId.Name = "textBoxMovementTypeId";
            this.textBoxMovementTypeId.Size = new System.Drawing.Size(200, 20);
            this.textBoxMovementTypeId.TabIndex = 1;

            // labelQuantity
            this.labelQuantity.Location = new System.Drawing.Point(12, 104);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(200, 20);
            this.labelQuantity.Text = "Quantity:";

            // textBoxQuantity
            this.textBoxQuantity.Location = new System.Drawing.Point(12, 124);
            this.textBoxQuantity.Name = "textBoxQuantity";
            this.textBoxQuantity.Size = new System.Drawing.Size(200, 20);
            this.textBoxQuantity.TabIndex = 2;

            // labelMovementDate
            this.labelMovementDate.Location = new System.Drawing.Point(12, 150);
            this.labelMovementDate.Name = "labelMovementDate";
            this.labelMovementDate.Size = new System.Drawing.Size(200, 20);
            this.labelMovementDate.Text = "Movement Date:";

            // dateTimePickerMovementDate
            this.dateTimePickerMovementDate.Location = new System.Drawing.Point(12, 170);
            this.dateTimePickerMovementDate.Name = "dateTimePickerMovementDate";
            this.dateTimePickerMovementDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerMovementDate.TabIndex = 3;

            // labelBatchNumber
            this.labelBatchNumber.Location = new System.Drawing.Point(12, 196);
            this.labelBatchNumber.Name = "labelBatchNumber";
            this.labelBatchNumber.Size = new System.Drawing.Size(200, 20);
            this.labelBatchNumber.Text = "Batch Number:";

            // textBoxBatchNumber
            this.textBoxBatchNumber.Location = new System.Drawing.Point(12, 216);
            this.textBoxBatchNumber.Name = "textBoxBatchNumber";
            this.textBoxBatchNumber.Size = new System.Drawing.Size(200, 20);
            this.textBoxBatchNumber.TabIndex = 4;

            // labelNotes
            this.labelNotes.Location = new System.Drawing.Point(12, 242);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(200, 20);
            this.labelNotes.Text = "Notes:";

            // textBoxNotes
            this.textBoxNotes.Location = new System.Drawing.Point(12, 262);
            this.textBoxNotes.textBox.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.textBox.Location = new Point(10, -20);
            this.textBoxNotes.textBox.Size = new Size(180, 70);
            this.textBoxNotes.Size = new System.Drawing.Size(200, 80);
            this.textBoxNotes.TabIndex = 5;

            // buttonSave
            this.buttonSave.Location = new System.Drawing.Point(12, 348);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(200, 40);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.BackColor = Color.LightSeaGreen;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // EditStockMovementForm
            this.ClientSize = new System.Drawing.Size(224, 400);
            this.Controls.Add(this.labelProductId);
            this.Controls.Add(this.textBoxProductId);
            this.Controls.Add(this.labelMovementTypeId);
            this.Controls.Add(this.textBoxMovementTypeId);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.textBoxQuantity);
            this.Controls.Add(this.labelMovementDate);
            this.Controls.Add(this.dateTimePickerMovementDate);
            this.Controls.Add(this.labelBatchNumber);
            this.Controls.Add(this.textBoxBatchNumber);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.buttonSave);
            this.Name = "EditStockMovementForm";
            this.Text = "Edit Stock Movement";
            this.ResumeLayout(false);
            this.PerformLayout();
            #endregion
        }
    }
}

        