namespace Building
{
    partial class Form2
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
        private void InitializeComponent()
        {
            System.Windows.Forms.Label iD_FLOORLabel;
            System.Windows.Forms.Label lOCATION_BREACHLabel;
            System.Windows.Forms.Label tOPIC_BREACHLabel;
            System.Windows.Forms.Label dESCRIPTION_BREACHLabel;
            System.Windows.Forms.Label dATE_BREACHLabel;
            this.iD_FLOORTextBox = new System.Windows.Forms.TextBox();
            this.lOCATION_BREACHTextBox = new System.Windows.Forms.TextBox();
            this.tOPIC_BREACHTextBox = new System.Windows.Forms.TextBox();
            this.dATE_BREACHDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            iD_FLOORLabel = new System.Windows.Forms.Label();
            lOCATION_BREACHLabel = new System.Windows.Forms.Label();
            tOPIC_BREACHLabel = new System.Windows.Forms.Label();
            dESCRIPTION_BREACHLabel = new System.Windows.Forms.Label();
            dATE_BREACHLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iD_FLOORLabel
            // 
            iD_FLOORLabel.AutoSize = true;
            iD_FLOORLabel.Location = new System.Drawing.Point(184, 46);
            iD_FLOORLabel.Name = "iD_FLOORLabel";
            iD_FLOORLabel.Size = new System.Drawing.Size(78, 13);
            iD_FLOORLabel.TabIndex = 3;
            iD_FLOORLabel.Text = "Номер этажа:";
            iD_FLOORLabel.Click += new System.EventHandler(this.iD_FLOORLabel_Click);
            // 
            // lOCATION_BREACHLabel
            // 
            lOCATION_BREACHLabel.AutoSize = true;
            lOCATION_BREACHLabel.Location = new System.Drawing.Point(164, 75);
            lOCATION_BREACHLabel.Name = "lOCATION_BREACHLabel";
            lOCATION_BREACHLabel.Size = new System.Drawing.Size(98, 13);
            lOCATION_BREACHLabel.TabIndex = 5;
            lOCATION_BREACHLabel.Text = "Местоположение:";
            // 
            // tOPIC_BREACHLabel
            // 
            tOPIC_BREACHLabel.AutoSize = true;
            tOPIC_BREACHLabel.Location = new System.Drawing.Point(167, 104);
            tOPIC_BREACHLabel.Name = "tOPIC_BREACHLabel";
            tOPIC_BREACHLabel.Size = new System.Drawing.Size(95, 13);
            tOPIC_BREACHLabel.TabIndex = 7;
            tOPIC_BREACHLabel.Text = "Тема нарушения:";
            // 
            // dESCRIPTION_BREACHLabel
            // 
            dESCRIPTION_BREACHLabel.AutoSize = true;
            dESCRIPTION_BREACHLabel.Location = new System.Drawing.Point(144, 130);
            dESCRIPTION_BREACHLabel.Name = "dESCRIPTION_BREACHLabel";
            dESCRIPTION_BREACHLabel.Size = new System.Drawing.Size(118, 13);
            dESCRIPTION_BREACHLabel.TabIndex = 9;
            dESCRIPTION_BREACHLabel.Text = "Описание нарушения:";
            // 
            // dATE_BREACHLabel
            // 
            dATE_BREACHLabel.AutoSize = true;
            dATE_BREACHLabel.Location = new System.Drawing.Point(87, 282);
            dATE_BREACHLabel.Name = "dATE_BREACHLabel";
            dATE_BREACHLabel.Size = new System.Drawing.Size(175, 13);
            dATE_BREACHLabel.TabIndex = 11;
            dATE_BREACHLabel.Text = "Дата возникновения нарушения:";
            // 
            // iD_FLOORTextBox
            // 
            this.iD_FLOORTextBox.Location = new System.Drawing.Point(279, 43);
            this.iD_FLOORTextBox.Name = "iD_FLOORTextBox";
            this.iD_FLOORTextBox.Size = new System.Drawing.Size(200, 20);
            this.iD_FLOORTextBox.TabIndex = 1;
            // 
            // lOCATION_BREACHTextBox
            // 
            this.lOCATION_BREACHTextBox.Location = new System.Drawing.Point(279, 75);
            this.lOCATION_BREACHTextBox.Name = "lOCATION_BREACHTextBox";
            this.lOCATION_BREACHTextBox.Size = new System.Drawing.Size(200, 20);
            this.lOCATION_BREACHTextBox.TabIndex = 2;
            // 
            // tOPIC_BREACHTextBox
            // 
            this.tOPIC_BREACHTextBox.Location = new System.Drawing.Point(279, 104);
            this.tOPIC_BREACHTextBox.Name = "tOPIC_BREACHTextBox";
            this.tOPIC_BREACHTextBox.Size = new System.Drawing.Size(200, 20);
            this.tOPIC_BREACHTextBox.TabIndex = 3;
            // 
            // dATE_BREACHDateTimePicker
            // 
            this.dATE_BREACHDateTimePicker.Location = new System.Drawing.Point(279, 275);
            this.dATE_BREACHDateTimePicker.Name = "dATE_BREACHDateTimePicker";
            this.dATE_BREACHDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dATE_BREACHDateTimePicker.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(78, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(279, 312);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(279, 130);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(200, 125);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 370);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(iD_FLOORLabel);
            this.Controls.Add(this.iD_FLOORTextBox);
            this.Controls.Add(lOCATION_BREACHLabel);
            this.Controls.Add(this.lOCATION_BREACHTextBox);
            this.Controls.Add(tOPIC_BREACHLabel);
            this.Controls.Add(this.tOPIC_BREACHTextBox);
            this.Controls.Add(dESCRIPTION_BREACHLabel);
            this.Controls.Add(dATE_BREACHLabel);
            this.Controls.Add(this.dATE_BREACHDateTimePicker);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Building. Добавление информации о нарушениях";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource breachesBindingSource;
        private System.Windows.Forms.TextBox iD_FLOORTextBox;
        private System.Windows.Forms.TextBox lOCATION_BREACHTextBox;
        private System.Windows.Forms.TextBox tOPIC_BREACHTextBox;
        private System.Windows.Forms.DateTimePicker dATE_BREACHDateTimePicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}